public static class LinkReader
{
    public static bool TryGetReference(this Member member, [NotNullWhen(true)] out string? reference)
    {
        var syntaxTrivia = member.GetLeadingTrivia();
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
            if (reference.Contains("learn.") && !reference.Contains("?view=net-11.0"))
            {
                throw new($"Missing view: {reference}");
            }
            return true;
        }

        reference = null;
        return false;
    }
}
