#pragma warning disable CS4014
partial class PolyfillTests
{
    [Test]
    public async Task TaskCompletionSource_TrySetCanceled_PropagatesToken()
    {
        using var source = new CancellationTokenSource();
        source.Cancel();
        var token = source.Token;

        var completionSource = new TaskCompletionSource();
        var result = completionSource.TrySetCanceled(token);

        await Assert.That(result).IsTrue();
        await Assert.That(completionSource.Task.IsCanceled).IsTrue();

        CancellationToken observed = default;
        try
        {
            await completionSource.Task;
        }
        catch (OperationCanceledException exception)
        {
            observed = exception.CancellationToken;
        }

        await Assert.That(observed).IsEqualTo(token);
    }

    [Test]
    public async Task TaskCompletionSource()
    {
        var completionSource = new TaskCompletionSource();

        // Simulate some background work
        Task.Run(async () =>
        {
            await Task.Delay(10); // Simulate a delay
            completionSource.SetResult();
        });

        // Await the task
        await completionSource.Task;
        Console.WriteLine("Task completed successfully");
    }

    [Test]
    public async Task TaskCompletionSource_SetCanceled_WithCancellationToken()
    {
        var completionSource = new TaskCompletionSource<int>();
        var cancelSource = new CancelSource();

        // Simulate some background work that will cancel the task
        Task.Run(async () =>
        {
            await Task.Delay(20); // Simulate a delay
            completionSource.SetCanceled(cancelSource.Token);
        });

        try
        {
            // Await the task
            var result = await completionSource.Task;
            Console.WriteLine($"Task completed with result: {result}");
        }
        catch (TaskCanceledException)
        {
            Console.WriteLine("Task was canceled.");
        }
    }

    [Test]
    public async Task TaskCompletionSource_SetFromTask_RanToCompletion()
    {
        var completionSource = new TaskCompletionSource();
        completionSource.SetFromTask(Task.FromResult(42));

        await completionSource.Task;
        await Assert.That(completionSource.Task.Status).IsEqualTo(TaskStatus.RanToCompletion);

        var generic = new TaskCompletionSource<string>();
        generic.SetFromTask(Task.FromResult("value"));

        await Assert.That(await generic.Task).IsEqualTo("value");
    }

    [Test]
    public async Task TaskCompletionSource_SetFromTask_Faulted()
    {
        var source = new TaskCompletionSource<int>();
        var first = new InvalidOperationException("first");
        var second = new FormatException("second");
        source.SetException(new Exception[] { first, second });

        var target = new TaskCompletionSource<int>();
        target.SetFromTask(source.Task);

        var exception = target.Task.Exception!;
        await Assert.That(exception.InnerExceptions.Count).IsEqualTo(2);
        // the original instances are carried over, not wrapped or flattened
        await Assert.That(exception.InnerExceptions[0]).IsSameReferenceAs(first);
        await Assert.That(exception.InnerExceptions[1]).IsSameReferenceAs(second);
    }

    [Test]
    public async Task TaskCompletionSource_SetFromTask_Canceled_PropagatesToken()
    {
        using var cancelSource = new CancellationTokenSource();
        cancelSource.Cancel();
        var token = cancelSource.Token;

        var source = new TaskCompletionSource<int>();
        source.TrySetCanceled(token);

        var target = new TaskCompletionSource<int>();
        target.SetFromTask(source.Task);

        await Assert.That(target.Task.Status).IsEqualTo(TaskStatus.Canceled);

        CancellationToken observed = default;
        try
        {
            await target.Task;
        }
        catch (OperationCanceledException exception)
        {
            observed = exception.CancellationToken;
        }

        await Assert.That(observed).IsEqualTo(token);
    }

    [Test]
    public async Task TaskCompletionSource_SetFromTask_CanceledWithNoToken()
    {
        var source = new TaskCompletionSource<int>();
        source.TrySetCanceled();

        var target = new TaskCompletionSource<int>();
        await Assert.That(target.TrySetFromTask(source.Task)).IsTrue();

        CancellationToken observed = default;
        try
        {
            await target.Task;
        }
        catch (OperationCanceledException exception)
        {
            observed = exception.CancellationToken;
        }

        await Assert.That(observed.CanBeCanceled).IsFalse();
    }

    [Test]
    public async Task TaskCompletionSource_SetFromTask_Guards()
    {
        var completionSource = new TaskCompletionSource();
        await Assert.That(() => completionSource.SetFromTask(null!)).Throws<ArgumentNullException>();
        await Assert.That(() => completionSource.TrySetFromTask(null!)).Throws<ArgumentNullException>();

        var generic = new TaskCompletionSource<int>();
        await Assert.That(() => generic.SetFromTask(null!)).Throws<ArgumentNullException>();
        await Assert.That(() => generic.TrySetFromTask(null!)).Throws<ArgumentNullException>();

        // an incomplete task is rejected by TrySetFromTask too, rather than returning false
        var pending = new TaskCompletionSource<int>();
        await Assert.That(() => completionSource.SetFromTask(pending.Task)).Throws<ArgumentException>();
        await Assert.That(() => completionSource.TrySetFromTask(pending.Task)).Throws<ArgumentException>();
        await Assert.That(() => generic.SetFromTask(pending.Task)).Throws<ArgumentException>();
        await Assert.That(() => generic.TrySetFromTask(pending.Task)).Throws<ArgumentException>();
    }

    [Test]
    public async Task TaskCompletionSource_SetFromTask_AlreadyCompleted()
    {
        var completionSource = new TaskCompletionSource();
        completionSource.SetResult();

        await Assert.That(() => completionSource.SetFromTask(Task.CompletedTask)).Throws<InvalidOperationException>();
        await Assert.That(completionSource.TrySetFromTask(Task.CompletedTask)).IsFalse();

        var generic = new TaskCompletionSource<int>();
        generic.SetResult(1);

        await Assert.That(() => generic.SetFromTask(Task.FromResult(2))).Throws<InvalidOperationException>();
        await Assert.That(generic.TrySetFromTask(Task.FromResult(2))).IsFalse();
        await Assert.That(await generic.Task).IsEqualTo(1);
    }
}
