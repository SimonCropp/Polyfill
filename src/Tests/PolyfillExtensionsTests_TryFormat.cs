using System.Globalization;

partial class PolyfillExtensionsTests
{
#if NET8_0

    [Test]
    public void TryFormatSByte()
    {
        sbyte value = 9;
        Span<char> buffer = stackalloc char[1];
        var result = value.TryFormat(buffer, out var written, format: "R", CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("9", buffer.ToString());
        Assert.AreEqual(buffer.Length, written);
    }

    [Test]
    public void TryFormatByte()
    {
        byte value = 9;
        Span<char> buffer = stackalloc char[1];
        var result = value.TryFormat(buffer, out var written, format: "R", CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("9", buffer.ToString());
        Assert.AreEqual(buffer.Length, written);
    }

    [Test]
    public void TryFormatInt16()
    {
        short value = 9;
        Span<char> buffer = stackalloc char[1];
        var result = value.TryFormat(buffer, out var written, format: "R", CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("9", buffer.ToString());
        Assert.AreEqual(buffer.Length, written);
    }

    [Test]
    public void TryFormatUInt16()
    {
        ushort value = 9;
        Span<char> buffer = stackalloc char[1];
        var result = value.TryFormat(buffer, out var written, format: "R", CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("9", buffer.ToString());
        Assert.AreEqual(buffer.Length, written);
    }

    [Test]
    public void TryFormatInt32()
    {
        int value = 9;
        Span<char> buffer = stackalloc char[1];
        var result = value.TryFormat(buffer, out var written, format: "R", CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("9", buffer.ToString());
        Assert.AreEqual(buffer.Length, written);
    }

    [Test]
    public void TryFormatUInt32()
    {
        uint value = 9;
        Span<char> buffer = stackalloc char[1];
        var result = value.TryFormat(buffer, out var written, format: "R", CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("9", buffer.ToString());
        Assert.AreEqual(buffer.Length, written);
    }

    [Test]
    public void TryFormatInt64()
    {
        long value = 9;
        Span<char> buffer = stackalloc char[1];
        var result = value.TryFormat(buffer, out var written, format: "R", CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("9", buffer.ToString());
        Assert.AreEqual(buffer.Length, written);
    }

    [Test]
    public void TryFormatUInt64()
    {
        ulong value = 9;
        Span<char> buffer = stackalloc char[1];
        var result = value.TryFormat(buffer, out var written, format: "R", CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("9", buffer.ToString());
        Assert.AreEqual(buffer.Length, written);
    }

    [Test]
    public void TryFormatIntPtr()
    {
        nint value = 9;
        Span<char> buffer = stackalloc char[1];
        var result = value.TryFormat(buffer, out var written, format: "R", CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("9", buffer.ToString());
        Assert.AreEqual(buffer.Length, written);
    }

    [Test]
    public void TryFormatUIntPtr()
    {
        nuint value = 9;
        Span<char> buffer = stackalloc char[1];
        var result = value.TryFormat(buffer, out var written, format: "R", CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("9", buffer.ToString());
        Assert.AreEqual(buffer.Length, written);
    }

    [Test]
    public void TryFormatSingle()
    {
        float value = 9;
        Span<char> buffer = stackalloc char[1];
        var result = value.TryFormat(buffer, out var written, format: "R", CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("9", buffer.ToString());
        Assert.AreEqual(buffer.Length, written);
    }

    [Test]
    public void TryFormatDouble()
    {
        double value = 9;
        Span<char> buffer = stackalloc char[1];
        var result = value.TryFormat(buffer, out var written, format: "R", CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("9", buffer.ToString());
        Assert.AreEqual(buffer.Length, written);
    }

    [Test]
    public void TryFormatDecimal()
    {
        decimal value = 9;
        Span<char> buffer = stackalloc char[1];
        var result = value.TryFormat(buffer, out var written, format: "R", CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("9", buffer.ToString());
        Assert.AreEqual(buffer.Length, written);
    }

    [Test]
    public void TryFormatDateTimeOffset()
    {
        var value = new DateTimeOffset(new DateTime(2001,10,1),TimeSpan.Zero);
        Span<char> buffer = stackalloc char[29];
        var result = value.TryFormat(buffer, out var written, format: "R", CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("Mon, 01 Oct 2001 00:00:00 GMT", buffer.ToString());
        Assert.AreEqual(buffer.Length, written);
    }

    [Test]
    public void TryFormatDateTime()
    {
        var value = new DateTime(2001,10,1);
        Span<char> buffer = stackalloc char[29];
        var result = value.TryFormat(buffer, out var written, format: "R", CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("Mon, 01 Oct 2001 00:00:00 GMT", buffer.ToString());
        Assert.AreEqual(buffer.Length, written);
    }

    [Test]
    public void TryFormatDate()
    {
        var value = new DateOnly(2001,10,1);
        Span<char> buffer = stackalloc char[16];
        var result = value.TryFormat(buffer, out var written, format: "R", CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("Mon, 01 Oct 2001", buffer.ToString());
        Assert.AreEqual(buffer.Length, written);
    }

    [Test]
    public void TryFormatTime()
    {
        var value = new TimeOnly(10,1);
        Span<char> buffer = stackalloc char[8];
        var result = value.TryFormat(buffer, out var written, format: "R", CultureInfo.InvariantCulture);
        Assert.True(result);
        Assert.AreEqual("10:01:00", buffer.ToString());
        Assert.AreEqual(buffer.Length, written);
    }

    [Test]
    public void TryFormatBoolean()
    {
        var value = true;
        Span<char> buffer = stackalloc char[4];
        var result = value.TryFormat(buffer, out var written);
        Assert.True(result);
        Assert.AreEqual("True", buffer.ToString());
        Assert.AreEqual(buffer.Length, written);
    }

    // [Test]
    // public void TryFormatChar ()
    // {
    //     char  value = 'a';
    //     Span<char> buffer = stackalloc char[1];
    //     var result = value.TryFormat(buffer, out var written, format: "R", CultureInfo.InvariantCulture);
    //     Assert.True(result);
    //     Assert.AreEqual("9", buffer.ToString());
    //     Assert.AreEqual(1, written);
    // }
#endif
}