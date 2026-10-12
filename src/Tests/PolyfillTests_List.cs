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
    [Arguments(0, 0)]
    [Arguments(0, 1)]
    [Arguments(0, 2)]
    [Arguments(0, 500)]
    [Arguments(1, 0)]
    [Arguments(1, 1)]
    [Arguments(1, 7)]
    [Arguments(5, 2)]
    [Arguments(5, 3)]
    [Arguments(100, 1)]
    [Arguments(100, 2)]
    [Arguments(100, 5000)]
    public async Task List_InsertRange_MatchesInsertRangeOfArray(int initialCount, int insertCount)
    {
        var inserted = Enumerable.Range(1000, insertCount).ToArray();

        // a spread of valid indexes, including the start and the end
        for (var index = 0; index <= initialCount; index += Math.Max(1, initialCount / 5))
        {
            var expected = Enumerable.Range(0, initialCount).ToList();
            expected.InsertRange(index, (IEnumerable<int>) inserted);

            var list = Enumerable.Range(0, initialCount).ToList();
            list.InsertRange(index, inserted.AsSpan());

            await Assert.That(list.Count).IsEqualTo(initialCount + insertCount);
            await Assert.That(list.SequenceEqual(expected)).IsTrue();
        }
    }

    [Test]
    public async Task List_InsertRange_ReferenceTypes()
    {
        var list = new List<string?> {"a", "e"};
        string?[] inserted = ["b", null, "d"];
        list.InsertRange(1, inserted.AsSpan());
        await Assert.That(list.SequenceEqual(["a", "b", null, "d", "e"])).IsTrue();

        // the source is not modified
        await Assert.That(string.Join(",", inserted)).IsEqualTo("b,,d");
    }

    [Test]
    public async Task List_InsertRange_SliceOfLargerArray()
    {
        var array = Enumerable.Range(0, 100).ToArray();
        var list = new List<int> {-1, -2};
        list.InsertRange(1, array.AsSpan(10, 20));
        var expected = new List<int> {-1};
        expected.AddRange(Enumerable.Range(10, 20));
        expected.Add(-2);
        await Assert.That(list.SequenceEqual(expected)).IsTrue();
    }

    [Test]
    public async Task List_InsertRange_GrowsRepeatedly()
    {
        var list = new List<int>();
        var expected = new List<int>();
        for (var round = 0; round < 50; round++)
        {
            var chunk = Enumerable.Range(round * 100, round % 7).ToArray();
            var index = list.Count / 2;
            list.InsertRange(index, chunk.AsSpan());
            expected.InsertRange(index, (IEnumerable<int>) chunk);
        }

        await Assert.That(list.SequenceEqual(expected)).IsTrue();
    }

    [Test]
    public async Task List_InsertRange_Empty_DoesNotInvalidateEnumerators()
    {
        var list = new List<int> {1, 2, 3};
        var seen = 0;
        foreach (var _ in list)
        {
            list.InsertRange(list.Count, ReadOnlySpan<int>.Empty);
            seen++;
        }

        await Assert.That(seen).IsEqualTo(3);
        await Assert.That(list.SequenceEqual([1, 2, 3])).IsTrue();
    }

    [Test]
    public async Task List_InsertRange_NonEmpty_InvalidatesEnumerators()
    {
        foreach (var count in new[] {1, 2})
        {
            var list = new List<int> {1, 2, 3};
            var inserted = new int[count];
            using IEnumerator<int> enumerator = list.GetEnumerator();
            enumerator.MoveNext();
            list.InsertRange(0, inserted.AsSpan());
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
    }

    [Test]
    public async Task List_InsertRange_OutOfRangeIndex_LeavesListUntouched()
    {
        var list = new List<int> {1, 2, 3};
        int[] inserted = [4, 5];
        await Assert.That(() => list.InsertRange(-1, inserted.AsSpan())).Throws<ArgumentOutOfRangeException>();
        await Assert.That(() => list.InsertRange(4, inserted.AsSpan())).Throws<ArgumentOutOfRangeException>();
        await Assert.That(list.SequenceEqual([1, 2, 3])).IsTrue();
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
}
