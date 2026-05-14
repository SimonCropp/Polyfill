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