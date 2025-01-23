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
    public void Encoding_GetChars()
    {
        // Arrange
        var encoding = Encoding.UTF8;
        var utf8Bytes = "Hello, World!"u8.ToArray();
        var byteSpan = new ReadOnlySpan<byte>(utf8Bytes);
        var charArray = new char[utf8Bytes.Length];
        var charSpan = new Span<char>(charArray);

        // Act
        var charCount = encoding.GetChars(byteSpan, charSpan);

        // Assert
        var result = charSpan.Slice(0, charCount).ToString();
        Assert.AreEqual("Hello, World!", result);
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