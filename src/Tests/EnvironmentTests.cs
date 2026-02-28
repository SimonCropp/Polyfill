public class EnvironmentTests
{
    [Test]
    public async Task ProcessPath()
    {
        var path = Environment.ProcessPath;
        await Assert.That(path).IsNotNull();
        await Assert.That(path!.Length).IsGreaterThan(0);
    }
}
