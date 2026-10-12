#if FeatureMemory

using System.Buffers;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

    static byte[] StatusPatternBytes(int length)
    {
        var bytes = new byte[length];

        for (var i = 0; i < length; i++)
        {
            bytes[i] = unchecked((byte) (i * 37 + 11));
        }

        return bytes;
    }

    [Test]
    public async Task OperationStatus_AllLengths_MatchConvert()
    {
        var failures = new System.Collections.Generic.List<string>();

        // 0 to 400 covers every remainder, and both the stack and the pooled buffers
        for (var length = 0; length <= 400; length++)
        {
            var source = StatusPatternBytes(length);
            var expected = Convert.ToBase64String(source);

            void Check(string name, bool condition)
            {
                if (!condition)
                {
                    failures.Add($"{name} length {length}");
                }
            }

            // one spare element, to check nothing is written past the end
            var chars = new char[expected.Length + 1];
            chars[expected.Length] = '!';
            var status = Base64.EncodeToChars(source, chars, out var consumed, out var written);
            Check("EncodeToChars", status == OperationStatus.Done && consumed == length && written == expected.Length && new string(chars) == expected + "!");

            // a destination far larger than needed
            chars = new char[expected.Length + 5000];
            status = Base64.EncodeToChars(source, chars, out consumed, out written);
            Check("EncodeToChars large destination", status == OperationStatus.Done && consumed == length && written == expected.Length && new string(chars, 0, written) == expected && chars[written] == '\0');

            var bytes = new byte[length + 1];
            bytes[length] = 0xEE;
            status = Base64.DecodeFromChars(expected.AsSpan(), bytes, out consumed, out written);
            Check("DecodeFromChars", status == OperationStatus.Done && consumed == expected.Length && written == length && bytes.AsSpan(0, length).SequenceEqual(source) && bytes[length] == 0xEE);

            if (length > 0)
            {
                // only the complete groups that fit are written
                var fit = (expected.Length - 1) / 4;
                chars = new char[expected.Length - 1];
                status = Base64.EncodeToChars(source, chars, out consumed, out written);
                Check("EncodeToChars too small", status == OperationStatus.DestinationTooSmall && consumed == fit * 3 && written == fit * 4 && new string(chars, 0, written) == expected.Substring(0, written));
            }

            if (length >= 3)
            {
                var complete = length / 3;
                bytes = new byte[complete * 3 - 1];
                status = Base64.DecodeFromChars(expected.AsSpan(), bytes, out consumed, out written);
                Check("DecodeFromChars too small", status == OperationStatus.DestinationTooSmall && consumed == (complete - 1) * 4 && written == (complete - 1) * 3 && bytes.AsSpan(0, written).SequenceEqual(source.AsSpan(0, written)));
            }
        }

        await Assert.That(failures).IsEmpty();
    }

    [Test]
    [Arguments(10)]
    [Arguments(1000)]
    public async Task EncodeToChars_OperationStatus_NotFinalBlock(int length)
    {
        var source = StatusPatternBytes(length);
        var expected = Convert.ToBase64String(source);
        var complete = length / 3;

        // the trailing partial group is left for the next block
        var destination = new char[expected.Length + 10];
        var status = Base64.EncodeToChars(source, destination, out var consumed, out var written, isFinalBlock: false);
        var text = new string(destination, 0, written);
        await Assert.That(status).IsEqualTo(OperationStatus.NeedMoreData);
        await Assert.That(consumed).IsEqualTo(complete * 3);
        await Assert.That(written).IsEqualTo(complete * 4);
        await Assert.That(text).IsEqualTo(expected.Substring(0, complete * 4));

        destination = new char[complete * 4 - 1];
        status = Base64.EncodeToChars(source, destination, out consumed, out written, isFinalBlock: false);
        text = new string(destination, 0, written);
        await Assert.That(status).IsEqualTo(OperationStatus.DestinationTooSmall);
        await Assert.That(consumed).IsEqualTo((complete - 1) * 3);
        await Assert.That(written).IsEqualTo((complete - 1) * 4);
        await Assert.That(text).IsEqualTo(expected.Substring(0, (complete - 1) * 4));

        status = Base64.EncodeToChars(source.AsSpan(0, complete * 3), destination, out consumed, out written, isFinalBlock: false);
        await Assert.That(status).IsEqualTo(OperationStatus.DestinationTooSmall);

        destination = new char[complete * 4];
        status = Base64.EncodeToChars(source.AsSpan(0, complete * 3), destination, out consumed, out written, isFinalBlock: false);
        text = new string(destination);

        // System.Memory reports NeedMoreData for a block that is not final, even when nothing is left over
        await Assert.That(status is OperationStatus.Done or OperationStatus.NeedMoreData).IsTrue();
        await Assert.That(consumed).IsEqualTo(complete * 3);
        await Assert.That(written).IsEqualTo(complete * 4);
        await Assert.That(text).IsEqualTo(expected.Substring(0, complete * 4));
    }

    [Test]
    public async Task EncodeToChars_OperationStatus_EmptyDestination()
    {
        var status = Base64.EncodeToChars(HelloBytes, Span<char>.Empty, out var consumed, out var written);
        await Assert.That(status).IsEqualTo(OperationStatus.DestinationTooSmall);
        await Assert.That(consumed).IsEqualTo(0);
        await Assert.That(written).IsEqualTo(0);

        status = Base64.EncodeToChars(ReadOnlySpan<byte>.Empty, Span<char>.Empty, out consumed, out written);
        await Assert.That(status).IsEqualTo(OperationStatus.Done);
        await Assert.That(consumed).IsEqualTo(0);
        await Assert.That(written).IsEqualTo(0);
    }

    [Test]
    [Arguments(0)]
    [Arguments(400)]
    public async Task DecodeFromChars_OperationStatus_NotFinalBlock(int prefixLength)
    {
        // a long prefix takes the pooled buffer path
        var prefix = Convert.ToBase64String(StatusPatternBytes(prefixLength / 4 * 3));
        var text = prefix + "AQIDAQ";
        var destination = new byte[text.Length];
        var status = Base64.DecodeFromChars(text.AsSpan(), destination, out var consumed, out var written, isFinalBlock: false);
        await Assert.That(status).IsEqualTo(OperationStatus.NeedMoreData);
        await Assert.That(consumed).IsEqualTo(prefix.Length + 4);
        await Assert.That(written).IsEqualTo(prefix.Length / 4 * 3 + 3);
    }

    [Test]
    [Arguments(0)]
    [Arguments(400)]
    public async Task DecodeFromChars_OperationStatus_Invalid(int prefixLength)
    {
        var prefixBytes = StatusPatternBytes(prefixLength / 4 * 3);
        var prefix = Convert.ToBase64String(prefixBytes);
        var destination = new byte[prefix.Length + 20];

        var status = Base64.DecodeFromChars((prefix + "AQID!!!!").AsSpan(), destination, out var consumed, out var written);
        var decoded = destination.AsSpan(0, prefixBytes.Length).ToArray();
        await Assert.That(status).IsEqualTo(OperationStatus.InvalidData);
        await Assert.That(consumed).IsEqualTo(prefix.Length + 4);
        await Assert.That(written).IsEqualTo(prefixBytes.Length + 3);
        await Assert.That(decoded).IsEquivalentTo(prefixBytes);

        // U+0141 narrows to the byte 'A'
        status = Base64.DecodeFromChars((prefix + "AQIDŁQID").AsSpan(), destination, out consumed, out written);
        await Assert.That(status).IsEqualTo(OperationStatus.InvalidData);
        await Assert.That(consumed).IsEqualTo(prefix.Length + 4);
        await Assert.That(written).IsEqualTo(prefixBytes.Length + 3);
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

    [Test]
    [Arguments("AQI\u0144", 0, 0)]
    [Arguments("\u0141QID", 0, 0)]
    [Arguments("AQIDAQI\u0144", 4, 3)]
    public async Task DecodeFromChars_OperationStatus_NonAscii(string text, int expectedConsumed, int expectedWritten)
    {
        // U+0144 and U+0141 narrow to the bytes 'D' and 'A'
        var destination = new byte[20];
        var status = Base64.DecodeFromChars(text.AsSpan(), destination, out var consumed, out var written);
        await Assert.That(status).IsEqualTo(OperationStatus.InvalidData);
        await Assert.That(consumed).IsEqualTo(expectedConsumed);
        await Assert.That(written).IsEqualTo(expectedWritten);
    }

    [Test]
    [Arguments("!!!!")]
    [Arguments("AQI")]
    [Arguments("AQ=I")]
    public async Task TryDecodeFromChars_Invalid(string text) =>
        await Assert.That(() => Base64.TryDecodeFromChars(text.AsSpan(), new byte[20], out _)).Throws<FormatException>();

    [Test]
    public async Task TryDecodeFromChars_TooSmall()
    {
        var destination = new byte[2];
        var result = Base64.TryDecodeFromChars("AQID".AsSpan(), destination, out var bytesWritten);
        await Assert.That(result).IsFalse();
        await Assert.That(bytesWritten).IsEqualTo(0);
    }

    // The Try overloads are part of the BCL from net11. It reports what was decoded before running out of room,
    // and reports a destination that is too small ahead of invalid input that comes later.
    static readonly bool isPolyfill = typeof(Base64).GetMethod("TryDecodeFromChars") == null;

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
    public async Task EncodeAndDecode_AllLengths_MatchConvert()
    {
        var failures = new List<string>();

        // 0 to 400 covers every remainder, and both the stack and the pooled buffers
        for (var length = 0; length <= 400; length++)
        {
            var source = PatternBytes(length);
            var expected = Convert.ToBase64String(source);
            var expectedUtf8 = Encoding.ASCII.GetBytes(expected);

            void Check(string name, bool condition)
            {
                if (!condition)
                {
                    failures.Add($"{name} length {length}");
                }
            }

            Check("GetEncodedLength", Base64.GetEncodedLength(length) == expected.Length);
            Check("EncodeToString", Base64.EncodeToString(source) == expected);
            Check("EncodeToChars array", new string(Base64.EncodeToChars(source)) == expected);
            Check("EncodeToUtf8 array", Base64.EncodeToUtf8(source).SequenceEqual(expectedUtf8));

            // one spare element, to check nothing is written past the end
            var chars = new char[expected.Length + 1];
            chars[expected.Length] = '!';
            var written = Base64.EncodeToChars(source, chars);
            Check("EncodeToChars", written == expected.Length && new string(chars) == expected + "!");

            chars = new char[expected.Length + 1];
            chars[expected.Length] = '!';
            var success = Base64.TryEncodeToChars(source, chars, out written);
            Check("TryEncodeToChars", success && written == expected.Length && new string(chars) == expected + "!");

            Check("DecodeFromChars array", Base64.DecodeFromChars(expected.AsSpan()).SequenceEqual(source));
            Check("DecodeFromUtf8 array", Base64.DecodeFromUtf8(expectedUtf8).SequenceEqual(source));

            var bytes = new byte[length + 1];
            bytes[length] = 0xEE;
            written = Base64.DecodeFromChars(expected.AsSpan(), bytes);
            Check("DecodeFromChars", written == length && bytes.AsSpan(0, length).SequenceEqual(source) && bytes[length] == 0xEE);

            bytes = new byte[length + 1];
            bytes[length] = 0xEE;
            success = Base64.TryDecodeFromChars(expected.AsSpan(), bytes, out written);
            Check("TryDecodeFromChars", success && written == length && bytes.AsSpan(0, length).SequenceEqual(source) && bytes[length] == 0xEE);

            if (length > 0)
            {
                success = Base64.TryEncodeToChars(source, new char[expected.Length - 1], out written);
                Check("TryEncodeToChars too small", !success && (written == 0 || !isPolyfill));

                success = Base64.TryDecodeFromChars(expected.AsSpan(), new byte[length - 1], out written);
                Check("TryDecodeFromChars too small", !success && (written == 0 || !isPolyfill));
            }
        }

        await Assert.That(failures).IsEmpty();
    }

    [Test]
    public async Task Encode_DestinationTooSmall_Throws()
    {
        var source = PatternBytes(10);
        await Assert.That(() => Base64.EncodeToChars(source, new char[15])).Throws<ArgumentException>();
    }

    [Test]
    [Arguments(4)]
    [Arguments(400)]
    public async Task DecodeFromChars_DestinationTooSmall(int length)
    {
        var encoded = Convert.ToBase64String(PatternBytes(length));
        await Assert.That(() => Base64.DecodeFromChars(encoded.AsSpan(), new byte[length - 1])).Throws<ArgumentException>();
    }

    [Test]
    [Arguments("")]
    [Arguments("  ")]
    [Arguments("\r\n")]
    public async Task Decode_EmptyAndWhitespace(string text)
    {
        var destination = new byte[4];
        var utf8 = Encoding.ASCII.GetBytes(text);

        await Assert.That(Base64.DecodeFromChars(text.AsSpan())).IsEmpty();
        await Assert.That(Base64.DecodeFromUtf8(utf8)).IsEmpty();
        await Assert.That(Base64.DecodeFromChars(text.AsSpan(), destination)).IsEqualTo(0);

        var success = Base64.TryDecodeFromChars(text.AsSpan(), Span<byte>.Empty, out var written);
        await Assert.That(success).IsTrue();
        await Assert.That(written).IsEqualTo(0);
    }

    [Test]
    [Arguments("SGVs\r\nbG8=")]
    [Arguments(" SGVsbG8= ")]
    [Arguments("S G\tV\ns\rbG8=")]
    public async Task Decode_Whitespace(string text)
    {
        var utf8 = Encoding.ASCII.GetBytes(text);
        var destination = new byte[HelloBytes.Length];
        var tryDestination = new byte[HelloBytes.Length];
        var written = Base64.DecodeFromChars(text.AsSpan(), destination);
        var success = Base64.TryDecodeFromChars(text.AsSpan(), tryDestination, out var tryWritten);

        await Assert.That(Base64.DecodeFromChars(text.AsSpan())).IsEquivalentTo(HelloBytes);
        await Assert.That(Base64.DecodeFromUtf8(utf8)).IsEquivalentTo(HelloBytes);
        await Assert.That(written).IsEqualTo(HelloBytes.Length);
        await Assert.That(destination).IsEquivalentTo(HelloBytes);
        await Assert.That(success).IsTrue();
        await Assert.That(tryWritten).IsEqualTo(HelloBytes.Length);
        await Assert.That(tryDestination).IsEquivalentTo(HelloBytes);
    }

    [Test]
    [Arguments("!!!!", 0)]
    [Arguments("AQI", 0)]
    [Arguments("AQ=I", 0)]
    [Arguments("AQID=", 0)]
    [Arguments("AQIDAQI*", 0)]
    [Arguments("!!!!", 400)]
    [Arguments("AQI", 400)]
    [Arguments("AQ=I", 400)]
    public async Task Decode_Invalid_Throws(string suffix, int validPrefixLength)
    {
        // a long valid prefix takes the pooled buffer path
        var text = Convert.ToBase64String(PatternBytes(validPrefixLength / 4 * 3)) + suffix;
        var utf8 = Encoding.ASCII.GetBytes(text);

        await Assert.That(() => Base64.DecodeFromChars(text.AsSpan())).Throws<FormatException>();
        await Assert.That(() => Base64.DecodeFromUtf8(utf8)).Throws<FormatException>();
        await Assert.That(() => Base64.DecodeFromChars(text.AsSpan(), new byte[text.Length])).Throws<FormatException>();
        await Assert.That(() => Base64.TryDecodeFromChars(text.AsSpan(), new byte[text.Length], out _)).Throws<FormatException>();

        if (isPolyfill)
        {
            // invalid input is reported even when the destination is also too small
            await Assert.That(() => Base64.DecodeFromChars(text.AsSpan(), Span<byte>.Empty)).Throws<FormatException>();
            await Assert.That(() => Base64.TryDecodeFromChars(text.AsSpan(), Span<byte>.Empty, out _)).Throws<FormatException>();
        }
    }

    [Test]
    public async Task DecodeFromUtf8_NonAscii_Throws()
    {
        // 0xC1 would be 'A' if only the low 7 bits were looked at
        byte[] utf8 = [(byte) 'A', (byte) 'Q', (byte) 'I', 0xC1];
        await Assert.That(() => Base64.DecodeFromUtf8(utf8)).Throws<FormatException>();
    }

    [Test]
    public async Task TryDecodeFromChars_TooSmall_LeavesDestination()
    {
        var encoded = Convert.ToBase64String(PatternBytes(9));
        var destination = new byte[8];
        var success = Base64.TryDecodeFromChars(encoded.AsSpan(), destination, out var written);
        await Assert.That(success).IsFalse();

        if (isPolyfill)
        {
            await Assert.That(written).IsEqualTo(0);
            await Assert.That(destination).IsEquivalentTo(new byte[8]);
        }
    }
}

#endif
