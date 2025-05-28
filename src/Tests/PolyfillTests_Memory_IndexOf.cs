#if FeatureMemory && NET8_0_OR_GREATER
partial class PolyfillTests
{
    [Test]
    public void ReadOnlySpan_IndexOf_FindsValue()
    {
        ReadOnlySpan<int> span = [1, 2, 3, 4, 5];
        var result = span.IndexOf(3, null);
        Assert.AreEqual(2, result);
    }

    [Test]
    public void ReadOnlySpan_IndexOf_ValueNotFound_ReturnsMinusOne()
    {
        ReadOnlySpan<int> span = [1, 2, 3, 4, 5];
        var result = span.IndexOf(6, null);
        Assert.AreEqual(-1, result);
    }

    [Test]
    public void ReadOnlySpan_IndexOf_EmptySpan_ReturnsMinusOne()
    {
        var span = ReadOnlySpan<int>.Empty;
        var result = span.IndexOf(1, null);
        Assert.AreEqual(-1, result);
    }

    [Test]
    public void ReadOnlySpan_IndexOf_WithCustomComparer_FindsValue()
    {
        ReadOnlySpan<string> span = new[] {"a", "b", "c"};
        var result = span.IndexOf("B", StringComparer.OrdinalIgnoreCase);
        Assert.AreEqual(1, result);
    }

    [Test]
    public void ReadOnlySpan_IndexOf_WithCustomComparer_ValueNotFound_ReturnsMinusOne()
    {
        ReadOnlySpan<string> span = new[] {"a", "b", "c"};
        var result = span.IndexOf("d", StringComparer.OrdinalIgnoreCase);
        Assert.AreEqual(-1, result);
    }
}
#endif