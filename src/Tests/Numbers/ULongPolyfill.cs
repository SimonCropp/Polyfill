// ReSharper disable PartialTypeWithSinglePart

using System.Globalization;

[TestFixture]
partial class ULongPolyfillTest
{
    [Test]
    public void TryParse()
    {
        Assert.True(ULongPolyfill.TryParse("1"u8, null, out var value));
        Assert.AreEqual(1, value);

        Assert.True(ULongPolyfill.TryParse(['1'], out value));
        Assert.AreEqual(1, value);

        Assert.True(ULongPolyfill.TryParse(['1'], null, out value));
        Assert.AreEqual(1, value);

        Assert.True(ULongPolyfill.TryParse("1", null, out value));
        Assert.AreEqual(1, value);

        Assert.True(ULongPolyfill.TryParse("1"u8, NumberStyles.Integer, null, out value));
        Assert.AreEqual(1, value);

        Assert.True(ULongPolyfill.TryParse("1"u8, out value));
        Assert.AreEqual(1, value);

        Assert.True(ULongPolyfill.TryParse(['1'], NumberStyles.Integer, null, out value));
        Assert.AreEqual(1, value);
    }
}