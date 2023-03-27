// ReSharper disable PartialTypeWithSinglePart

partial class PolyfillExtensionsTests
{
    [Test]
    public void GetHashCodeStringComparison()
    {
        var hash = "value".GetHashCode(StringComparison.Ordinal);
        Assert.AreNotEqual(0, hash);
    }

    [Test]
    public void EndsWith()
    {
        Assert.True("value".EndsWith('e'));
        Assert.False("".EndsWith('e'));
    }

    [Test]
    public void StringContainsStringComparison() =>
        Assert.True("value".Contains("E", StringComparison.OrdinalIgnoreCase));

    [Test]
    public void StartsWith()
    {
        Assert.True("value".StartsWith('v'));
        Assert.False("".StartsWith('v'));
    }

    [Test]
    public void Split()
    {
        Assert.AreEqual(new []{"a","b"}, "a b".Split(' ', StringSplitOptions.RemoveEmptyEntries));
        Assert.AreEqual(new []{"a","b"}, "a b".Split(' ', 2, StringSplitOptions.RemoveEmptyEntries));
    }

    [Test]
    public void ContainsChar() =>
        Assert.True("value".Contains('v'));
}
