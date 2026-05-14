namespace Polyfills;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

static partial class Polyfill
{
#if NETFRAMEWORK || NETSTANDARD || NETCOREAPP2X || WINDOWS_UWP

    /// <summary>
    /// Immediately stops the associated process, and optionally its child/descendent processes.
    /// Maps to <see cref="Process.Kill"/>.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.process.kill?view=net-11.0#system-diagnostics-process-kill(system-boolean)
    [SupportedOSPlatform("maccatalyst")]
    [UnsupportedOSPlatform("ios")]
    [UnsupportedOSPlatform("tvos")]
    public static void Kill(this Process target, bool entireProcessTree) =>
        target.Kill();

#endif

#if !NET

    /// <summary>
    /// Instructs the Process component to wait for the associated process to exit, or
    /// for the <paramref name="cancellationToken"/> to be canceled.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.process.waitforexitasync?view=net-11.0
    public static async Task WaitForExitAsync(this Process target, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (target.HasExited)
        {
            return;
        }

        try
        {
            target.EnableRaisingEvents = true;
        }
        catch (InvalidOperationException) when (target.HasExited)
        {
            return;
        }

        var tcs = new TaskCompletionSource<object?>(TaskCreationOptions.RunContinuationsAsynchronously);

        EventHandler handler = (_, _) => tcs.TrySetResult(null);
        target.Exited += handler;

        try
        {
            if (!target.HasExited)
            {
                using (cancellationToken.UnsafeRegister(static (s, cancellationToken) => ((TaskCompletionSource<object>) s!).TrySetCanceled(cancellationToken), tcs))
                {
                    await tcs.Task;
                }
            }
        }
        finally
        {
            target.Exited -= handler;
        }
    }
#endif

#if !NET11_0_OR_GREATER

#if FeatureValueTuple
    /// <summary>
    /// Reads the standard output and standard error of the process as text, waiting for the process to exit.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.process.readalltext?view=net-11.0
    public static (string StandardOutput, string StandardError) ReadAllText(this Process target, TimeSpan? timeout = default)
    {
        var stdoutTask = target.StandardOutput.ReadToEndAsync();
        var stderrTask = target.StandardError.ReadToEndAsync();
        WaitForExitOrThrow(target, timeout);
        return (stdoutTask.GetAwaiter().GetResult(), stderrTask.GetAwaiter().GetResult());
    }

    /// <summary>
    /// Asynchronously reads the standard output and standard error of the process as text, waiting for the process to exit.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.process.readalltextasync?view=net-11.0
    public static async Task<(string StandardOutput, string StandardError)> ReadAllTextAsync(this Process target, CancellationToken cancellationToken = default)
    {
        var stdoutTask = target.StandardOutput.ReadToEndAsync();
        var stderrTask = target.StandardError.ReadToEndAsync();
        await target.WaitForExitAsync(cancellationToken);
        return (await stdoutTask, await stderrTask);
    }

    /// <summary>
    /// Reads the standard output and standard error of the process as bytes, waiting for the process to exit.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.process.readallbytes?view=net-11.0
    public static (byte[] StandardOutput, byte[] StandardError) ReadAllBytes(this Process target, TimeSpan? timeout = default)
    {
        var outMs = new MemoryStream();
        var errMs = new MemoryStream();
        var outTask = target.StandardOutput.BaseStream.CopyToAsync(outMs);
        var errTask = target.StandardError.BaseStream.CopyToAsync(errMs);
        WaitForExitOrThrow(target, timeout);
        Task.WaitAll(outTask, errTask);
        return (outMs.ToArray(), errMs.ToArray());
    }

    /// <summary>
    /// Asynchronously reads the standard output and standard error of the process as bytes, waiting for the process to exit.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.process.readallbytesasync?view=net-11.0
    public static async Task<(byte[] StandardOutput, byte[] StandardError)> ReadAllBytesAsync(this Process target, CancellationToken cancellationToken = default)
    {
        var outMs = new MemoryStream();
        var errMs = new MemoryStream();
        var outTask = target.StandardOutput.BaseStream.CopyToAsync(outMs, 81920, cancellationToken);
        var errTask = target.StandardError.BaseStream.CopyToAsync(errMs, 81920, cancellationToken);
        await target.WaitForExitAsync(cancellationToken);
        await Task.WhenAll(outTask, errTask);
        return (outMs.ToArray(), errMs.ToArray());
    }
#endif

#if FeatureAsyncInterfaces
    /// <summary>
    /// Asynchronously reads the standard output and standard error of the process line-by-line, waiting for the process to exit.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.process.readalllinesasync?view=net-11.0
    public static async IAsyncEnumerable<ProcessOutputLine> ReadAllLinesAsync(
        this Process target,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var queue = new System.Collections.Concurrent.ConcurrentQueue<ProcessOutputLine>();
        var signal = new SemaphoreSlim(0);
        var sync = new object();
        var stdoutDone = false;
        var stderrDone = false;

        DataReceivedEventHandler outHandler = (_, e) =>
        {
            if (e.Data is null)
            {
                lock (sync) stdoutDone = true;
            }
            else
            {
                queue.Enqueue(new(e.Data, false));
            }
            signal.Release();
        };
        DataReceivedEventHandler errHandler = (_, e) =>
        {
            if (e.Data is null)
            {
                lock (sync) stderrDone = true;
            }
            else
            {
                queue.Enqueue(new(e.Data, true));
            }
            signal.Release();
        };

        target.OutputDataReceived += outHandler;
        target.ErrorDataReceived += errHandler;
        target.BeginOutputReadLine();
        target.BeginErrorReadLine();

        try
        {
            while (true)
            {
                await signal.WaitAsync(cancellationToken);
                while (queue.TryDequeue(out var line))
                {
                    yield return line;
                }
                bool done;
                lock (sync)
                {
                    done = stdoutDone && stderrDone;
                }
                if (done)
                {
                    while (queue.TryDequeue(out var line))
                    {
                        yield return line;
                    }
                    yield break;
                }
            }
        }
        finally
        {
            target.OutputDataReceived -= outHandler;
            target.ErrorDataReceived -= errHandler;
            signal.Dispose();
        }
    }
#endif

    static void WaitForExitOrThrow(Process target, TimeSpan? timeout)
    {
        if (timeout is { } value)
        {
            var ms = (long)value.TotalMilliseconds;
            if (ms < 0 || ms > int.MaxValue)
            {
                target.WaitForExit();
                return;
            }
            if (!target.WaitForExit((int)ms))
            {
                throw new TimeoutException("The process did not exit within the specified timeout.");
            }
        }
        else
        {
            target.WaitForExit();
        }
    }
#endif
}
