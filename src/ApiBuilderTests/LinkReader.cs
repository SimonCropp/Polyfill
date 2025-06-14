static class LinkReader
{
    public static bool TryGetReference(this MethodDeclarationSyntax method, [NotNullWhen(true)] out string? reference) =>
        Find(out reference, method.GetLeadingTrivia());

    public static bool TryGetReference(this PropertyDeclarationSyntax property, [NotNullWhen(true)] out string? reference) =>
        Find(out reference, property.GetLeadingTrivia());

    static bool Find(out string? reference, SyntaxTriviaList syntaxTrivia)
    {
        foreach (var trivia in syntaxTrivia)
        {
            if (!trivia.IsKind(SyntaxKind.SingleLineCommentTrivia))
            {
                continue;
            }

            var comment = trivia.ToString();
            if (!comment.StartsWith("//Link: "))
            {
                continue;
            }

            reference = comment.Replace("//Link: ", string.Empty);
            if (reference.Contains("learn.") && !reference.Contains("?view=net-10.0"))
            {
                throw new($"Missing view: {reference}");
            }

            return true;
        }

        reference = null;
        return false;
    }
}