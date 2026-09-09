#if FeatureMemory

partial class PolyfillTests
{
    [Test]
    public async Task Span_Min()
    {
        ReadOnlySpan<int> span = [3, 1, 4, 1, 5, 9, 2, 6];

        var result = span.Min();

        await Assert.That(result).IsEqualTo(1);
    }

    [Test]
    public async Task Span_Max()
    {
        ReadOnlySpan<int> span = [3, 1, 4, 1, 5, 9, 2, 6];

        var result = span.Max();

        await Assert.That(result).IsEqualTo(9);
    }

    [Test]
    public async Task Span_MinMax_SingleElement()
    {
        ReadOnlySpan<int> span = [42];

        var min = span.Min();
        var max = span.Max();

        await Assert.That(min).IsEqualTo(42);
        await Assert.That(max).IsEqualTo(42);
    }

    [Test]
    public async Task Span_MinMax_Chars()
    {
        ReadOnlySpan<char> span = "polyfill".AsSpan();

        var min = span.Min();
        var max = span.Max();

        await Assert.That(min).IsEqualTo('f');
        await Assert.That(max).IsEqualTo('y');
    }

    [Test]
    public async Task Span_MinMax_NullComparerUsesDefault()
    {
        ReadOnlySpan<int> span = [3, 1, 2];

        var min = span.Min(null);
        var max = span.Max(null);

        await Assert.That(min).IsEqualTo(1);
        await Assert.That(max).IsEqualTo(3);
    }

    [Test]
    public async Task Span_MinMax_WithComparer()
    {
        ReadOnlySpan<int> span = [3, 1, 4, 1, 5];
        var reverse = Comparer<int>.Create((x, y) => y.CompareTo(x));

        var min = span.Min(reverse);
        var max = span.Max(reverse);

        await Assert.That(min).IsEqualTo(5);
        await Assert.That(max).IsEqualTo(1);
    }

    [Test]
    public async Task Span_MinMax_StringComparer()
    {
        ReadOnlySpan<string> span = ["b", "A", "c"];

        var min = span.Min(StringComparer.OrdinalIgnoreCase);
        var max = span.Max(StringComparer.OrdinalIgnoreCase);

        await Assert.That(min).IsEqualTo("A");
        await Assert.That(max).IsEqualTo("c");
    }

    [Test]
    public async Task Span_MinMax_EmptyValueTypeThrows()
    {
        await Assert.That(
                () =>
                {
                    ReadOnlySpan<int> span = [];
                    _ = span.Min();
                })
            .Throws<InvalidOperationException>();

        await Assert.That(
                () =>
                {
                    ReadOnlySpan<int> span = [];
                    _ = span.Max();
                })
            .Throws<InvalidOperationException>();
    }

    [Test]
    public async Task Span_MinMax_EmptyReferenceTypeIsNull()
    {
        ReadOnlySpan<string> span = [];

        var min = span.Min();
        var max = span.Max();

        await Assert.That(min).IsNull();
        await Assert.That(max).IsNull();
    }

    [Test]
    public async Task Span_MinMax_EmptyNullableValueTypeIsNull()
    {
        ReadOnlySpan<int?> span = [];

        var min = span.Min();
        var max = span.Max();

        await Assert.That(min).IsNull();
        await Assert.That(max).IsNull();
    }

    [Test]
    public async Task Span_MinMax_SkipsNullReferences()
    {
        ReadOnlySpan<string?> span = [null, "b", null, "a", null];

        var min = span.Min();
        var max = span.Max();

        await Assert.That(min).IsEqualTo("a");
        await Assert.That(max).IsEqualTo("b");
    }

    [Test]
    public async Task Span_MinMax_SkipsNullNullables()
    {
        ReadOnlySpan<int?> span = [null, 3, null, 1, null];

        var min = span.Min();
        var max = span.Max();

        await Assert.That(min).IsEqualTo(1);
        await Assert.That(max).IsEqualTo(3);
    }

    [Test]
    public async Task Span_MinMax_AllNullsIsNull()
    {
        ReadOnlySpan<string?> span = [null, null];

        var min = span.Min();
        var max = span.Max();

        await Assert.That(min).IsNull();
        await Assert.That(max).IsNull();
    }

    [Test]
    public async Task Span_MinMax_Nan()
    {
        ReadOnlySpan<double> span = [double.NaN, 1, 2];

        var min = span.Min();
        var max = span.Max();

        await Assert.That(min).IsEqualTo(double.NaN);
        await Assert.That(max).IsEqualTo(2);
    }

    [Test]
    public async Task Span_MinMax_MatchesLinq()
    {
        int[] values = [3, 1, 4, 1, 5, 9, 2, 6];
        ReadOnlySpan<int> span = values;

        var min = span.Min();
        var max = span.Max();

        await Assert.That(min).IsEqualTo(System.Linq.Enumerable.Min(values));
        await Assert.That(max).IsEqualTo(System.Linq.Enumerable.Max(values));
    }
}

#endif
