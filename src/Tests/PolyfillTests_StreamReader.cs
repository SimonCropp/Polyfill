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
        Assert.AreEqual(5, read);
        Assert.IsTrue("value".SequenceEqual(result));
    }

    [Test]
    public async Task StreamReaderReadToEndAsync()
    {
        using var stream = new MemoryStream("value"u8.ToArray());
        using var reader = new StreamReader(stream);
        var read = await reader.ReadToEndAsync(Cancel.None);
        Assert.AreEqual("value", read);
    }
}
