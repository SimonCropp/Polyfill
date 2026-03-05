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
    public Task TryFromBase64Chars_ValidInput()
    {
        ReadOnlySpan<char> chars = "SGVsbG8=".AsSpan();
        Span<byte> bytes = stackalloc byte[5];
        var result = Convert.TryFromBase64Chars(chars, bytes, out var bytesWritten);
        if (!result)
            throw new("Expected true");
        if (bytesWritten != 5)
            throw new($"Expected bytesWritten 5 but got {bytesWritten}");
        var expected = "Hello"u8;
        for (var i = 0; i < bytesWritten; i++)
        {
            if (bytes[i] != expected[i])
                throw new($"Mismatch at index {i}");
        }

        return Task.CompletedTask;
    }

    [Test]
    public Task TryFromBase64Chars_InvalidInput()
    {
        ReadOnlySpan<char> chars = "!!!".AsSpan();
        Span<byte> bytes = stackalloc byte[10];
        var result = Convert.TryFromBase64Chars(chars, bytes, out var bytesWritten);
        if (result)
            throw new("Expected false");
        if (bytesWritten != 0)
            throw new($"Expected bytesWritten 0 but got {bytesWritten}");
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFromBase64Chars_BufferTooSmall()
    {
        ReadOnlySpan<char> chars = "SGVsbG8=".AsSpan();
        Span<byte> bytes = stackalloc byte[2];
        var result = Convert.TryFromBase64Chars(chars, bytes, out var bytesWritten);
        if (result)
            throw new("Expected false");
        if (bytesWritten != 0)
            throw new($"Expected bytesWritten 0 but got {bytesWritten}");
        return Task.CompletedTask;
    }

    [Test]
    public Task TryFromBase64String_ValidInput()
    {
        Span<byte> bytes = stackalloc byte[5];
        var result = Convert.TryFromBase64String("SGVsbG8=", bytes, out var bytesWritten);
        if (!result)
            throw new("Expected true");
        if (bytesWritten != 5)
            throw new($"Expected bytesWritten 5 but got {bytesWritten}");
        var expected = "Hello"u8;
        for (var i = 0; i < bytesWritten; i++)
        {
            if (bytes[i] != expected[i])
                throw new($"Mismatch at index {i}");
        }

        return Task.CompletedTask;
    }

    [Test]
    public Task TryFromBase64String_InvalidInput()
    {
        Span<byte> bytes = stackalloc byte[10];
        var result = Convert.TryFromBase64String("!!!", bytes, out var bytesWritten);
        if (result)
            throw new("Expected false");
        if (bytesWritten != 0)
            throw new($"Expected bytesWritten 0 but got {bytesWritten}");
        return Task.CompletedTask;
    }

    [Test]
    public Task TryToBase64Chars_ValidInput()
    {
        ReadOnlySpan<byte> bytes = "Hello"u8;
        Span<char> chars = stackalloc char[8];
        var result = Convert.TryToBase64Chars(bytes, chars, out var charsWritten);
        if (!result)
            throw new("Expected true");
        if (charsWritten != 8)
            throw new($"Expected charsWritten 8 but got {charsWritten}");
        if (chars.Slice(0, charsWritten).ToString() != "SGVsbG8=")
            throw new("Expected SGVsbG8=");
        return Task.CompletedTask;
    }

    [Test]
    public Task TryToBase64Chars_BufferTooSmall()
    {
        ReadOnlySpan<byte> bytes = "Hello"u8;
        Span<char> chars = stackalloc char[2];
        var result = Convert.TryToBase64Chars(bytes, chars, out var charsWritten);
        if (result)
            throw new("Expected false");
        if (charsWritten != 0)
            throw new($"Expected charsWritten 0 but got {charsWritten}");
        return Task.CompletedTask;
    }

    [Test]
    public Task FromHexString_OperationStatus_Chars_ValidInput()
    {
        ReadOnlySpan<char> source = "0FA35C".AsSpan();
        Span<byte> destination = stackalloc byte[3];
        var status = Convert.FromHexString(source, destination, out var charsConsumed, out var bytesWritten);
        if (status != System.Buffers.OperationStatus.Done)
            throw new($"Expected Done but got {status}");
        if (charsConsumed != 6)
            throw new($"Expected charsConsumed 6 but got {charsConsumed}");
        if (bytesWritten != 3)
            throw new($"Expected bytesWritten 3 but got {bytesWritten}");
        if (destination[0] != 0x0F || destination[1] != 0xA3 || destination[2] != 0x5C)
            throw new("Unexpected output bytes");
        return Task.CompletedTask;
    }

    [Test]
    public Task FromHexString_OperationStatus_Chars_DestinationTooSmall()
    {
        ReadOnlySpan<char> source = "0FA35C".AsSpan();
        Span<byte> destination = stackalloc byte[1];
        var status = Convert.FromHexString(source, destination, out var charsConsumed, out var bytesWritten);
        if (status != System.Buffers.OperationStatus.DestinationTooSmall)
            throw new($"Expected DestinationTooSmall but got {status}");
        if (bytesWritten != 1)
            throw new($"Expected bytesWritten 1 but got {bytesWritten}");
        if (charsConsumed != 2)
            throw new($"Expected charsConsumed 2 but got {charsConsumed}");
        return Task.CompletedTask;
    }

    [Test]
    public Task FromHexString_OperationStatus_Chars_InvalidData()
    {
        ReadOnlySpan<char> source = "ZZZZ".AsSpan();
        Span<byte> destination = stackalloc byte[2];
        var status = Convert.FromHexString(source, destination, out _, out _);
        if (status != System.Buffers.OperationStatus.InvalidData)
            throw new($"Expected InvalidData but got {status}");
        return Task.CompletedTask;
    }

    [Test]
    public Task FromHexString_OperationStatus_String_ValidInput()
    {
        Span<byte> destination = stackalloc byte[3];
        var status = Convert.FromHexString("0FA35C", destination, out var charsConsumed, out var bytesWritten);
        if (status != System.Buffers.OperationStatus.Done)
            throw new($"Expected Done but got {status}");
        if (charsConsumed != 6)
            throw new($"Expected charsConsumed 6 but got {charsConsumed}");
        if (bytesWritten != 3)
            throw new($"Expected bytesWritten 3 but got {bytesWritten}");
        return Task.CompletedTask;
    }

    [Test]
    public Task FromHexString_Utf8_ByteArray()
    {
        ReadOnlySpan<byte> utf8Source = "0FA35C"u8;
        var result = Convert.FromHexString(utf8Source);
        if (result.Length != 3)
            throw new($"Expected length 3 but got {result.Length}");
        if (result[0] != 0x0F || result[1] != 0xA3 || result[2] != 0x5C)
            throw new("Unexpected output bytes");
        return Task.CompletedTask;
    }

    [Test]
    public Task FromHexString_Utf8_OperationStatus_ValidInput()
    {
        ReadOnlySpan<byte> utf8Source = "0FA35C"u8;
        Span<byte> destination = stackalloc byte[3];
        var status = Convert.FromHexString(utf8Source, destination, out var bytesConsumed, out var bytesWritten);
        if (status != System.Buffers.OperationStatus.Done)
            throw new($"Expected Done but got {status}");
        if (bytesConsumed != 6)
            throw new($"Expected bytesConsumed 6 but got {bytesConsumed}");
        if (bytesWritten != 3)
            throw new($"Expected bytesWritten 3 but got {bytesWritten}");
        if (destination[0] != 0x0F || destination[1] != 0xA3 || destination[2] != 0x5C)
            throw new("Unexpected output bytes");
        return Task.CompletedTask;
    }

    [Test]
    public Task FromHexString_Utf8_OperationStatus_DestinationTooSmall()
    {
        ReadOnlySpan<byte> utf8Source = "0FA35C"u8;
        Span<byte> destination = stackalloc byte[1];
        var status = Convert.FromHexString(utf8Source, destination, out _, out var bytesWritten);
        if (status != System.Buffers.OperationStatus.DestinationTooSmall)
            throw new($"Expected DestinationTooSmall but got {status}");
        if (bytesWritten != 1)
            throw new($"Expected bytesWritten 1 but got {bytesWritten}");
        return Task.CompletedTask;
    }

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

    [Test]
    public Task TryToHexStringUtf8_ValidInput()
    {
        ReadOnlySpan<byte> source = [0x0F, 0xA3, 0x5C];
        Span<byte> destination = stackalloc byte[6];
        var result = Convert.TryToHexString(source, destination, out var bytesWritten);
        if (!result)
            throw new("Expected true");
        if (bytesWritten != 6)
            throw new($"Expected bytesWritten 6 but got {bytesWritten}");
        var expected = "0FA35C"u8;
        for (var i = 0; i < bytesWritten; i++)
        {
            if (destination[i] != expected[i])
                throw new($"Mismatch at index {i}");
        }

        return Task.CompletedTask;
    }

    [Test]
    public Task TryToHexStringUtf8_BufferTooSmall()
    {
        ReadOnlySpan<byte> source = [0x0F, 0xA3, 0x5C];
        Span<byte> destination = stackalloc byte[4];
        var result = Convert.TryToHexString(source, destination, out var bytesWritten);
        if (result)
            throw new("Expected false");
        if (bytesWritten != 0)
            throw new($"Expected bytesWritten 0 but got {bytesWritten}");
        return Task.CompletedTask;
    }

    [Test]
    public Task TryToHexStringLowerUtf8_ValidInput()
    {
        ReadOnlySpan<byte> source = [0x0F, 0xA3, 0x5C];
        Span<byte> destination = stackalloc byte[6];
        var result = Convert.TryToHexStringLower(source, destination, out var bytesWritten);
        if (!result)
            throw new("Expected true");
        if (bytesWritten != 6)
            throw new($"Expected bytesWritten 6 but got {bytesWritten}");
        var expected = "0fa35c"u8;
        for (var i = 0; i < bytesWritten; i++)
        {
            if (destination[i] != expected[i])
                throw new($"Mismatch at index {i}");
        }

        return Task.CompletedTask;
    }

    [Test]
    public Task TryToHexStringLowerUtf8_BufferTooSmall()
    {
        ReadOnlySpan<byte> source = [0x0F, 0xA3, 0x5C];
        Span<byte> destination = stackalloc byte[4];
        var result = Convert.TryToHexStringLower(source, destination, out var bytesWritten);
        if (result)
            throw new("Expected false");
        if (bytesWritten != 0)
            throw new($"Expected bytesWritten 0 but got {bytesWritten}");
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
