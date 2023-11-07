partial class PolyfillExtensionsTests
{
    [Test, RequiresThread]
    [TestCase(0)]  // poll
    [TestCase(10)] // real timeout
    public void Process_CurrentProcess_WaitAsyncNeverCompletes(int milliseconds)
    {
        using var cancelSource = new CancellationTokenSource(milliseconds);
        var token = cancelSource.Token;
        var process = Process.GetCurrentProcess();

        var ex = Assert.CatchAsync(() => process.WaitForExitAsync(token)) as OperationCanceledException;

        Assert.IsNotNull(ex);
        Assert.AreEqual(token, ex!.CancellationToken);
        Assert.False(process.HasExited);
    }

    [Test, RequiresThread]
    public void Process_WaitForExitAsync_NotDirected_ThrowsInvalidOperationException()
    {
        var process = new Process();
        Assert.ThrowsAsync<InvalidOperationException>(async () => await process.WaitForExitAsync());
    }
}
