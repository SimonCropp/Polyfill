partial class PolyfillTests
{
    [Test]
    public async Task IReadOnlyDictionaryGetValueOrDefault()
    {
        var dictionary = new Dictionary<string, string?>
        {
            {
                "key", "value"
            }
        };

        await Assert.That(dictionary.GetValueOrDefault("key")).IsEqualTo("value");
        await Assert.That(dictionary.GetValueOrDefault("key1")).IsEqualTo(null);
        await Assert.That(dictionary.GetValueOrDefault("key1", "value1")).IsEqualTo("value1");
    }

    [Test]
    public async Task IReadOnlyDictionaryGetValueOrDefault_NonNullValue()
    {
        var dictionary = new Dictionary<string, string>
        {
            {
                "key", "value"
            }
        };

        await Assert.That(dictionary.GetValueOrDefault("key")).IsEqualTo("value");
        await Assert.That(dictionary.GetValueOrDefault("key1")).IsEqualTo(null);
        await Assert.That(dictionary.GetValueOrDefault("key1", "value1")).IsEqualTo("value1");
    }
}
