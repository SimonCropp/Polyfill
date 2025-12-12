partial class PolyfillTests
{
    [Test]
    public async Task ConcurrentQueueClear()
    {
        var bag = new ConcurrentQueue<string>();
        bag.Enqueue("Hello");
        bag.Clear();
        await Assert.That(bag.Count).IsEqualTo(0);
    }
}