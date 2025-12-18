public class RandomPolyfillTests
{
    [Test, Repeat(100)]
    public async Task NextInt()
    {
        var number = Random.Shared.Next(10);

        await Assert.That(number).IsGreaterThanOrEqualTo(0).And.IsLessThanOrEqualTo(10);
    }
}