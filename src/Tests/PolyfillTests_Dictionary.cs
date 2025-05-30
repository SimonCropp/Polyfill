partial class PolyfillTests
{
    [Test]
    public void IReadOnlyDictionaryAsReadOnly()
    {
        IDictionary<string, string> dictionary = new Dictionary<string, string>
        {
            {
                "key", "value"
            }
        };

        var readOnly = dictionary.AsReadOnly();
        Assert.AreEqual("value", readOnly["key"]);
    }

    [Test]
    public void Dictionary_TryAdd_ThrowsOnNullKey()
    {
        var dictionary = new Dictionary<string, int>();

        Assert.Throws<ArgumentNullException>(() => dictionary.TryAdd(null!, 1));
    }

    [Test]
    public void IDictionary_TryAdd_ThrowsOnNullKey()
    {
        IDictionary<string, int> dictionary = new Dictionary<string, int>();
        Assert.Throws<ArgumentNullException>(() => dictionary.TryAdd(null!, 1));
    }

    [Test]
    public void IDictionary_TryAdd_ReturnsTrueOnSuccessfulAdd()
    {
        IDictionary<string, string> dictionary = new Dictionary<string, string>();

        var entryAdded = dictionary.TryAdd("key", "value");

        Assert.True(entryAdded);
        Assert.AreEqual("value", dictionary["key"]);
    }

    [Test]
    public void Dictionary_TryAdd_ReturnsTrueOnSuccessfulAdd()
    {
        var dictionary = new Dictionary<string, string>();

        var entryAdded = dictionary.TryAdd("key", "value");

        Assert.True(entryAdded);
        Assert.AreEqual("value", dictionary["key"]);
    }

    [Test]
    public void Dictionary_TryAdd_ReturnsFalseIfElementAlreadyPresent()
    {
        var dictionary = new Dictionary<string, string>
        {
            {
                "existingKey", "original value"
            }
        };

        var entryAdded = dictionary.TryAdd("existingKey", "new value");

        Assert.False(entryAdded);
        Assert.AreEqual("original value", dictionary["existingKey"]);
    }

    [Test]
    public void IDictionary_TryAdd_ReturnsFalseIfElementAlreadyPresent()
    {
        IDictionary<string, string> dictionary = new Dictionary<string, string>
        {
            {
                "existingKey", "original value"
            }
        };

        var entryAdded = dictionary.TryAdd("existingKey", "new value");

        Assert.False(entryAdded);
        Assert.AreEqual("original value", dictionary["existingKey"]);
    }

    [Test]
    public void Dictionary_Remove()
    {
        var dictionary = new Dictionary<string, string?>
        {
            {
                "key", "value"
            }
        };

        Assert.True(dictionary.Remove("key", out var value));
        Assert.AreEqual("value", value);
    }

    [Test]
    public void IDictionary_Remove()
    {
        IDictionary<string, string?> dictionary = new Dictionary<string, string?>
        {
            {
                "key", "value"
            }
        };

        Assert.True(dictionary.Remove("key", out var value));
        Assert.AreEqual("value", value);
    }

    [Test]
    public void Dictionary_Remove_DoesntThrowOnMissingKey()
    {
        var dictionary = new Dictionary<string, string?>();

        Assert.False(dictionary.Remove("non-existent key", out var value));
        Assert.IsNull(value);
    }

    [Test]
    public void IDictionary_Remove_DoesntThrowOnMissingKey()
    {
        IDictionary<string, string?> dictionary = new Dictionary<string, string?>();

        Assert.False(dictionary.Remove("non-existent key", out var value));
        Assert.IsNull(value);
    }

    [Test]
    public void Dictionary_Remove_ThrowsOnNull()
    {
        var dictionary = new Dictionary<string, string>();
        Assert.Throws<ArgumentNullException>(() => dictionary.Remove(null!, out _));
    }

    [Test]
    public void IDictionary_Remove_ThrowsOnNull()
    {
        IDictionary<string, string> dictionary = new Dictionary<string, string>();
        Assert.Throws<ArgumentNullException>(() => dictionary.Remove(null!, out _));
    }
}