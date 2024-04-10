partial class PolyfillTests
{
    [Test]
    public void ConcurrentDictionaryGetOrAddFunc()
    {
        var dictionary = new ConcurrentDictionary<string, int>();

        Func<string, string, int> valueFactory = (key, arg) => arg.Length;

        var value = dictionary.GetOrAdd("Hello", valueFactory, "World");

        Assert.AreEqual(5, value);

        value = dictionary.GetOrAdd("Hello", valueFactory, "Universe");

        Assert.AreEqual(5, value);
    }
}
