[TestFixture]
public class BuildApiTest
{
    static string solutionDirectory;
    static string polyfillDir;

    static BuildApiTest()
    {
        solutionDirectory = SolutionDirectoryFinder.Find();
        polyfillDir = Path.Combine(solutionDirectory, "Polyfill");
    }

    [Test]
    public void RunWithRoslyn()
    {
        var md = Path.Combine(solutionDirectory, "..", "api_list.include.md");
        File.Delete(md);
        using var writer = File.CreateText(md);
        var count = 0;
        count = WriteExtensions(writer, count);

        WriteHelper("Guard*", writer, ref count);
        WriteHelper("Lock", writer, ref count);
        WriteHelper(nameof(KeyValuePair), writer, ref count);
        WriteType(nameof(TaskCompletionSource), writer, ref count);
        WriteType(nameof(UnreachableException), writer, ref count);

        count += Directory.EnumerateFiles(polyfillDir, "*Attribute.cs", SearchOption.AllDirectories).Count();
        // Index and Range
        count++;
        //Nullability*
        count += 3;

        var countMd = Path.Combine(solutionDirectory, "..", "apiCount.include.md");
        File.Delete(countMd);
        File.WriteAllText(countMd, $"**API count: {count}**");
    }

    static int WriteExtensions(StreamWriter writer, int count)
    {
        var files = Directory.EnumerateFiles(polyfillDir, "*Polyfill*.cs", SearchOption.AllDirectories).ToList();

        var instanceFiles = files
            .Where(_ => Path.GetFileNameWithoutExtension(_).StartsWith("Polyfill_"))
            .ToList();

        var staticFiles = files
            .Where(_ =>
            {
                var name = Path.GetFileNameWithoutExtension(_);
                return name.EndsWith("Polyfill") &&
                       name != "Polyfill";
            })
            .ToList();

        var instanceMethods = ReadMethodsForFiles(instanceFiles);

        var instanceTypeNames = instanceMethods
            .Select(FirstParameterType);

        var staticTypeNames = staticFiles
            .Select(Path.GetFileNameWithoutExtension)
            .Select(_ => _![..^8].ToString());

        var typeNames = instanceTypeNames.Concat(staticTypeNames)
            .Distinct()
            .ToList();

        writer.WriteLine("### Extension methods");
        writer.WriteLine();

        foreach (var name in typeNames.Order())
        {
            var instanceMethodsForType = instanceMethods
                .Where(_ => FirstParameterType(_) == name)
                .ToList();
            writer.WriteLine($"#### {name}");
            writer.WriteLine();
            if (instanceMethodsForType.Count != 0)
            {
                foreach (var method in instanceMethodsForType.OrderBy(Key))
                {
                    count++;
                    WriteSignature(method, writer);
                }
            }

            var staticExtension = staticFiles
                .SingleOrDefault(_ => Path.GetFileNameWithoutExtension(_)[..^8].ToString() == name);
            if (staticExtension != null)
            {
                foreach (var method in ReadMethodsForFiles([staticExtension]).OrderBy(Key))
                {
                    count++;
                    WriteSignature(method, writer);
                }

                foreach (var property in ReadPropertiesForFiles([staticExtension]))
                {
                    count++;
                    WriteSignature(property, writer);
                }
            }

            writer.WriteLine();
            writer.WriteLine();
        }

        return count;
    }

    static Dictionary<string, string> langwordToType = new()
    {
        ["bool"] = "Boolean",
        ["byte"] = "Byte",
        ["sbyte"] = "SByte",
        ["char"] = "Char",
        ["decimal"] = "Decimal",
        ["double"] = "Double",
        ["float"] = "Single",
        ["int"] = "Int32",
        ["uint"] = "UInt32",
        ["long"] = "Int64",
        ["ulong"] = "UInt64",
        ["object"] = "Object",
        ["short"] = "Int16",
        ["ushort"] = "UInt16",
        ["string"] = "String"
    };

    static string FirstParameterType(Method method)
    {
        var type = method.ParameterList.Parameters[0].Type!.ToString();
        return langwordToType.GetValueOrDefault(type, type);
    }

    static List<Method> ReadMethodsForFiles(string pattern)
    {
        var files = Directory.EnumerateFiles(polyfillDir, $"{pattern}.cs", SearchOption.AllDirectories);
        return ReadMethodsForFiles(files);
    }

    private static List<Method> ReadMethodsForFiles(IEnumerable<string> files)
    {
        var types = files
            .SelectMany(Identifiers.ReadTypesForFile)
            .ToList();

        var distinctTypes = types.Select(_ => _.Identifier.Text).Distinct().ToList();
        if (distinctTypes.Count > 1)
        {
            throw new(string.Join(", ", distinctTypes));
        }

        return types
            .SelectMany(_ => _.PublicMethods())
            .DistinctBy(Key)
            .OrderBy(Key)
            .ToList();
    }

    static List<Property> ReadPropertiesForFiles(string pattern)
    {
        var files = Directory.EnumerateFiles(polyfillDir, $"{pattern}.cs", SearchOption.AllDirectories);
        return ReadPropertiesForFiles(files);
    }

    static List<Property> ReadPropertiesForFiles(IEnumerable<string> files)
    {
        var types = files
            .SelectMany(Identifiers.ReadTypesForFile)
            .ToList();

        var distinctTypes = types.DistinctBy(_ => _.Identifier.Text).ToList();
        if (distinctTypes.Count > 1)
        {
            throw new(string.Join(", ", distinctTypes));
        }

        return types
            .SelectMany(_ => _.PublicProperties())
            .DistinctBy(_ => _.Identifier.Text)
            .OrderBy(_ => _.Identifier.Text)
            .ToList();
    }

    static void WriteType(string name, StreamWriter writer, ref int count)
    {
        writer.WriteLine(
            $"""
             #### {name}

             """);
        count++;
    }

    static void WriteHelper(string name, StreamWriter writer, ref int count) =>
        WriteTypeMethods(name.Trim('*'), writer, ref count, ReadMethodsForFiles(name));

    static void WriteTypeMethods(string name, StreamWriter writer, ref int count, IEnumerable<Method> methods)
    {
        writer.WriteLine($"#### {name}");
        writer.WriteLine();
        foreach (var method in methods.OrderBy(Key))
        {
            count++;
            WriteSignature(method, writer);
        }

        writer.WriteLine();
        writer.WriteLine();
    }

    static void WriteSignature(Method method, StreamWriter writer)
    {
        var signature = new StringBuilder($"{method.ReturnType} {method.Identifier.Text}{BuildTypeArgs(method)}({BuildParameters(method, true)})");

        if (method.ConstraintClauses.Count > 0)
        {
            foreach (var constraint in method.ConstraintClauses)
            {
                signature.Append($" where {constraint.Name} : ");
                signature.Append(string.Join(", ", constraint.Constraints.Select(_ => _.ToString())));
            }
        }

        if (method.TryGetReference(out var reference))
        {
            writer.WriteLine($" * `{signature}` [reference]({reference})");
        }
        else
        {
            writer.WriteLine($" * `{signature}`");
        }
    }

    static void WriteSignature(Property method, StreamWriter writer)
    {
        var signature = new StringBuilder(method.Identifier.Text);

        if (method.TryGetReference(out var reference))
        {
            writer.WriteLine($" * `{signature}` [reference]({reference})");
        }
        else
        {
            writer.WriteLine($" * `{signature}`");
        }
    }

    static string Key(Method method) =>
        $"{method.Identifier.Text}{BuildTypeArgs(method)}({BuildParameters(method, false)})";

    static string BuildTypeArgs(Method method)
    {
        var types = method.TypeParameterList;
        if (types == null || types.Parameters.Count == 0)
        {
            return string.Empty;
        }

        return $"<{string.Join(", ", types.Parameters.Select(_ => _.Identifier.Text))}>";
    }

    static string BuildParameters(Method method, bool skipThisModified)
    {
        var parameters = method
            .ParameterList
            .Parameters
            .ToList();

        if (skipThisModified)
        {
            parameters = parameters
                .Where(_ => !_.IsThis())
                .ToList();
        }

        if (parameters.Count > 0)
        {
            var last = parameters.Last();
            if (last.IsCaller())
            {
                parameters.Remove(last);
            }
        }

        return string.Join(", ", parameters.Select(_ => _.Type!.ToString()));
    }
}