using System.Linq;
using System.Threading;

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

    [Test]
    public async Task Process_ReadAllText()
    {
        using var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = "--info",
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
            }
        };
        process.Start();
        var (stdout, stderr) = process.ReadAllText();
        await Assert.That(stdout).IsNotNull();
        await Assert.That(stderr).IsNotNull();
        await Assert.That(stdout.Length).IsGreaterThan(0);
    }

    [Test]
    public async Task Process_ReadAllTextAsync()
    {
        using var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = "--info",
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
            }
        };
        process.Start();
        var (stdout, stderr) = await process.ReadAllTextAsync();
        await Assert.That(stdout).IsNotNull();
        await Assert.That(stderr).IsNotNull();
        await Assert.That(stdout.Length).IsGreaterThan(0);
    }

    [Test]
    public async Task Process_ReadAllBytes()
    {
        using var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = "--info",
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
            }
        };
        process.Start();
        var (stdout, stderr) = process.ReadAllBytes();
        await Assert.That(stdout).IsNotNull();
        await Assert.That(stderr).IsNotNull();
        await Assert.That(stdout.Length).IsGreaterThan(0);
    }

    [Test]
    public async Task Process_ReadAllBytesAsync()
    {
        using var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = "--info",
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
            }
        };
        process.Start();
        var (stdout, stderr) = await process.ReadAllBytesAsync();
        await Assert.That(stdout).IsNotNull();
        await Assert.That(stderr).IsNotNull();
        await Assert.That(stdout.Length).IsGreaterThan(0);
    }

    [Test]
    public async Task Process_ReadAllLinesAsync()
    {
        using var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = "--info",
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
            }
        };
        process.Start();
        var lines = new List<ProcessOutputLine>();
        await foreach (var line in process.ReadAllLinesAsync())
        {
            lines.Add(line);
        }
        await Assert.That(lines.Count).IsGreaterThan(0);
        await Assert.That(lines.Any(_ => !_.StandardError)).IsTrue();
    }

    [Test]
    public async Task Process_Run()
    {
        var status = Process.Run("dotnet", new[] { "--info" });
        await Assert.That(status).IsNotNull();
        await Assert.That(status.Canceled).IsFalse();
        await Assert.That(status.ExitCode).IsEqualTo(0);
    }

    [Test]
    public async Task Process_RunAsync()
    {
        var status = await Process.RunAsync("dotnet", new[] { "--info" });
        await Assert.That(status).IsNotNull();
        await Assert.That(status.Canceled).IsFalse();
        await Assert.That(status.ExitCode).IsEqualTo(0);
    }

    [Test]
    public async Task Process_RunAsync_Canceled()
    {
        using var source = new CancellationTokenSource();
        source.Cancel();
        var status = await Process.RunAsync("dotnet", new[] { "--info" }, source.Token);
        await Assert.That(status.Canceled).IsTrue();
    }

    [Test]
    public async Task Process_RunAndCaptureText()
    {
        var output = Process.RunAndCaptureText("dotnet", new[] { "--info" });
        await Assert.That(output).IsNotNull();
        await Assert.That(output.ExitStatus.ExitCode).IsEqualTo(0);
        await Assert.That(output.ExitStatus.Canceled).IsFalse();
        await Assert.That(output.StandardOutput.Length).IsGreaterThan(0);
        await Assert.That(output.ProcessId).IsGreaterThan(0);
    }

    [Test]
    public async Task Process_RunAndCaptureTextAsync()
    {
        var output = await Process.RunAndCaptureTextAsync("dotnet", new[] { "--info" });
        await Assert.That(output).IsNotNull();
        await Assert.That(output.ExitStatus.ExitCode).IsEqualTo(0);
        await Assert.That(output.StandardOutput.Length).IsGreaterThan(0);
    }

    [Test]
    public async Task Process_StartAndForget()
    {
        var pid = Process.StartAndForget(
            new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = "--info",
                UseShellExecute = false,
                CreateNoWindow = true,
            });
        await Assert.That(pid).IsGreaterThan(0);
    }

    [Test]
    public async Task ProcessOutputLine_Construction()
    {
        var line = new ProcessOutputLine("content", standardError: true);
        await Assert.That(line.Content).IsEqualTo("content");
        await Assert.That(line.StandardError).IsTrue();
    }

    [Test]
    public async Task ProcessExitStatus_Construction()
    {
        var status = new ProcessExitStatus(42, canceled: true);
        await Assert.That(status.ExitCode).IsEqualTo(42);
        await Assert.That(status.Canceled).IsTrue();
        await Assert.That(status.Signal).IsNull();
    }

    [Test]
    public async Task ProcessTextOutput_Construction()
    {
        var exit = new ProcessExitStatus(0, canceled: false);
        var output = new ProcessTextOutput(exit, "out", "err", 1234);
        await Assert.That(output.ExitStatus).IsSameReferenceAs(exit);
        await Assert.That(output.StandardOutput).IsEqualTo("out");
        await Assert.That(output.StandardError).IsEqualTo("err");
        await Assert.That(output.ProcessId).IsEqualTo(1234);
    }
}
