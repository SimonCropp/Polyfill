// ReSharper disable PartialTypeWithSinglePart

using System.Globalization;

[TestFixture]
partial class SBytePolyfillTest
{
    [Test]
    public void TryParse()
    {
        Assert.True(SBytePolyfill.TryParse("1"u8, null, out var value));
        Assert.AreEqual(1, value);

        Assert.True(SBytePolyfill.TryParse(['1'], out value));
        Assert.AreEqual(1, value);

        Assert.True(SBytePolyfill.TryParse(['1'], null, out value));
        Assert.AreEqual(1, value);

        Assert.True(SBytePolyfill.TryParse("1", null, out value));
        Assert.AreEqual(1, value);

        Assert.True(SBytePolyfill.TryParse("1"u8, NumberStyles.Integer, null, out value));
        Assert.AreEqual(1, value);

        Assert.True(SBytePolyfill.TryParse("1"u8, out value));
        Assert.AreEqual(1, value);

        Assert.True(SBytePolyfill.TryParse(['1'], NumberStyles.Integer, null, out value));
        Assert.AreEqual(1, value);
    }
}