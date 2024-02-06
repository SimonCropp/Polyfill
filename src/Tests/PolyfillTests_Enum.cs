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
}
