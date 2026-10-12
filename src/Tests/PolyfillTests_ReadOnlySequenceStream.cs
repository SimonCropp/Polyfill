#if FeatureMemory
using System.Buffers;
using System.IO;
using System.Threading;
using System.Linq;

partial class PolyfillTests
{
    [Test]
    public async Task ReadOnlySequenceStream_Capabilities()
    {
        using var stream = new ReadOnlySequenceStream(new ReadOnlySequence<byte>(new byte[] { 1, 2, 3 }));

        await Assert.That(stream.CanRead).IsTrue();
        await Assert.That(stream.CanSeek).IsFalse();
        await Assert.That(stream.CanWrite).IsFalse();
    }

    [Test]
    public async Task ReadOnlySequenceStream_SingleSegment_ReadsAll()
    {
        using var stream = new ReadOnlySequenceStream(new ReadOnlySequence<byte>(new byte[] { 1, 2, 3, 4, 5 }));

        var result = ReadToEnd(stream);

        await Assert.That(result).IsEquivalentTo(new byte[] { 1, 2, 3, 4, 5 });
    }

    [Test]
    public async Task ReadOnlySequenceStream_MultiSegment_ReadsAcrossSegments()
    {
        var sequence = CreateMultiSegment(new byte[] { 1, 2, 3 }, new byte[] { 4, 5 }, new byte[] { 6, 7, 8, 9 });
        using var stream = new ReadOnlySequenceStream(sequence);

        // Read in small chunks so reads straddle segment boundaries.
        using var accumulator = new MemoryStream();
        var buffer = new byte[2];
        int read;
        while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
        {
            accumulator.Write(buffer, 0, read);
        }

        await Assert.That(accumulator.ToArray()).IsEquivalentTo(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
    }

    [Test]
    public async Task ReadOnlySequenceStream_ReadByte_AcrossSegments()
    {
        var sequence = CreateMultiSegment(new byte[] { 1 }, new byte[] { 2, 3 });
        using var stream = new ReadOnlySequenceStream(sequence);

        await Assert.That(stream.ReadByte()).IsEqualTo(1);
        await Assert.That(stream.ReadByte()).IsEqualTo(2);
        await Assert.That(stream.ReadByte()).IsEqualTo(3);
        await Assert.That(stream.ReadByte()).IsEqualTo(-1);
    }

    [Test]
    public async Task ReadOnlySequenceStream_SeekingThrows()
    {
        using var stream = new ReadOnlySequenceStream(new ReadOnlySequence<byte>(new byte[] { 1, 2, 3 }));

        await Assert.That(() => stream.Length).Throws<NotSupportedException>();
        await Assert.That(() => stream.Position).Throws<NotSupportedException>();
        await Assert.That(() => stream.Position = 1).Throws<NotSupportedException>();
        await Assert.That(() => stream.Seek(0, SeekOrigin.Begin)).Throws<NotSupportedException>();
        await Assert.That(() => stream.Seek(1, SeekOrigin.Current)).Throws<NotSupportedException>();
        await Assert.That(() => stream.Seek(0, SeekOrigin.End)).Throws<NotSupportedException>();
    }

    [Test]
    public async Task ReadOnlySequenceStream_ReadsAreForwardOnly()
    {
        var sequence = CreateMultiSegment(new byte[] { 1, 2, 3 }, new byte[] { 4, 5 });
        using var stream = new ReadOnlySequenceStream(sequence);

        // A failed seek attempt must not disturb the cursor.
        await Assert.That(stream.ReadByte()).IsEqualTo(1);
        await Assert.That(() => stream.Seek(0, SeekOrigin.Begin)).Throws<NotSupportedException>();
        await Assert.That(stream.ReadByte()).IsEqualTo(2);

        var rest = new byte[4];
        await Assert.That(stream.Read(rest, 0, 4)).IsEqualTo(3);
        await Assert.That(rest).IsEquivalentTo(new byte[] { 3, 4, 5, 0 });
        await Assert.That(stream.Read(rest, 0, 4)).IsEqualTo(0);
    }

    [Test]
    public async Task ReadOnlySequenceStream_WritingThrows()
    {
        using var stream = new ReadOnlySequenceStream(new ReadOnlySequence<byte>(new byte[] { 1, 2, 3 }));

        await Assert.That(() => stream.Write(new byte[1], 0, 1)).Throws<NotSupportedException>();
        await Assert.That(() => stream.SetLength(1)).Throws<NotSupportedException>();
    }

    // ReadAsync may hand back a cached task when consecutive reads return the same count, so pin that
    // every task carries the count of its own read, including tasks that are awaited only later.
    [Test]
    public async Task ReadOnlySequenceStream_ReadAsync_RepeatedReads_ReturnOwnCounts()
    {
        var data = new byte[10];
        for (var index = 0; index < data.Length; index++)
        {
            data[index] = (byte) (index + 1);
        }

        using var stream = new ReadOnlySequenceStream(CreateMultiSegment(data.AsSpan(0, 4).ToArray(), data.AsSpan(4, 1).ToArray(), data.AsSpan(5).ToArray()));
        var buffer = new byte[16];
        var requested = new[] {3, 3, 0, 3, 4, 0, 4, 4};
        var expected = new[] {3, 3, 0, 3, 1, 0, 0, 0};
        var tasks = new List<Task<int>>();
        var offset = 0;
        for (var index = 0; index < requested.Length; index++)
        {
            var task = stream.ReadAsync(buffer, offset, requested[index]);
            await Assert.That(task.IsCompleted).IsTrue();
            await Assert.That(task.Result).IsEqualTo(expected[index]);
            tasks.Add(task);
            offset += expected[index];
        }

        // Earlier tasks still report their own count after later reads returned different ones
        for (var index = 0; index < tasks.Count; index++)
        {
            await Assert.That(await tasks[index]).IsEqualTo(expected[index]);
        }

        await Assert.That(buffer.AsSpan(0, 10).SequenceEqual(data)).IsTrue();
        await Assert.That(buffer.AsSpan(10).ToArray().All(_ => _ == 0)).IsTrue();
    }

    [Test]
    public async Task ReadOnlySequenceStream_ReadAsync_Canceled_DoesNotDisturbLaterReads()
    {
        using var stream = new ReadOnlySequenceStream(CreateMultiSegment(new byte[] {1, 2, 3}, new byte[] {4, 5}));
        var buffer = new byte[4];

        await Assert.That(await stream.ReadAsync(buffer, 0, 2)).IsEqualTo(2);

        var canceled = stream.ReadAsync(buffer, 2, 2, new CancellationToken(true));
        await Assert.That(canceled.IsCanceled).IsTrue();

        // The canceled read consumed nothing
        await Assert.That(await stream.ReadAsync(buffer, 2, 2)).IsEqualTo(2);
        await Assert.That(buffer).IsEquivalentTo(new byte[] {1, 2, 3, 4});
        await Assert.That(await stream.ReadAsync(buffer, 0, 2)).IsEqualTo(1);
        await Assert.That(await stream.ReadAsync(buffer, 0, 2)).IsEqualTo(0);
    }

    [Test]
    public async Task ReadOnlySequenceStream_ReadAsync()
    {
        using var stream = new ReadOnlySequenceStream(new ReadOnlySequence<byte>(new byte[] { 1, 2, 3, 4 }));

        var buffer = new byte[4];
        var read = await stream.ReadAsync(buffer, 0, 4);

        await Assert.That(read).IsEqualTo(4);
        await Assert.That(buffer).IsEquivalentTo(new byte[] { 1, 2, 3, 4 });
    }

    [Test]
    public async Task ReadOnlySequenceStream_Empty()
    {
        using var stream = new ReadOnlySequenceStream(ReadOnlySequence<byte>.Empty);

        await Assert.That(stream.Read(new byte[4], 0, 4)).IsEqualTo(0);
        await Assert.That(stream.ReadByte()).IsEqualTo(-1);
    }

    [Test]
    public async Task ReadOnlySequenceStream_CopyTo_MultiSegment()
    {
        var sequence = CreateMultiSegment(new byte[] { 1, 2, 3 }, new byte[] { 4, 5 }, new byte[] { 6, 7, 8, 9 });
        using var stream = new ReadOnlySequenceStream(sequence);
        using var target = new MemoryStream();

        stream.CopyTo(target);

        await Assert.That(target.ToArray()).IsEquivalentTo(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
        await Assert.That(stream.Read(new byte[1], 0, 1)).IsEqualTo(0);
    }

    [Test]
    public async Task ReadOnlySequenceStream_CopyTo_AfterPartialRead_CopiesRemainder()
    {
        var sequence = CreateMultiSegment(new byte[] { 1, 2, 3 }, new byte[] { 4, 5, 6 });
        using var stream = new ReadOnlySequenceStream(sequence);

        var head = new byte[2];
        stream.Read(head, 0, 2);

        using var target = new MemoryStream();
        stream.CopyTo(target);

        await Assert.That(target.ToArray()).IsEquivalentTo(new byte[] { 3, 4, 5, 6 });
        await Assert.That(stream.Read(new byte[1], 0, 1)).IsEqualTo(0);
    }

    [Test]
    public async Task ReadOnlySequenceStream_CopyToAsync_MultiSegment()
    {
        var sequence = CreateMultiSegment(new byte[] { 1, 2 }, new byte[] { 3, 4, 5 });
        using var stream = new ReadOnlySequenceStream(sequence);
        using var target = new MemoryStream();

        await stream.CopyToAsync(target);

        await Assert.That(target.ToArray()).IsEquivalentTo(new byte[] { 1, 2, 3, 4, 5 });
        await Assert.That(stream.Read(new byte[1], 0, 1)).IsEqualTo(0);
    }

    static ReadOnlySequence<byte> CreateMultiSegment(params byte[][] parts)
    {
        var first = new ReadOnlySequenceStreamSegment(parts[0]);
        var last = first;
        for (var index = 1; index < parts.Length; index++)
        {
            last = last.Append(parts[index]);
        }

        return new(first, 0, last, last.Memory.Length);
    }

    class ReadOnlySequenceStreamSegment :
        ReadOnlySequenceSegment<byte>
    {
        public ReadOnlySequenceStreamSegment(ReadOnlyMemory<byte> memory) =>
            Memory = memory;

        public ReadOnlySequenceStreamSegment Append(ReadOnlyMemory<byte> memory)
        {
            var segment = new ReadOnlySequenceStreamSegment(memory)
            {
                RunningIndex = RunningIndex + Memory.Length
            };
            Next = segment;
            return segment;
        }
    }
}
#endif
