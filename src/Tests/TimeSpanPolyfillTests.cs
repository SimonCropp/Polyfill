public class TimeSpanPolyfillTests
{
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
}
