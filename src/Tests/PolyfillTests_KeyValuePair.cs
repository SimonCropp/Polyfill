partial class PolyfillTests
{
    [Test]
    public async Task KeyValuePair_Deconstruct()
    {
        var kvp = new KeyValuePair<string, int>("key", 42);
        var (key, value) = kvp;
        await Assert.That(key).IsEqualTo("key");
        await Assert.That(value).IsEqualTo(42);
    }

    [Test]
    public async Task KeyValuePair_Create()
    {
        var kvp = KeyValuePair.Create("key", 42);
        await Assert.That(kvp.Key).IsEqualTo("key");
        await Assert.That(kvp.Value).IsEqualTo(42);
    }
}
