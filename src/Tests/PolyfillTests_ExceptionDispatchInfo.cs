using System.Runtime.ExceptionServices;

partial class PolyfillTests
{
    [Test]
    public async Task ExceptionDispatchInfoSetCurrentStackTrace()
    {
        var exception = new Exception("test");
        var result = ExceptionDispatchInfo.SetCurrentStackTrace(exception);
        await Assert.That(result).IsEqualTo(exception);
        await Assert.That(exception.StackTrace).IsNotNull();
    }
}
