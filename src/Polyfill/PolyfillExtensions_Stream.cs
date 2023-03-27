#if TASKSEXTENSIONSREFERENCED && (NETFRAMEWORK || NETSTANDARD2_0 || NETCOREAPP2_0)

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
    /// <summary>
    /// Asynchronously reads a sequence of bytes from the current stream, advances the position within the stream by
    /// the number of bytes read, and monitors cancellation requests.
    /// </summary>
    /// <param name="buffer">The region of memory to write the data into.</param>
    /// <param name="cancellationToken">
    /// The token to monitor for cancellation requests. The default value is None.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous read operation. The value of its Result property contains the
    /// total number of bytes read into the buffer. The result value can be less than the number of bytes allocated in
    /// the buffer if that many bytes are not currently available, or it can be 0 (zero) if the end of the stream has
    /// been reached.
    /// </returns>
    public static ValueTask<int> ReadAsync(
        this Stream target,
        Memory<byte> buffer,
        CancellationToken cancellationToken = default)
    {
        if (!MemoryMarshal.TryGetArray((ReadOnlyMemory<byte>) buffer, out var segment))
        {
            segment = new(buffer.ToArray());
        }

        return new(target.ReadAsync(segment.Array!, segment.Offset, segment.Count, cancellationToken));
    }

    /// <summary>
    /// Asynchronously writes a sequence of bytes to the current stream, advances the current position
    /// within this stream by the number of bytes written, and monitors cancellation requests.
    /// </summary>
    /// <param name="buffer">The region of memory to write data from.</param>
    /// <param name="cancellationToken">
    /// The token to monitor for cancellation requests. The default value is None.
    /// </param>
    /// <returns>A task that represents the asynchronous write operation.</returns>
    public static ValueTask WriteAsync(
        this Stream target,
        ReadOnlyMemory<byte> buffer,
        CancellationToken cancellationToken = default)
    {
        if (!MemoryMarshal.TryGetArray(buffer, out var segment))
        {
            segment = new(buffer.ToArray());
        }

        return new(target.WriteAsync(segment.Array!, segment.Offset, segment.Count, cancellationToken));
    }

    /// <summary>
    /// Asynchronously reads the bytes from the current stream and writes them to another stream, using a specified
    /// cancellation token. Both streams positions are advanced by the number of bytes copied.
    /// </summary>
    /// <param name="destination">The stream to which the contents of the current stream will be copied.</param>
    /// <param name="cancellationToken">
    /// The token to monitor for cancellation requests. The default value is None.
    /// </param>
    /// <returns>A task that represents the asynchronous copy operation.</returns>
    public static Task CopyToAsync(
        this Stream target,
        Stream destination,
        CancellationToken cancellationToken = default) =>
        target.CopyToAsync(destination, 81920, cancellationToken);
}
#endif