partial class PolyfillTests
{
#if NET9_0_OR_GREATER
    [Test]
    public void OrderedDictionaryTryAdd()
    {
        var dictionary = new OrderedDictionary<string, int>();
        var key = "Hello";
        var value = 5;
        var result = dictionary.TryAdd(key, value, out var index);
        Assert.IsTrue(result);
        Assert.AreEqual(0, index);
    }

    [Test]
    public void OrderedDictionaryTryGetValue()
    {
        var dictionary = new OrderedDictionary<string, int>();
        var key = "Hello";
        var value = 5;
        dictionary.TryAdd(key, value, out _);
        var result = dictionary.TryGetValue(key, out var valueOut, out var index);
        Assert.IsTrue(result);
        Assert.AreEqual(value, valueOut);
        Assert.AreEqual(0, index);
    }
#endif
}