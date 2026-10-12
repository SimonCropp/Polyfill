// ReSharper disable ReplaceSliceWithRangeIndexer

using System.Security.Cryptography;

#pragma warning disable IDE0057

public class Sha1PolyfillTests
{
    static byte[] data = [1, 2, 3, 4, 5];

    // SHA1 hash of [1, 2, 3, 4, 5]
    static byte[] expected = [17, 150, 106, 185, 192, 153, 248, 250, 190, 250, 197, 76, 8, 213, 190, 43, 216, 201, 3, 175];

    [Test]
    public async Task HashData_ByteArray_ReturnsCorrectHash()
    {
        var actualHash = SHA1.HashData(data);
        await Assert.That(actualHash.SequenceEqual(expected)).IsTrue();
    }

    [Test]
    public async Task HashData_Stream_ReturnsCorrectHash()
    {
        using var stream = new MemoryStream(data);

        stream.Position = 0;

        // ReSharper disable once MethodHasAsyncOverload
        var actualHash = SHA1.HashData(stream);
        await Assert.That(actualHash.SequenceEqual(expected)).IsTrue();
    }

#if FeatureValueTask
    [Test]
    public async Task HashDataAsync_Stream_ReturnsCorrectHash()
    {
        using var stream = new MemoryStream(data);

        stream.Position = 0;

        var actualHash = await SHA1.HashDataAsync(stream);
        await Assert.That(actualHash.SequenceEqual(expected)).IsTrue();
    }

    [Test]
    public async Task HashDataAsync_Stream_ReadsAsynchronously()
    {
        using var stream = new AsyncOnlyStream(data);

        var actualHash = await SHA1.HashDataAsync(stream);
        await Assert.That(actualHash.SequenceEqual(expected)).IsTrue();
    }

    [Test]
    public async Task HashDataAsync_Stream_PreCancelled_ReturnsCancelledTask()
    {
        using var stream = new AsyncOnlyStream(data);

        var task = SHA1.HashDataAsync(stream, new Cancel(true)).AsTask();
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

        var task = SHA1.HashDataAsync(stream, source.Token).AsTask();
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

        var actualHash = SHA1.HashData(span);
        await Assert.That(actualHash.SequenceEqual(expected)).IsTrue();
    }

#if FeatureValueTask
    [Test]
    public async Task HashDataAsync_Memory_ReturnsCorrectHash()
    {
        using var stream = new MemoryStream(data);

        stream.Position = 0;
        Memory<byte> destination = new byte[expected.Length];

        var length = await SHA1.HashDataAsync(stream, destination);
        await Assert.That(length).IsEqualTo(expected.Length);
        await Assert.That(expected.AsSpan().SequenceEqual(destination.Span)).IsTrue();
    }

    [Test]
    public async Task HashDataAsync_Memory_ReadsAsynchronously()
    {
        using var stream = new AsyncOnlyStream(data);
        Memory<byte> destination = new byte[expected.Length];

        var length = await SHA1.HashDataAsync(stream, destination);
        await Assert.That(length).IsEqualTo(expected.Length);
        await Assert.That(destination.ToArray().SequenceEqual(expected)).IsTrue();
    }

    [Test]
    public async Task HashDataAsync_Memory_PreCancelled_ReturnsCancelledTask()
    {
        using var stream = new AsyncOnlyStream(data);
        Memory<byte> destination = new byte[expected.Length];

        var task = SHA1.HashDataAsync(stream, destination, new Cancel(true)).AsTask();
        await Assert.That(task.IsCanceled).IsTrue();
        await Assert.That(stream.ReadCount).IsEqualTo(0);
    }

    [Test]
    public async Task HashDataAsync_Memory_ShortDestination_ThrowsSynchronously()
    {
        using var stream = new AsyncOnlyStream(data);
        Memory<byte> destination = new byte[expected.Length - 1];

        var exception = Assert.Throws<ArgumentException>(() => SHA1.HashDataAsync(stream, destination));
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

        var length = SHA1.HashData(stream, destination);

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

        var length = SHA1.HashData(data, destination);

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
            SHA1.HashData(stream, destination);
        }).Throws<ArgumentException>();

        await Assert.That(exception!.Message.StartsWith("Destination is too short.")).IsTrue();
    }

    [Test]
    public async Task HashData_ReadOnlySpanAndSpan_ShorterDestination()
    {
        var exception = await Assert.That(() =>
        {
            Span<byte> destination = stackalloc byte[expected.Length - 1];
            SHA1.HashData(data, destination);
        }).Throws<ArgumentException>();

        await Assert.That(exception!.Message.StartsWith("Destination is too short.")).IsTrue();
    }


    [Test]
    public Task TryHashData_ValidInput_ReturnsTrueAndCorrectHash()
    {
        Span<byte> destination = stackalloc byte[expected.Length];

        var result = SHA1.TryHashData(data, destination, out var written);

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

        var result = SHA1.TryHashData(data, destination, out var written);

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

    [Test]
    [Arguments(0)]
    [Arguments(1)]
    [Arguments(55)]
    [Arguments(56)]
    [Arguments(63)]
    [Arguments(64)]
    [Arguments(65)]
    [Arguments(127)]
    [Arguments(128)]
    [Arguments(129)]
    [Arguments(4096)]
    [Arguments(65535)]
    [Arguments(65536)]
    [Arguments(65537)]
    [Arguments(200_000)]
    public async Task HashData_Span_AnyLength(int length)
    {
        // a margin either side of the input, so that the span is not the whole of an array
        var buffer = new byte[length + 7];
        for (var i = 0; i < buffer.Length; i++)
        {
            buffer[i] = unchecked((byte) (i * 31 + 7));
        }

        var input = buffer.AsSpan(3, length).ToArray();
        byte[] reference;
        using (var hasher = SHA1.Create())
        {
            reference = hasher.ComputeHash(input);
        }

        var fromArray = SHA1.HashData(input);
        var fromSpan = SHA1.HashData(new ReadOnlySpan<byte>(buffer, 3, length));

        // one spare byte, to check nothing is written past the hash
        var destination = new byte[reference.Length + 1];
        destination[reference.Length] = 0xEE;
        var written = SHA1.HashData(new ReadOnlySpan<byte>(buffer, 3, length), destination);

        var tryDestination = new byte[reference.Length + 1];
        tryDestination[reference.Length] = 0xEE;
        var success = SHA1.TryHashData(new ReadOnlySpan<byte>(buffer, 3, length), tryDestination, out var tryWritten);

        var exactDestination = new byte[reference.Length];
        var exactSuccess = SHA1.TryHashData(new ReadOnlySpan<byte>(buffer, 3, length), exactDestination, out var exactWritten);

        var shortDestination = new byte[reference.Length - 1];
        var shortSuccess = SHA1.TryHashData(new ReadOnlySpan<byte>(buffer, 3, length), shortDestination, out var shortWritten);

        await Assert.That(fromArray).IsEquivalentTo(reference);
        await Assert.That(fromSpan).IsEquivalentTo(reference);

        await Assert.That(written).IsEqualTo(reference.Length);
        await Assert.That(destination.AsSpan(0, written).ToArray()).IsEquivalentTo(reference);
        await Assert.That(destination[reference.Length]).IsEqualTo((byte) 0xEE);

        await Assert.That(success).IsTrue();
        await Assert.That(tryWritten).IsEqualTo(reference.Length);
        await Assert.That(tryDestination.AsSpan(0, tryWritten).ToArray()).IsEquivalentTo(reference);
        await Assert.That(tryDestination[reference.Length]).IsEqualTo((byte) 0xEE);

        await Assert.That(exactSuccess).IsTrue();
        await Assert.That(exactWritten).IsEqualTo(reference.Length);
        await Assert.That(exactDestination).IsEquivalentTo(reference);

        await Assert.That(shortSuccess).IsFalse();
        await Assert.That(shortWritten).IsEqualTo(0);
        await Assert.That(shortDestination).IsEquivalentTo(new byte[reference.Length - 1]);

        // the input is left as it was
        for (var i = 0; i < buffer.Length; i++)
        {
            if (buffer[i] != unchecked((byte) (i * 31 + 7)))
            {
                Assert.Fail($"Input changed at {i}");
            }
        }
    }

    [Test]
    public async Task HashData_Span_RepeatedAndConcurrent()
    {
        var first = new byte[70_000];
        var second = new byte[33];
        for (var i = 0; i < first.Length; i++)
        {
            first[i] = unchecked((byte) (i * 13));
        }

        for (var i = 0; i < second.Length; i++)
        {
            second[i] = unchecked((byte) (i * 17 + 1));
        }

        var firstExpected = SHA1.HashData(first);
        var secondExpected = SHA1.HashData(second);

        // the pooled buffer is shared, so a result must not depend on what was hashed before or alongside
        var tasks = new List<Task<bool>>();
        for (var t = 0; t < 4; t++)
        {
            tasks.Add(Task.Run(() =>
            {
                for (var i = 0; i < 50; i++)
                {
                    if (!SHA1.HashData(new ReadOnlySpan<byte>(first)).SequenceEqual(firstExpected) ||
                        !SHA1.HashData(new ReadOnlySpan<byte>(second)).SequenceEqual(secondExpected))
                    {
                        return false;
                    }
                }

                return true;
            }));
        }

        var results = await Task.WhenAll(tasks);
        await Assert.That(results.All(_ => _)).IsTrue();
    }
#endif
}
