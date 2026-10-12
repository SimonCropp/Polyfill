using System.Security.Cryptography;

public class CryptographicOperationsPolyfillTests
{
    static byte[] data = [1, 2, 3, 4, 5];
    static byte[] key = [10, 20, 30, 40, 50, 60];

    static byte[] ExpectedSha256()
    {
        using var sha = SHA256.Create();
        return sha.ComputeHash(data);
    }

    static byte[] ExpectedHmacSha256()
    {
        using var hmac = new HMACSHA256(key);
        return hmac.ComputeHash(data);
    }

    [Test]
    public async Task HashData_ByteArray()
    {
        var actual = CryptographicOperations.HashData(HashAlgorithmName.SHA256, data);
        await Assert.That(actual.SequenceEqual(ExpectedSha256())).IsTrue();
    }

    [Test]
    public async Task HashData_ByteArray_Null_Throws() =>
        await Assert.That(() => CryptographicOperations.HashData(HashAlgorithmName.SHA256, (byte[]) null!))
            .Throws<ArgumentNullException>();

    [Test]
    public async Task HashData_Stream()
    {
        using var stream = new MemoryStream(data);
        var actual = CryptographicOperations.HashData(HashAlgorithmName.SHA256, stream);
        await Assert.That(actual.SequenceEqual(ExpectedSha256())).IsTrue();
    }

    [Test]
    public async Task HmacData_ByteArray()
    {
        var actual = CryptographicOperations.HmacData(HashAlgorithmName.SHA256, key, data);
        await Assert.That(actual.SequenceEqual(ExpectedHmacSha256())).IsTrue();
    }

    [Test]
    public async Task HmacData_Stream()
    {
        using var stream = new MemoryStream(data);
        var actual = CryptographicOperations.HmacData(HashAlgorithmName.SHA256, key, stream);
        await Assert.That(actual.SequenceEqual(ExpectedHmacSha256())).IsTrue();
    }

    [Test]
    public async Task VerifyHmac_ByteArray_Valid()
    {
        var mac = ExpectedHmacSha256();
        await Assert.That(CryptographicOperations.VerifyHmac(HashAlgorithmName.SHA256, key, data, mac)).IsTrue();
    }

    [Test]
    public async Task VerifyHmac_ByteArray_Tampered()
    {
        var mac = ExpectedHmacSha256();
        mac[0] ^= 0xFF;
        await Assert.That(CryptographicOperations.VerifyHmac(HashAlgorithmName.SHA256, key, data, mac)).IsFalse();
    }

    [Test]
    public async Task VerifyHmac_Stream_Valid()
    {
        using var stream = new MemoryStream(data);
        var mac = ExpectedHmacSha256();
        await Assert.That(CryptographicOperations.VerifyHmac(HashAlgorithmName.SHA256, key, stream, mac)).IsTrue();
    }

#if FeatureValueTask
    [Test]
    public async Task HashDataAsync_Stream()
    {
        using var stream = new MemoryStream(data);
        var actual = await CryptographicOperations.HashDataAsync(HashAlgorithmName.SHA256, stream);
        await Assert.That(actual.SequenceEqual(ExpectedSha256())).IsTrue();
    }

    [Test]
    public async Task HmacDataAsync_ByteArray_Stream()
    {
        using var stream = new MemoryStream(data);
        var actual = await CryptographicOperations.HmacDataAsync(HashAlgorithmName.SHA256, key, stream);
        await Assert.That(actual.SequenceEqual(ExpectedHmacSha256())).IsTrue();
    }

    [Test]
    public async Task VerifyHmacAsync_ByteArray_Stream()
    {
        using var stream = new MemoryStream(data);
        var mac = ExpectedHmacSha256();
        var result = await CryptographicOperations.VerifyHmacAsync(HashAlgorithmName.SHA256, key, stream, mac);
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task HashDataAsync_Stream_ReadsAsynchronously()
    {
        using var stream = new AsyncOnlyStream(data);
        var actual = await CryptographicOperations.HashDataAsync(HashAlgorithmName.SHA256, stream);
        await Assert.That(actual.SequenceEqual(ExpectedSha256())).IsTrue();
    }

    [Test]
    public async Task HmacDataAsync_ByteArray_Stream_ReadsAsynchronously()
    {
        using var stream = new AsyncOnlyStream(data);
        var actual = await CryptographicOperations.HmacDataAsync(HashAlgorithmName.SHA256, key, stream);
        await Assert.That(actual.SequenceEqual(ExpectedHmacSha256())).IsTrue();
    }

    [Test]
    public async Task VerifyHmacAsync_ByteArray_Stream_ReadsAsynchronously()
    {
        var mac = ExpectedHmacSha256();
        using var stream = new AsyncOnlyStream(data);
        await Assert.That(await CryptographicOperations.VerifyHmacAsync(HashAlgorithmName.SHA256, key, stream, mac)).IsTrue();

        mac[0] ^= 0xFF;
        using var tamperedStream = new AsyncOnlyStream(data);
        await Assert.That(await CryptographicOperations.VerifyHmacAsync(HashAlgorithmName.SHA256, key, tamperedStream, mac)).IsFalse();
    }

    [Test]
    public async Task HashDataAsync_Stream_PreCancelled_ReturnsCancelledTask()
    {
        using var stream = new AsyncOnlyStream(data);
        var task = CryptographicOperations.HashDataAsync(HashAlgorithmName.SHA256, stream, new Cancel(true)).AsTask();
        await Assert.That(task.IsCanceled).IsTrue();
        await Assert.That(stream.ReadCount).IsEqualTo(0);
    }

    [Test]
    public async Task HmacDataAsync_ByteArray_Stream_PreCancelled_ReturnsCancelledTask()
    {
        using var stream = new AsyncOnlyStream(data);
        var task = CryptographicOperations.HmacDataAsync(HashAlgorithmName.SHA256, key, stream, new Cancel(true)).AsTask();
        await Assert.That(task.IsCanceled).IsTrue();
        await Assert.That(stream.ReadCount).IsEqualTo(0);
    }

    [Test]
    public async Task VerifyHmacAsync_ByteArray_Stream_PreCancelled_ReturnsCancelledTask()
    {
        using var stream = new AsyncOnlyStream(data);
        var task = CryptographicOperations.VerifyHmacAsync(HashAlgorithmName.SHA256, key, stream, ExpectedHmacSha256(), new Cancel(true)).AsTask();
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

        var task = CryptographicOperations.HashDataAsync(HashAlgorithmName.SHA256, stream, source.Token).AsTask();
        await Task.WhenAny(task);
        await Assert.That(task.IsCanceled).IsTrue();
        await Assert.That(stream.ReadCount).IsEqualTo(1);
    }

    [Test]
    public async Task HashDataAsync_Stream_Null_ThrowsSynchronously()
    {
        var exception = Assert.Throws<ArgumentNullException>(() => CryptographicOperations.HashDataAsync(HashAlgorithmName.SHA256, null!));
        await Assert.That(exception.ParamName).IsEqualTo("source");
    }

    [Test]
    public async Task VerifyHmacAsync_ByteArray_Stream_WrongHashLength_ThrowsSynchronously()
    {
        using var stream = new AsyncOnlyStream(data);
        var exception = Assert.Throws<ArgumentException>(() => CryptographicOperations.VerifyHmacAsync(HashAlgorithmName.SHA256, key, stream, new byte[3]));
        await Assert.That(exception.ParamName).IsEqualTo("hash");
        await Assert.That(stream.ReadCount).IsEqualTo(0);
    }
#endif

#if FeatureMemory
    [Test]
    public async Task FixedTimeEquals_Equal()
    {
        var left = new byte[] {1, 2, 3, 4};
        var right = new byte[] {1, 2, 3, 4};
        await Assert.That(CryptographicOperations.FixedTimeEquals(left, right)).IsTrue();
    }

    [Test]
    public async Task FixedTimeEquals_NotEqual()
    {
        var left = new byte[] {1, 2, 3, 4};
        var right = new byte[] {1, 2, 3, 5};
        await Assert.That(CryptographicOperations.FixedTimeEquals(left, right)).IsFalse();
    }

    [Test]
    public async Task FixedTimeEquals_DifferentLength()
    {
        var left = new byte[] {1, 2, 3, 4};
        var right = new byte[] {1, 2, 3};
        await Assert.That(CryptographicOperations.FixedTimeEquals(left, right)).IsFalse();
    }

    [Test]
    public async Task FixedTimeEquals_SingleByte_AllMatch()
    {
        var source = new byte[] {7, 7, 7, 7};
        await Assert.That(CryptographicOperations.FixedTimeEquals(source, (byte) 7)).IsTrue();
    }

    [Test]
    public async Task FixedTimeEquals_SingleByte_Mismatch()
    {
        var source = new byte[] {7, 7, 8, 7};
        await Assert.That(CryptographicOperations.FixedTimeEquals(source, (byte) 7)).IsFalse();
    }

    [Test]
    public async Task ZeroMemory_Clears()
    {
        var buffer = new byte[] {1, 2, 3, 4, 5};
        CryptographicOperations.ZeroMemory(buffer);
        await Assert.That(buffer.All(_ => _ == 0)).IsTrue();
    }

    [Test]
    public async Task HashData_Span()
    {
        var destination = new byte[32];
        var written = CryptographicOperations.HashData(HashAlgorithmName.SHA256, data.AsSpan(), destination);
        await Assert.That(written).IsEqualTo(32);
        await Assert.That(destination.SequenceEqual(ExpectedSha256())).IsTrue();
    }

    [Test]
    public async Task HashData_Span_DestinationTooShort()
    {
        var exception = await Assert.That(() =>
            {
                var destination = new byte[8];
                CryptographicOperations.HashData(HashAlgorithmName.SHA256, data.AsSpan(), destination);
            })
            .Throws<ArgumentException>();
        await Assert.That(exception!.Message.StartsWith("Destination is too short.")).IsTrue();
    }

    [Test]
    public async Task TryHashData_Valid()
    {
        var destination = new byte[32];
        var result = CryptographicOperations.TryHashData(HashAlgorithmName.SHA256, data.AsSpan(), destination, out var written);
        await Assert.That(result).IsTrue();
        await Assert.That(written).IsEqualTo(32);
        await Assert.That(destination.SequenceEqual(ExpectedSha256())).IsTrue();
    }

    [Test]
    public async Task TryHashData_TooShort()
    {
        var destination = new byte[8];
        var result = CryptographicOperations.TryHashData(HashAlgorithmName.SHA256, data.AsSpan(), destination, out var written);
        await Assert.That(result).IsFalse();
        await Assert.That(written).IsEqualTo(0);
    }

    static byte[] ReferenceHash(string name, byte[] input)
    {
        using HashAlgorithm hasher = name switch
        {
            "MD5" => MD5.Create(),
            "SHA1" => SHA1.Create(),
            "SHA256" => SHA256.Create(),
            "SHA384" => SHA384.Create(),
            _ => SHA512.Create()
        };
        return hasher.ComputeHash(input);
    }

    static byte[] ReferenceMac(string name, byte[] macKey, byte[] input)
    {
        using HMAC hmac = name switch
        {
            "MD5" => new HMACMD5(macKey),
            "SHA1" => new HMACSHA1(macKey),
            "SHA256" => new HMACSHA256(macKey),
            "SHA384" => new HMACSHA384(macKey),
            _ => new HMACSHA512(macKey)
        };
        return hmac.ComputeHash(input);
    }

    [Test]
    [Arguments("MD5")]
    [Arguments("SHA1")]
    [Arguments("SHA256")]
    [Arguments("SHA384")]
    [Arguments("SHA512")]
    public async Task AllOverloads_MatchReference(string name)
    {
        var algorithm = new HashAlgorithmName(name);
        var problems = new List<string>();

        // keys shorter than, equal to and longer than the block size of the algorithm
        foreach (var keyLength in new[] {1, 6, 64, 128, 200})
        {
            var macKey = new byte[keyLength];
            for (var i = 0; i < macKey.Length; i++)
            {
                macKey[i] = unchecked((byte) (i * 7 + 3));
            }

            foreach (var length in new[] {0, 1, 55, 56, 64, 111, 112, 128, 1000, 100_000})
            {
                // a margin either side of the input, so that the span is not the whole of an array
                var buffer = new byte[length + 7];
                for (var i = 0; i < buffer.Length; i++)
                {
                    buffer[i] = unchecked((byte) (i * 31 + 7));
                }

                var input = buffer.AsSpan(3, length).ToArray();
                var expectedHash = ReferenceHash(name, input);
                var expectedMac = ReferenceMac(name, macKey, input);
                var size = expectedHash.Length;

                void Check(string check, bool condition)
                {
                    if (!condition)
                    {
                        problems.Add($"{check} key {keyLength} length {length}");
                    }
                }

                Check("HashData array", CryptographicOperations.HashData(algorithm, input).SequenceEqual(expectedHash));
                Check("HashData span", CryptographicOperations.HashData(algorithm, new ReadOnlySpan<byte>(buffer, 3, length)).SequenceEqual(expectedHash));

                // one spare byte, to check nothing is written past the hash
                var destination = new byte[size + 1];
                destination[size] = 0xEE;
                var written = CryptographicOperations.HashData(algorithm, new ReadOnlySpan<byte>(buffer, 3, length), destination);
                Check("HashData destination", written == size && destination.AsSpan(0, size).SequenceEqual(expectedHash) && destination[size] == 0xEE);

                destination = new byte[size];
                var success = CryptographicOperations.TryHashData(algorithm, new ReadOnlySpan<byte>(buffer, 3, length), destination, out written);
                Check("TryHashData", success && written == size && destination.SequenceEqual(expectedHash));

                destination = new byte[size - 1];
                success = CryptographicOperations.TryHashData(algorithm, new ReadOnlySpan<byte>(buffer, 3, length), destination, out written);
                Check("TryHashData too short", !success && written == 0 && destination.All(_ => _ == 0));

                Check("HmacData array", CryptographicOperations.HmacData(algorithm, macKey, input).SequenceEqual(expectedMac));
                Check("HmacData span", CryptographicOperations.HmacData(algorithm, macKey.AsSpan(), new ReadOnlySpan<byte>(buffer, 3, length)).SequenceEqual(expectedMac));

                destination = new byte[size + 1];
                destination[size] = 0xEE;
                written = CryptographicOperations.HmacData(algorithm, macKey.AsSpan(), new ReadOnlySpan<byte>(buffer, 3, length), destination);
                Check("HmacData destination", written == size && destination.AsSpan(0, size).SequenceEqual(expectedMac) && destination[size] == 0xEE);

                destination = new byte[size];
                success = CryptographicOperations.TryHmacData(algorithm, macKey.AsSpan(), new ReadOnlySpan<byte>(buffer, 3, length), destination, out written);
                Check("TryHmacData", success && written == size && destination.SequenceEqual(expectedMac));

                destination = new byte[size - 1];
                success = CryptographicOperations.TryHmacData(algorithm, macKey.AsSpan(), new ReadOnlySpan<byte>(buffer, 3, length), destination, out written);
                Check("TryHmacData too short", !success && written == 0 && destination.All(_ => _ == 0));

                Check("VerifyHmac array", CryptographicOperations.VerifyHmac(algorithm, macKey, input, expectedMac));
                Check("VerifyHmac span", CryptographicOperations.VerifyHmac(algorithm, macKey.AsSpan(), new ReadOnlySpan<byte>(buffer, 3, length), expectedMac.AsSpan()));

                // a difference in the first and in the last byte
                foreach (var index in new[] {0, size - 1})
                {
                    var tampered = (byte[]) expectedMac.Clone();
                    tampered[index] ^= 0x80;
                    Check("VerifyHmac array tampered", !CryptographicOperations.VerifyHmac(algorithm, macKey, input, tampered));
                    Check("VerifyHmac span tampered", !CryptographicOperations.VerifyHmac(algorithm, macKey.AsSpan(), new ReadOnlySpan<byte>(buffer, 3, length), tampered.AsSpan()));
                }

                for (var i = 0; i < buffer.Length; i++)
                {
                    if (buffer[i] != unchecked((byte) (i * 31 + 7)))
                    {
                        problems.Add($"input changed at {i} length {length}");
                        break;
                    }
                }
            }
        }

        await Assert.That(problems).IsEmpty();
    }

    [Test]
    public async Task UnknownAlgorithm_Throws()
    {
        var unknown = new HashAlgorithmName("NOPE");
        var destination = new byte[64];

        await Assert.That(() => CryptographicOperations.HashData(unknown, data)).Throws<CryptographicException>();
        await Assert.That(() => CryptographicOperations.HashData(unknown, data.AsSpan())).Throws<CryptographicException>();
        await Assert.That(() => CryptographicOperations.HashData(unknown, data.AsSpan(), destination)).Throws<CryptographicException>();
        await Assert.That(() => CryptographicOperations.TryHashData(unknown, data.AsSpan(), destination, out _)).Throws<CryptographicException>();
        await Assert.That(() => CryptographicOperations.HmacData(unknown, key, data)).Throws<CryptographicException>();
        await Assert.That(() => CryptographicOperations.HmacData(unknown, key.AsSpan(), data.AsSpan())).Throws<CryptographicException>();
        await Assert.That(() => CryptographicOperations.HmacData(unknown, key.AsSpan(), data.AsSpan(), destination)).Throws<CryptographicException>();
        await Assert.That(() => CryptographicOperations.TryHmacData(unknown, key.AsSpan(), data.AsSpan(), destination, out _)).Throws<CryptographicException>();
        await Assert.That(() => CryptographicOperations.VerifyHmac(unknown, key.AsSpan(), data.AsSpan(), destination.AsSpan())).Throws<CryptographicException>();
    }

    [Test]
    public async Task VerifyHmac_Span_WrongHashLength_Throws()
    {
        var mac = ExpectedHmacSha256();
        await Assert.That(() => CryptographicOperations.VerifyHmac(HashAlgorithmName.SHA256, key.AsSpan(), data.AsSpan(), mac.AsSpan(0, 31))).Throws<ArgumentException>();
        await Assert.That(() => CryptographicOperations.VerifyHmac(HashAlgorithmName.SHA256, key.AsSpan(), data.AsSpan(), new byte[33].AsSpan())).Throws<ArgumentException>();
        await Assert.That(() => CryptographicOperations.VerifyHmac(HashAlgorithmName.SHA256, key, data, new byte[31])).Throws<ArgumentException>();
    }

    [Test]
    public async Task HmacData_DestinationTooShort_Throws()
    {
        var exception = await Assert.That(() =>
            {
                var destination = new byte[31];
                CryptographicOperations.HmacData(HashAlgorithmName.SHA256, key.AsSpan(), data.AsSpan(), destination);
            })
            .Throws<ArgumentException>();
        await Assert.That(exception!.Message.StartsWith("Destination is too short.")).IsTrue();
    }

    [Test]
    public async Task HashAndHmac_Concurrent()
    {
        var input = new byte[5000];
        for (var i = 0; i < input.Length; i++)
        {
            input[i] = unchecked((byte) (i * 13));
        }

        var expectedHash = ReferenceHash("SHA256", input);
        var expectedMac = ReferenceMac("SHA256", key, input);

        var tasks = new List<Task<bool>>();
        for (var t = 0; t < 4; t++)
        {
            tasks.Add(Task.Run(() =>
            {
                var destination = new byte[32];
                for (var i = 0; i < 500; i++)
                {
                    if (!CryptographicOperations.TryHashData(HashAlgorithmName.SHA256, input.AsSpan(), destination, out _) ||
                        !destination.SequenceEqual(expectedHash) ||
                        !CryptographicOperations.TryHmacData(HashAlgorithmName.SHA256, key.AsSpan(), input.AsSpan(), destination, out _) ||
                        !destination.SequenceEqual(expectedMac) ||
                        !CryptographicOperations.VerifyHmac(HashAlgorithmName.SHA256, key.AsSpan(), input.AsSpan(), expectedMac.AsSpan()))
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

    [Test]
    public async Task HmacData_Span()
    {
        var actual = CryptographicOperations.HmacData(HashAlgorithmName.SHA256, key.AsSpan(), data.AsSpan());
        await Assert.That(actual.SequenceEqual(ExpectedHmacSha256())).IsTrue();
    }

    [Test]
    public async Task TryHmacData_Valid()
    {
        var destination = new byte[32];
        var result = CryptographicOperations.TryHmacData(HashAlgorithmName.SHA256, key.AsSpan(), data.AsSpan(), destination, out var written);
        await Assert.That(result).IsTrue();
        await Assert.That(written).IsEqualTo(32);
        await Assert.That(destination.SequenceEqual(ExpectedHmacSha256())).IsTrue();
    }

    [Test]
    public async Task VerifyHmac_Span_Valid()
    {
        var mac = ExpectedHmacSha256();
        await Assert.That(CryptographicOperations.VerifyHmac(HashAlgorithmName.SHA256, key.AsSpan(), data.AsSpan(), mac.AsSpan())).IsTrue();
    }

    [Test]
    public async Task VerifyHmac_Span_Tampered()
    {
        var mac = ExpectedHmacSha256();
        mac[0] ^= 0xFF;
        await Assert.That(CryptographicOperations.VerifyHmac(HashAlgorithmName.SHA256, key.AsSpan(), data.AsSpan(), mac.AsSpan())).IsFalse();
    }

#if FeatureValueTask
    [Test]
    public async Task HashDataAsync_Memory()
    {
        using var stream = new MemoryStream(data);
        Memory<byte> destination = new byte[32];
        var written = await CryptographicOperations.HashDataAsync(HashAlgorithmName.SHA256, stream, destination);
        await Assert.That(written).IsEqualTo(32);
        await Assert.That(destination.ToArray().SequenceEqual(ExpectedSha256())).IsTrue();
    }

    [Test]
    public async Task HmacDataAsync_Memory()
    {
        using var stream = new MemoryStream(data);
        ReadOnlyMemory<byte> keyMemory = key;
        var actual = await CryptographicOperations.HmacDataAsync(HashAlgorithmName.SHA256, keyMemory, stream);
        await Assert.That(actual.SequenceEqual(ExpectedHmacSha256())).IsTrue();
    }

    [Test]
    public async Task VerifyHmacAsync_Memory()
    {
        using var stream = new MemoryStream(data);
        ReadOnlyMemory<byte> keyMemory = key;
        ReadOnlyMemory<byte> macMemory = ExpectedHmacSha256();
        var result = await CryptographicOperations.VerifyHmacAsync(HashAlgorithmName.SHA256, keyMemory, stream, macMemory);
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task HashDataAsync_Memory_ReadsAsynchronously()
    {
        using var stream = new AsyncOnlyStream(data);
        Memory<byte> destination = new byte[32];
        var written = await CryptographicOperations.HashDataAsync(HashAlgorithmName.SHA256, stream, destination);
        await Assert.That(written).IsEqualTo(32);
        await Assert.That(destination.ToArray().SequenceEqual(ExpectedSha256())).IsTrue();
    }

    [Test]
    public async Task HmacDataAsync_Memory_ReadsAsynchronously()
    {
        ReadOnlyMemory<byte> keyMemory = key;
        using var stream = new AsyncOnlyStream(data);
        var actual = await CryptographicOperations.HmacDataAsync(HashAlgorithmName.SHA256, keyMemory, stream);
        await Assert.That(actual.SequenceEqual(ExpectedHmacSha256())).IsTrue();

        using var destinationStream = new AsyncOnlyStream(data);
        Memory<byte> destination = new byte[32];
        var written = await CryptographicOperations.HmacDataAsync(HashAlgorithmName.SHA256, keyMemory, destinationStream, destination);
        await Assert.That(written).IsEqualTo(32);
        await Assert.That(destination.ToArray().SequenceEqual(ExpectedHmacSha256())).IsTrue();
    }

    [Test]
    public async Task VerifyHmacAsync_Memory_ReadsAsynchronously()
    {
        ReadOnlyMemory<byte> keyMemory = key;
        var mac = ExpectedHmacSha256();
        using var stream = new AsyncOnlyStream(data);
        await Assert.That(await CryptographicOperations.VerifyHmacAsync(HashAlgorithmName.SHA256, keyMemory, stream, (ReadOnlyMemory<byte>) mac)).IsTrue();

        mac[0] ^= 0xFF;
        using var tamperedStream = new AsyncOnlyStream(data);
        await Assert.That(await CryptographicOperations.VerifyHmacAsync(HashAlgorithmName.SHA256, keyMemory, tamperedStream, (ReadOnlyMemory<byte>) mac)).IsFalse();
    }

    [Test]
    public async Task Async_Memory_PreCancelled_ReturnsCancelledTask()
    {
        ReadOnlyMemory<byte> keyMemory = key;
        ReadOnlyMemory<byte> macMemory = ExpectedHmacSha256();
        Memory<byte> destination = new byte[32];
        var cancel = new Cancel(true);
        using var stream = new AsyncOnlyStream(data);

        var hashTask = CryptographicOperations.HashDataAsync(HashAlgorithmName.SHA256, stream, destination, cancel).AsTask();
        await Assert.That(hashTask.IsCanceled).IsTrue();

        var hmacTask = CryptographicOperations.HmacDataAsync(HashAlgorithmName.SHA256, keyMemory, stream, cancel).AsTask();
        await Assert.That(hmacTask.IsCanceled).IsTrue();

        var hmacDestinationTask = CryptographicOperations.HmacDataAsync(HashAlgorithmName.SHA256, keyMemory, stream, destination, cancel).AsTask();
        await Assert.That(hmacDestinationTask.IsCanceled).IsTrue();

        var verifyTask = CryptographicOperations.VerifyHmacAsync(HashAlgorithmName.SHA256, keyMemory, stream, macMemory, cancel).AsTask();
        await Assert.That(verifyTask.IsCanceled).IsTrue();

        await Assert.That(stream.ReadCount).IsEqualTo(0);
    }

    [Test]
    public async Task Async_Memory_ShortDestination_ThrowsSynchronously()
    {
        ReadOnlyMemory<byte> keyMemory = key;
        Memory<byte> destination = new byte[31];
        using var stream = new AsyncOnlyStream(data);

        var exception = Assert.Throws<ArgumentException>(() => CryptographicOperations.HashDataAsync(HashAlgorithmName.SHA256, stream, destination));
        await Assert.That(exception.ParamName).IsEqualTo("destination");

        exception = Assert.Throws<ArgumentException>(() => CryptographicOperations.HmacDataAsync(HashAlgorithmName.SHA256, keyMemory, stream, destination));
        await Assert.That(exception.ParamName).IsEqualTo("destination");

        await Assert.That(stream.ReadCount).IsEqualTo(0);
    }
#endif
#endif
}
