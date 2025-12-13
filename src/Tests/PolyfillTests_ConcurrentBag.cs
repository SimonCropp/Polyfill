partial class PolyfillTests
{
    [Test]
    public async Task ConcurrentBagClear()
    {
        var bag = new ConcurrentBag<string>
        {
            "Hello"
        };
        bag.Clear();
        await Assert.That(bag.Count).IsEqualTo(0);
    }
}