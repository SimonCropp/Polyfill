using System.Collections.Generic;
using System.Linq;

partial class PolyfillTests
{
    [Test]
    public async Task IEnumerable_ToDictionary_KeyValuePairs()
    {
        var pairs = new[]
        {
            new KeyValuePair<string, int>("a", 1),
            new KeyValuePair<string, int>("b", 2),
        };
        var dict = pairs.ToDictionary();
        await Assert.That(dict["a"]).IsEqualTo(1);
        await Assert.That(dict["b"]).IsEqualTo(2);
    }

    [Test]
    public async Task IEnumerable_ToDictionary_KeyValuePairs_WithComparer()
    {
        var pairs = new[]
        {
            new KeyValuePair<string, int>("A", 1),
        };
        var dict = pairs.ToDictionary(StringComparer.OrdinalIgnoreCase);
        await Assert.That(dict["a"]).IsEqualTo(1);
    }

    [Test]
    public async Task IEnumerable_ToDictionary_FromDictionary()
    {
        var source = new Dictionary<string, int> { ["x"] = 10, ["y"] = 20 };
        var dict = source.ToDictionary();
        await Assert.That(dict["x"]).IsEqualTo(10);
        await Assert.That(dict["y"]).IsEqualTo(20);
    }
    static IEnumerable<KeyValuePair<string, int>> ToDictionaryPairs(int count)
    {
        for (var index = 0; index < count; index++)
        {
            yield return new($"key{index}", index);
        }
    }

    [ExcludeFromCodeCoverage]
    [DebuggerNonUserCode]
    sealed class ReadOnlyPairs(int count) :
        IReadOnlyCollection<KeyValuePair<string, int>>
    {
        public int Count => count;

        public IEnumerator<KeyValuePair<string, int>> GetEnumerator() =>
            ToDictionaryPairs(count).GetEnumerator();

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() =>
            GetEnumerator();
    }

    [Test]
    [Arguments(0)]
    [Arguments(1)]
    [Arguments(2)]
    [Arguments(100)]
    [Arguments(5000)]
    public async Task IEnumerable_ToDictionary_KeyValuePairs_AllSourceKinds(int count)
    {
        var expected = ToDictionaryPairs(count).ToList();

        IEnumerable<KeyValuePair<string, int>>[] sources =
        [
            expected.ToArray(),
            expected,
            new Dictionary<string, int>(expected.ToDictionary(_ => _.Key, _ => _.Value)),
            new Queue<KeyValuePair<string, int>>(expected),
            new ReadOnlyPairs(count),
            ToDictionaryPairs(count),
            expected.Where(_ => true)
        ];

        foreach (var source in sources)
        {
            var dictionary = source.ToDictionary();
            await Assert.That(dictionary.Count).IsEqualTo(count);
            // items are added in source order, so enumerate in source order
            await Assert.That(dictionary.SequenceEqual(expected)).IsTrue();
            await Assert.That(dictionary.Comparer).IsSameReferenceAs(EqualityComparer<string>.Default);

            var withComparer = source.ToDictionary(StringComparer.OrdinalIgnoreCase);
            await Assert.That(withComparer.Count).IsEqualTo(count);
            await Assert.That(withComparer.Comparer).IsSameReferenceAs(StringComparer.OrdinalIgnoreCase);
            if (count > 0)
            {
                await Assert.That(withComparer[$"KEY{count - 1}"]).IsEqualTo(count - 1);
            }

            // the result is a copy that can be added to
            dictionary.Add("another", -1);
            await Assert.That(dictionary.Count).IsEqualTo(count + 1);
        }
    }

    [Test]
    public async Task IEnumerable_ToDictionary_KeyValuePairs_DuplicateKey_Throws()
    {
        KeyValuePair<string, int>[] array = [new("a", 1), new("b", 2), new("a", 3)];
        await Assert.That(() => array.ToDictionary()).Throws<ArgumentException>();
        await Assert.That(() => array.Where(_ => true).ToDictionary()).Throws<ArgumentException>();

        KeyValuePair<string, int>[] differByCase = [new("a", 1), new("A", 2)];
        await Assert.That(differByCase.ToDictionary().Count).IsEqualTo(2);
        await Assert.That(() => differByCase.ToDictionary(StringComparer.OrdinalIgnoreCase)).Throws<ArgumentException>();
    }

    [Test]
    public async Task IEnumerable_ToDictionary_KeyValuePairs_SourceNotModified()
    {
        var source = new Dictionary<string, int> {["x"] = 10};
        var copy = source.ToDictionary();
        copy["x"] = 11;
        copy["y"] = 20;
        await Assert.That(source.Count).IsEqualTo(1);
        await Assert.That(source["x"]).IsEqualTo(10);
    }
}
