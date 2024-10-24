#if NET9_0 && DEBUG
using Microsoft.CodeAnalysis.CSharp;

[TestFixture]
class BuildApiTest2
{
    static string solutionDirectory = SolutionDirectoryFinder.Find();

    [Test]
    public void RunWithRoslyn()
    {
        var types = ReadFiles();

        var md = Path.Combine(solutionDirectory, "..", "api_list2.include.md");
        File.Delete(md);
        using var writer = File.CreateText(md);
        var count = 0;

        var extensions = types.Single(_ => _.Key == "Polyfill").Value;
        writer.WriteLine("### Extension methods");
        writer.WriteLine();
        foreach (var extension in PublicMethods(extensions)
                     .GroupBy(_ =>
                     {
                         var syntaxNode = _.Parent;
                         var s = _.ToString();
                         return _.ParameterList.Parameters[0].Type!.ToString();
                     })
                     .OrderBy(_ => _.Key))
        {
            writer.WriteLine($"#### {extension.Key}");
            writer.WriteLine();

            foreach (var method in extension)
            {
                count++;
                WriteSignature(method, writer);
            }

            writer.WriteLine();
            writer.WriteLine();
        }

        writer.WriteLine("### Static helpers");
        writer.WriteLine();

        count += types.Count(_ => _.Key.EndsWith("Attribute"));

        WriteHelper(types, nameof(EnumPolyfill), writer, ref count);
        WriteHelper(types, "RegexPolyfill", writer, ref count);
        WriteHelper(types, "StringPolyfill", writer, ref count);
        WriteHelper(types, "BytePolyfill", writer, ref count);
        WriteHelper(types, "GuidPolyfill", writer, ref count);
        WriteHelper(types, "DateTimePolyfill", writer, ref count);
        WriteHelper(types, "DateTimeOffsetPolyfill", writer, ref count);
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

        var countMd = Path.Combine(solutionDirectory, "..", "apiCount.include.md");
        File.Delete(countMd);
        File.WriteAllText(countMd, $"**API count: {count}**");
    }

    static IEnumerable<MethodDeclarationSyntax> PublicMethods(HashSet<MethodDeclarationSyntax> type) =>
        type.Where(_ => _.IsPublic() && !_.IsConstructor())
            .OrderBy(_ => _.Identifier.ToString());

    static Dictionary<string, HashSet<MethodDeclarationSyntax>> ReadFiles()
    {
        var polyfillDir = Path.Combine(solutionDirectory, "Polyfill");
        var types = new Dictionary<string, HashSet<MethodDeclarationSyntax>>();
        var methodComparer = EqualityComparer<MethodDeclarationSyntax>
            .Create(
                (x, y) => BuildKey(x!) == BuildKey(y!),
                _ => BuildKey(_).GetHashCode());
        foreach (var file in Directory.EnumerateFiles(polyfillDir, "*.cs", SearchOption.AllDirectories))
        {
            var code = File.ReadAllText(file);
            foreach (var directive in identifiers)
            {
                var directives = directive.Directives.Concat(sharedIdentifiers).ToHashSet();
                var options = CSharpParseOptions.Default.WithPreprocessorSymbols(directives);
                var tree = CSharpSyntaxTree.ParseText(code, options);
                var typeDeclarations = tree
                    .GetRoot()
                    .DescendantNodes()
                    .OfType<TypeDeclarationSyntax>()
                    .Where(_ => !_.IsNested());

                foreach (var type in typeDeclarations)
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
        }

        return types;
    }

    static void WriteType(string name, StreamWriter writer, ref int count)
    {
        writer.WriteLine($"#### {name}");
        count++;
    }

    static void WriteHelper(Dictionary<string, HashSet<MethodDeclarationSyntax>> types, string name, StreamWriter writer, ref int count)
    {
        var methods = types[name];

        writer.WriteLine($"#### {name}");
        writer.WriteLine();
        foreach (var method in methods.Where(_ => _.IsPublic() && !_.IsConstructor()))
        {
            count++;
            WriteSignature(method, writer);
        }

        writer.WriteLine();
        writer.WriteLine();
    }

    static void WriteSignature(MethodDeclarationSyntax method, StreamWriter writer)
    {
        var parameters = BuildParameters(method);
        var typeArgs = BuildTypeArgs(method);
        var signature = new StringBuilder($"{method.ReturnType.ToString()} {method.Identifier.Text}{typeArgs}({parameters})");

        if (method.ConstraintClauses.Count > 0)
        {
            foreach (var constraint in method.ConstraintClauses)
            {
                signature.Append(" where ");
                signature.Append(constraint.Name);
                signature.Append(" : ");
                signature.Append(string.Join(", ", constraint.Constraints.Select(_ => _.ToString())));
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

    static string BuildKey(MethodDeclarationSyntax method)
    {
        var parameters = BuildParameters(method);
        var typeArgs = BuildTypeArgs(method);
        return $"{method.ReturnType.ToString()} {method.Identifier.Text}{typeArgs}({parameters})";
    }

    static string BuildTypeArgs(MethodDeclarationSyntax method)
    {
        if (method.TypeParameterList == null || method.TypeParameterList.Parameters.Count == 0)
        {
            return string.Empty;
        }

        return $"<{string.Join(", ", method.TypeParameterList.Parameters.Select(p => p.Identifier.Text))}>";
    }

    static string BuildParameters(MethodDeclarationSyntax method)
    {
        if (method.ParameterList.Parameters.Count == 0)
        {
            return "";
        }

        List<ParameterSyntax> parameters;
        if (method.IsExtensionMethod())
        {
            parameters = method.ParameterList.Parameters.Skip(1).ToList();
            if (parameters.Count == 0)
            {
                return "";
            }
        }
        else
        {
            parameters = method.ParameterList.Parameters.ToList();
        }

        var last = parameters.Last();
        if (last.IsCaller())
        {
            parameters.Remove(last);
        }

        return string.Join(", ", parameters.Select(_ => _.Type!.ToString()));
    }

    static bool TryGetReference(MethodDeclarationSyntax method, [NotNullWhen(true)] out string? reference)
    {
        var descriptionAttribute = method.Attributes()
            .SingleOrDefault(_ => _.Name.ToString() == "Link");
        if (descriptionAttribute == null)
        {
            reference = null;
            return false;
        }

        reference = descriptionAttribute.ArgumentList!.Arguments.Single().Value();
        return true;
    }


    static readonly List<Identifier> identifiers;

    static List<string> sharedIdentifiers =
    [
        "FeatureMemory",
        "PolyGuard",
        "PolyPublic",
        "FeatureHttp",
        "PolyNullability",
        "AllowUnsafeBlocks",
        "FeatureValueTask",
        "LangVersion13",
        "FeatureValueTuple"
    ];

    static BuildApiTest2() =>
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

#endif