public class Identifier
{
    public required string Moniker { get; init; }
    public required List<string> Directives { get; init; }
    public static List<string> SharedIdentifiers =
    [
        "FeatureMemory",
        "FeatureRuntimeInformation",
        "PolyGuard",
        "PolyPublic",
        "FeatureHttp",
        "PolyNullability",
        "AllowUnsafeBlocks",
        "FeatureValueTask",
        "LangVersion13",
        "FeatureValueTuple"
    ];
}