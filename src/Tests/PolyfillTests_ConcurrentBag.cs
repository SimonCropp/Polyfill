partial class PolyfillTests
{
    [Test]
    public void ConcurrentBagClear()
    {
        var bag = new ConcurrentBag<string>
        {
            "Hello"
        };
        bag.Clear();
        Assert.AreEqual(0, bag.Count);
    }
}