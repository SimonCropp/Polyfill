[TestFixture]
public partial class BuildApiTest
{
    static readonly string polyfillDir;
    static string solutionDirectory;

    [Test]
    public void RunWithRoslyn()
    {
        var types = ReadFiles();

        var md = Path.Combine(solutionDirectory, "..", "api_list.include.md");
        File.Delete(md);
        using var writer = File.CreateText(md);
        var count = 0;

        count = WriteExtensions(writer, count);

        count += CountAttributes(types);
        // Index and Range
        count++;
        //Nullability*
        count += 3;

        WriteHelper(types, "Guard", writer, ref count);
        WriteHelper(types, "Lock", writer, ref count);
        WriteHelper(types, nameof(KeyValuePair), writer, ref count);
        WriteType(nameof(TaskCompletionSource), writer, ref count);
        WriteType(nameof(UnreachableException), writer, ref count);

        var countMd = Path.Combine(solutionDirectory, "..", "apiCount.include.md");
        File.Delete(countMd);
        File.WriteAllText(countMd, $"**API count: {count}**");
    }

    static int WriteExtensions(StreamWriter writer, int count)
    {
        writer.WriteLine("### Extension methods");
        writer.WriteLine();

        foreach (var file in Directory.EnumerateFiles(polyfillDir, "Polyfill_*.cs"))
        {
            var typeName = Path.GetFileNameWithoutExtension(file).Split('_')[1];
            writer.WriteLine($"#### {typeName}");
            writer.WriteLine();
            var code = File.ReadAllText(file);

            var types = GetTypesForCode(code).ToList();
            var methods = types.SelectMany(_=>_.PublicMethods()).DistinctBy(_=>_.Key()).ToList();
            var properties = types.SelectMany(_=>_.PublicProperties()).DistinctBy(_=>_.Identifier.Text).ToList();
            foreach (var method in methods)
            {
                count++;
                WriteMethod(writer, method);
            }

            foreach (var property in properties)
            {
                    count++;
                    WriteProperty(writer, property);
            }

            writer.WriteLine();
            writer.WriteLine();
        }

        return count;
    }

    static int CountAttributes(List<Type> types) =>
        types
            .Where(_ => _.Id.EndsWith("Attribute"))
            .Distinct()
            .Count();

    class Type(string id, List<MethodDeclarationSyntax> methods, List<PropertyDeclarationSyntax> properties)
    {
        public string Id { get; } = id;
        public List<MethodDeclarationSyntax> Methods { get; } = methods;
        public List<PropertyDeclarationSyntax> Properties { get; } = properties;
    }

    static List<Type> ReadFiles()
    {
        var types = new Dictionary<string, (HashSet<MethodDeclarationSyntax> methods, HashSet<PropertyDeclarationSyntax> properties)>();

        var methodComparer = EqualityComparer<MethodDeclarationSyntax>
            .Create(
                (x, y) => x!.Key() == y!.Key(),
                _ => _.Key().GetHashCode());

        var propertyComparer = EqualityComparer<PropertyDeclarationSyntax>
            .Create(
                (x, y) => x!.Identifier.ToString() == y!.Identifier.ToString(),
                _ => _.Identifier.ToString().GetHashCode());

        foreach (var type in ReadTypesFromFiles())
        {
            var identifier = type.Identifier.Text;
            if (!types.TryGetValue(identifier, out var members))
            {
                members = new(new(methodComparer), new(propertyComparer));
                types.Add(identifier, members);
            }

            foreach (var method in type.PublicMethods())
            {
                members.methods.Add(method);
            }

            foreach (var property in type.PublicProperties())
            {
                members.properties.Add(property);
            }
        }

        var result = new List<Type>();
        foreach (var pair in types.OrderBy(_ => _.Key))
        {
            var type = new Type(
                pair.Key,
                pair.Value.methods.OrderBy(_ => _.Key()).ToList(),
                pair.Value.properties.OrderBy(_ => _.Identifier.ToString()).ToList());
            result.Add(type);
        }
        return result;
    }

    static IEnumerable<TypeDeclarationSyntax> ReadTypesFromFiles()
    {
        foreach (var file in Directory.EnumerateFiles(polyfillDir, "*.cs", SearchOption.AllDirectories))
        {
            var code = File.ReadAllText(file);

            foreach (var typeDeclarationSyntax in GetTypesForCode(code))
            {
                yield return typeDeclarationSyntax;
            }
        }
    }

    static IEnumerable<TypeDeclarationSyntax> GetTypesForCode(string code)
    {
        foreach (var directive in identifiers)
        {
            var directives = directive.Directives.Concat(sharedIdentifiers).ToHashSet();
            var options = CSharpParseOptions.Default.WithPreprocessorSymbols(directives);
            var tree = CSharpSyntaxTree.ParseText(code, options);

            foreach (var type in tree.GetTypes())
            {
                yield return type;
            }
        }
    }

    static void WriteType(string name, StreamWriter writer, ref int count)
    {
        writer.WriteLine(
            $"""
             #### {name}

             """);
        count++;
    }

    static void WriteHelper(List<Type> types, string name, StreamWriter writer, ref int count)
    {
        var type = types.Single(_=>_.Id == name);
        writer.WriteLine($"#### {type.Id}");
        writer.WriteLine();
        foreach (var method in type.Methods)
        {
            count++;
            WriteMethod(writer, method);
        }

        writer.WriteLine();
        writer.WriteLine();
    }

    static void WriteMethod(StreamWriter writer, MethodDeclarationSyntax method)
    {
        if (method.TryGetReference(out var reference))
        {
            writer.WriteLine($" * `{method.DisplayString()}` [reference]({reference})");
        }
        else
        {
            writer.WriteLine($" * `{method.DisplayString()}`");
        }
    }

    static void WriteProperty(StreamWriter writer, PropertyDeclarationSyntax method)
    {
        if (method.TryGetReference(out var reference))
        {
            writer.WriteLine($" * `{method.Identifier}` [reference]({reference})");
        }
        else
        {
            writer.WriteLine($" * `{method.Identifier}`");
        }
    }
}