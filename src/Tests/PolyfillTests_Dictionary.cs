// ReSharper disable CollectionNeverUpdated.Local
partial class PolyfillTests
{
    [Test]
    public async Task IReadOnlyDictionaryAsReadOnly()
    {
        IDictionary<string, string> dictionary = new Dictionary<string, string>
        {
            {
                "key", "value"
            }
        };

        var readOnly = dictionary.AsReadOnly();
        await Assert.That(readOnly["key"]).IsEqualTo("value");
    }

    [Test]
    public async Task Dictionary_TryAdd_ThrowsOnNullKey()
    {
        var dictionary = new Dictionary<string, int>();

        await Assert.That(() => dictionary.TryAdd(null!, 1)).Throws<ArgumentNullException>();
    }

    [Test]
    public async Task IDictionary_TryAdd_ThrowsOnNullKey()
    {
        IDictionary<string, int> dictionary = new Dictionary<string, int>();
        await Assert.That(() => dictionary.TryAdd(null!, 1)).Throws<ArgumentNullException>();
    }

    [Test]
    public async Task IDictionary_TryAdd_ReturnsTrueOnSuccessfulAdd()
    {
        IDictionary<string, string> dictionary = new Dictionary<string, string>();

        var entryAdded = dictionary.TryAdd("key", "value");

        await Assert.That(entryAdded).IsTrue();
        await Assert.That(dictionary["key"]).IsEqualTo("value");
    }

    [Test]
    public async Task Dictionary_TryAdd_ReturnsTrueOnSuccessfulAdd()
    {
        var dictionary = new Dictionary<string, string>();

        var entryAdded = dictionary.TryAdd("key", "value");

        await Assert.That(entryAdded).IsTrue();
        await Assert.That(dictionary["key"]).IsEqualTo("value");
    }

    [Test]
    public async Task Dictionary_TryAdd_ReturnsFalseIfElementAlreadyPresent()
    {
        var dictionary = new Dictionary<string, string>
        {
            {
                "existingKey", "original value"
            }
        };

        var entryAdded = dictionary.TryAdd("existingKey", "new value");

        await Assert.That(entryAdded).IsFalse();
        await Assert.That(dictionary["existingKey"]).IsEqualTo("original value");
    }

    [Test]
    public async Task IDictionary_TryAdd_ReturnsFalseIfElementAlreadyPresent()
    {
        IDictionary<string, string> dictionary = new Dictionary<string, string>
        {
            {
                "existingKey", "original value"
            }
        };

        var entryAdded = dictionary.TryAdd("existingKey", "new value");

        await Assert.That(entryAdded).IsFalse();
        await Assert.That(dictionary["existingKey"]).IsEqualTo("original value");
    }

    [Test]
    public async Task Dictionary_Remove()
    {
        var dictionary = new Dictionary<string, string?>
        {
            {
                "key", "value"
            }
        };

        await Assert.That(dictionary.Remove("key", out var value)).IsTrue();
        await Assert.That(value).IsEqualTo("value");
    }

    [Test]
    public async Task IDictionary_Remove()
    {
        IDictionary<string, string?> dictionary = new Dictionary<string, string?>
        {
            {
                "key", "value"
            }
        };

        await Assert.That(dictionary.Remove("key", out var value)).IsTrue();
        await Assert.That(value).IsEqualTo("value");
    }

    [Test]
    public async Task Dictionary_Remove_DoesntThrowOnMissingKey()
    {
        var dictionary = new Dictionary<string, string?>();

        await Assert.That(dictionary.Remove("non-existent key", out var value)).IsFalse();
        await Assert.That(value).IsNull();
    }

    [Test]
    public async Task IDictionary_Remove_DoesntThrowOnMissingKey()
    {
        IDictionary<string, string?> dictionary = new Dictionary<string, string?>();

        await Assert.That(dictionary.Remove("non-existent key", out var value)).IsFalse();
        await Assert.That(value).IsNull();
    }

    [Test]
    public async Task Dictionary_Remove_ThrowsOnNull()
    {
        var dictionary = new Dictionary<string, string>();
        await Assert.That(() => dictionary.Remove(null!, out _)).Throws<ArgumentNullException>();
    }

    [Test]
    public async Task IDictionary_Remove_ThrowsOnNull()
    {
        IDictionary<string, string> dictionary = new Dictionary<string, string>();
        await Assert.That(() => dictionary.Remove(null!, out _)).Throws<ArgumentNullException>();
    }
}