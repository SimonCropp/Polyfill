[TestFixture]
public class Sha256PolyfillTests
{
    static byte[] data = [1, 2, 3, 4, 5];
    static byte[] expected = [116, 248, 31, 225, 103, 217, 155, 76, 180, 29, 109, 12, 205, 168, 34, 120, 202, 238, 159, 62, 47, 37, 213, 229, 163, 147, 111, 243, 220, 236, 96, 208];

    [Test]
    public void HashData_ByteArray_ReturnsCorrectHash()
    {
        var actualHash = SHA256Polyfill.HashData(data);
        Assert.AreEqual(expected, actualHash);
    }

    [Test]
    public void HashData_Stream_ReturnsCorrectHash()
    {
        using var stream = new MemoryStream(data);

        stream.Position = 0;

        var actualHash = SHA256Polyfill.HashData(stream);
        Assert.AreEqual(expected, actualHash);
    }

    [Test]
    public async Task HashDataAsync_Stream_ReturnsCorrectHash()
    {
        using var stream = new MemoryStream(data);

        stream.Position = 0;

        var actualHash = await SHA256Polyfill.HashDataAsync(stream);
        Assert.AreEqual(expected, actualHash);
    }

#if FeatureMemory
    [Test]
    public void HashData_ReadOnlySpan_ReturnsCorrectHash()
    {
        ReadOnlySpan<byte> span = data;

        var actualHash = SHA256Polyfill.HashData(span);
        Assert.AreEqual(expected, actualHash);
    }

    [Test]
    public async Task HashDataAsync_Memory_ReturnsCorrectHash()
    {
        using var stream = new MemoryStream(data);

        stream.Position = 0;
        Memory<byte> destination = new byte[32];

        var length = await SHA256Polyfill.HashDataAsync(stream, destination);
        Assert.AreEqual(32, length);
        Assert.IsTrue(expected.AsSpan().SequenceEqual(destination.Span));
    }

    [Test]
    public void HashData_StreamAndSpan_LongerDestination()
    {
        using var stream = new MemoryStream(data);
        Span<byte> destination = stackalloc byte[35];

        stream.Position = 0;

        var length = SHA256Polyfill.HashData(stream, destination);

        Assert.AreEqual(32, length);
        Assert.IsTrue(expected.AsSpan().SequenceEqual(destination[..32]));
    }

    [Test]
    public void HashData_ReadOnlySpanAndSpan_LongerDestination()
    {
        Span<byte> destination = stackalloc byte[35];

        var length = SHA256Polyfill.HashData(data, destination);

        Assert.AreEqual(32, length);
        Assert.IsTrue(expected.AsSpan().SequenceEqual(destination[..32]));
    }

    [Test]
    public void HashData_StreamAndSpan_ShorterDestination()
    {
        using var stream = new MemoryStream(data);

        stream.Position = 0;

        var exception = Assert.Throws<ArgumentException>(() =>
        {
            Span<byte> destination = stackalloc byte[30];
            SHA256Polyfill.HashData(stream, destination);
        });

        Assert.True(exception!.Message.StartsWith("Destination is too short."));
    }

    [Test]
    public void HashData_ReadOnlySpanAndSpan_ShorterDestination()
    {
        var exception = Assert.Throws<ArgumentException>(() =>
        {
            Span<byte> destination = stackalloc byte[30];
            SHA256Polyfill.HashData(data, destination);
        });

        Assert.True(exception!.Message.StartsWith("Destination is too short."));
    }

#endif
}