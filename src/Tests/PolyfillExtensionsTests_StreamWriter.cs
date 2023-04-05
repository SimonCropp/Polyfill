partial class PolyfillExtensionsTests
{
    [Test]
    public async Task StreamWriterWriteAsync()
    {
        using var stream = new MemoryStream();
        var memory = new Memory<char>("value".ToArray());
        using var writer = new StreamWriter(stream);
        await writer.WriteAsync(memory);
        await writer.FlushAsync();
        var s = Encoding.UTF8.GetString(stream.ToArray());
        Assert.AreEqual("value", s);
    }

    [Test]
    public async Task StreamWriterWriteLineAsync()
    {
        using var stream = new MemoryStream();
        var memory = new Memory<char>("value".ToArray());
        using var writer = new StreamWriter(stream);
        await writer.WriteLineAsync(memory);
        await writer.FlushAsync();
        var s = Encoding.UTF8.GetString(stream.ToArray());
        Assert.AreEqual("value" + Environment.NewLine, s);
    }
}
