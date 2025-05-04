[TestFixture]
public class ConvertPolyfillTests
{
    [Test]
    public void ToHexString_ValidInput()
    {
        byte[] input = [0x0F, 0xA3, 0x5C];
        var result = ConvertPolyfill.ToHexString(input, 0, input.Length);
        Assert.AreEqual("0FA35C", result);
        result = ConvertPolyfill.ToHexString(input);
        Assert.AreEqual("0FA35C", result);
#if  FeatureMemory
        result = ConvertPolyfill.ToHexString(input.AsSpan());
        Assert.AreEqual("0FA35C", result);
#endif
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

    [Test]
    public void ToHexStringLower_ValidInput()
    {
        byte[] input = [0x0F, 0xA3, 0x5C];
        var result = ConvertPolyfill.ToHexString(input, 0, input.Length);
        Assert.AreEqual("0FA35C", result);
        result = ConvertPolyfill.ToHexStringLower(input);
        Assert.AreEqual("0fa35c", result);
#if  FeatureMemory
        result = ConvertPolyfill.ToHexStringLower(input.AsSpan());
        Assert.AreEqual("0fa35c", result);
#endif
    }

    [Test]
    public void ToHexStringLower_OffsetAndLength()
    {
        byte[] input = [0x0F, 0xA3, 0x5C, 0x7E];
        var result = ConvertPolyfill.ToHexStringLower(input, 1, 2);
        Assert.AreEqual("a35c", result);
    }

    [Test]
    public void ToHexStringLower_NegativeLength_ThrowsException()
    {
        byte[] input = [0x0F, 0xA3, 0x5C];
        Assert.Throws<ArgumentOutOfRangeException>(() => ConvertPolyfill.ToHexStringLower(input, 0, -1));
    }

    [Test]
    public void ToHexStringLower_NegativeOffset_ThrowsException()
    {
        byte[] input = [0x0F, 0xA3, 0x5C];
        Assert.Throws<ArgumentOutOfRangeException>(() => ConvertPolyfill.ToHexStringLower(input, -1, 2));
    }

    [Test]
    public void ToHexStringLower_OffsetPlusLengthExceedsArray_ThrowsException()
    {
        byte[] input = [0x0F, 0xA3, 0x5C];
        Assert.Throws<ArgumentOutOfRangeException>(() => ConvertPolyfill.ToHexStringLower(input, 2, 2));
    }

#if FeatureMemory

    [Test]
    public void TryToHexString_ValidInput()
    {
        ReadOnlySpan<byte> source = [0x0F, 0xA3, 0x5C];
        Span<char> destination = stackalloc char[6];
        var result = ConvertPolyfill.TryToHexString(source, destination, out var charsWritten);
        Assert.IsTrue(result);
        Assert.AreEqual(6, charsWritten);
        Assert.AreEqual("0FA35C", destination.Slice(0, charsWritten).ToString());
    }

    [Test]
    public void TryToHexString_BufferTooSmall()
    {
        ReadOnlySpan<byte> source = [0x0F, 0xA3, 0x5C];
        Span<char> destination = stackalloc char[4];
        var result = ConvertPolyfill.TryToHexString(source, destination, out var charsWritten);
        Assert.IsFalse(result);
        Assert.AreEqual(0, charsWritten);
    }

    [Test]
    public void TryToHexString_EmptySource()
    {
        var source = ReadOnlySpan<byte>.Empty;
        Span<char> destination = [];
        var result = ConvertPolyfill.TryToHexString(source, destination, out var charsWritten);
        Assert.IsTrue(result);
        Assert.AreEqual(0, charsWritten);
    }

    [Test]
    public void TryToHexStringLower_ValidInput()
    {
        ReadOnlySpan<byte> source = [0x0F, 0xA3, 0x5C];
        Span<char> destination = stackalloc char[6];
        var result = ConvertPolyfill.TryToHexStringLower(source, destination, out var charsWritten);
        Assert.IsTrue(result);
        Assert.AreEqual(6, charsWritten);
        Assert.AreEqual("0fa35c", destination.Slice(0, charsWritten).ToString());
    }

    [Test]
    public void TryToHexStringLower_BufferTooSmall()
    {
        ReadOnlySpan<byte> source = [0x0F, 0xA3, 0x5C];
        Span<char> destination = stackalloc char[4];
        var result = ConvertPolyfill.TryToHexStringLower(source, destination, out var charsWritten);
        Assert.IsFalse(result);
        Assert.AreEqual(0, charsWritten);
    }

    [Test]
    public void TryToHexStringLower_EmptySource()
    {
        var source = ReadOnlySpan<byte>.Empty;
        Span<char> destination = [];
        var result = ConvertPolyfill.TryToHexStringLower(source, destination, out var charsWritten);
        Assert.IsTrue(result);
        Assert.AreEqual(0, charsWritten);
    }

#endif

    [Test]
    public void FromHexString_ValidInput()
    {
        var hexString = "0FA35C";
        var result = ConvertPolyfill.FromHexString(hexString);
        CollectionAssert.AreEqual(new byte[] { 0x0F, 0xA3, 0x5C }, result);
    }

    [Test]
    public void FromHexString_EmptyString()
    {
        var hexString = string.Empty;
        var result = ConvertPolyfill.FromHexString(hexString);
        CollectionAssert.AreEqual(Array.Empty<byte>(), result);
    }

    [Test]
    public void FromHexString_InvalidCharacter_ThrowsFormatException()
    {
        var hexString = "0FA3ZC";
        Assert.Throws<FormatException>(() => ConvertPolyfill.FromHexString(hexString));
    }

    [Test]
    public void FromHexString_OddLength_ThrowsFormatException()
    {
        var hexString = "0FA3C";
        Assert.Throws<FormatException>(() => ConvertPolyfill.FromHexString(hexString));
    }

    [Test]
    public void FromHexString_LowercaseInput()
    {
        var hexString = "0fa35c";
        var result = ConvertPolyfill.FromHexString(hexString);
        CollectionAssert.AreEqual(new byte[] { 0x0F, 0xA3, 0x5C }, result);
    }
}