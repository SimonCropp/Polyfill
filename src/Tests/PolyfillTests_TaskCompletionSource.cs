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
}