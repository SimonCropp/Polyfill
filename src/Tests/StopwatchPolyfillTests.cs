using System.Diagnostics;

public class StopwatchPolyfillTests
{
    [Test]
    public async Task GetElapsedTime_SingleTimestamp()
    {
        var start = Stopwatch.GetTimestamp();
        await Task.Delay(10);
        var elapsed = Stopwatch.GetElapsedTime(start);
        await Assert.That(elapsed.TotalMilliseconds).IsGreaterThan(0);
    }

    [Test]
    public async Task GetElapsedTime_TwoTimestamps()
    {
        var start = Stopwatch.GetTimestamp();
        await Task.Delay(10);
        var end = Stopwatch.GetTimestamp();
        var elapsed = Stopwatch.GetElapsedTime(start, end);
        await Assert.That(elapsed.TotalMilliseconds).IsGreaterThan(0);
    }
}
