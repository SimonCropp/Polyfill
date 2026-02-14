namespace Polyfills;

using System;
using System.Diagnostics;
using System.Runtime.Versioning;
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
        if (!target.HasExited)
        {
            cancellationToken.ThrowIfCancellationRequested();
        }

        try
        {
            target.EnableRaisingEvents = true;
        }
        catch (InvalidOperationException)
        {
            if (target.HasExited)
            {
                return;
            }

            throw;
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
}

