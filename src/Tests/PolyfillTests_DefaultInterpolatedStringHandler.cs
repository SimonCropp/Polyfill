#if FeatureMemory && NET6_0_OR_GREATER
using System.Runtime.CompilerServices;

partial class PolyfillTests
{
    [Test]
    public Task DefaultInterpolatedStringHandler_Clear()
    {
        var handler = new DefaultInterpolatedStringHandler(0, 1);
        handler.AppendLiteral("hello");
        // Clear should not throw
        handler.Clear();
        return Task.CompletedTask;
    }
}
#endif
