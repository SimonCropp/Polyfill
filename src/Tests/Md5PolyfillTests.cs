// ReSharper disable ReplaceSliceWithRangeIndexer

using System.Security.Cryptography;

#pragma warning disable IDE0057

public class Md5PolyfillTests
{
    static byte[] data = [1, 2, 3, 4, 5];

    // MD5 hash of [1, 2, 3, 4, 5]
    static byte[] expected = [124, 253, 208, 120, 137, 179, 41, 93, 106, 85, 9, 20, 171, 53, 224, 104];

    [Test]
    public async Task HashData_ByteArray_ReturnsCorrectHash()
    {
        var actualHash = MD5.HashData(data);
        await Assert.That(actualHash.SequenceEqual(expected)).IsTrue();
    }

    [Test]
    public async Task HashData_Stream_ReturnsCorrectHash()
    {
        using var stream = new MemoryStream(data);

        stream.Position = 0;

        // ReSharper disable once MethodHasAsyncOverload
        var actualHash = MD5.HashData(stream);
        await Assert.That(actualHash.SequenceEqual(expected)).IsTrue();
    }

#if FeatureValueTask
    [Test]
    public async Task HashDataAsync_Stream_ReturnsCorrectHash()
    {
        using var stream = new MemoryStream(data);

        stream.Position = 0;

        var actualHash = await MD5.HashDataAsync(stream);
        await Assert.That(actualHash.SequenceEqual(expected)).IsTrue();
    }

    [Test]
    public async Task HashDataAsync_Stream_ReadsAsynchronously()
    {
        using var stream = new AsyncOnlyStream(data);

        var actualHash = await MD5.HashDataAsync(stream);
        await Assert.That(actualHash.SequenceEqual(expected)).IsTrue();
    }

    [Test]
    public async Task HashDataAsync_Stream_PreCancelled_ReturnsCancelledTask()
    {
        using var stream = new AsyncOnlyStream(data);

        var task = MD5.HashDataAsync(stream, new Cancel(true)).AsTask();
        await Assert.That(task.IsCanceled).IsTrue();
        await Assert.That(stream.ReadCount).IsEqualTo(0);
    }

    [Test]
    public async Task HashDataAsync_Stream_CancelledWhileReading()
    {
        using var source = new CancellationTokenSource();
        using var stream = new AsyncOnlyStream(data)
        {
            CancelAfterFirstRead = source
        };

        var task = MD5.HashDataAsync(stream, source.Token).AsTask();
        await Task.WhenAny(task);
        await Assert.That(task.IsCanceled).IsTrue();
        await Assert.That(stream.ReadCount).IsEqualTo(1);
    }
#endif

#if FeatureMemory
    [Test]
    public async Task HashData_ReadOnlySpan_ReturnsCorrectHash()
    {
        ReadOnlySpan<byte> span = data;

        var actualHash = MD5.HashData(span);
        await Assert.That(actualHash.SequenceEqual(expected)).IsTrue();
    }

#if FeatureValueTask
    [Test]
    public async Task HashDataAsync_Memory_ReturnsCorrectHash()
    {
        using var stream = new MemoryStream(data);

        stream.Position = 0;
        Memory<byte> destination = new byte[expected.Length];

        var length = await MD5.HashDataAsync(stream, destination);
        await Assert.That(length).IsEqualTo(expected.Length);
        await Assert.That(expected.AsSpan().SequenceEqual(destination.Span)).IsTrue();
    }

    [Test]
    public async Task HashDataAsync_Memory_ReadsAsynchronously()
    {
        using var stream = new AsyncOnlyStream(data);
        Memory<byte> destination = new byte[expected.Length];

        var length = await MD5.HashDataAsync(stream, destination);
        await Assert.That(length).IsEqualTo(expected.Length);
        await Assert.That(destination.ToArray().SequenceEqual(expected)).IsTrue();
    }

    [Test]
    public async Task HashDataAsync_Memory_PreCancelled_ReturnsCancelledTask()
    {
        using var stream = new AsyncOnlyStream(data);
        Memory<byte> destination = new byte[expected.Length];

        var task = MD5.HashDataAsync(stream, destination, new Cancel(true)).AsTask();
        await Assert.That(task.IsCanceled).IsTrue();
        await Assert.That(stream.ReadCount).IsEqualTo(0);
    }

    [Test]
    public async Task HashDataAsync_Memory_ShortDestination_ThrowsSynchronously()
    {
        using var stream = new AsyncOnlyStream(data);
        Memory<byte> destination = new byte[expected.Length - 1];

        var exception = Assert.Throws<ArgumentException>(() => MD5.HashDataAsync(stream, destination));
        await Assert.That(exception.ParamName).IsEqualTo("destination");
        await Assert.That(stream.ReadCount).IsEqualTo(0);
    }
#endif

    [Test]
    public Task HashData_StreamAndSpan_LongerDestination()
    {
        using var stream = new MemoryStream(data);
        Span<byte> destination = stackalloc byte[expected.Length + 1];

        stream.Position = 0;

        var length = MD5.HashData(stream, destination);

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

        var length = MD5.HashData(data, destination);

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
            MD5.HashData(stream, destination);
        }).Throws<ArgumentException>();

        await Assert.That(exception!.Message.StartsWith("Destination is too short.")).IsTrue();
    }

    [Test]
    public async Task HashData_ReadOnlySpanAndSpan_ShorterDestination()
    {
        var exception = await Assert.That(() =>
        {
            Span<byte> destination = stackalloc byte[expected.Length - 1];
            MD5.HashData(data, destination);
        }).Throws<ArgumentException>();

        await Assert.That(exception!.Message.StartsWith("Destination is too short.")).IsTrue();
    }


    [Test]
    public Task TryHashData_ValidInput_ReturnsTrueAndCorrectHash()
    {
        Span<byte> destination = stackalloc byte[expected.Length];

        var result = MD5.TryHashData(data, destination, out var written);

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

        var result = MD5.TryHashData(data, destination, out var written);

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
