partial class PolyfillTests
{
    static ReverseComparer reverseComparer = new();

    static IEnumerable<int> comparableValues = [1, 2];
    static IEnumerable<int?> comparableNullables = [null, 1, 2];
    static IEnumerable<string?> comparableReferences = [null, "ab", "baa"];

    static IEnumerable<int> emptyValues = [];
    static IEnumerable<int?> emptyNullables = [];
    static IEnumerable<string?> emptyReferences = [];

    static void AssertThrowsNoElementsException(TestDelegate code) =>
        Assert.AreEqual("Sequence contains no elements",
            Assert.Throws<InvalidOperationException>(code)!.Message);

    // Avoid CA1806: Do not ignore method results
    // LINQ methods are known to not have side effects, and the result should not be ignored.
    [StackTraceHidden]
    static void AssertThrowsNoElementsException<T>(Func<T> code) =>
        AssertThrowsNoElementsException(() => { code(); });

    [Test]
    public void MaxBy()
    {
        Assert.AreEqual(1, comparableValues.MaxBy(n => -n));
        Assert.AreEqual(1, comparableNullables.MaxBy(n => -n));
        Assert.AreEqual("baa", comparableReferences.MaxBy(s => s?.Length));
        Assert.AreEqual("ab", comparableReferences.MaxBy(ReverseString));

        AssertThrowsNoElementsException(static () => emptyValues.MaxBy(n => -n));
        Assert.AreEqual(null, emptyNullables.MaxBy(n => -n));
        Assert.AreEqual(null, emptyReferences.MaxBy(s => s?.Length));
        Assert.AreEqual(null, emptyReferences.MaxBy(ReverseString));
    }

    [Test]
    public void MaxComparer()
    {
        Assert.AreEqual(1, comparableValues.Max(reverseComparer));
        Assert.AreEqual(1, comparableNullables.Max(reverseComparer));
        Assert.AreEqual("ab", comparableReferences.Max(reverseComparer));

        AssertThrowsNoElementsException(static () => emptyValues.Max(reverseComparer));
        Assert.AreEqual(null, emptyNullables.Max(reverseComparer));
        Assert.AreEqual(null, emptyReferences.Max(reverseComparer));
        Assert.AreEqual(null, emptyReferences.Max(reverseComparer));
    }

    [Test]
    public void MaxByComparer()
    {
        Assert.AreEqual(2, comparableValues.MaxBy(n => -n, reverseComparer));
        Assert.AreEqual(2, comparableNullables.MaxBy(n => -n, reverseComparer));
        Assert.AreEqual("ab", comparableReferences.MaxBy(s => s?.Length, reverseComparer));
        Assert.AreEqual("baa", comparableReferences.MaxBy(ReverseString, reverseComparer));

        AssertThrowsNoElementsException(static () => emptyValues.MaxBy(n => -n, reverseComparer));
        Assert.AreEqual(null, emptyNullables.MaxBy(n => -n, reverseComparer));
        Assert.AreEqual(null, emptyReferences.MaxBy(s => s?.Length, reverseComparer));
        Assert.AreEqual(null, emptyReferences.MaxBy(ReverseString, reverseComparer));
    }

    [Test]
    public void MinBy()
    {
        Assert.AreEqual(2, comparableValues.MinBy(n => -n));
        Assert.AreEqual(2, comparableNullables.MinBy(n => -n));
        Assert.AreEqual("ab", comparableReferences.MinBy(s => s?.Length));
        Assert.AreEqual("baa", comparableReferences.MinBy(ReverseString));

        AssertThrowsNoElementsException(static () => emptyValues.MinBy(n => -n));
        Assert.AreEqual(null, emptyNullables.MinBy(n => -n));
        Assert.AreEqual(null, emptyReferences.MinBy(s => s?.Length));
        Assert.AreEqual(null, emptyReferences.MinBy(ReverseString));
    }

    [Test]
    public void MinComparer()
    {
        Assert.AreEqual(2, comparableValues.Min(reverseComparer));
        Assert.AreEqual(2, comparableNullables.Min(reverseComparer));
        Assert.AreEqual("baa", comparableReferences.Min(reverseComparer));

        AssertThrowsNoElementsException(static () => emptyValues.Min(reverseComparer));
        Assert.AreEqual(null, emptyNullables.Min(reverseComparer));
        Assert.AreEqual(null, emptyReferences.Min(reverseComparer));
        Assert.AreEqual(null, emptyReferences.Min(reverseComparer));
    }

    [Test]
    public void MinByComparer()
    {
        Assert.AreEqual(1, comparableValues.MinBy(n => -n, reverseComparer));
        Assert.AreEqual(1, comparableNullables.MinBy(n => -n, reverseComparer));
        Assert.AreEqual("baa", comparableReferences.MinBy(s => s?.Length, reverseComparer));
        Assert.AreEqual("ab", comparableReferences.MinBy(ReverseString, reverseComparer));

        AssertThrowsNoElementsException(static () => emptyValues.MinBy(n => -n, reverseComparer));
        Assert.AreEqual(null, emptyNullables.MinBy(n => -n, reverseComparer));
        Assert.AreEqual(null, emptyReferences.MinBy(s => s?.Length, reverseComparer));
        Assert.AreEqual(null, emptyReferences.MinBy(ReverseString, reverseComparer));
    }

    class ReverseComparer : IComparer<int>, IComparer<int?>, IComparer<string?>
    {
        public int Compare(int x, int y) => Comparer<int>.Default.Compare(y, x);

        public int Compare(int? x, int? y) => Comparer<int?>.Default.Compare(y, x);

        public int Compare(string? x, string? y) => Comparer<string?>.Default.Compare(y, x);
    }

    static string? ReverseString(string? s) =>
        s is null ? null : string.Concat(s.Reverse());
}
