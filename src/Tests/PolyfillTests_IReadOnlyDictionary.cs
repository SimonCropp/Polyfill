partial class PolyfillTests
{
    [Test]
    public void IReadOnlyDictionaryGetValueOrDefault()
    {
        var dictionary = new Dictionary<string,string?>
        {
            {"key", "value"}
        };

        Assert.AreEqual("value", dictionary.GetValueOrDefault("key"));
        Assert.AreEqual(null, dictionary.GetValueOrDefault("key1"));
        Assert.AreEqual("value1", dictionary.GetValueOrDefault("key1","value1"));
    }
}
