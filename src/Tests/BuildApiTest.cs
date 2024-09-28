#if NET8_0 && DEBUG
using Mono.Cecil;

[TestFixture]
class BuildApiTest
{
    static string[] namespacesToClean =
    [
        "System.Text.RegularExpressions.",
        "System.Diagnostics.",
        "System.Diagnostics.CodeAnalysis.",
        "System.Collections.Generic.",
        "System.Threading.Tasks.",
        "System.Threading.",
        "System.Net.Http.",
        "System.Text.",
        "System.IO.",
        "System."
    ];

    static string solutionDirectory = SolutionDirectoryFinder.Find();

    static Dictionary<string, List<MethodDefinition>> types = GetTypes();

    [Test]
    public void Run()
    {
        var extensions = types[nameof(Polyfill)];

        var md = Path.Combine(solutionDirectory, "..", "api_list.include.md");
        File.Delete(md);
        using var writer = File.CreateText(md);
        var count = 0;
        writer.WriteLine("### Extension methods");
        writer.WriteLine();
        foreach (var type in PublicMethods(extensions)
                     .GroupBy(_ => _.Parameters[0].ParameterType.FullName)
                     .OrderBy(_ => GetTypeName(_.Key)))
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
        WriteHelper(types, "Guard", writer, ref count);
        WriteHelper(types, "Lock", writer, ref count);
        WriteType(nameof(TaskCompletionSource), writer, ref count);

        count += types.Count(_ => _.Key.EndsWith("Attribute"));
        var countMd = Path.Combine(solutionDirectory, "..", "apiCount.include.md");
        File.Delete(countMd);
        File.WriteAllText(countMd, $"**API count: {count}**");
    }

    [Test]
    public void RunGuard()
    {
        var md = Path.Combine(solutionDirectory, "..", "api_guard.include.md");
        File.Delete(md);
        using var writer = File.CreateText(md);
        var count = 0;
        WriteHelper(types, "Guard", writer, ref count);
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

    static void WriteType(string name, StreamWriter writer, ref int count)
    {
        writer.WriteLine($"#### {name}");
        count++;
    }

    static string GetTypeName(string targetType)
    {
        var name = targetType.Replace("`1", "").Replace("`2", "").Replace("`3", "");
        foreach (var toClean in namespacesToClean)
        {
            name = name.Replace(toClean, "");
        }

        if (name == "Void")
        {
            return "void";
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
        var signature = new StringBuilder($"{GetTypeName(method.ReturnType.FullName)} {method.Name}{typeArgs}({parameters})");
        if (method.HasGenericParameters)
        {
            var withConstraints = method.GenericParameters.Where(_ => _.HasConstraints).ToList();
            if (withConstraints.Count > 0)
            {
                foreach (var genericParameter in withConstraints)
                {
                    signature.Append(" where ");
                    signature.Append(genericParameter.Name);
                    signature.Append(" : ");
                    signature.Append(string.Join(", ", genericParameter.Constraints.Select(_ => GetTypeName(_.ConstraintType.FullName))));
                }
            }
        }

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
        if (method.Parameters.Count == 0)
        {
            return "";
        }

        List<ParameterDefinition> parameters;
        if (IsExtensionMethod(method))
        {
            parameters = method.Parameters.Skip(1).ToList();
            if (parameters.Count == 0)
            {
                return "";
            }
        }
        else
        {
            parameters = method.Parameters.ToList();
        }

        var last = parameters.Last();
        if (last.CustomAttributes.Any(_ => _.AttributeType.Name.StartsWith("Caller")))
        {
            parameters.Remove(last);
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