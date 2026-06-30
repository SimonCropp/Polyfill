partial class PolyfillTests
{
    [Test]
    public async Task XElement_LoadAsync_HonorsDeclaredEncoding()
    {
        var accentedE = (char) 0x00E9;
        var xml = $"<?xml version=\"1.0\" encoding=\"utf-16\"?><root>caf{accentedE}</root>";
        // UTF-16 LE, no BOM: a UTF-8 StreamReader would mis-decode this.
        var bytes = Encoding.Unicode.GetBytes(xml);

        using var stream = new MemoryStream(bytes);
        var element = await XElement.LoadAsync(stream, LoadOptions.None, CancellationToken.None);
        await Assert.That(element.Value).IsEqualTo($"caf{accentedE}");
    }

    [Test]
    public async Task XElementSaveAsyncXmlWriter()
    {
        var document = new XElement("Child");
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
    public async Task XElementSaveAsyncStream()
    {
        var document = new XElement("Child");
        using var stream = new MemoryStream();

        await document.SaveAsync(stream, SaveOptions.DisableFormatting, Cancel.None);
        stream.Position = 0;
        using var reader = new StreamReader(stream);
        var result = await reader.ReadToEndAsync();

        await Assert.That(result).IsEqualTo("<?xml version=\"1.0\" encoding=\"utf-8\"?><Child />");
    }

    [Test]
    public async Task XElementSaveAsyncTextWriter()
    {
        var document = new XElement("Child");
        using var writer = new StringWriter();

        await document.SaveAsync(writer, SaveOptions.DisableFormatting, Cancel.None);

        await Assert.That(writer.ToString()).IsEqualTo("<?xml version=\"1.0\" encoding=\"utf-16\"?><Child />");
    }
}
