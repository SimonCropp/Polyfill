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

    [Test]
    public async Task AddMicroseconds_SubMillisecond()
    {
        // 1 microsecond == 10 ticks; sub-millisecond precision must be preserved
        // (AddMilliseconds rounds to whole milliseconds on .NET Framework).
        var baseDateTime = new DateTime(2020, 1, 1);
        await Assert.That((baseDateTime.AddMicroseconds(500) - baseDateTime).Ticks).IsEqualTo(5000L);
        await Assert.That((baseDateTime.AddMicroseconds(1) - baseDateTime).Ticks).IsEqualTo(10L);

        var baseOffset = new DateTimeOffset(2020, 1, 1, 0, 0, 0, TimeSpan.Zero);
        await Assert.That((baseOffset.AddMicroseconds(500) - baseOffset).Ticks).IsEqualTo(5000L);
        await Assert.That((baseOffset.AddMicroseconds(1) - baseOffset).Ticks).IsEqualTo(10L);
    }

    [Test]
    public async Task Microsecond_and_Nanosecond()
    {
        // 1234567 sub-second ticks decompose to 123 ms, 456 µs, 700 ns
        var dateTimeValue = new DateTime(2020, 1, 1, 12, 30, 45).AddTicks(1234567);
        await Assert.That(dateTimeValue.Microsecond).IsEqualTo(456);
        await Assert.That(dateTimeValue.Nanosecond).IsEqualTo(700);

        var dateTimeOffsetValue = new DateTimeOffset(dateTimeValue, TimeSpan.Zero);
        await Assert.That(dateTimeOffsetValue.Microsecond).IsEqualTo(456);
        await Assert.That(dateTimeOffsetValue.Nanosecond).IsEqualTo(700);

        var timeSpanValue = new TimeSpan(1234567);
        await Assert.That(timeSpanValue.Microseconds).IsEqualTo(456);
        await Assert.That(timeSpanValue.Nanoseconds).IsEqualTo(700);
    }
}