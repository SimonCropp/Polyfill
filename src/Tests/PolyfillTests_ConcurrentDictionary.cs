partial class PolyfillTests
{
    [Test]
    public async Task ConcurrentDictionaryGetOrAddFunc()
    {
        var dictionary = new ConcurrentDictionary<string, int>();

        Func<string, string, int> valueFactory = static (key, arg) => arg.Length;

        var value = dictionary.GetOrAdd("Hello", valueFactory, "World");

        await Assert.That(value).IsEqualTo(5);

        value = dictionary.GetOrAdd("Hello", valueFactory, "Universe");

        await Assert.That(value).IsEqualTo(5);
    }
}
