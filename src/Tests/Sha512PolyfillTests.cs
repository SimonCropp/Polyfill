// ReSharper disable ReplaceSliceWithRangeIndexer

using System.Security.Cryptography;

#pragma warning disable IDE0057

[TestFixture]
public class Sha512PolyfillTests
{
    static byte[] data = [1, 2, 3, 4, 5];
    static byte[] expected = [80, 84, 11, 196, 174, 49, 135, 95, 206, 179, 130, 148, 52, 197, 94, 60, 43, 102, 221, 215, 34, 122, 136, 58, 59, 76, 200, 246, 205, 169, 101, 173, 23, 18, 179, 238, 0, 8, 249, 206, 224, 141, 169, 63, 82, 52, 193, 167, 191, 14, 37, 112, 239, 86, 214, 82, 128, 255, 234, 105, 27, 149, 62, 254];

    [Test]
    public void HashData_ByteArray_ReturnsCorrectHash()
    {
        var actualHash = SHA512.HashData(data);
        Assert.AreEqual(expected, actualHash);
    }

    [Test]
    public void HashData_Stream_ReturnsCorrectHash()
    {
        using var stream = new MemoryStream(data);

        stream.Position = 0;

        var actualHash = SHA512.HashData(stream);
        Assert.AreEqual(expected, actualHash);
    }

#if FeatureValueTask
    [Test]
    public async Task HashDataAsync_Stream_ReturnsCorrectHash()
    {
        using var stream = new MemoryStream(data);

        stream.Position = 0;

        var actualHash = await SHA512.HashDataAsync(stream);
        Assert.AreEqual(expected, actualHash);
    }
#endif

#if FeatureMemory
    [Test]
    public void HashData_ReadOnlySpan_ReturnsCorrectHash()
    {
        ReadOnlySpan<byte> span = data;

        var actualHash = SHA512.HashData(span);
        Assert.AreEqual(expected, actualHash);
    }

#if FeatureValueTask
    [Test]
    public async Task HashDataAsync_Memory_ReturnsCorrectHash()
    {
        using var stream = new MemoryStream(data);

        stream.Position = 0;
        Memory<byte> destination = new byte[expected.Length];

        var length = await SHA512Polyfill.HashDataAsync(stream, destination);
        Assert.AreEqual(expected.Length, length);
        Assert.IsTrue(expected.AsSpan().SequenceEqual(destination.Span));
    }
#endif

    [Test]
    public void HashData_StreamAndSpan_LongerDestination()
    {
        using var stream = new MemoryStream(data);
        Span<byte> destination = stackalloc byte[expected.Length + 1];

        stream.Position = 0;

        var length = SHA512Polyfill.HashData(stream, destination);

        Assert.AreEqual(expected.Length, length);
        Assert.IsTrue(expected.AsSpan().SequenceEqual(destination.Slice(0, expected.Length)));
    }

    [Test]
    public void HashData_ReadOnlySpanAndSpan_LongerDestination()
    {
        Span<byte> destination = stackalloc byte[expected.Length + 1];

        var length = SHA512Polyfill.HashData(data, destination);

        Assert.AreEqual(expected.Length, length);
        Assert.IsTrue(expected.AsSpan().SequenceEqual(destination.Slice(0, expected.Length)));
    }

    [Test]
    public void HashData_StreamAndSpan_ShorterDestination()
    {
        using var stream = new MemoryStream(data);

        stream.Position = 0;

        var exception = Assert.Throws<ArgumentException>(() =>
        {
            Span<byte> destination = stackalloc byte[expected.Length - 1];
            SHA512Polyfill.HashData(stream, destination);
        });

        Assert.True(exception!.Message.StartsWith("Destination is too short."));
    }

    [Test]
    public void HashData_ReadOnlySpanAndSpan_ShorterDestination()
    {
        var exception = Assert.Throws<ArgumentException>(() =>
        {
            Span<byte> destination = stackalloc byte[expected.Length - 1];
            SHA512Polyfill.HashData(data, destination);
        });

        Assert.True(exception!.Message.StartsWith("Destination is too short."));
    }


    [Test]
    public void TryHashData_ValidInput_ReturnsTrueAndCorrectHash()
    {
        Span<byte> destination = stackalloc byte[expected.Length];

        var result = SHA512Polyfill.TryHashData(data, destination, out var bytesWritten);

        Assert.IsTrue(result);
        Assert.AreEqual(expected.Length, bytesWritten);
        Assert.IsTrue(expected.AsSpan().SequenceEqual(destination.Slice(0, expected.Length)));
    }

    [Test]
    public void TryHashData_InsufficientDestinationBuffer_ReturnsFalse()
    {
        Span<byte> destination = stackalloc byte[expected.Length - 1];

        var result = SHA512Polyfill.TryHashData(data, destination, out var bytesWritten);

        Assert.IsFalse(result);
        Assert.AreEqual(0, bytesWritten);
    }
#endif
}