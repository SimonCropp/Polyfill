
#pragma warning disable


namespace Polyfills;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Link = System.ComponentModel.DescriptionAttribute;

static partial class Polyfill
{

    /// <summary>
    /// Asynchronously reads the bytes from the current stream and writes them to another stream, using a specified
    /// cancellation token. Both streams positions are advanced by the number of bytes copied.
    /// </summary>
    /// <param name="destination">The stream to which the contents of the current stream will be copied.</param>
    /// <param name="cancellationToken">
    /// The token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None"/>.
    /// </param>
    /// <returns>A task that represents the asynchronous copy operation.</returns>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.copytoasync#system-io-stream-copytoasync(system-io-stream-system-threading-cancellationtoken)")]
    public static Task CopyToAsync(
        this Stream target,
        Stream destination,
        CancellationToken cancellationToken = default) =>
        target.CopyToAsync(destination, 81920, cancellationToken);
}