public class XDocumentPolyfillTests
{
    const string XmlContent = "<root><child>value</child></root>";

    [Test]
    public async Task LoadAsync_Stream_ReturnsXDocument()
    {
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(XmlContent));
        var result = await XDocument.LoadAsync(stream, LoadOptions.None, CancellationToken.None);
        Assert.AreEqual("root", result.Root?.Name.LocalName);
        Assert.AreEqual("value", result.Root?.Element("child")?.Value);
    }

    [Test]
    public async Task LoadAsync_TextReader_ReturnsXDocument()
    {
        using var reader = new StringReader(XmlContent);
        var result = await XDocument.LoadAsync(reader, LoadOptions.None, CancellationToken.None);
        Assert.AreEqual("root", result.Root?.Name.LocalName);
        Assert.AreEqual("value", result.Root?.Element("child")?.Value);
    }

    [Test]
    public async Task LoadAsync_XmlReader_ReturnsXDocument()
    {
        using var stringReader = new StringReader(XmlContent);
        using var xmlReader = XmlReader.Create(
            stringReader,
            new()
            {
                Async = true
            });
        var result = await XDocument.LoadAsync(xmlReader, LoadOptions.None, CancellationToken.None);
        Assert.AreEqual("root", result.Root?.Name.LocalName);
        Assert.AreEqual("value", result.Root?.Element("child")?.Value);
    }

    [Test]
    public async Task LoadAsync_Stream_CancellationRequested_Throws()
    {
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(XmlContent));
        var tokenSource = new CancelSource();
        tokenSource.Cancel();
        Exception? exception = null;
        try
        {
            await XDocument.LoadAsync(stream, LoadOptions.None, tokenSource.Token);
        }
        catch (Exception e)
        {
            exception = e;
        }

        Assert.IsNotNull(exception);
    }
}