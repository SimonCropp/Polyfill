#if FeatureMemory
using System.IO;
using System.Text;

partial class PolyfillTests
{
    [Test]
    public async Task StringStream_ReadsExpectedUtf8Bytes()
    {
        var expected = Encoding.UTF8.GetBytes("Hello, World!");
        using var stream = new StringStream("Hello, World!", Encoding.UTF8);

        var actual = ReadToEnd(stream);

        await Assert.That(actual).IsEquivalentTo(expected);
    }

    [Test]
    public async Task StringStream_FromReadOnlyMemory_ReadsExpectedBytes()
    {
        var expected = Encoding.UTF8.GetBytes("chars");
        using var stream = new StringStream("chars".AsMemory(), Encoding.UTF8);

        var actual = ReadToEnd(stream);

        await Assert.That(actual).IsEquivalentTo(expected);
    }

    [Test]
    public async Task StringStream_Unicode_RoundTripsThroughEncoding()
    {
        const string input = "Unicode: 你好世界 🌍";
        var expected = Encoding.UTF8.GetBytes(input);
        using var stream = new StringStream(input, Encoding.UTF8);

        var actual = ReadToEnd(stream);

        await Assert.That(actual).IsEquivalentTo(expected);
    }

    [Test]
    public async Task StringStream_ReadInSmallChunks_ReconstructsOutput()
    {
        var expected = Encoding.UTF8.GetBytes("The quick brown fox");
        using var stream = new StringStream("The quick brown fox", Encoding.UTF8);

        using var accumulator = new MemoryStream();
        var buffer = new byte[3];
        int read;
        while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
        {
            accumulator.Write(buffer, 0, read);
        }

        await Assert.That(accumulator.ToArray()).IsEquivalentTo(expected);
    }

    [Test]
    public async Task StringStream_MultiByteReadByteByByte_MatchesGetBytes()
    {
        // Reading one byte at a time forces the spillover path: each multi-byte character
        // (and the surrogate pair) is encoded into the internal buffer and drained byte by byte.
        const string input = "Unicode: 你好世界 🌍 café";
        var expected = Encoding.UTF8.GetBytes(input);
        using var stream = new StringStream(input, Encoding.UTF8);

        using var accumulator = new MemoryStream();
        int value;
        while ((value = stream.ReadByte()) != -1)
        {
            accumulator.WriteByte((byte) value);
        }

        await Assert.That(accumulator.ToArray()).IsEquivalentTo(expected);
    }

    [Test]
    public async Task StringStream_MultiByteBoundarySplit_MatchesGetBytes()
    {
        // A 7-byte buffer is not a multiple of the 3-byte CJK width, so the encoder must carry
        // conversion state across reads without splitting or duplicating a character.
        const string input = "你好世界你好世界你好世界";
        var expected = Encoding.UTF8.GetBytes(input);
        using var stream = new StringStream(input, Encoding.UTF8);

        using var accumulator = new MemoryStream();
        var buffer = new byte[7];
        int read;
        while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
        {
            accumulator.Write(buffer, 0, read);
        }

        await Assert.That(accumulator.ToArray()).IsEquivalentTo(expected);
    }

    [Test]
    public async Task StringStream_ReadByte_ReturnsBytesThenMinusOne()
    {
        using var stream = new StringStream("AB", Encoding.ASCII);

        var first = stream.ReadByte();
        var second = stream.ReadByte();
        var third = stream.ReadByte();

        await Assert.That(first).IsEqualTo((int)'A');
        await Assert.That(second).IsEqualTo((int)'B');
        await Assert.That(third).IsEqualTo(-1);
    }

    [Test]
    public async Task StringStream_Empty_ReadsZero()
    {
        using var stream = new StringStream("", Encoding.UTF8);

        var buffer = new byte[8];
        var read = stream.Read(buffer, 0, buffer.Length);

        await Assert.That(read).IsEqualTo(0);
    }

    [Test]
    public async Task StringStream_NeverEmitsBom()
    {
        // UTF8 with BOM preamble: StringStream must not write the preamble.
        var encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: true);
        using var stream = new StringStream("x", encoding);

        var actual = ReadToEnd(stream);

        await Assert.That(actual).IsEquivalentTo(new byte[] { (byte)'x' });
    }

    [Test]
    public async Task StringStream_ReadAsync_ReadsExpectedBytes()
    {
        var expected = Encoding.UTF8.GetBytes("async");
        using var stream = new StringStream("async", Encoding.UTF8);

        var buffer = new byte[expected.Length];
        var total = 0;
        int read;
        while (total < buffer.Length &&
               (read = await stream.ReadAsync(buffer, total, buffer.Length - total)) > 0)
        {
            total += read;
        }

        await Assert.That(total).IsEqualTo(expected.Length);
        await Assert.That(buffer).IsEquivalentTo(expected);
    }

    [Test]
    public async Task StringStream_Capabilities()
    {
        using var stream = new StringStream("data", Encoding.UTF8);

        await Assert.That(stream.CanRead).IsTrue();
        await Assert.That(stream.CanSeek).IsFalse();
        await Assert.That(stream.CanWrite).IsFalse();
        await Assert.That(stream.Encoding).IsEqualTo((Encoding)Encoding.UTF8);
    }

    [Test]
    public async Task StringStream_SeekingMembersThrow()
    {
        using var stream = new StringStream("data", Encoding.UTF8);

        await Assert.That(() => stream.Length).Throws<NotSupportedException>();
        await Assert.That(() => stream.Position).Throws<NotSupportedException>();
        await Assert.That(() => stream.Seek(0, SeekOrigin.Begin)).Throws<NotSupportedException>();
    }

    [Test]
    public async Task StringStream_WritingMembersThrow()
    {
        using var stream = new StringStream("data", Encoding.UTF8);

        await Assert.That(() => stream.Write(new byte[1], 0, 1)).Throws<NotSupportedException>();
        await Assert.That(() => stream.SetLength(1)).Throws<NotSupportedException>();
    }

    [Test]
    public async Task StringStream_NullArgumentsThrow()
    {
        await Assert.That(() => new StringStream((string)null!, Encoding.UTF8)).Throws<ArgumentNullException>();
        await Assert.That(() => new StringStream("x", null!)).Throws<ArgumentNullException>();
    }

    [Test]
    public async Task StringStream_AfterDispose_CanReadIsFalse()
    {
        var stream = new StringStream("data", Encoding.UTF8);
        stream.Dispose();

        await Assert.That(stream.CanRead).IsFalse();
    }

    [Test]
    [Arguments("Hello, World! ")]
    [Arguments("Unicode: 你好世界 🌍")]
    [Arguments("Multi\nLine\r\nText")]
    public async Task StringStream_DifferentStrings_MatchGetBytes(string input)
    {
        var expected = Encoding.UTF8.GetBytes(input);
        using var stream = new StringStream(input, Encoding.UTF8);

        await Assert.That(ReadToEnd(stream)).IsEquivalentTo(expected);
    }

    [Test]
    [Arguments("ASCII text")]
    [Arguments("Ñoño español")]
    public async Task StringStream_DifferentEncodings_MatchGetBytes(string input)
    {
        foreach (var encoding in new[] { Encoding.UTF8, Encoding.Unicode, Encoding.UTF32 })
        {
            var expected = encoding.GetBytes(input);
            using var stream = new StringStream(input, encoding);

            await Assert.That(ReadToEnd(stream)).IsEquivalentTo(expected);
        }
    }

    [Test]
    public async Task StringStream_SurrogatePairs_MatchGetBytes()
    {
        const string input = "😀😁😂🤣😃😄";
        var expected = Encoding.UTF8.GetBytes(input);
        using var stream = new StringStream(input, Encoding.UTF8);

        await Assert.That(ReadToEnd(stream)).IsEquivalentTo(expected);
    }

    [Test]
    public async Task StringStream_MultiByteAcrossChunkBoundary_MatchGetBytes()
    {
        var input = new string('A', 1023) + "你";
        var expected = Encoding.UTF8.GetBytes(input);
        using var stream = new StringStream(input, Encoding.UTF8);

        await Assert.That(ReadToEnd(stream)).IsEquivalentTo(expected);
    }

    [Test]
    public async Task StringStream_ChunkedReading_LargeInput()
    {
        var input = new string('A', 10000);
        var expected = Encoding.UTF8.GetBytes(input);
        using var stream = new StringStream(input, Encoding.UTF8);

        var actual = new byte[expected.Length];
        var total = 0;
        const int chunkSize = 512;
        int read;
        while ((read = stream.Read(actual, total, Math.Min(chunkSize, expected.Length - total))) > 0)
        {
            total += read;
        }

        await Assert.That(total).IsEqualTo(expected.Length);
        await Assert.That(actual).IsEquivalentTo(expected);
    }

    [Test]
    public async Task StringStream_FastPathGuard_DoesNotOverflow()
    {
        // GetMaxByteCount(text.Length) would overflow int for this encoding; the fast-path guard
        // must short-circuit before calling it rather than throwing OverflowException.
        using var stream = new StringStream("hello", new OverflowingEncoding());

        var threw = false;
        try
        {
            stream.Read(new byte[16], 0, 16);
        }
        catch (OverflowException)
        {
            threw = true;
        }

        await Assert.That(threw).IsFalse();
    }

    [Test]
    public async Task StringStream_MemorySlice_MatchGetBytes()
    {
        const string source = "0123456789ABCDEFGHIJ";
        var slice = source.AsMemory(5, 10);
        var expected = Encoding.UTF8.GetBytes("56789ABCDE");
        using var stream = new StringStream(slice, Encoding.UTF8);

        await Assert.That(ReadToEnd(stream)).IsEquivalentTo(expected);
    }

    [Test]
    public async Task StringStream_CharArrayMemory_MatchGetBytes()
    {
        var chars = new[] { 'H', 'e', 'l', 'l', 'o' };
        var expected = Encoding.UTF8.GetBytes("Hello");
        using var stream = new StringStream(new ReadOnlyMemory<char>(chars), Encoding.UTF8);

        await Assert.That(ReadToEnd(stream)).IsEquivalentTo(expected);
    }

    [Test]
    [Arguments("Hello")]
    [Arguments("Unicode: 你好")]
    [Arguments("Emoji: 😀")]
    public async Task StringStream_MemoryOverloadMatchesStringOverload(string input)
    {
        using var fromMemory = new StringStream(input.AsMemory(), Encoding.UTF8);
        using var fromString = new StringStream(input, Encoding.UTF8);

        var memoryBytes = ReadToEnd(fromMemory);
        var stringBytes = ReadToEnd(fromString);

        await Assert.That(memoryBytes).IsEquivalentTo(stringBytes);
    }

    [Test]
    public async Task StringStream_TruncatedSurrogate_ProducesReplacementChar()
    {
        // "A" plus the high surrogate of U+1F30D with no trailing low surrogate.
        const string emoji = "A\U0001F30D";
        var truncated = emoji.AsMemory(0, 2);
        var expected = Encoding.UTF8.GetBytes("A�");
        using var stream = new StringStream(truncated, Encoding.UTF8);

        await Assert.That(ReadToEnd(stream)).IsEquivalentTo(expected);
    }

    [Test]
    public async Task StringStream_CopyToAsync_HonorsCancellation()
    {
        using var stream = new StringStream("hello", Encoding.UTF8);
        using var destination = new MemoryStream();
        using var cancelSource = new CancelSource();
        cancelSource.Cancel();

        await Assert.That(async () => await stream.CopyToAsync(destination, 81920, cancelSource.Token))
            .Throws<OperationCanceledException>();
    }

    static byte[] ReadToEnd(Stream stream)
    {
        using var memory = new MemoryStream();
        var buffer = new byte[16];
        int read;
        while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
        {
            memory.Write(buffer, 0, read);
        }

        return memory.ToArray();
    }

    // An encoding whose GetMaxByteCount overflows int for larger char counts, used to verify the
    // StringStream fast-path guard skips GetMaxByteCount rather than throwing OverflowException.
    sealed class OverflowingEncoding :
        Encoding
    {
        public override int GetByteCount(char[] chars, int index, int count) =>
            UTF8.GetByteCount(chars, index, count);

        public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex) =>
            UTF8.GetBytes(chars, charIndex, charCount, bytes, byteIndex);

        public override int GetMaxByteCount(int charCount) =>
            charCount switch
            {
                1 => int.MaxValue,
                2 => 8,
                _ => checked((charCount + 1) * int.MaxValue)
            };

        public override int GetCharCount(byte[] bytes, int index, int count) =>
            throw new NotImplementedException();

        public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex) =>
            throw new NotImplementedException();

        public override int GetMaxCharCount(int byteCount) =>
            throw new NotImplementedException();
    }
}
#endif
