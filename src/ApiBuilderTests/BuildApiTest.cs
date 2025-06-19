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
        var types = ReadFiles();

        var md = Path.Combine(solutionDirectory, "..", "api_list.include.md");
        File.Delete(md);
        using var writer = File.CreateText(md);
        var count = 0;

        var extensions = types.Single(_ => _.Key == "Polyfill").Value;
        writer.WriteLine("### Extension methods");
        writer.WriteLine();
        foreach (var grouping in PublicMethods(extensions)
                     .GroupBy(_ => _.ParameterList.Parameters[0].Type!.ToString())
                     .OrderBy(_ => _.Key))
        {
            WriteTypeMethods(grouping.Key, writer, ref count, grouping);
        }

        writer.WriteLine("### Static helpers");
        writer.WriteLine();

        count += types.Count(_ => _.Key.EndsWith("Attribute"));
        // Index and Range
        count++;
        //Nullability*
        count += 3;

        foreach (var (key, value) in types
                     .OrderBy(_ => _.Key)
                     .Where(_ => _.Key.EndsWith("Polyfill") &&
                                 _.Key != "Polyfill"))
        {
            WriteTypeMethods(key, writer, ref count, value.OrderBy(Key));
        }

        WriteHelper(types, "Guard", writer, ref count);
        WriteHelper(types, "Lock", writer, ref count);
        WriteHelper(types, nameof(KeyValuePair), writer, ref count);
        WriteType(nameof(TaskCompletionSource), writer, ref count);
        WriteType(nameof(UnreachableException), writer, ref count);

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

    static void WriteType(string name, StreamWriter writer, ref int count)
    {
        writer.WriteLine(
            $"""
             #### {name}

             """);
        count++;
    }

    static void WriteHelper(Dictionary<string, HashSet<Method>> types, string name, StreamWriter writer, ref int count)
    {
        var methods = types[name];

        WriteTypeMethods(name, writer, ref count, methods.OrderBy(Key));
    }

    static void WriteTypeMethods(string name, StreamWriter writer, ref int count, IEnumerable<Method> items)
    {
        writer.WriteLine($"#### {name}");
        writer.WriteLine();
        foreach (var method in items)
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