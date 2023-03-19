[TestFixture]
public class SanityChecks
{
    [Test]
    public void NoPublicTypes()
    {
        var visibleTypes = typeof(SanityChecks).Assembly
            .GetExportedTypes()
            .Where(type => type.Namespace?.StartsWith("System") == true || type.Namespace == "Polyfill")
            .ToList();

        Assert.That(visibleTypes, Is.Empty);
    }
}
