public class IntPtrPolyfillTest
{
    [Test]
    public async Task TryParse()
    {
        await Assert.That(nint.TryParse("1", out var value)).IsTrue();
        await Assert.That(value).IsEqualTo((nint)1);

        await Assert.That(nint.TryParse("1", NumberStyles.Integer, null, out value)).IsTrue();
        await Assert.That(value).IsEqualTo((nint)1);

        await Assert.That(nint.TryParse("1", null, out value)).IsTrue();
        await Assert.That(value).IsEqualTo((nint)1);

        await Assert.That(nint.TryParse("1"u8, null, out value)).IsTrue();
        await Assert.That(value).IsEqualTo((nint)1);

        await Assert.That(nint.TryParse(['1'], out value)).IsTrue();
        await Assert.That(value).IsEqualTo((nint)1);

        await Assert.That(nint.TryParse(['1'], null, out value)).IsTrue();
        await Assert.That(value).IsEqualTo((nint)1);

        await Assert.That(nint.TryParse("1"u8, NumberStyles.Integer, null, out value)).IsTrue();
        await Assert.That(value).IsEqualTo((nint)1);

        await Assert.That(nint.TryParse("1"u8, out value)).IsTrue();
        await Assert.That(value).IsEqualTo((nint)1);

        await Assert.That(nint.TryParse(['1'], NumberStyles.Integer, null, out value)).IsTrue();
        await Assert.That(value).IsEqualTo((nint)1);
    }
}
