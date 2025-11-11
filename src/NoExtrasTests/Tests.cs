[TestFixture]
public class Tests
{
    [Test]
    public void NoEnsure() =>
        Assert.IsNull(GetType().Assembly.GetType("Polyfills.Ensure"));

    [Test]
    public void NoNullExtensions()
    {
        var method = typeof(Polyfill)
            .GetMethod("GetNullabilityInfo", BindingFlags.Static | BindingFlags.Public, null, [typeof(MemberInfo)], null);
        Assert.IsNull(method);
    }
}