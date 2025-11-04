partial class PolyfillTests
{
    [Test]
    public void TryFormatSByte_ToByteSpan()
    {
        sbyte value = 9;
        Span<byte> buffer = stackalloc byte[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("9", Encoding.UTF8.GetString(buffer));
        Assert.AreEqual(1, written);
    }

    [Test]
    public void TryFormatSByteSmaller_ToByteSpan()
    {
        sbyte value = 9;
        Span<byte> buffer = stackalloc byte[2];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("9\0",  Encoding.UTF8.GetString(buffer));
        Assert.AreEqual(1, written);
    }

    [Test]
    public void TryFormatSByteLarger_ToByteSpan()
    {
        sbyte value = 99;
        Span<byte> buffer = stackalloc byte[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.False(result);
        Assert.AreEqual("\0", Encoding.UTF8.GetString(buffer));
        Assert.AreEqual(0, written);
    }

    [Test]
    public void TryFormatByte_ToByteSpan()
    {
        byte value = 9;
        Span<byte> buffer = stackalloc byte[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("9", Encoding.UTF8.GetString(buffer));
        Assert.AreEqual(1, written);
    }

    [Test]
    public void TryFormatByteSmaller_ToByteSpan()
    {
        byte value = 9;
        Span<byte> buffer = stackalloc byte[2];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("9\0", Encoding.UTF8.GetString(buffer));
        Assert.AreEqual(1, written);
    }

    [Test]
    public void TryFormatByteLarger_ToByteSpan()
    {
        byte value = 99;
        Span<byte> buffer = stackalloc byte[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.False(result);
        Assert.AreEqual("\0", Encoding.UTF8.GetString(buffer));
        Assert.AreEqual(0, written);
    }

    [Test]
    public void TryFormatInt16_ToByteSpan()
    {
        short value = 9;
        Span<byte> buffer = stackalloc byte[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("9", Encoding.UTF8.GetString(buffer));
        Assert.AreEqual(1, written);
    }

    [Test]
    public void TryFormatInt16Smaller_ToByteSpan()
    {
        short value = 9;
        Span<byte> buffer = stackalloc byte[2];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("9\0", Encoding.UTF8.GetString(buffer));
        Assert.AreEqual(1, written);
    }

    [Test]
    public void TryFormatInt16Larger_ToByteSpan()
    {
        short value = 99;
        Span<byte> buffer = stackalloc byte[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.False(result);
        Assert.AreEqual("\0", Encoding.UTF8.GetString(buffer));
        Assert.AreEqual(0, written);
    }

    [Test]
    public void TryFormatUInt16_ToByteSpan()
    {
        ushort value = 9;
        Span<byte> buffer = stackalloc byte[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("9", Encoding.UTF8.GetString(buffer));
        Assert.AreEqual(1, written);
    }

    [Test]
    public void TryFormatUInt16Smaller_ToByteSpan()
    {
        ushort value = 9;
        Span<byte> buffer = stackalloc byte[2];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("9\0", Encoding.UTF8.GetString(buffer));
        Assert.AreEqual(1, written);
    }

    [Test]
    public void TryFormatUInt16Larger_ToByteSpan()
    {
        ushort value = 99;
        Span<byte> buffer = stackalloc byte[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.False(result);
        Assert.AreEqual("\0", Encoding.UTF8.GetString(buffer));
        Assert.AreEqual(0, written);
    }

    [Test]
    public void TryFormatInt32_ToByteSpan()
    {
        var value = 9;
        Span<byte> buffer = stackalloc byte[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("9", Encoding.UTF8.GetString(buffer));
        Assert.AreEqual(1, written);
    }

    [Test]
    public void TryFormatInt32Smaller_ToByteSpan()
    {
        var value = 9;
        Span<byte> buffer = stackalloc byte[2];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("9\0", Encoding.UTF8.GetString(buffer));
        Assert.AreEqual(1, written);
    }

    [Test]
    public void TryFormatInt32Larger_ToByteSpan()
    {
        var value = 99;
        Span<byte> buffer = stackalloc byte[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.False(result);
        Assert.AreEqual("\0", Encoding.UTF8.GetString(buffer));
        Assert.AreEqual(0, written);
    }

    [Test]
    public void TryFormatUInt32_ToByteSpan()
    {
        uint value = 9;
        Span<byte> buffer = stackalloc byte[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("9", Encoding.UTF8.GetString(buffer));
        Assert.AreEqual(1, written);
    }

    [Test]
    public void TryFormatUInt32Smaller_ToByteSpan()
    {
        uint value = 9;
        Span<byte> buffer = stackalloc byte[2];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("9\0", Encoding.UTF8.GetString(buffer));
        Assert.AreEqual(1, written);
    }

    [Test]
    public void TryFormatUInt32Larger_ToByteSpan()
    {
        uint value = 99;
        Span<byte> buffer = stackalloc byte[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.False(result);
        Assert.AreEqual("\0", Encoding.UTF8.GetString(buffer));
        Assert.AreEqual(0, written);
    }

    [Test]
    public void TryFormatInt64_ToByteSpan()
    {
        long value = 9;
        Span<byte> buffer = stackalloc byte[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("9", Encoding.UTF8.GetString(buffer));
        Assert.AreEqual(1, written);
    }

    [Test]
    public void TryFormatInt64Smaller_ToByteSpan()
    {
        long value = 9;
        Span<byte> buffer = stackalloc byte[2];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("9\0", Encoding.UTF8.GetString(buffer));
        Assert.AreEqual(1, written);
    }

    [Test]
    public void TryFormatInt64Larger_ToByteSpan()
    {
        long value = 99;
        Span<byte> buffer = stackalloc byte[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.False(result);
        Assert.AreEqual("\0", Encoding.UTF8.GetString(buffer));
        Assert.AreEqual(0, written);
    }

    [Test]
    public void TryFormatUInt64_ToByteSpan()
    {
        ulong value = 9;
        Span<byte> buffer = stackalloc byte[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("9", Encoding.UTF8.GetString(buffer));
        Assert.AreEqual(buffer.Length, written);
    }

    [Test]
    public void TryFormatUInt64Smaller_ToByteSpan()
    {
        ulong value = 9;
        Span<byte> buffer = stackalloc byte[2];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("9\0", Encoding.UTF8.GetString(buffer));
        Assert.AreEqual(1, written);
    }

    [Test]
    public void TryFormatUInt64Larger_ToByteSpan()
    {
        ulong value = 99;
        Span<byte> buffer = stackalloc byte[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.False(result);
        Assert.AreEqual("\0", Encoding.UTF8.GetString(buffer));
        Assert.AreEqual(0, written);
    }

    [Test]
    public void TryFormatSingle_ToByteSpan()
    {
        float value = 9;
        Span<byte> buffer = stackalloc byte[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("9", Encoding.UTF8.GetString(buffer));
        Assert.AreEqual(1, written);
    }

    [Test]
    public void TryFormatSingleSmaller_ToByteSpan()
    {
        float value = 9;
        Span<byte> buffer = stackalloc byte[2];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("9\0", Encoding.UTF8.GetString(buffer));
        Assert.AreEqual(1, written);
    }

    [Test]
    public void TryFormatSingleLarger_ToByteSpan()
    {
        float value = 99;
        Span<byte> buffer = stackalloc byte[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.False(result);
        Assert.AreEqual("\0", Encoding.UTF8.GetString(buffer));
        Assert.AreEqual(0, written);
    }

    [Test]
    public void TryFormatGuid_ToByteSpan()
    {
        var value = new Guid("97008c2d-2114-4396-ae19-392c8e6f8f1b");
        Span<byte> buffer = stackalloc byte[36];
        var result = value.TryFormat(buffer, out var written);
        Assert.True(result);
        Assert.AreEqual("97008c2d-2114-4396-ae19-392c8e6f8f1b", Encoding.UTF8.GetString(buffer));
        Assert.AreEqual(buffer.Length, written);
    }

    [Test]
    public void TryFormatGuidSmaller_ToByteSpan()
    {
        var value = new Guid("97008c2d-2114-4396-ae19-392c8e6f8f1b");
        Span<byte> buffer = stackalloc byte[40];
        var result = value.TryFormat(buffer, out var written);
        Assert.True(result);
        Assert.AreEqual("97008c2d-2114-4396-ae19-392c8e6f8f1b\0\0\0\0", Encoding.UTF8.GetString(buffer));
        Assert.AreEqual(36, written);
    }

    [Test]
    public void TryFormatGuidLarger_ToByteSpan()
    {
        var value = new Guid("97008c2d-2114-4396-ae19-392c8e6f8f1b");
        Span<byte> buffer = stackalloc byte[35];
        var result = value.TryFormat(buffer, out var written);
        Assert.False(result);
        Assert.AreEqual("\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0", Encoding.UTF8.GetString(buffer));
        Assert.AreEqual(0, written);
    }

    [Test]
    public void TryFormatDouble_ToByteSpan()
    {
        double value = 9;
        Span<byte> buffer = stackalloc byte[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("9", Encoding.UTF8.GetString(buffer));
        Assert.AreEqual(1, written);
    }

    [Test]
    public void TryFormatDoubleSmaller_ToByteSpan()
    {
        double value = 9;
        Span<byte> buffer = stackalloc byte[2];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("9\0", Encoding.UTF8.GetString(buffer));
        Assert.AreEqual(1, written);
    }

    [Test]
    public void TryFormatDoubleLarger_ToByteSpan()
    {
        double value = 99;
        Span<byte> buffer = stackalloc byte[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.False(result);
        Assert.AreEqual("\0", Encoding.UTF8.GetString(buffer));
        Assert.AreEqual(0, written);
    }

    [Test]
    public void TryFormatDecimal_ToByteSpan()
    {
        decimal value = 9;
        Span<byte> buffer = stackalloc byte[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("9", Encoding.UTF8.GetString(buffer));
        Assert.AreEqual(1, written);
    }

    [Test]
    public void TryFormatDecimalSmaller_ToByteSpan()
    {
        decimal value = 9;
        Span<byte> buffer = stackalloc byte[2];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("9\0", Encoding.UTF8.GetString(buffer));
        Assert.AreEqual(1, written);
    }

    [Test]
    public void TryFormatDecimalLarger_ToByteSpan()
    {
        decimal value = 99;
        Span<byte> buffer = stackalloc byte[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.False(result);
        Assert.AreEqual("\0", Encoding.UTF8.GetString(buffer));
        Assert.AreEqual(0, written);
    }

    [Test]
    public void TryFormatDateTimeOffset_ToByteSpan()
    {
        var value = new DateTimeOffset(new DateTime(2001, 10, 1), TimeSpan.Zero);
        Span<byte> buffer = stackalloc byte[29];
        var result = value.TryFormat(buffer, out var written, format: "R".AsSpan(), CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("Mon, 01 Oct 2001 00:00:00 GMT", Encoding.UTF8.GetString(buffer));
        Assert.AreEqual(29, written);
    }

    [Test]
    public void TryFormatDateTimeOffsetSmaller_ToByteSpan()
    {
        var value = new DateTimeOffset(new DateTime(2001, 10, 1), TimeSpan.Zero);
        Span<byte> buffer = stackalloc byte[40];
        var result = value.TryFormat(buffer, out var written, format: "R".AsSpan(), CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("Mon, 01 Oct 2001 00:00:00 GMT\0\0\0\0\0\0\0\0\0\0\0", Encoding.UTF8.GetString(buffer));
        Assert.AreEqual(29, written);
    }

    [Test]
    public void TryFormatDateTimeOffsetLarger_ToByteSpan()
    {
        var value = new DateTimeOffset(new DateTime(2001, 10, 1), TimeSpan.Zero);
        Span<byte> buffer = stackalloc byte[28];
        var result = value.TryFormat(buffer, out var written, format: "R".AsSpan(), CultureInfo.InvariantCulture);
        Assert.False(result);
        Assert.AreEqual("\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0", Encoding.UTF8.GetString(buffer));
        Assert.AreEqual(0, written);
    }

    [Test]
    public void TryFormatDateTime_ToByteSpan()
    {
        var value = new DateTime(2001, 10, 1);
        Span<byte> buffer = stackalloc byte[29];
        var result = value.TryFormat(buffer, out var written, format: "R".AsSpan(), CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("Mon, 01 Oct 2001 00:00:00 GMT", Encoding.UTF8.GetString(buffer));
        Assert.AreEqual(29, written);
    }

    [Test]
    public void TryFormatDateTimeSmaller_ToByteSpan()
    {
        var value = new DateTime(2001, 10, 1);
        Span<byte> buffer = stackalloc byte[40];
        var result = value.TryFormat(buffer, out var written, format: "R".AsSpan(), CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("Mon, 01 Oct 2001 00:00:00 GMT\0\0\0\0\0\0\0\0\0\0\0", Encoding.UTF8.GetString(buffer));
        Assert.AreEqual(29, written);
    }

    [Test]
    public void TryFormatDateTimeLarger_ToByteSpan()
    {
        var value = new DateTime(2001, 10, 1);
        Span<byte> buffer = stackalloc byte[28];
        var result = value.TryFormat(buffer, out var written, format: "R".AsSpan(), CultureInfo.InvariantCulture);
        Assert.False(result);
        Assert.AreEqual("\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0", Encoding.UTF8.GetString(buffer));
        Assert.AreEqual(0, written);
    }

#if NET6_0_OR_GREATER
    [Test]
    public void TryFormatDate_ToByteSpan()
    {
        var value = new Date(2001, 10, 1);
        Span<byte> buffer = stackalloc byte[16];
        var result = value.TryFormat(buffer, out var written, format: "R", CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("Mon, 01 Oct 2001", Encoding.UTF8.GetString(buffer));
        Assert.AreEqual(16, written);
    }

    [Test]
    public void TryFormatDateSmaller_ToByteSpan()
    {
        var value = new Date(2001, 10, 1);
        Span<byte> buffer = stackalloc byte[20];
        var result = value.TryFormat(buffer, out var written, format: "R", CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("Mon, 01 Oct 2001\0\0\0\0", Encoding.UTF8.GetString(buffer));
        Assert.AreEqual(16, written);
    }

    [Test]
    public void TryFormatDateLarger_ToByteSpan()
    {
        var value = new DateOnly(2001, 10, 1);
        Span<byte> buffer = stackalloc byte[15];
        var result = value.TryFormat(buffer, out var written, format: "R", CultureInfo.InvariantCulture);
        Assert.False(result);
        Assert.AreEqual("\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0", Encoding.UTF8.GetString(buffer));
        Assert.AreEqual(0, written);
    }

    [Test]
    public void TryFormatTime_ToByteSpan()
    {
        var value = new Time(10, 1);
        Span<byte> buffer = stackalloc byte[8];
        var result = value.TryFormat(buffer, out var written, format: "R", CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("10:01:00", Encoding.UTF8.GetString(buffer));
        Assert.AreEqual(8, written);
    }

    [Test]
    public void TryFormatTimeSmaller_ToByteSpan()
    {
        var value = new Time(10, 1);
        Span<byte> buffer = stackalloc byte[10];
        var result = value.TryFormat(buffer, out var written, format: "R", CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("10:01:00\0\0", Encoding.UTF8.GetString(buffer));
        Assert.AreEqual(8, written);
    }

    [Test]
    public void TryFormatTimeLarger_ToByteSpan()
    {
        var value = new Time(10, 1);
        Span<byte> buffer = stackalloc byte[7];
        var result = value.TryFormat(buffer, out var written, format: "R", CultureInfo.InvariantCulture);
        Assert.False(result);
        Assert.AreEqual("\0\0\0\0\0\0\0", Encoding.UTF8.GetString(buffer));
        Assert.AreEqual(0, written);
    }
#endif
}