public class DoublePolyfillTest
{
    [Test]
    public async Task TryParse_AllowsThousands()
    {
        var invariant = CultureInfo.InvariantCulture;

        await Assert.That(double.TryParse("1,234.5", invariant, out var value)).IsTrue();
        await Assert.That(value).IsEqualTo(1234.5);

        await Assert.That(double.TryParse("1,234.5".AsSpan(), invariant, out var spanValue)).IsTrue();
        await Assert.That(spanValue).IsEqualTo(1234.5);
    }

    [Test]
    public async Task TryParse()
    {
        await Assert.That(double.TryParse("1"u8, null, out var value)).IsTrue();
        await Assert.That(value).IsEqualTo(1);

        await Assert.That(double.TryParse(['1'], out value)).IsTrue();
        await Assert.That(value).IsEqualTo(1);

        await Assert.That(double.TryParse(['1'], null, out value)).IsTrue();
        await Assert.That(value).IsEqualTo(1);

        await Assert.That(double.TryParse("1", null, out value)).IsTrue();
        await Assert.That(value).IsEqualTo(1);

        await Assert.That(double.TryParse("1"u8, NumberStyles.Integer, null, out value)).IsTrue();
        await Assert.That(value).IsEqualTo(1);

        await Assert.That(double.TryParse("1"u8, out value)).IsTrue();
        await Assert.That(value).IsEqualTo(1);

        await Assert.That(double.TryParse(['1'], NumberStyles.Integer, null, out value)).IsTrue();
        await Assert.That(value).IsEqualTo(1);
    }
}