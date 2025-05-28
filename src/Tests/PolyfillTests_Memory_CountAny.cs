#if FeatureMemory && NET8_0_OR_GREATER
using System.Buffers;

partial class PolyfillTests
{
    [Test]
    public void ReadOnlySpan_CountAny()
    {
        ReadOnlySpan<char> span = ['a', 'b', 'c'];
        var values = SearchValues.Create(['b', 'c']);
        Assert.AreEqual(2, span.CountAny(values));
        Assert.AreEqual(2, span.CountAny('b', 'c'));
        Assert.AreEqual(2, span.CountAny(['b', 'c'], EqualityComparer<char>.Default));
    }
}
#endif