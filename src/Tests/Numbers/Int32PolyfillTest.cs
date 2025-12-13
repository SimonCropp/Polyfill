public class Int32PolyfillTest
{
    [Test]
    public async Task TryParse()
    {
        await Assert.That(int.TryParse("1"u8, null, out var value)).IsTrue();
        await Assert.That(value).IsEqualTo(1);

        await Assert.That(int.TryParse(['1'], out value)).IsTrue();
        await Assert.That(value).IsEqualTo(1);

        await Assert.That(int.TryParse(['1'], null, out value)).IsTrue();
        await Assert.That(value).IsEqualTo(1);

        await Assert.That(int.TryParse("1", null, out value)).IsTrue();
        await Assert.That(value).IsEqualTo(1);

        await Assert.That(int.TryParse("1"u8, NumberStyles.Integer, null, out value)).IsTrue();
        await Assert.That(value).IsEqualTo(1);

        await Assert.That(int.TryParse("1"u8, out value)).IsTrue();
        await Assert.That(value).IsEqualTo(1);

        await Assert.That(int.TryParse(['1'], NumberStyles.Integer, null, out value)).IsTrue();
        await Assert.That(value).IsEqualTo(1);
    }
}