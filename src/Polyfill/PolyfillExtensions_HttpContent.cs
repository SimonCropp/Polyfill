#if ((NETFRAMEWORK && HTTPREFERENCED) || NETSTANDARD2_0 || NETCOREAPP2X || NETCOREAPP3X)

#pragma warning disable

// ReSharper disable RedundantUsingDirective
// ReSharper disable UnusedMember.Global

using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    /// <summary>
    /// Serializes the HTTP content and returns a stream that represents the content.
    /// </summary>
    /// <remarks>
    /// Note that this method will internally buffer the content unless <c>CreateContentReadStreamAsync()</c> has been implemented to do otherwise.
    /// </remarks>
    /// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is None.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public static async Task<Stream> ReadAsStreamAsync(
        this HttpContent httpContent,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return await httpContent.ReadAsStreamAsync().ConfigureAwait(false);
    }

    /// <summary>
    /// Serializes the HTTP content to a byte array as an asynchronous operation.
    /// </summary>
    /// <remarks>
    /// Note that this method will internally buffer the content unless <c>CreateContentReadStreamAsync()</c> has been implemented to do otherwise.
    /// </remarks>
    /// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is None.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public static async Task<byte[]> ReadAsByteArrayAsync(
        this HttpContent httpContent,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return await httpContent.ReadAsByteArrayAsync().ConfigureAwait(false);
    }

    /// <summary>
    /// Serializes the HTTP content to a string as an asynchronous operation.
    /// </summary>
    /// <remarks>
    /// Note that this method will internally buffer the content unless <c>CreateContentReadStreamAsync()</c> has been implemented to do otherwise.
    /// </remarks>
    /// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is None.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public static async Task<string> ReadAsStringAsync(
        this HttpContent httpContent,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return await httpContent.ReadAsStringAsync().ConfigureAwait(false);
    }
}
#endif