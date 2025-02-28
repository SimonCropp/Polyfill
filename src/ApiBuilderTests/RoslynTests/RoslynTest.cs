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

        foreach (var file in GetFiles())
        {
            var source = File.ReadAllText(file);

            var fileName = Path.GetFileName(file);

            foreach (var identifier in Identifier.Items)
            {
                ProcessIdentifier(identifier, fileName, source);
            }
        }
    }

    static void ProcessIdentifier(Identifier identifier, string fileName, string source)
    {
        if (!fileName.Contains("Polyfill_IEnumerable"))
        {
            return;
        }

        var resultPath = Path.Combine(slicedPath, identifier.Moniker, fileName);
        var directives = identifier.Directives
            .Concat(Identifier.SharedIdentifiers);
        var options = CSharpParseOptions.Default.WithPreprocessorSymbols(directives);
        var newTree = CSharpSyntaxTree.ParseText(source, options);
        var stripped = CommentStripper.Strip(newTree.ToString());
        stripped = EmptyDirectiveStripper.Strip(stripped.ToString());
        Directory.CreateDirectory(Path.GetDirectoryName(resultPath)!);
        File.WriteAllText(resultPath, stripped.ToString());
    }

    static IEnumerable<string> GetFiles()
    {
        foreach (var file in Directory.EnumerateFiles(polyfillPath, "*.cs", SearchOption.AllDirectories))
        {
            var directory = Path.GetDirectoryName(file)!;
            if (directory.Contains("obj") ||
                directory.Contains("bin"))
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
}