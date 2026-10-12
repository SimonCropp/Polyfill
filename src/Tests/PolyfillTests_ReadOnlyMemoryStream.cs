#if FeatureMemory
using System.IO;
using System.Threading;
using System.Linq;

partial class PolyfillTests
{
    [Test]
    public async Task ReadOnlyMemoryStream_IsAStream()
    {
        using var stream = new ReadOnlyMemoryStream(new byte[] { 1, 2, 3 });

        await Assert.That(stream).IsAssignableTo<Stream>();
    }

    [Test]
    public async Task ReadOnlyMemoryStream_Capabilities()
    {
        using var stream = new ReadOnlyMemoryStream(new byte[] { 1, 2, 3 });

        await Assert.That(stream.CanRead).IsTrue();
        await Assert.That(stream.CanSeek).IsTrue();
        await Assert.That(stream.CanWrite).IsFalse();
        await Assert.That(stream.Length).IsEqualTo(3L);
    }

    [Test]
    public async Task ReadOnlyMemoryStream_ReadsAllBytes()
    {
        using var stream = new ReadOnlyMemoryStream(new byte[] { 10, 20, 30, 40, 50 });

        var buffer = new byte[5];
        var read = stream.Read(buffer, 0, 5);

        await Assert.That(read).IsEqualTo(5);
        await Assert.That(buffer).IsEquivalentTo(new byte[] { 10, 20, 30, 40, 50 });
    }

    [Test]
    public async Task ReadOnlyMemoryStream_WorksWithSlicedMemory()
    {
        var largeBuffer = new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        var slice = largeBuffer.AsMemory(3, 4);
        using var stream = new ReadOnlyMemoryStream(slice);

        await Assert.That(stream.Length).IsEqualTo(4L);

        var result = new byte[4];
        var read = stream.Read(result, 0, 4);

        await Assert.That(read).IsEqualTo(4);
        await Assert.That(result).IsEquivalentTo(new byte[] { 3, 4, 5, 6 });
    }

    [Test]
    public async Task ReadOnlyMemoryStream_SeekAndPosition()
    {
        using var stream = new ReadOnlyMemoryStream(new byte[] { 1, 2, 3, 4, 5 });

        stream.Seek(2, SeekOrigin.Begin);
        await Assert.That(stream.Position).IsEqualTo(2L);
        await Assert.That(stream.ReadByte()).IsEqualTo(3);

        stream.Position = 0;
        await Assert.That(stream.ReadByte()).IsEqualTo(1);
    }

    [Test]
    public async Task ReadOnlyMemoryStream_SeekToEnd()
    {
        using var stream = new ReadOnlyMemoryStream(new byte[] { 1, 2, 3 });

        await Assert.That(stream.Seek(0, SeekOrigin.End)).IsEqualTo(3L);
        await Assert.That(stream.ReadByte()).IsEqualTo(-1);
    }

    [Test]
    public async Task ReadOnlyMemoryStream_SeekBeforeBeginThrows()
    {
        using var stream = new ReadOnlyMemoryStream(new byte[] { 1, 2, 3 });

        await Assert.That(() => stream.Seek(-1, SeekOrigin.Begin)).Throws<IOException>();
    }

    [Test]
    public async Task ReadOnlyMemoryStream_PositionPastEndReadsNothing()
    {
        using var stream = new ReadOnlyMemoryStream(new byte[] { 1, 2, 3 });

        stream.Position = 100;

        await Assert.That(stream.Position).IsEqualTo(100L);
        await Assert.That(stream.ReadByte()).IsEqualTo(-1);
        await Assert.That(stream.Read(new byte[4], 0, 4)).IsEqualTo(0);
    }

    [Test]
    public async Task ReadOnlyMemoryStream_NegativePositionThrows()
    {
        using var stream = new ReadOnlyMemoryStream(new byte[] { 1, 2, 3 });

        await Assert.That(() => { stream.Position = -1; }).Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task ReadOnlyMemoryStream_ReadIsBoundedByLength()
    {
        using var stream = new ReadOnlyMemoryStream(new byte[] { 1, 2, 3 });

        var buffer = new byte[10];
        var read = stream.Read(buffer, 0, 10);

        await Assert.That(read).IsEqualTo(3);
        await Assert.That(buffer).IsEquivalentTo(new byte[] { 1, 2, 3, 0, 0, 0, 0, 0, 0, 0 });
    }

    [Test]
    public async Task ReadOnlyMemoryStream_WritingThrows()
    {
        using var stream = new ReadOnlyMemoryStream(new byte[] { 1, 2, 3 });

        await Assert.That(() => stream.Write(new byte[1], 0, 1)).Throws<NotSupportedException>();
        await Assert.That(() => stream.WriteByte(1)).Throws<NotSupportedException>();
        await Assert.That(() => stream.SetLength(2)).Throws<NotSupportedException>();
    }

    [Test]
    public async Task ReadOnlyMemoryStream_CopyTo()
    {
        using var stream = new ReadOnlyMemoryStream(new byte[] { 1, 2, 3, 4 });
        using var target = new MemoryStream();

        stream.CopyTo(target);

        await Assert.That(target.ToArray()).IsEquivalentTo(new byte[] { 1, 2, 3, 4 });
    }

    // ReadAsync may hand back a cached task when consecutive reads return the same count, so pin that
    // every task carries the count of its own read, including tasks that are awaited only later.
    [Test]
    public async Task ReadOnlyMemoryStream_ReadAsync_RepeatedReads_ReturnOwnCounts()
    {
        var data = new byte[10];
        for (var index = 0; index < data.Length; index++)
        {
            data[index] = (byte) (index + 1);
        }

        using var stream = new ReadOnlyMemoryStream(data);
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
    public async Task ReadOnlyMemoryStream_ReadAsync_Canceled_DoesNotDisturbLaterReads()
    {
        using var stream = new ReadOnlyMemoryStream(new byte[] {1, 2, 3, 4, 5});
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
    public async Task ReadOnlyMemoryStream_ReadAsync()
    {
        using var stream = new ReadOnlyMemoryStream(new byte[] { 1, 2, 3, 4 });

        var buffer = new byte[4];
        var read = await stream.ReadAsync(buffer, 0, 4);

        await Assert.That(read).IsEqualTo(4);
        await Assert.That(buffer).IsEquivalentTo(new byte[] { 1, 2, 3, 4 });
    }

    [Test]
    public async Task ReadOnlyMemoryStream_FromMemoryViaImplicitConversion()
    {
        var buffer = new byte[] { 1, 2, 3, 4, 5 };
        Memory<byte> memory = buffer;
        using var stream = new ReadOnlyMemoryStream(memory);

        await Assert.That(stream.Length).IsEqualTo(5L);
        await Assert.That(stream.CanRead).IsTrue();
    }

    [Test]
    public async Task ReadOnlyMemoryStream_Empty()
    {
        using var stream = new ReadOnlyMemoryStream(ReadOnlyMemory<byte>.Empty);

        await Assert.That(stream.Length).IsEqualTo(0L);
        await Assert.That(stream.Read(new byte[4], 0, 4)).IsEqualTo(0);
    }

    [Test]
    public async Task ReadOnlyMemoryStream_DisposedThrows()
    {
        var stream = new ReadOnlyMemoryStream(new byte[] { 1, 2, 3 });
        stream.Dispose();

        await Assert.That(stream.CanRead).IsFalse();
        await Assert.That(stream.CanSeek).IsFalse();
        await Assert.That(stream.CanWrite).IsFalse();
        await Assert.That(() => stream.Length).Throws<ObjectDisposedException>();
    }
}
#endif
