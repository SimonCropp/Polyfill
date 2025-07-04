// <auto-generated />
#pragma warning disable

#if !NET

namespace Polyfills;

using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

static partial class Polyfill
{
    /// <summary>
    /// Instructs the Process component to wait for the associated process to exit, or
    /// for the <paramref name="cancellationToken"/> to be canceled.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.process.waitforexitasync?view=net-10.0
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
}

#endif
