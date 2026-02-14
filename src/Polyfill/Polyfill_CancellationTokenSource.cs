#if !NET8_0_OR_GREATER

namespace Polyfills;

using System.Threading;
using System.Threading.Tasks;

static partial class Polyfill
{
    /// <summary>Communicates a request for cancellation asynchronously.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtokensource.cancelasync?view=net-11.0
    public static Task CancelAsync(this CancellationTokenSource target)
    {
        if (target.IsCancellationRequested)
        {
            return Task.CompletedTask;
        }

        var task = Task.Run(target.Cancel);

        while (!target.IsCancellationRequested) ;

        return task;
    }
}
#endif
