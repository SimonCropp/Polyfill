[TestFixture]
public class ConvertPolyfillTests
{
    [Test]
    public void ToHexString_ValidInput()
    {
        byte[] input = [0x0F, 0xA3, 0x5C];
        var result = ConvertPolyfill.ToHexString(input, 0, input.Length);
        Assert.AreEqual("0FA35C", result);
    }

    [Test]
    public void ToHexString_OffsetAndLength()
    {
        byte[] input = [0x0F, 0xA3, 0x5C, 0x7E];
        var result = ConvertPolyfill.ToHexString(input, 1, 2);
        Assert.AreEqual("A35C", result);
    }

    [Test]
    public void ToHexString_NegativeLength_ThrowsException()
    {
        byte[] input = [0x0F, 0xA3, 0x5C];
        Assert.Throws<ArgumentOutOfRangeException>(() => ConvertPolyfill.ToHexString(input, 0, -1));
    }

    [Test]
    public void ToHexString_NegativeOffset_ThrowsException()
    {
        byte[] input = [0x0F, 0xA3, 0x5C];
        Assert.Throws<ArgumentOutOfRangeException>(() => ConvertPolyfill.ToHexString(input, -1, 2));
    }

    [Test]
    public void ToHexString_OffsetPlusLengthExceedsArray_ThrowsException()
    {
        byte[] input = [0x0F, 0xA3, 0x5C];
        Assert.Throws<ArgumentOutOfRangeException>(() => ConvertPolyfill.ToHexString(input, 2, 2));
    }
}