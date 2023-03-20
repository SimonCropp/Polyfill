#if TASKSEXTENSIONSREFERENCED && (NET4X || NETSTANDARD2_0 || NETCOREAPP2_0)

// ReSharper disable RedundantUsingDirective
// ReSharper disable UnusedMember.Global

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static ValueTask<int> ReadAsync(
        this Stream stream,
        Memory<byte> buffer,
        CancellationToken cancellationToken = default)
    {
        if (!MemoryMarshal.TryGetArray((ReadOnlyMemory<byte>) buffer, out var segment))
        {
            segment = new(buffer.ToArray());
        }

        return new(stream.ReadAsync(segment.Array!, segment.Offset, segment.Count, cancellationToken));
    }

    public static ValueTask<int> ReadAsync(
        this StreamReader reader,
        Memory<char> buffer,
        CancellationToken cancellationToken = default)
    {
        // StreamReader doesn't accept cancellation token (pre-netstd2.1)
        cancellationToken.ThrowIfCancellationRequested();

        if (!MemoryMarshal.TryGetArray((ReadOnlyMemory<char>)buffer, out var segment))
        {
            segment = new(buffer.ToArray());
        }

        return new(reader.ReadAsync(segment.Array!, segment.Offset, segment.Count));
    }

    public static ValueTask WriteAsync(
        this Stream stream,
        ReadOnlyMemory<byte> buffer,
        CancellationToken cancellationToken = default)
    {
        if (!MemoryMarshal.TryGetArray(buffer, out var segment))
        {
            segment = new(buffer.ToArray());
        }

        return new(stream.WriteAsync(segment.Array!, segment.Offset, segment.Count, cancellationToken));
    }

    public static ValueTask WriteAsync(
        this StreamWriter stream,
        ReadOnlyMemory<char> buffer,
        CancellationToken cancellationToken = default)
    {
        // StreamReader doesn't accept cancellation token (pre-netstd2.1)
        cancellationToken.ThrowIfCancellationRequested();

        if (!MemoryMarshal.TryGetArray(buffer, out var segment))
        {
            segment = new(buffer.ToArray());
        }

        return new(stream.WriteAsync(segment.Array!, segment.Offset, segment.Count));
    }
}
#endif