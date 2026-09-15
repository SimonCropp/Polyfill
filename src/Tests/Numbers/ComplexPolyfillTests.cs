#if FeatureNumerics && NET7_0_OR_GREATER
using System.Numerics;
using System.Text;

public class ComplexPolyfillTests
{
    static readonly Complex expected = new(1.5, -2.25);

    static byte[] ExpectedUtf8() =>
        Encoding.UTF8.GetBytes(expected.ToString(CultureInfo.InvariantCulture));

    [Test]
    public async Task TryParse()
    {
        await Assert.That(Complex.TryParse(ExpectedUtf8(), CultureInfo.InvariantCulture, out var value)).IsTrue();
        await Assert.That(value).IsEqualTo(expected);

        await Assert.That(Complex.TryParse(ExpectedUtf8(), NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out value)).IsTrue();
        await Assert.That(value).IsEqualTo(expected);

        await Assert.That(Complex.TryParse("abc"u8, CultureInfo.InvariantCulture, out value)).IsFalse();
    }
}
#endif
