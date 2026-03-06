#if NET6_0_OR_GREATER
partial class PolyfillTests
{
    [Test]
    public async Task TimeOnly_Deconstruct_HourMinute()
    {
        var t = new TimeOnly(14, 30);
        var (hour, minute) = t;
        await Assert.That(hour).IsEqualTo(14);
        await Assert.That(minute).IsEqualTo(30);
    }

    [Test]
    public async Task TimeOnly_Deconstruct_HourMinuteSecond()
    {
        var t = new TimeOnly(14, 30, 45);
        var (hour, minute, second) = t;
        await Assert.That(hour).IsEqualTo(14);
        await Assert.That(minute).IsEqualTo(30);
        await Assert.That(second).IsEqualTo(45);
    }

    [Test]
    public async Task TimeOnly_Deconstruct_HourMinuteSecondMillisecond()
    {
        var t = new TimeOnly(14, 30, 45, 123);
        var (hour, minute, second, millisecond) = t;
        await Assert.That(hour).IsEqualTo(14);
        await Assert.That(minute).IsEqualTo(30);
        await Assert.That(second).IsEqualTo(45);
        await Assert.That(millisecond).IsEqualTo(123);
    }

#if NET7_0_OR_GREATER
    [Test]
    public async Task TimeOnly_Deconstruct_HourMinuteSecondMillisecondMicrosecond()
    {
        var t = new TimeOnly(14, 30, 45, 123, 456);
        var (hour, minute, second, millisecond, microsecond) = t;
        await Assert.That(hour).IsEqualTo(14);
        await Assert.That(minute).IsEqualTo(30);
        await Assert.That(second).IsEqualTo(45);
        await Assert.That(millisecond).IsEqualTo(123);
        await Assert.That(microsecond).IsEqualTo(456);
    }
#endif
}
#endif
