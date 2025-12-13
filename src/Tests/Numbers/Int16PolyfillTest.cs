public class Int16PolyfillTest
{
    [Test]
    public async Task TryParse()
    {
        await Assert.That(short.TryParse("1"u8, null, out var value)).IsTrue();
        await Assert.That((int)value).IsEqualTo(1);

        await Assert.That(short.TryParse(['1'], out value)).IsTrue();
        await Assert.That((int)value).IsEqualTo(1);

        await Assert.That(short.TryParse(['1'], null, out value)).IsTrue();
        await Assert.That((int)value).IsEqualTo(1);

        await Assert.That(short.TryParse("1", null, out value)).IsTrue();
        await Assert.That((int)value).IsEqualTo(1);

        await Assert.That(short.TryParse("1"u8, NumberStyles.Integer, null, out value)).IsTrue();
        await Assert.That((int)value).IsEqualTo(1);

        await Assert.That(short.TryParse("1"u8, out value)).IsTrue();
        await Assert.That((int)value).IsEqualTo(1);

        await Assert.That(short.TryParse(['1'], NumberStyles.Integer, null, out value)).IsTrue();
        await Assert.That((int)value).IsEqualTo(1);
    }
}