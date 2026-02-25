partial class PolyfillTests
{
    [Test]
    public async Task XNodeWriteToAsyncXElement()
    {
        var element = new XElement("Child");
        var builder = new StringBuilder();
        var settings = new XmlWriterSettings
        {
            OmitXmlDeclaration = true,
            Async = true
        };

        using var writer = XmlWriter.Create(builder, settings);
        await element.WriteToAsync(writer, Cancel.None);
        await writer.FlushAsync();
        await Assert.That(builder.ToString()).IsEqualTo("<Child />");
    }

    [Test]
    public async Task XNodeWriteToAsyncXDocument()
    {
        var document = new XDocument(new XElement("Child"));
        var builder = new StringBuilder();
        var settings = new XmlWriterSettings
        {
            OmitXmlDeclaration = true,
            Async = true
        };

        using var writer = XmlWriter.Create(builder, settings);
        await document.WriteToAsync(writer, Cancel.None);
        await writer.FlushAsync();
        await Assert.That(builder.ToString()).IsEqualTo("<Child />");
    }

    [Test]
    public async Task XNodeWriteToAsyncXComment()
    {
        var comment = new XComment("test comment");
        var builder = new StringBuilder();
        var settings = new XmlWriterSettings
        {
            OmitXmlDeclaration = true,
            ConformanceLevel = ConformanceLevel.Fragment,
            Async = true
        };

        using var writer = XmlWriter.Create(builder, settings);
        await comment.WriteToAsync(writer, Cancel.None);
        await writer.FlushAsync();
        await Assert.That(builder.ToString()).IsEqualTo("<!--test comment-->");
    }

    [Test]
    public async Task XNodeWriteToAsyncXCData()
    {
        var cdata = new XCData("cdata content");
        var builder = new StringBuilder();
        var settings = new XmlWriterSettings
        {
            OmitXmlDeclaration = true,
            ConformanceLevel = ConformanceLevel.Fragment,
            Async = true
        };

        using var writer = XmlWriter.Create(builder, settings);
        await cdata.WriteToAsync(writer, Cancel.None);
        await writer.FlushAsync();
        await Assert.That(builder.ToString()).IsEqualTo("<![CDATA[cdata content]]>");
    }
}
