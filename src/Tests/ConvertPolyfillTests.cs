public class ConvertPolyfillTests
{
    [Test]
    public async Task ToHexString_ValidInput()
    {
        byte[] input = [0x0F, 0xA3, 0x5C];
        var result = Convert.ToHexString(input, 0, input.Length);
        await Assert.That(result).IsEqualTo("0FA35C");
        result = Convert.ToHexString(input);
        await Assert.That(result).IsEqualTo("0FA35C");
#if FeatureMemory
        result = Convert.ToHexString(input.AsSpan());
        await Assert.That(result).IsEqualTo("0FA35C");
#endif
    }

    [Test]
    public async Task ToHexString_OffsetAndLength()
    {
        byte[] input = [0x0F, 0xA3, 0x5C, 0x7E];
        var result = Convert.ToHexString(input, 1, 2);
        await Assert.That(result).IsEqualTo("A35C");
    }

    [Test]
    public async Task ToHexString_NegativeLength_ThrowsException()
    {
        byte[] input = [0x0F, 0xA3, 0x5C];
        await Assert.That(() => Convert.ToHexString(input, 0, -1)).Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task ToHexString_NegativeOffset_ThrowsException()
    {
        byte[] input = [0x0F, 0xA3, 0x5C];
        await Assert.That(() => Convert.ToHexString(input, -1, 2)).Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task ToHexString_OffsetPlusLengthExceedsArray_ThrowsException()
    {
        byte[] input = [0x0F, 0xA3, 0x5C];
        await Assert.That(() => Convert.ToHexString(input, 2, 2)).Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task ToHexStringLower_ValidInput()
    {
        byte[] input = [0x0F, 0xA3, 0x5C];
        var result = Convert.ToHexString(input, 0, input.Length);
        await Assert.That(result).IsEqualTo("0FA35C");
        result = Convert.ToHexStringLower(input);
        await Assert.That(result).IsEqualTo("0fa35c");
#if FeatureMemory
        result = Convert.ToHexStringLower(input.AsSpan());
        await Assert.That(result).IsEqualTo("0fa35c");
#endif
    }

    [Test]
    public async Task ToHexStringLower_OffsetAndLength()
    {
        byte[] input = [0x0F, 0xA3, 0x5C, 0x7E];
        var result = Convert.ToHexStringLower(input, 1, 2);
        await Assert.That(result).IsEqualTo("a35c");
    }

    [Test]
    public async Task ToHexStringLower_NegativeLength_ThrowsException()
    {
        byte[] input = [0x0F, 0xA3, 0x5C];
        await Assert.That(() => Convert.ToHexStringLower(input, 0, -1)).Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task ToHexStringLower_NegativeOffset_ThrowsException()
    {
        byte[] input = [0x0F, 0xA3, 0x5C];
        await Assert.That(() => Convert.ToHexStringLower(input, -1, 2)).Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task ToHexStringLower_OffsetPlusLengthExceedsArray_ThrowsException()
    {
        byte[] input = [0x0F, 0xA3, 0x5C];
        await Assert.That(() => Convert.ToHexStringLower(input, 2, 2)).Throws<ArgumentOutOfRangeException>();
    }

#if FeatureMemory

    [Test]
    public Task TryToHexString_ValidInput()
    {
        ReadOnlySpan<byte> source = [0x0F, 0xA3, 0x5C];
        Span<char> destination = stackalloc char[6];
        var result = Convert.TryToHexString(source, destination, out var charsWritten);
        if (!result)
            throw new("Expected true");
        if (charsWritten != 6)
        {
            throw new($"Expected charsWritten 6 but got {charsWritten}");
        }

        if (destination.Slice(0, charsWritten).ToString() != "0FA35C")
        {
            throw new("Expected 0FA35C");
        }

        return Task.CompletedTask;
    }

    [Test]
    public Task TryToHexString_BufferTooSmall()
    {
        ReadOnlySpan<byte> source = [0x0F, 0xA3, 0x5C];
        Span<char> destination = stackalloc char[4];
        var result = Convert.TryToHexString(source, destination, out var charsWritten);
        if (result)
        {
            throw new("Expected false");
        }

        if (charsWritten != 0)
        {
            throw new($"Expected charsWritten 0 but got {charsWritten}");
        }

        return Task.CompletedTask;
    }

    [Test]
    public Task TryToHexString_EmptySource()
    {
        var source = ReadOnlySpan<byte>.Empty;
        Span<char> destination = [];
        var result = Convert.TryToHexString(source, destination, out var charsWritten);
        if (!result)
        {
            throw new("Expected true");
        }

        if (charsWritten != 0)
        {
            throw new($"Expected charsWritten 0 but got {charsWritten}");
        }

        return Task.CompletedTask;
    }

    [Test]
    public Task TryToHexStringLower_ValidInput()
    {
        ReadOnlySpan<byte> source = [0x0F, 0xA3, 0x5C];
        Span<char> destination = stackalloc char[6];
        var result = Convert.TryToHexStringLower(source, destination, out var charsWritten);
        if (!result)
        {
            throw new("Expected true");
        }

        if (charsWritten != 6)
        {
            throw new($"Expected charsWritten 6 but got {charsWritten}");
        }

        if (destination.Slice(0, charsWritten).ToString() != "0fa35c")
        {
            throw new("Expected 0fa35c");
        }

        return Task.CompletedTask;
    }

    [Test]
    public Task TryToHexStringLower_BufferTooSmall()
    {
        ReadOnlySpan<byte> source = [0x0F, 0xA3, 0x5C];
        Span<char> destination = stackalloc char[4];
        var result = Convert.TryToHexStringLower(source, destination, out var charsWritten);
        if (result)
            throw new("Expected false");
        if (charsWritten != 0)
            throw new($"Expected charsWritten 0 but got {charsWritten}");
        return Task.CompletedTask;
    }

    [Test]
    public Task TryToHexStringLower_EmptySource()
    {
        var source = ReadOnlySpan<byte>.Empty;
        Span<char> destination = [];
        var result = Convert.TryToHexStringLower(source, destination, out var charsWritten);
        if (!result)
            throw new("Expected true");
        if (charsWritten != 0)
            throw new($"Expected charsWritten 0 but got {charsWritten}");
        return Task.CompletedTask;
    }

#endif

    [Test]
    public async Task FromHexString_ValidInput()
    {
        var hexString = "0FA35C";
        var result = Convert.FromHexString(hexString);
        await Assert.That(result.SequenceEqual(new byte[]
        {
            0x0F,
            0xA3,
            0x5C
        })).IsTrue();
    }

    [Test]
    public async Task FromHexString_EmptyString()
    {
        var hexString = string.Empty;
        var result = Convert.FromHexString(hexString);
        await Assert.That(result.SequenceEqual(Array.Empty<byte>())).IsTrue();
    }

    [Test]
    public async Task FromHexString_InvalidCharacter_ThrowsFormatException()
    {
        var hexString = "0FA3ZC";
        await Assert.That(() => Convert.FromHexString(hexString)).Throws<FormatException>();
    }

    [Test]
    public async Task FromHexString_OddLength_ThrowsFormatException()
    {
        var hexString = "0FA3C";
        await Assert.That(() => Convert.FromHexString(hexString)).Throws<FormatException>();
    }

    [Test]
    public async Task FromHexString_LowercaseInput()
    {
        var hexString = "0fa35c";
        var result = Convert.FromHexString(hexString);
        await Assert.That(result.SequenceEqual(new byte[] { 0x0F, 0xA3, 0x5C })).IsTrue();
    }
}
