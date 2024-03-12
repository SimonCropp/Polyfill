partial class PolyfillTests
{
    [Test]
    public void TryGetValue()
    {
        var value = "value";
        var set = new HashSet<string>
        {
            value
        };
        var found = set.TryGetValue("value", out var result);
        Assert.True(found);
        Assert.AreSame(value, result!);
    }
}