#if FeatureAsyncInterfaces
public class TaskWhenEachTests
{
    [Test]
    public async Task WhenEach_ReturnsTasksAsTheyComplete()
    {
        var tcs1 = new TaskCompletionSource<int>();
        var tcs2 = new TaskCompletionSource<int>();
        var tcs3 = new TaskCompletionSource<int>();

        var tasks = new[] { tcs1.Task, tcs2.Task, tcs3.Task };

        tcs2.SetResult(2);
        tcs1.SetResult(1);
        tcs3.SetResult(3);

        var results = new List<int>();
        await foreach (var task in Task.WhenEach(tasks))
        {
            results.Add(await task);
        }

        await Assert.That(results.Count).IsEqualTo(3);
        await Assert.That(results).Contains(1);
        await Assert.That(results).Contains(2);
        await Assert.That(results).Contains(3);
    }

    [Test]
    public async Task WhenEach_NonGeneric_ReturnsTasksAsTheyComplete()
    {
        var tcs1 = new TaskCompletionSource<bool>();
        var tcs2 = new TaskCompletionSource<bool>();

        Task[] tasks = [tcs1.Task, tcs2.Task];

        tcs2.SetResult(true);
        tcs1.SetResult(true);

        var count = 0;
        await foreach (var task in Task.WhenEach((IEnumerable<Task>)tasks))
        {
            await Assert.That(task.IsCompleted).IsTrue();
            count++;
        }

        await Assert.That(count).IsEqualTo(2);
    }

    [Test]
    public async Task WhenEach_EmptyCollection_YieldsNothing()
    {
        var count = 0;
        await foreach (var _ in Task.WhenEach(Array.Empty<Task>()))
        {
            count++;
        }

        await Assert.That(count).IsEqualTo(0);
    }

    [Test]
    public async Task WhenEach_ThrowsOnNull()
    {
        await Assert.That(async () =>
        {
            await foreach (var _ in Task.WhenEach((IEnumerable<Task>)null!))
            {
            }
        }).Throws<ArgumentNullException>();
    }

    [Test]
    public async Task WhenEach_YieldsInCompletionOrder()
    {
        var tcs1 = new TaskCompletionSource<int>();
        var tcs2 = new TaskCompletionSource<int>();
        var tcs3 = new TaskCompletionSource<int>();

        var enumerator = Task.WhenEach(new[] {tcs1.Task, tcs2.Task, tcs3.Task}).GetAsyncEnumerator();

        tcs2.SetResult(2);
        await Assert.That(await enumerator.MoveNextAsync()).IsTrue();
        await Assert.That(enumerator.Current == tcs2.Task).IsTrue();

        tcs3.SetResult(3);
        await Assert.That(await enumerator.MoveNextAsync()).IsTrue();
        await Assert.That(enumerator.Current == tcs3.Task).IsTrue();

        tcs1.SetResult(1);
        await Assert.That(await enumerator.MoveNextAsync()).IsTrue();
        await Assert.That(enumerator.Current == tcs1.Task).IsTrue();

        await Assert.That(await enumerator.MoveNextAsync()).IsFalse();
        await enumerator.DisposeAsync();
    }

    [Test]
    public async Task WhenEach_WithCancellation_CancelsWhileTasksPending()
    {
        var pending = new TaskCompletionSource<int>();
        using var cancelSource = new System.Threading.CancellationTokenSource();

        var enumerate = Task.Run(async () =>
        {
            await foreach (var _ in Task.WhenEach(new[] {pending.Task}).WithCancellation(cancelSource.Token))
            {
            }
        });

        cancelSource.CancelAfter(100);
        var finished = await Task.WhenAny(enumerate, Task.Delay(TimeSpan.FromSeconds(10)));
        pending.SetResult(0);

        await Assert.That(finished == enumerate).IsTrue();
        await Assert.That(enumerate.IsCanceled).IsTrue();
    }

    [Test]
    public async Task WhenEach_ManyTasks()
    {
        const int count = 20000;
        var sources = new TaskCompletionSource<int>[count];
        var tasks = new Task<int>[count];
        for (var index = 0; index < count; index++)
        {
            sources[index] = new TaskCompletionSource<int>();
            tasks[index] = sources[index].Task;
        }

        for (var index = count - 1; index >= 0; index--)
        {
            sources[index].SetResult(index);
        }

        long sum = 0;
        await foreach (var task in Task.WhenEach(tasks))
        {
            sum += await task;
        }

        await Assert.That(sum).IsEqualTo((long) count * (count - 1) / 2);
    }
}
#endif
