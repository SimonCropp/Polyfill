partial class PolyfillTests
{
    static ReverseComparer reverseComparer = new();

    static IEnumerable<int> comparableValues = [1, 2];
    static IEnumerable<int?> comparableNullables = [null, 1, 2];
    static IEnumerable<string?> comparableReferences = [null, "ab", "baa"];

    static IEnumerable<int> emptyValues = [];
    static IEnumerable<int?> emptyNullables = [];
    static IEnumerable<string?> emptyReferences = [];

    static async Task AssertThrowsNoElementsException(Func<Task> code)
    {
        var ex = await Assert.That(code).Throws<InvalidOperationException>();
        await Assert.That(ex!.Message).IsEqualTo("Sequence contains no elements");
    }

    // Avoid CA1806: Do not ignore method results
    // LINQ methods are known to not have side effects, and the result should not be ignored.
    [StackTraceHidden]
    static Task AssertThrowsNoElementsException<T>(Func<T> code) =>
        AssertThrowsNoElementsException(async () =>
        {
            code();
            await Task.CompletedTask;
        });

    [Test]
    public async Task MaxBy()
    {
        await Assert.That(comparableValues.MaxBy(_ => -_)).IsEqualTo(1);
        await Assert.That(comparableNullables.MaxBy(_ => -_)).IsEqualTo(1);
        await Assert.That(comparableReferences.MaxBy(_ => _?.Length)).IsEqualTo("baa");
        await Assert.That(comparableReferences.MaxBy(ReverseString)).IsEqualTo("ab");

        await AssertThrowsNoElementsException(static () => emptyValues.MaxBy(_ => -_));
        await Assert.That(emptyNullables.MaxBy(_ => -_)).IsNull();
        await Assert.That(emptyReferences.MaxBy(_ => _?.Length)).IsNull();
        await Assert.That(emptyReferences.MaxBy(ReverseString)).IsNull();
    }

    [Test]
    public async Task MaxComparer()
    {
        await Assert.That(comparableValues.Max(reverseComparer)).IsEqualTo(1);
        await Assert.That(comparableNullables.Max(reverseComparer)).IsEqualTo(1);
        await Assert.That(comparableReferences.Max(reverseComparer)).IsEqualTo("ab");

        await AssertThrowsNoElementsException(static () => emptyValues.Max(reverseComparer));
        await Assert.That(emptyNullables.Max(reverseComparer)).IsNull();
        await Assert.That(emptyReferences.Max(reverseComparer)).IsNull();
        await Assert.That(emptyReferences.Max(reverseComparer)).IsNull();
    }

    [Test]
    public async Task MaxByComparer()
    {
        await Assert.That(comparableValues.MaxBy(_ => -_, reverseComparer)).IsEqualTo(2);
        await Assert.That(comparableNullables.MaxBy(_ => -_, reverseComparer)).IsEqualTo(2);
        await Assert.That(comparableReferences.MaxBy(_ => _?.Length, reverseComparer)).IsEqualTo("ab");
        await Assert.That(comparableReferences.MaxBy(ReverseString, reverseComparer)).IsEqualTo("baa");

        await AssertThrowsNoElementsException(static () => emptyValues.MaxBy(_ => -_, reverseComparer));
        await Assert.That(emptyNullables.MaxBy(_ => -_, reverseComparer)).IsNull();
        await Assert.That(emptyReferences.MaxBy(_ => _?.Length, reverseComparer)).IsNull();
        await Assert.That(emptyReferences.MaxBy(ReverseString, reverseComparer)).IsNull();
    }

    [Test]
    public async Task MinBy()
    {
        await Assert.That(comparableValues.MinBy(_ => -_)).IsEqualTo(2);
        await Assert.That(comparableNullables.MinBy(_ => -_)).IsEqualTo(2);
        await Assert.That(comparableReferences.MinBy(_ => _?.Length)).IsEqualTo("ab");
        await Assert.That(comparableReferences.MinBy(ReverseString)).IsEqualTo("baa");

        await AssertThrowsNoElementsException(static () => emptyValues.MinBy(_ => -_));
        await Assert.That(emptyNullables.MinBy(_ => -_)).IsNull();
        await Assert.That(emptyReferences.MinBy(_ => _?.Length)).IsNull();
        await Assert.That(emptyReferences.MinBy(ReverseString)).IsNull();
    }

    [Test]
    public async Task MinComparer()
    {
        await Assert.That(comparableValues.Min(reverseComparer)).IsEqualTo(2);
        await Assert.That(comparableNullables.Min(reverseComparer)).IsEqualTo(2);
        await Assert.That(comparableReferences.Min(reverseComparer)).IsEqualTo("baa");

        await AssertThrowsNoElementsException(static () => emptyValues.Min(reverseComparer));
        await Assert.That(emptyNullables.Min(reverseComparer)).IsNull();
        await Assert.That(emptyReferences.Min(reverseComparer)).IsNull();
        await Assert.That(emptyReferences.Min(reverseComparer)).IsNull();
    }

    [Test]
    public async Task MinByComparer()
    {
        await Assert.That(comparableValues.MinBy(_ => -_, reverseComparer)).IsEqualTo(1);
        await Assert.That(comparableNullables.MinBy(_ => -_, reverseComparer)).IsEqualTo(1);
        await Assert.That(comparableReferences.MinBy(_ => _?.Length, reverseComparer)).IsEqualTo("baa");
        await Assert.That(comparableReferences.MinBy(ReverseString, reverseComparer)).IsEqualTo("ab");

        await AssertThrowsNoElementsException(static () => emptyValues.MinBy(_ => -_, reverseComparer));
        await Assert.That(emptyNullables.MinBy(_ => -_, reverseComparer)).IsNull();
        await Assert.That(emptyReferences.MinBy(_ => _?.Length, reverseComparer)).IsNull();
        await Assert.That(emptyReferences.MinBy(ReverseString, reverseComparer)).IsNull();
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
