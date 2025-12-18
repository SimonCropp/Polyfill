public class Tests
{
    [Test]
    public async Task NoEnsure() =>
        await Assert.That(GetType().Assembly.GetType("Polyfills.Ensure")).IsNull();

    [Test]
    public async Task NoNullExtensions()
    {
        var method = typeof(Polyfill)
            .GetMethod("GetNullabilityInfo", BindingFlags.Static | BindingFlags.Public, null, [typeof(MemberInfo)], null);
        await Assert.That(method).IsNull();
    }
}