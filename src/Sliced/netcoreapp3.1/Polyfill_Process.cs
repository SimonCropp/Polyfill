
#pragma warning disable


namespace Polyfills;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Link = System.ComponentModel.DescriptionAttribute;

static partial class Polyfill
{
    /// <summary>
    /// Instructs the Process component to wait for the associated process to exit, or
    /// for the <paramref name="cancellationToken"/> to be canceled.
    /// </summary>
    /// <remarks>
    /// Calling this method will set <see cref="EnableRaisingEvents"/> to <see langword="true" />.
    /// </remarks>
    /// <returns>
    /// A task that will complete when the process has exited, cancellation has been requested,
    /// or an error occurs.
    /// </returns>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.process.waitforexitasync")]
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

        var tcs = new TaskCompletionSource<object>(TaskCreationOptions.RunContinuationsAsynchronously);

        EventHandler handler = (_, _) => tcs.TrySetResult(null);
        target.Exited += handler;

        try
        {
            if (target.HasExited)
            {
                
            }
            else
            {
                
                using (cancellationToken.UnsafeRegister(static (s, cancellationToken) => ((TaskCompletionSource<object>)s!).TrySetCanceled(cancellationToken), tcs))
                {
                    await tcs.Task.ConfigureAwait(false);
                }
            }
        }
        finally
        {
            target.Exited -= handler;
        }
    }
}
