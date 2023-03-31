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
        StringAssert.Contains("Example Domain", content);
    }

    [Test]
    public async Task HttpClientGetStreamAsync_Negative()
    {
        // Arrange
        var cancellationToken = new CancellationToken(true);
        using var httpClient = new HttpClient();

        // Act
        try
        {
            await httpClient.GetStreamAsync("https://example.com", cancellationToken);
            Assert.Fail();
        }
        catch (OperationCanceledException ex)
        {
            // Assert
            Assert.AreEqual(cancellationToken, ex.CancellationToken);
        }
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
        StringAssert.Contains("Example Domain", content);
    }

    [Test]
    public async Task HttpClientGetByteArrayAsync_Negative()
    {
        // Arrange
        var cancellationToken = new CancellationToken(true);
        using var httpClient = new HttpClient();

        // Act
        try
        {
            await httpClient.GetByteArrayAsync("https://example.com", cancellationToken);
            Assert.Fail();
        }
        catch (OperationCanceledException ex)
        {
            // Assert
            Assert.AreEqual(cancellationToken, ex.CancellationToken);
        }
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
        StringAssert.Contains("Example Domain", content);
    }

    [Test]
    public async Task HttpClientGetStringAsync_Negative()
    {
        // Arrange
        var cancellationToken = new CancellationToken(true);
        using var httpClient = new HttpClient();

        // Act
        try
        {
            await httpClient.GetStringAsync("https://example.com", cancellationToken);
            Assert.Fail();
        }
        catch (OperationCanceledException ex)
        {
            // Assert
            Assert.AreEqual(cancellationToken, ex.CancellationToken);
        }
    }
}