using System.Buffers;

public class ConvertPolyfillTests
{
    [Test]
    public async Task FromHexString_ConsumedOnInvalidData()
    {
        var destination = new byte[8];

        // A valid leading nibble is counted as consumed; consumed stops at i unless the low nibble is valid.
        await AssertConsumed("4G", 1); // hi valid, lo invalid
        await AssertConsumed("G4", 0); // hi invalid, lo valid
        await AssertConsumed("GG", 1); // both invalid
        await AssertConsumed("444G", 3); // failure mid-span

        // The UTF-8 byte overload shares the same consumed semantics.
        var bytesStatus = Convert.FromHexString("4G"u8, destination, out var bytesConsumed, out _);
        await Assert.That(bytesStatus).IsEqualTo(OperationStatus.InvalidData);
        await Assert.That(bytesConsumed).IsEqualTo(1);

        // DestinationTooSmall takes priority over InvalidData when the buffer is already full.
        var smallDestination = new byte[2];
        var fullStatus = Convert.FromHexString("a1F6CG".AsSpan(), smallDestination, out var fullConsumed, out var fullWritten);
        await Assert.That(fullStatus).IsEqualTo(OperationStatus.DestinationTooSmall);
        await Assert.That(fullConsumed).IsEqualTo(4);
        await Assert.That(fullWritten).IsEqualTo(2);

        async Task AssertConsumed(string source, int expectedConsumed)
        {
            var status = Convert.FromHexString(source.AsSpan(), destination, out var consumed, out _);
            await Assert.That(status).IsEqualTo(OperationStatus.InvalidData);
            await Assert.That(consumed).IsEqualTo(expectedConsumed);
        }
    }

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

    static byte[] HexPatternBytes(int length)
    {
        var bytes = new byte[length];

        for (var i = 0; i < length; i++)
        {
            bytes[i] = unchecked((byte) (i * 37 + 11));
        }

        return bytes;
    }

    [Test]
    public async Task Hex_Arrays_AllLengths_MatchBitConverter()
    {
        var failures = new List<string>();

        // 0 to 300 covers both sides of the 128 byte stack buffer
        for (var length = 0; length <= 300; length++)
        {
            // a margin either side of the input
            var buffer = HexPatternBytes(length + 7);
            var input = new byte[length];
            Array.Copy(buffer, 3, input, 0, length);
            var upper = BitConverter.ToString(input).Replace("-", "");
            var lower = upper.ToLowerInvariant();

            void Check(string name, bool condition)
            {
                if (!condition)
                {
                    failures.Add($"{name} length {length}");
                }
            }

            Check("ToHexString array", Convert.ToHexString(input) == upper);
            Check("ToHexString offset", Convert.ToHexString(buffer, 3, length) == upper);
            Check("ToHexStringLower array", Convert.ToHexStringLower(input) == lower);
            Check("ToHexStringLower offset", Convert.ToHexStringLower(buffer, 3, length) == lower);
            Check("FromHexString upper", Convert.FromHexString(upper).SequenceEqual(input));
            Check("FromHexString lower", Convert.FromHexString(lower).SequenceEqual(input));
        }

        await Assert.That(failures).IsEmpty();
    }

#if FeatureMemory
    [Test]
    public async Task Hex_Spans_AllLengths_MatchBitConverter()
    {
        var failures = new List<string>();

        for (var length = 0; length <= 300; length++)
        {
            // a margin either side of the input, so that the span is not the whole of an array
            var buffer = HexPatternBytes(length + 7);
            var input = buffer.AsSpan(3, length).ToArray();
            var upper = BitConverter.ToString(input).Replace("-", "");
            var lower = upper.ToLowerInvariant();

            // upper and lower case digits alternating
            var mixedChars = upper.ToCharArray();
            for (var i = 0; i < mixedChars.Length; i += 2)
            {
                mixedChars[i] = char.ToLowerInvariant(mixedChars[i]);
            }

            var mixed = new string(mixedChars);

            void Check(string name, bool condition)
            {
                if (!condition)
                {
                    failures.Add($"{name} length {length}");
                }
            }

            Check("ToHexString span", Convert.ToHexString(new ReadOnlySpan<byte>(buffer, 3, length)) == upper);
            Check("ToHexStringLower span", Convert.ToHexStringLower(new ReadOnlySpan<byte>(buffer, 3, length)) == lower);

            // the chars are a slice of a longer string of valid hex digits
            Check("FromHexString span upper", Convert.FromHexString(("AB" + upper + "CD").AsSpan(2, upper.Length)).SequenceEqual(input));
            Check("FromHexString span lower", Convert.FromHexString(("ab" + lower + "cd").AsSpan(2, lower.Length)).SequenceEqual(input));
            Check("FromHexString span mixed", Convert.FromHexString(("ab" + mixed + "cd").AsSpan(2, mixed.Length)).SequenceEqual(input));
        }

        await Assert.That(failures).IsEmpty();
    }

    [Test]
    public async Task ToHexString_Span_Large()
    {
        var input = HexPatternBytes(100_000);
        var upper = BitConverter.ToString(input).Replace("-", "");

        await Assert.That(Convert.ToHexString(new ReadOnlySpan<byte>(input))).IsEqualTo(upper);
        await Assert.That(Convert.ToHexStringLower(new ReadOnlySpan<byte>(input))).IsEqualTo(upper.ToLowerInvariant());
        await Assert.That(Convert.FromHexString(upper.AsSpan()).SequenceEqual(input)).IsTrue();
    }

    [Test]
    public async Task Hex_Span_AllByteValues()
    {
        var input = new byte[256];
        for (var i = 0; i < input.Length; i++)
        {
            input[i] = (byte) i;
        }

        var upper = string.Concat(input.Select(_ => _.ToString("X2")));
        var lower = string.Concat(input.Select(_ => _.ToString("x2")));

        await Assert.That(Convert.ToHexString(new ReadOnlySpan<byte>(input))).IsEqualTo(upper);
        await Assert.That(Convert.ToHexStringLower(new ReadOnlySpan<byte>(input))).IsEqualTo(lower);
        await Assert.That(Convert.FromHexString(upper.AsSpan()).SequenceEqual(input)).IsTrue();
        await Assert.That(Convert.FromHexString(lower.AsSpan()).SequenceEqual(input)).IsTrue();
    }

    [Test]
    [Arguments("0")]
    [Arguments("0FA")]
    [Arguments("0FA3C")]
    public async Task FromHexString_Span_OddLength_ThrowsFormatException(string text) =>
        await Assert.That(() => Convert.FromHexString(text.AsSpan())).Throws<FormatException>();

    [Test]
    [Arguments("G0")]
    [Arguments("0G")]
    [Arguments("0FZ3")]
    [Arguments("0F3Z")]
    [Arguments("0F 3")]
    [Arguments("0x0F")]
    [Arguments("-1")]
    [Arguments("0F١١")]
    [Arguments("０１")]
    [Arguments("0F:A")]
    [Arguments("0F@A")]
    [Arguments("0F`A")]
    [Arguments("0FgA")]
    [Arguments("0F/A")]
    public async Task FromHexString_Span_InvalidCharacter_ThrowsFormatException(string text) =>
        await Assert.That(() => Convert.FromHexString(text.AsSpan())).Throws<FormatException>();

    [Test]
    public async Task FromHexString_Span_Empty()
    {
        var result = Convert.FromHexString(ReadOnlySpan<char>.Empty);
        await Assert.That(result).IsEmpty();
        await Assert.That(Convert.ToHexString(ReadOnlySpan<byte>.Empty)).IsEqualTo(string.Empty);
        await Assert.That(Convert.ToHexStringLower(ReadOnlySpan<byte>.Empty)).IsEqualTo(string.Empty);
    }
#endif
}
