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
        var polyfillFiles = Directory.EnumerateFiles(polyfillDir, "*Polyfill*.cs", SearchOption.AllDirectories).ToList();
        var instanceExtensions = polyfillFiles
            .Where(_ => Path.GetFileNameWithoutExtension(_).StartsWith("Polyfill_"))
            .ToList();
        var staticExtensions = polyfillFiles
            .Where(_ =>
            {
                var name = Path.GetFileNameWithoutExtension(_);
                return name.EndsWith("Polyfill") &&
                       name != "Polyfill";
            })
            .ToList();
        var instanceExtensionMethods = ReadMethodsForFiles(instanceExtensions);
        writer.WriteLine("### Extension methods");
        writer.WriteLine();
        foreach (var grouping in instanceExtensionMethods
                     .GroupBy(_ => _.ParameterList.Parameters[0].Type!.ToString())
                     .OrderBy(_ => _.Key))
        {
            WriteTypeMethods(grouping.Key, writer, ref count, grouping);
        }

        var types = ReadFiles();

        writer.WriteLine("### Static helpers");
        writer.WriteLine();
        foreach (var (key, value) in types
                     .OrderBy(_ => _.Key)
                     .Where(_ => _.Key.EndsWith("Polyfill") &&
                                 _.Key != "Polyfill"))
        {
            WriteTypeMethods(key, writer, ref count, value);
        }

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

    static IEnumerable<Method> PublicMethods(HashSet<Method> type) =>
        type.Where(_ => _.IsPublic() &&
                        !_.IsConstructor())
            .OrderBy(_ => _.Identifier.ToString());

    static Dictionary<string, HashSet<Method>> ReadFiles()
    {
        var types = new Dictionary<string, HashSet<Method>>();
        var methodComparer = EqualityComparer<Method>
            .Create(
                (x, y) => Key(x!) == Key(y!),
                _ => Key(_).GetHashCode());
        foreach (var file in Directory.EnumerateFiles(polyfillDir, "*.cs", SearchOption.AllDirectories))
        {
            foreach (var type in Identifiers.ReadTypesForFile(file))
            {
                var identifier = type.Identifier.Text;
                if (!types.TryGetValue(identifier, out var methods))
                {
                    methods = new(methodComparer);
                    types.Add(identifier, methods);
                }

                foreach (var method in type.PublicMethods())
                {
                    methods.Add(method);
                }
            }
        }

        return types;
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

        var distinctTypes = types.DistinctBy(_ => _.Identifier.Text).ToList();
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
        List<Parameter> parameters;
        if (skipThisModified)
        {
            parameters = method.ParameterList
                .Parameters
                .Where(_ => !_.IsThis()).ToList();
        }
        else
        {
            parameters = method.ParameterList
                .Parameters.ToList();
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