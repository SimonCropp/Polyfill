partial class PolyfillTests
{
    static void AssertByteSpanFormat(bool result, bool expectedResult, Span<byte> buffer, string expectedString, int written, int expectedWritten)
    {
        if (result != expectedResult)
        {
            throw new($"Expected result {expectedResult} but got {result}");
        }

        if (Encoding.UTF8.GetString(buffer) != expectedString)
        {
            throw new($"Expected '{expectedString}' but got '{Encoding.UTF8.GetString(buffer)}'");
        }

        if (written != expectedWritten)
        {
            throw new($"Expected written {expectedWritten} but got {written}");
        }
    }

    [Test]
    public Task TryFormatSByte_ToByteSpan()
    {
        sbyte value = 9;
        Span<byte> buffer = stackalloc byte[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertByteSpanFormat(result, true, buffer, "9", written, 1);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatSByteSmaller_ToByteSpan()
    {
        sbyte value = 9;
        Span<byte> buffer = stackalloc byte[2];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertByteSpanFormat(result, true, buffer, "9\0", written, 1);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatSByteLarger_ToByteSpan()
    {
        sbyte value = 99;
        Span<byte> buffer = stackalloc byte[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertByteSpanFormat(result, false, buffer, "\0", written, 0);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatByte_ToByteSpan()
    {
        byte value = 9;
        Span<byte> buffer = stackalloc byte[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertByteSpanFormat(result, true, buffer, "9", written, 1);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatByteSmaller_ToByteSpan()
    {
        byte value = 9;
        Span<byte> buffer = stackalloc byte[2];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertByteSpanFormat(result, true, buffer, "9\0", written, 1);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatByteLarger_ToByteSpan()
    {
        byte value = 99;
        Span<byte> buffer = stackalloc byte[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertByteSpanFormat(result, false, buffer, "\0", written, 0);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatInt16_ToByteSpan()
    {
        short value = 9;
        Span<byte> buffer = stackalloc byte[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertByteSpanFormat(result, true, buffer, "9", written, 1);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatInt16Smaller_ToByteSpan()
    {
        short value = 9;
        Span<byte> buffer = stackalloc byte[2];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertByteSpanFormat(result, true, buffer, "9\0", written, 1);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatInt16Larger_ToByteSpan()
    {
        short value = 99;
        Span<byte> buffer = stackalloc byte[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertByteSpanFormat(result, false, buffer, "\0", written, 0);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatUInt16_ToByteSpan()
    {
        ushort value = 9;
        Span<byte> buffer = stackalloc byte[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertByteSpanFormat(result, true, buffer, "9", written, 1);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatUInt16Smaller_ToByteSpan()
    {
        ushort value = 9;
        Span<byte> buffer = stackalloc byte[2];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertByteSpanFormat(result, true, buffer, "9\0", written, 1);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatUInt16Larger_ToByteSpan()
    {
        ushort value = 99;
        Span<byte> buffer = stackalloc byte[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertByteSpanFormat(result, false, buffer, "\0", written, 0);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatInt32_ToByteSpan()
    {
        var value = 9;
        Span<byte> buffer = stackalloc byte[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertByteSpanFormat(result, true, buffer, "9", written, 1);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatInt32Smaller_ToByteSpan()
    {
        var value = 9;
        Span<byte> buffer = stackalloc byte[2];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertByteSpanFormat(result, true, buffer, "9\0", written, 1);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatInt32Larger_ToByteSpan()
    {
        var value = 99;
        Span<byte> buffer = stackalloc byte[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertByteSpanFormat(result, false, buffer, "\0", written, 0);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatUInt32_ToByteSpan()
    {
        uint value = 9;
        Span<byte> buffer = stackalloc byte[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertByteSpanFormat(result, true, buffer, "9", written, 1);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatUInt32Smaller_ToByteSpan()
    {
        uint value = 9;
        Span<byte> buffer = stackalloc byte[2];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertByteSpanFormat(result, true, buffer, "9\0", written, 1);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatUInt32Larger_ToByteSpan()
    {
        uint value = 99;
        Span<byte> buffer = stackalloc byte[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertByteSpanFormat(result, false, buffer, "\0", written, 0);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatInt64_ToByteSpan()
    {
        long value = 9;
        Span<byte> buffer = stackalloc byte[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertByteSpanFormat(result, true, buffer, "9", written, 1);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatInt64Smaller_ToByteSpan()
    {
        long value = 9;
        Span<byte> buffer = stackalloc byte[2];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertByteSpanFormat(result, true, buffer, "9\0", written, 1);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatInt64Larger_ToByteSpan()
    {
        long value = 99;
        Span<byte> buffer = stackalloc byte[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertByteSpanFormat(result, false, buffer, "\0", written, 0);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatUInt64_ToByteSpan()
    {
        ulong value = 9;
        Span<byte> buffer = stackalloc byte[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertByteSpanFormat(result, true, buffer, "9", written, 1);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatUInt64Smaller_ToByteSpan()
    {
        ulong value = 9;
        Span<byte> buffer = stackalloc byte[2];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertByteSpanFormat(result, true, buffer, "9\0", written, 1);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatUInt64Larger_ToByteSpan()
    {
        ulong value = 99;
        Span<byte> buffer = stackalloc byte[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertByteSpanFormat(result, false, buffer, "\0", written, 0);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatSingle_ToByteSpan()
    {
        float value = 9;
        Span<byte> buffer = stackalloc byte[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertByteSpanFormat(result, true, buffer, "9", written, 1);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatSingleSmaller_ToByteSpan()
    {
        float value = 9;
        Span<byte> buffer = stackalloc byte[2];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertByteSpanFormat(result, true, buffer, "9\0", written, 1);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatSingleLarger_ToByteSpan()
    {
        float value = 99;
        Span<byte> buffer = stackalloc byte[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertByteSpanFormat(result, false, buffer, "\0", written, 0);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatGuid_ToByteSpan()
    {
        var value = new Guid("97008c2d-2114-4396-ae19-392c8e6f8f1b");
        Span<byte> buffer = stackalloc byte[36];
        var result = value.TryFormat(buffer, out var written);
        AssertByteSpanFormat(result, true, buffer, "97008c2d-2114-4396-ae19-392c8e6f8f1b", written, 36);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatGuidSmaller_ToByteSpan()
    {
        var value = new Guid("97008c2d-2114-4396-ae19-392c8e6f8f1b");
        Span<byte> buffer = stackalloc byte[40];
        var result = value.TryFormat(buffer, out var written);
        AssertByteSpanFormat(result, true, buffer, "97008c2d-2114-4396-ae19-392c8e6f8f1b\0\0\0\0", written, 36);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatGuidLarger_ToByteSpan()
    {
        var value = new Guid("97008c2d-2114-4396-ae19-392c8e6f8f1b");
        Span<byte> buffer = stackalloc byte[35];
        var result = value.TryFormat(buffer, out var written);
        AssertByteSpanFormat(result, false, buffer, "\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0", written, 0);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatDouble_ToByteSpan()
    {
        double value = 9;
        Span<byte> buffer = stackalloc byte[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertByteSpanFormat(result, true, buffer, "9", written, 1);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatDoubleSmaller_ToByteSpan()
    {
        double value = 9;
        Span<byte> buffer = stackalloc byte[2];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertByteSpanFormat(result, true, buffer, "9\0", written, 1);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatDoubleLarger_ToByteSpan()
    {
        double value = 99;
        Span<byte> buffer = stackalloc byte[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertByteSpanFormat(result, false, buffer, "\0", written, 0);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatDecimal_ToByteSpan()
    {
        decimal value = 9;
        Span<byte> buffer = stackalloc byte[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertByteSpanFormat(result, true, buffer, "9", written, 1);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatDecimalSmaller_ToByteSpan()
    {
        decimal value = 9;
        Span<byte> buffer = stackalloc byte[2];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertByteSpanFormat(result, true, buffer, "9\0", written, 1);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatDecimalLarger_ToByteSpan()
    {
        decimal value = 99;
        Span<byte> buffer = stackalloc byte[1];
        var result = value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture);
        AssertByteSpanFormat(result, false, buffer, "\0", written, 0);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatDateTimeOffset_ToByteSpan()
    {
        var value = new DateTimeOffset(new DateTime(2001, 10, 1), TimeSpan.Zero);
        Span<byte> buffer = stackalloc byte[29];
        var result = value.TryFormat(buffer, out var written, format: "R".AsSpan(), CultureInfo.InvariantCulture);
        AssertByteSpanFormat(result, true, buffer, "Mon, 01 Oct 2001 00:00:00 GMT", written, 29);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatDateTimeOffsetSmaller_ToByteSpan()
    {
        var value = new DateTimeOffset(new DateTime(2001, 10, 1), TimeSpan.Zero);
        Span<byte> buffer = stackalloc byte[40];
        var result = value.TryFormat(buffer, out var written, format: "R".AsSpan(), CultureInfo.InvariantCulture);
        AssertByteSpanFormat(result, true, buffer, "Mon, 01 Oct 2001 00:00:00 GMT\0\0\0\0\0\0\0\0\0\0\0", written, 29);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatDateTimeOffsetLarger_ToByteSpan()
    {
        var value = new DateTimeOffset(new DateTime(2001, 10, 1), TimeSpan.Zero);
        Span<byte> buffer = stackalloc byte[28];
        var result = value.TryFormat(buffer, out var written, format: "R".AsSpan(), CultureInfo.InvariantCulture);
        AssertByteSpanFormat(result, false, buffer, "\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0", written, 0);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatDateTime_ToByteSpan()
    {
        var value = new DateTime(2001, 10, 1);
        Span<byte> buffer = stackalloc byte[29];
        var result = value.TryFormat(buffer, out var written, format: "R".AsSpan(), CultureInfo.InvariantCulture);
        AssertByteSpanFormat(result, true, buffer, "Mon, 01 Oct 2001 00:00:00 GMT", written, 29);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatDateTimeSmaller_ToByteSpan()
    {
        var value = new DateTime(2001, 10, 1);
        Span<byte> buffer = stackalloc byte[40];
        var result = value.TryFormat(buffer, out var written, format: "R".AsSpan(), CultureInfo.InvariantCulture);
        AssertByteSpanFormat(result, true, buffer, "Mon, 01 Oct 2001 00:00:00 GMT\0\0\0\0\0\0\0\0\0\0\0", written, 29);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatDateTimeLarger_ToByteSpan()
    {
        var value = new DateTime(2001, 10, 1);
        Span<byte> buffer = stackalloc byte[28];
        var result = value.TryFormat(buffer, out var written, format: "R".AsSpan(), CultureInfo.InvariantCulture);
        AssertByteSpanFormat(result, false, buffer, "\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0", written, 0);
        return Task.CompletedTask;
    }

#if NET6_0_OR_GREATER
    [Test]
    public Task TryFormatDate_ToByteSpan()
    {
        var value = new Date(2001, 10, 1);
        Span<byte> buffer = stackalloc byte[16];
        var result = value.TryFormat(buffer, out var written, format: "R", CultureInfo.InvariantCulture);
        AssertByteSpanFormat(result, true, buffer, "Mon, 01 Oct 2001", written, 16);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatDateSmaller_ToByteSpan()
    {
        var value = new Date(2001, 10, 1);
        Span<byte> buffer = stackalloc byte[20];
        var result = value.TryFormat(buffer, out var written, format: "R", CultureInfo.InvariantCulture);
        AssertByteSpanFormat(result, true, buffer, "Mon, 01 Oct 2001\0\0\0\0", written, 16);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatDateLarger_ToByteSpan()
    {
        var value = new Date(2001, 10, 1);
        Span<byte> buffer = stackalloc byte[15];
        var result = value.TryFormat(buffer, out var written, format: "R", CultureInfo.InvariantCulture);
        AssertByteSpanFormat(result, false, buffer, "\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0", written, 0);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatTime_ToByteSpan()
    {
        var value = new Time(10, 1);
        Span<byte> buffer = stackalloc byte[8];
        var result = value.TryFormat(buffer, out var written, format: "R", CultureInfo.InvariantCulture);
        AssertByteSpanFormat(result, true, buffer, "10:01:00", written, 8);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatTimeSmaller_ToByteSpan()
    {
        var value = new Time(10, 1);
        Span<byte> buffer = stackalloc byte[10];
        var result = value.TryFormat(buffer, out var written, format: "R", CultureInfo.InvariantCulture);
        AssertByteSpanFormat(result, true, buffer, "10:01:00\0\0", written, 8);
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFormatTimeLarger_ToByteSpan()
    {
        var value = new Time(10, 1);
        Span<byte> buffer = stackalloc byte[7];
        var result = value.TryFormat(buffer, out var written, format: "R", CultureInfo.InvariantCulture);
        AssertByteSpanFormat(result, false, buffer, "\0\0\0\0\0\0\0", written, 0);
        return Task.CompletedTask;
    }
#endif
}
