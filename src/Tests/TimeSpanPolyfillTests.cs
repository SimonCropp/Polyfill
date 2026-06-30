public class TimeSpanPolyfillTests
{
    [Test]
    public async Task From_HappyPath_And_Overflow()
    {
        await Assert.That(TimeSpan.FromMicroseconds(1000000).Ticks).IsEqualTo(TimeSpan.TicksPerSecond);
        await Assert.That(TimeSpan.FromDays(1).Ticks).IsEqualTo(TimeSpan.TicksPerDay);
        await Assert.That(TimeSpan.FromSeconds(1, 0, 0).Ticks).IsEqualTo(TimeSpan.TicksPerSecond);

        // Components that cancel out must stay in range. Accumulation happens in a wide (decimal)
        // space, so even when an individual component product overflows Int64, a cancelling
        // component keeps the result in range (matching the BCL's 128-bit accumulation).
        await Assert.That(TimeSpan.FromSeconds(1000000000000, -1000000000000000, 0).Ticks).IsEqualTo(0L);
        await Assert.That(TimeSpan.FromSeconds(0, -9498038399810654, long.MaxValue).Ticks).IsEqualTo(-2746663629558781930L);

        // The multi-argument overloads reach the integer-based factory on every framework
        // (the single-argument FromMicroseconds(double) BCL overload shadows the polyfill on net7/8).
        await Assert.That(() => TimeSpan.FromDays(int.MaxValue, 0, 0, 0, 0, 0)).Throws<ArgumentOutOfRangeException>();
        await Assert.That(() => TimeSpan.FromSeconds(long.MaxValue, 0, 0)).Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task Microseconds_ReturnsCorrectValue()
    {
        var ts = new TimeSpan(1, 2, 3, 4, 5);
        var microseconds = ts.Microseconds;
        await Assert.That(microseconds).IsGreaterThanOrEqualTo(0);
    }

    [Test]
    public async Task Nanoseconds_ReturnsCorrectValue()
    {
        var ts = new TimeSpan(1, 2, 3, 4, 5);
        var nanoseconds = ts.Nanoseconds;
        await Assert.That(nanoseconds).IsGreaterThanOrEqualTo(0);
    }

    [Test]
    public async Task Microseconds_Zero_TimeSpan()
    {
        var ts = TimeSpan.Zero;
        await Assert.That(ts.Microseconds).IsEqualTo(0);
        await Assert.That(ts.Nanoseconds).IsEqualTo(0);
    }

    [Test]
    public async Task FromDays_Int()
    {
        var ts = TimeSpan.FromDays(2);
        await Assert.That(ts.TotalDays).IsEqualTo(2.0);
    }

    [Test]
    public async Task FromDays_WithHours()
    {
        var ts = TimeSpan.FromDays(1, 12);
        await Assert.That(ts.TotalHours).IsEqualTo(36.0);
    }

    [Test]
    public async Task FromHours_Int()
    {
        var ts = TimeSpan.FromHours(5);
        await Assert.That(ts.TotalHours).IsEqualTo(5.0);
    }

    [Test]
    public async Task FromMinutes_Int()
    {
        var ts = TimeSpan.FromMinutes(90);
        await Assert.That(ts.TotalMinutes).IsEqualTo(90.0);
    }

    [Test]
    public async Task FromSeconds_Long()
    {
        var ts = TimeSpan.FromSeconds(120L);
        await Assert.That(ts.TotalSeconds).IsEqualTo(120.0);
    }

    [Test]
    public async Task FromSeconds_WithMilliseconds()
    {
        var ts = TimeSpan.FromSeconds(1L, 500L);
        await Assert.That(ts.TotalMilliseconds).IsEqualTo(1500.0);
    }

    [Test]
    public async Task FromMilliseconds_Long()
    {
        var ts = TimeSpan.FromMilliseconds(2500L);
        await Assert.That(ts.TotalMilliseconds).IsEqualTo(2500.0);
    }

    [Test]
    public async Task FromMicroseconds_Long()
    {
        var ts = TimeSpan.FromMicroseconds(1000L);
        await Assert.That(ts.TotalMilliseconds).IsEqualTo(1.0);
    }
}
