using System.Collections;

partial class PolyfillTests
{
    [Test]
    public async Task DictionaryEntry_Deconstruct()
    {
        var entry = new DictionaryEntry("key", "value");
        var (key, value) = entry;
        await Assert.That(key).IsEqualTo("key");
        await Assert.That(value).IsEqualTo("value");
    }

    [Test]
    public async Task DictionaryEntry_Deconstruct_NullValue()
    {
        var entry = new DictionaryEntry("key", null);
        var (key, value) = entry;
        await Assert.That(key).IsEqualTo("key");
        await Assert.That(value).IsNull();
    }
}
