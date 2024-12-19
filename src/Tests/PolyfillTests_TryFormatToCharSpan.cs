using System.Globalization;

partial class PolyfillTests
{
    [Test]
    public void TryFormatSByte()
    {
        sbyte value = 9;
        Span<char> buffer = stackalloc char[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("9", buffer.ToString());
        Assert.AreEqual(1, written);
    }

    [Test]
    public void TryFormatSByteSmaller()
    {
        sbyte value = 9;
        Span<char> buffer = stackalloc char[2];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("9\0", buffer.ToString());
        Assert.AreEqual(1, written);
    }

    [Test]
    public void TryFormatSByteLarger()
    {
        sbyte value = 99;
        Span<char> buffer = stackalloc char[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.False(result);
        Assert.AreEqual("\0", buffer.ToString());
        Assert.AreEqual(0, written);
    }

    [Test]
    public void TryFormatByte()
    {
        byte value = 9;
        Span<char> buffer = stackalloc char[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("9", buffer.ToString());
        Assert.AreEqual(1, written);
    }

    [Test]
    public void TryFormatByteSmaller()
    {
        byte value = 9;
        Span<char> buffer = stackalloc char[2];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("9\0", buffer.ToString());
        Assert.AreEqual(1, written);
    }

    [Test]
    public void TryFormatByteLarger()
    {
        byte value = 99;
        Span<char> buffer = stackalloc char[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.False(result);
        Assert.AreEqual("\0", buffer.ToString());
        Assert.AreEqual(0, written);
    }

    [Test]
    public void TryFormatInt16()
    {
        short value = 9;
        Span<char> buffer = stackalloc char[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("9", buffer.ToString());
        Assert.AreEqual(1, written);
    }

    [Test]
    public void TryFormatInt16Smaller()
    {
        short value = 9;
        Span<char> buffer = stackalloc char[2];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("9\0", buffer.ToString());
        Assert.AreEqual(1, written);
    }

    [Test]
    public void TryFormatInt16Larger()
    {
        short value = 99;
        Span<char> buffer = stackalloc char[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.False(result);
        Assert.AreEqual("\0", buffer.ToString());
        Assert.AreEqual(0, written);
    }

    [Test]
    public void TryFormatUInt16()
    {
        ushort value = 9;
        Span<char> buffer = stackalloc char[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("9", buffer.ToString());
        Assert.AreEqual(1, written);
    }

    [Test]
    public void TryFormatUInt16Smaller()
    {
        ushort value = 9;
        Span<char> buffer = stackalloc char[2];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("9\0", buffer.ToString());
        Assert.AreEqual(1, written);
    }

    [Test]
    public void TryFormatUInt16Larger()
    {
        ushort value = 99;
        Span<char> buffer = stackalloc char[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.False(result);
        Assert.AreEqual("\0", buffer.ToString());
        Assert.AreEqual(0, written);
    }

    [Test]
    public void TryFormatInt32()
    {
        var value = 9;
        Span<char> buffer = stackalloc char[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("9", buffer.ToString());
        Assert.AreEqual(1, written);
    }

    [Test]
    public void TryFormatInt32Smaller()
    {
        var value = 9;
        Span<char> buffer = stackalloc char[2];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("9\0", buffer.ToString());
        Assert.AreEqual(1, written);
    }

    [Test]
    public void TryFormatInt32Larger()
    {
        var value = 99;
        Span<char> buffer = stackalloc char[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.False(result);
        Assert.AreEqual("\0", buffer.ToString());
        Assert.AreEqual(0, written);
    }

    [Test]
    public void TryFormatUInt32()
    {
        uint value = 9;
        Span<char> buffer = stackalloc char[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("9", buffer.ToString());
        Assert.AreEqual(1, written);
    }

    [Test]
    public void TryFormatUInt32Smaller()
    {
        uint value = 9;
        Span<char> buffer = stackalloc char[2];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("9\0", buffer.ToString());
        Assert.AreEqual(1, written);
    }

    [Test]
    public void TryFormatUInt32Larger()
    {
        uint value = 99;
        Span<char> buffer = stackalloc char[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.False(result);
        Assert.AreEqual("\0", buffer.ToString());
        Assert.AreEqual(0, written);
    }

    [Test]
    public void TryFormatInt64()
    {
        long value = 9;
        Span<char> buffer = stackalloc char[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("9", buffer.ToString());
        Assert.AreEqual(1, written);
    }

    [Test]
    public void TryFormatInt64Smaller()
    {
        long value = 9;
        Span<char> buffer = stackalloc char[2];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("9\0", buffer.ToString());
        Assert.AreEqual(1, written);
    }

    [Test]
    public void TryFormatInt64Larger()
    {
        long value = 99;
        Span<char> buffer = stackalloc char[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.False(result);
        Assert.AreEqual("\0", buffer.ToString());
        Assert.AreEqual(0, written);
    }

    [Test]
    public void TryFormatUInt64()
    {
        ulong value = 9;
        Span<char> buffer = stackalloc char[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("9", buffer.ToString());
        Assert.AreEqual(buffer.Length, written);
    }

    [Test]
    public void TryFormatUInt64Smaller()
    {
        ulong value = 9;
        Span<char> buffer = stackalloc char[2];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("9\0", buffer.ToString());
        Assert.AreEqual(1, written);
    }

    [Test]
    public void TryFormatUInt64Larger()
    {
        ulong value = 99;
        Span<char> buffer = stackalloc char[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.False(result);
        Assert.AreEqual("\0", buffer.ToString());
        Assert.AreEqual(0, written);
    }

    [Test]
    public void TryFormatSingle()
    {
        float value = 9;
        Span<char> buffer = stackalloc char[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("9", buffer.ToString());
        Assert.AreEqual(1, written);
    }

    [Test]
    public void TryFormatSingleSmaller()
    {
        float value = 9;
        Span<char> buffer = stackalloc char[2];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("9\0", buffer.ToString());
        Assert.AreEqual(1, written);
    }

    [Test]
    public void TryFormatSingleLarger()
    {
        float value = 99;
        Span<char> buffer = stackalloc char[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.False(result);
        Assert.AreEqual("\0", buffer.ToString());
        Assert.AreEqual(0, written);
    }

    [Test]
    public void TryFormatGuid()
    {
        var value = new Guid("97008c2d-2114-4396-ae19-392c8e6f8f1b");
        Span<char> buffer = stackalloc char[36];
        var result = value.TryFormat(buffer, out var written);
        Assert.True(result);
        Assert.AreEqual("97008c2d-2114-4396-ae19-392c8e6f8f1b", buffer.ToString());
        Assert.AreEqual(buffer.Length, written);
    }

    [Test]
    public void TryFormatGuidSmaller()
    {
        var value = new Guid("97008c2d-2114-4396-ae19-392c8e6f8f1b");
        Span<char> buffer = stackalloc char[40];
        var result = value.TryFormat(buffer, out var written);
        Assert.True(result);
        Assert.AreEqual("97008c2d-2114-4396-ae19-392c8e6f8f1b\0\0\0\0", buffer.ToString());
        Assert.AreEqual(36, written);
    }

    [Test]
    public void TryFormatGuidLarger()
    {
        var value = new Guid("97008c2d-2114-4396-ae19-392c8e6f8f1b");
        Span<char> buffer = stackalloc char[35];
        var result = value.TryFormat(buffer, out var written);
        Assert.False(result);
        Assert.AreEqual("\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0", buffer.ToString());
        Assert.AreEqual(0, written);
    }

    [Test]
    public void TryFormatDouble()
    {
        double value = 9;
        Span<char> buffer = stackalloc char[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("9", buffer.ToString());
        Assert.AreEqual(1, written);
    }

    [Test]
    public void TryFormatDoubleSmaller()
    {
        double value = 9;
        Span<char> buffer = stackalloc char[2];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("9\0", buffer.ToString());
        Assert.AreEqual(1, written);
    }

    [Test]
    public void TryFormatDoubleLarger()
    {
        double value = 99;
        Span<char> buffer = stackalloc char[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.False(result);
        Assert.AreEqual("\0", buffer.ToString());
        Assert.AreEqual(0, written);
    }

    [Test]
    public void TryFormatDecimal()
    {
        decimal value = 9;
        Span<char> buffer = stackalloc char[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("9", buffer.ToString());
        Assert.AreEqual(1, written);
    }

    [Test]
    public void TryFormatDecimalSmaller()
    {
        decimal value = 9;
        Span<char> buffer = stackalloc char[2];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("9\0", buffer.ToString());
        Assert.AreEqual(1, written);
    }

    [Test]
    public void TryFormatDecimalLarger()
    {
        decimal value = 99;
        Span<char> buffer = stackalloc char[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        Assert.False(result);
        Assert.AreEqual("\0", buffer.ToString());
        Assert.AreEqual(0, written);
    }

    [Test]
    public void TryFormatDateTimeOffset()
    {
        var value = new DateTimeOffset(new DateTime(2001, 10, 1), TimeSpan.Zero);
        Span<char> buffer = stackalloc char[29];
        var result = value.TryFormat(buffer, out var written, format: "R".AsSpan(), CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("Mon, 01 Oct 2001 00:00:00 GMT", buffer.ToString());
        Assert.AreEqual(29, written);
    }

    [Test]
    public void TryFormatDateTimeOffsetSmaller()
    {
        var value = new DateTimeOffset(new DateTime(2001, 10, 1), TimeSpan.Zero);
        Span<char> buffer = stackalloc char[40];
        var result = value.TryFormat(buffer, out var written, format: "R".AsSpan(), CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("Mon, 01 Oct 2001 00:00:00 GMT\0\0\0\0\0\0\0\0\0\0\0", buffer.ToString());
        Assert.AreEqual(29, written);
    }

    [Test]
    public void TryFormatDateTimeOffsetLarger()
    {
        var value = new DateTimeOffset(new DateTime(2001, 10, 1), TimeSpan.Zero);
        Span<char> buffer = stackalloc char[28];
        var result = value.TryFormat(buffer, out var written, format: "R".AsSpan(), CultureInfo.InvariantCulture);
        Assert.False(result);
        Assert.AreEqual("\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0", buffer.ToString());
        Assert.AreEqual(0, written);
    }

    [Test]
    public void TryFormatDateTime()
    {
        var value = new DateTime(2001, 10, 1);
        Span<char> buffer = stackalloc char[29];
        var result = value.TryFormat(buffer, out var written, format: "R".AsSpan(), CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("Mon, 01 Oct 2001 00:00:00 GMT", buffer.ToString());
        Assert.AreEqual(29, written);
    }

    [Test]
    public void TryFormatDateTimeSmaller()
    {
        var value = new DateTime(2001, 10, 1);
        Span<char> buffer = stackalloc char[40];
        var result = value.TryFormat(buffer, out var written, format: "R".AsSpan(), CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("Mon, 01 Oct 2001 00:00:00 GMT\0\0\0\0\0\0\0\0\0\0\0", buffer.ToString());
        Assert.AreEqual(29, written);
    }

    [Test]
    public void TryFormatDateTimeLarger()
    {
        var value = new DateTime(2001, 10, 1);
        Span<char> buffer = stackalloc char[28];
        var result = value.TryFormat(buffer, out var written, format: "R".AsSpan(), CultureInfo.InvariantCulture);
        Assert.False(result);
        Assert.AreEqual("\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0", buffer.ToString());
        Assert.AreEqual(0, written);
    }

    [Test]
    public void TryFormatBoolean()
    {
        var value = true;
        Span<char> buffer = stackalloc char[4];
        var result = value.TryFormat(buffer, out var written);
        Assert.True(result);
        Assert.AreEqual("True", buffer.ToString());
        Assert.AreEqual(4, written);
    }

    [Test]
    public void TryFormatBooleanSmaller()
    {
        var value = true;
        Span<char> buffer = stackalloc char[10];
        var result = value.TryFormat(buffer, out var written);
        Assert.True(result);
        Assert.AreEqual("True\0\0\0\0\0\0", buffer.ToString());
        Assert.AreEqual(4, written);
    }

    [Test]
    public void TryFormatBooleanLarger()
    {
        var value = true;
        Span<char> buffer = stackalloc char[3];
        var result = value.TryFormat(buffer, out var written);
        Assert.False(result);
        Assert.AreEqual("\0\0\0", buffer.ToString());
        Assert.AreEqual(0, written);
    }

#if NET6_0_OR_GREATER
    [Test]
    public void TryFormatDate()
    {
        var value = new DateOnly(2001, 10, 1);
        Span<char> buffer = stackalloc char[16];
        var result = value.TryFormat(buffer, out var written, format: "R", CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("Mon, 01 Oct 2001", buffer.ToString());
        Assert.AreEqual(16, written);
    }

    [Test]
    public void TryFormatDateSmaller()
    {
        var value = new DateOnly(2001, 10, 1);
        Span<char> buffer = stackalloc char[20];
        var result = value.TryFormat(buffer, out var written, format: "R", CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("Mon, 01 Oct 2001\0\0\0\0", buffer.ToString());
        Assert.AreEqual(16, written);
    }

    [Test]
    public void TryFormatDateLarger()
    {
        var value = new DateOnly(2001, 10, 1);
        Span<char> buffer = stackalloc char[15];
        var result = value.TryFormat(buffer, out var written, format: "R", CultureInfo.InvariantCulture);
        Assert.False(result);
        Assert.AreEqual("\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0", buffer.ToString());
        Assert.AreEqual(0, written);
    }

    [Test]
    public void TryFormatTime()
    {
        var value = new TimeOnly(10, 1);
        Span<char> buffer = stackalloc char[8];
        var result = value.TryFormat(buffer, out var written, format: "R", CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("10:01:00", buffer.ToString());
        Assert.AreEqual(8, written);
    }

    [Test]
    public void TryFormatTimeSmaller()
    {
        var value = new TimeOnly(10, 1);
        Span<char> buffer = stackalloc char[10];
        var result = value.TryFormat(buffer, out var written, format: "R", CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("10:01:00\0\0", buffer.ToString());
        Assert.AreEqual(8, written);
    }

    [Test]
    public void TryFormatTimeLarger()
    {
        var value = new TimeOnly(10, 1);
        Span<char> buffer = stackalloc char[7];
        var result = value.TryFormat(buffer, out var written, format: "R", CultureInfo.InvariantCulture);
        Assert.False(result);
        Assert.AreEqual("\0\0\0\0\0\0\0", buffer.ToString());
        Assert.AreEqual(0, written);
    }
#endif
}