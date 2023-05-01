partial class PolyfillExtensionsTests
{
    [Test]
    public async Task TextWriterWriteSpan()
    {
        using var stream = new MemoryStream();
        using var writer = new StreamWriter(stream);
        writer.Write("value".AsSpan());
        await writer.FlushAsync();
        var s = Encoding.UTF8.GetString(stream.ToArray());
        Assert.AreEqual("value", s);
    }

    [Test]
    public async Task TextWriterWriteLineSpan()
    {
        using var stream = new MemoryStream();
        using var writer = new StreamWriter(stream);
        writer.WriteLine("value".AsSpan());
        await writer.FlushAsync();
        var s = Encoding.UTF8.GetString(stream.ToArray());
        Assert.AreEqual("value" + Environment.NewLine, s);
    }

    [Test]
    public async Task TextWriterWriteMemoryAsync()
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
    public async Task TextWriterWriteLineMemoryAsync()
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
    public async Task TextWriterWriteMemory()
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
    public async Task TextWriterWriteLineMemory()
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
