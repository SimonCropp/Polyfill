namespace Polyfills;

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

static partial class Polyfill
{
#if FeatureValueTask && FeatureMemory && !NETCOREAPP3_0_OR_GREATER && !NETSTANDARD2_1_OR_GREATER
    /// <summary>
    /// Asynchronously reads the characters from the current stream into a memory block.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.textreader.readasync?view=net-11.0#system-io-textreader-readasync(system-memory((system-char))-system-threading-cancellationtoken)
    public static ValueTask<int> ReadAsync(
        this TextReader target,
        Memory<char> buffer,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (!MemoryMarshal.TryGetArray((ReadOnlyMemory<char>)buffer, out var segment))
        {
            segment = new(buffer.ToArray());
        }

        var task = target.ReadAsync(segment.Array!, segment.Offset, segment.Count)
            .WaitAsync(cancellationToken);
        return new(task);
    }
#endif

#if !NET7_0_OR_GREATER
    /// <summary>
    /// Reads all characters from the current position to the end of the stream asynchronously and returns them as one string.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.textreader.readtoendasync?view=net-11.0#system-io-textreader-readtoendasync(system-threading-cancellationtoken)
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
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.textreader.readtoendasync?view=net-11.0#system-io-textreader-readlineasync(system-threading-cancellationtoken)
    public static Task<string> ReadLineAsync(
        this TextReader target,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return target.ReadLineAsync()
            .WaitAsync(cancellationToken);
    }
#endif
}
