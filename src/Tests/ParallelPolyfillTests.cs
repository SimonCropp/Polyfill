using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

public class ParallelPolyfillTests
{
    [Test]
    public async Task ForEachAsync_ProcessesAllItems()
    {
        var items = new[] { 1, 2, 3, 4, 5 };
        var processed = new ConcurrentBag<int>();

        await Parallel.ForEachAsync(
            items,
            (item, _) =>
            {
                processed.Add(item);
                return default;
            });

        await Assert.That(processed.Count).IsEqualTo(5);
    }

    [Test]
    public async Task ForEachAsync_WithCancellationToken()
    {
        var items = new[] { 1, 2, 3 };
        var processed = new ConcurrentBag<int>();

        await Parallel.ForEachAsync(
            items,
            CancellationToken.None,
            (item, _) =>
            {
                processed.Add(item);
                return default;
            });

        await Assert.That(processed.Count).IsEqualTo(3);
    }

    [Test]
    public async Task ForEachAsync_WithParallelOptions()
    {
        var items = new[] { 1, 2, 3 };
        var processed = new ConcurrentBag<int>();

        await Parallel.ForEachAsync(
            items,
            new ParallelOptions { MaxDegreeOfParallelism = 2 },
            (item, _) =>
            {
                processed.Add(item);
                return default;
            });

        await Assert.That(processed.Count).IsEqualTo(3);
    }

#if FeatureAsyncInterfaces

    static async IAsyncEnumerable<int> GetAsyncItems()
    {
        yield return 1;
        await Task.Yield();
        yield return 2;
        yield return 3;
    }

    [Test]
    public async Task ForEachAsync_AsyncEnumerable()
    {
        var processed = new ConcurrentBag<int>();

        await Parallel.ForEachAsync(
            GetAsyncItems(),
            (item, _) =>
            {
                processed.Add(item);
                return default;
            });

        await Assert.That(processed.Count).IsEqualTo(3);
    }

    [Test]
    public async Task ForEachAsync_AsyncEnumerable_WithParallelOptions()
    {
        var processed = new ConcurrentBag<int>();

        await Parallel.ForEachAsync(
            GetAsyncItems(),
            new ParallelOptions { MaxDegreeOfParallelism = 1 },
            (item, _) =>
            {
                processed.Add(item);
                return default;
            });

        await Assert.That(processed.Count).IsEqualTo(3);
    }

    [Test]
    public async Task ForEachAsync_AsyncEnumerable_WithCancellationToken()
    {
        var processed = new ConcurrentBag<int>();

        await Parallel.ForEachAsync(
            GetAsyncItems(),
            CancellationToken.None,
            (item, _) =>
            {
                processed.Add(item);
                return default;
            });

        await Assert.That(processed.Count).IsEqualTo(3);
    }

#endif
}
