partial class PolyfillTests
{
    [Test]
    public async Task TextReader_ReadToEndAsync_WithCancellation()
    {
        using var reader = new StringReader("hello world");
        var result = await reader.ReadToEndAsync(CancellationToken.None);
        await Assert.That(result).IsEqualTo("hello world");
    }

    [Test]
    public async Task TextReader_ReadLineAsync_WithCancellation()
    {
        using var reader = new StringReader("line1\nline2");
        var result = await reader.ReadLineAsync(CancellationToken.None);
        await Assert.That(result).IsEqualTo("line1");
    }

#if FeatureMemory
    [Test]
    public async Task TextReader_ReadAsync_Memory()
    {
        using var reader = new StringReader("hello");
        var buffer = new char[5];
        var read = await reader.ReadAsync(buffer.AsMemory());
        await Assert.That(read).IsEqualTo(5);
        await Assert.That(new string(buffer)).IsEqualTo("hello");
    }
#endif
}
