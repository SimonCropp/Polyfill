
#pragma warning disable


namespace Polyfills;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Link = System.ComponentModel.DescriptionAttribute;

static partial class Polyfill
{
    /// <summary>
    /// Serializes the HTTP content and returns a stream that represents the content.
    /// </summary>
    /// <remarks>
    /// Note that this method will internally buffer the content unless <c>CreateContentReadStreamAsync()</c> has been
    /// implemented to do otherwise.
    /// </remarks>
    /// <param name="cancellationToken">
    /// The token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None"/>.
    /// </param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent.readasstreamasync#system-net-http-httpcontent-readasstreamasync(system-threading-cancellationtoken)")]
    public static Task<Stream> ReadAsStreamAsync(
        this HttpContent target,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return target.ReadAsStreamAsync()
            .WaitAsync(cancellationToken);
    }

    /// <summary>
    /// Serializes the HTTP content to a byte array as an asynchronous operation.
    /// </summary>
    /// <remarks>
    /// Note that this method will internally buffer the content unless <c>CreateContentReadStreamAsync()</c> has been
    /// implemented to do otherwise.
    /// </remarks>
    /// <param name="cancellationToken">
    /// The token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None"/>.
    /// </param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent.readasbytearrayasync#system-net-http-httpcontent-readasbytearrayasync(system-threading-cancellationtoken)")]
    public static Task<byte[]> ReadAsByteArrayAsync(
        this HttpContent target,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return target.ReadAsByteArrayAsync()
            .WaitAsync(cancellationToken);
    }

    /// <summary>
    /// Serializes the HTTP content to a string as an asynchronous operation.
    /// </summary>
    /// <remarks>
    /// Note that this method will internally buffer the content unless <c>CreateContentReadStreamAsync()</c> has been
    /// implemented to do otherwise.
    /// </remarks>
    /// <param name="cancellationToken">
    /// The token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None"/>.
    /// </param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent.readasstringasync#system-net-http-httpcontent-readasstringasync(system-threading-cancellationtoken)")]
    public static Task<string> ReadAsStringAsync(
        this HttpContent target,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return target.ReadAsStringAsync()
            .WaitAsync(cancellationToken);
    }
}