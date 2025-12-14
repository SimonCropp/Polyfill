partial class PolyfillTests
{
#if NET9_0_OR_GREATER
    [Test]
    public async Task OrderedDictionaryTryAdd()
    {
        var dictionary = new OrderedDictionary<string, int>();
        var key = "Hello";
        var value = 5;
        var result = dictionary.TryAdd(key, value, out var index);
        await Assert.That(result).IsTrue();
        await Assert.That(index).IsEqualTo(0);
    }

    [Test]
    public async Task OrderedDictionaryTryGetValue()
    {
        var dictionary = new OrderedDictionary<string, int>();
        var key = "Hello";
        var value = 5;
        dictionary.TryAdd(key, value, out _);
        var result = dictionary.TryGetValue(key, out var valueOut, out var index);
        await Assert.That(result).IsTrue();
        await Assert.That(valueOut).IsEqualTo(value);
        await Assert.That(index).IsEqualTo(0);
    }
#endif
}
