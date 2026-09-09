#if FeatureMemory
using System.IO;

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
