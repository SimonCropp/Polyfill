#if NETCOREAPP2_1_OR_GREATER

using System.Collections.Immutable;
using System.Linq;

// Runs from netcoreapp2.1, which is where ImmutableDictionary is in the shared framework, so
// net10.0 and later validate the BCL and everything below the polyfill.
public class ImmutableDictionaryPolyfillTests
{
    static KeyValuePair<string, int>[] withDuplicates =
    [
        new("a", 1),
        new("b", 2),
        new("a", 3),
        new("c", 4),
        new("b", 5)
    ];

    [Test]
    public async Task LaterEntriesOverwriteEarlierOnes()
    {
        var result = Create(withDuplicates);

        await Assert.That(result.Count).IsEqualTo(3);
        await Assert.That(result["a"]).IsEqualTo(3);
        await Assert.That(result["b"]).IsEqualTo(5);
        await Assert.That(result["c"]).IsEqualTo(4);
    }

    // the point of the API: CreateRange rejects the same input
    [Test]
    public async Task CreateRangeRejectsWhatThisAccepts()
    {
        await Assert.That(() => ImmutableDictionary.CreateRange(withDuplicates)).Throws<ArgumentException>();

        var result = Create(withDuplicates);
        await Assert.That(result.Count).IsEqualTo(3);
    }

    [Test]
    public async Task EmptyReturnsTheSharedEmptyInstance()
    {
        var result = Create([]);

        await Assert.That(result.Count).IsEqualTo(0);
        await Assert.That(result).IsSameReferenceAs(ImmutableDictionary<string, int>.Empty);
    }

    [Test]
    public async Task ComparerIsHonouredAndPreserved()
    {
        KeyValuePair<string, int>[] items = [new("A", 1), new("a", 2)];

        var ignoringCase = Create(StringComparer.OrdinalIgnoreCase, items);
        await Assert.That(ignoringCase.Count).IsEqualTo(1);
        await Assert.That(ignoringCase["A"]).IsEqualTo(2);
        await Assert.That(ignoringCase.KeyComparer).IsSameReferenceAs(StringComparer.OrdinalIgnoreCase);

        var caseSensitive = Create(StringComparer.Ordinal, items);
        await Assert.That(caseSensitive.Count).IsEqualTo(2);

        // a null comparer means the default, and an empty range still keeps a supplied one
        await Assert.That(Create(null, withDuplicates).KeyComparer).IsSameReferenceAs(EqualityComparer<string>.Default);
        await Assert.That(Create(StringComparer.Ordinal, []).KeyComparer).IsSameReferenceAs(StringComparer.Ordinal);
    }

    [Test]
    public async Task NullKeyIsRejected()
    {
        KeyValuePair<string, int>[] items = [new("a", 1), new(null!, 2)];

        var captured = Capture(() => Create(items));

        // the BCL names the KeyValuePair member rather than the builder's parameter
        await Assert.That(captured).IsEqualTo("ArgumentNullException:Key");
    }

    [Test]
    public async Task MatchesABuilderWithTheSamePolicy()
    {
        var builder = ImmutableDictionary.CreateBuilder<string, int>();
        foreach (var pair in withDuplicates)
        {
            builder[pair.Key] = pair.Value;
        }

        var viaBuilder = builder.ToImmutable();
        var viaCreate = Create(withDuplicates);

        await Assert.That(viaCreate.Count).IsEqualTo(viaBuilder.Count);
        await Assert.That(viaCreate.OrderBy(_ => _.Key).Select(_ => $"{_.Key}={_.Value}").ToList())
            .IsEquivalentTo(viaBuilder.OrderBy(_ => _.Key).Select(_ => $"{_.Key}={_.Value}").ToList());
    }

    // the spans stay inside these helpers, so nothing ref struct shaped lives across an await
    static ImmutableDictionary<string, int> Create(KeyValuePair<string, int>[] items) =>
        ImmutableDictionary.CreateRangeWithOverwrite<string, int>(items);

    static ImmutableDictionary<string, int> Create(IEqualityComparer<string>? comparer, KeyValuePair<string, int>[] items) =>
        ImmutableDictionary.CreateRangeWithOverwrite(comparer, items);

    static string Capture(Func<object> func)
    {
        try
        {
            func();
            return "none";
        }
        catch (ArgumentException exception)
        {
            return $"{exception.GetType().Name}:{exception.ParamName}";
        }
    }
}

#endif
