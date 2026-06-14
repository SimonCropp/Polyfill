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
    public async Task Queue_TryPeek_Empty()
    {
        var queue = new Queue<int>();

        var result = queue.TryPeek(out var value);

        await Assert.That(result).IsFalse();
        await Assert.That(value).IsEqualTo(0);
    }

    [Test]
    public async Task Queue_TryPeek()
    {
        var queue = new Queue<int>();
        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Enqueue(3);

        var result = queue.TryPeek(out var value);

        await Assert.That(result).IsTrue();
        // Front of the queue (FIFO) is the first enqueued element
        await Assert.That(value).IsEqualTo(1);

        // Peek is non-destructive and stable across repeated calls
        result = queue.TryPeek(out value);
        await Assert.That(result).IsTrue();
        await Assert.That(value).IsEqualTo(1);
        await Assert.That(queue.Count).IsEqualTo(3);
    }

    [Test]
    public async Task Queue_TryDequeue_Empty()
    {
        var queue = new Queue<int>();

        var result = queue.TryDequeue(out var value);

        await Assert.That(result).IsFalse();
        await Assert.That(value).IsEqualTo(0);
    }

    [Test]
    public async Task Queue_TryDequeue()
    {
        var queue = new Queue<int>();
        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Enqueue(3);

        // Draining returns elements in FIFO order
        var result = queue.TryDequeue(out var value);
        await Assert.That(result).IsTrue();
        await Assert.That(value).IsEqualTo(1);
        await Assert.That(queue.Count).IsEqualTo(2);

        result = queue.TryDequeue(out value);
        await Assert.That(result).IsTrue();
        await Assert.That(value).IsEqualTo(2);

        result = queue.TryDequeue(out value);
        await Assert.That(result).IsTrue();
        await Assert.That(value).IsEqualTo(3);

        // Once drained it reports empty again
        result = queue.TryDequeue(out value);
        await Assert.That(result).IsFalse();
        await Assert.That(value).IsEqualTo(0);
        await Assert.That(queue.Count).IsEqualTo(0);
    }
}
