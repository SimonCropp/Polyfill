#if NET6_0_OR_GREATER
partial class PolyfillTests
{
    [Test]
    public async Task DateOnly_Deconstruct()
    {
        var d = new DateOnly(2024, 6, 20);
        var (year, month, day) = d;
        await Assert.That(year).IsEqualTo(2024);
        await Assert.That(month).IsEqualTo(6);
        await Assert.That(day).IsEqualTo(20);
    }
}
#endif
