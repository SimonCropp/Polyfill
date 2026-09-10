public static class LinkReader
{
    // A //Link: or //Note: can sit above the declaration, or between its attributes and
    // its modifiers. GetLeadingTrivia only covers the first of those, since an attribute
    // list is part of the declaration, so the modifiers are scanned as well.
    static IEnumerable<SyntaxTrivia> LeadingComments(this Member member) =>
        member
            .GetLeadingTrivia()
            .Concat(member.Modifiers.SelectMany(_ => _.LeadingTrivia));

    public static bool TryGetReference(this Member member, [NotNullWhen(true)] out string? reference)
    {
        foreach (var trivia in member.LeadingComments())
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

    public static IReadOnlyList<string> GetNotes(this Member member)
    {
        List<string>? notes = null;
        foreach (var trivia in member.LeadingComments())
        {
            if (!trivia.IsKind(SyntaxKind.SingleLineCommentTrivia))
            {
                continue;
            }

            var comment = trivia.ToString();
            if (!comment.StartsWith("//Note: "))
            {
                continue;
            }

            notes ??= [];
            notes.Add(comment.Substring("//Note: ".Length));
        }

        return (IReadOnlyList<string>?)notes ?? [];
    }
}
