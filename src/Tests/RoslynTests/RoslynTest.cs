#if NET9_0

[TestFixture]
public class RoslynTest
{
    static string polyfillPath = Path.Combine(SolutionDirectoryFinder.Find(), "Polyfill");
    static string slicedPath = Path.Combine(SolutionDirectoryFinder.Find(), "Sliced");

    [Test]
    public void Run()
    {
        Directory.CreateDirectory(slicedPath);
        PurgeDirectory(slicedPath);

        var sharedIdentifiers = new List<string>
        {
            "FeatureMemory",
            "PolyGuard",
            "PolyPublic",
            "FeatureHttp",
            "PolyNullability",
            "AllowUnsafeBlocks",
            "FeatureValueTask",
            "LangVersion13",
            "FeatureValueTuple"
        };

        var identifiers = new List<Identifier>
        {
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
        };

        foreach (var file in GetFiles())
        {
            var source = File.ReadAllText(file);

            var fileName = Path.GetFileName(file);

            foreach (var identifier in identifiers)
            {
                var resultPath = Path.Combine(slicedPath, identifier.Moniker, fileName);
                var directives = identifier.Directives.Concat(sharedIdentifiers);
                var options = CSharpParseOptions.Default.WithPreprocessorSymbols(directives);
                var newTree = CSharpSyntaxTree.ParseText(source, options);
                var strippedTree = Stripper.Strip(newTree);
                Directory.CreateDirectory(Path.GetDirectoryName(resultPath)!);
                using var writer = new StreamWriter(resultPath);
                strippedTree.GetText().Write(writer);
            }
        }
    }

    static IEnumerable<string> GetFiles()
    {
        foreach (var file in Directory.EnumerateFiles(polyfillPath, "*.cs", SearchOption.AllDirectories))
        {
            var directoryName = Path.GetDirectoryName(file)!;
            if (directoryName.Contains("obj") ||
                directoryName.Contains("bin"))
            {
                continue;
            }

            yield return file;
        }
    }

    static void PurgeDirectory(string directory)
    {
        foreach (var subDirectory in Directory.EnumerateDirectories(directory))
        {
            Directory.Delete(subDirectory, true);
        }

        foreach (var file in Directory.EnumerateFiles(directory))
        {
            File.Delete(file);
        }
    }

    public class Identifier
    {
        public required string Moniker { get; init; }
        public required List<string> Directives { get; init; }
    }
}
#endif