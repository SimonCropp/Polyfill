public class ConsolePolyfillTests
{
    [Test]
    public async Task OpenStandardInputHandle()
    {
        using var handle = Console.OpenStandardInputHandle();

        await Assert.That(handle.IsInvalid).IsFalse();
        await Assert.That(handle.IsClosed).IsFalse();
    }

    [Test]
    public async Task OpenStandardOutputHandle()
    {
        using var handle = Console.OpenStandardOutputHandle();

        await Assert.That(handle.IsInvalid).IsFalse();
        await Assert.That(handle.IsClosed).IsFalse();
    }

    [Test]
    public async Task OpenStandardErrorHandle()
    {
        using var handle = Console.OpenStandardErrorHandle();

        await Assert.That(handle.IsInvalid).IsFalse();
        await Assert.That(handle.IsClosed).IsFalse();
    }
}
