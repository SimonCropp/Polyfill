#if TASKSEXTENSIONSREFERENCED && (NETFRAMEWORK || NETSTANDARD2_0 || NETCOREAPP2_0)

#pragma warning disable

// ReSharper disable RedundantUsingDirective
// ReSharper disable UnusedMember.Global

using System;
using System.IO;
using System.Runtime.InteropServices;
using DescriptionAttribute = System.ComponentModel.DescriptionAttribute;
using System.Threading;
using System.Threading.Tasks;
// ReSharper disable RedundantAttributeSuffix

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
    [DescriptionAttribute("https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.writeasync#system-io-textwriter-writeasync(system-readonlymemory((system-char))-system-threading-cancellationtoken)")]
    public static ValueTask WriteAsync(
        this TextWriter target,
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

    /// <summary>
    /// Asynchronously writes the text representation of a character memory region to the stream, followed by a line terminator.
    /// </summary>
    /// <param name="buffer">The character memory region to write to the stream.</param>
    /// <param name="cancellationToken">
    /// The token to monitor for cancellation requests.
    /// The default value is <see cref="Cancellation.None"/>.
    /// </param>
    /// <returns>A task that represents the asynchronous write operation.</returns>
    [DescriptionAttribute("https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.writelineasync#system-io-textwriter-writelineasync(system-readonlymemory((system-char))-system-threading-cancellationtoken)")]
    public static ValueTask WriteLineAsync(
        this TextWriter target,
        ReadOnlyMemory<char> buffer,
        CancellationToken cancellationToken = default)
    {
        // StreamReader doesn't accept cancellation token (pre-netstd2.1)
        cancellationToken.ThrowIfCancellationRequested();

        if (!MemoryMarshal.TryGetArray(buffer, out var segment))
        {
            segment = new(buffer.ToArray());
        }

        return new(target.WriteLineAsync(segment.Array!, segment.Offset, segment.Count));
    }
}
#endif