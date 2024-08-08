#pragma warning disable CS4014
partial class PolyfillTests
{
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
}