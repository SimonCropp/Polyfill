partial class PolyfillTests
{
    [Test]
    public async Task List_CopyTo_TooShort_Throws_AndLeavesDestinationUntouched()
    {
        var list = new List<int> {10, 20, 30, 40, 50};

        var destination = new int[3];
        await Assert.That(() => list.CopyTo(new Span<int>(destination))).Throws<ArgumentException>();
        await Assert.That(destination.All(_ => _ == 0)).IsTrue();

        var ok = new int[5];
        list.CopyTo(new Span<int>(ok));
        await Assert.That(string.Join(",", ok)).IsEqualTo("10,20,30,40,50");
    }

    [Test]
    public async Task List_InsertRange_InvalidIndex_Throws()
    {
        var list = new List<int> {1, 2, 3};
        await Assert.That(() => list.InsertRange(10, ReadOnlySpan<int>.Empty)).Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task IListAsReadOnly()
    {
        IList<char> list = ['a'];
        var readOnly = list.AsReadOnly();
        await Assert.That(readOnly[0]).IsEqualTo('a');
    }

    [Test]
    public async Task ListAddRangeReadOnlySpan()
    {
        var list = new List<char>();
        list.AddRange("ab".AsSpan());
        await Assert.That(list[0]).IsEqualTo('a');
        await Assert.That(list[1]).IsEqualTo('b');
    }

    [Test]
    public async Task ListInsertRangeReadOnlySpan()
    {
        var list = new List<char>
        {
            'a'
        };
        list.InsertRange(1, "bc".AsSpan());
        await Assert.That(list[1]).IsEqualTo('b');
        await Assert.That(list[2]).IsEqualTo('c');
    }

    [Test]
    public async Task ListCopyToSpan()
    {
        var list = new List<char>
        {
            'a'
        };
        var array = new char[1];
        list.CopyTo(array.AsSpan());
        await Assert.That(array[0]).IsEqualTo('a');
    }

    [Test]
    public async Task List_EnsureCapacity()
    {
        var list = new List<int>();
        list.EnsureCapacity(100);
        // Should not throw - capacity is a hint
        await Assert.That(list.Count).IsEqualTo(0);
    }

    [Test]
    public async Task List_TrimExcess()
    {
        var list = new List<int> { 1, 2, 3 };
        list.TrimExcess();
        // Should not throw
        await Assert.That(list.Count).IsEqualTo(3);
    }
    [Test]
    [Arguments(0, 0)]
    [Arguments(0, 1)]
    [Arguments(0, 63)]
    [Arguments(0, 64)]
    [Arguments(0, 65)]
    [Arguments(3, 0)]
    [Arguments(3, 1)]
    [Arguments(3, 2)]
    [Arguments(3, 64)]
    [Arguments(100, 5)]
    [Arguments(100, 5000)]
    public async Task List_AddRange_Span_MatchesAddRangeOfArray(int initialCount, int addCount)
    {
        var added = Enumerable.Range(1000, addCount).ToArray();
        var expected = Enumerable.Range(0, initialCount).ToList();
        expected.AddRange((IEnumerable<int>) added);

        var list = Enumerable.Range(0, initialCount).ToList();
        list.AddRange(added.AsSpan());
        await Assert.That(list.Count).IsEqualTo(initialCount + addCount);
        await Assert.That(list.SequenceEqual(expected)).IsTrue();

        // same again for a reference type
        var addedStrings = added.Select(_ => _ % 7 == 0 ? null : _.ToString()).ToArray();
        var expectedStrings = Enumerable.Range(0, initialCount).Select(_ => (string?) _.ToString()).ToList();
        expectedStrings.AddRange((IEnumerable<string?>) addedStrings);

        var strings = Enumerable.Range(0, initialCount).Select(_ => (string?) _.ToString()).ToList();
        strings.AddRange(addedStrings.AsSpan());
        await Assert.That(strings.SequenceEqual(expectedStrings)).IsTrue();

        // and for a struct that is not a primitive
        var addedPairs = added.Select(_ => new KeyValuePair<int, string>(_, _.ToString())).ToArray();
        var pairs = new List<KeyValuePair<int, string>>();
        pairs.AddRange(addedPairs.AsSpan());
        await Assert.That(pairs.SequenceEqual(addedPairs)).IsTrue();
    }

    [Test]
    public async Task List_AddRange_Span_Repeated()
    {
        var list = new List<int>();
        var expected = new List<int>();
        for (var round = 0; round < 200; round++)
        {
            var chunk = Enumerable.Range(round * 1000, round % 9 == 0 ? 150 : round % 5).ToArray();
            list.AddRange(chunk.AsSpan());
            expected.AddRange((IEnumerable<int>) chunk);
        }

        await Assert.That(list.SequenceEqual(expected)).IsTrue();
    }

    [Test]
    public async Task List_AddRange_Span_GrowthIsAmortized()
    {
        // appending many small spans must not reallocate for every call
        var list = new List<int>();
        var reallocations = 0;
        var capacity = list.Capacity;
        int[] chunk = [1, 2, 3];
        for (var round = 0; round < 1000; round++)
        {
            list.AddRange(chunk.AsSpan());
            if (list.Capacity != capacity)
            {
                capacity = list.Capacity;
                reallocations++;
            }
        }

        await Assert.That(list.Count).IsEqualTo(3000);
        await Assert.That(reallocations).IsLessThan(20);
    }

    [Test]
    [Arguments(2)]
    [Arguments(200)]
    public async Task List_AddRange_Span_OfItself(int count)
    {
        var list = Enumerable.Range(0, count).ToList();
        list.AddRange(CollectionsMarshal.AsSpan(list));
        await Assert.That(list.SequenceEqual(Enumerable.Range(0, count).Concat(Enumerable.Range(0, count)))).IsTrue();
    }

    [Test]
    public async Task List_AddRange_Span_Empty_DoesNotInvalidateEnumerators()
    {
        var list = new List<int> {1, 2, 3};
        var seen = 0;
        foreach (var _ in list)
        {
            list.AddRange(ReadOnlySpan<int>.Empty);
            seen++;
        }

        await Assert.That(seen).IsEqualTo(3);
    }

    [Test]
    [Arguments(1)]
    [Arguments(100)]
    public async Task List_AddRange_Span_NonEmpty_InvalidatesEnumerators(int count)
    {
        var list = new List<int> {1, 2, 3};
        var added = new int[count];
        using IEnumerator<int> enumerator = list.GetEnumerator();
        enumerator.MoveNext();
        list.AddRange(added.AsSpan());
        var threw = false;
        try
        {
            enumerator.MoveNext();
        }
        catch (InvalidOperationException)
        {
            threw = true;
        }

        await Assert.That(threw).IsTrue();
    }

    [Test]
    [Arguments(0)]
    [Arguments(1)]
    [Arguments(63)]
    [Arguments(64)]
    [Arguments(65)]
    [Arguments(5000)]
    public async Task List_CopyTo_Span_CopiesAllItems(int count)
    {
        var list = Enumerable.Range(10, count).ToList();
        // spare capacity must not be copied
        list.Capacity = count + 10;

        var exact = new int[count];
        list.CopyTo(exact.AsSpan());
        await Assert.That(exact.SequenceEqual(list)).IsTrue();

        // a longer destination keeps its tail, and an offset destination its head
        var longer = Enumerable.Repeat(-1, count + 4).ToArray();
        list.CopyTo(longer.AsSpan(2));
        await Assert.That(longer.Take(2).All(_ => _ == -1)).IsTrue();
        await Assert.That(longer.Skip(2).Take(count).SequenceEqual(list)).IsTrue();
        await Assert.That(longer.Skip(2 + count).All(_ => _ == -1)).IsTrue();

        var strings = list.Select(_ => _ % 7 == 0 ? null : _.ToString()).ToList();
        var stringDestination = new string?[count];
        strings.CopyTo(stringDestination.AsSpan());
        await Assert.That(((IEnumerable<string?>) stringDestination).SequenceEqual(strings)).IsTrue();
        // the list is left as it was
        await Assert.That(list.SequenceEqual(Enumerable.Range(10, count))).IsTrue();
    }

    [Test]
    public async Task List_CopyTo_Span_TooShort_Large_Throws()
    {
        var list = Enumerable.Range(0, 200).ToList();
        var destination = new int[199];
        await Assert.That(() => list.CopyTo(destination.AsSpan())).Throws<ArgumentException>();
        await Assert.That(destination.All(_ => _ == 0)).IsTrue();
    }
}
