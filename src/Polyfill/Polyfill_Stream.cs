namespace Polyfills;

// ReSharper disable once RedundantUsingDirective
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

static partial class Polyfill
{
#if !NETCOREAPP2_1_OR_GREATER && !NETSTANDARD2_1_OR_GREATER
#if FeatureMemory && FeatureValueTask

    /// <summary>
    /// Asynchronously reads a sequence of bytes from the current stream, advances the position within the stream by
    /// the number of bytes read, and monitors cancellation requests.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.readasync?view=net-11.0#system-io-stream-readasync(system-memory((system-byte))-system-threading-cancellationtoken)
    public static ValueTask<int> ReadAsync(
        this Stream target,
        Memory<byte> buffer,
        CancellationToken cancellationToken = default)
    {
        if (!MemoryMarshal.TryGetArray((ReadOnlyMemory<byte>) buffer, out var segment))
        {
            segment = new(buffer.ToArray());
        }

        var task = target.ReadAsync(segment.Array!, segment.Offset, segment.Count, cancellationToken);
        return new(task);
    }

    /// <summary>
    /// Asynchronously writes a sequence of bytes to the current stream, advances the current position
    /// within this stream by the number of bytes written, and monitors cancellation requests.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.writeasync?view=net-11.0#system-io-stream-writeasync(system-readonlymemory((system-byte))-system-threading-cancellationtoken)
    public static ValueTask WriteAsync(
        this Stream target,
        ReadOnlyMemory<byte> buffer,
        CancellationToken cancellationToken = default)
    {
        if (!MemoryMarshal.TryGetArray(buffer, out var segment))
        {
            segment = new(buffer.ToArray());
        }

        var task = target.WriteAsync(segment.Array!, segment.Offset, segment.Count, cancellationToken);
        return new(task);
    }

#endif

    /// <summary>
    /// Asynchronously reads the bytes from the current stream and writes them to another stream, using a specified
    /// cancellation token. Both streams positions are advanced by the number of bytes copied.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.copytoasync?view=net-11.0#system-io-stream-copytoasync(system-io-stream-system-threading-cancellationtoken)
    public static Task CopyToAsync(
        this Stream target,
        Stream destination,
        CancellationToken cancellationToken = default) =>
        target.CopyToAsync(destination, 81920, cancellationToken);
#endif
}
