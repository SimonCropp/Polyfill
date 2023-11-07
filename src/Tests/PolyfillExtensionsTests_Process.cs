partial class PolyfillExtensionsTests
{
    [Test, RequiresThread]
    [TestCase(0)]  // poll
    [TestCase(10)] // real timeout
    public void Process_CurrentProcess_WaitAsyncNeverCompletes(int milliseconds)
    {
        using (var cts = new CancellationTokenSource(milliseconds))
        {
            CancellationToken token = cts.Token;
            Process process = Process.GetCurrentProcess();

            OperationCanceledException? ex = Assert.CatchAsync(async () => await process.WaitForExitAsync(token)) as OperationCanceledException;

            Assert.IsNotNull(ex);
            Assert.AreEqual(token, ex!.CancellationToken);
            Assert.False(process.HasExited);
        }
    }

    [Test, RequiresThread]
    public void Process_WaitForExitAsync_NotDirected_ThrowsInvalidOperationException()
    {
        var process = new Process();
        Assert.ThrowsAsync<InvalidOperationException>(async () => await process.WaitForExitAsync());
    }
}
