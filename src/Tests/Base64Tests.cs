#if FeatureMemory

using System.Buffers;
using System.Buffers.Text;

public class Base64Tests
{
    // "Hello" -> standard base64 "SGVsbG8="
    static readonly byte[] HelloBytes = "Hello"u8.ToArray();
    const string HelloEncoded = "SGVsbG8=";

    [Test]
    public async Task GetEncodedLength()
    {
        await Assert.That(Base64.GetEncodedLength(0)).IsEqualTo(0);
        await Assert.That(Base64.GetEncodedLength(1)).IsEqualTo(4);
        await Assert.That(Base64.GetEncodedLength(2)).IsEqualTo(4);
        await Assert.That(Base64.GetEncodedLength(3)).IsEqualTo(4);
        await Assert.That(Base64.GetEncodedLength(5)).IsEqualTo(8);
    }

    [Test]
    public async Task GetMaxDecodedLength()
    {
        await Assert.That(Base64.GetMaxDecodedLength(0)).IsEqualTo(0);
        await Assert.That(Base64.GetMaxDecodedLength(4)).IsEqualTo(3);
        await Assert.That(Base64.GetMaxDecodedLength(8)).IsEqualTo(6);
    }

    [Test]
    public async Task EncodeToString()
    {
        var result = Base64.EncodeToString(HelloBytes);
        await Assert.That(result).IsEqualTo(HelloEncoded);
    }

    [Test]
    public async Task EncodeToString_Empty()
    {
        var result = Base64.EncodeToString(ReadOnlySpan<byte>.Empty);
        await Assert.That(result).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task EncodeToChars_Span()
    {
        var destination = new char[Base64.GetEncodedLength(HelloBytes.Length)];
        var written = Base64.EncodeToChars(HelloBytes, destination);
        var result = new string(destination, 0, written);
        await Assert.That(result).IsEqualTo(HelloEncoded);
    }

    [Test]
    public async Task EncodeToChars_Array()
    {
        var chars = Base64.EncodeToChars(HelloBytes);
        var result = new string(chars);
        await Assert.That(result).IsEqualTo(HelloEncoded);
    }

    [Test]
    public async Task EncodeToChars_OperationStatus()
    {
        var destination = new char[Base64.GetEncodedLength(HelloBytes.Length)];
        var status = Base64.EncodeToChars(HelloBytes, destination, out var consumed, out var written, isFinalBlock: true);
        var result = new string(destination, 0, written);
        await Assert.That(status).IsEqualTo(OperationStatus.Done);
        await Assert.That(consumed).IsEqualTo(HelloBytes.Length);
        await Assert.That(result).IsEqualTo(HelloEncoded);
    }

    [Test]
    public async Task TryEncodeToChars_Success()
    {
        var destination = new char[20];
        var success = Base64.TryEncodeToChars(HelloBytes, destination, out var written);
        var result = new string(destination, 0, written);
        await Assert.That(success).IsTrue();
        await Assert.That(result).IsEqualTo(HelloEncoded);
    }

    [Test]
    public async Task TryEncodeToChars_TooSmall()
    {
        var destination = new char[2];
        var success = Base64.TryEncodeToChars(HelloBytes, destination, out var written);
        await Assert.That(success).IsFalse();
        await Assert.That(written).IsEqualTo(0);
    }

    [Test]
    public async Task EncodeToUtf8_Array()
    {
        var result = Base64.EncodeToUtf8(HelloBytes);
        var expected = System.Text.Encoding.UTF8.GetBytes(HelloEncoded);
        await Assert.That(result).IsEquivalentTo(expected);
    }

    [Test]
    public async Task DecodeFromChars_Array()
    {
        var result = Base64.DecodeFromChars(HelloEncoded.AsSpan());
        await Assert.That(result).IsEquivalentTo(HelloBytes);
    }

    [Test]
    public async Task DecodeFromChars_Span()
    {
        var destination = new byte[Base64.GetMaxDecodedLength(HelloEncoded.Length)];
        var written = Base64.DecodeFromChars(HelloEncoded.AsSpan(), destination);
        var decoded = destination.AsSpan(0, written).ToArray();
        await Assert.That(decoded).IsEquivalentTo(HelloBytes);
    }

    [Test]
    public async Task DecodeFromChars_OperationStatus()
    {
        var destination = new byte[Base64.GetMaxDecodedLength(HelloEncoded.Length)];
        var status = Base64.DecodeFromChars(HelloEncoded.AsSpan(), destination, out var consumed, out var written, isFinalBlock: true);
        var decoded = destination.AsSpan(0, written).ToArray();
        await Assert.That(status).IsEqualTo(OperationStatus.Done);
        await Assert.That(consumed).IsEqualTo(HelloEncoded.Length);
        await Assert.That(decoded).IsEquivalentTo(HelloBytes);
    }

    [Test]
    public async Task TryDecodeFromChars_Success()
    {
        var destination = new byte[Base64.GetMaxDecodedLength(HelloEncoded.Length)];
        var success = Base64.TryDecodeFromChars(HelloEncoded.AsSpan(), destination, out var written);
        var decoded = destination.AsSpan(0, written).ToArray();
        await Assert.That(success).IsTrue();
        await Assert.That(decoded).IsEquivalentTo(HelloBytes);
    }

    [Test]
    public async Task DecodeFromUtf8_Array()
    {
        var utf8 = "SGVsbG8="u8.ToArray();
        var result = Base64.DecodeFromUtf8(utf8);
        await Assert.That(result).IsEquivalentTo(HelloBytes);
    }

    [Test]
    public async Task DecodeFromChars_Empty()
    {
        var result = Base64.DecodeFromChars(ReadOnlySpan<char>.Empty);
        await Assert.That(result).IsEmpty();
    }

    [Test]
    public async Task RoundTrip()
    {
        byte[] data = [0x00, 0x01, 0x02, 0xFB, 0xFF, 0xFE, 0x10];
        var encoded = Base64.EncodeToString(data);
        var decoded = Base64.DecodeFromChars(encoded.AsSpan());
        await Assert.That(decoded).IsEquivalentTo(data);
    }
}

#endif
