#pragma warning disable IL2026

[TestFixture]
public class SanityChecks
{
    [Test]
    public void NoPublicTypes()
    {
        var visibleTypes = typeof(SanityChecks).Assembly
            .GetExportedTypes()
            .Where(type => type.Namespace?.StartsWith("System") == true)
            .ToList();
#if PolyPublic
#if !NET7_0_OR_GREATER
        Assert.That(visibleTypes, Is.Not.Empty);
#endif
#else
        Assert.That(visibleTypes, Is.Empty);
#endif
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
                name == "RefSafetyRulesAttribute" ||
                name == "ScopedRefAttribute" ||
                name == "IsByRefLikeAttribute")
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
