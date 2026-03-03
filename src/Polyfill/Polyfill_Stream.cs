namespace Polyfills;

// ReSharper disable once RedundantUsingDirective
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

static partial class Polyfill
{
#if !NETCOREAPP2_1_OR_GREATER && !NETSTANDARD2_1_OR_GREATER

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
