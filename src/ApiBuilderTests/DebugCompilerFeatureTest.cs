[TestFixture]
public class DebugCompilerFeatureTest
{
    [Test]
    public void DebugCompilerFeatureRequiredAttribute()
    {
        var sourceFile = @"D:\Code\Polyfill\src\Polyfill\CompilerFeatureRequiredAttribute.cs";
        var code = File.ReadAllText(sourceFile);

        // Parse with net10.0 symbols (has NET7_0_OR_GREATER, so #else branch should be active)
        var net10Symbols = new List<string>
        {
            "NET",
            "NET10_0",
            "NET10_0_OR_GREATER",
            "NET9_0_OR_GREATER",
            "NET8_0_OR_GREATER",
            "NET7_0_OR_GREATER",
            "NET6_0_OR_GREATER",
            "NET5_0_OR_GREATER",
            // Shared symbols
            "FeatureMemory",
            "PolyUseEmbeddedAttribute",
            "PolyPublic"
        };

        var parseOptions = CSharpParseOptions.Default.WithPreprocessorSymbols(net10Symbols);
        var syntaxTree = CSharpSyntaxTree.ParseText(code, parseOptions);
        var root = syntaxTree.GetRoot();

        Console.WriteLine("=== FULL PARSED OUTPUT ===");
        Console.WriteLine(root.ToFullString());

        Console.WriteLine("\n=== CHECKING FOR POLY DIRECTIVES ===");
        var allTrivia = root.DescendantTrivia(descendIntoTrivia: false);
        foreach (var trivia in allTrivia)
        {
            if (trivia.HasStructure)
            {
                var structure = trivia.GetStructure();
                if (structure is IfDirectiveTriviaSyntax ifDir)
                {
                    Console.WriteLine($"#if {ifDir.Condition} (Active: {ifDir.IsActive})");
                }
                else if (structure is ElseDirectiveTriviaSyntax elseDir)
                {
                    Console.WriteLine($"#else (Active: {elseDir.IsActive})");
                }
                else if (structure is EndIfDirectiveTriviaSyntax endifDir)
                {
                    Console.WriteLine($"#endif (Active: {endifDir.IsActive})");
                }
            }
        }
    }
}
