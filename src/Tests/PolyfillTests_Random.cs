partial class PolyfillTests
{
#if FeatureMemory
    [Test]
    public void RandomNextBytesSpan()
    {
        var random = new Random();
        Span<byte> buffer = new byte[10];
        random.NextBytes(buffer);
        Assert.IsTrue(buffer.ToArray().Any(b => b != 0));
    }

    [Test]
    public void RandomShuffleSpan()
    {
        var random = new Random();
        Span<byte> buffer = new byte[10];
        random.NextBytes(buffer);
        random.Shuffle(buffer);
        Assert.IsTrue(buffer.ToArray().Any(b => b != 0));
    }
#endif
    [Test]
    public void RandomShuffleArray()
    {
        var random = new Random();
        var buffer = new byte[10];
        random.NextBytes(buffer);
        random.Shuffle(buffer);
        Assert.IsTrue(buffer.ToArray().Any(b => b != 0));
    }
}