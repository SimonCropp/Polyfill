// These tests cover HttpContent polyfills as well

using System.Net.Http;

partial class PolyfillExtensionsTests
{
    [Test]
    public async Task HttpClientGetStreamAsync_Positive()
    {
        // Arrange
        var cancellationToken = new CancellationToken();
        using var httpClient = new HttpClient();

        // Act
        using var stream = await httpClient.GetStreamAsync("https://example.com", cancellationToken);
        using var reader = new StreamReader(stream);
        var content = await reader.ReadToEndAsync();

        // Assert
        Assert.AreEqual("Example Domain", content);
    }

    [Test]
    public void HttpClientGetStreamAsync_Negative()
    {
        // Arrange
        var cancellationToken = new CancellationToken(true);
        using var httpClient = new HttpClient();

        // Act
        var ex = Assert.CatchAsync<OperationCanceledException>(async () =>
            await httpClient.GetStreamAsync("https://example.com", cancellationToken)
        );

        // Assert
        Assert.IsNotNull(ex);
        Assert.AreEqual(cancellationToken, ex!.CancellationToken);
    }

    [Test]
    public async Task HttpClientGetByteArrayAsync_Positive()
    {
        // Arrange
        var cancellationToken = new CancellationToken();
        using var httpClient = new HttpClient();

        // Act
        var bytes = await httpClient.GetByteArrayAsync("https://example.com", cancellationToken);
        var content = Encoding.UTF8.GetString(bytes);

        // Assert
        Assert.AreEqual("Example Domain", content);
    }

    [Test]
    public void HttpClientGetByteArrayAsync_Negative()
    {
        // Arrange
        var cancellationToken = new CancellationToken(true);
        using var httpClient = new HttpClient();

        // Act
        var ex = Assert.CatchAsync<OperationCanceledException>(async () =>
            await httpClient.GetByteArrayAsync("https://example.com", cancellationToken)
        );

        // Assert
        Assert.IsNotNull(ex);
        Assert.AreEqual(cancellationToken, ex!.CancellationToken);
    }

    [Test]
    public async Task HttpClientGetStringAsync_Positive()
    {
        // Arrange
        var cancellationToken = new CancellationToken();
        using var httpClient = new HttpClient();

        // Act
        var content = await httpClient.GetStringAsync("https://example.com", cancellationToken);

        // Assert
        Assert.AreEqual("Example Domain", content);
    }

    [Test]
    public void HttpClientGetStringAsync_Negative()
    {
        // Arrange
        var cancellationToken = new CancellationToken(true);
        using var httpClient = new HttpClient();

        // Act
        var ex = Assert.CatchAsync<OperationCanceledException>(async () =>
            await httpClient.GetStringAsync("https://example.com", cancellationToken)
        );

        // Assert
        Assert.IsNotNull(ex);
        Assert.AreEqual(cancellationToken, ex!.CancellationToken);
    }
}