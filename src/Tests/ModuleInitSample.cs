public class ModuleInitSample
{
    #region ModuleInitializerAttribute

    static bool InitCalled;

    [Test]
    public async Task ModuleInitTest() =>
        await Assert.That(InitCalled).IsTrue();

    [ModuleInitializer]
    public static void ModuleInit() =>
        InitCalled = true;

    #endregion
}

