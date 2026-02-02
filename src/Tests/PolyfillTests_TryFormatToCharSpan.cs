partial class PolyfillTests
{
    static void AssertCharSpanFormat(bool result, bool expectedResult, Span<char> buffer, string expectedString, int written, int expectedWritten)
    {
        if (result != expectedResult)
        {
            throw new($"Expected result {expectedResult} but got {result}");
        }

        if (buffer.ToString() != expectedString)
        {
            throw new($"Expected '{expectedString}' but got '{buffer.ToString()}'");
        }

        if (written != expectedWritten)
        {
            throw new($"Expected written {expectedWritten} but got {written}");
        }
    }

    [Test]
    public Task TryFormatSByte()
    {
        sbyte value = 9;
        Span<char> buffer = stackalloc char[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertCharSpanFormat(result, true, buffer, "9", written, 1);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatSByteSmaller()
    {
        sbyte value = 9;
        Span<char> buffer = stackalloc char[2];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertCharSpanFormat(result, true, buffer, "9\0", written, 1);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatSByteLarger()
    {
        sbyte value = 99;
        Span<char> buffer = stackalloc char[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertCharSpanFormat(result, false, buffer, "\0", written, 0);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatByte()
    {
        byte value = 9;
        Span<char> buffer = stackalloc char[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertCharSpanFormat(result, true, buffer, "9", written, 1);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatByteSmaller()
    {
        byte value = 9;
        Span<char> buffer = stackalloc char[2];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertCharSpanFormat(result, true, buffer, "9\0", written, 1);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatByteLarger()
    {
        byte value = 99;
        Span<char> buffer = stackalloc char[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertCharSpanFormat(result, false, buffer, "\0", written, 0);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatInt16()
    {
        short value = 9;
        Span<char> buffer = stackalloc char[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertCharSpanFormat(result, true, buffer, "9", written, 1);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatInt16Smaller()
    {
        short value = 9;
        Span<char> buffer = stackalloc char[2];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertCharSpanFormat(result, true, buffer, "9\0", written, 1);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatInt16Larger()
    {
        short value = 99;
        Span<char> buffer = stackalloc char[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertCharSpanFormat(result, false, buffer, "\0", written, 0);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatUInt16()
    {
        ushort value = 9;
        Span<char> buffer = stackalloc char[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertCharSpanFormat(result, true, buffer, "9", written, 1);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatUInt16Smaller()
    {
        ushort value = 9;
        Span<char> buffer = stackalloc char[2];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertCharSpanFormat(result, true, buffer, "9\0", written, 1);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatUInt16Larger()
    {
        ushort value = 99;
        Span<char> buffer = stackalloc char[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertCharSpanFormat(result, false, buffer, "\0", written, 0);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatInt32()
    {
        var value = 9;
        Span<char> buffer = stackalloc char[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertCharSpanFormat(result, true, buffer, "9", written, 1);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatInt32Smaller()
    {
        var value = 9;
        Span<char> buffer = stackalloc char[2];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertCharSpanFormat(result, true, buffer, "9\0", written, 1);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatInt32Larger()
    {
        var value = 99;
        Span<char> buffer = stackalloc char[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertCharSpanFormat(result, false, buffer, "\0", written, 0);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatUInt32()
    {
        uint value = 9;
        Span<char> buffer = stackalloc char[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertCharSpanFormat(result, true, buffer, "9", written, 1);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatUInt32Smaller()
    {
        uint value = 9;
        Span<char> buffer = stackalloc char[2];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertCharSpanFormat(result, true, buffer, "9\0", written, 1);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatUInt32Larger()
    {
        uint value = 99;
        Span<char> buffer = stackalloc char[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertCharSpanFormat(result, false, buffer, "\0", written, 0);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatInt64()
    {
        long value = 9;
        Span<char> buffer = stackalloc char[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertCharSpanFormat(result, true, buffer, "9", written, 1);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatInt64Smaller()
    {
        long value = 9;
        Span<char> buffer = stackalloc char[2];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertCharSpanFormat(result, true, buffer, "9\0", written, 1);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatInt64Larger()
    {
        long value = 99;
        Span<char> buffer = stackalloc char[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertCharSpanFormat(result, false, buffer, "\0", written, 0);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatUInt64()
    {
        ulong value = 9;
        Span<char> buffer = stackalloc char[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertCharSpanFormat(result, true, buffer, "9", written, 1);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatUInt64Smaller()
    {
        ulong value = 9;
        Span<char> buffer = stackalloc char[2];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertCharSpanFormat(result, true, buffer, "9\0", written, 1);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatUInt64Larger()
    {
        ulong value = 99;
        Span<char> buffer = stackalloc char[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertCharSpanFormat(result, false, buffer, "\0", written, 0);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatSingle()
    {
        float value = 9;
        Span<char> buffer = stackalloc char[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertCharSpanFormat(result, true, buffer, "9", written, 1);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatSingleSmaller()
    {
        float value = 9;
        Span<char> buffer = stackalloc char[2];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertCharSpanFormat(result, true, buffer, "9\0", written, 1);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatSingleLarger()
    {
        float value = 99;
        Span<char> buffer = stackalloc char[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertCharSpanFormat(result, false, buffer, "\0", written, 0);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatGuid()
    {
        var value = new Guid("97008c2d-2114-4396-ae19-392c8e6f8f1b");
        Span<char> buffer = stackalloc char[36];
        var result = value.TryFormat(buffer, out var written);
        AssertCharSpanFormat(result, true, buffer, "97008c2d-2114-4396-ae19-392c8e6f8f1b", written, 36);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatGuidSmaller()
    {
        var value = new Guid("97008c2d-2114-4396-ae19-392c8e6f8f1b");
        Span<char> buffer = stackalloc char[40];
        var result = value.TryFormat(buffer, out var written);
        AssertCharSpanFormat(result, true, buffer, "97008c2d-2114-4396-ae19-392c8e6f8f1b\0\0\0\0", written, 36);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatGuidLarger()
    {
        var value = new Guid("97008c2d-2114-4396-ae19-392c8e6f8f1b");
        Span<char> buffer = stackalloc char[35];
        var result = value.TryFormat(buffer, out var written);
        AssertCharSpanFormat(result, false, buffer, "\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0", written, 0);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatDouble()
    {
        double value = 9;
        Span<char> buffer = stackalloc char[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertCharSpanFormat(result, true, buffer, "9", written, 1);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatDoubleSmaller()
    {
        double value = 9;
        Span<char> buffer = stackalloc char[2];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertCharSpanFormat(result, true, buffer, "9\0", written, 1);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatDoubleLarger()
    {
        double value = 99;
        Span<char> buffer = stackalloc char[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertCharSpanFormat(result, false, buffer, "\0", written, 0);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatDecimal()
    {
        decimal value = 9;
        Span<char> buffer = stackalloc char[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertCharSpanFormat(result, true, buffer, "9", written, 1);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatDecimalSmaller()
    {
        decimal value = 9;
        Span<char> buffer = stackalloc char[2];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertCharSpanFormat(result, true, buffer, "9\0", written, 1);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatDecimalLarger()
    {
        decimal value = 99;
        Span<char> buffer = stackalloc char[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertCharSpanFormat(result, false, buffer, "\0", written, 0);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatDateTimeOffset()
    {
        var value = new DateTimeOffset(new DateTime(2001, 10, 1), TimeSpan.Zero);
        Span<char> buffer = stackalloc char[29];
        var result = value.TryFormat(buffer, out var written, format: "R".AsSpan(), CultureInfo.InvariantCulture);
        AssertCharSpanFormat(result, true, buffer, "Mon, 01 Oct 2001 00:00:00 GMT", written, 29);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatDateTimeOffsetSmaller()
    {
        var value = new DateTimeOffset(new DateTime(2001, 10, 1), TimeSpan.Zero);
        Span<char> buffer = stackalloc char[40];
        var result = value.TryFormat(buffer, out var written, format: "R".AsSpan(), CultureInfo.InvariantCulture);
        AssertCharSpanFormat(result, true, buffer, "Mon, 01 Oct 2001 00:00:00 GMT\0\0\0\0\0\0\0\0\0\0\0", written, 29);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatDateTimeOffsetLarger()
    {
        var value = new DateTimeOffset(new DateTime(2001, 10, 1), TimeSpan.Zero);
        Span<char> buffer = stackalloc char[28];
        var result = value.TryFormat(buffer, out var written, format: "R".AsSpan(), CultureInfo.InvariantCulture);
        AssertCharSpanFormat(result, false, buffer, "\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0", written, 0);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatDateTime()
    {
        var value = new DateTime(2001, 10, 1);
        Span<char> buffer = stackalloc char[29];
        var result = value.TryFormat(buffer, out var written, format: "R".AsSpan(), CultureInfo.InvariantCulture);
        AssertCharSpanFormat(result, true, buffer, "Mon, 01 Oct 2001 00:00:00 GMT", written, 29);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatDateTimeSmaller()
    {
        var value = new DateTime(2001, 10, 1);
        Span<char> buffer = stackalloc char[40];
        var result = value.TryFormat(buffer, out var written, format: "R".AsSpan(), CultureInfo.InvariantCulture);
        AssertCharSpanFormat(result, true, buffer, "Mon, 01 Oct 2001 00:00:00 GMT\0\0\0\0\0\0\0\0\0\0\0", written, 29);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatDateTimeLarger()
    {
        var value = new DateTime(2001, 10, 1);
        Span<char> buffer = stackalloc char[28];
        var result = value.TryFormat(buffer, out var written, format: "R".AsSpan(), CultureInfo.InvariantCulture);
        AssertCharSpanFormat(result, false, buffer, "\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0", written, 0);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatBoolean()
    {
        var value = true;
        Span<char> buffer = stackalloc char[4];
        var result = value.TryFormat(buffer, out var written);
        AssertCharSpanFormat(result, true, buffer, "True", written, 4);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatBooleanSmaller()
    {
        var value = true;
        Span<char> buffer = stackalloc char[10];
        var result = value.TryFormat(buffer, out var written);
        AssertCharSpanFormat(result, true, buffer, "True\0\0\0\0\0\0", written, 4);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatBooleanLarger()
    {
        var value = true;
        Span<char> buffer = stackalloc char[3];
        var result = value.TryFormat(buffer, out var written);
        AssertCharSpanFormat(result, false, buffer, "\0\0\0", written, 0);
        return Task.CompletedTask;
    }

#if NET6_0_OR_GREATER
    [Test]
    public Task TryFormatDate()
    {
        var value = new Date(2001, 10, 1);
        Span<char> buffer = stackalloc char[16];
        var result = value.TryFormat(buffer, out var written, format: "R", CultureInfo.InvariantCulture);
        AssertCharSpanFormat(result, true, buffer, "Mon, 01 Oct 2001", written, 16);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatDateSmaller()
    {
        var value = new Date(2001, 10, 1);
        Span<char> buffer = stackalloc char[20];
        var result = value.TryFormat(buffer, out var written, format: "R", CultureInfo.InvariantCulture);
        AssertCharSpanFormat(result, true, buffer, "Mon, 01 Oct 2001\0\0\0\0", written, 16);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatDateLarger()
    {
        var value = new Date(2001, 10, 1);
        Span<char> buffer = stackalloc char[15];
        var result = value.TryFormat(buffer, out var written, format: "R", CultureInfo.InvariantCulture);
        AssertCharSpanFormat(result, false, buffer, "\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0", written, 0);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatTime()
    {
        var value = new Time(10, 1);
        Span<char> buffer = stackalloc char[8];
        var result = value.TryFormat(buffer, out var written, format: "R", CultureInfo.InvariantCulture);
        AssertCharSpanFormat(result, true, buffer, "10:01:00", written, 8);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatTimeSmaller()
    {
        var value = new Time(10, 1);
        Span<char> buffer = stackalloc char[10];
        var result = value.TryFormat(buffer, out var written, format: "R", CultureInfo.InvariantCulture);
        AssertCharSpanFormat(result, true, buffer, "10:01:00\0\0", written, 8);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatTimeLarger()
    {
        var value = new Time(10, 1);
        Span<char> buffer = stackalloc char[7];
        var result = value.TryFormat(buffer, out var written, format: "R", CultureInfo.InvariantCulture);
        AssertCharSpanFormat(result, false, buffer, "\0\0\0\0\0\0\0", written, 0);
        return Task.CompletedTask;
    }
#endif
}
