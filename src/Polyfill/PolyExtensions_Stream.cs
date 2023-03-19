#if (NET46X && VALUETUPLEREFERENCED) || NET47X ||NET48X || NETSTANDARD2_0

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace Polyfill;

static partial class PolyExtensions
{
    public static ValueTask<int> ReadAsync(
        this Stream stream,
        Memory<byte> buffer,
        CancellationToken cancellationToken)
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
        CancellationToken cancellationToken)
    {
        // StreamReader doesn't accept cancellation token anywhere (pre-netstd2.1)
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
        CancellationToken cancellationToken)
    {
        if (!MemoryMarshal.TryGetArray(buffer, out var segment))
        {
            segment = new(buffer.ToArray());
        }

        return new(stream.WriteAsync(segment.Array!, segment.Offset, segment.Count, cancellationToken));
    }

    public static ValueTask CopyToAsync(
        this Stream stream,
        Stream destination,
        CancellationToken cancellationToken) =>
        new(stream.CopyToAsync(destination, 81920, cancellationToken));
}
#endif