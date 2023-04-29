partial class PolyfillExtensionsTests
{
    [Test]
    public async Task TextWriterWriteAsync()
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
    public async Task TextWriterWriteLineAsync()
    {
        using var stream = new MemoryStream();
        var memory = new Memory<char>("value".ToArray());
        using var writer = new StreamWriter(stream);
        await writer.WriteLineAsync(memory);
        await writer.FlushAsync();
        var s = Encoding.UTF8.GetString(stream.ToArray());
        Assert.AreEqual("value" + Environment.NewLine, s);
    }
    [Test]
    public async Task TextWriterWrite()
    {
        using var stream = new MemoryStream();
        var memory = new Memory<char>("value".ToArray());
        using var writer = new StreamWriter(stream);
        writer.Write(memory);
        await writer.FlushAsync();
        var s = Encoding.UTF8.GetString(stream.ToArray());
        Assert.AreEqual("value", s);
    }

    [Test]
    public async Task TextWriterWriteLine()
    {
        using var stream = new MemoryStream();
        var memory = new Memory<char>("value".ToArray());
        using var writer = new StreamWriter(stream);
        writer.WriteLine(memory);
        await writer.FlushAsync();
        var s = Encoding.UTF8.GetString(stream.ToArray());
        Assert.AreEqual("value" + Environment.NewLine, s);
    }
}
