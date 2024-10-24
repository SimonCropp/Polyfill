#if NET9_0 && DEBUG
public class Identifier
{
    public required string Moniker { get; init; }
    public required List<string> Directives { get; init; }
}
#endif