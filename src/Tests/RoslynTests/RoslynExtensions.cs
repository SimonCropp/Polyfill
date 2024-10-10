#if NET9_0
static class RoslynExtensions
{
    public static int Line(this DirectiveTriviaSyntax node) =>
        node.GetLocation().GetMappedLineSpan().StartLinePosition.Line;
}
#endif