
#pragma warning disable

namespace Polyfills;
using System;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using Link = System.ComponentModel.DescriptionAttribute;
using System.Threading;
using System.Threading.Tasks;

static partial class Polyfill
{

    

    /// <summary>
    /// Asynchronously clears all buffers for the current writer and causes any buffered data to
    /// be written to the underlying device.
    /// </summary>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> that represents the asynchronous flush operation.</returns>
    /// <exception cref="ObjectDisposedException">The text writer is disposed.</exception>
    /// <exception cref="InvalidOperationException">The writer is currently in use by a previous write operation.</exception>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.flushasync#system-io-textwriter-flushasync(system-threading-cancellationtoken)")]
    public static Task FlushAsync(this TextWriter target, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return Task.FromCanceled(cancellationToken);
        }

        return target.FlushAsync()
            .WaitAsync(cancellationToken);
    }



}