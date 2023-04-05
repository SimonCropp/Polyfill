partial class PolyfillExtensionsTests
{
#if NET7_0_OR_GREATER

    static DateTimeOffset dateTimeOffset = DateTimeOffset.Now;
    static DateTime dateTime = DateTime.Now;

    [Test]
    public void Nanoseconds()
    {
        Assert.AreEqual(dateTimeOffset.Nanosecond, dateTimeOffset.Nanosecond());
        Assert.AreEqual(dateTime.Nanosecond, dateTime.Nanosecond());
    }

    [Test]
    public void Microsecond()
    {
        Assert.AreEqual(dateTimeOffset.Microsecond, dateTimeOffset.Microsecond());
        Assert.AreEqual(dateTime.Microsecond, dateTime.Microsecond());
    }
#endif
}