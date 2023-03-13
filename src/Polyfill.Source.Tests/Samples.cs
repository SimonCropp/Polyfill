public class Samples
{
    #region ModuleInitializerAttribute

    static bool InitCalled;

    [Fact]
    public void ModuleInitTest() =>
        Assert.True(InitCalled);

    [ModuleInitializer]
    public static void ModuleInit() =>
        InitCalled = true;

    #endregion
}