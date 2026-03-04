#if NET6_0_OR_GREATER
partial class PolyfillTests
{
    [Test]
    public async Task DateTimeOffset_Deconstruct()
    {
        var offset = TimeSpan.FromHours(5);
        var dto = new DateTimeOffset(2024, 3, 15, 10, 30, 45, offset);
        var (date, time, resultOffset) = dto;
        await Assert.That(date).IsEqualTo(new DateOnly(2024, 3, 15));
        await Assert.That(time).IsEqualTo(new TimeOnly(10, 30, 45));
        await Assert.That(resultOffset).IsEqualTo(offset);
    }
}
#endif
