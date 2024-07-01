partial class PolyfillTests
{
#if FeatureMemory
    [Test]
    public void RandomNextBytesSpan()
    {
        // Create a new instance of Random
        var random = new Random();

        // Create a span of bytes
        Span<byte> buffer = new byte[10];

        // Fill the span with random bytes
        random.NextBytes(buffer);

        // Assert that the span is filled with random bytes
        Assert.IsTrue(buffer.ToArray().Any(b => b != 0));
    }
#endif
}