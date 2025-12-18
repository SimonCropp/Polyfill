partial class PolyfillTests
{
    static DateTimeOffset dateTimeOffset = DateTimeOffset.Now;
    static DateTime dateTime = DateTime.Now;

    [Test]
    public async Task AddMicroseconds()
    {
        var fromTicksDateTimeOffset = dateTimeOffset.AddSeconds(1);
        var fromMicrosecondsDateTimeOffset = dateTimeOffset.AddMicroseconds(1000000);
        await Assert.That(fromMicrosecondsDateTimeOffset).IsEqualTo(fromTicksDateTimeOffset);

        var fromTicksDateTime = dateTime.AddSeconds(1);
        var fromMicrosecondsDateTime = dateTime.AddMicroseconds(1000000);
        await Assert.That(fromMicrosecondsDateTime).IsEqualTo(fromTicksDateTime);
    }

#if NET7_0_OR_GREATER
    static TimeSpan timeSpan = DateTime.Now.TimeOfDay;
    [Test]
    public async Task Nanoseconds()
    {
        await Assert.That(dateTimeOffset.Nanosecond()).IsEqualTo(dateTimeOffset.Nanosecond);
        await Assert.That(timeSpan.Nanoseconds()).IsEqualTo(timeSpan.Nanoseconds);
        await Assert.That(dateTime.Nanosecond()).IsEqualTo(dateTime.Nanosecond);
    }

    [Test]
    public async Task Microsecond()
    {
        await Assert.That(dateTimeOffset.Microsecond()).IsEqualTo(dateTimeOffset.Microsecond);
        await Assert.That(timeSpan.Microseconds()).IsEqualTo(timeSpan.Microseconds);
        await Assert.That(dateTime.Microsecond()).IsEqualTo(dateTime.Microsecond);
    }
#endif

}