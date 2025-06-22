[TestFixture]
public class DoublePolyfillTest
{
    [Test]
    public void TryParse()
    {
        Assert.True(double.TryParse("1"u8, null, out var value));
        Assert.AreEqual(1, value);

        Assert.True(double.TryParse(['1'], out value));
        Assert.AreEqual(1, value);

        Assert.True(double.TryParse(['1'], null, out value));
        Assert.AreEqual(1, value);

        Assert.True(double.TryParse("1", null, out value));
        Assert.AreEqual(1, value);

        Assert.True(double.TryParse("1"u8, NumberStyles.Integer, null, out value));
        Assert.AreEqual(1, value);

        Assert.True(double.TryParse("1"u8, out value));
        Assert.AreEqual(1, value);

        Assert.True(double.TryParse(['1'], NumberStyles.Integer, null, out value));
        Assert.AreEqual(1, value);
    }
}