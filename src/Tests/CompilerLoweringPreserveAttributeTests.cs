using System.Reflection;

public class CompilerLoweringPreserveAttributeTests
{
    [Test]
    public async Task Run()
    {
        var attribute = typeof(Decorated).GetCustomAttribute<CompilerLoweringPreserveAttribute>();
        await Assert.That(attribute).IsNotNull();
    }

    [CompilerLoweringPreserve]
    class Decorated : Attribute;
}
