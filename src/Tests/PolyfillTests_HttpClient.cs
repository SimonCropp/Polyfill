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
}