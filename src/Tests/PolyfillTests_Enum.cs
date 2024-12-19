partial class PolyfillTests
{
    [Test]
    public void EnumGetValues()
    {
        var dayOfWeeks = EnumPolyfill.GetValues<DayOfWeek>();
        Assert.AreEqual(DayOfWeek.Sunday, dayOfWeeks[0]);
    }

    [Test]
    public void EnumGetNames()
    {
        var dayOfWeeks = EnumPolyfill.GetNames<DayOfWeek>();
        Assert.AreEqual("Sunday", dayOfWeeks[0]);
    }

    [Test]
    public void Parse()
    {
        var dayOfWeek = EnumPolyfill.Parse<DayOfWeek>("Sunday");
        Assert.AreEqual(DayOfWeek.Sunday, dayOfWeek);

        Assert.Throws<ArgumentException>(() => EnumPolyfill.Parse<DayOfWeek>("a"));

        dayOfWeek = EnumPolyfill.Parse<DayOfWeek>("sunday", true);
        Assert.AreEqual(DayOfWeek.Sunday, dayOfWeek);

        Assert.Throws<ArgumentException>(() => EnumPolyfill.Parse<DayOfWeek>("a", true));

#if FeatureMemory

        dayOfWeek = EnumPolyfill.Parse<DayOfWeek>("Sunday".AsSpan());
        Assert.AreEqual(DayOfWeek.Sunday, dayOfWeek);

        Assert.Throws<ArgumentException>(() => EnumPolyfill.Parse<DayOfWeek>("a".AsSpan()));

        dayOfWeek = EnumPolyfill.Parse<DayOfWeek>("sunday".AsSpan(), true);
        Assert.AreEqual(DayOfWeek.Sunday, dayOfWeek);

        Assert.Throws<ArgumentException>(() => EnumPolyfill.Parse<DayOfWeek>("a".AsSpan(), true));

#endif
    }

#if FeatureMemory

    [Test]
    public void TryParse()
    {
        var result = EnumPolyfill.TryParse<DayOfWeek>("Sunday".AsSpan(), out var dayOfWeek);
        Assert.AreEqual(DayOfWeek.Sunday, dayOfWeek);
        Assert.True(result);

        result = EnumPolyfill.TryParse<DayOfWeek>("sunday".AsSpan(), true, out dayOfWeek);
        Assert.AreEqual(DayOfWeek.Sunday, dayOfWeek);
        Assert.True(result);
    }

#endif
}