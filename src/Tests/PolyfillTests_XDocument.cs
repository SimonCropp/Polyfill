partial class PolyfillTests
{
    [Test]
    public async Task XDocumentSaveAsyncXmlWriter()
    {
        var document = GetXDocument();
        var builder = new StringBuilder();
        var settings = new XmlWriterSettings
        {
            OmitXmlDeclaration = true,
            Indent = true,
            Async = true
        };

        using var writer = XmlWriter.Create(builder, settings);
        await document.SaveAsync(writer, Cancel.None);
        await writer.FlushAsync();
        await Assert.That(builder.ToString()).IsEqualTo("<Child />");
    }

    [Test]
    public async Task XDocumentSaveAsyncStream()
    {
        var document = GetXDocument();
        using var stream = new MemoryStream();

        await document.SaveAsync(stream, SaveOptions.DisableFormatting, Cancel.None);
        stream.Position = 0;
        using var reader = new StreamReader(stream);
        var result = await reader.ReadToEndAsync();

        await Assert.That(result).IsEqualTo("<?xml version=\"1.0\" encoding=\"utf-8\"?><Child />");
    }

    static XDocument GetXDocument() =>
        new(new XElement("Child"));
}
