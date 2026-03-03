public class MathPolyfillTests
{
    [Test]
    public async Task Clamp_Int_ReturnsValue_WhenInRange()
    {
        await Assert.That(Math.Clamp(5, 1, 10)).IsEqualTo(5);
    }

    [Test]
    public async Task Clamp_Int_ReturnsMin_WhenBelowRange()
    {
        await Assert.That(Math.Clamp(-5, 1, 10)).IsEqualTo(1);
    }

    [Test]
    public async Task Clamp_Int_ReturnsMax_WhenAboveRange()
    {
        await Assert.That(Math.Clamp(15, 1, 10)).IsEqualTo(10);
    }

    [Test]
    public async Task Clamp_Double()
    {
        await Assert.That(Math.Clamp(1.5, 2.0, 3.0)).IsEqualTo(2.0);
        await Assert.That(Math.Clamp(2.5, 2.0, 3.0)).IsEqualTo(2.5);
        await Assert.That(Math.Clamp(3.5, 2.0, 3.0)).IsEqualTo(3.0);
    }

    [Test]
    public async Task Clamp_Float()
    {
        await Assert.That(Math.Clamp(1.5f, 2.0f, 3.0f)).IsEqualTo(2.0f);
        await Assert.That(Math.Clamp(2.5f, 2.0f, 3.0f)).IsEqualTo(2.5f);
        await Assert.That(Math.Clamp(3.5f, 2.0f, 3.0f)).IsEqualTo(3.0f);
    }

    [Test]
    public async Task Clamp_Long()
    {
        await Assert.That(Math.Clamp(5L, 1L, 10L)).IsEqualTo(5L);
        await Assert.That(Math.Clamp(-5L, 1L, 10L)).IsEqualTo(1L);
        await Assert.That(Math.Clamp(15L, 1L, 10L)).IsEqualTo(10L);
    }

    [Test]
    public async Task Clamp_Short()
    {
        await Assert.That(Math.Clamp((short)5, (short)1, (short)10)).IsEqualTo((short)5);
    }

    [Test]
    public async Task Clamp_Byte()
    {
        await Assert.That(Math.Clamp((byte)5, (byte)1, (byte)10)).IsEqualTo((byte)5);
        await Assert.That(Math.Clamp((byte)0, (byte)1, (byte)10)).IsEqualTo((byte)1);
    }

    [Test]
    public async Task Clamp_Decimal()
    {
        await Assert.That(Math.Clamp(5.5m, 1.0m, 10.0m)).IsEqualTo(5.5m);
        await Assert.That(Math.Clamp(0.5m, 1.0m, 10.0m)).IsEqualTo(1.0m);
    }

    [Test]
    public async Task Clamp_ThrowsWhenMinGreaterThanMax()
    {
        await Assert.That(() => Math.Clamp(5, 10, 1)).Throws<ArgumentException>();
    }
}
