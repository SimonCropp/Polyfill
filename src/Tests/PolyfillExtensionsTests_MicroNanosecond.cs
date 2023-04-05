partial class PolyfillExtensionsTests
{
    static DateTimeOffset dateTimeOffset = DateTimeOffset.Now;
    static DateTime dateTime = DateTime.Now;
    static TimeSpan timeSpan = DateTime.Now.TimeOfDay;

#if NET7_0_OR_GREATER
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

    [Test]
    public void AddMicroseconds()
    {
        var fromTicksDateTimeOffset = dateTimeOffset.AddSeconds(1);
        var fromMicrosecondsDateTimeOffset = dateTimeOffset.AddMicroseconds(1000000);
        Assert.AreEqual(fromTicksDateTimeOffset, fromMicrosecondsDateTimeOffset);

        var fromTicksDateTime = dateTime.AddSeconds(1);
        var fromMicrosecondsDateTime = dateTime.AddMicroseconds(1000000);
        Assert.AreEqual(fromTicksDateTime, fromMicrosecondsDateTime);
    }
}