partial class PolyfillTests
{
    [Test]
    public async Task TryGetValue()
    {
        var value = "value";
        var set = new HashSet<string>
        {
            value
        };
        var found = set.TryGetValue("value", out var result);
        await Assert.That(found).IsTrue();
        await Assert.That(result!).IsEqualTo(value);
        found = set.TryGetValue("value2", out result);
        await Assert.That(result).IsNull();
        await Assert.That(found).IsFalse();
    }
}