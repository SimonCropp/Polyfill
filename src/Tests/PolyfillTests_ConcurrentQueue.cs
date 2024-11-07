partial class PolyfillTests
{
    [Test]
    public void ConcurrentQueueClear()
    {
        var bag = new ConcurrentQueue<string>();
        bag.Enqueue("Hello");
        bag.Clear();
        Assert.AreEqual(0, bag.Count);
    }
}