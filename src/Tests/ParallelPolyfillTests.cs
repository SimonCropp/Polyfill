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

    static IEnumerable<int> CountedItems(int count, StrongBox<int> pulled)
    {
        for (var item = 1; item <= count; item++)
        {
            Interlocked.Increment(ref pulled.Value);
            yield return item;
        }
    }

    [Test]
    public async Task ForEachAsync_PullsItemsLazily()
    {
        var pulled = new StrongBox<int>();
        var processed = 0;
        var started = new SemaphoreSlim(0);
        var release = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);

        var task = Parallel.ForEachAsync(
            CountedItems(100, pulled),
            new ParallelOptions {MaxDegreeOfParallelism = 2},
            async (_, _) =>
            {
                started.Release();
                await release.Task;
                Interlocked.Increment(ref processed);
            });

        await Assert.That(await started.WaitAsync(TimeSpan.FromSeconds(10))).IsTrue();
        await Assert.That(await started.WaitAsync(TimeSpan.FromSeconds(10))).IsTrue();
        await Task.Delay(200);

        // only the items being processed have been taken from the source
        var pulledWhileBlocked = Volatile.Read(ref pulled.Value);
        release.SetResult(true);
        await task;

        await Assert.That(pulledWhileBlocked).IsEqualTo(2);
        await Assert.That(pulled.Value).IsEqualTo(100);
        await Assert.That(processed).IsEqualTo(100);
    }

    [Test]
    public async Task ForEachAsync_StopsAfterFirstFailure()
    {
        var pulled = new StrongBox<int>();
        var processed = 0;

        var task = Parallel.ForEachAsync(
            CountedItems(100, pulled),
            new ParallelOptions {MaxDegreeOfParallelism = 1},
            (item, _) =>
            {
                Interlocked.Increment(ref processed);
                if (item == 3)
                {
                    throw new InvalidOperationException("item 3");
                }

                return default;
            });

        await Assert.That(async () => await task).Throws<InvalidOperationException>();
        await Assert.That(pulled.Value).IsEqualTo(3);
        await Assert.That(processed).IsEqualTo(3);
    }

    [Test]
    public async Task ForEachAsync_FailureCancelsTokenPassedToBodies()
    {
        var waiting = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);

        var task = Parallel.ForEachAsync(
            new[] {1, 2},
            new ParallelOptions {MaxDegreeOfParallelism = 2},
            async (item, token) =>
            {
                if (item == 1)
                {
                    waiting.SetResult(true);
                    await Task.Delay(Timeout.Infinite, token);
                    return;
                }

                await waiting.Task;
                throw new InvalidOperationException("item 2");
            });

        var finished = await Task.WhenAny(task, Task.Delay(TimeSpan.FromSeconds(10)));

        await Assert.That(finished == task).IsTrue();
        await Assert.That(task.IsFaulted).IsTrue();
        await Assert.That(task.Exception!.InnerExceptions[0]).IsTypeOf<InvalidOperationException>();
    }

    [Test]
    public async Task ForEachAsync_ExternalCancellation()
    {
        using var cancelSource = new CancellationTokenSource();
        var waiting = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);

        var task = Parallel.ForEachAsync(
            new[] {1, 2, 3},
            new ParallelOptions {MaxDegreeOfParallelism = 1, CancellationToken = cancelSource.Token},
            async (_, token) =>
            {
                waiting.TrySetResult(true);
                await Task.Delay(Timeout.Infinite, token);
            });

        await waiting.Task;
        cancelSource.Cancel();
        var finished = await Task.WhenAny(task, Task.Delay(TimeSpan.FromSeconds(10)));

        await Assert.That(finished == task).IsTrue();
        await Assert.That(task.IsCanceled).IsTrue();
    }

    [Test]
    public async Task ForEachAsync_AlreadyCanceled()
    {
        var processed = 0;
        var task = Parallel.ForEachAsync(
            new[] {1, 2, 3},
            new CancellationToken(true),
            (_, _) =>
            {
                Interlocked.Increment(ref processed);
                return default;
            });

        var finished = await Task.WhenAny(task, Task.Delay(TimeSpan.FromSeconds(10)));

        await Assert.That(finished == task).IsTrue();
        await Assert.That(task.IsCanceled).IsTrue();
        await Assert.That(processed).IsEqualTo(0);
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

    static async IAsyncEnumerable<int> CountedAsyncItems(int count, StrongBox<int> pulled)
    {
        for (var item = 1; item <= count; item++)
        {
            await Task.Yield();
            Interlocked.Increment(ref pulled.Value);
            yield return item;
        }
    }

    [Test]
    public async Task ForEachAsync_AsyncEnumerable_PullsItemsLazily()
    {
        var pulled = new StrongBox<int>();
        var processed = 0;
        var started = new SemaphoreSlim(0);
        var release = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);

        var task = Parallel.ForEachAsync(
            CountedAsyncItems(100, pulled),
            new ParallelOptions {MaxDegreeOfParallelism = 2},
            async (_, _) =>
            {
                started.Release();
                await release.Task;
                Interlocked.Increment(ref processed);
            });

        await Assert.That(await started.WaitAsync(TimeSpan.FromSeconds(10))).IsTrue();
        await Assert.That(await started.WaitAsync(TimeSpan.FromSeconds(10))).IsTrue();
        await Task.Delay(200);

        // only the items being processed have been taken from the source
        var pulledWhileBlocked = Volatile.Read(ref pulled.Value);
        release.SetResult(true);
        await task;

        await Assert.That(pulledWhileBlocked).IsEqualTo(2);
        await Assert.That(pulled.Value).IsEqualTo(100);
        await Assert.That(processed).IsEqualTo(100);
    }

    [Test]
    public async Task ForEachAsync_AsyncEnumerable_StopsAfterFirstFailure()
    {
        var pulled = new StrongBox<int>();
        var processed = 0;

        var task = Parallel.ForEachAsync(
            CountedAsyncItems(100, pulled),
            new ParallelOptions {MaxDegreeOfParallelism = 1},
            (item, _) =>
            {
                Interlocked.Increment(ref processed);
                if (item == 3)
                {
                    throw new InvalidOperationException("item 3");
                }

                return default;
            });

        await Assert.That(async () => await task).Throws<InvalidOperationException>();
        await Assert.That(pulled.Value).IsEqualTo(3);
        await Assert.That(processed).IsEqualTo(3);
    }

#endif
}