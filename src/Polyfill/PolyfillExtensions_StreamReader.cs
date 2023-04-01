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
        // StreamReader doesn't accept cancellation token (pre-netstd2.1)
        cancellationToken.ThrowIfCancellationRequested();

        if (!MemoryMarshal.TryGetArray((ReadOnlyMemory<char>)buffer, out var segment))
        {
            segment = new(buffer.ToArray());
        }

        return new(target.ReadAsync(segment.Array!, segment.Offset, segment.Count));
    }
}
#endif