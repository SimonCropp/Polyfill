using System.Security.Cryptography;

[TestFixture]
public class RandomNumberGeneratorPolyfillTests
{
    [Test]
    public void GetInt32_Range_Valid()
    {
        var min = 5;
        var max = 10;
        for (var i = 0; i < 100; i++)
        {
            var value = RandomNumberGenerator.GetInt32(min, max);
            Assert.That(value, Is.GreaterThanOrEqualTo(min).And.LessThan(max));
        }
    }

    [Test]
    public void GetInt32_Range_Invalid_Throws()
    {
        Assert.Throws<ArgumentException>(() => RandomNumberGenerator.GetInt32(10, 5));
        Assert.Throws<ArgumentException>(() => RandomNumberGenerator.GetInt32(5, 5));
    }

    [Test]
    public void GetInt32_Exclusive_Valid()
    {
        var max = 10;
        for (var i = 0; i < 100; i++)
        {
            var value = RandomNumberGenerator.GetInt32(max);
            Assert.That(value, Is.GreaterThanOrEqualTo(0).And.LessThan(max));
        }
    }

    [Test]
    public void GetInt32_Exclusive_Invalid_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => RandomNumberGenerator.GetInt32(0));
        Assert.Throws<ArgumentOutOfRangeException>(() => RandomNumberGenerator.GetInt32(-1));
    }

    [Test]
    public void GetBytes_ReturnsArrayOfCorrectLength()
    {
        var bytes = RandomNumberGenerator.GetBytes(16);
        Assert.That(bytes.Length, Is.EqualTo(16));
        Assert.That(bytes, Is.Not.All.EqualTo(0));
    }

    [Test]
    public void GetBytes_Invalid_Throws() =>
        Assert.Throws<ArgumentOutOfRangeException>(() => RandomNumberGenerator.GetBytes(-1));

#if FeatureMemory
    [Test]
    public void Fill_FillsSpan()
    {
        Span<byte> data = new byte[8];
        RandomNumberGenerator.Fill(data);
        Assert.That(data.ToArray(), Has.Some.Not.EqualTo(0));
    }

    [Test]
    public void GetHexString_Span_FillsWithHex()
    {
        Span<char> chars = new char[8];
        RandomNumberGenerator.GetHexString(chars, lowercase: true);
        Assert.That(chars.ToArray(), Is.All.Matches<char>(c => "0123456789abcdef".Contains(c)));
    }
#endif

    [Test]
    public void GetHexString_StringLength_Valid()
    {
        var hex = RandomNumberGenerator.GetHexString(12, lowercase: false);
        Assert.That(hex.Length, Is.EqualTo(12));
        Assert.That(hex, Is.All.Matches<char>(c => "0123456789ABCDEF".Contains(c)));
    }

    [Test]
    public void GetHexString_StringLength_Invalid_Throws() =>
        Assert.Throws<ArgumentOutOfRangeException>(() => RandomNumberGenerator.GetHexString(-1));
}