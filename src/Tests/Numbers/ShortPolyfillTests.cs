[TestFixture]
public class ShortPolyfillTest
{
    [Test]
    public void TryParse()
    {
        Assert.True(ShortPolyfill.TryParse("1"u8, null, out var value));
        Assert.AreEqual(1, value);

        Assert.True(ShortPolyfill.TryParse(['1'], out value));
        Assert.AreEqual(1, value);

        Assert.True(ShortPolyfill.TryParse(['1'], null, out value));
        Assert.AreEqual(1, value);

        Assert.True(ShortPolyfill.TryParse("1", null, out value));
        Assert.AreEqual(1, value);

        Assert.True(ShortPolyfill.TryParse("1"u8, NumberStyles.Integer, null, out value));
        Assert.AreEqual(1, value);

        Assert.True(ShortPolyfill.TryParse("1"u8, out value));
        Assert.AreEqual(1, value);

        Assert.True(ShortPolyfill.TryParse(['1'], NumberStyles.Integer, null, out value));
        Assert.AreEqual(1, value);
    }
}