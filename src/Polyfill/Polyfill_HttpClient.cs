#if FeatureHttp

namespace Polyfills;

using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

static partial class Polyfill
{
#if !NET5_0_OR_GREATER
    /// <summary>
    /// Sends an HTTP request with the specified request.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.send?view=net-11.0#system-net-http-httpclient-send(system-net-http-httprequestmessage)
    public static HttpResponseMessage Send(
        this HttpClient target,
        HttpRequestMessage request) =>
        target.SendAsync(request).GetAwaiter().GetResult();

    /// <summary>
    /// Sends an HTTP request.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.send?view=net-11.0#system-net-http-httpclient-send(system-net-http-httprequestmessage-system-net-http-httpcompletionoption)
    public static HttpResponseMessage Send(
        this HttpClient target,
        HttpRequestMessage request,
        HttpCompletionOption completionOption) =>
        target.SendAsync(request, completionOption).GetAwaiter().GetResult();

    /// <summary>
    /// Sends an HTTP request with the specified request and cancellation token.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.send?view=net-11.0#system-net-http-httpclient-send(system-net-http-httprequestmessage-system-threading-cancellationtoken)
    public static HttpResponseMessage Send(
        this HttpClient target,
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return target.SendAsync(request, cancellationToken).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Sends an HTTP request with the specified request, completion option and cancellation token.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.send?view=net-11.0#system-net-http-httpclient-send(system-net-http-httprequestmessage-system-net-http-httpcompletionoption-system-threading-cancellationtoken)
    public static HttpResponseMessage Send(
        this HttpClient target,
        HttpRequestMessage request,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return target.SendAsync(request, completionOption, cancellationToken).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Send a GET request to the specified Uri and return the response body as a stream in an asynchronous operation.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.getstreamasync?view=net-11.0#system-net-http-httpclient-getstreamasync(system-string-system-threading-cancellationtoken)
    public static async Task<Stream> GetStreamAsync(
        this HttpClient target,
        string requestUri,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await target.GetAsync(
                requestUri,
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            );

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStreamAsync(cancellationToken);
        }
        catch (OperationCanceledException ex) when (
            ex.CancellationToken != cancellationToken &&
            cancellationToken.IsCancellationRequested)
        {
            throw new OperationCanceledException(ex.Message, ex.InnerException, cancellationToken);
        }
    }

    /// <summary>
    /// Send a GET request to the specified Uri and return the response body as a stream in an asynchronous operation.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.getstreamasync?view=net-11.0#system-net-http-httpclient-getstreamasync(system-uri-system-threading-cancellationtoken)
    public static Task<Stream> GetStreamAsync(
        this HttpClient target,
        Uri requestUri,
        CancellationToken cancellationToken = default) =>
        target.GetStreamAsync(requestUri.ToString(), cancellationToken);

    /// <summary>
    /// Send a GET request to the specified Uri and return the response body as a byte array in an asynchronous operation.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.getbytearrayasync?view=net-11.0#system-net-http-httpclient-getbytearrayasync(system-string-system-threading-cancellationtoken)
    public static async Task<byte[]> GetByteArrayAsync(
        this HttpClient target,
        string requestUri,
        CancellationToken cancellationToken = default)
    {
        try
        {
            using var response = await target.GetAsync(
                requestUri,
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken);

            response.EnsureSuccessStatusCode();

            return await response.Content
                .ReadAsByteArrayAsync(cancellationToken);
        }
        catch (OperationCanceledException exception) when (
            exception.CancellationToken != cancellationToken &&
            cancellationToken.IsCancellationRequested)
        {
            throw new OperationCanceledException(exception.Message, exception.InnerException, cancellationToken);
        }
    }

    /// <summary>
    /// Send a GET request to the specified Uri and return the response body as a byte array in an asynchronous operation.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.getbytearrayasync?view=net-11.0#system-net-http-httpclient-getbytearrayasync(system-uri-system-threading-cancellationtoken)
    public static Task<byte[]> GetByteArrayAsync(
        this HttpClient target,
        Uri requestUri,
        CancellationToken cancellationToken = default) =>
        target.GetByteArrayAsync(requestUri.ToString(), cancellationToken);

    /// <summary>
    /// Send a GET request to the specified Uri and return the response body as a string in an asynchronous operation.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.getstringasync?view=net-11.0#system-net-http-httpclient-getstringasync(system-string-system-threading-cancellationtoken)
    public static async Task<string> GetStringAsync(
        this HttpClient target,
        string requestUri,
        CancellationToken cancellationToken = default)
    {
        try
        {
            using var response = await target.GetAsync(
                requestUri,
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken);

            response.EnsureSuccessStatusCode();

            return await response.Content
                .ReadAsStringAsync(cancellationToken);
        }
        catch (OperationCanceledException exception) when (
            exception.CancellationToken != cancellationToken &&
            cancellationToken.IsCancellationRequested)
        {
            throw new OperationCanceledException(exception.Message, exception.InnerException, cancellationToken);
        }
    }

    /// <summary>
    /// Send a GET request to the specified Uri and return the response body as a string in an asynchronous operation.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.getstringasync?view=net-11.0#system-net-http-httpclient-getstringasync(system-uri-system-threading-cancellationtoken)
    public static Task<string> GetStringAsync(
        this HttpClient target,
        Uri requestUri,
        CancellationToken cancellationToken = default) =>
        target.GetStringAsync(requestUri.ToString(), cancellationToken);
#endif
}
#endif
