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

    static string solutionDirectory = SolutionDirectoryFinder.Find();

    [Test]
    public void Run()
    {
        var types = GetTypes();
        var extensions = types[nameof(Polyfill)];

        var md = Path.Combine(solutionDirectory, "..", "api_list.include.md");
        File.Delete(md);
        using var writer = File.CreateText(md);
        var count = 0;
        writer.WriteLine("### Extension methods");
        writer.WriteLine();
        foreach (var type in PublicMethods(extensions)
                     .GroupBy(_ => _.Parameters[0].ParameterType.FullName)
                     .OrderBy(_ => _.Key))
        {
            writer.WriteLine($"#### {GetTypeName(type.Key)}");
            writer.WriteLine();
            foreach (var method in type)
            {
                count++;
                WriteSignature(method, writer);
            }

            writer.WriteLine();
            writer.WriteLine();
        }

        writer.WriteLine("### Static helpers");
        writer.WriteLine();

        WriteHelper(types, nameof(EnumPolyfill), writer, ref count);
        WriteHelper(types, "RegexPolyfill", writer, ref count);
        WriteHelper(types, "StringPolyfill", writer, ref count);
        WriteHelper(types, "BytePolyfill", writer, ref count);
        WriteHelper(types, "DoublePolyfill", writer, ref count);
        WriteHelper(types, "IntPolyfill", writer, ref count);
        WriteHelper(types, "LongPolyfill", writer, ref count);
        WriteHelper(types, "SBytePolyfill", writer, ref count);
        WriteHelper(types, "ShortPolyfill", writer, ref count);
        WriteHelper(types, "UIntPolyfill", writer, ref count);
        WriteHelper(types, "ULongPolyfill", writer, ref count);
        WriteHelper(types, "UShortPolyfill", writer, ref count);

        count += types.Count(_ => _.Key.EndsWith("Attribute"));
        var countMd = Path.Combine(solutionDirectory, "..", "apiCount.include.md");
        File.Delete(countMd);
        File.WriteAllText(countMd, $"**API count: {count}**");
    }

    static Dictionary<string, List<MethodDefinition>> GetTypes()
    {
        var types = new Dictionary<string, List<MethodDefinition>>();

        var output = Path.Combine(solutionDirectory, "Consume", "bin", "Debug");

        foreach (var assembly in Directory.EnumerateFiles(output, "Consume.dll", SearchOption.AllDirectories))
        {
            ProcessAssembly(assembly, types);
        }

        return types;
    }

    static void ProcessAssembly(string path, Dictionary<string, List<MethodDefinition>> types)
    {
        var module = ModuleDefinition.ReadModule(path);

        foreach (var type in module
                     .GetTypes()
                     .Where(_=>!_.IsNested))
        {
            if(!types.TryGetValue(type.Name, out var methods))
            {
                types[type.Name] = methods = new();
            }

            foreach (var method in type.Methods)
            {
                if (methods.Any(_ => _.FullName == method.FullName))
                {
                    continue;
                }
                methods.Add(method);
            }
        }
    }

    static void WriteHelper(Dictionary<string, List<MethodDefinition>> types, string name, StreamWriter writer, ref int count)
    {
        var methods = types[name];

        writer.WriteLine($"#### {name}");
        writer.WriteLine();
        foreach (var method in PublicMethods(methods))
        {
            count++;
            WriteSignature(method, writer);
        }

        writer.WriteLine();
        writer.WriteLine();
    }

    static string GetTypeName(string targetType)
    {
        var name = targetType.Replace("`1", "").Replace("`2", "").Replace("`3", "");
        foreach (var toClean in namespacesToClean)
        {
            name = name.Replace(toClean, "");
        }

        return name;
    }

    static IEnumerable<MethodDefinition> PublicMethods(IEnumerable<MethodDefinition> type) =>
        type.Where(_ => _ is {IsPublic: true, IsConstructor: false})
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

    static bool TryGetReference(MethodDefinition method, [NotNullWhen(true)] out string? reference)
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