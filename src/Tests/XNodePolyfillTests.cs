public class XNodePolyfillTests
{
    [Test]
    public async Task ReadFromAsync_ReturnsXNode()
    {
        using var stringReader = new StringReader("<root><child>value</child></root>");
        using var xmlReader = XmlReader.Create(
            stringReader,
            new()
            {
                Async = true
            });
        xmlReader.Read();
        var result = await XNode.ReadFromAsync(xmlReader, Cancel.None);
        await Assert.That(result).IsTypeOf<XElement>();
        var element = (XElement) result;
        await Assert.That(element.Name.LocalName).IsEqualTo("root");
    }
}
