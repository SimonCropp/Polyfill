[TestFixture]
public class GuardTests
{
    [Test]
    public void ModuleInitTest()
    {
        Assert.True(InitCalled);
    }

}

