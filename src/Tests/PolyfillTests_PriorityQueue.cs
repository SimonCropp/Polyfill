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
    static PriorityQueue<string, int> BuildPriorityQueue(int seed, int count, int priorities)
    {
        var random = new Random(seed);
        var queue = new PriorityQueue<string, int>();
        for (var index = 0; index < count; index++)
        {
            queue.Enqueue($"e{random.Next(0, count)}", random.Next(0, priorities));
        }

        return queue;
    }

    [Test]
    [Arguments(1, 1)]
    [Arguments(2, 1)]
    [Arguments(5, 100)]
    [Arguments(40, 3)]
    [Arguments(40, 1000)]
    [Arguments(300, 5)]
    public async Task PriorityQueueRemove_RemovesFirstUnorderedMatch(int count, int priorities)
    {
        for (var seed = 0; seed < 20; seed++)
        {
            var queue = BuildPriorityQueue(seed, count, priorities);
            var before = queue.UnorderedItems.ToList();
            var target = before[seed % before.Count].Element;
            // the first match in the order the queue holds its items
            var expected = before.First(_ => _.Element == target);

            var removed = queue.Remove(target, out var element, out var priority);

            await Assert.That(removed).IsTrue();
            await Assert.That(element).IsEqualTo(expected.Element);
            await Assert.That(priority).IsEqualTo(expected.Priority);
            await Assert.That(queue.Count).IsEqualTo(count - 1);

            // exactly one item is gone
            var remaining = before.ToList();
            remaining.Remove(expected);
            var after = queue.UnorderedItems.ToList();
            await Assert.That(after.OrderBy(_ => _.Priority).ThenBy(_ => _.Element, StringComparer.Ordinal)
                    .SequenceEqual(remaining.OrderBy(_ => _.Priority).ThenBy(_ => _.Element, StringComparer.Ordinal)))
                .IsTrue();

            // and the queue still dequeues in priority order, and can be added to
            queue.Enqueue("added", -1);
            var dequeued = new List<int>();
            while (queue.TryDequeue(out _, out var dequeuedPriority))
            {
                dequeued.Add(dequeuedPriority);
            }

            await Assert.That(dequeued.SequenceEqual(remaining.Select(_ => _.Priority).Append(-1).OrderBy(_ => _))).IsTrue();
        }
    }

    [Test]
    public async Task PriorityQueueRemove_DistinctPriorities_DequeueOrder()
    {
        var queue = new PriorityQueue<string, int>();
        foreach (var priority in new[] {7, 3, 9, 1, 8, 2, 6, 4, 5, 0})
        {
            queue.Enqueue($"p{priority}", priority);
        }

        await Assert.That(queue.Remove("p0", out _, out _)).IsTrue();
        await Assert.That(queue.Remove("p5", out _, out _)).IsTrue();
        await Assert.That(queue.Remove("p9", out _, out _)).IsTrue();
        await Assert.That(queue.Remove("p9", out _, out _)).IsFalse();

        var dequeued = new List<string>();
        while (queue.TryDequeue(out var element, out _))
        {
            dequeued.Add(element);
        }

        await Assert.That(dequeued.SequenceEqual(["p1", "p2", "p3", "p4", "p6", "p7", "p8"])).IsTrue();
    }

    [Test]
    public async Task PriorityQueueRemove_NotFound_LeavesQueueUntouched()
    {
        var queue = BuildPriorityQueue(1, 50, 4);
        var before = queue.UnorderedItems.ToList();

        // a miss does not modify the queue, so does not invalidate an enumeration of it
        var seen = 0;
        foreach (var _ in queue.UnorderedItems)
        {
            var removed = queue.Remove("missing", out var element, out var priority);
            await Assert.That(removed).IsFalse();
            await Assert.That(element).IsNull();
            await Assert.That(priority).IsEqualTo(0);
            seen++;
        }

        await Assert.That(seen).IsEqualTo(50);
        await Assert.That(queue.UnorderedItems.SequenceEqual(before)).IsTrue();

        var empty = new PriorityQueue<string, int>();
        await Assert.That(empty.Remove("missing", out _, out _)).IsFalse();
        await Assert.That(empty.Count).IsEqualTo(0);
    }

    [Test]
    public async Task PriorityQueueRemove_LastItem_AndCustomPriorityComparer()
    {
        var single = new PriorityQueue<string, int>();
        single.Enqueue("only", 3);
        await Assert.That(single.Remove("only", out var element, out var priority)).IsTrue();
        await Assert.That(element).IsEqualTo("only");
        await Assert.That(priority).IsEqualTo(3);
        await Assert.That(single.Count).IsEqualTo(0);

        // a max-queue keeps its ordering after a removal
        var max = new PriorityQueue<string, int>(Comparer<int>.Create((left, right) => right.CompareTo(left)));
        foreach (var value in new[] {4, 1, 5, 2, 3})
        {
            max.Enqueue($"p{value}", value);
        }

        await Assert.That(max.Remove("p5", out _, out priority)).IsTrue();
        await Assert.That(priority).IsEqualTo(5);

        var dequeued = new List<string>();
        while (max.TryDequeue(out var next, out _))
        {
            dequeued.Add(next);
        }

        await Assert.That(dequeued.SequenceEqual(["p4", "p3", "p2", "p1"])).IsTrue();
    }

    [Test]
    public async Task PriorityQueueRemove_Polyfill_KeepsOrderOfEqualPriorities()
    {
        // The order of items of equal priority is not specified, and .NET 9 leaves the queue in a different
        // layout to the polyfill. This pins the layout the polyfill has always produced: the remaining
        // items enqueued one at a time, in the order the queue held them.
        if (Environment.Version.Major >= 9)
        {
            return;
        }

        for (var seed = 0; seed < 50; seed++)
        {
            var queue = BuildPriorityQueue(seed, 30, 3);
            var before = queue.UnorderedItems.ToList();
            var target = before[seed % before.Count].Element;
            var foundIndex = before.FindIndex(_ => _.Element == target);

            var expected = new PriorityQueue<string, int>();
            for (var index = 0; index < before.Count; index++)
            {
                if (index != foundIndex)
                {
                    expected.Enqueue(before[index].Element, before[index].Priority);
                }
            }

            queue.Remove(target, out _, out _);

            await Assert.That(queue.UnorderedItems.SequenceEqual(expected.UnorderedItems)).IsTrue();

            var dequeued = new List<string>();
            while (queue.TryDequeue(out var element, out _))
            {
                dequeued.Add(element);
            }

            var expectedDequeued = new List<string>();
            while (expected.TryDequeue(out var element, out _))
            {
                expectedDequeued.Add(element);
            }

            await Assert.That(dequeued.SequenceEqual(expectedDequeued)).IsTrue();
        }
    }
}

#endif
