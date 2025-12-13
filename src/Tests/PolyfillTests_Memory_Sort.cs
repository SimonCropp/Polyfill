#if FeatureMemory && !NET5_0_OR_GREATER

partial class PolyfillTests
{
    [Test]
    public static void DefaultSpanSortBehaviour()
    {
        var expected = Enumerable.Range(0, 10).ToArray();
        var reversed = Enumerable.Range(0, 10)
            .Reverse().ToArray();

        Span<int> actual = reversed;

        Span<char> chars = ['d', 'c', 'b', 'a'];

        chars.Sort();

        Assert.That(expected.SequenceEqual(actual));
    }

    [Test]
    public static void Sort_Comparison_HighToLow()
    {
        var expected = Enumerable.Range(0, 10).Reverse().ToArray();
        var notReversed = Enumerable.Range(0, 10).ToArray();

        Span<int> actual = notReversed;

        actual.Sort((x, y) => x.CompareTo(y));

        Assert.That(expected.SequenceEqual(actual));
    }

    [Test]
    public static void Sort_Separate_Spans_Comparison_HighToLow()
    {
        var expectedKeys = Enumerable.Range(0, 3).Reverse().ToArray();
        var expectedItems = new[]{'a', 'b', 'c'}.AsEnumerable().Reverse().ToArray();

        var actualKeys = Enumerable.Range(0, 3).ToArray();
        var actualItems = new[]{'a', 'b', 'c'};

        Span<int> actualKeysSpan = actualKeys;

        actualKeysSpan.Sort(actualItems, (x, y) => x >= y ? 1 : -1);

        Assert.That(expectedKeys.SequenceEqual(actualKeys));
        Assert.That(expectedItems.SequenceEqual(actualItems));
    }
}
#endif