using VerifyTests;

public static class ModuleInitializer
{
    [ModuleInitializer]
    public static void Initialize() =>
        VerifyDiffPlex.Initialize();
}