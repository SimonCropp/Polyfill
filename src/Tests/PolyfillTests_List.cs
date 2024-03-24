partial class PolyfillTests
{
    [Test]
    public void ListAddRangeReadOnlySpan()
    {
        var list = new List<char>();
        list.AddRange("ab".AsSpan());
        Assert.AreEqual('a', list[0]);
        Assert.AreEqual('b', list[1]);
    }
}
