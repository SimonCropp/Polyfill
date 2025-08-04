[TestFixture]
public class RandomPolyfillTests
{
    [Test, Repeat(100)]
    public void NextInt()
    {
        var number = Random.Shared.Next(10);

        Assert.That(number, Is.InRange(0, 10));
    }
}