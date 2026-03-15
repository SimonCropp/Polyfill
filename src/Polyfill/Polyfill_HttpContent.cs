#if FeatureHttp
namespace Polyfills;

using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

static partial class Polyfill
{
#if !NET6_0_OR_GREATER
    /// <summary>
    /// Serializes the HTTP content and returns a stream that represents the content.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent.readasstream?view=net-11.0#system-net-http-httpcontent-readasstream(system-threading-cancellationtoken)
    public static Stream ReadAsStream(
        this HttpContent target,
        CancellationToken cancellationToken = default) =>
        target.ReadAsStreamAsync(cancellationToken).GetAwaiter().GetResult();
#endif

#if !NET5_0_OR_GREATER
    /// <summary>
    /// Serializes the HTTP content into a stream of bytes and copies it to the stream object provided as the stream parameter.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent.copytoasync?view=net-11.0#system-net-http-httpcontent-copytoasync(system-io-stream-system-threading-cancellationtoken)
    public static Task CopyToAsync(
        this HttpContent target,
        Stream stream,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return target.CopyToAsync(stream)
            .WaitAsync(cancellationToken);
    }

    /// <summary>
    /// Serializes the HTTP content into a stream of bytes and copies it to the stream object provided as the stream parameter.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent.copytoasync?view=net-11.0#system-net-http-httpcontent-copytoasync(system-io-stream-system-net-transportcontext-system-threading-cancellationtoken)
    public static Task CopyToAsync(
        this HttpContent target,
        Stream stream,
        System.Net.TransportContext? context,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return target.CopyToAsync(stream, context)
            .WaitAsync(cancellationToken);
    }

    /// <summary>
    /// Serializes the HTTP content into a stream.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent.copyto?view=net-11.0
    public static void CopyTo(
        this HttpContent target,
        Stream stream,
        System.Net.TransportContext? context,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        target.CopyToAsync(stream, context).GetAwaiter().GetResult();
    }

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

#if !NET9_0_OR_GREATER
    /// <summary>
    /// Serialize the HTTP content to a memory buffer as an asynchronous operation.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent.loadintobufferasync?view=net-11.0#system-net-http-httpcontent-loadintobufferasync(system-threading-cancellationtoken)
    public static Task LoadIntoBufferAsync(
        this HttpContent target,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return target.LoadIntoBufferAsync()
            .WaitAsync(cancellationToken);
    }

    /// <summary>
    /// Serialize the HTTP content to a memory buffer as an asynchronous operation.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent.loadintobufferasync?view=net-11.0#system-net-http-httpcontent-loadintobufferasync(system-int64-system-threading-cancellationtoken)
    public static Task LoadIntoBufferAsync(
        this HttpContent target,
        long maxBufferSize,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return target.LoadIntoBufferAsync(maxBufferSize)
            .WaitAsync(cancellationToken);
    }
#endif
}
#endif
