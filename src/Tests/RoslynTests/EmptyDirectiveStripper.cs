#if NET9_0
class EmptyDirectiveStripper : CSharpSyntaxRewriter
{
    public static SyntaxTree Strip(SyntaxTree syntaxTree)
    {
        var stripper = new EmptyDirectiveStripper();
        var newRoot = stripper.Visit(syntaxTree.GetRoot());
        return syntaxTree.WithRootAndOptions(newRoot, syntaxTree.Options);
    }

    public override bool VisitIntoStructuredTrivia => true;

    public override SyntaxNode? VisitIfDirectiveTrivia(IfDirectiveTriviaSyntax node)
    {
        var nextToken = node.GetNextDirective();

        if (nextToken != null)
        {
            if (nextToken.IsKind(SyntaxKind.EndIfDirectiveTrivia))
            {
                var ifLine = node.Line();
                var nextLine = nextToken.Line();
                if (ifLine + 1 == nextLine)
                {
                    return null;
                }
            }
        }

        return base.VisitIfDirectiveTrivia(node);
    }

    public override SyntaxNode? VisitEndIfDirectiveTrivia(EndIfDirectiveTriviaSyntax node)
    {
        var previous = node.GetPreviousDirective();

        if (previous != null &&
            previous.IsKind(SyntaxKind.IfDirectiveTrivia))
        {
            var endifLine = node.Line();
            var ifLine = previous.Line();

            if (ifLine + 1 == endifLine)
            {
                return null;
            }
        }

        return base.VisitEndIfDirectiveTrivia(node);
    }

}

#endif