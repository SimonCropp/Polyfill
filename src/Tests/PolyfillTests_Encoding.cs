partial class PolyfillTests
{
#if FeatureMemory
    [Test]
    public async Task Encoding_GetByteCount()
    {
        var encoding = Encoding.UTF8;
        var chars = "Hello, World!".AsSpan();

        var byteCount = encoding.GetByteCount(chars);
        await Assert.That(byteCount).IsEqualTo(13);
    }

    [Test]
    public async Task Encoding_GetChars()
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
        await Assert.That(result).IsEqualTo("Hello, World!");
    }

    [Test]
    public async Task Encoding_GetString()
    {
        var array = (ReadOnlySpan<byte>)"value"u8.ToArray().AsSpan();
        var result = Encoding.UTF8.GetString(array);
        await Assert.That(result).IsEqualTo("value");
    }

    [Test]
    public Task Encoding_GetBytes()
    {
        var encoding = Encoding.UTF8;
        var chars = "Hello, World!".AsSpan();
        var bytes = new byte[encoding.GetByteCount(chars)].AsSpan();

        var byteCount = encoding.GetBytes(chars, bytes);

        if (byteCount != encoding.GetByteCount(chars))
        {
            throw new($"Expected {encoding.GetByteCount(chars)} but got {byteCount}");
        }

        if (!bytes.ToArray().SequenceEqual(encoding.GetBytes("Hello, World!")))
        {
            throw new("Bytes do not match expected");
        }

        return Task.CompletedTask;
    }

    [Test]
    public Task TryGetChars_WithValidInput_ReturnsTrue()
    {
        // Arrange
        var encoding = Encoding.UTF8;
        var utf8Bytes = "Hello, World!"u8.ToArray();
        var byteSpan = new ReadOnlySpan<byte>(utf8Bytes);
        var charArray = new char[utf8Bytes.Length];
        var charSpan = new Span<char>(charArray);

        // Act
        var result = encoding.TryGetChars(byteSpan, charSpan, out var written);

        // Assert
        if (!result)
        {
            throw new("Expected result to be true");
        }

        if (charSpan.Slice(0, written).ToString() != "Hello, World!")
        {
            throw new("Expected 'Hello, World!'");
        }

        return Task.CompletedTask;
    }

    [Test]
    public Task TryGetChars_WithSmallDestination_ReturnsFalse()
    {
        // Arrange
        var encoding = Encoding.UTF8;
        var utf8Bytes = "Hello, World!"u8.ToArray();
        var byteSpan = new ReadOnlySpan<byte>(utf8Bytes);
        // Smaller than needed
        var charArray = new char[5];
        var charSpan = new Span<char>(charArray);

        // Act
        var result = encoding.TryGetChars(byteSpan, charSpan, out var written);

        // Assert
        if (result)
        {
            throw new("Expected result to be false");
        }

        if (written != 0)
        {
            throw new($"Expected written to be 0 but got {written}");
        }

        return Task.CompletedTask;
    }
#endif
}
