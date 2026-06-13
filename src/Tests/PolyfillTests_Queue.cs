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

    [Test]
    public async Task Queue_TryPeek()
    {
        var queue = new Queue<int>();

        // Test when queue is empty
        var result = queue.TryPeek(out var value);
        await Assert.That(result).IsFalse();
        await Assert.That(value).IsEqualTo(0);

        // Test when queue has elements
        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Enqueue(3);

        result = queue.TryPeek(out value);
        await Assert.That(result).IsTrue();
        // The top element should be 1
        await Assert.That(value).IsEqualTo(1);

        // Ensure the queue is not modified
        await Assert.That(queue.Count).IsEqualTo(3);
    }

    [Test]
    public async Task Queue_TryDequeue()
    {
        var queue = new Queue<int>();

        // Test when queue is empty
        var result = queue.TryDequeue(out var value);
        await Assert.That(result).IsFalse();
        await Assert.That(value).IsEqualTo(0);

        // Test when queue has elements
        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Enqueue(3);

        result = queue.TryDequeue(out value);
        await Assert.That(result).IsTrue();
        // The top element should be 1
        await Assert.That(value).IsEqualTo(1);

        // Ensure the queue is modified
        await Assert.That(queue.Count).IsEqualTo(2);
        await Assert.That(queue.Peek()).IsEqualTo(2);
    }
}
