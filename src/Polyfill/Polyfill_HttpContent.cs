namespace Polyfills;

using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

#if FeatureHttp
static partial class Polyfill
{
#if !NET5_0_OR_GREATER
    /// <summary>
    /// Serializes the HTTP content and returns a stream that represents the content.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent.readasstreamasync?view=net-11.0#system-net-http-httpcontent-readasstreamasync(system-threading-cancellationtoken)
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
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent.readasbytearrayasync?view=net-11.0#system-net-http-httpcontent-readasbytearrayasync(system-threading-cancellationtoken)
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
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent.readasstringasync?view=net-11.0#system-net-http-httpcontent-readasstringasync(system-threading-cancellationtoken)
    public static Task<string> ReadAsStringAsync(
        this HttpContent target,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return target.ReadAsStringAsync()
            .WaitAsync(cancellationToken);
    }
#endif
}
#endif
