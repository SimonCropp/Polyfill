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
}
#endif
