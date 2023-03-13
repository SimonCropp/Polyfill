using NUnit.Framework;

[TestFixture]
public class ModuleInitSample
{
    #region ModuleInitializerAttribute

    static bool InitCalled;

    [Test]
    public void ModuleInitTest() =>
        Assert.True(InitCalled);

    [ModuleInitializer]
    public static void ModuleInit() =>
        InitCalled = true;

    #endregion
}

