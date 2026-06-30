partial class PolyfillTests
{
    [Test]
    public async Task ConcurrentDictionary_GetOrAdd_NullFactory_Throws()
    {
        var dictionary = new ConcurrentDictionary<string, int>();
        dictionary.TryAdd("a", 1);
        Func<string, int, int> factory = null!;

        // Eager validation: throws for both present and absent keys.
        await Assert.That(() => dictionary.GetOrAdd("a", factory, 5)).Throws<ArgumentNullException>();
        await Assert.That(() => dictionary.GetOrAdd("b", factory, 5)).Throws<ArgumentNullException>();
    }

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
