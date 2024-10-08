#if NET9_0
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

[TestFixture]
public class RoslynTest
{
    [Test]
    public void Run()
    {
        var solutionDirectory = SolutionDirectoryFinder.Find();
        var polyfillPath = Path.Combine(solutionDirectory, "Polyfill");
        var slicedPath = Path.Combine(solutionDirectory, "Sliced");

        Directory.CreateDirectory(slicedPath);
        PurgeDirectory(slicedPath);

        var identifiers = new List<Identifier>
        {
            new()
            {
                Moniker = "netcoreapp2.0",
                Directives = ["NETCOREAPP2_0", "NETCOREAPP2X", "NETCOREAPP2_0_OR_GREATER"]
            },
            new()
            {
                Moniker = "netcoreapp2.1",
                Directives = ["NETCOREAPP2_1", "NETCOREAPP2X", "NETCOREAPP2_1_OR_GREATER", "NETCOREAPP2_0_OR_GREATER"]
            },
            new()
            {
                Moniker = "netcoreapp3.0",
                Directives = ["NETCOREAPP3_0", "NETCOREAPP3X", "NETCOREAPP3_0_OR_GREATER", "NETCOREAPP2_1_OR_GREATER", "NETCOREAPP2_0_OR_GREATER"]
            },
            new()
            {
                Moniker = "netcoreapp3.1",
                Directives = ["NETCOREAPP3_1", "NETCOREAPP3X", "NETCOREAPP3_1_OR_GREATER", "NETCOREAPP3_0_OR_GREATER", "NETCOREAPP2_1_OR_GREATER", "NETCOREAPP2_0_OR_GREATER"]
            },
            new()
            {
                Moniker = "netstandard2.0",
                Directives = ["NETSTANDARD2_0", "NETSTANDARD", "NETSTANDARD2_0_OR_GREATER"]
            },
            new()
            {
                Moniker = "netstandard2.1",
                Directives = ["NETSTANDARD2_1", "NETSTANDARD", "NETSTANDARD2_1_OR_GREATER", "NETSTANDARD2_0_OR_GREATER"]
            }
        };
        foreach (var file in Directory.EnumerateFiles(polyfillPath, "*.cs", SearchOption.AllDirectories))
        {
            var directoryName = Path.GetDirectoryName(file)!;
            if (directoryName.Contains("obj") ||
                directoryName.Contains("bin"))
            {
                continue;
            }

            var source = File.ReadAllText(file);

            var fileName = Path.GetFileName(file);

            foreach (var identifier in identifiers)
            {
                var resultPath = Path.Combine(slicedPath, identifier.Moniker, fileName);
                var options = CSharpParseOptions.Default.WithPreprocessorSymbols(identifier.Directives);
                var newTree = CSharpSyntaxTree.ParseText(source, options);
                var strippedTree = PreprocessorStripper.Strip(newTree);
                Directory.CreateDirectory(Path.GetDirectoryName(resultPath)!);
                using var writer = new StreamWriter(resultPath);
                strippedTree.GetText().Write(writer);
            }
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

    static IEnumerable<string> GetReferencedPreprocessorSymbols(SyntaxTree tree) =>
        tree.GetRoot()
            .DescendantTrivia()
            .Where(_ => _.IsKind(SyntaxKind.IfDirectiveTrivia) ||
                        _.IsKind(SyntaxKind.ElifDirectiveTrivia))
            .Select(_ => _.GetStructure())
            .Cast<ConditionalDirectiveTriviaSyntax>()
            .SelectMany(_ => _.Condition.DescendantNodesAndSelf().OfType<IdentifierNameSyntax>())
            .Select(_ => _.Identifier.ValueText)
            .Distinct()
            .OrderBy(_ => _);

    public class Identifier
    {
        public required string Moniker { get; init; }
        public required List<string> Directives { get; init; }
    }
}
#endif