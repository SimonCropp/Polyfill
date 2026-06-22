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
#endif
#endif
}
