#if FeatureMemory
partial class PolyfillTests
{
    [Test]
    public async Task Encoder_Convert()
    {
        var encoder = Encoding.UTF8.GetEncoder();
        var chars = "Hello, World!".AsSpan();
        var bytes = new byte[Encoding.UTF8.GetByteCount("Hello, World!")];

        encoder.Convert(chars, bytes.AsSpan(), true, out var charsUsed, out var bytesUsed, out var completed);

        await Assert.That(charsUsed).IsEqualTo(13);
        await Assert.That(bytesUsed).IsEqualTo(13);
        await Assert.That(completed).IsTrue();
        await Assert.That(bytes).IsEquivalentTo(Encoding.UTF8.GetBytes("Hello, World!"));
    }

    [Test]
    public async Task Encoder_Convert_PartialOutput()
    {
        var encoder = Encoding.UTF8.GetEncoder();
        var chars = "Hello, World!".AsSpan();
        // Output buffer smaller than the encoded length: only a prefix should be converted.
        var bytes = new byte[5];

        encoder.Convert(chars, bytes.AsSpan(), false, out var charsUsed, out var bytesUsed, out var completed);

        await Assert.That(charsUsed).IsEqualTo(5);
        await Assert.That(bytesUsed).IsEqualTo(5);
        await Assert.That(completed).IsFalse();
        await Assert.That(bytes).IsEquivalentTo(Encoding.UTF8.GetBytes("Hello"));
    }

    [Test]
    public async Task Encoder_Convert_SurrogatePair()
    {
        var encoder = Encoding.UTF8.GetEncoder();
        // U+1F600 GRINNING FACE, a surrogate pair that encodes to 4 UTF-8 bytes.
        var text = "\U0001F600";
        var chars = text.AsSpan();
        var bytes = new byte[Encoding.UTF8.GetByteCount(text)];

        encoder.Convert(chars, bytes.AsSpan(), true, out var charsUsed, out var bytesUsed, out var completed);

        await Assert.That(charsUsed).IsEqualTo(2);
        await Assert.That(bytesUsed).IsEqualTo(4);
        await Assert.That(completed).IsTrue();
        await Assert.That(bytes).IsEquivalentTo(Encoding.UTF8.GetBytes(text));
    }

    [Test]
    public async Task Encoder_Convert_EmptySource()
    {
        var encoder = Encoding.UTF8.GetEncoder();
        ReadOnlySpan<char> chars = default;
        var bytes = new byte[1];

        encoder.Convert(chars, bytes, true, out var charsUsed, out var bytesUsed, out var completed);

        await Assert.That(charsUsed).IsEqualTo(0);
        await Assert.That(bytesUsed).IsEqualTo(0);
        await Assert.That(completed).IsTrue();
    }

    [Test]
    public async Task Encoder_Convert_EmptyDestination() =>
        await Assert.That(() =>
        {
            var encoder = Encoding.UTF8.GetEncoder();
            ReadOnlySpan<char> chars = "value";
            Span<byte> bytes = default;
            encoder.Convert(chars, bytes, false, out _, out _, out _);
        }).Throws<ArgumentException>();

    [Test]
    public async Task Encoder_Convert_EmptySourceAndDestination()
    {
        var encoder = Encoding.UTF8.GetEncoder();
        ReadOnlySpan<char> chars = default;
        Span<byte> bytes = default;

        encoder.Convert(chars, bytes, true, out var charsUsed, out var bytesUsed, out var completed);

        await Assert.That(charsUsed).IsEqualTo(0);
        await Assert.That(bytesUsed).IsEqualTo(0);
        await Assert.That(completed).IsTrue();
    }

    [Test]
    public async Task Encoder_Convert_FlushPendingWithEmptySource()
    {
        var encoder = Encoding.UTF8.GetEncoder();
        var buffer = new byte[16];

        // Feed a lone high surrogate without flushing; a stateful encoder buffers it and emits nothing yet.
        ReadOnlySpan<char> highSurrogate = "\ud83d";
        encoder.Convert(highSurrogate, buffer, false, out _, out var pendingBytes, out _);

        // Flushing with an empty source must still emit the buffered surrogate (via the replacement fallback),
        // proving the empty-source path cannot be short-circuited for the stateful Encoder.
        ReadOnlySpan<char> empty = default;
        encoder.Convert(empty, buffer, true, out var charsUsed, out var bytesUsed, out _);

        // `completed` is intentionally not asserted: .NET Core 3.1's UTF8 encoder returns false here,
        // while .NET Framework (polyfill path) and .NET 5+ return true. The point of this test is that
        // flushing with an empty source still emits the buffered surrogate (bytesUsed == 3), which holds
        // on every runtime.
        await Assert.That(pendingBytes).IsEqualTo(0);
        await Assert.That(charsUsed).IsEqualTo(0);
        await Assert.That(bytesUsed).IsEqualTo(3);
    }
}
#endif
