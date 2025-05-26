public class ConditionalCompilationFilterTests
{
    [Fact]
    public Task FilterCodeByTargetFramework_ShouldKeepMatchingCode()
    {
        var sourceCode =
            """

            #if NET6_0
            Console.WriteLine("NET6_0");
            #endif

            """;
        var targetFramework = "NET6_0";

        var result = ConditionalCompilationFilter.FilterCodeByTargetFramework(sourceCode, targetFramework);

        return Verify(result);
    }

    [Fact]
    public Task FilterCodeByTargetFramework_ShouldRemoveNonMatchingCode()
    {
        var sourceCode =
            """

            #if NET5_0
            Console.WriteLine("NET5_0");
            #endif

            """;
        var targetFramework = "NET6_0";

        var result = ConditionalCompilationFilter.FilterCodeByTargetFramework(sourceCode, targetFramework);

        return Verify(result);
    }

    [Fact]
    public Task FilterCodeByTargetFramework_ShouldHandleElseBlocks()
    {
        var sourceCode =
            """

            #if NET6_0
            Console.WriteLine("NET6_0");
            #else
            Console.WriteLine("Other");
            #endif

            """;
        var targetFramework = "NET6_0";

        var result = ConditionalCompilationFilter.FilterCodeByTargetFramework(sourceCode, targetFramework);

        return Verify(result);
    }

    [Fact]
    public Task FilterCodeByTargetFramework_ShouldHandleNestedDirectives()
    {
        var sourceCode =
            """

            #if NET6_0
            Console.WriteLine("NET6_0");
            #if DEBUG
            Console.WriteLine("Debug");
            #endif
            #endif

            """;
        var targetFramework = "NET6_0";

        var result = ConditionalCompilationFilter.FilterCodeByTargetFramework(sourceCode, targetFramework);

        return Verify(result);
    }

    [Fact]
    public void FilterCodeByTargetFramework_ShouldHandleEmptySourceCode()
    {
        var sourceCode = "";
        var targetFramework = "NET6_0";

        var result = ConditionalCompilationFilter.FilterCodeByTargetFramework(sourceCode, targetFramework);

        Assert.Empty(result);
    }
}

public static class ConditionalCompilationFilter
{
    public static string FilterCodeByTargetFramework(string sourceCode, string targetFramework)
    {
        var syntaxTree = CSharpSyntaxTree.ParseText(sourceCode);
        var root = syntaxTree.GetRoot();

        var rewriter = new ConditionalCompilationRewriter(targetFramework);
        var newRoot = rewriter.Visit(root);

        return newRoot.ToFullString();
    }

    class ConditionalCompilationRewriter(string targetFramework) :
        CSharpSyntaxRewriter
    {
        static SyntaxNode? GetNextSibling(SyntaxNode node)
        {
            var parent = node.Parent;
            if (parent == null)
            {
                return null;
            }

            var siblings = parent.ChildNodes()
                .ToList();
            var index = siblings.IndexOf(node);

            if (index >= 0 && index < siblings.Count - 1)
            {
                return siblings[index + 1];
            }

            return null;
        }

        public override SyntaxNode? VisitIfDirectiveTrivia(IfDirectiveTriviaSyntax node)
        {
            if (node.Condition.ToString()
                .Contains(targetFramework, StringComparison.OrdinalIgnoreCase))
            {
                // Keep the code inside the matching #if block
                return base.Visit(GetNextSibling(node));
            }

            // Remove the #if block and its content
            return null;
        }

        public override SyntaxNode? VisitElseDirectiveTrivia(ElseDirectiveTriviaSyntax node) =>
            base.Visit(GetNextSibling(node));

        public override SyntaxNode? VisitEndIfDirectiveTrivia(EndIfDirectiveTriviaSyntax node) =>
            // Remove the #endif directive
            null;
    }
}