// These tests cover HttpContent polyfills as well

using System.Net;
using System.Net.Http;

partial class PolyfillExtensionsTests
{
    class FakeHttpMessageHandler : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            Cancel cancellation)
        {
            cancellation.ThrowIfCancellationRequested();

            return Task.FromResult(
                new HttpResponseMessage(HttpStatusCode.OK)
                {
                    RequestMessage = request,
                    Content = new StringContent("Fake Content")
                }
            );
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
        Assert.AreEqual("Fake Content", content);
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
            Assert.Fail();
        }
        catch (OperationCanceledException exception)
        {
            // Assert
            Assert.IsTrue(exception.CancellationToken.IsCancellationRequested);
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
        Assert.AreEqual("Fake Content", content);
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
            Assert.Fail();
        }
        catch (OperationCanceledException exception)
        {
            // Assert
            Assert.IsTrue(exception.CancellationToken.IsCancellationRequested);
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
        Assert.AreEqual("Fake Content", content);
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
            Assert.Fail();
        }
        catch (OperationCanceledException exception)
        {
            // Assert
            Assert.IsTrue(exception.CancellationToken.IsCancellationRequested);
        }
    }
}