using System.Diagnostics.CodeAnalysis;
using Mono.Cecil;

#if NET8_0 && DEBUG
[TestFixture]
class BuildApiTest
{
    static string[] namespacesToClean =
    [
        "System.Diagnostics.",
        "System.Collections.Generic.",
        "System.Threading.Tasks.",
        "System.Threading.",
        "System.Net.Http.",
        "System.Text.",
        "System.IO.",
        "System."
    ];

    [Test]
    public void Run()
    {
        var solutionDirectory = SolutionDirectoryFinder.Find();
        var path = Path.Combine(solutionDirectory, "Consume", "bin", "Debug", "netstandard2.0", "Consume.dll");
        var md = Path.Combine(solutionDirectory, "..", "api_list.include.md");
        File.Delete(md);
        using var module = ModuleDefinition.ReadModule(path);
        var extensions = module.GetTypes().Single(_ => _.Name == nameof(Polyfill));
        using var writer = File.CreateText(md);

        writer.WriteLine($"### Extension methods");
        writer.WriteLine();
        foreach (var type in extensions.Methods
                     .Where(_ => !_.IsConstructor)
                     .GroupBy(_ => _.Parameters[0].ParameterType.FullName)
                     .OrderBy(_ => _.Key))
        {
            if (!type.Any(_ => _.IsPublic))
            {
                continue;
            }

            var targetType = type.Key;
            var targetFullName = targetType.Replace("`1", "").Replace("`2", "");
            writer.WriteLine($"#### {GetTypeName(targetFullName)}");
            writer.WriteLine();
            foreach (var method in PublicMethods(type))
            {
                WriteSignature(method, writer);
            }

            writer.WriteLine();
            writer.WriteLine();
        }
        writer.WriteLine($"### Static helpers");
        foreach (var type in extensions.Methods
                     .Where(_ => !_.IsConstructor)
                     .GroupBy(_ => _.Parameters[0].ParameterType.FullName)
                     .OrderBy(_ => _.Key))
        {
            if (!type.Any(_ => _.IsPublic))
            {
                continue;
            }

            var targetType = type.Key;
            writer.WriteLine($"#### {GetTypeName(targetType)}");
            writer.WriteLine();
            foreach (var method in PublicMethods(type))
            {
                WriteSignature(method, writer);
            }

            writer.WriteLine();
            writer.WriteLine();
        }
    }

    static string GetTypeName(string targetType)
    {
        var targetFullName = targetType.Replace("`1", "").Replace("`2", "");
        var name = targetFullName.Replace("`1", "").Replace("`2", "");
        foreach (var toClean in namespacesToClean)
        {
            name = name.Replace(toClean, "");
        }

        return name;
    }

    static IEnumerable<MethodDefinition> PublicMethods(IEnumerable<MethodDefinition> type) =>
        type.Where(_=>_.IsPublic)
            .OrderBy(_ => _.Name);

    static void WriteSignature(MethodDefinition method, StreamWriter writer)
    {
        var parameters = BuildParameters(method);
        var typeArgs = BuildTypeArgs(method);
        var signature = $"{GetTypeName(method.ReturnType.FullName)} {method.Name}{typeArgs}({parameters})";
        if (TryGetReference(method, out var reference))
        {
            writer.WriteLine($" * `{signature}` [reference]({reference})");
        }
        else
        {
            writer.WriteLine($" * `{signature}`");
        }
    }

    static string BuildParameters(MethodDefinition method) =>
        string.Join(", ", method.Parameters.Skip(1).Select(_ => GetTypeName(_.ParameterType.FullName)));

    static string BuildTypeArgs(MethodDefinition method)
    {
        var typeArgs = "";
        if (method.HasGenericParameters)
        {
            typeArgs = $"<{string.Join(", ", method.GenericParameters.Select(_ => _.Name))}>";
        }

        return typeArgs;
    }

    static bool TryGetReference(MethodDefinition method,[NotNullWhen(true)] out string? reference)
    {
        var descriptionAttribute = method.CustomAttributes
            .SingleOrDefault(_ => _.AttributeType.Name == "DescriptionAttribute");
        if (descriptionAttribute == null)
        {
            reference = null;
            return false;
        }

        reference = (string) descriptionAttribute.ConstructorArguments.Single().Value!;
        return true;
    }
}
#endif