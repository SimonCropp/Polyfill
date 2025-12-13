partial class PolyfillTests
{
    [Test]
    public async Task StreamReaderReadAsync()
    {
        using var stream = new MemoryStream("value"u8.ToArray());
        var result = new char[5];
        var memory = new Memory<char>(result);
        using var reader = new StreamReader(stream);
        var read = await reader.ReadAsync(memory);
        await Assert.That(read).IsEqualTo(5);
        await Assert.That("value".SequenceEqual(result)).IsTrue();
    }

    [Test]
    public async Task StreamReaderReadToEndAsync()
    {
        using var stream = new MemoryStream("value"u8.ToArray());
        using var reader = new StreamReader(stream);
        var read = await reader.ReadToEndAsync(Cancel.None);
        await Assert.That(read).IsEqualTo("value");
    }

    [Test]
    public async Task StreamReaderReadLineAsync()
    {
        using var stream = new MemoryStream("line1\nline2"u8.ToArray());
        using var reader = new StreamReader(stream);
        var read = await reader.ReadLineAsync(Cancel.None);
        await Assert.That(read).IsEqualTo("line1");
    }
}
