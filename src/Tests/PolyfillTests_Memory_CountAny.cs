#if FeatureMemory && NET8_0_OR_GREATER
using System.Buffers;

partial class PolyfillTests
{
    [Test]
    public Task ReadOnlySpan_CountAny()
    {
        ReadOnlySpan<char> span = ['a', 'b', 'c'];
        var values = SearchValues.Create(['b', 'c']);
        if (span.CountAny(values) != 2)
        {
            throw new($"Expected CountAny(values) to be 2 but got {span.CountAny(values)}");
        }

        if (span.CountAny('b', 'c') != 2)
        {
            throw new($"Expected CountAny('b', 'c') to be 2 but got {span.CountAny('b', 'c')}");
        }

        if (span.CountAny(['b', 'c'], EqualityComparer<char>.Default) != 2)
        {
            throw new("Expected CountAny with comparer to be 2");
        }

        return Task.CompletedTask;
    }
}
#endif
