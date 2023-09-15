partial class PolyfillExtensionsTests
{
    [Test]
    public void Dictionary_Remove()
    {
        var dictionary = new Dictionary<string, string?> { {"key", "value"} };

        Assert.True(dictionary.Remove("key", out var value));
        Assert.AreEqual("value", value);
    }

    [Test]
    public void Dictionary_Remove_DoesntThrowOnMissingKey()
    {
        var dictionary = new Dictionary<string, string?>();

        Assert.False(dictionary.Remove("non-existent key", out var value));
        Assert.AreEqual(default, value);
    }

    [Test]
    public void Dictionary_Remove_ThrowsOnNull()
    {
        var dictionary = new Dictionary<string, string>();
        Assert.Throws<ArgumentNullException>(() => dictionary.Remove(null!, out _));
    }
}
