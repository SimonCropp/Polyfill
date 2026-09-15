#if FeatureNumerics && FeatureMemory
using System.Numerics;
using System.Text;

public class BigIntegerPolyfillTests
{
    const string large = "123456789012345678901234567890";

    [Test]
    public async Task Parse()
    {
        await Assert.That(BigInteger.Parse("123456789012345678901234567890"u8)).IsEqualTo(BigInteger.Parse(large));
        await Assert.That(BigInteger.Parse("-42"u8, CultureInfo.InvariantCulture)).IsEqualTo(new BigInteger(-42));
        await Assert.That(BigInteger.Parse("FF"u8, NumberStyles.HexNumber, CultureInfo.InvariantCulture))
            .IsEqualTo(BigInteger.Parse("FF", NumberStyles.HexNumber, CultureInfo.InvariantCulture));
        await Assert.That(() => BigInteger.Parse("abc"u8)).Throws<FormatException>();
    }

    [Test]
    public async Task TryParse()
    {
        await Assert.That(BigInteger.TryParse("123456789012345678901234567890"u8, out var value)).IsTrue();
        await Assert.That(value).IsEqualTo(BigInteger.Parse(large));

        await Assert.That(BigInteger.TryParse("-42"u8, CultureInfo.InvariantCulture, out value)).IsTrue();
        await Assert.That(value).IsEqualTo(new BigInteger(-42));

        await Assert.That(BigInteger.TryParse("1,000"u8, NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out value)).IsTrue();
        await Assert.That(value).IsEqualTo(new BigInteger(1000));

        await Assert.That(BigInteger.TryParse("abc"u8, out value)).IsFalse();
    }

    [Test]
    public async Task TryFormat()
    {
        var value = BigInteger.Parse(large);
        var buffer = new byte[64];

        await Assert.That(value.TryFormat(buffer, out var written)).IsTrue();
        await Assert.That(Encoding.UTF8.GetString(buffer, 0, written)).IsEqualTo(large);

        await Assert.That(value.TryFormat(buffer, out written, "N0", CultureInfo.InvariantCulture)).IsTrue();
        await Assert.That(Encoding.UTF8.GetString(buffer, 0, written)).IsEqualTo(value.ToString("N0", CultureInfo.InvariantCulture));

        await Assert.That(value.TryFormat(new byte[2], out written)).IsFalse();
        await Assert.That(written).IsEqualTo(0);
    }
}
#endif
