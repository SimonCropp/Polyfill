#if FeatureHttp
using System.IO;
using System.Net;
using System.Net.Http;

partial class PolyfillTests
{
    [Test]
    public async Task HttpContent_ReadAsStringAsync_WithCancellation()
    {
        using var content = new StringContent("test data");
        var result = await content.ReadAsStringAsync(CancellationToken.None);
        await Assert.That(result).IsEqualTo("test data");
    }

    [Test]
    public async Task HttpContent_ReadAsByteArrayAsync_WithCancellation()
    {
        using var content = new StringContent("test");
        var result = await content.ReadAsByteArrayAsync(CancellationToken.None);
        await Assert.That(result.Length).IsGreaterThan(0);
    }

    [Test]
    public async Task HttpContent_CopyTo()
    {
        using var content = new StringContent("test data");
        using var destination = new MemoryStream();
        content.CopyTo(destination, null, CancellationToken.None);
        await Assert.That(destination.Length).IsGreaterThan(0);
    }

    [Test]
    public async Task HttpContent_ReadAsStream()
    {
        using var content = new StringContent("test data");
        using var stream = content.ReadAsStream();
        await Assert.That(stream).IsNotNull();
        await Assert.That(stream.CanRead).IsTrue();
    }

    [Test]
    public async Task HttpContent_ReadAsStream_WithCancellation()
    {
        using var content = new StringContent("test data");
        using var stream = content.ReadAsStream(CancellationToken.None);
        await Assert.That(stream).IsNotNull();
        await Assert.That(stream.CanRead).IsTrue();
    }

    [Test]
    public async Task HttpContent_ReadAsStreamAsync_WithCancellation()
    {
        using var content = new StringContent("test");
        using var stream = await content.ReadAsStreamAsync(CancellationToken.None);
        await Assert.That(stream).IsNotNull();
        await Assert.That(stream.CanRead).IsTrue();
    }
}
#endif
