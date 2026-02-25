// ReSharper disable ReplaceSliceWithRangeIndexer

using System.Security.Cryptography;

#pragma warning disable IDE0057

public class Sha256PolyfillTests
{
    static byte[] data = [1, 2, 3, 4, 5];
    static byte[] expected = [116, 248, 31, 225, 103, 217, 155, 76, 180, 29, 109, 12, 205, 168, 34, 120, 202, 238, 159, 62, 47, 37, 213, 229, 163, 147, 111, 243, 220, 236, 96, 208];

    [Test]
    public async Task HashData_ByteArray_ReturnsCorrectHash()
    {
        var actualHash = SHA256.HashData(data);
        await Assert.That(actualHash.SequenceEqual(expected)).IsTrue();
    }

    [Test]
    public async Task HashData_Stream_ReturnsCorrectHash()
    {
        using var stream = new MemoryStream(data);

        stream.Position = 0;

        // ReSharper disable once MethodHasAsyncOverload
        var actualHash = SHA256.HashData(stream);
        await Assert.That(actualHash.SequenceEqual(expected)).IsTrue();
    }

#if FeatureValueTask
    [Test]
    public async Task HashDataAsync_Stream_ReturnsCorrectHash()
    {
        using var stream = new MemoryStream(data);

        stream.Position = 0;

        var actualHash = await SHA256.HashDataAsync(stream);
        await Assert.That(actualHash.SequenceEqual(expected)).IsTrue();
    }
#endif

#if FeatureMemory
    [Test]
    public async Task HashData_ReadOnlySpan_ReturnsCorrectHash()
    {
        ReadOnlySpan<byte> span = data;

        var actualHash = SHA256.HashData(span);
        await Assert.That(actualHash.SequenceEqual(expected)).IsTrue();
    }

#if FeatureValueTask
    [Test]
    public async Task HashDataAsync_Memory_ReturnsCorrectHash()
    {
        using var stream = new MemoryStream(data);

        stream.Position = 0;
        Memory<byte> destination = new byte[expected.Length];

        var length = await SHA256.HashDataAsync(stream, destination);
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

        var length = SHA256.HashData(stream, destination);

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

        var length = SHA256.HashData(data, destination);

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
            SHA256.HashData(stream, destination);
        }).Throws<ArgumentException>();

        await Assert.That(exception!.Message.StartsWith("Destination is too short.")).IsTrue();
    }

    [Test]
    public async Task HashData_ReadOnlySpanAndSpan_ShorterDestination()
    {
        var exception = await Assert.That(() =>
        {
            Span<byte> destination = stackalloc byte[expected.Length - 1];
            SHA256.HashData(data, destination);
        }).Throws<ArgumentException>();

        await Assert.That(exception!.Message.StartsWith("Destination is too short.")).IsTrue();
    }


    [Test]
    public Task TryHashData_ValidInput_ReturnsTrueAndCorrectHash()
    {
        Span<byte> destination = stackalloc byte[expected.Length];

        var result = SHA256.TryHashData(data, destination, out var written);

        if (!result)
        {
            throw new("Expected true");
        }

        if (written != expected.Length)
        {
            throw new($"Expected written {expected.Length} but got {written}");
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

        var result = SHA256.TryHashData(data, destination, out var written);

        if (result)
        {
            throw new("Expected false");
        }

        if (written != 0)
        {
            throw new($"Expected written 0 but got {written}");
        }

        return Task.CompletedTask;
    }
#endif
}
