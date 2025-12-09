partial class PolyfillTests
{
    [Test]
    public async Task ValueTask_CompletedTask()
    {
        await ValueTask.CompletedTask;
    }
}