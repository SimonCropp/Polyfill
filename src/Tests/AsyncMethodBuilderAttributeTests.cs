public class AsyncMethodBuilderAttributeTests
{
    [Test]
    public async Task Run()
    {
        var attribute = new AsyncMethodBuilderAttribute(typeof(string));
        await Assert.That(attribute.BuilderType).IsEqualTo(typeof(string));
    }
}
