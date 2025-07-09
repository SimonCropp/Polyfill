[TestFixture]
public class XElementPolyfillTests
{
    const string XmlContent = "<root><child>value</child></root>";

    [Test]
    public async Task LoadAsync_Stream_ReturnsXElement()
    {
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(XmlContent));
        var result = await XElement.LoadAsync(stream, LoadOptions.None, Cancel.None);
        Assert.AreEqual("root", result.Name.LocalName);
        Assert.AreEqual("value", result.Element("child")?.Value);
    }

    [Test]
    public async Task LoadAsync_TextReader_ReturnsXElement()
    {
        using var reader = new StringReader(XmlContent);
        var result = await XElement.LoadAsync(reader, LoadOptions.None, Cancel.None);
        Assert.AreEqual("root", result.Name.LocalName);
        Assert.AreEqual("value", result.Element("child")?.Value);
    }

    [Test]
    public async Task LoadAsync_XmlReader_ReturnsXElement()
    {
        using var stringReader = new StringReader(XmlContent);
        using var xmlReader = XmlReader.Create(stringReader, new()
        {
            Async = true
        });
        var result = await XElement.LoadAsync(xmlReader, LoadOptions.None, Cancel.None);
        Assert.AreEqual("root", result.Name.LocalName);
        Assert.AreEqual("value", result.Element("child")?.Value);
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
            await XElement.LoadAsync(stream, LoadOptions.None, tokenSource.Token);
        }
        catch (Exception e)
        {
            exception = e;
        }

        Assert.IsNotNull(exception);
    }
}