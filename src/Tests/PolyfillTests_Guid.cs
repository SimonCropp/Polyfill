partial class PolyfillTests
{
    [Test]
    public void GuidCreate7()
    {
        var dayOfWeeks = GuidPolyfill.CreateVersion7();
        Assert.AreEqual(DayOfWeek.Sunday, dayOfWeeks[0]);
    }
}