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
        var ifLine = node.GetLocation().GetMappedLineSpan().StartLinePosition;
        var nextToken = node.GetNextDirective();

        if (nextToken != null)
        {
            var nextTokenParent = nextToken.Parent;
            var nextLine = nextToken.GetLocation().GetMappedLineSpan().StartLinePosition;
            if (nextToken.IsKind(SyntaxKind.EndIfDirectiveTrivia))
            {
                if (ifLine.Line + 1 == nextLine.Line)
                {
                    return null;
                }
            }
        }

        // if (nextToken.IsKind(SyntaxKind.ElseDirectiveTrivia))
        // {
        //     return null;
        // }
        //
        // if (nextToken.IsKind(SyntaxKind.ElifDirectiveTrivia))
        // {
        //
        //     return null;
        // }

        return base.VisitIfDirectiveTrivia(node);
    }


    public override SyntaxNode? VisitEndIfDirectiveTrivia(EndIfDirectiveTriviaSyntax node)
    {
        var previousDirective = node.GetPreviousDirective();

        if (previousDirective != null &&
            previousDirective.IsKind(SyntaxKind.IfDirectiveTrivia))
        {
            var endifLine = node.GetLocation().GetMappedLineSpan().StartLinePosition.Line;
            var ifLine = previousDirective.GetLocation().GetMappedLineSpan().StartLinePosition.Line;

            if (ifLine + 1 == endifLine)
            {
                return null; // Remove the empty #endif directive
            }
        }

        return base.VisitEndIfDirectiveTrivia(node);
    }
    //
    // public override SyntaxNode? VisitElifDirectiveTrivia(ElifDirectiveTriviaSyntax node) =>
    //     null;
    //
    // public override SyntaxNode? VisitElseDirectiveTrivia(ElseDirectiveTriviaSyntax node) =>
    //     null;
    //
    // public override SyntaxNode? VisitEndIfDirectiveTrivia(EndIfDirectiveTriviaSyntax node) =>
    //     null;
}
#endif