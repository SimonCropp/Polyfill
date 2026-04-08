#if FeatureMemory

using System.Buffers;
using System.Buffers.Text;

public class Base64UrlTests
{
    // "Hello" -> base64url "SGVsbG8"
    static readonly byte[] HelloBytes = "Hello"u8.ToArray();
    const string HelloEncoded = "SGVsbG8";

    // "AB" -> base64url "QUI"
    static readonly byte[] AbBytes = "AB"u8.ToArray();
    const string AbEncoded = "QUI";

    [Test]
    public async Task GetEncodedLength()
    {
        await Assert.That(Base64Url.GetEncodedLength(0)).IsEqualTo(0);
        await Assert.That(Base64Url.GetEncodedLength(1)).IsEqualTo(2);
        await Assert.That(Base64Url.GetEncodedLength(2)).IsEqualTo(3);
        await Assert.That(Base64Url.GetEncodedLength(3)).IsEqualTo(4);
        await Assert.That(Base64Url.GetEncodedLength(5)).IsEqualTo(7);
    }

    [Test]
    public async Task GetMaxDecodedLength()
    {
        await Assert.That(Base64Url.GetMaxDecodedLength(0)).IsEqualTo(0);
        await Assert.That(Base64Url.GetMaxDecodedLength(2)).IsEqualTo(1);
        await Assert.That(Base64Url.GetMaxDecodedLength(3)).IsEqualTo(2);
        await Assert.That(Base64Url.GetMaxDecodedLength(4)).IsEqualTo(3);
        await Assert.That(Base64Url.GetMaxDecodedLength(8)).IsEqualTo(6);
    }

    [Test]
    public async Task EncodeToString_Basic()
    {
        var result = Base64Url.EncodeToString(HelloBytes);
        await Assert.That(result).IsEqualTo(HelloEncoded);
    }

    [Test]
    public async Task EncodeToString_Empty()
    {
        var result = Base64Url.EncodeToString(ReadOnlySpan<byte>.Empty);
        await Assert.That(result).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task EncodeToString_UrlSafeChars()
    {
        // Bytes that produce + and / in standard base64
        byte[] input = [0xFB, 0xFF, 0xFE];
        var result = Base64Url.EncodeToString(input);
        await Assert.That(result.Contains('+')).IsFalse();
        await Assert.That(result.Contains('/')).IsFalse();
        await Assert.That(result.Contains('=')).IsFalse();
    }

    [Test]
    public async Task EncodeToChars_Basic()
    {
        var destination = new char[Base64Url.GetEncodedLength(HelloBytes.Length)];
        var written = Base64Url.EncodeToChars(HelloBytes, destination);
        var result = new string(destination, 0, written);
        await Assert.That(written).IsEqualTo(HelloEncoded.Length);
        await Assert.That(result).IsEqualTo(HelloEncoded);
    }

    [Test]
    public async Task TryEncodeToChars_Success()
    {
        var destination = new char[20];
        var success = Base64Url.TryEncodeToChars(HelloBytes, destination, out var written);
        var result = new string(destination, 0, written);
        await Assert.That(success).IsTrue();
        await Assert.That(result).IsEqualTo(HelloEncoded);
    }

    [Test]
    public async Task TryEncodeToChars_TooSmall()
    {
        var destination = new char[2];
        var success = Base64Url.TryEncodeToChars(HelloBytes, destination, out var written);
        await Assert.That(success).IsFalse();
        await Assert.That(written).IsEqualTo(0);
    }

    [Test]
    public async Task EncodeToUtf8_Array()
    {
        var result = Base64Url.EncodeToUtf8(HelloBytes);
        var expected = System.Text.Encoding.UTF8.GetBytes(HelloEncoded);
        await Assert.That(result).IsEquivalentTo(expected);
    }

    [Test]
    public async Task TryEncodeToUtf8_Success()
    {
        var destination = new byte[20];
        var success = Base64Url.TryEncodeToUtf8(HelloBytes, destination, out var written);
        await Assert.That(success).IsTrue();
        await Assert.That(written).IsEqualTo(HelloEncoded.Length);
    }

    [Test]
    public async Task TryEncodeToUtf8InPlace()
    {
        var encodedLength = Base64Url.GetEncodedLength(AbBytes.Length);
        var buffer = new byte[encodedLength];
        AbBytes.CopyTo(buffer, 0);
        var success = Base64Url.TryEncodeToUtf8InPlace(buffer, AbBytes.Length, out var written);
        await Assert.That(success).IsTrue();
        await Assert.That(written).IsEqualTo(encodedLength);
    }

    [Test]
    public async Task DecodeFromChars_Basic()
    {
        var result = Base64Url.DecodeFromChars(HelloEncoded.AsSpan());
        await Assert.That(result).IsEquivalentTo(HelloBytes);
    }

    [Test]
    public async Task DecodeFromChars_Empty()
    {
        var result = Base64Url.DecodeFromChars(ReadOnlySpan<char>.Empty);
        await Assert.That(result).IsEmpty();
    }

    [Test]
    public async Task DecodeFromChars_InvalidLength()
    {
        await Assert.That(() => Base64Url.DecodeFromChars("A".AsSpan())).Throws<FormatException>();
    }

    [Test]
    public async Task DecodeFromChars_InvalidChar()
    {
        await Assert.That(() => Base64Url.DecodeFromChars("AB=D".AsSpan())).Throws<FormatException>();
    }

    [Test]
    public async Task DecodeFromChars_ToSpan()
    {
        var destination = new byte[20];
        var written = Base64Url.DecodeFromChars(HelloEncoded.AsSpan(), destination);
        var result = destination.AsSpan().Slice(0, written).ToArray();
        await Assert.That(result).IsEquivalentTo(HelloBytes);
    }

    [Test]
    public async Task TryDecodeFromChars_Success()
    {
        var destination = new byte[20];
        var success = Base64Url.TryDecodeFromChars(HelloEncoded.AsSpan(), destination, out var written);
        var result = destination.AsSpan().Slice(0, written).ToArray();
        await Assert.That(success).IsTrue();
        await Assert.That(result).IsEquivalentTo(HelloBytes);
    }

    [Test]
    public async Task TryDecodeFromChars_Invalid()
    {
        var destination = new byte[20];
        await Assert.That(() => Base64Url.TryDecodeFromChars("!!!".AsSpan(), destination, out _)).Throws<FormatException>();
    }

    [Test]
    public async Task TryDecodeFromChars_TooSmall()
    {
        var destination = new byte[1];
        var success = Base64Url.TryDecodeFromChars(HelloEncoded.AsSpan(), destination, out var written);
        await Assert.That(success).IsFalse();
        await Assert.That(written).IsEqualTo(0);
    }

    [Test]
    public async Task DecodeFromUtf8_Basic()
    {
        var utf8 = System.Text.Encoding.UTF8.GetBytes(HelloEncoded);
        var result = Base64Url.DecodeFromUtf8(utf8);
        await Assert.That(result).IsEquivalentTo(HelloBytes);
    }

    [Test]
    public async Task TryDecodeFromUtf8_Success()
    {
        var utf8 = System.Text.Encoding.UTF8.GetBytes(HelloEncoded);
        var destination = new byte[20];
        var success = Base64Url.TryDecodeFromUtf8(utf8, destination, out var written);
        var result = destination.AsSpan().Slice(0, written).ToArray();
        await Assert.That(success).IsTrue();
        await Assert.That(result).IsEquivalentTo(HelloBytes);
    }

    [Test]
    public async Task DecodeFromUtf8InPlace()
    {
        var utf8 = System.Text.Encoding.UTF8.GetBytes(HelloEncoded);
        var buffer = new byte[utf8.Length];
        utf8.CopyTo(buffer, 0);
        var written = Base64Url.DecodeFromUtf8InPlace(buffer);
        var result = buffer.AsSpan().Slice(0, written).ToArray();
        await Assert.That(result).IsEquivalentTo(HelloBytes);
    }

    [Test]
    public async Task IsValid_Chars_Valid()
    {
        await Assert.That(Base64Url.IsValid(HelloEncoded.AsSpan())).IsTrue();
        await Assert.That(Base64Url.IsValid(ReadOnlySpan<char>.Empty)).IsTrue();
    }

    [Test]
    public async Task IsValid_Chars_Invalid()
    {
        // Length 1 mod 4
        await Assert.That(Base64Url.IsValid("A".AsSpan())).IsFalse();
        // Invalid chars
        await Assert.That(Base64Url.IsValid("AB=D".AsSpan())).IsFalse();
        await Assert.That(Base64Url.IsValid("AB+D".AsSpan())).IsFalse();
    }

    [Test]
    public async Task IsValid_Chars_DecodedLength()
    {
        Base64Url.IsValid(HelloEncoded.AsSpan(), out var decodedLength);
        await Assert.That(decodedLength).IsEqualTo(HelloBytes.Length);
    }

    [Test]
    public async Task IsValid_Utf8_Valid()
    {
        var utf8 = System.Text.Encoding.UTF8.GetBytes(HelloEncoded);
        await Assert.That(Base64Url.IsValid((ReadOnlySpan<byte>)utf8)).IsTrue();
        await Assert.That(Base64Url.IsValid(ReadOnlySpan<byte>.Empty)).IsTrue();
    }

    [Test]
    public async Task IsValid_Utf8_Invalid()
    {
        await Assert.That(Base64Url.IsValid(new ReadOnlySpan<byte>([(byte)'A']))).IsFalse();
        await Assert.That(Base64Url.IsValid(new ReadOnlySpan<byte>([(byte)'A', (byte)'=']))).IsFalse();
    }

    [Test]
    public async Task Roundtrip()
    {
        byte[] original = [0, 1, 2, 127, 128, 253, 254, 255];
        var encoded = Base64Url.EncodeToString(original);
        var decoded = Base64Url.DecodeFromChars(encoded.AsSpan());
        await Assert.That(decoded).IsEquivalentTo(original);
    }

    [Test]
    public async Task Roundtrip_Utf8()
    {
        byte[] original = [0, 1, 2, 127, 128, 253, 254, 255];
        var encoded = Base64Url.EncodeToUtf8(original);
        var decoded = Base64Url.DecodeFromUtf8(encoded);
        await Assert.That(decoded).IsEquivalentTo(original);
    }

    [Test]
    public async Task EncodeToChars_OperationStatus_Done()
    {
        var destination = new char[20];
        var status = Base64Url.EncodeToChars(HelloBytes, destination, out var bytesConsumed, out var charsWritten);
        await Assert.That(status).IsEqualTo(OperationStatus.Done);
        await Assert.That(bytesConsumed).IsEqualTo(HelloBytes.Length);
        await Assert.That(charsWritten).IsEqualTo(HelloEncoded.Length);
    }

    [Test]
    public async Task DecodeFromChars_OperationStatus_Done()
    {
        var destination = new byte[20];
        var status = Base64Url.DecodeFromChars(HelloEncoded.AsSpan(), destination, out var charsConsumed, out var bytesWritten);
        await Assert.That(status).IsEqualTo(OperationStatus.Done);
        await Assert.That(charsConsumed).IsEqualTo(HelloEncoded.Length);
        await Assert.That(bytesWritten).IsEqualTo(HelloBytes.Length);
    }
}

#endif
