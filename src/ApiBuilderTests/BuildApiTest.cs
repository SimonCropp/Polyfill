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

    static IEnumerable<MethodDeclarationSyntax> PublicMethods(HashSet<MethodDeclarationSyntax> type) =>
        type.Where(_ => _.IsPublic() &&
                        !_.IsConstructor())
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
        writer.WriteLine(
            $"""
             #### {name}

             """);
        count++;
    }

    static void WriteHelper(Dictionary<string, HashSet<MethodDeclarationSyntax>> types, string name, StreamWriter writer, ref int count)
    {
        var methods = types[name];

        WriteTypeMethods(name, writer, ref count, methods.OrderBy(Key));
    }

    static void WriteTypeMethods(string name, StreamWriter writer, ref int count, IEnumerable<MethodDeclarationSyntax> items)
    {
        writer.WriteLine($"#### {name}");
        writer.WriteLine();
        foreach (var method in items
                     .DistinctBy(Key))
        {
            count++;
            WriteSignature(method, writer);
        }

        writer.WriteLine();
        writer.WriteLine();
    }

    static void WriteSignature(MethodDeclarationSyntax method, StreamWriter writer)
    {
        var signature = new StringBuilder($"{method.ReturnType} {Key(method)}");

        if (method.ConstraintClauses.Count > 0)
        {
            foreach (var constraint in method.ConstraintClauses)
            {
                signature.Append($" where {constraint.Name} : ");
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

    static string Key(MethodDeclarationSyntax method)
    {
        var parameters = BuildParameters(method);
        var typeArgs = BuildTypeArgs(method);
        return $"{method.Identifier.Text}{typeArgs}({parameters})";
    }

    static string BuildKey(MethodDeclarationSyntax method)
    {
        var parameters = string.Join(',', method.ParameterList.Parameters.Select(_ => _.Type!.ToString()));
        var returnType = method.ReturnType.ToString();
        var identifier = method.Identifier.Text;
        if (method.TypeParameterList is null)
        {
            return $"{returnType}{identifier}({parameters})";
        }

        return $"{returnType}{identifier}<{string.Join(',', method.TypeParameterList.Parameters.Select(_ => _.Identifier.Text))}>({parameters})";
    }

    static string BuildTypeArgs(MethodDeclarationSyntax method)
    {
        var types = method.TypeParameterList;
        if (types == null || types.Parameters.Count == 0)
        {
            return string.Empty;
        }

        return $"<{string.Join(", ", types.Parameters.Select(_ => _.Identifier.Text))}>";
    }

    static string BuildParameters(MethodDeclarationSyntax method)
    {
        var parameters = method.ParameterList.Parameters.ToList();

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

    static bool TryGetReference(MethodDeclarationSyntax method, [NotNullWhen(true)] out string? reference)
    {
        var syntaxTrivia = method.GetLeadingTrivia();
        foreach (var trivia in syntaxTrivia)
        {
            if (!trivia.IsKind(SyntaxKind.SingleLineCommentTrivia))
            {
                continue;
            }

            var comment = trivia.ToString();
            if (!comment.StartsWith("//Link: "))
            {
                continue;
            }

            reference = comment.Replace("//Link: ", string.Empty);
            if (reference.Contains("learn.") && !reference.Contains("?view=net-10.0"))
            {
                throw new($"Missing view: {reference}");
            }
            return true;
        }

        reference = null;
        return false;
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