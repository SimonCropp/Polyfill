using NUnit.Framework;

[TestFixture]
public class Tests
{
    [Test]
    public void NoGuard() =>
        Assert.IsNull(GetType().Assembly.GetType("Polyfills.Guard"));
}