#if NET9_0
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

// Syntax rewriter that nukes all preprocessor directives and text that is
// active i.e under a preprocessor directive that evaluated to true.
class PreprocessorStripper : CSharpSyntaxRewriter
{
    PreprocessorStripper()
    {
    }

    public static SyntaxTree Strip(SyntaxTree syntaxTree)
    {
        var stripper = new PreprocessorStripper();
        var newRoot = stripper.Visit(syntaxTree.GetRoot());
        return syntaxTree.WithRootAndOptions(newRoot, syntaxTree.Options);
    }

    public override bool VisitIntoStructuredTrivia => true;

    public override SyntaxTrivia VisitTrivia(SyntaxTrivia trivia)
    {
        if (trivia.IsKind(SyntaxKind.DisabledTextTrivia))
        {
            return SyntaxFactory.Whitespace("");
        }

        return base.VisitTrivia(trivia);
    }

    public override SyntaxNode? VisitIfDirectiveTrivia(IfDirectiveTriviaSyntax node) =>
        null;

    public override SyntaxNode? VisitElifDirectiveTrivia(ElifDirectiveTriviaSyntax node) =>
        null;

    public override SyntaxNode? VisitElseDirectiveTrivia(ElseDirectiveTriviaSyntax node) =>
        null;

    public override SyntaxNode? VisitEndIfDirectiveTrivia(EndIfDirectiveTriviaSyntax node) =>
        null;
}
#endif