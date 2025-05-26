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
        CSharpSyntaxRewriter(true)
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