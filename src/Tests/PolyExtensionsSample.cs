[TestFixture]
class PolyExtensionsSample
{
    [Test]
    public void EndsWith() =>
        Assert.True("value".EndsWith('e'));

    [Test]
    public void StartsWith() =>
        Assert.True("value".StartsWith('v'));
}
