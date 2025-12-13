#if FeatureMemory && NET8_0_OR_GREATER
partial class PolyfillTests
{
    [Test]
    public async Task ReadOnlySpan_IndexOfAny_FindsFirstMatchingValue()
    {
        ReadOnlySpan<int> span = [1, 2, 3, 4, 5];
        ReadOnlySpan<int> values = [3, 6];
        var result = span.IndexOfAny(values, null);
        await Assert.That(result).IsEqualTo(2);
    }

    [Test]
    public async Task ReadOnlySpan_IndexOfAny_NoMatchingValue_ReturnsMinusOne()
    {
        ReadOnlySpan<int> span = [1, 2, 3, 4, 5];
        ReadOnlySpan<int> values = [6, 7];
        var result = span.IndexOfAny(values, null);
        await Assert.That(result).IsEqualTo(-1);
    }

    [Test]
    public async Task ReadOnlySpan_IndexOfAny_EmptySpan_ReturnsMinusOne()
    {
        var span = ReadOnlySpan<int>.Empty;
        ReadOnlySpan<int> values = [1, 2];
        var result = span.IndexOfAny(values, null);
        await Assert.That(result).IsEqualTo(-1);
    }

    [Test]
    public async Task ReadOnlySpan_IndexOfAny_EmptyValues_ReturnsMinusOne()
    {
        ReadOnlySpan<int> span = [1, 2, 3, 4, 5];
        var values = ReadOnlySpan<int>.Empty;
        var result = span.IndexOfAny(values, null);
        await Assert.That(result).IsEqualTo(-1);
    }

    [Test]
    public async Task ReadOnlySpan_IndexOfAny_WithCustomComparer_FindsMatchingValue()
    {
        ReadOnlySpan<string> span = new[] { "a", "b", "c" };
        ReadOnlySpan<string> values = new[] { "B", "d" };
        var result = span.IndexOfAny(values, StringComparer.OrdinalIgnoreCase);
        await Assert.That(result).IsEqualTo(1);
    }

    [Test]
    public async Task ReadOnlySpan_IndexOfAny_WithCustomComparer_NoMatchingValue_ReturnsMinusOne()
    {
        ReadOnlySpan<string> span = new[] { "a", "b", "c" };
        ReadOnlySpan<string> values = new[] { "d", "e" };
        var result = span.IndexOfAny(values, StringComparer.OrdinalIgnoreCase);
        await Assert.That(result).IsEqualTo(-1);
    }
}
#endif