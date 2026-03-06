partial class PolyfillTests
{
    [Test]
    public async Task DateTime_Deconstruct_YearMonthDay()
    {
        var dt = new DateTime(2024, 3, 15);
        var (year, month, day) = dt;
        await Assert.That(year).IsEqualTo(2024);
        await Assert.That(month).IsEqualTo(3);
        await Assert.That(day).IsEqualTo(15);
    }

#if NET6_0_OR_GREATER
    [Test]
    public async Task DateTime_Deconstruct_DateAndTime()
    {
        var dt = new DateTime(2024, 3, 15, 10, 30, 45);
        var (date, time) = dt;
        await Assert.That(date).IsEqualTo(new DateOnly(2024, 3, 15));
        await Assert.That(time).IsEqualTo(new TimeOnly(10, 30, 45));
    }
#endif
}
