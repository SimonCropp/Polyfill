partial class PolyfillTests
{
    [Test]
    public async Task TryGetValue()
    {
        var value = "value";
        var set = new HashSet<string>
        {
            value
        };
        var found = set.TryGetValue("value", out var result);
        await Assert.That(found).IsTrue();
        await Assert.That(result!).IsEqualTo(value);
        found = set.TryGetValue("value2", out result);
        await Assert.That(result).IsNull();
        await Assert.That(found).IsFalse();
    }

    [Test]
    public async Task TryGetValue_NullWithStringComparer()
    {
        var set = new HashSet<string?>(StringComparer.OrdinalIgnoreCase)
        {
            "Value",
            null
        };

        var found = set.TryGetValue("VALUE", out var result);
        await Assert.That(found).IsTrue();
        await Assert.That(result).IsEqualTo("Value");

        found = set.TryGetValue(null, out result);
        await Assert.That(found).IsTrue();
        await Assert.That(result).IsNull();

        found = set.TryGetValue("missing", out result);
        await Assert.That(found).IsFalse();
        await Assert.That(result).IsNull();

        var withoutNull = new HashSet<string?>(StringComparer.OrdinalIgnoreCase)
        {
            "Value"
        };
        found = withoutNull.TryGetValue(null, out result);
        await Assert.That(found).IsFalse();
        await Assert.That(result).IsNull();
    }

    [Test]
    public async Task TryGetValue_ReturnsStoredInstance()
    {
        var stored = new string('a', 3);
        var equal = new string('a', 3);
        var set = new HashSet<string> {stored};

        var found = set.TryGetValue(equal, out var result);
        await Assert.That(found).IsTrue();
        await Assert.That(ReferenceEquals(result, stored)).IsTrue();
        await Assert.That(ReferenceEquals(result, equal)).IsFalse();
    }

    [Test]
    public async Task TryGetValue_LargeSet_HitsAndMisses()
    {
        var set = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        for (var index = 0; index < 2000; index++)
        {
            set.Add($"Item{index}");
        }

        for (var index = 0; index < 2000; index += 37)
        {
            var found = set.TryGetValue($"ITEM{index}", out var result);
            await Assert.That(found).IsTrue();
            await Assert.That(result).IsEqualTo($"Item{index}");
        }

        var missing = set.TryGetValue("ITEM2000", out var missingResult);
        await Assert.That(missing).IsFalse();
        await Assert.That(missingResult).IsNull();
    }

    [Test]
    public async Task TryGetValue_ValueTypes()
    {
        var ints = new HashSet<int> {0, 1, -5, int.MaxValue};
        await Assert.That(ints.TryGetValue(-5, out var intResult)).IsTrue();
        await Assert.That(intResult).IsEqualTo(-5);
        await Assert.That(ints.TryGetValue(0, out intResult)).IsTrue();
        await Assert.That(intResult).IsEqualTo(0);
        await Assert.That(ints.TryGetValue(7, out intResult)).IsFalse();
        await Assert.That(intResult).IsEqualTo(0);

        var nullables = new HashSet<int?> {1, null};
        await Assert.That(nullables.TryGetValue(null, out var nullableResult)).IsTrue();
        await Assert.That(nullableResult).IsNull();
        await Assert.That(nullables.TryGetValue(1, out nullableResult)).IsTrue();
        await Assert.That(nullableResult).IsEqualTo(1);
        await Assert.That(nullables.TryGetValue(2, out nullableResult)).IsFalse();
        await Assert.That(nullableResult).IsNull();

        var pairs = new HashSet<KeyValuePair<int, string>> {new(1, "a")};
        await Assert.That(pairs.TryGetValue(new(1, "a"), out var pairResult)).IsTrue();
        await Assert.That(pairResult.Value).IsEqualTo("a");
        await Assert.That(pairs.TryGetValue(new(1, "b"), out pairResult)).IsFalse();
        await Assert.That(pairResult.Value).IsNull();
    }

    [Test]
    public async Task TryGetValue_AfterRemoveAndClear()
    {
        var set = new HashSet<string> {"a", "b", "c"};

        set.Remove("b");
        await Assert.That(set.TryGetValue("b", out var result)).IsFalse();
        await Assert.That(result).IsNull();
        await Assert.That(set.TryGetValue("c", out result)).IsTrue();
        await Assert.That(result).IsEqualTo("c");

        set.Add("b");
        await Assert.That(set.TryGetValue("b", out result)).IsTrue();
        await Assert.That(result).IsEqualTo("b");

        set.Clear();
        await Assert.That(set.TryGetValue("a", out result)).IsFalse();
        await Assert.That(result).IsNull();

        var empty = new HashSet<string>();
        await Assert.That(empty.TryGetValue("a", out result)).IsFalse();
        await Assert.That(result).IsNull();
    }

    [Test]
    public async Task TryGetValue_DoesNotInvalidateEnumerators()
    {
        var set = new HashSet<int> {1, 2, 3};
        var seen = 0;
        foreach (var item in set)
        {
            await Assert.That(set.TryGetValue(item, out var result)).IsTrue();
            await Assert.That(result).IsEqualTo(item);
            seen++;
        }

        await Assert.That(seen).IsEqualTo(3);
    }

    [Test]
    public async Task HashSet_EnsureCapacity()
    {
        var set = new HashSet<int>();
        set.EnsureCapacity(100);
        // Should not throw - capacity is a hint
        await Assert.That(set.Count).IsEqualTo(0);
    }

    [Test]
    public async Task HashSet_TrimExcess()
    {
        var set = new HashSet<int> { 1, 2, 3 };
        set.TrimExcess(100);
        // Should not throw
        await Assert.That(set.Count).IsEqualTo(3);
    }

    sealed class TagByNameComparer :
        IEqualityComparer<Tag>,
        IAlternateEqualityComparer<string, Tag>
    {
        public static readonly TagByNameComparer Instance = new();

        public bool Equals(Tag? left, Tag? right) =>
            string.Equals(left?.Name, right?.Name, StringComparison.Ordinal);

        public int GetHashCode(Tag tag) => tag.Name.GetHashCode();

        public bool Equals(string alternate, Tag other) =>
            string.Equals(alternate, other.Name, StringComparison.Ordinal);

        public int GetHashCode(string alternate) => alternate.GetHashCode();

        public Tag Create(string alternate) => new(alternate);
    }

    sealed record Tag(string Name);

    [Test]
    public async Task HashSet_GetAlternateLookup()
    {
        var set = new HashSet<Tag>(TagByNameComparer.Instance)
        {
            new("red"),
            new("green"),
        };

#if NET9_0_OR_GREATER
        var lookup = set.GetAlternateLookup<string>();
#else
        var lookup = set.GetAlternateLookup<Tag, string>();
#endif

        await Assert.That(lookup.Contains("red")).IsTrue();
        await Assert.That(lookup.Contains("blue")).IsFalse();

        await Assert.That(lookup.TryGetValue("green", out var actual)).IsTrue();
        await Assert.That(actual!.Name).IsEqualTo("green");

        await Assert.That(lookup.Add("blue")).IsTrue();
        await Assert.That(lookup.Add("blue")).IsFalse();
        await Assert.That(lookup.Contains("blue")).IsTrue();

        await Assert.That(lookup.Remove("red")).IsTrue();
        await Assert.That(lookup.Contains("red")).IsFalse();
        await Assert.That(lookup.Remove("ghost")).IsFalse();
    }

    [Test]
    public async Task HashSet_TryGetAlternateLookup_FailsForIncompatibleComparer()
    {
        var set = new HashSet<string>();

#if NET9_0_OR_GREATER
        var found = set.TryGetAlternateLookup<int>(out _);
#else
        var found = set.TryGetAlternateLookup<string, int>(out _);
#endif

        await Assert.That(found).IsFalse();
    }

    [Test]
    public async Task HashSet_GetAlternateLookup_ThrowsForIncompatibleComparer()
    {
        var set = new HashSet<string>();

        await Assert.That(() =>
        {
#if NET9_0_OR_GREATER
            _ = set.GetAlternateLookup<int>();
#else
            _ = set.GetAlternateLookup<string, int>();
#endif
        }).Throws<InvalidOperationException>();
    }
}