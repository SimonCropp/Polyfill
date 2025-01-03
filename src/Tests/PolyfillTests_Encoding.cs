partial class PolyfillTests
{
#if FeatureMemory
    [Test]
    public void Encoding_GetByteCount()
    {
        var encoding = Encoding.UTF8;
        var chars = "Hello, World!".AsSpan();

        var byteCount = encoding.GetByteCount(chars);
        Assert.AreEqual(13, byteCount);
    }

    [Test]
    public void Encoding_GetString()
    {
        var array = (ReadOnlySpan<byte>)"value"u8.ToArray().AsSpan();
        var result = Encoding.UTF8.GetString(array);
        Assert.AreEqual("value", result);
    }

    [Test]
    public void Encoding_GetBytes()
    {
        var encoding = Encoding.UTF8;
        var chars = "Hello, World!".AsSpan();
        var bytes = new byte[encoding.GetByteCount(chars)].AsSpan();

        var byteCount = encoding.GetBytes(chars, bytes);

        Assert.AreEqual(encoding.GetByteCount(chars), byteCount);
        Assert.AreEqual(encoding.GetBytes("Hello, World!"), bytes.ToArray());
    }

#endif
}