using System.Security.Cryptography;

public class RandomNumberGeneratorPolyfillTests
{
    [Test]
    public async Task GetInt32_Range_Valid()
    {
        var min = 5;
        var max = 10;
        for (var i = 0; i < 100; i++)
        {
            var value = RandomNumberGenerator.GetInt32(min, max);
            await Assert.That(value).IsGreaterThanOrEqualTo(min);
            await Assert.That(value).IsLessThan(max);
        }
    }

    [Test]
    public async Task GetInt32_Range_Invalid_Throws()
    {
        await Assert.That(() => RandomNumberGenerator.GetInt32(10, 5)).Throws<ArgumentException>();
        await Assert.That(() => RandomNumberGenerator.GetInt32(5, 5)).Throws<ArgumentException>();
    }

    [Test]
    public async Task GetInt32_Exclusive_Valid()
    {
        var max = 10;
        for (var i = 0; i < 100; i++)
        {
            var value = RandomNumberGenerator.GetInt32(max);
            await Assert.That(value).IsGreaterThanOrEqualTo(0);
            await Assert.That(value).IsLessThan(max);
        }
    }

    [Test]
    public async Task GetInt32_Exclusive_Invalid_Throws()
    {
        await Assert.That(() => RandomNumberGenerator.GetInt32(0)).Throws<ArgumentOutOfRangeException>();
        await Assert.That(() => RandomNumberGenerator.GetInt32(-1)).Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task GetBytes_ReturnsArrayOfCorrectLength()
    {
        var bytes = RandomNumberGenerator.GetBytes(16);
        await Assert.That(bytes.Length).IsEqualTo(16);
        await Assert.That(bytes.Any(b => b != 0)).IsTrue();
    }

    [Test]
    public async Task GetBytes_Invalid_Throws() =>
        await Assert.That(() => RandomNumberGenerator.GetBytes(-1)).Throws<ArgumentOutOfRangeException>();

#if FeatureMemory
    [Test]
    public async Task Fill_FillsSpan()
    {
        Span<byte> data = new byte[8];
        RandomNumberGenerator.Fill(data);
        await Assert.That(data.ToArray().Any(b => b != 0)).IsTrue();
    }

    [Test]
    public async Task GetHexString_Span_FillsWithHex()
    {
        Span<char> chars = new char[8];
        RandomNumberGenerator.GetHexString(chars, lowercase: true);
        await Assert.That(chars.ToArray().All(c => "0123456789abcdef".Contains(c))).IsTrue();
    }
#endif

    [Test]
    public async Task GetHexString_StringLength_Valid()
    {
        var hex = RandomNumberGenerator.GetHexString(12, lowercase: false);
        await Assert.That(hex.Length).IsEqualTo(12);
        await Assert.That(hex.All(c => "0123456789ABCDEF".Contains(c))).IsTrue();
    }

    [Test]
    public async Task GetHexString_StringLength_Invalid_Throws() =>
        await Assert.That(() => RandomNumberGenerator.GetHexString(-1)).Throws<ArgumentOutOfRangeException>();
}
