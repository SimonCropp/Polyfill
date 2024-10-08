
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


    /// <summary>
    /// Equivalent to Write(stringBuilder.ToString()) however it uses the
    /// StringBuilder.GetChunks() method to avoid creating the intermediate string
    /// </summary>
    /// <param name="value">The string (as a StringBuilder) to write to the stream</param>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.write#system-io-textwriter-write(system-text-stringbuilder)")]
    public static void Write(this TextWriter target, StringBuilder? value)
    {
        if (value == null)
        {
            return;
        }

        target.Write(value.ToString());
    }

    /// <summary>
    /// Equivalent to WriteAsync(stringBuilder.ToString()) however it uses the
    /// StringBuilder.GetChunks() method to avoid creating the intermediate string
    /// </summary>
    /// <param name="value">The string (as a StringBuilder) to write to the stream</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.writeasync#system-io-textwriter-writeasync(system-readonlymemory((system-char))-system-threading-cancellationtoken)")]
    public static Task WriteAsync(this TextWriter target, StringBuilder? value, CancellationToken cancellationToken = default)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return Task.FromCanceled(cancellationToken);
        }

        if (value == null)
        {
            return Task.CompletedTask;
        }

        return WriteAsyncCore(value, cancellationToken);

        async Task WriteAsyncCore(StringBuilder builder, CancellationToken cancel)
        {
            await target.WriteAsync(builder.ToString())
                .WaitAsync(cancellationToken);
        }
    }

}