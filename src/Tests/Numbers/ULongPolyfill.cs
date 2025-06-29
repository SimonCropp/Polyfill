[TestFixture]
public class UInt64PolyfillTest
{
    [Test]
    public void TryParse()
    {
        Assert.True(ulong.TryParse("1"u8, null, out var value));
        Assert.AreEqual(1, value);

        Assert.True(ulong.TryParse(['1'], out value));
        Assert.AreEqual(1, value);

        Assert.True(ulong.TryParse(['1'], null, out value));
        Assert.AreEqual(1, value);

        Assert.True(ulong.TryParse("1", null, out value));
        Assert.AreEqual(1, value);

        Assert.True(ulong.TryParse("1"u8, NumberStyles.Integer, null, out value));
        Assert.AreEqual(1, value);

        Assert.True(ulong.TryParse("1"u8, out value));
        Assert.AreEqual(1, value);

        Assert.True(ulong.TryParse(['1'], NumberStyles.Integer, null, out value));
        Assert.AreEqual(1, value);
    }
}