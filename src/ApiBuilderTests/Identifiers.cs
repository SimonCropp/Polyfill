public class Identifiers
{
    public static IEnumerable<Type> ReadTypesForFile(string file)
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
                .OfType<Type>()
                .Where(_ => !_.IsNested());

            foreach (var type in typeDeclarations)
            {
                yield return type;
            }
        }
    }

    static List<Identifier> identifiers;

    static List<string> sharedIdentifiers =
    [
        "FeatureMemory",
        "FeatureRuntimeInformation",
        "PolyEnsure",
        "PolyArgumentExceptions",
        "PolyPublic",
        "FeatureHttp",
        "PolyNullability",
        "AllowUnsafeBlocks",
        "FeatureValueTask",
        "FeatureValueTuple",
        "FeatureCompression",
        "FeatureAsyncInterfaces"
    ];

    static Identifiers() =>
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
                Moniker = "net11.0",
                Directives =
                [
                    "NET11_0",
                    "NET11_0_OR_GREATER",
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
                Moniker = "net11.0",
                Directives =
                [
                    "NET11_0",
                    "NET11_0_OR_GREATER",
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
