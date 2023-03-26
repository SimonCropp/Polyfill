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
    /// Asynchronously writes a character memory region to the stream.
    /// </summary>
    /// <param name="buffer">The character memory region to write to the stream.</param>
    /// <param name="cancellationToken">
    /// The token to monitor for cancellation requests.
    /// The default value is <see cref="Cancellation.None"/>.
    /// </param>
    /// <returns>A task that represents the asynchronous write operation.</returns>
    public static ValueTask WriteAsync(
        this StreamWriter target,
        ReadOnlyMemory<char> buffer,
        CancellationToken cancellationToken = default)
    {
        // StreamReader doesn't accept cancellation token (pre-netstd2.1)
        cancellationToken.ThrowIfCancellationRequested();

        if (!MemoryMarshal.TryGetArray(buffer, out var segment))
        {
            segment = new(buffer.ToArray());
        }

        return new(target.WriteAsync(segment.Array!, segment.Offset, segment.Count));
    }
}
#endif