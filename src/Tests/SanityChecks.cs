using System.Diagnostics.CodeAnalysis;

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

    [Test]
    public void Attributes()
    {
        foreach (var type in typeof(SanityChecks).Assembly.GetTypes())
        {
            if (type.Namespace?.StartsWith("System") == false)
            {
                continue;
            }

            var name = type.Name;
            if (!name.EndsWith("Attribute") ||
                name == "EmbeddedAttribute" ||
                name == "NullableAttribute" ||
                name == "NullableContextAttribute" ||
                name == "IsReadOnlyAttribute" ||
                name == "RefSafetyRulesAttribute")
            {
                continue;
            }

            if (type.GetCustomAttribute(typeof(ExcludeFromCodeCoverageAttribute)) == null)
            {
                throw new($"{name} must have ExcludeFromCodeCoverageAttribute");
            }
            if (type.GetCustomAttribute(typeof(DebuggerNonUserCodeAttribute)) == null)
            {
                throw new($"{name} must have DebuggerNonUserCode");
            }
        }
    }
}
