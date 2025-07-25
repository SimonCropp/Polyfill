partial class PolyfillTests
{
    [Test]
    public void EnumGetValues()
    {
        var dayOfWeeks = Enum.GetValues<DayOfWeek>();
        Assert.AreEqual(DayOfWeek.Sunday, dayOfWeeks[0]);
    }

    [Test]
    public void EnumGetNames()
    {
        var dayOfWeeks = Enum.GetNames<DayOfWeek>();
        Assert.AreEqual("Sunday", dayOfWeeks[0]);
    }

    [Test]
    public void Parse()
    {
        var dayOfWeek = Enum.Parse<DayOfWeek>("Sunday");
        Assert.AreEqual(DayOfWeek.Sunday, dayOfWeek);

        Assert.Throws<ArgumentException>(() => Enum.Parse<DayOfWeek>("a"));

        dayOfWeek = Enum.Parse<DayOfWeek>("sunday", true);
        Assert.AreEqual(DayOfWeek.Sunday, dayOfWeek);

        Assert.Throws<ArgumentException>(() => Enum.Parse<DayOfWeek>("a", true));

#if FeatureMemory

        dayOfWeek = Enum.Parse<DayOfWeek>("Sunday".AsSpan());
        Assert.AreEqual(DayOfWeek.Sunday, dayOfWeek);

        Assert.Throws<ArgumentException>(() => Enum.Parse<DayOfWeek>("a".AsSpan()));

        dayOfWeek = Enum.Parse<DayOfWeek>("sunday".AsSpan(), true);
        Assert.AreEqual(DayOfWeek.Sunday, dayOfWeek);

        Assert.Throws<ArgumentException>(() => Enum.Parse<DayOfWeek>("a".AsSpan(), true));

#endif
    }

#if FeatureMemory

    [Test]
    public void TryParse()
    {
        var result = Enum.TryParse<DayOfWeek>("Sunday".AsSpan(), out var dayOfWeek);
        Assert.AreEqual(DayOfWeek.Sunday, dayOfWeek);
        Assert.True(result);

        result = Enum.TryParse<DayOfWeek>("sunday".AsSpan(), true, out dayOfWeek);
        Assert.AreEqual(DayOfWeek.Sunday, dayOfWeek);
        Assert.True(result);
    }

    enum Colors { Red, Green, Blue }

    [Test]
    public void EnumTryFormat_ValidValue()
    {
        Span<char> buffer = stackalloc char[10];
        var result = Enum.TryFormat(Colors.Green, buffer, out var charsWritten);
        Assert.IsTrue(result);
        Assert.AreEqual("Green", buffer[..charsWritten].ToString());
    }

    [Test]
    public void EnumTryFormat_BufferTooSmall()
    {
        Span<char> buffer = stackalloc char[3];
        var result = Enum.TryFormat(Colors.Blue, buffer, out var charsWritten);
        Assert.IsFalse(result);
        Assert.AreEqual(0, charsWritten);
    }

    [Test]
    public void EnumTryFormat_WithFormatSpecifier()
    {
        Span<char> buffer = stackalloc char[10];
        var result = Enum.TryFormat(Colors.Red, buffer, out var charsWritten, "G");
        Assert.IsTrue(result);
        Assert.AreEqual("Red", buffer[..charsWritten].ToString());
    }

#endif
}