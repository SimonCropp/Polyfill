#if FeatureMemory
using System.Buffers;
using System.IO;

partial class PolyfillTests
{
    [Test]
    public async Task ReadOnlySequenceStream_Capabilities()
    {
        using var stream = new ReadOnlySequenceStream(new ReadOnlySequence<byte>(new byte[] { 1, 2, 3 }));

        await Assert.That(stream.CanRead).IsTrue();
        await Assert.That(stream.CanSeek).IsTrue();
        await Assert.That(stream.CanWrite).IsFalse();
        await Assert.That(stream.Length).IsEqualTo(3L);
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

        await Assert.That(stream.Length).IsEqualTo(9L);

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
    public async Task ReadOnlySequenceStream_Seek()
    {
        using var stream = new ReadOnlySequenceStream(new ReadOnlySequence<byte>(new byte[] { 1, 2, 3, 4, 5 }));

        stream.Seek(3, SeekOrigin.Begin);
        await Assert.That(stream.Position).IsEqualTo(3L);
        await Assert.That(stream.ReadByte()).IsEqualTo(4);

        stream.Seek(-2, SeekOrigin.Current);
        await Assert.That(stream.ReadByte()).IsEqualTo(3);

        stream.Seek(-1, SeekOrigin.End);
        await Assert.That(stream.ReadByte()).IsEqualTo(5);
    }

    [Test]
    public async Task ReadOnlySequenceStream_SeekBeforeBeginThrows()
    {
        using var stream = new ReadOnlySequenceStream(new ReadOnlySequence<byte>(new byte[] { 1, 2, 3 }));

        await Assert.That(() => stream.Seek(-1, SeekOrigin.Begin)).Throws<IOException>();
    }

    [Test]
    public async Task ReadOnlySequenceStream_WritingThrows()
    {
        using var stream = new ReadOnlySequenceStream(new ReadOnlySequence<byte>(new byte[] { 1, 2, 3 }));

        await Assert.That(() => stream.Write(new byte[1], 0, 1)).Throws<NotSupportedException>();
        await Assert.That(() => stream.SetLength(1)).Throws<NotSupportedException>();
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

        await Assert.That(stream.Length).IsEqualTo(0L);
        await Assert.That(stream.Read(new byte[4], 0, 4)).IsEqualTo(0);
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
