[TestFixture]
public class Tests
{
    [Test]
    public void NoGuard() =>
        Assert.IsNull(GetType().Assembly.GetType("Polyfills.Guard"));

    [Test]
    public void NoNullExtensions()
    {
        var method = typeof(Polyfill)
            .GetMethod("GetNullabilityInfo", BindingFlags.Static | BindingFlags.Public, null, [typeof(MemberInfo)], null);
        Assert.IsNull(method);
    }
}