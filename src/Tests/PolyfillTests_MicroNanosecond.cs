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

    [Test]
    public async Task TimeSpan_FromMilliseconds_Long_CreatesCorrectTimeSpan()
    {
        // Arrange & Act
        var timeSpan = TimeSpan.FromMilliseconds(5000L);

        // Assert
        await Assert.That(timeSpan.TotalSeconds).IsEqualTo(5);
        await Assert.That(timeSpan.TotalMilliseconds).IsEqualTo(5000);
    }

    [Test]
    public async Task TimeSpan_FromMilliseconds_Long_Zero()
    {
        // Arrange & Act
        var timeSpan = TimeSpan.FromMilliseconds(0L);

        // Assert
        await Assert.That(timeSpan).IsEqualTo(TimeSpan.Zero);
    }

    [Test]
    public async Task TimeSpan_FromMilliseconds_Long_Negative()
    {
        // Arrange & Act
        var timeSpan = TimeSpan.FromMilliseconds(-1000L);

        // Assert
        await Assert.That(timeSpan.TotalSeconds).IsEqualTo(-1);
        await Assert.That(timeSpan.TotalMilliseconds).IsEqualTo(-1000);
    }

    [Test]
    public async Task TimeSpan_FromMilliseconds_Long_LargeValue()
    {
        // Arrange - 1 hour in milliseconds
        var oneHourMs = 3600000L;

        // Act
        var timeSpan = TimeSpan.FromMilliseconds(oneHourMs);

        // Assert
        await Assert.That(timeSpan.TotalHours).IsEqualTo(1);
        await Assert.That(timeSpan.TotalMinutes).IsEqualTo(60);
    }

    [Test]
    public async Task TimeSpan_FromMilliseconds_Long_MaxValue_ThrowsOverflowException()
    {
        // Arrange - value that would overflow
        var tooLarge = long.MaxValue;

        await Assert.That(() => TimeSpan.FromMilliseconds(tooLarge)).Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task TimeSpan_FromMilliseconds_Long_MinValue_ThrowsOverflowException()
    {
        // Arrange - value that would overflow
        var tooSmall = long.MinValue;

        await Assert.That(() => TimeSpan.FromMilliseconds(tooSmall)).Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task TimeSpan_FromMilliseconds_Long_NearMaxValue()
    {
        // Arrange - near max safe milliseconds value
        const long nearMaxMilliseconds = long.MaxValue / 10000 - 1;

        // Act
        var timeSpan = TimeSpan.FromMilliseconds(nearMaxMilliseconds);

        // Assert
        await Assert.That((long)timeSpan.TotalMilliseconds).IsEqualTo(nearMaxMilliseconds);
    }

    [Test]
    public async Task TimeSpan_FromMilliseconds_Long_NearMinValue()
    {
        // Arrange - near min safe milliseconds value
        const long nearMinMilliseconds = long.MinValue / 10000 + 1;

        // Act
        var timeSpan = TimeSpan.FromMilliseconds(nearMinMilliseconds);

        // Assert
        await Assert.That((long)timeSpan.TotalMilliseconds).IsEqualTo(nearMinMilliseconds);
    }
}