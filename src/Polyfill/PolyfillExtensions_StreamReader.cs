#pragma warning disable

// ReSharper disable RedundantUsingDirective
// ReSharper disable UnusedMember.Global

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
#if TASKSEXTENSIONSREFERENCED && (NETFRAMEWORK || NETSTANDARD2_0 || NETCOREAPP2_0)
    /// <summary>
    /// Asynchronously reads the characters from the current stream into a memory block.
    /// </summary>
    /// <param name="buffer">
    /// When this method returns, contains the specified memory block of characters replaced by the characters read
    /// from the current source.
    /// </param>
    /// <param name="cancellationToken">
    /// The token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None"/>.
    /// </param>
    /// <returns>
    /// A value task that represents the asynchronous read operation. The value of the type parameter of the value task
    /// contains the number of characters that have been read, or 0 if at the end of the stream and no data was read.
    /// The number will be less than or equal to the <paramref name="buffer"/> length, depending on whether the data is
    /// available within the stream.
    /// </returns>
    public static ValueTask<int> ReadAsync(
        this StreamReader target,
        Memory<char> buffer,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (!MemoryMarshal.TryGetArray((ReadOnlyMemory<char>)buffer, out var segment))
        {
            segment = new(buffer.ToArray());
        }

        return new(target.ReadAsync(segment.Array!, segment.Offset, segment.Count));
    }
#endif

#if !NET7_0_OR_GREATER
    /// <summary>
    /// Reads all characters from the current position to the end of the stream asynchronously and returns them as one string.
    /// </summary>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous read operation. The value of the <c>TResult</c> parameter contains
    /// a string with the characters from the current position to the end of the stream.</returns>
    /// <exception cref="ArgumentOutOfRangeException">The number of characters is larger than <see cref="int.MaxValue"/>.</exception>
    /// <exception cref="ObjectDisposedException">The stream reader has been disposed.</exception>
    /// <exception cref="InvalidOperationException">The reader is currently in use by a previous read operation.</exception>
    /// <example>
    /// The following example shows how to read the contents of a file by using the <see cref="ReadToEndAsync(CancellationToken)"/> method.
    /// <code lang="C#">
    /// using CancellationTokenSource tokenSource = new (TimeSpan.FromSeconds(1));
    /// using StreamReader reader = File.OpenText("existingfile.txt");
    ///
    /// Console.WriteLine(await reader.ReadToEndAsync(tokenSource.Token));
    /// </code>
    /// </example>
    /// <remarks>
    /// If this method is canceled via <paramref name="cancellationToken"/>, some data
    /// that has been read from the current <see cref="Stream"/> but not stored (by the
    /// <see cref="StreamReader"/>) or returned (to the caller) may be lost.
    /// </remarks>
    public static Task<string> ReadToEndAsync(
        this StreamReader target,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return target.ReadToEndAsync();
    }
#endif
}