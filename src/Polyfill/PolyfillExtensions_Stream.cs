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

    public static Task CopyToAsync(
        this Stream target,
        Stream destination,
        CancellationToken cancellationToken = default) =>
        target.CopyToAsync(destination, 81920, cancellationToken);
}
#endif