#pragma warning disable CS4014
partial class PolyfillTests
{
    [Test]
    public async Task TaskCompletionSource_SetCanceled_WithCancellationToken()
    {
        var completionSource = new TaskCompletionSource<int>();
        var tokenSource = new CancellationTokenSource();

        // Simulate some background work that will cancel the task
        Task.Run(async () =>
        {
            await Task.Delay(20); // Simulate a delay
            completionSource.SetCanceled(tokenSource.Token);
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