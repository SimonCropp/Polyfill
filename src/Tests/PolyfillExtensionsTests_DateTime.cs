partial class PolyfillExtensionsTests
{
#if NET7_0_OR_GREATER

    static DateTimeOffset dateTimeOffset = DateTimeOffset.Now;
    static DateTime dateTime = DateTime.Now;
    static TimeSpan timeSpan = DateTime.Now.TimeOfDay;

    [Test]
    public void Nanoseconds()
    {
        Assert.AreEqual(dateTimeOffset.Nanosecond, dateTimeOffset.Nanosecond());
        Assert.AreEqual(timeSpan.Nanoseconds, timeSpan.Nanoseconds());
        Assert.AreEqual(dateTime.Nanosecond, dateTime.Nanosecond());
    }

    [Test]
    public void Microsecond()
    {
        Assert.AreEqual(dateTimeOffset.Microsecond, dateTimeOffset.Microsecond());
        Assert.AreEqual(timeSpan.Microseconds, timeSpan.Microseconds());
        Assert.AreEqual(dateTime.Microsecond, dateTime.Microsecond());
    }
#endif
}