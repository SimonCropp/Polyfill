partial class PolyfillTests
{
    [Test]
    public async Task Queue_EnsureCapacity()
    {
        var queue = new Queue<int>();
        queue.EnsureCapacity(100);
        // Should not throw - capacity is a hint
        await Assert.That(queue.Count).IsEqualTo(0);
    }

    [Test]
    public async Task Queue_TrimExcess()
    {
        var queue = new Queue<int>();
        for (var i = 0; i < 10; i++)
        {
            queue.Enqueue(i);
        }

        queue.TrimExcess(100);
        // Should not throw
        await Assert.That(queue.Count).IsEqualTo(10);
    }
}
