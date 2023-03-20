partial class PolyfillExtensionsSample
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
    public async Task StreamReadAsync()
    {
        var input = new byte[]{1,2};
        using var stream = new MemoryStream(input);
        var result = new byte[2];
        var memory = new Memory<byte>(result);
        var read = await stream.ReadAsync(memory);
        Assert.AreEqual(2, read);
        Assert.IsTrue(input.SequenceEqual(result));
    }
}
