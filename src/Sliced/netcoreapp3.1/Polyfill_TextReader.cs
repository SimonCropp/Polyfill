
#pragma warning disable

namespace Polyfills;
using System;
using System.IO;
using System.Runtime.InteropServices;
using Link = System.ComponentModel.DescriptionAttribute;
using System.Threading;
using System.Threading.Tasks;

static partial class Polyfill
{

    /// <summary>
    /// Reads all characters from the current position to the end of the stream asynchronously and returns them as one string.
    /// </summary>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous read operation. The value of the <c>TResult</c> parameter contains
    /// a string with the characters from the current position to the end of the stream.</returns>
    /// <exception cref="ArgumentOutOfRangeException">The number of characters is larger than <see cref="int.MaxValue"/>.</exception>
    /// <exception cref="ObjectDisposedException">The stream reader has been disposed.</exception>
    /// <exception cref="InvalidOperationException">The reader is currently in use by a previous read operation.</exception>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.io.textreader.readtoendasync#system-io-textreader-readtoendasync(system-threading-cancellationtoken)")]
    public static Task<string> ReadToEndAsync(
        this TextReader target,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return target.ReadToEndAsync()
            .WaitAsync(cancellationToken);
    }

    /// <summary>
    /// Reads a line of characters asynchronously and returns the data as a string.
    /// </summary>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A value task that represents the asynchronous read operation. The value of the <c>TResult</c>
    /// parameter contains the next line from the text reader, or is <see langword="null" /> if all of the characters have been read.</returns>
    /// <exception cref="ArgumentOutOfRangeException">The number of characters in the next line is larger than <see cref="int.MaxValue"/>.</exception>
    /// <exception cref="ObjectDisposedException">The text reader has been disposed.</exception>
    /// <exception cref="InvalidOperationException">The reader is currently in use by a previous read operation.</exception>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.io.textreader.readtoendasync#system-io-textreader-readlineasync(system-threading-cancellationtoken)")]
    public static Task<string> ReadLineAsync(
        this TextReader target,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return target.ReadLineAsync()
            .WaitAsync(cancellationToken);
    }
}