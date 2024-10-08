using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

#if NET9_0
[TestFixture]
public class RoslynTest
{
    [Test]
    public void Run()
    {
        var solutionDirectory = SolutionDirectoryFinder.Find();
        var polyfillPath = Path.Combine(solutionDirectory, "Polyfill");
        var options = new CSharpParseOptions(LanguageVersion.CSharp12).WithPreprocessorSymbols();
        foreach (var file in Directory.EnumerateFiles(polyfillPath, "*.cs", SearchOption.AllDirectories))
        {
            var tree = CSharpSyntaxTree.ParseText(File.ReadAllText(file), options);
            var root = tree.GetRoot();

            var usings = root.DescendantNodes();
            foreach (var node in usings)
            {
                Debug.WriteLine(node.GetType());
            }
        }
    }
}
#endif