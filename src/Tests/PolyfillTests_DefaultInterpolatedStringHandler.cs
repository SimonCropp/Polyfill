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

    [Test]
    public async Task DefaultInterpolatedStringHandler_Clear_ClearsCallersHandler()
    {
        var handler = new DefaultInterpolatedStringHandler(0, 1);
        handler.AppendLiteral("hello");
        handler.Clear();
        var afterClear = handler.ToStringAndClear();

        handler = new DefaultInterpolatedStringHandler(0, 1);
        handler.AppendLiteral("hello");
        handler.Clear();
        handler.AppendLiteral("world");
        var afterReuse = handler.ToStringAndClear();

        await Assert.That(afterClear).IsEqualTo("");
        await Assert.That(afterReuse).IsEqualTo("world");
    }
}
#endif
