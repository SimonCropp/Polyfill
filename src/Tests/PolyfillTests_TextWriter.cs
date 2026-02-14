partial class PolyfillTests
{
    [Test]
    public async Task TextWriterWriteSpan()
    {
        using var stream = new MemoryStream();
        using var writer = new StreamWriter(stream);
        writer.Write("value".AsSpan());
        await writer.FlushAsync();
        var s = Encoding.UTF8.GetString(stream.ToArray());
        await Assert.That(s).IsEqualTo("value");
    }

    [Test]
    public async Task TextWriterFlushAsync()
    {
        using var stream = new MemoryStream();
        using var writer = new StreamWriter(stream);
        await writer.WriteAsync("value");
        await writer.FlushAsync(Cancel.None);
        var s = Encoding.UTF8.GetString(stream.ToArray());
        await Assert.That(s).IsEqualTo("value");
    }

    [Test]
    public async Task TextWriterWriteStringBuilder()
    {
        using var stream = new MemoryStream();
        using var writer = new StreamWriter(stream);
        // ReSharper disable once MethodHasAsyncOverload
        writer.Write(new StringBuilder("value"));
        await writer.FlushAsync();
        var s = Encoding.UTF8.GetString(stream.ToArray());
        await Assert.That(s).IsEqualTo("value");
    }

    [Test]
    public async Task TextWriterWriteStringBuilderAsync()
    {
        using var stream = new MemoryStream();
        using var writer = new StreamWriter(stream);
        await writer.WriteAsync(new StringBuilder("value"));
        await writer.FlushAsync();
        var s = Encoding.UTF8.GetString(stream.ToArray());
        await Assert.That(s).IsEqualTo("value");
    }

    [Test]
    public async Task TextWriterWriteLineSpan()
    {
        using var stream = new MemoryStream();
        using var writer = new StreamWriter(stream);
        writer.WriteLine("value".AsSpan());
        await writer.FlushAsync();
        var s = Encoding.UTF8.GetString(stream.ToArray());
        await Assert.That(s).IsEqualTo("value" + Environment.NewLine);
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
        await Assert.That(s).IsEqualTo("value");
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
        await Assert.That(s).IsEqualTo("value" + Environment.NewLine);
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
        await Assert.That(s).IsEqualTo("value");
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
        await Assert.That(s).IsEqualTo("value" + Environment.NewLine);
    }

    [Test]
    public async Task TextWriterWriteAsyncStringWithCancellation()
    {
        using var stream = new MemoryStream();
        using var writer = new StreamWriter(stream);
        await writer.WriteAsync("value", Cancel.None);
        await writer.FlushAsync();
        var s = Encoding.UTF8.GetString(stream.ToArray());
        await Assert.That(s).IsEqualTo("value");
    }

    [Test]
    public async Task TextWriterWriteLineAsyncWithCancellation()
    {
        using var stream = new MemoryStream();
        using var writer = new StreamWriter(stream);
        await writer.WriteAsync("value");
        await writer.WriteLineAsync(Cancel.None);
        await writer.FlushAsync();
        var s = Encoding.UTF8.GetString(stream.ToArray());
        await Assert.That(s).IsEqualTo("value" + Environment.NewLine);
    }

    [Test]
    public async Task TextWriterWriteLineAsyncStringWithCancellation()
    {
        using var stream = new MemoryStream();
        using var writer = new StreamWriter(stream);
        await writer.WriteLineAsync("value", Cancel.None);
        await writer.FlushAsync();
        var s = Encoding.UTF8.GetString(stream.ToArray());
        await Assert.That(s).IsEqualTo("value" + Environment.NewLine);
    }
}
