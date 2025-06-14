[TestFixture]
public class BuildApiTest
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
        var extensions = types.Single(_ => _.Id == "Polyfill");
        writer.WriteLine("### Extension methods");
        writer.WriteLine();
        foreach (var grouping in extensions
                     .Methods
                     .GroupBy(FindTypeMethodExtends)
                     .OrderBy(_ => _.Key))
        {
            writer.WriteLine($"#### {grouping.Key}");
            writer.WriteLine();
            foreach (var method in grouping)
            {
                count++;
                WriteMethod(writer, method);
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

    private static string FindTypeMethodExtends(MethodDeclarationSyntax method)
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

        var methodParent = (ClassDeclarationSyntax) method.Parent!;
        var members = methodParent.Members;
        var indexOf = members.IndexOf(method);
        for (var i = indexOf; i >= 0; i--)
        {
            var member = members[i];
            if (member is ConstructorDeclarationSyntax constructor)
            {
                var extensionParameter = constructor.ParameterList.Parameters[0];
                key = extensionParameter.Type!.ToString();
                return key;
            }
        }

        throw new();
    }

    class Type(string id, List<MethodDeclarationSyntax> methods)
    {
        public string Id { get; } = id;
        public List<MethodDeclarationSyntax> Methods { get; } = methods;
    }

    static List<Type> ReadFiles()
    {
        var types = new Dictionary<string, HashSet<MethodDeclarationSyntax>>();
        var methodComparer = EqualityComparer<MethodDeclarationSyntax>
            .Create(
                (x, y) => x!.Key() == y!.Key(),
                _ => _.Key().GetHashCode());

        foreach (var type in ReadTypesFromFiles())
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

        return types
            .OrderBy(_ => _.Key)
            .Select(_ =>
                new Type(
                    _.Key,
                    _.Value.OrderBy(_ => _.Key())
                        .ToList()))
            .ToList();
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
        foreach (var method in (IEnumerable<MethodDeclarationSyntax>) type.Methods)
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

    static List<Identifier> identifiers;

    static List<string> sharedIdentifiers =
    [
        "FeatureMemory",
        "FeatureRuntimeInformation",
        "PolyGuard",
        "PolyPublic",
        "FeatureHttp",
        "PolyNullability",
        "AllowUnsafeBlocks",
        "FeatureValueTask",
        "LangVersion13",
        "FeatureValueTuple",
        "FeatureCompression"
    ];

    static BuildApiTest() =>
        identifiers =
        [
            new()
            {
                Moniker = "net5.0",
                Directives =
                [
                    "NET5_0",
                    "NET5_0_OR_GREATER"
                ]
            },

            new()
            {
                Moniker = "net6.0",
                Directives =
                [
                    "NET6_0",
                    "NET6_0_OR_GREATER",
                    "NET5_0_OR_GREATER"
                ]
            },

            new()
            {
                Moniker = "net7.0",
                Directives =
                [
                    "NET7_0",
                    "NET7_0_OR_GREATER",
                    "NET6_0_OR_GREATER",
                    "NET5_0_OR_GREATER"
                ]
            },

            new()
            {
                Moniker = "net8.0",
                Directives =
                [
                    "NET8_0",
                    "NET8_0_OR_GREATER",
                    "NET7_0_OR_GREATER",
                    "NET6_0_OR_GREATER",
                    "NET5_0_OR_GREATER"
                ]
            },

            new()
            {
                Moniker = "net9.0",
                Directives =
                [
                    "NET9_0",
                    "NET9_0_OR_GREATER",
                    "NET8_0_OR_GREATER",
                    "NET7_0_OR_GREATER",
                    "NET6_0_OR_GREATER",
                    "NET5_0_OR_GREATER"
                ]
            },

            new()
            {
                Moniker = "net10.0",
                Directives =
                [
                    "NET10_0",
                    "NET10_0_OR_GREATER",
                    "NET9_0_OR_GREATER",
                    "NET8_0_OR_GREATER",
                    "NET7_0_OR_GREATER",
                    "NET6_0_OR_GREATER",
                    "NET5_0_OR_GREATER"
                ]
            },

            new()
            {
                Moniker = "net461",
                Directives =
                [
                    "NET461",
                    "NET461_OR_GREATER",
                    "NET46X",
                    "NETFRAMEWORK",
                ]
            },

            new()
            {
                Moniker = "NET47",
                Directives =
                [
                    "NET47",
                    "NET47_OR_GREATER",
                    "NET47X",
                    "NET461_OR_GREATER",
                    "NETFRAMEWORK",
                ]
            },

            new()
            {
                Moniker = "NET471",
                Directives =
                [
                    "NET471",
                    "NET471_OR_GREATER",
                    "NET47_OR_GREATER",
                    "NET47X",
                    "NET461_OR_GREATER",
                    "NETFRAMEWORK",
                ]
            },

            new()
            {
                Moniker = "NET472",
                Directives =
                [
                    "NET472",
                    "NET472_OR_GREATER",
                    "NET471_OR_GREATER",
                    "NET47_OR_GREATER",
                    "NET47X",
                    "NET461_OR_GREATER",
                    "NETFRAMEWORK",
                ]
            },

            new()
            {
                Moniker = "NET48",
                Directives =
                [
                    "NET48",
                    "NET48_OR_GREATER",
                    "NET472_OR_GREATER",
                    "NET471_OR_GREATER",
                    "NET47_OR_GREATER",
                    "NET47X",
                    "NET461_OR_GREATER",
                    "NETFRAMEWORK",
                ]
            },

            new()
            {
                Moniker = "NET481",
                Directives =
                [
                    "NET481",
                    "NET48X",
                    "NET481_OR_GREATER",
                    "NET48_OR_GREATER",
                    "NET472_OR_GREATER",
                    "NET471_OR_GREATER",
                    "NET47_OR_GREATER",
                    "NET461_OR_GREATER",
                    "NETFRAMEWORK",
                ]
            },

            new()
            {
                Moniker = "net5.0",
                Directives =
                [
                    "NET5_0",
                    "NET5_0_OR_GREATER",
                ]
            },

            new()
            {
                Moniker = "net6.0",
                Directives =
                [
                    "NET6_0",
                    "NET6_0_OR_GREATER",
                    "NET5_0_OR_GREATER",
                ]
            },

            new()
            {
                Moniker = "net7.0",
                Directives =
                [
                    "NET7_0",
                    "NET7_0_OR_GREATER",
                    "NET6_0_OR_GREATER",
                    "NET5_0_OR_GREATER",
                ]
            },

            new()
            {
                Moniker = "net8.0",
                Directives =
                [
                    "NET8_0",
                    "NET8_0_OR_GREATER",
                    "NET7_0_OR_GREATER",
                    "NET6_0_OR_GREATER",
                    "NET5_0_OR_GREATER",
                ]
            },

            new()
            {
                Moniker = "net9.0",
                Directives =
                [
                    "NET9_0",
                    "NET9_0_OR_GREATER",
                    "NET8_0_OR_GREATER",
                    "NET7_0_OR_GREATER",
                    "NET6_0_OR_GREATER",
                    "NET5_0_OR_GREATER",
                ]
            },

            new()
            {
                Moniker = "net10.0",
                Directives =
                [
                    "NET10_0",
                    "NET10_0_OR_GREATER",
                    "NET9_0_OR_GREATER",
                    "NET8_0_OR_GREATER",
                    "NET7_0_OR_GREATER",
                    "NET6_0_OR_GREATER",
                    "NET5_0_OR_GREATER",
                ]
            },

            new()
            {
                Moniker = "netcoreapp2.0",
                Directives =
                [
                    "NETCOREAPP2_0",
                    "NETCOREAPP2X",
                    "NETCOREAPP",
                    "NETCOREAPP2_0_OR_GREATER"
                ]
            },

            new()
            {
                Moniker = "netcoreapp2.1",
                Directives =
                [
                    "NETCOREAPP2_1",
                    "NETCOREAPP2X",
                    "NETCOREAPP",
                    "NETCOREAPP2_1_OR_GREATER",
                    "NETCOREAPP2_0_OR_GREATER"
                ]
            },

            new()
            {
                Moniker = "netcoreapp3.0",
                Directives =
                [
                    "NETCOREAPP3_0",
                    "NETCOREAPP3X",
                    "NETCOREAPP",
                    "NETCOREAPP3_0_OR_GREATER",
                    "NETCOREAPP2_1_OR_GREATER",
                    "NETCOREAPP2_0_OR_GREATER"
                ]
            },

            new()
            {
                Moniker = "netcoreapp3.1",
                Directives =
                [
                    "NETCOREAPP3_1",
                    "NETCOREAPP3X",
                    "NETCOREAPP",
                    "NETCOREAPP3_1_OR_GREATER",
                    "NETCOREAPP3_0_OR_GREATER",
                    "NETCOREAPP2_1_OR_GREATER",
                    "NETCOREAPP2_0_OR_GREATER"
                ]
            },

            new()
            {
                Moniker = "netstandard2.0",
                Directives =
                [
                    "NETSTANDARD2_0",
                    "NETSTANDARD",
                    "NETSTANDARD2_0_OR_GREATER"
                ]
            },

            new()
            {
                Moniker = "netstandard2.1",
                Directives =
                [
                    "NETSTANDARD2_1",
                    "NETSTANDARD",
                    "NETSTANDARD2_1_OR_GREATER",
                    "NETSTANDARD2_0_OR_GREATER"
                ]
            }
        ];
}