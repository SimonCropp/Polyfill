#if FeatureMemory

public class BitConverterPolyfillTests
{
    [Test]
    public async Task Int32BitsToSingle_Zero()
    {
        var result = BitConverter.Int32BitsToSingle(0);
        await Assert.That(result).IsEqualTo(0.0f);
    }

    [Test]
    public async Task Int32BitsToSingle_One()
    {
        // IEEE 754 representation of 1.0f is 0x3F800000
        var result = BitConverter.Int32BitsToSingle(0x3F800000);
        await Assert.That(result).IsEqualTo(1.0f);
    }

    [Test]
    public async Task Int32BitsToSingle_NegativeOne()
    {
        // IEEE 754 representation of -1.0f is 0xBF800000
        var result = BitConverter.Int32BitsToSingle(unchecked((int)0xBF800000));
        await Assert.That(result).IsEqualTo(-1.0f);
    }

    [Test]
    public async Task SingleToInt32Bits_Zero()
    {
        var result = BitConverter.SingleToInt32Bits(0.0f);
        await Assert.That(result).IsEqualTo(0);
    }

    [Test]
    public async Task SingleToInt32Bits_One()
    {
        var result = BitConverter.SingleToInt32Bits(1.0f);
        await Assert.That(result).IsEqualTo(0x3F800000);
    }

    [Test]
    public async Task SingleToInt32Bits_NegativeOne()
    {
        var result = BitConverter.SingleToInt32Bits(-1.0f);
        await Assert.That(result).IsEqualTo(unchecked((int)0xBF800000));
    }

    [Test]
    public async Task Int32BitsToSingle_RoundTrip()
    {
        var original = 12345;
        var asFloat = BitConverter.Int32BitsToSingle(original);
        var backToInt = BitConverter.SingleToInt32Bits(asFloat);
        await Assert.That(backToInt).IsEqualTo(original);
    }

    [Test]
    public async Task SingleToInt32Bits_RoundTrip()
    {
        var original = 3.14159f;
        var asInt = BitConverter.SingleToInt32Bits(original);
        var backToFloat = BitConverter.Int32BitsToSingle(asInt);
        await Assert.That(backToFloat).IsEqualTo(original);
    }

    [Test]
    public async Task UInt32BitsToSingle_Zero()
    {
        var result = BitConverter.UInt32BitsToSingle(0u);
        await Assert.That(result).IsEqualTo(0.0f);
    }

    [Test]
    public async Task UInt32BitsToSingle_One()
    {
        // IEEE 754 representation of 1.0f is 0x3F800000
        var result = BitConverter.UInt32BitsToSingle(0x3F800000u);
        await Assert.That(result).IsEqualTo(1.0f);
    }

    [Test]
    public async Task SingleToUInt32Bits_Zero()
    {
        var result = BitConverter.SingleToUInt32Bits(0.0f);
        await Assert.That(result).IsEqualTo(0u);
    }

    [Test]
    public async Task SingleToUInt32Bits_One()
    {
        var result = BitConverter.SingleToUInt32Bits(1.0f);
        await Assert.That(result).IsEqualTo(0x3F800000u);
    }

    [Test]
    public async Task UInt32BitsToSingle_RoundTrip()
    {
        var original = 0xDEADBEEFu;
        var asFloat = BitConverter.UInt32BitsToSingle(original);
        var backToUInt = BitConverter.SingleToUInt32Bits(asFloat);
        await Assert.That(backToUInt).IsEqualTo(original);
    }

    [Test]
    public async Task UInt64BitsToDouble_Zero()
    {
        var result = BitConverter.UInt64BitsToDouble(0ul);
        await Assert.That(result).IsEqualTo(0.0);
    }

    [Test]
    public async Task UInt64BitsToDouble_One()
    {
        // IEEE 754 representation of 1.0 is 0x3FF0000000000000
        var result = BitConverter.UInt64BitsToDouble(0x3FF0000000000000ul);
        await Assert.That(result).IsEqualTo(1.0);
    }

    [Test]
    public async Task DoubleToUInt64Bits_Zero()
    {
        var result = BitConverter.DoubleToUInt64Bits(0.0);
        await Assert.That(result).IsEqualTo(0ul);
    }

    [Test]
    public async Task DoubleToUInt64Bits_One()
    {
        var result = BitConverter.DoubleToUInt64Bits(1.0);
        await Assert.That(result).IsEqualTo(0x3FF0000000000000ul);
    }

    [Test]
    public async Task UInt64BitsToDouble_RoundTrip()
    {
        var original = 0xDEADBEEFCAFEBABEul;
        var asDouble = BitConverter.UInt64BitsToDouble(original);
        var backToULong = BitConverter.DoubleToUInt64Bits(asDouble);
        await Assert.That(backToULong).IsEqualTo(original);
    }

    [Test]
    public async Task DoubleToUInt64Bits_RoundTrip()
    {
        var original = 3.141592653589793;
        var asULong = BitConverter.DoubleToUInt64Bits(original);
        var backToDouble = BitConverter.UInt64BitsToDouble(asULong);
        await Assert.That(backToDouble).IsEqualTo(original);
    }
}

#endif
