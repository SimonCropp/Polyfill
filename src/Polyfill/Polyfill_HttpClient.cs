#if FeatureHttp

namespace Polyfills;

using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

static partial class Polyfill
{
#if !NETCOREAPP2_1_OR_GREATER && !NETSTANDARD2_1_OR_GREATER
    /// <summary>
    /// Sends a PATCH request to a URI designated as a string as an asynchronous operation.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.patchasync?view=net-11.0#system-net-http-httpclient-patchasync(system-string-system-net-http-httpcontent)
    public static Task<HttpResponseMessage> PatchAsync(
        this HttpClient target,
        string? requestUri,
        HttpContent? content) =>
        target.SendAsync(new HttpRequestMessage(new HttpMethod("PATCH"), requestUri) { Content = content });

    /// <summary>
    /// Sends a PATCH request as an asynchronous operation.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.patchasync?view=net-11.0#system-net-http-httpclient-patchasync(system-uri-system-net-http-httpcontent)
    public static Task<HttpResponseMessage> PatchAsync(
        this HttpClient target,
        Uri? requestUri,
        HttpContent? content) =>
        target.SendAsync(new HttpRequestMessage(new HttpMethod("PATCH"), requestUri) { Content = content });

    /// <summary>
    /// Sends a PATCH request with a cancellation token to a URI represented as a string as an asynchronous operation.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.patchasync?view=net-11.0#system-net-http-httpclient-patchasync(system-string-system-net-http-httpcontent-system-threading-cancellationtoken)
    public static Task<HttpResponseMessage> PatchAsync(
        this HttpClient target,
        string? requestUri,
        HttpContent? content,
        CancellationToken cancellationToken) =>
        target.SendAsync(new HttpRequestMessage(new HttpMethod("PATCH"), requestUri) { Content = content }, cancellationToken);

    /// <summary>
    /// Sends a PATCH request with a cancellation token as an asynchronous operation.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.patchasync?view=net-11.0#system-net-http-httpclient-patchasync(system-uri-system-net-http-httpcontent-system-threading-cancellationtoken)
    public static Task<HttpResponseMessage> PatchAsync(
        this HttpClient target,
        Uri? requestUri,
        HttpContent? content,
        CancellationToken cancellationToken) =>
        target.SendAsync(new HttpRequestMessage(new HttpMethod("PATCH"), requestUri) { Content = content }, cancellationToken);
#endif

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
            ).ConfigureAwait(false);

            // The returned stream keeps the response alive. If no stream is returned, the response must be disposed.
            try
            {
                response.EnsureSuccessStatusCode();

                var content = response.Content;
                if (content == null)
                {
                    response.Dispose();
                    return Stream.Null;
                }

                return await content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
            }
            catch
            {
                response.Dispose();
                throw;
            }
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
            // ResponseContentRead so that the client buffers the body,
            // applying its Timeout and MaxResponseContentBufferSize to it.
            using var response = await target.GetAsync(
                requestUri,
                HttpCompletionOption.ResponseContentRead,
                cancellationToken).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var content = response.Content;
            if (content == null)
            {
                return [];
            }

            return await content
                .ReadAsByteArrayAsync(cancellationToken).ConfigureAwait(false);
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
            // ResponseContentRead so that the client buffers the body,
            // applying its Timeout and MaxResponseContentBufferSize to it.
            using var response = await target.GetAsync(
                requestUri,
                HttpCompletionOption.ResponseContentRead,
                cancellationToken).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var content = response.Content;
            if (content == null)
            {
                return string.Empty;
            }

            return await content
                .ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
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
