// ReSharper disable ReplaceSliceWithRangeIndexer

using System.Security.Cryptography;

#pragma warning disable IDE0057

public class Sha512PolyfillTests
{
    static byte[] data = [1, 2, 3, 4, 5];
    static byte[] expected = [80, 84, 11, 196, 174, 49, 135, 95, 206, 179, 130, 148, 52, 197, 94, 60, 43, 102, 221, 215, 34, 122, 136, 58, 59, 76, 200, 246, 205, 169, 101, 173, 23, 18, 179, 238, 0, 8, 249, 206, 224, 141, 169, 63, 82, 52, 193, 167, 191, 14, 37, 112, 239, 86, 214, 82, 128, 255, 234, 105, 27, 149, 62, 254];

    [Test]
    public async Task HashData_ByteArray_ReturnsCorrectHash()
    {
        var actualHash = SHA512.HashData(data);
        await Assert.That(actualHash.SequenceEqual(expected)).IsTrue();
    }

    [Test]
    public async Task HashData_Stream_ReturnsCorrectHash()
    {
        using var stream = new MemoryStream(data);

        stream.Position = 0;

        // ReSharper disable once MethodHasAsyncOverload
        var actualHash = SHA512.HashData(stream);
        await Assert.That(actualHash.SequenceEqual(expected)).IsTrue();
    }

#if FeatureValueTask
    [Test]
    public async Task HashDataAsync_Stream_ReturnsCorrectHash()
    {
        using var stream = new MemoryStream(data);

        stream.Position = 0;

        var actualHash = await SHA512.HashDataAsync(stream);
        await Assert.That(actualHash.SequenceEqual(expected)).IsTrue();
    }
#endif

#if FeatureMemory
    [Test]
    public async Task HashData_ReadOnlySpan_ReturnsCorrectHash()
    {
        ReadOnlySpan<byte> span = data;

        var actualHash = SHA512.HashData(span);
        await Assert.That(actualHash.SequenceEqual(expected)).IsTrue();
    }

#if FeatureValueTask
    [Test]
    public async Task HashDataAsync_Memory_ReturnsCorrectHash()
    {
        using var stream = new MemoryStream(data);

        stream.Position = 0;
        Memory<byte> destination = new byte[expected.Length];

        var length = await SHA512.HashDataAsync(stream, destination);
        await Assert.That(length).IsEqualTo(expected.Length);
        await Assert.That(expected.AsSpan().SequenceEqual(destination.Span)).IsTrue();
    }
#endif

    [Test]
    public Task HashData_StreamAndSpan_LongerDestination()
    {
        using var stream = new MemoryStream(data);
        Span<byte> destination = stackalloc byte[expected.Length + 1];

        stream.Position = 0;

        var length = SHA512.HashData(stream, destination);

        if (length != expected.Length)
        {
            throw new($"Expected length {expected.Length} but got {length}");
        }

        if (!expected.AsSpan().SequenceEqual(destination.Slice(0, expected.Length)))
        {
            throw new("Hash mismatch");
        }

        return Task.CompletedTask;
    }

    [Test]
    public Task HashData_ReadOnlySpanAndSpan_LongerDestination()
    {
        Span<byte> destination = stackalloc byte[expected.Length + 1];

        var length = SHA512.HashData(data, destination);

        if (length != expected.Length)
        {
            throw new($"Expected length {expected.Length} but got {length}");
        }

        if (!expected.AsSpan().SequenceEqual(destination.Slice(0, expected.Length)))
        {
            throw new("Hash mismatch");
        }

        return Task.CompletedTask;
    }

    [Test]
    public async Task HashData_StreamAndSpan_ShorterDestination()
    {
        using var stream = new MemoryStream(data);

        stream.Position = 0;

        var exception = await Assert.That(() =>
        {
            Span<byte> destination = stackalloc byte[expected.Length - 1];
            SHA512.HashData(stream, destination);
        }).Throws<ArgumentException>();

        await Assert.That(exception!.Message.StartsWith("Destination is too short.")).IsTrue();
    }

    [Test]
    public async Task HashData_ReadOnlySpanAndSpan_ShorterDestination()
    {
        var exception = await Assert.That(() =>
        {
            Span<byte> destination = stackalloc byte[expected.Length - 1];
            SHA512.HashData(data, destination);
        }).Throws<ArgumentException>();

        await Assert.That(exception!.Message.StartsWith("Destination is too short.")).IsTrue();
    }

    [Test]
    public Task TryHashData_ValidInput_ReturnsTrueAndCorrectHash()
    {
        Span<byte> destination = stackalloc byte[expected.Length];

        var result = SHA512.TryHashData(data, destination, out var bytesWritten);

        if (!result)
        {
            throw new("Expected true");
        }

        if (bytesWritten != expected.Length)
        {
            throw new($"Expected bytesWritten {expected.Length} but got {bytesWritten}");
        }

        if (!expected.AsSpan().SequenceEqual(destination.Slice(0, expected.Length)))
        {
            throw new("Hash mismatch");
        }

        return Task.CompletedTask;
    }

    [Test]
    public Task TryHashData_InsufficientDestinationBuffer_ReturnsFalse()
    {
        Span<byte> destination = stackalloc byte[expected.Length - 1];

        var result = SHA512.TryHashData(data, destination, out var bytesWritten);

        if (result)
        {
            throw new("Expected false");
        }

        if (bytesWritten != 0)
        {
            throw new($"Expected bytesWritten 0 but got {bytesWritten}");
        }

        return Task.CompletedTask;
    }
#endif
}
