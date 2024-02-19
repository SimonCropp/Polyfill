#if NET8_0 && DEBUG
using System.Diagnostics.CodeAnalysis;
using Mono.Cecil;

[TestFixture]
class BuildApiTest
{
    static string[] namespacesToClean =
    [
        "System.Text.RegularExpressions.",
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
        var types = module.GetTypes().ToList();
        var extensions = types.Single(_ => _.Name == nameof(Polyfill));
        using var writer = File.CreateText(md);

        writer.WriteLine($"### Extension methods");
        writer.WriteLine();
        foreach (var type in PublicMethods(extensions.Methods)
                     .GroupBy(_ => _.Parameters[0].ParameterType.FullName)
                     .OrderBy(_ => _.Key))
        {
            writer.WriteLine($"#### {GetTypeName(type.Key)}");
            writer.WriteLine();
            foreach (var method in type)
            {
                WriteSignature(method, writer);
            }

            writer.WriteLine();
            writer.WriteLine();
        }
        writer.WriteLine($"### Static helpers");
        writer.WriteLine();

        WriteHelper(types, nameof(EnumPolyfill), writer);
        WriteHelper(types, "RegexPolyfill", writer);
    }

    static void WriteHelper(List<TypeDefinition> types, string name, StreamWriter writer)
    {
        var helper = types.Single(_ => _.Name == name);

        writer.WriteLine($"#### {helper.Name}");
        writer.WriteLine();
        foreach (var method in PublicMethods(helper.Methods))
        {
            WriteSignature(method, writer);
        }
        writer.WriteLine();
        writer.WriteLine();
    }

    static string GetTypeName(string targetType)
    {
        var name = targetType.Replace("`1", "").Replace("`2", "");
        foreach (var toClean in namespacesToClean)
        {
            name = name.Replace(toClean, "");
        }

        return name;
    }

    static IEnumerable<MethodDefinition> PublicMethods(IEnumerable<MethodDefinition> type) =>
        type.Where(_=>_ is {IsPublic: true, IsConstructor: false})
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

    static string BuildParameters(MethodDefinition method)
    {
        IEnumerable<ParameterDefinition> parameters;
        if (IsExtensionMethod(method))
        {
            parameters = method.Parameters.Skip(1);
        }
        else
        {

            parameters = method.Parameters;
        }

        return string.Join(", ", parameters.Select(_ => GetTypeName(_.ParameterType.FullName)));
    }

    static bool IsExtensionMethod(MethodDefinition method) =>
        method.CustomAttributes.Any(_ => _.AttributeType.Name == "ExtensionAttribute");

    static string BuildTypeArgs(MethodDefinition method)
    {
        if (method.HasGenericParameters)
        {
            return $"<{string.Join(", ", method.GenericParameters.Select(_ => _.Name))}>";
        }

        return "";
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