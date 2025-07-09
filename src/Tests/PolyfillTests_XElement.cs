using System.Xml;
using System.Xml.Linq;

partial class PolyfillTests
{
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
        Assert.AreEqual("<Child />", builder.ToString());
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

        Assert.AreEqual("<?xml version=\"1.0\" encoding=\"utf-8\"?><Child />", result);
    }
}