#if NET7_0_OR_GREATER

// Runs on net7.0 and later, so net9.0 and above validate the BCL and net7.0/net8.0 the polyfill.
public class BitConverterInt128Tests
{
    static Int128[] signed =
    [
        0,
        1,
        -1,
        2,
        -2,
        ulong.MaxValue,
        (Int128) ulong.MaxValue + 1,
        -(Int128) ulong.MaxValue,
        (Int128) long.MaxValue * 3,
        Int128.MaxValue,
        Int128.MinValue
    ];

    static UInt128[] unsigned =
    [
        0,
        1,
        2,
        ulong.MaxValue,
        (UInt128) ulong.MaxValue + 1,
        UInt128.MaxValue
    ];

    [Test]
    public async Task Int128_RoundTrips()
    {
        foreach (var value in signed)
        {
            var bytes = BitConverter.GetBytes(value);
            await Assert.That(bytes.Length).IsEqualTo(16);
            await Assert.That(BitConverter.ToInt128(bytes, 0)).IsEqualTo(value);
            await Assert.That(FromSpan(bytes)).IsEqualTo(value);
        }
    }

    [Test]
    public async Task UInt128_RoundTrips()
    {
        foreach (var value in unsigned)
        {
            var bytes = BitConverter.GetBytes(value);
            await Assert.That(bytes.Length).IsEqualTo(16);
            await Assert.That(BitConverter.ToUInt128(bytes, 0)).IsEqualTo(value);
            await Assert.That(FromSpanUnsigned(bytes)).IsEqualTo(value);
        }
    }

    // an independent check of the byte order, rather than reading back through the same code
    [Test]
    public async Task ByteLayout()
    {
        if (!BitConverter.IsLittleEndian)
        {
            return;
        }

        await Assert.That(Hex(BitConverter.GetBytes((UInt128) 1))).IsEqualTo("01000000000000000000000000000000");
        await Assert.That(Hex(BitConverter.GetBytes((Int128) (-1)))).IsEqualTo("ffffffffffffffffffffffffffffffff");
        await Assert.That(Hex(BitConverter.GetBytes(UInt128.MaxValue >> 64))).IsEqualTo("ffffffffffffffff0000000000000000");
        await Assert.That(Hex(BitConverter.GetBytes((UInt128) ulong.MaxValue + 1))).IsEqualTo("00000000000000000100000000000000");
        await Assert.That(Hex(BitConverter.GetBytes(Int128.MinValue))).IsEqualTo("00000000000000000000000000000080");
    }

    [Test]
    public async Task ReadsAtOffset()
    {
        var buffer = new byte[20];
        buffer[4] = 0x01;

        await Assert.That(BitConverter.ToUInt128(buffer, 4)).IsEqualTo((UInt128) 1);
        await Assert.That(BitConverter.ToInt128(buffer, 4)).IsEqualTo((Int128) 1);
        await Assert.That(FromSpanUnsigned(buffer, 4)).IsEqualTo((UInt128) 1);
    }

    [Test]
    public async Task TryWriteBytes_Sizing()
    {
        foreach (var value in unsigned)
        {
            var expected = BitConverter.GetBytes(value);

            await Assert.That(TryWrite(15, value).Succeeded).IsFalse();

            var exact = TryWrite(16, value);
            await Assert.That(exact.Succeeded).IsTrue();
            await Assert.That(Hex(exact.Buffer)).IsEqualTo(Hex(expected));

            var larger = TryWrite(17, value);
            await Assert.That(larger.Succeeded).IsTrue();
            await Assert.That(Hex(larger.Buffer[..16])).IsEqualTo(Hex(expected));
        }

        var signedExpected = BitConverter.GetBytes(Int128.MinValue);
        await Assert.That(TryWriteSigned(15, Int128.MinValue).Succeeded).IsFalse();
        var signedExact = TryWriteSigned(16, Int128.MinValue);
        await Assert.That(signedExact.Succeeded).IsTrue();
        await Assert.That(Hex(signedExact.Buffer)).IsEqualTo(Hex(signedExpected));
    }

    [Test]
    public async Task ArrayArgumentValidation()
    {
        var sixteen = new byte[16];
        var twenty = new byte[20];

        await Assert.That(Capture(() => BitConverter.ToInt128(null!, 0))).IsEqualTo("ArgumentNullException:value");
        await Assert.That(Capture(() => BitConverter.ToUInt128(null!, 0))).IsEqualTo("ArgumentNullException:value");

        await Assert.That(Capture(() => BitConverter.ToInt128(sixteen, -1))).IsEqualTo("ArgumentOutOfRangeException:startIndex");
        await Assert.That(Capture(() => BitConverter.ToInt128(sixteen, 16))).IsEqualTo("ArgumentOutOfRangeException:startIndex");
        await Assert.That(Capture(() => BitConverter.ToInt128(sixteen, 17))).IsEqualTo("ArgumentOutOfRangeException:startIndex");
        await Assert.That(Capture(() => BitConverter.ToInt128([], 0))).IsEqualTo("ArgumentOutOfRangeException:startIndex");

        // in range as an index, but fewer than sixteen bytes remain
        await Assert.That(Capture(() => BitConverter.ToInt128(sixteen, 1))).IsEqualTo("ArgumentException:value");
        await Assert.That(Capture(() => BitConverter.ToUInt128(sixteen, 1))).IsEqualTo("ArgumentException:value");
        await Assert.That(Capture(() => BitConverter.ToInt128(twenty, 5))).IsEqualTo("ArgumentException:value");

        // the last position that still leaves sixteen bytes
        await Assert.That(Capture(() => BitConverter.ToInt128(twenty, 4))).IsEqualTo("none");
    }

    [Test]
    public async Task SpanArgumentValidation()
    {
        await Assert.That(Capture(() => FromSpan(new byte[15]))).IsEqualTo("ArgumentOutOfRangeException:value");
        await Assert.That(Capture(() => FromSpanUnsigned(new byte[15]))).IsEqualTo("ArgumentOutOfRangeException:value");
        await Assert.That(Capture(() => FromSpan(new byte[16]))).IsEqualTo("none");
        await Assert.That(Capture(() => FromSpan(new byte[17]))).IsEqualTo("none");
    }

    // the spans stay inside these helpers, so nothing ref struct shaped lives across an await
    static Int128 FromSpan(byte[] buffer) =>
        BitConverter.ToInt128(buffer.AsSpan());

    static UInt128 FromSpanUnsigned(byte[] buffer) =>
        BitConverter.ToUInt128(buffer.AsSpan());

    static UInt128 FromSpanUnsigned(byte[] buffer, int start) =>
        BitConverter.ToUInt128(buffer.AsSpan(start));

    static (bool Succeeded, byte[] Buffer) TryWrite(int size, UInt128 value)
    {
        var buffer = new byte[size];
        return (BitConverter.TryWriteBytes(buffer, value), buffer);
    }

    static (bool Succeeded, byte[] Buffer) TryWriteSigned(int size, Int128 value)
    {
        var buffer = new byte[size];
        return (BitConverter.TryWriteBytes(buffer, value), buffer);
    }

    static string Hex(byte[] bytes)
    {
        var builder = new System.Text.StringBuilder(bytes.Length * 2);
        foreach (var b in bytes)
        {
            builder.Append(b.ToString("x2"));
        }

        return builder.ToString();
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
