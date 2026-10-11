// These tests cover HttpContent polyfills as well

using System.Net;
using System.Net.Http;

partial class PolyfillTests
{
    class FakeHttpMessageHandler : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            Cancel cancellation)
        {
            cancellation.ThrowIfCancellationRequested();

            return Task.FromResult(CreateResponse(request));
        }

#if NET5_0_OR_GREATER
        protected override HttpResponseMessage Send(
            HttpRequestMessage request,
            Cancel cancellation)
        {
            cancellation.ThrowIfCancellationRequested();

            return CreateResponse(request);
        }
#endif

        static HttpResponseMessage CreateResponse(HttpRequestMessage request) =>
            new(HttpStatusCode.OK)
            {
                RequestMessage = request,
                Content = new StringContent("Fake Content")
            };
    }

    [Test]
    public async Task HttpClientSend()
    {
        using var httpClient = new HttpClient(new FakeHttpMessageHandler(), true);
        using var request = new HttpRequestMessage(HttpMethod.Get, "https://example.com");

        using var response = httpClient.Send(request);

        await Assert.That(response.StatusCode).IsEqualTo(HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        await Assert.That(content).IsEqualTo("Fake Content");
    }

    [Test]
    public async Task HttpClientSendWithCompletionOption()
    {
        using var httpClient = new HttpClient(new FakeHttpMessageHandler(), true);
        using var request = new HttpRequestMessage(HttpMethod.Get, "https://example.com");

        using var response = httpClient.Send(request, HttpCompletionOption.ResponseHeadersRead);

        await Assert.That(response.StatusCode).IsEqualTo(HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        await Assert.That(content).IsEqualTo("Fake Content");
    }

    [Test]
    public async Task HttpClientSendWithCancellationToken()
    {
        var cancel = new Cancel();
        using var httpClient = new HttpClient(new FakeHttpMessageHandler(), true);
        using var request = new HttpRequestMessage(HttpMethod.Get, "https://example.com");

        using var response = httpClient.Send(request, cancel);

        await Assert.That(response.StatusCode).IsEqualTo(HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        await Assert.That(content).IsEqualTo("Fake Content");
    }

    [Test]
    public async Task HttpClientSendWithCancellationToken_Cancelled()
    {
        var cancel = new Cancel(true);
        using var httpClient = new HttpClient(new FakeHttpMessageHandler(), true);
        using var request = new HttpRequestMessage(HttpMethod.Get, "https://example.com");

        try
        {
            httpClient.Send(request, cancel);
            Assert.Fail("Test failed");
        }
        catch (OperationCanceledException exception)
        {
            await Assert.That(exception.CancellationToken.IsCancellationRequested).IsTrue();
        }
    }

    [Test]
    public async Task HttpClientSendWithCompletionOptionAndCancellationToken()
    {
        var cancel = new Cancel();
        using var httpClient = new HttpClient(new FakeHttpMessageHandler(), true);
        using var request = new HttpRequestMessage(HttpMethod.Get, "https://example.com");

        using var response = httpClient.Send(request, HttpCompletionOption.ResponseContentRead, cancel);

        await Assert.That(response.StatusCode).IsEqualTo(HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        await Assert.That(content).IsEqualTo("Fake Content");
    }

    [Test]
    public async Task HttpClientSendWithCompletionOptionAndCancellationToken_Cancelled()
    {
        var cancel = new Cancel(true);
        using var httpClient = new HttpClient(new FakeHttpMessageHandler(), true);
        using var request = new HttpRequestMessage(HttpMethod.Get, "https://example.com");

        try
        {
            httpClient.Send(request, HttpCompletionOption.ResponseContentRead, cancel);
            Assert.Fail("Test failed");
        }
        catch (OperationCanceledException exception)
        {
            await Assert.That(exception.CancellationToken.IsCancellationRequested).IsTrue();
        }
    }

    [Test]
    public async Task HttpClientPatchAsync_String()
    {
        using var httpClient = new HttpClient(new FakeHttpMessageHandler(), true);
        using var content = new StringContent("patch data");

        using var response = await httpClient.PatchAsync("https://example.com", content);

        await Assert.That(response.StatusCode).IsEqualTo(HttpStatusCode.OK);
    }

    [Test]
    public async Task HttpClientPatchAsync_Uri()
    {
        using var httpClient = new HttpClient(new FakeHttpMessageHandler(), true);
        using var content = new StringContent("patch data");

        using var response = await httpClient.PatchAsync(new Uri("https://example.com"), content);

        await Assert.That(response.StatusCode).IsEqualTo(HttpStatusCode.OK);
    }

    [Test]
    public async Task HttpClientPatchAsync_String_WithCancellationToken()
    {
        var cancel = new Cancel();
        using var httpClient = new HttpClient(new FakeHttpMessageHandler(), true);
        using var content = new StringContent("patch data");

        using var response = await httpClient.PatchAsync("https://example.com", content, cancel);

        await Assert.That(response.StatusCode).IsEqualTo(HttpStatusCode.OK);
    }

    [Test]
    public async Task HttpClientPatchAsync_Uri_WithCancellationToken()
    {
        var cancel = new Cancel();
        using var httpClient = new HttpClient(new FakeHttpMessageHandler(), true);
        using var content = new StringContent("patch data");

        using var response = await httpClient.PatchAsync(new Uri("https://example.com"), content, cancel);

        await Assert.That(response.StatusCode).IsEqualTo(HttpStatusCode.OK);
    }

    [Test]
    public async Task HttpClientGetStreamAsync_Positive()
    {
        // Arrange
        var cancel = new Cancel();
        using var httpClient = new HttpClient(new FakeHttpMessageHandler(), true);

        // Act
        using var stream = await httpClient.GetStreamAsync("https://example.com", cancel);
        using var reader = new StreamReader(stream);
        var content = await reader.ReadToEndAsync(cancel);

        // Assert
        await Assert.That(content).IsEqualTo("Fake Content");
    }

    [Test]
    public async Task HttpClientGetStreamAsync_Negative()
    {
        // Arrange
        var cancel = new Cancel(true);
        using var httpClient = new HttpClient(new FakeHttpMessageHandler(), true);

        // Act
        try
        {
            await httpClient.GetStreamAsync("https://example.com", cancel);
            Assert.Fail("Test failed");
        }
        catch (OperationCanceledException exception)
        {
            // Assert
            await Assert.That(exception.CancellationToken.IsCancellationRequested).IsTrue();
        }
    }

    [Test]
    public async Task HttpClientGetByteArrayAsync_Positive()
    {
        // Arrange
        var cancel = new Cancel();
        using var httpClient = new HttpClient(new FakeHttpMessageHandler(), true);

        // Act
        var bytes = await httpClient.GetByteArrayAsync("https://example.com", cancel);
        var content = Encoding.UTF8.GetString(bytes);

        // Assert
        await Assert.That(content).IsEqualTo("Fake Content");
    }

    [Test]
    public async Task HttpClientGetByteArrayAsync_Negative()
    {
        // Arrange
        var cancel = new Cancel(true);
        using var httpClient = new HttpClient(new FakeHttpMessageHandler(), true);

        // Act
        try
        {
            await httpClient.GetByteArrayAsync("https://example.com", cancel);
            Assert.Fail("Test failed");
        }
        catch (OperationCanceledException exception)
        {
            // Assert
            await Assert.That(exception.CancellationToken.IsCancellationRequested).IsTrue();
        }
    }

    [Test]
    public async Task HttpClientGetStringAsync_Positive()
    {
        // Arrange
        var cancel = new Cancel();
        using var httpClient = new HttpClient(new FakeHttpMessageHandler(), true);

        // Act
        var content = await httpClient.GetStringAsync("https://example.com", cancel);

        // Assert
        await Assert.That(content).IsEqualTo("Fake Content");
    }

    [Test]
    public async Task HttpClientGetStringAsync_Negative()
    {
        // Arrange
        var cancel = new Cancel(true);
        using var httpClient = new HttpClient(new FakeHttpMessageHandler(), true);

        // Act
        try
        {
            await httpClient.GetStringAsync("https://example.com", cancel);
            Assert.Fail("Test failed");
        }
        catch (OperationCanceledException exception)
        {
            // Assert
            await Assert.That(exception.CancellationToken.IsCancellationRequested).IsTrue();
        }
    }

    class ResponseHandler(Func<HttpRequestMessage, Cancel, HttpResponseMessage> createResponse) :
        HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, Cancel cancel) =>
            Task.FromResult(createResponse(request, cancel));
    }

    // A body that never arrives. As with a real transport, reading it is aborted when the send is cancelled.
    class StalledContent(Cancel sendCancel) :
        HttpContent
    {
        protected override Task SerializeToStreamAsync(Stream stream, TransportContext? context) =>
            Task.Delay(Timeout.Infinite, sendCancel);

        protected override bool TryComputeLength(out long length)
        {
            length = 0;
            return false;
        }
    }

    // A body of unknown length, written to the stream the client supplies. As with a response read from the network.
    class StreamedContent(int length) :
        HttpContent
    {
        protected override Task SerializeToStreamAsync(Stream stream, TransportContext? context) =>
            stream.WriteAsync(new byte[length], 0, length);

        protected override bool TryComputeLength(out long length)
        {
            length = 0;
            return false;
        }
    }

    class TrackingResponse(HttpStatusCode statusCode) :
        HttpResponseMessage(statusCode)
    {
        public bool Disposed { get; private set; }

        protected override void Dispose(bool disposing)
        {
            Disposed = true;
            base.Dispose(disposing);
        }
    }

    static async Task<Exception?> CatchWithin(Task task, TimeSpan timeout)
    {
        var completed = await Task.WhenAny(task, Task.Delay(timeout));
        if (completed != task)
        {
            return new TimeoutException("The task did not complete.");
        }

        try
        {
            await task;
            return null;
        }
        catch (Exception exception)
        {
            return exception;
        }
    }

    [Test]
    public async Task HttpClientGetStringAsync_AppliesTimeoutToBody()
    {
        using var httpClient = new HttpClient(
            new ResponseHandler((_, cancel) => new(HttpStatusCode.OK) {Content = new StalledContent(cancel)}));
        httpClient.Timeout = TimeSpan.FromMilliseconds(200);

        var exception = await CatchWithin(
            httpClient.GetStringAsync("https://example.com", Cancel.None),
            TimeSpan.FromSeconds(30));

        await Assert.That(exception is OperationCanceledException).IsTrue();
    }

    [Test]
    public async Task HttpClientGetByteArrayAsync_AppliesTimeoutToBody()
    {
        using var httpClient = new HttpClient(
            new ResponseHandler((_, cancel) => new(HttpStatusCode.OK) {Content = new StalledContent(cancel)}));
        httpClient.Timeout = TimeSpan.FromMilliseconds(200);

        var exception = await CatchWithin(
            httpClient.GetByteArrayAsync("https://example.com", Cancel.None),
            TimeSpan.FromSeconds(30));

        await Assert.That(exception is OperationCanceledException).IsTrue();
    }

    [Test]
    public async Task HttpClientGetStringAsync_AppliesMaxResponseContentBufferSize()
    {
        using var httpClient = new HttpClient(
            new ResponseHandler((_, _) => new(HttpStatusCode.OK) {Content = new StreamedContent(100)}));
        httpClient.MaxResponseContentBufferSize = 10;

        var exception = await CatchWithin(
            httpClient.GetStringAsync("https://example.com", Cancel.None),
            TimeSpan.FromSeconds(30));

        await Assert.That(exception is HttpRequestException).IsTrue();
    }

    [Test]
    public async Task HttpClientGetByteArrayAsync_AppliesMaxResponseContentBufferSize()
    {
        using var httpClient = new HttpClient(
            new ResponseHandler((_, _) => new(HttpStatusCode.OK) {Content = new StreamedContent(100)}));
        httpClient.MaxResponseContentBufferSize = 10;

        var exception = await CatchWithin(
            httpClient.GetByteArrayAsync("https://example.com", Cancel.None),
            TimeSpan.FromSeconds(30));

        await Assert.That(exception is HttpRequestException).IsTrue();
    }

    [Test]
    public async Task HttpClientGetAsync_NullContent()
    {
        using var httpClient = new HttpClient(
            new ResponseHandler((_, _) => new(HttpStatusCode.OK) {Content = null}));

        var text = await httpClient.GetStringAsync("https://example.com", Cancel.None);
        await Assert.That(text).IsEqualTo(string.Empty);

        var bytes = await httpClient.GetByteArrayAsync("https://example.com", Cancel.None);
        await Assert.That(bytes.Length).IsEqualTo(0);

        using var stream = await httpClient.GetStreamAsync("https://example.com", Cancel.None);
        await Assert.That(stream.ReadByte()).IsEqualTo(-1);
    }

    [Test]
    public async Task HttpClientGetStreamAsync_DisposesResponseOnFailure()
    {
        var response = new TrackingResponse(HttpStatusCode.InternalServerError)
        {
            Content = new StringContent("error")
        };
        using var httpClient = new HttpClient(new ResponseHandler((_, _) => response));

        var exception = await CatchWithin(
            httpClient.GetStreamAsync("https://example.com", Cancel.None),
            TimeSpan.FromSeconds(30));

        await Assert.That(exception is HttpRequestException).IsTrue();
        await Assert.That(response.Disposed).IsTrue();
    }
}