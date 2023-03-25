// ReSharper disable PartialTypeWithSinglePart

partial class PolyfillExtensionsSample
{
    [Test]
    public void GetHashCodeStringComparison()
    {
        var hash = "value".GetHashCode(StringComparison.Ordinal);
        Assert.AreEqual(1, hash);
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
}
