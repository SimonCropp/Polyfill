#if NET6_0_OR_GREATER

using System.Collections.Generic;

partial class PolyfillTests
{
    [Test]
    public async Task PriorityQueueRemove_Found()
    {
        var queue = new PriorityQueue<string, int>();
        queue.Enqueue("low", 10);
        queue.Enqueue("high", 1);
        queue.Enqueue("medium", 5);

        var removed = queue.Remove("medium", out var element, out var priority);

        await Assert.That(removed).IsTrue();
        await Assert.That(element).IsEqualTo("medium");
        await Assert.That(priority).IsEqualTo(5);
        await Assert.That(queue.Count).IsEqualTo(2);
    }

    [Test]
    public async Task PriorityQueueRemove_NotFound()
    {
        var queue = new PriorityQueue<string, int>();
        queue.Enqueue("low", 10);

        var removed = queue.Remove("missing", out var element, out var priority);

        await Assert.That(removed).IsFalse();
        await Assert.That(queue.Count).IsEqualTo(1);
    }

    [Test]
    public async Task PriorityQueueRemove_WithCustomComparer()
    {
        var queue = new PriorityQueue<string, int>();
        queue.Enqueue("Hello", 1);
        queue.Enqueue("World", 2);

        var removed = queue.Remove("hello", out var element, out var priority, StringComparer.OrdinalIgnoreCase);

        await Assert.That(removed).IsTrue();
        await Assert.That(element).IsEqualTo("Hello");
        await Assert.That(priority).IsEqualTo(1);
    }

    [Test]
    public async Task PriorityQueueRemove_RemovesFirstOccurrence()
    {
        var queue = new PriorityQueue<string, int>();
        queue.Enqueue("dup", 1);
        queue.Enqueue("dup", 2);

        var removed = queue.Remove("dup", out _, out _);

        await Assert.That(removed).IsTrue();
        await Assert.That(queue.Count).IsEqualTo(1);
    }
}

#endif
