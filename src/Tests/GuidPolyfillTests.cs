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

    // The 48 bit Unix millisecond timestamp, which RFC 9562 puts big-endian in the first six
    // bytes. Guid.ToByteArray writes the first two fields little-endian.
    static long Version7Timestamp(Guid guid)
    {
        var bytes = guid.ToByteArray();
        return ((long) bytes[3] << 40) |
               ((long) bytes[2] << 32) |
               ((long) bytes[1] << 24) |
               ((long) bytes[0] << 16) |
               ((long) bytes[5] << 8) |
               bytes[4];
    }

    static IEnumerable<long> Version7Timestamps()
    {
        yield return 0;
        yield return 1;
        yield return 0xFF;
        yield return 0x100;
        yield return 0xFFFF;
        yield return 0x1_0000;
        yield return 0xFF_FFFF;
        yield return 0x100_0000;
        yield return 0x7FFF_FFFF;
        yield return 0x8000_0000;
        yield return 0xFFFF_FFFF;
        yield return 0x1_0000_0000;
        yield return 1_700_000_000_000;
        // the highest bit of the first Guid field becomes set from here on
        yield return 0x7FFF_FFFF_FFFF;
        yield return 0x8000_0000_0000;
        yield return 0x8000_0000_0001;
        yield return 0xABCD_EF01_2345;
        yield return DateTimeOffset.MaxValue.ToUnixTimeMilliseconds();
    }

    [Test]
    public async Task CreateVersion7_WithTimestamp_RoundTripsTimestamp()
    {
        foreach (var milliseconds in Version7Timestamps())
        {
            var guid = Guid.CreateVersion7(DateTimeOffset.FromUnixTimeMilliseconds(milliseconds));

            await Assert.That(Version7Timestamp(guid)).IsEqualTo(milliseconds);
            await Assert.That(guid.ToString("N").Substring(0, 12)).IsEqualTo(milliseconds.ToString("x12"));
        }
    }

    [Test]
    public async Task CreateVersion7_WithTimestamp_IgnoresOffsetAndSubMillisecondTicks()
    {
        var utc = new DateTimeOffset(2024, 2, 29, 23, 59, 59, 999, TimeSpan.Zero);
        var expected = utc.ToUnixTimeMilliseconds();

        // the same instant expressed with another offset
        await Assert.That(Version7Timestamp(Guid.CreateVersion7(utc.ToOffset(TimeSpan.FromHours(10))))).IsEqualTo(expected);
        await Assert.That(Version7Timestamp(Guid.CreateVersion7(utc.ToOffset(TimeSpan.FromHours(-11.5))))).IsEqualTo(expected);
        // ticks below a millisecond are dropped, not rounded
        await Assert.That(Version7Timestamp(Guid.CreateVersion7(utc.AddTicks(9_999)))).IsEqualTo(expected);
        await Assert.That(Version7Timestamp(Guid.CreateVersion7(utc.AddTicks(10_000)))).IsEqualTo(expected + 1);
    }

    [Test]
    public async Task CreateVersion7_WithTimestamp_SetsVersionAndVariantBits()
    {
        foreach (var milliseconds in Version7Timestamps())
        {
            for (var iteration = 0; iteration < 50; iteration++)
            {
                var bytes = Guid.CreateVersion7(DateTimeOffset.FromUnixTimeMilliseconds(milliseconds)).ToByteArray();

                // ver: the high nibble of the third field
                await Assert.That(bytes[7] >> 4).IsEqualTo(7);
                // var: the two high bits of the ninth byte are 10
                await Assert.That(bytes[8] & 0xC0).IsEqualTo(0x80);
            }
        }
    }

    [Test]
    public async Task CreateVersion7_UsesCurrentTime()
    {
        var before = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        var guid = Guid.CreateVersion7();
        var after = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        var timestamp = Version7Timestamp(guid);
        await Assert.That(timestamp >= before).IsTrue();
        await Assert.That(timestamp <= after).IsTrue();
        await Assert.That(guid.ToByteArray()[7] >> 4).IsEqualTo(7);
    }

    // Guids for increasing timestamps sort in that order, both as Guids and as text, including
    // across the point where the highest bit of the first field becomes set.
    [Test]
    public async Task CreateVersion7_WithTimestamp_OrdersByTimestamp()
    {
        var timestamps = new List<long>(Version7Timestamps());
        for (var index = 0; index < 200; index++)
        {
            timestamps.Add(1_700_000_000_000 + index);
        }

        timestamps.Sort();

        Guid? previous = null;
        var previousTimestamp = -1L;
        foreach (var milliseconds in timestamps)
        {
            if (milliseconds == previousTimestamp)
            {
                continue;
            }

            var guid = Guid.CreateVersion7(DateTimeOffset.FromUnixTimeMilliseconds(milliseconds));
            if (previous != null)
            {
                await Assert.That(previous.Value.CompareTo(guid) < 0).IsTrue();
                await Assert.That(string.CompareOrdinal(previous.Value.ToString(), guid.ToString()) < 0).IsTrue();
            }

            previous = guid;
            previousTimestamp = milliseconds;
        }
    }

    // Everything outside the timestamp, version and variant is random: 74 bits. Over many Guids
    // for one timestamp every one of those bits has to come up both set and clear, and no two
    // Guids may be equal.
    [Test]
    public async Task CreateVersion7_WithTimestamp_RandomBitsVary()
    {
        var timestamp = DateTimeOffset.FromUnixTimeMilliseconds(1_700_000_000_000);
        var seenSet = new byte[16];
        var seenClear = new byte[16];
        var distinct = new HashSet<Guid>();
        for (var iteration = 0; iteration < 2000; iteration++)
        {
            var guid = Guid.CreateVersion7(timestamp);
            distinct.Add(guid);
            var bytes = guid.ToByteArray();
            for (var index = 0; index < 16; index++)
            {
                seenSet[index] |= bytes[index];
                seenClear[index] |= (byte) ~bytes[index];
            }
        }

        await Assert.That(distinct.Count).IsEqualTo(2000);

        // bytes 6 and 7 hold the third field little-endian: 12 random bits below the version
        await Assert.That(seenSet[6]).IsEqualTo((byte) 0xFF);
        await Assert.That(seenClear[6]).IsEqualTo((byte) 0xFF);
        await Assert.That(seenSet[7] & 0x0F).IsEqualTo(0x0F);
        await Assert.That(seenClear[7] & 0x0F).IsEqualTo(0x0F);
        // byte 8: 6 random bits below the variant
        await Assert.That(seenSet[8] & 0x3F).IsEqualTo(0x3F);
        await Assert.That(seenClear[8] & 0x3F).IsEqualTo(0x3F);
        for (var index = 9; index < 16; index++)
        {
            await Assert.That(seenSet[index]).IsEqualTo((byte) 0xFF);
            await Assert.That(seenClear[index]).IsEqualTo((byte) 0xFF);
        }
    }

    [Test]
    public async Task CreateVersion7_ManyGuids_AreDistinct()
    {
        var distinct = new HashSet<Guid>();
        for (var iteration = 0; iteration < 10_000; iteration++)
        {
            distinct.Add(Guid.CreateVersion7());
        }

        await Assert.That(distinct.Count).IsEqualTo(10_000);
    }
}
