#pragma warning disable CS9113 // Parameter is unread.
public static class ConditionalCompilationFilter
{
    public static string FilterCodeByTargetFramework(string sourceCode, string targetFramework)
    {
        var options = CSharpParseOptions.Default.WithPreprocessorSymbols(targetFramework);
        var syntaxTree = CSharpSyntaxTree.ParseText(sourceCode, options);
        var root = syntaxTree.GetRoot();

        var rewriter = new ConditionalCompilationRewriter(targetFramework);
        var newRoot = rewriter.Visit(root);

        var filterCodeByTargetFramework = newRoot.ToFullString();
        return filterCodeByTargetFramework;
    }

    class ConditionalCompilationRewriter(string targetFramework) :
        CSharpSyntaxRewriter(true)
    {
        public override SyntaxTrivia VisitTrivia(SyntaxTrivia trivia)
        {
            if (trivia.IsKind(SyntaxKind.DisabledTextTrivia))
            {
                return SyntaxFactory.Whitespace("");
            }

            return base.VisitTrivia(trivia);
        }
        //
        // public override SyntaxNode? VisitIfDirectiveTrivia(IfDirectiveTriviaSyntax node)
        // {
        //     var expressionSyntax = node.Condition;
        //     if (expressionSyntax.ToString()
        //         .Contains(targetFramework, StringComparison.OrdinalIgnoreCase))
        //     {
        //         // Keep the code inside the matching #if block
        //         return base.Visit(GetNextSibling(node));
        //     }
        //
        //     // Remove the #if block and its content
        //     return null;
        // }
        //
        // public override SyntaxNode? VisitElseDirectiveTrivia(ElseDirectiveTriviaSyntax node) =>
        //     base.Visit(GetNextSibling(node));
        //
        // public override SyntaxNode? VisitEndIfDirectiveTrivia(EndIfDirectiveTriviaSyntax node) =>
        //     // Remove the #endif directive
        //     null;
    }
}