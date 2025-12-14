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
    public void TimeSpan_FromMilliseconds_Long_CreatesCorrectTimeSpan()
    {
        // Arrange & Act
        var timeSpan = TimeSpan.FromMilliseconds(5000L);

        // Assert
        Assert.AreEqual(5, timeSpan.TotalSeconds);
        Assert.AreEqual(5000, timeSpan.TotalMilliseconds);
    }

    [Test]
    public void TimeSpan_FromMilliseconds_Long_Zero()
    {
        // Arrange & Act
        var timeSpan = TimeSpan.FromMilliseconds(0L);

        // Assert
        Assert.AreEqual(TimeSpan.Zero, timeSpan);
    }

    [Test]
    public void TimeSpan_FromMilliseconds_Long_Negative()
    {
        // Arrange & Act
        var timeSpan = TimeSpan.FromMilliseconds(-1000L);

        // Assert
        Assert.AreEqual(-1, timeSpan.TotalSeconds);
        Assert.AreEqual(-1000, timeSpan.TotalMilliseconds);
    }

    [Test]
    public void TimeSpan_FromMilliseconds_Long_LargeValue()
    {
        // Arrange - 1 hour in milliseconds
        var oneHourMs = 3600000L;

        // Act
        var timeSpan = TimeSpan.FromMilliseconds(oneHourMs);

        // Assert
        Assert.AreEqual(1, timeSpan.TotalHours);
        Assert.AreEqual(60, timeSpan.TotalMinutes);
    }

    [Test]
    public void TimeSpan_FromMilliseconds_Long_MaxValue_ThrowsOverflowException()
    {
        // Arrange - value that would overflow
        var tooLarge = long.MaxValue;

        // Act & Assert - Native .NET 10+ throws ArgumentOutOfRangeException, polyfill throws OverflowException
#if NET10_0_OR_GREATER
        Assert.Throws<ArgumentOutOfRangeException>(() => TimeSpan.FromMilliseconds(tooLarge));
#else
        Assert.Throws<OverflowException>(() => TimeSpan.FromMilliseconds(tooLarge));
#endif
    }

    [Test]
    public void TimeSpan_FromMilliseconds_Long_MinValue_ThrowsOverflowException()
    {
        // Arrange - value that would overflow
        var tooSmall = long.MinValue;

        // Act & Assert - Native .NET 10+ throws ArgumentOutOfRangeException, polyfill throws OverflowException
#if NET10_0_OR_GREATER
        Assert.Throws<ArgumentOutOfRangeException>(() => TimeSpan.FromMilliseconds(tooSmall));
#else
        Assert.Throws<OverflowException>(() => TimeSpan.FromMilliseconds(tooSmall));
#endif
    }

    [Test]
    public void TimeSpan_FromMilliseconds_Long_NearMaxValue()
    {
        // Arrange - near max safe milliseconds value
        const long nearMaxMilliseconds = (long.MaxValue / 10000) - 1;

        // Act
        var timeSpan = TimeSpan.FromMilliseconds(nearMaxMilliseconds);

        // Assert
        Assert.AreEqual(nearMaxMilliseconds, (long)timeSpan.TotalMilliseconds);
    }

    [Test]
    public void TimeSpan_FromMilliseconds_Long_NearMinValue()
    {
        // Arrange - near min safe milliseconds value
        const long nearMinMilliseconds = (long.MinValue / 10000) + 1;

        // Act
        var timeSpan = TimeSpan.FromMilliseconds(nearMinMilliseconds);

        // Assert
        Assert.AreEqual(nearMinMilliseconds, (long)timeSpan.TotalMilliseconds);
    }
}