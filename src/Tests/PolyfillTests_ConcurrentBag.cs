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
    [Test]
    [Arguments(0)]
    [Arguments(1)]
    [Arguments(2)]
    [Arguments(1000)]
    public async Task ConcurrentBagClear_RemovesEverything(int count)
    {
        var bag = new ConcurrentBag<int>(Enumerable.Range(0, count));

        bag.Clear();

        await Assert.That(bag.IsEmpty).IsTrue();
        await Assert.That(bag.Count).IsEqualTo(0);
        await Assert.That(bag.ToArray().Length).IsEqualTo(0);
        await Assert.That(bag.TryTake(out _)).IsFalse();
        await Assert.That(bag.TryPeek(out _)).IsFalse();

        // clearing an empty bag is fine, and the bag is still usable
        bag.Clear();
        bag.Add(7);
        await Assert.That(bag.Count).IsEqualTo(1);
        await Assert.That(bag.TryTake(out var taken)).IsTrue();
        await Assert.That(taken).IsEqualTo(7);
    }

    [Test]
    public async Task ConcurrentBagClear_ItemsAddedFromSeveralThreads()
    {
        // each thread adds to its own list inside the bag, and all of them must be emptied
        var bag = new ConcurrentBag<string>();
        var threads = new List<System.Threading.Thread>();
        for (var thread = 0; thread < 4; thread++)
        {
            var name = thread;
            threads.Add(
                new(() =>
                {
                    for (var index = 0; index < 500; index++)
                    {
                        bag.Add($"{name}-{index}");
                    }
                }));
        }

        foreach (var thread in threads)
        {
            thread.Start();
        }

        foreach (var thread in threads)
        {
            thread.Join();
        }

        await Assert.That(bag.Count).IsEqualTo(2000);

        bag.Clear();

        await Assert.That(bag.IsEmpty).IsTrue();
        await Assert.That(bag.Count).IsEqualTo(0);
        await Assert.That(bag.TryTake(out _)).IsFalse();
    }

    [Test]
    public async Task ConcurrentBagClear_NullAndDefaultItems()
    {
        var strings = new ConcurrentBag<string?> {null, "a", null};
        strings.Clear();
        await Assert.That(strings.IsEmpty).IsTrue();

        var ints = new ConcurrentBag<int> {0, 0, 0};
        ints.Clear();
        await Assert.That(ints.IsEmpty).IsTrue();
    }
}