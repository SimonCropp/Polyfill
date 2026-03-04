partial class PolyfillTests
{
    [Test]
    [Arguments(0)]  // poll
    [Arguments(10)] // real timeout
    public async Task Process_CurrentProcess_WaitAsyncNeverCompletes(int milliseconds)
    {
        using var cancelSource = new CancelSource(milliseconds);
        var cancel = cancelSource.Token;
        var process = Process.GetCurrentProcess();

        var ex = await Assert.That(async () => await process.WaitForExitAsync(cancel)).Throws<OperationCanceledException>();

        await Assert.That(ex).IsNotNull();
        await Assert.That(ex!.CancellationToken).IsEqualTo(cancel);
        await Assert.That(process.HasExited).IsFalse();
    }

    [Test]
    public async Task Process_WaitForExitAsync_NotDirected_ThrowsInvalidOperationException()
    {
        var process = new Process();
        await Assert.That(async () => await process.WaitForExitAsync()).Throws<InvalidOperationException>();
    }

    [Test]
    public async Task Process_Kill_EntireProcessTree()
    {
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = "--list-runtimes",
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
            }
        };
        process.Start();
        process.Kill(entireProcessTree: true);
        await process.WaitForExitAsync();
        await Assert.That(process.HasExited).IsTrue();
    }
}
