partial class PolyfillTests
{
    [Test]
    public async Task Encoding_Latin1()
    {
        var latin1 = Encoding.Latin1;
        await Assert.That(latin1).IsNotNull();
        await Assert.That(latin1.CodePage).IsEqualTo(28591);
    }

#if FeatureMemory
    [Test]
    public async Task Encoding_Preamble()
    {
        var preamble = Encoding.UTF8.Preamble.ToArray();
        await Assert.That(preamble).IsEquivalentTo(new byte[] { 0xEF, 0xBB, 0xBF });
    }

    [Test]
    public async Task Encoding_GetByteCount()
    {
        var encoding = Encoding.UTF8;
        var chars = "Hello, World!".AsSpan();

        var byteCount = encoding.GetByteCount(chars);
        await Assert.That(byteCount).IsEqualTo(13);
    }

    [Test]
    public async Task Encoding_GetByteCount_EmptySource()
    {
        ReadOnlySpan<char> chars = default;

        var byteCount = Encoding.UTF8.GetByteCount(chars);

        await Assert.That(byteCount).IsEqualTo(0);
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
    public async Task Encoding_GetChars_MultiByte()
    {
        var encoding = Encoding.UTF8;
        var text = "héllo wörld";
        var utf8Bytes = encoding.GetBytes(text);
        // Decoded char count is smaller than the byte count for multi-byte input,
        // so a destination sized to the char count must not be over-written.
        var charCount = encoding.GetCharCount(utf8Bytes);
        var charArray = new char[charCount];
        var written = encoding.GetChars(new ReadOnlySpan<byte>(utf8Bytes), new Span<char>(charArray));

        await Assert.That(charCount).IsLessThan(utf8Bytes.Length);
        await Assert.That(written).IsEqualTo(charCount);
        await Assert.That(new string(charArray)).IsEqualTo(text);
    }

    [Test]
    public async Task Encoding_GetChars_EmptySource()
    {
        ReadOnlySpan<byte> bytes = default;
        var chars = new char[1];

        var charCount = Encoding.UTF8.GetChars(bytes, chars);

        await Assert.That(charCount).IsEqualTo(0);
    }

    [Test]
    public async Task Encoding_GetChars_EmptyDestination()
    {
        ReadOnlySpan<byte> bytes = "value"u8;
        Span<char> chars = default;
        Exception? exception = null;

        try
        {
            Encoding.UTF8.GetChars(bytes, chars);
        }
        catch (Exception caught)
        {
            exception = caught;
        }

        await Assert.That(exception?.GetType()).IsEqualTo(typeof(ArgumentException));
    }

    [Test]
    public async Task Encoding_GetString()
    {
        var array = (ReadOnlySpan<byte>)"value"u8.ToArray().AsSpan();
        var result = Encoding.UTF8.GetString(array);
        await Assert.That(result).IsEqualTo("value");
    }

    [Test]
    public async Task Encoding_GetString_EmptySource()
    {
        ReadOnlySpan<byte> bytes = default;

        var result = Encoding.UTF8.GetString(bytes);

        await Assert.That(result).IsEmpty();
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
    public async Task Encoding_GetBytes_EmptySource()
    {
        ReadOnlySpan<char> chars = default;
        var bytes = new byte[1];

        var byteCount = Encoding.UTF8.GetBytes(chars, bytes);

        await Assert.That(byteCount).IsEqualTo(0);
    }

    [Test]
    public async Task Encoding_GetBytes_EmptyDestination()
    {
        ReadOnlySpan<char> chars = "value";
        Span<byte> bytes = default;
        Exception? exception = null;

        try
        {
            Encoding.UTF8.GetBytes(chars, bytes);
        }
        catch (Exception caught)
        {
            exception = caught;
        }

        await Assert.That(exception?.GetType()).IsEqualTo(typeof(ArgumentException));
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
    public async Task Encoding_GetCharCount()
    {
        var encoding = Encoding.UTF8;
        var utf8Bytes = "Hello, World!"u8.ToArray();
        var byteSpan = new ReadOnlySpan<byte>(utf8Bytes);
        var charCount = encoding.GetCharCount(byteSpan);
        await Assert.That(charCount).IsEqualTo(13);
    }

    [Test]
    public async Task Encoding_GetCharCount_EmptySource()
    {
        ReadOnlySpan<byte> bytes = default;

        var charCount = Encoding.UTF8.GetCharCount(bytes);

        await Assert.That(charCount).IsEqualTo(0);
    }

    [Test]
    public Task TryGetBytes_WithValidInput_ReturnsTrue()
    {
        var encoding = Encoding.UTF8;
        var chars = "Hello, World!".AsSpan();
        var bytes = new byte[encoding.GetByteCount(chars)].AsSpan();

        var result = encoding.TryGetBytes(chars, bytes, out var written);

        if (!result)
        {
            throw new("Expected result to be true");
        }

        if (written != 13)
        {
            throw new($"Expected 13 bytes written but got {written}");
        }

        return Task.CompletedTask;
    }

    [Test]
    public Task TryGetBytes_WithSmallDestination_ReturnsFalse()
    {
        var encoding = Encoding.UTF8;
        var chars = "Hello, World!".AsSpan();
        var bytes = new byte[2].AsSpan();

        var result = encoding.TryGetBytes(chars, bytes, out var written);

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
