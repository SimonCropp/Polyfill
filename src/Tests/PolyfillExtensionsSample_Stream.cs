partial class PolyfillExtensionsSample
{
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
