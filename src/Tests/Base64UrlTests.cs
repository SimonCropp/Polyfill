#if FeatureMemory

using System.Buffers;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

    [Test]
    [Arguments("AQ==", new byte[] {1})]
    [Arguments("AQ=", new byte[] {1})]
    [Arguments("AQ%%", new byte[] {1})]
    [Arguments("AQ=%", new byte[] {1})]
    [Arguments("AQ= =", new byte[] {1})]
    [Arguments("AQI=", new byte[] {1, 2})]
    [Arguments("AQI%", new byte[] {1, 2})]
    [Arguments("AQIDAQ==", new byte[] {1, 2, 3, 1})]
    [Arguments("AQID\n", new byte[] {1, 2, 3})]
    [Arguments(" A Q\tI\rD\n", new byte[] {1, 2, 3})]
    [Arguments("AQIDAQID AQ", new byte[] {1, 2, 3, 1, 2, 3, 1})]
    [Arguments(" ", new byte[0])]
    public async Task Decode_PaddingAndWhitespace(string text, byte[] expected)
    {
        var utf8 = Encoding.UTF8.GetBytes(text);

        await Assert.That(Base64Url.DecodeFromChars(text.AsSpan())).IsEquivalentTo(expected);
        await Assert.That(Base64Url.DecodeFromUtf8(utf8)).IsEquivalentTo(expected);

        var destination = new byte[20];
        var written = Base64Url.DecodeFromChars(text.AsSpan(), destination);
        await Assert.That(destination.AsSpan(0, written).ToArray()).IsEquivalentTo(expected);

        destination = new byte[20];
        var success = Base64Url.TryDecodeFromChars(text.AsSpan(), destination, out written);
        await Assert.That(success).IsTrue();
        await Assert.That(destination.AsSpan(0, written).ToArray()).IsEquivalentTo(expected);

        destination = new byte[20];
        success = Base64Url.TryDecodeFromUtf8(utf8, destination, out written);
        await Assert.That(success).IsTrue();
        await Assert.That(destination.AsSpan(0, written).ToArray()).IsEquivalentTo(expected);

        destination = new byte[20];
        var status = Base64Url.DecodeFromChars(text.AsSpan(), destination, out var consumed, out written);
        await Assert.That(status).IsEqualTo(OperationStatus.Done);
        await Assert.That(consumed).IsEqualTo(text.Length);
        await Assert.That(destination.AsSpan(0, written).ToArray()).IsEquivalentTo(expected);

        var inPlace = (byte[]) utf8.Clone();
        written = Base64Url.DecodeFromUtf8InPlace(inPlace);
        await Assert.That(inPlace.AsSpan(0, written).ToArray()).IsEquivalentTo(expected);

        var valid = Base64Url.IsValid(text.AsSpan(), out var decodedLength);
        await Assert.That(valid).IsTrue();
        await Assert.That(decodedLength).IsEqualTo(expected.Length);

        valid = Base64Url.IsValid(utf8, out decodedLength);
        await Assert.That(valid).IsTrue();
        await Assert.That(decodedLength).IsEqualTo(expected.Length);
    }

    [Test]
    [Arguments("AQ===")]
    [Arguments("AQID=")]
    [Arguments("AQ=I")]
    [Arguments("A=")]
    [Arguments("=")]
    [Arguments("====")]
    [Arguments("AQ==AQID")]
    [Arguments("AQ%3D")]
    [Arguments("AQID\f")]
    [Arguments("AQ\v==")]
    [Arguments("AR==")]
    [Arguments("AR")]
    [Arguments("AQJ")]
    public async Task Decode_InvalidPaddingAndWhitespace(string text)
    {
        var utf8 = Encoding.UTF8.GetBytes(text);

        await Assert.That(() => Base64Url.DecodeFromChars(text.AsSpan())).Throws<FormatException>();
        await Assert.That(() => Base64Url.DecodeFromUtf8(utf8)).Throws<FormatException>();
        await Assert.That(() => Base64Url.TryDecodeFromChars(text.AsSpan(), new byte[20], out _)).Throws<FormatException>();
        await Assert.That(() => Base64Url.TryDecodeFromUtf8(utf8, new byte[20], out _)).Throws<FormatException>();

        var status = Base64Url.DecodeFromChars(text.AsSpan(), new byte[20], out _, out _);
        await Assert.That(status).IsEqualTo(OperationStatus.InvalidData);

        var valid = Base64Url.IsValid(text.AsSpan(), out var decodedLength);
        await Assert.That(valid).IsFalse();
        await Assert.That(decodedLength).IsEqualTo(0);

        valid = Base64Url.IsValid(utf8, out decodedLength);
        await Assert.That(valid).IsFalse();
        await Assert.That(decodedLength).IsEqualTo(0);
    }

    [Test]
    public async Task DecodeFromChars_OperationStatus_Blocks()
    {
        var destination = new byte[20];

        var status = Base64Url.DecodeFromChars("AQIDAQ".AsSpan(), destination, out var consumed, out var written, isFinalBlock: false);
        await Assert.That(status).IsEqualTo(OperationStatus.NeedMoreData);
        await Assert.That(consumed).IsEqualTo(4);
        await Assert.That(written).IsEqualTo(3);

        // padding is only valid in the final block
        status = Base64Url.DecodeFromChars("AQIDAQ==".AsSpan(), destination, out consumed, out written, isFinalBlock: false);
        await Assert.That(status).IsEqualTo(OperationStatus.InvalidData);
        await Assert.That(consumed).IsEqualTo(4);
        await Assert.That(written).IsEqualTo(3);

        status = Base64Url.DecodeFromChars("AQID!".AsSpan(), destination, out consumed, out written);
        await Assert.That(status).IsEqualTo(OperationStatus.InvalidData);
        await Assert.That(consumed).IsEqualTo(4);
        await Assert.That(written).IsEqualTo(3);

        status = Base64Url.DecodeFromChars("AQIDAQ==".AsSpan(), new byte[3], out consumed, out written);
        await Assert.That(status).IsEqualTo(OperationStatus.DestinationTooSmall);
        await Assert.That(consumed).IsEqualTo(4);
        await Assert.That(written).IsEqualTo(3);
    }

    [Test]
    public async Task DecodeFromUtf8_OperationStatus_Done()
    {
        var destination = new byte[20];
        var status = Base64Url.DecodeFromUtf8("AQID"u8, destination, out var bytesConsumed, out var bytesWritten);
        await Assert.That(status).IsEqualTo(OperationStatus.Done);
        await Assert.That(bytesConsumed).IsEqualTo(4);
        await Assert.That(bytesWritten).IsEqualTo(3);
        await Assert.That(destination.AsSpan(0, 3).ToArray()).IsEquivalentTo(new byte[] {1, 2, 3});
    }

    static string ReferenceEncode(byte[] bytes) =>
        Convert.ToBase64String(bytes).TrimEnd('=').Replace('+', '-').Replace('/', '_');

    static byte[] PatternBytes(int length)
    {
        var bytes = new byte[length];

        for (var i = 0; i < length; i++)
        {
            bytes[i] = unchecked((byte) (i * 37 + 11));
        }

        return bytes;
    }

    [Test]
    public async Task Encode_Alphabet()
    {
        const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_";
        var source = Convert.FromBase64String(alphabet.Replace('-', '+').Replace('_', '/'));

        await Assert.That(Base64Url.EncodeToString(source)).IsEqualTo(alphabet);
        await Assert.That(Encoding.ASCII.GetString(Base64Url.EncodeToUtf8(source))).IsEqualTo(alphabet);
    }

    [Test]
    public async Task Encode_AllOverloads_MatchReference()
    {
        var failures = new List<string>();

        // 0 to 300 covers every remainder, and both the stack and the pooled path of EncodeToString
        for (var length = 0; length <= 300; length++)
        {
            var source = PatternBytes(length);
            var expected = ReferenceEncode(source);
            var expectedUtf8 = Encoding.ASCII.GetBytes(expected);

            void Check(string name, bool condition)
            {
                if (!condition)
                {
                    failures.Add($"{name} length {length}");
                }
            }

            Check("GetEncodedLength", Base64Url.GetEncodedLength(length) == expected.Length);
            Check("EncodeToString", Base64Url.EncodeToString(source) == expected);
            Check("EncodeToUtf8", Base64Url.EncodeToUtf8(source).SequenceEqual(expectedUtf8));

            // one spare element, to check nothing is written past the end
            var chars = new char[expected.Length + 1];
            chars[expected.Length] = '!';
            var written = Base64Url.EncodeToChars(source, chars);
            Check("EncodeToChars", written == expected.Length && new string(chars) == expected + "!");

            chars = new char[expected.Length + 1];
            chars[expected.Length] = '!';
            var success = Base64Url.TryEncodeToChars(source, chars, out written);
            Check("TryEncodeToChars", success && written == expected.Length && new string(chars) == expected + "!");

            chars = new char[expected.Length + 1];
            chars[expected.Length] = '!';
            var status = Base64Url.EncodeToChars(source, chars, out var consumed, out written);
            Check("EncodeToChars status", status == OperationStatus.Done && consumed == length && written == expected.Length && new string(chars) == expected + "!");

            var bytes = new byte[expected.Length + 1];
            bytes[expected.Length] = 0xEE;
            written = Base64Url.EncodeToUtf8(source, bytes);
            Check("EncodeToUtf8 destination", written == expected.Length && bytes.AsSpan(0, written).SequenceEqual(expectedUtf8) && bytes[expected.Length] == 0xEE);

            bytes = new byte[expected.Length + 1];
            bytes[expected.Length] = 0xEE;
            success = Base64Url.TryEncodeToUtf8(source, bytes, out written);
            Check("TryEncodeToUtf8", success && written == expected.Length && bytes.AsSpan(0, written).SequenceEqual(expectedUtf8) && bytes[expected.Length] == 0xEE);

            bytes = new byte[expected.Length + 1];
            bytes[expected.Length] = 0xEE;
            status = Base64Url.EncodeToUtf8(source, bytes, out consumed, out written);
            Check("EncodeToUtf8 status", status == OperationStatus.Done && consumed == length && written == expected.Length && bytes.AsSpan(0, written).SequenceEqual(expectedUtf8) && bytes[expected.Length] == 0xEE);

            bytes = new byte[expected.Length + 1];
            bytes[expected.Length] = 0xEE;
            source.CopyTo(bytes, 0);
            success = Base64Url.TryEncodeToUtf8InPlace(bytes, length, out written);
            Check("TryEncodeToUtf8InPlace", success && written == expected.Length && bytes.AsSpan(0, written).SequenceEqual(expectedUtf8) && bytes[expected.Length] == 0xEE);

            if (length > 0)
            {
                Check("TryEncodeToChars too small", !Base64Url.TryEncodeToChars(source, new char[expected.Length - 1], out _));
                Check("TryEncodeToUtf8 too small", !Base64Url.TryEncodeToUtf8(source, new byte[expected.Length - 1], out _));
            }
        }

        await Assert.That(failures).IsEmpty();
    }

    [Test]
    public async Task EncodeToString_Large()
    {
        var source = PatternBytes(100_000);
        await Assert.That(Base64Url.EncodeToString(source)).IsEqualTo(ReferenceEncode(source));
    }

    [Test]
    public async Task Encode_DestinationTooSmall_Throws()
    {
        var source = PatternBytes(10);
        await Assert.That(() => Base64Url.EncodeToChars(source, new char[13])).Throws<ArgumentException>();
        await Assert.That(() => Base64Url.EncodeToUtf8(source, new byte[13])).Throws<ArgumentException>();
    }

    [Test]
    public async Task TryEncodeToUtf8InPlace_TooSmall()
    {
        var buffer = PatternBytes(10);
        var success = Base64Url.TryEncodeToUtf8InPlace(buffer, 10, out var written);
        await Assert.That(success).IsFalse();
        await Assert.That(written).IsEqualTo(0);
        await Assert.That(buffer).IsEquivalentTo(PatternBytes(10));
    }

    [Test]
    public async Task EncodeToChars_OperationStatus_Blocks()
    {
        var source = PatternBytes(10);
        var expected = ReferenceEncode(source);

        // final block that does not fit: only the complete groups that fit are written
        var destination = new char[9];
        var status = Base64Url.EncodeToChars(source, destination, out var consumed, out var written);
        var text = new string(destination, 0, written);
        await Assert.That(status).IsEqualTo(OperationStatus.DestinationTooSmall);
        await Assert.That(consumed).IsEqualTo(6);
        await Assert.That(written).IsEqualTo(8);
        await Assert.That(text).IsEqualTo(expected.Substring(0, 8));
        await Assert.That(destination[8]).IsEqualTo('\0');

        // the trailing partial group is left for the next block
        destination = new char[20];
        status = Base64Url.EncodeToChars(source, destination, out consumed, out written, isFinalBlock: false);
        text = new string(destination, 0, written);
        await Assert.That(status).IsEqualTo(OperationStatus.NeedMoreData);
        await Assert.That(consumed).IsEqualTo(9);
        await Assert.That(written).IsEqualTo(12);
        await Assert.That(text).IsEqualTo(expected.Substring(0, 12));

        destination = new char[7];
        status = Base64Url.EncodeToChars(source, destination, out consumed, out written, isFinalBlock: false);
        text = new string(destination, 0, written);
        await Assert.That(status).IsEqualTo(OperationStatus.DestinationTooSmall);
        await Assert.That(consumed).IsEqualTo(3);
        await Assert.That(written).IsEqualTo(4);
        await Assert.That(text).IsEqualTo(expected.Substring(0, 4));

        destination = new char[12];
        status = Base64Url.EncodeToChars(source.AsSpan(0, 9), destination, out consumed, out written, isFinalBlock: false);
        text = new string(destination, 0, written);
        await Assert.That(status).IsEqualTo(OperationStatus.Done);
        await Assert.That(consumed).IsEqualTo(9);
        await Assert.That(written).IsEqualTo(12);
        await Assert.That(text).IsEqualTo(expected.Substring(0, 12));

        status = Base64Url.EncodeToChars(ReadOnlySpan<byte>.Empty, Span<char>.Empty, out consumed, out written);
        await Assert.That(status).IsEqualTo(OperationStatus.Done);
        await Assert.That(consumed).IsEqualTo(0);
        await Assert.That(written).IsEqualTo(0);
    }

    [Test]
    public async Task EncodeToUtf8_OperationStatus_Blocks()
    {
        var source = PatternBytes(10);
        var expected = ReferenceEncode(source);

        var destination = new byte[9];
        var status = Base64Url.EncodeToUtf8(source, destination, out var consumed, out var written);
        var text = Encoding.ASCII.GetString(destination, 0, written);
        await Assert.That(status).IsEqualTo(OperationStatus.DestinationTooSmall);
        await Assert.That(consumed).IsEqualTo(6);
        await Assert.That(written).IsEqualTo(8);
        await Assert.That(text).IsEqualTo(expected.Substring(0, 8));
        await Assert.That(destination[8]).IsEqualTo((byte) 0);

        destination = new byte[20];
        status = Base64Url.EncodeToUtf8(source, destination, out consumed, out written, isFinalBlock: false);
        text = Encoding.ASCII.GetString(destination, 0, written);
        await Assert.That(status).IsEqualTo(OperationStatus.NeedMoreData);
        await Assert.That(consumed).IsEqualTo(9);
        await Assert.That(written).IsEqualTo(12);
        await Assert.That(text).IsEqualTo(expected.Substring(0, 12));

        destination = new byte[7];
        status = Base64Url.EncodeToUtf8(source, destination, out consumed, out written, isFinalBlock: false);
        text = Encoding.ASCII.GetString(destination, 0, written);
        await Assert.That(status).IsEqualTo(OperationStatus.DestinationTooSmall);
        await Assert.That(consumed).IsEqualTo(3);
        await Assert.That(written).IsEqualTo(4);
        await Assert.That(text).IsEqualTo(expected.Substring(0, 4));
    }
}

#endif
