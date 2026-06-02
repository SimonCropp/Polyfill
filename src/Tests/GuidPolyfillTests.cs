public class GuidPolyfillTests
{
    [Test]
    public async Task TryParse_ReturnsTrue_ForValidGuid()
    {
        var guidString = Guid.NewGuid().ToString();
        var result = Guid.TryParse(guidString, null, out var guid);
        await Assert.That(result).IsTrue();
        await Assert.That(guid).IsNotEqualTo(Guid.Empty);
    }

    [Test]
    public async Task TryParse_ReturnsFalse_ForInvalidGuid()
    {
        var result = Guid.TryParse("not-a-guid", null, out var guid);
        await Assert.That(result).IsFalse();
        await Assert.That(guid).IsEqualTo(Guid.Empty);
    }

#if FeatureMemory
    [Test]
    public async Task TryParse_Span_ReturnsTrue_ForValidGuid()
    {
        var guid = Guid.NewGuid();
        var span = guid.ToString().AsSpan();
        var result = Guid.TryParse(span, null, out var parsed);
        await Assert.That(result).IsTrue();
        await Assert.That(parsed).IsEqualTo(guid);
    }

    [Test]
    public async Task TryParse_ReturnsTrue_ForValidUtf8Bytes()
    {
        var guid = Guid.NewGuid();
        var utf8Bytes = Encoding.UTF8.GetBytes(guid.ToString());
        var result = Guid.TryParse(utf8Bytes, out var parsed);
        await Assert.That(result).IsTrue();
        await Assert.That(parsed).IsEqualTo(guid);
    }

    [Test]
    public async Task Parse_ReturnsGuid_ForValidUtf8Bytes()
    {
        var guid = Guid.NewGuid();
        var utf8Bytes = Encoding.UTF8.GetBytes(guid.ToString());
        var parsed = Guid.Parse(utf8Bytes);
        await Assert.That(parsed).IsEqualTo(guid);
    }

    [Test]
    public async Task Parse_ThrowsFormatException_ForInvalidUtf8Bytes()
    {
        var utf8Bytes = "not-a-guid"u8.ToArray();
        await Assert.That(() => Guid.Parse(utf8Bytes)).Throws<FormatException>();
    }

    [Test]
    public async Task TryParse_ReturnsFalse_ForInvalidUtf8Bytes()
    {
        var utf8Bytes = "not-a-guid"u8.ToArray();
        var result = Guid.TryParse(utf8Bytes, out var parsed);
        await Assert.That(result).IsFalse();
        await Assert.That(parsed).IsEqualTo(Guid.Empty);
    }

    [Test]
    public async Task TryParse_Span_ReturnsFalse_ForInvalidGuid()
    {
        var span = "invalid".AsSpan();
        var result = Guid.TryParse(span, null, out var parsed);
        await Assert.That(result).IsFalse();
        await Assert.That(parsed).IsEqualTo(Guid.Empty);
    }

    [Test]
    public async Task TryParse_Span_NoProvider_ReturnsTrue_ForValidGuid()
    {
        var guid = Guid.NewGuid();
        var span = guid.ToString().AsSpan();
        var result = Guid.TryParse(span, out var parsed);
        await Assert.That(result).IsTrue();
        await Assert.That(parsed).IsEqualTo(guid);
    }

    [Test]
    public async Task TryParse_Span_NoProvider_ReturnsFalse_ForInvalidGuid()
    {
        var span = "invalid".AsSpan();
        var result = Guid.TryParse(span, out var parsed);
        await Assert.That(result).IsFalse();
        await Assert.That(parsed).IsEqualTo(Guid.Empty);
    }

    [Test]
    public async Task TryParseExact_Span_ReturnsTrue_ForExactFormat()
    {
        var guid = Guid.NewGuid();
        var span = guid.ToString("N").AsSpan();
        var format = "N".AsSpan();
        var result = Guid.TryParseExact(span, format, out var parsed);
        await Assert.That(result).IsTrue();
        await Assert.That(parsed).IsEqualTo(guid);
    }

    [Test]
    public async Task TryParseExact_Span_ReturnsFalse_ForInvalidFormat()
    {
        var span = "invalid".AsSpan();
        var format = "N".AsSpan();
        var result = Guid.TryParseExact(span, format, out var parsed);
        await Assert.That(result).IsFalse();
        await Assert.That(parsed).IsEqualTo(Guid.Empty);
    }
#endif

    [Test]
    public async Task CreateVersion7_ReturnsUniqueGuids()
    {
        var guid1 = Guid.CreateVersion7();
        var guid2 = Guid.CreateVersion7();
        await Assert.That(guid1).IsNotEqualTo(guid2);
    }

    [Test]
    public async Task CreateVersion7_SetsVersionAndVariantBits()
    {
        // Run many times: without the variant fix byte 8 is fully random,
        // so the variant nibble would pass by luck ~25% of the time. Looping
        // makes a missing variant a deterministic failure (0.25^N).
        for (var iteration = 0; iteration < 100; iteration++)
        {
            var text = Guid.CreateVersion7().ToString();
            // RFC 9562: first hex digit of the 3rd group is the version → '7'
            await Assert.That(text[14]).IsEqualTo('7');
            // RFC 9562: first hex digit of the 4th group encodes the '10xx' variant → 8, 9, a or b
            var variant = text[19];
            await Assert.That(variant is '8' or '9' or 'a' or 'b').IsTrue();
        }
    }

    [Test]
    public async Task CreateVersion7_WithTimestamp_EncodesTimestampExactly()
    {
        var timestamp = DateTimeOffset.FromUnixTimeMilliseconds(1_700_000_000_000);
        var guid = Guid.CreateVersion7(timestamp);

        // RFC 9562: the first 48 bits (12 hex digits) are the Unix-ms timestamp, big-endian.
        var text = guid.ToString();
        var timeHex = text.Substring(0, 8) + text.Substring(9, 4);
        var expected = timestamp.ToUnixTimeMilliseconds().ToString("x12");
        await Assert.That(timeHex).IsEqualTo(expected);
    }

    [Test]
    public async Task CreateVersion7_WithTimestamp_ReturnsDeterministicPrefix()
    {
        var timestamp = DateTimeOffset.UtcNow;
        var guid1 = Guid.CreateVersion7(timestamp);
        var guid2 = Guid.CreateVersion7(timestamp);
        // The time-based prefix should match, but random part will differ
        await Assert.That(guid1).IsNotEqualTo(guid2);
        await Assert.That(guid1.ToByteArray().Take(6).SequenceEqual(guid2.ToByteArray().Take(6))).IsTrue();
    }
}
