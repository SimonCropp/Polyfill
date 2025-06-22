// ReSharper disable PartialTypeWithSinglePart

using System.Globalization;

[TestFixture]
public class Int16PolyfillTest
{
    [Test]
    public void TryParse()
    {
        Assert.True(short.TryParse("1"u8, null, out var value));
        Assert.AreEqual(1, value);

        Assert.True(short.TryParse(['1'], out value));
        Assert.AreEqual(1, value);

        Assert.True(short.TryParse(['1'], null, out value));
        Assert.AreEqual(1, value);

        Assert.True(short.TryParse("1", null, out value));
        Assert.AreEqual(1, value);

        Assert.True(short.TryParse("1"u8, NumberStyles.Integer, null, out value));
        Assert.AreEqual(1, value);

        Assert.True(short.TryParse("1"u8, out value));
        Assert.AreEqual(1, value);

        Assert.True(short.TryParse(['1'], NumberStyles.Integer, null, out value));
        Assert.AreEqual(1, value);
    }
}