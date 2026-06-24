#if FeatureMemory
using System.IO;

partial class PolyfillTests
{
    [Test]
    public async Task WritableMemoryStream_IsAMemoryStream()
    {
        using var stream = new WritableMemoryStream(new byte[4]);

        await Assert.That(stream).IsAssignableTo<MemoryStream>();
    }

    [Test]
    public async Task WritableMemoryStream_StartsEmptyWithFixedCapacity()
    {
        using var stream = new WritableMemoryStream(new byte[8]);

        await Assert.That(stream.Length).IsEqualTo(0L);
        await Assert.That(stream.Position).IsEqualTo(0L);
        await Assert.That(stream.CanWrite).IsTrue();
        await Assert.That(stream.CanRead).IsTrue();
        await Assert.That(stream.CanSeek).IsTrue();
        await Assert.That(stream.Capacity).IsEqualTo(8);
    }

    [Test]
    public async Task WritableMemoryStream_WritesIntoBackingArray()
    {
        var backing = new byte[5];
        using var stream = new WritableMemoryStream(backing);

        stream.Write(new byte[] { 1, 2, 3 }, 0, 3);

        await Assert.That(stream.Length).IsEqualTo(3L);
        await Assert.That(backing).IsEquivalentTo(new byte[] { 1, 2, 3, 0, 0 });
    }

    [Test]
    public async Task WritableMemoryStream_WritesIntoSlicedBackingMemory()
    {
        var backing = new byte[8];
        using var stream = new WritableMemoryStream(backing.AsMemory(2, 3));

        stream.WriteByte(7);
        stream.WriteByte(8);

        await Assert.That(backing).IsEquivalentTo(new byte[] { 0, 0, 7, 8, 0, 0, 0, 0 });
    }

    [Test]
    public async Task WritableMemoryStream_ReadBackWhatWasWritten()
    {
        using var stream = new WritableMemoryStream(new byte[16]);
        stream.Write(new byte[] { 4, 5, 6 }, 0, 3);

        stream.Position = 0;
        var buffer = new byte[3];
        var read = stream.Read(buffer, 0, 3);

        await Assert.That(read).IsEqualTo(3);
        await Assert.That(buffer).IsEquivalentTo(new byte[] { 4, 5, 6 });
    }

    [Test]
    public async Task WritableMemoryStream_WritingBeyondCapacityThrows()
    {
        using var stream = new WritableMemoryStream(new byte[3]);

        await Assert.That(() => stream.Write(new byte[] { 1, 2, 3, 4 }, 0, 4)).Throws<NotSupportedException>();
    }

    [Test]
    public async Task WritableMemoryStream_SetLengthThrows()
    {
        using var stream = new WritableMemoryStream(new byte[4]);

        await Assert.That(() => stream.SetLength(2)).Throws<NotSupportedException>();
    }

    [Test]
    public async Task WritableMemoryStream_GetBufferThrows_TryGetBufferFalse()
    {
        using var stream = new WritableMemoryStream(new byte[4]);

        await Assert.That(() => stream.GetBuffer()).Throws<UnauthorizedAccessException>();
        await Assert.That(stream.TryGetBuffer(out var segment)).IsFalse();
        await Assert.That(segment.Array).IsNull();
    }

    [Test]
    public async Task WritableMemoryStream_ToArrayReturnsWrittenContent()
    {
        using var stream = new WritableMemoryStream(new byte[16]);
        stream.Write(new byte[] { 1, 2, 3 }, 0, 3);

        await Assert.That(stream.ToArray()).IsEquivalentTo(new byte[] { 1, 2, 3 });
    }
}
#endif
