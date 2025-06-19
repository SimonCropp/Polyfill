public static class LinkReader
{
    public static bool TryGetReference(this Method method, [NotNullWhen(true)] out string? reference)
    {
        var syntaxTrivia = method.GetLeadingTrivia();
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