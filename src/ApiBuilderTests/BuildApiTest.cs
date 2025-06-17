[TestFixture]
public partial class BuildApiTest
{
    static string solutionDirectory = SolutionDirectoryFinder.Find();

    [Test]
    public void RunWithRoslyn()
    {
        var types = ReadFiles();

        var md = Path.Combine(solutionDirectory, "..", "api_list.include.md");
        File.Delete(md);
        using var writer = File.CreateText(md);
        var count = 0;

        count = WriteExtensions(types, writer, count);

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

    static int WriteExtensions(List<Type> types, StreamWriter writer, int count)
    {
        var type = types.Single(_ => _.Id == "Polyfill");
        writer.WriteLine("### Extension methods");
        writer.WriteLine();

        var properties = type.Properties;
        var methods = type.Methods;

        var typesExtended = methods.Select(FindTypeMethodExtends)
            .Concat(properties.Select(FindTypePropertyExtends))
            .Distinct()
            .OrderBy(_ => _)
            .ToList();

        foreach (var typeExtended in typesExtended)
        {
            writer.WriteLine($"#### {typeExtended}");
            writer.WriteLine();

            foreach (var method in methods)
            {
                if (FindTypeMethodExtends(method) == typeExtended)
                {
                    count++;
                    WriteMethod(writer, method);
                }
            }

            foreach (var property in properties)
            {
                if (FindTypePropertyExtends(property) == typeExtended)
                {
                    count++;
                    WriteProperty(writer, property);
                }
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

    static string FindTypeMethodExtends(MethodDeclarationSyntax method)
    {
        if (method.ParameterList.Parameters.Count > 0)
        {
            var firstParameter = method.ParameterList.Parameters[0];
            var key = firstParameter.Type!.ToString();
            if (firstParameter.Modifiers.Any(_ => _.IsKind(SyntaxKind.ThisKeyword)))
            {
                //TODO: delete this path when all are C#14 extensions
                return key;
            }

            // method is a MethodDeclarationSyntax
            var returnType = method.ReturnType.ToString();
            //TODO: handle this better
            if (returnType == "StringBuilder")
            {
                return key;
            }
        }
        var methodParent = (ClassDeclarationSyntax) method.Parent!;
        var members = methodParent.Members;
        var indexOf = members.IndexOf(method);

        var extensionIndex = -1;
        for (var i = indexOf; i >= 0; i--)
        {
            var member = members[i];
            var s = member.ToString();
            if (s.StartsWith("extension"))
            {
                extensionIndex = i;
                break;
            }
        }

        var memberDeclarationSyntaxes = members.Skip(extensionIndex).ToList();
        foreach (var member in memberDeclarationSyntaxes)
        {
            if (member is FieldDeclarationSyntax fieldSyntax)
            {
                var variableDeclarationSyntax = fieldSyntax.Declaration;
                var typeSyntax = (TupleTypeSyntax)variableDeclarationSyntax.Type;
                var tupleElementSyntax = typeSyntax.Elements.First();
                var genericNameSyntax = (GenericNameSyntax) tupleElementSyntax.Type;
                return genericNameSyntax.Identifier.ToString();
            }
            if (member is ConstructorDeclarationSyntax constructor)
            {
                var extensionParameter = constructor.ParameterList.Parameters[0];
                return extensionParameter.Type!.ToString();
            }

            if (member is IncompleteMemberSyntax incomplete)
            {
                var syntaxNodes = incomplete.ChildNodes().ToList();
                if (syntaxNodes[0] is TupleTypeSyntax tupleTypeSyntax)
                {
                    var tupleElementSyntax = tupleTypeSyntax.Elements.First();
                    var typeSyntax = (GenericNameSyntax) tupleElementSyntax.Type;
                    return typeSyntax.Identifier.ToString();
                }
            }
        }

        throw new();
    }

    //TODO: respect generics on return type
    static string FindTypePropertyExtends(PropertyDeclarationSyntax property)
    {
        var members =  property.Parent!.ChildNodes().ToList();;
        var indexOf = members.IndexOf(property);
        for (var i = indexOf; i >= 0; i--)
        {
            var member = members[i];
            if (member is ConstructorDeclarationSyntax constructor)
            {
                var extensionParameter = constructor.ParameterList.Parameters[0];
                return extensionParameter.Type!.ToString();
            }

            if (member is IncompleteMemberSyntax incomplete)
            {
                var syntaxNodes = incomplete.ChildNodes().ToList();
                var tupleTypeSyntax = (TupleTypeSyntax) syntaxNodes[0];
                var tupleElementSyntax = tupleTypeSyntax.Elements.First();
                var typeSyntax = (GenericNameSyntax) tupleElementSyntax.Type;
                return typeSyntax.Identifier.ToString();
            }
        }

        throw new();
    }

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
        var polyfillDir = Path.Combine(solutionDirectory, "Polyfill");
        foreach (var file in Directory.EnumerateFiles(polyfillDir, "*.cs", SearchOption.AllDirectories))
        {
            var code = File.ReadAllText(file);

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