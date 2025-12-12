[TestFixture]
public class DiagnosticTest
{
    [Test]
    public void CheckParsingOfConstantExpectedAttribute()
    {
        var sourceFile = @"D:\Code\Polyfill\src\Polyfill\ConstantExpectedAttribute.cs";
        var code = File.ReadAllText(sourceFile);

        // Parse with net472 symbols + shared symbols
        var net472Symbols = new List<string>
        {
            "NET472",
            "NET472_OR_GREATER",
            "NET471_OR_GREATER",
            "NET47_OR_GREATER",
            "NET47X",
            "NET462_OR_GREATER",
            "NET461_OR_GREATER",
            "NETFRAMEWORK",
            // Shared symbols
            "FeatureMemory",
            "PolyUseEmbeddedAttribute",
            "PolyPublic"
        };

        var parseOptions = CSharpParseOptions.Default.WithPreprocessorSymbols(net472Symbols);
        var syntaxTree = CSharpSyntaxTree.ParseText(code, parseOptions);
        var root = syntaxTree.GetRoot();

        // Look for the EmbeddedAttribute
        var allText = root.ToFullString();
        Console.WriteLine("=== FULL OUTPUT ===");
        Console.WriteLine(allText);

        var hasEmbeddedAttribute = allText.Contains("EmbeddedAttribute");
        Console.WriteLine($"\n=== Contains EmbeddedAttribute: {hasEmbeddedAttribute} ===");

        // Find all tokens and their trivia
        Console.WriteLine("\n=== CHECKING TOKENS AROUND LINE 17 ===");
        var tokens = root.DescendantTokens(descendIntoTrivia: true).ToList();
        var attributeToken = tokens.FirstOrDefault(t => t.ToString().Contains("AttributeUsage"));
        if (attributeToken != default)
        {
            Console.WriteLine($"Found AttributeUsage token");
            Console.WriteLine($"Trailing trivia count: {attributeToken.TrailingTrivia.Count}");
            foreach (var trivia in attributeToken.TrailingTrivia)
            {
                Console.WriteLine($"  Trivia: {trivia.Kind()} - HasStructure: {trivia.HasStructure}");
                if (trivia.HasStructure)
                {
                    var structure = trivia.GetStructure();
                    Console.WriteLine($"    Structure: {structure?.GetType().Name}");
                    Console.WriteLine($"    Content: {trivia.ToFullString().Substring(0, Math.Min(100, trivia.ToFullString().Length))}");
                }
            }
        }
    }
}
