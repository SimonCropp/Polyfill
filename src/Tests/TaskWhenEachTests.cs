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
}
#endif
