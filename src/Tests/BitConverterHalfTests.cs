#if NET5_0_OR_GREATER

// Half is a 16 bit type, so every representable value can be checked rather than sampled.
// Runs on net5.0 and later, so net6.0 and above validate the BCL and net5.0 the polyfill.
public class BitConverterHalfTests
{
    [Test]
    public async Task EveryBitPatternRoundTrips()
    {
        var failures = 0;
        for (var i = 0; i <= ushort.MaxValue; i++)
        {
            var bits = (ushort) i;
            var signedBits = unchecked((short) bits);

            var value = BitConverter.UInt16BitsToHalf(bits);
            if (BitConverter.HalfToUInt16Bits(value) != bits)
            {
                failures++;
                continue;
            }

            if (BitConverter.HalfToInt16Bits(BitConverter.Int16BitsToHalf(signedBits)) != signedBits)
            {
                failures++;
                continue;
            }

            // the signed and unsigned views must be the same sixteen bits
            if (unchecked((ushort) BitConverter.HalfToInt16Bits(value)) != bits)
            {
                failures++;
                continue;
            }

            var bytes = BitConverter.GetBytes(value);
            if (bytes.Length != 2 ||
                BitConverter.HalfToUInt16Bits(BitConverter.ToHalf(bytes, 0)) != bits ||
                BitConverter.HalfToUInt16Bits(ToHalfSpan(bytes)) != bits)
            {
                failures++;
                continue;
            }

            var written = new byte[2];
            if (!TryWrite(written, value) ||
                written[0] != bytes[0] ||
                written[1] != bytes[1])
            {
                failures++;
            }
        }

        await Assert.That(failures).IsEqualTo(0);
    }

    // NaN payloads, both infinities, negative zero and the subnormal boundary are the patterns
    // a float based implementation would silently lose, so they are pinned explicitly
    [Test]
    public async Task NotablePatterns()
    {
        await Assert.That(Hex(0x0000)).IsEqualTo("0000");
        await Assert.That(Hex(0x8000)).IsEqualTo("0080");
        await Assert.That(Hex(0x3C00)).IsEqualTo("003c");
        await Assert.That(Hex(0x7C00)).IsEqualTo("007c");
        await Assert.That(Hex(0xFC00)).IsEqualTo("00fc");
        await Assert.That(Hex(0x7E00)).IsEqualTo("007e");
        await Assert.That(Hex(0x7C01)).IsEqualTo("017c");
        await Assert.That(Hex(0x0001)).IsEqualTo("0100");
        await Assert.That(Hex(0x7BFF)).IsEqualTo("ff7b");

        await Assert.That(BitConverter.HalfToInt16Bits(BitConverter.UInt16BitsToHalf(0x8000))).IsEqualTo(short.MinValue);
        await Assert.That(BitConverter.HalfToUInt16Bits(Half.NaN)).IsEqualTo((ushort) 0xFE00);
        await Assert.That(BitConverter.HalfToUInt16Bits(Half.PositiveInfinity)).IsEqualTo((ushort) 0x7C00);
        await Assert.That(BitConverter.HalfToUInt16Bits(Half.NegativeInfinity)).IsEqualTo((ushort) 0xFC00);
    }

    [Test]
    public async Task ArrayArgumentValidation()
    {
        var four = new byte[4];

        await Assert.That(Capture(() => BitConverter.ToHalf(null!, 0))).IsEqualTo("ArgumentNullException:value");
        await Assert.That(Capture(() => BitConverter.ToHalf(four, -1))).IsEqualTo("ArgumentOutOfRangeException:startIndex");
        await Assert.That(Capture(() => BitConverter.ToHalf(four, 4))).IsEqualTo("ArgumentOutOfRangeException:startIndex");

        // in range as an index, but only one byte remains
        await Assert.That(Capture(() => BitConverter.ToHalf(four, 3))).IsEqualTo("ArgumentException:value");

        // the last position that still leaves two bytes
        await Assert.That(Capture(() => BitConverter.ToHalf(four, 2))).IsEqualTo("none");
    }

    [Test]
    public async Task SpanArgumentValidationAndSizing()
    {
        await Assert.That(Capture(() => ToHalfSpan(new byte[1]))).IsEqualTo("ArgumentOutOfRangeException:value");
        await Assert.That(Capture(() => ToHalfSpan(new byte[2]))).IsEqualTo("none");
        await Assert.That(Capture(() => ToHalfSpan(new byte[3]))).IsEqualTo("none");

        var value = (Half) 1.5f;
        var expected = BitConverter.GetBytes(value);

        var small = Filled(1);
        await Assert.That(TryWrite(small, value)).IsFalse();
        await Assert.That(small[0]).IsEqualTo((byte) 0xAA);

        var exact = Filled(2);
        await Assert.That(TryWrite(exact, value)).IsTrue();
        await Assert.That(exact[0]).IsEqualTo(expected[0]);
        await Assert.That(exact[1]).IsEqualTo(expected[1]);

        // a larger destination keeps its trailing bytes
        var larger = Filled(3);
        await Assert.That(TryWrite(larger, value)).IsTrue();
        await Assert.That(larger[2]).IsEqualTo((byte) 0xAA);
    }

    static byte[] Filled(int size)
    {
        var buffer = new byte[size];
        for (var i = 0; i < size; i++)
        {
            buffer[i] = 0xAA;
        }

        return buffer;
    }

    // the spans stay inside these helpers, so nothing ref struct shaped lives across an await
    static Half ToHalfSpan(byte[] buffer) =>
        BitConverter.ToHalf(buffer.AsSpan());

    static bool TryWrite(byte[] buffer, Half value) =>
        BitConverter.TryWriteBytes(buffer, value);

    static string Hex(int bits)
    {
        var bytes = BitConverter.GetBytes(BitConverter.UInt16BitsToHalf((ushort) bits));
        return bytes[0].ToString("x2") + bytes[1].ToString("x2");
    }

    static string Capture(Action action)
    {
        try
        {
            action();
            return "none";
        }
        catch (ArgumentException exception)
        {
            return $"{exception.GetType().Name}:{exception.ParamName}";
        }
    }
}

#endif
