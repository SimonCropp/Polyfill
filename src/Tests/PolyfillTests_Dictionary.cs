// ReSharper disable CollectionNeverUpdated.Local
partial class PolyfillTests
{
    [Test]
    public async Task IReadOnlyDictionaryAsReadOnly()
    {
        IDictionary<string, string> dictionary = new Dictionary<string, string>
        {
            {
                "key", "value"
            }
        };

        var readOnly = dictionary.AsReadOnly();
        await Assert.That(readOnly["key"]).IsEqualTo("value");
    }

    [Test]
    public async Task Dictionary_TryAdd_ThrowsOnNullKey()
    {
        var dictionary = new Dictionary<string, int>();

        await Assert.That(() => dictionary.TryAdd(null!, 1)).Throws<ArgumentNullException>();
    }

    [Test]
    public async Task IDictionary_TryAdd_ThrowsOnNullKey()
    {
        IDictionary<string, int> dictionary = new Dictionary<string, int>();
        await Assert.That(() => dictionary.TryAdd(null!, 1)).Throws<ArgumentNullException>();
    }

    [Test]
    public async Task IDictionary_TryAdd_ReturnsTrueOnSuccessfulAdd()
    {
        IDictionary<string, string> dictionary = new Dictionary<string, string>();

        var entryAdded = dictionary.TryAdd("key", "value");

        await Assert.That(entryAdded).IsTrue();
        await Assert.That(dictionary["key"]).IsEqualTo("value");
    }

    [Test]
    public async Task Dictionary_TryAdd_ReturnsTrueOnSuccessfulAdd()
    {
        var dictionary = new Dictionary<string, string>();

        var entryAdded = dictionary.TryAdd("key", "value");

        await Assert.That(entryAdded).IsTrue();
        await Assert.That(dictionary["key"]).IsEqualTo("value");
    }

    [Test]
    public async Task Dictionary_TryAdd_ReturnsFalseIfElementAlreadyPresent()
    {
        var dictionary = new Dictionary<string, string>
        {
            {
                "existingKey", "original value"
            }
        };

        var entryAdded = dictionary.TryAdd("existingKey", "new value");

        await Assert.That(entryAdded).IsFalse();
        await Assert.That(dictionary["existingKey"]).IsEqualTo("original value");
    }

    [Test]
    public async Task IDictionary_TryAdd_ReturnsFalseIfElementAlreadyPresent()
    {
        IDictionary<string, string> dictionary = new Dictionary<string, string>
        {
            {
                "existingKey", "original value"
            }
        };

        var entryAdded = dictionary.TryAdd("existingKey", "new value");

        await Assert.That(entryAdded).IsFalse();
        await Assert.That(dictionary["existingKey"]).IsEqualTo("original value");
    }

    [Test]
    public async Task Dictionary_Remove()
    {
        var dictionary = new Dictionary<string, string?>
        {
            {
                "key", "value"
            }
        };

        await Assert.That(dictionary.Remove("key", out var value)).IsTrue();
        await Assert.That(value).IsEqualTo("value");
    }

    [Test]
    public async Task IDictionary_Remove()
    {
        IDictionary<string, string?> dictionary = new Dictionary<string, string?>
        {
            {
                "key", "value"
            }
        };

        await Assert.That(dictionary.Remove("key", out var value)).IsTrue();
        await Assert.That(value).IsEqualTo("value");
    }

    [Test]
    public async Task Dictionary_Remove_DoesntThrowOnMissingKey()
    {
        var dictionary = new Dictionary<string, string?>();

        await Assert.That(dictionary.Remove("non-existent key", out var value)).IsFalse();
        await Assert.That(value).IsNull();
    }

    [Test]
    public async Task IDictionary_Remove_DoesntThrowOnMissingKey()
    {
        IDictionary<string, string?> dictionary = new Dictionary<string, string?>();

        await Assert.That(dictionary.Remove("non-existent key", out var value)).IsFalse();
        await Assert.That(value).IsNull();
    }

    [Test]
    public async Task Dictionary_Remove_ThrowsOnNull()
    {
        var dictionary = new Dictionary<string, string>();
        await Assert.That(() => dictionary.Remove(null!, out _)).Throws<ArgumentNullException>();
    }

    [Test]
    public async Task IDictionary_Remove_ThrowsOnNull()
    {
        IDictionary<string, string> dictionary = new Dictionary<string, string>();
        await Assert.That(() => dictionary.Remove(null!, out _)).Throws<ArgumentNullException>();
    }

    [Test]
    public async Task Dictionary_EnsureCapacity()
    {
        var dictionary = new Dictionary<string, int>();
        dictionary.EnsureCapacity(100);
        // Should not throw - capacity is a hint
        await Assert.That(dictionary.Count).IsEqualTo(0);
    }

    [Test]
    public async Task Dictionary_TrimExcess_WithCapacity()
    {
        var dictionary = new Dictionary<string, int>
        {
            { "a", 1 },
            { "b", 2 }
        };
        dictionary.TrimExcess(100);
        // Should not throw
        await Assert.That(dictionary.Count).IsEqualTo(2);
    }

    [Test]
    public async Task Dictionary_TrimExcess()
    {
        var dictionary = new Dictionary<string, int>
        {
            { "a", 1 },
            { "b", 2 }
        };
        dictionary.TrimExcess();
        // Should not throw
        await Assert.That(dictionary.Count).IsEqualTo(2);
    }

    sealed class PersonByNameComparer :
        IEqualityComparer<Person>,
        IAlternateEqualityComparer<string, Person>
    {
        public static readonly PersonByNameComparer Instance = new();

        public bool Equals(Person? left, Person? right) =>
            string.Equals(left?.Name, right?.Name, StringComparison.Ordinal);

        public int GetHashCode(Person person) => person.Name.GetHashCode();

        public bool Equals(string alternate, Person other) =>
            string.Equals(alternate, other.Name, StringComparison.Ordinal);

        public int GetHashCode(string alternate) => alternate.GetHashCode();

        public Person Create(string alternate) => new(alternate, 0);
    }

    sealed record Person(string Name, int Age);

    [Test]
    public async Task Dictionary_GetAlternateLookup()
    {
        var dictionary = new Dictionary<Person, int>(PersonByNameComparer.Instance)
        {
            [new("alice", 30)] = 1,
            [new("bob", 40)] = 2,
        };

#if NET9_0_OR_GREATER
        var lookup = dictionary.GetAlternateLookup<string>();
#else
        var lookup = dictionary.GetAlternateLookup<Person, int, string>();
#endif

        await Assert.That(lookup.ContainsKey("alice")).IsTrue();
        await Assert.That(lookup.ContainsKey("missing")).IsFalse();
        await Assert.That(lookup["bob"]).IsEqualTo(2);

        await Assert.That(lookup.TryGetValue("alice", out var value)).IsTrue();
        await Assert.That(value).IsEqualTo(1);

        await Assert.That(lookup.TryGetValue("alice", out var actualKey, out value)).IsTrue();
        await Assert.That(actualKey!.Age).IsEqualTo(30);
        await Assert.That(value).IsEqualTo(1);

        await Assert.That(lookup.TryAdd("carol", 3)).IsTrue();
        await Assert.That(lookup.TryAdd("carol", 99)).IsFalse();
        await Assert.That(lookup["carol"]).IsEqualTo(3);

        lookup["carol"] = 33;
        await Assert.That(lookup["carol"]).IsEqualTo(33);

        await Assert.That(lookup.Remove("bob")).IsTrue();
        await Assert.That(lookup.ContainsKey("bob")).IsFalse();
        await Assert.That(lookup.Remove("ghost")).IsFalse();
    }

    [Test]
    public async Task Dictionary_TryGetAlternateLookup_Succeeds()
    {
        var dictionary = new Dictionary<Person, int>(PersonByNameComparer.Instance);

#if NET9_0_OR_GREATER
        var found = dictionary.TryGetAlternateLookup<string>(out _);
#else
        var found = dictionary.TryGetAlternateLookup<Person, int, string>(out _);
#endif

        await Assert.That(found).IsTrue();
    }

    [Test]
    public async Task Dictionary_TryGetAlternateLookup_FailsForIncompatibleComparer()
    {
        var dictionary = new Dictionary<string, int>();

#if NET9_0_OR_GREATER
        var found = dictionary.TryGetAlternateLookup<int>(out _);
#else
        var found = dictionary.TryGetAlternateLookup<string, int, int>(out _);
#endif

        await Assert.That(found).IsFalse();
    }

    [Test]
    public async Task Dictionary_GetAlternateLookup_ThrowsForIncompatibleComparer()
    {
        var dictionary = new Dictionary<string, int>();

        await Assert.That(() =>
        {
#if NET9_0_OR_GREATER
            _ = dictionary.GetAlternateLookup<int>();
#else
            _ = dictionary.GetAlternateLookup<string, int, int>();
#endif
        }).Throws<InvalidOperationException>();
    }
}