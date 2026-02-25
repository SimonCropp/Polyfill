#if FeatureMemory

partial class PolyfillTests
{
    [Test]
    public async Task DefaultSpanSortBehaviour()
    {
        Span<char> chars = ['d', 'c', 'b', 'a'];
        Span<char> expected = ['a', 'b', 'c', 'd'];

        chars.Sort();

        await Assert.That(expected.SequenceEqual(chars)).IsTrue();
    }

    [Test]
    public async Task SpanSort_WithComparison()
    {
        Span<int> numbers = [3, 1, 4, 1, 5, 9, 2, 6];
        Span<int> expected = [9, 6, 5, 4, 3, 2, 1, 1];

        numbers.Sort((x, y) => y.CompareTo(x)); // descending

        await Assert.That(expected.SequenceEqual(numbers)).IsTrue();
    }

    [Test]
    public async Task SpanSort_EmptySpan()
    {
        Span<int> empty = [];

        empty.Sort();

        await Assert.That(empty.Length).IsEqualTo(0);
    }

    [Test]
    public async Task SpanSort_SingleElement()
    {
        Span<int> single = [42];
        Span<int> expected = [42];

        single.Sort();

        await Assert.That(expected.SequenceEqual(single)).IsTrue();
    }

    [Test]
    public async Task SpanSort_KeysAndValues_DefaultComparer()
    {
        int[] keys = [3, 1, 2];
        string[] values = ["three", "one", "two"];
        int[] expectedKeys = [1, 2, 3];
        string[] expectedValues = ["one", "two", "three"];

        keys.AsSpan().Sort(values);

        await Assert.That(expectedKeys.SequenceEqual(keys)).IsTrue();
        await Assert.That(expectedValues.SequenceEqual(values)).IsTrue();
    }

    [Test]
    public async Task SpanSort_KeysAndValues_CustomComparer()
    {
        int[] keys = [3, 1, 2];
        string[] values = ["three", "one", "two"];
        int[] expectedKeys = [3, 2, 1];
        string[] expectedValues = ["three", "two", "one"];

        keys.AsSpan().Sort(values, Comparer<int>.Create((x, y) => y.CompareTo(x))); // descending

        await Assert.That(expectedKeys.SequenceEqual(keys)).IsTrue();
        await Assert.That(expectedValues.SequenceEqual(values)).IsTrue();
    }

    [Test]
    public async Task SpanSort_KeysAndValues_WithComparison()
    {
        int[] keys = [3, 1, 2];
        string[] values = ["three", "one", "two"];
        int[] expectedKeys = [3, 2, 1];
        string[] expectedValues = ["three", "two", "one"];

        keys.AsSpan().Sort(values, (x, y) => y.CompareTo(x)); // descending

        await Assert.That(expectedKeys.SequenceEqual(keys)).IsTrue();
        await Assert.That(expectedValues.SequenceEqual(values)).IsTrue();
    }

#if !NET5_0_OR_GREATER
    [Test]
    public async Task SpanSort_NullComparison_ThrowsArgumentNullException()
    {
        int[] numbers = [3, 1, 2];

        await Assert.ThrowsAsync<ArgumentNullException>(async () =>
        {
            // ReSharper disable once RedundantCast
            numbers.AsSpan().Sort((Comparison<int>)null!);
            await Task.CompletedTask;
        });
    }

    [Test]
    public async Task SpanSort_KeysAndValues_MismatchedLength_ThrowsArgumentException()
    {
        int[] keys = [3, 1, 2];
        string[] values = ["one", "two"];

        await Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            keys.AsSpan().Sort(values);
            await Task.CompletedTask;
        });
    }

    [Test]
    public async Task SpanSort_KeysAndValues_NullComparer_ThrowsArgumentNullException()
    {
        int[] keys = [3, 1, 2];
        string[] values = ["three", "one", "two"];

        await Assert.ThrowsAsync<ArgumentNullException>(async () =>
        {
            keys.AsSpan().Sort(values, (IComparer<int>)null!);
            await Task.CompletedTask;
        });
    }

    [Test]
    public async Task SpanSort_KeysAndValues_NullComparison_ThrowsArgumentNullException()
    {
        int[] keys = [3, 1, 2];
        string[] values = ["three", "one", "two"];

        await Assert.ThrowsAsync<ArgumentNullException>(async () =>
        {
            // ReSharper disable once RedundantCast
            keys.AsSpan().Sort(values, (Comparison<int>)null!);
            await Task.CompletedTask;
        });
    }
#endif
}
#endif
