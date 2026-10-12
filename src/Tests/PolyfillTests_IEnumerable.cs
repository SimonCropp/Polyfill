// ReSharper disable ReturnValueOfPureMethodIsNotUsed
partial class PolyfillTests
{
    [Test]
    public async Task TakeRange()
    {
        var letters = new List<char>
        {
            'a',
            'b',
            'c',
            'd',
            'e',
        };

        var taken = letters.Take(1..3).ToList();
        await Assert.That(taken.SequenceEqual(['b', 'c'])).IsTrue();
    }

    [Test]
    public async Task TakeLast()
    {
        var letters = new List<char>
        {
            'a',
            'b',
            'c',
            'd',
            'e',
        };

        var taken = letters.TakeLast(2).ToList();
        await Assert.That(taken.SequenceEqual(['d', 'e'])).IsTrue();
    }

    static IEnumerable<int> Iterate(int count)
    {
        for (var index = 0; index < count; index++)
        {
            yield return index;
        }
    }

    static int[] ExpectedTake(int count, Range range)
    {
        var start = range.Start.Value;
        if (range.Start.IsFromEnd)
        {
            start = count - start;
        }

        var end = range.End.Value;
        if (range.End.IsFromEnd)
        {
            end = count - end;
        }

        start = Math.Max(0, start);
        end = Math.Min(count, end);
        if (start >= end)
        {
            return [];
        }

        return Enumerable.Range(start, end - start).ToArray();
    }

    [Test]
    [Arguments(0)]
    [Arguments(1)]
    [Arguments(2)]
    [Arguments(10)]
    public async Task TakeRange_AllSourceKinds(int count)
    {
        var ranges = new[]
        {
            Range.All, 0..0, 0..1, 1..3, 2..2, 3..1, 0..count, 0..100, 5..100, 100..200,
            ^0.., ^1.., ^3.., ^100.., ..^0, ..^1, ..^3, ..^100,
            ^3..^1, ^1..^3, ^100..^1, ^5..^100, 1..^1, 4..^4, 100..^1, ^3..2, ^3..9, ^3..100, ^100..3
        };

        foreach (var range in ranges)
        {
            var expected = ExpectedTake(count, range);
            var array = Enumerable.Range(0, count).ToArray();

            await Assert.That(array.Take(range).SequenceEqual(expected)).IsTrue();
            await Assert.That(array.ToList().Take(range).SequenceEqual(expected)).IsTrue();
            await Assert.That(new System.Collections.ObjectModel.ReadOnlyCollection<int>(array).Take(range).SequenceEqual(expected)).IsTrue();
            await Assert.That(new Queue<int>(array).Take(range).SequenceEqual(expected)).IsTrue();
            await Assert.That(Iterate(count).Take(range).SequenceEqual(expected)).IsTrue();

            // the result can be enumerated more than once
            var taken = array.ToList().Take(range);
            await Assert.That(taken.SequenceEqual(expected)).IsTrue();
            await Assert.That(taken.SequenceEqual(expected)).IsTrue();
        }
    }

    [Test]
    [Arguments(0)]
    [Arguments(1)]
    [Arguments(2)]
    [Arguments(10)]
    public async Task TakeLast_AllSourceKinds(int count)
    {
        foreach (var take in new[] {-1, 0, 1, 2, 3, 9, 10, 11, 100})
        {
            var expected = ExpectedTake(count, ^Math.Max(0, take)..);
            var array = Enumerable.Range(0, count).ToArray();

            await Assert.That(array.TakeLast(take).SequenceEqual(expected)).IsTrue();
            await Assert.That(array.ToList().TakeLast(take).SequenceEqual(expected)).IsTrue();
            await Assert.That(new System.Collections.ObjectModel.ReadOnlyCollection<int>(array).TakeLast(take).SequenceEqual(expected)).IsTrue();
            await Assert.That(new Queue<int>(array).TakeLast(take).SequenceEqual(expected)).IsTrue();
            await Assert.That(Iterate(count).TakeLast(take).SequenceEqual(expected)).IsTrue();
        }
    }

    [Test]
    public async Task TakeRange_List_IsDeferred()
    {
        var list = new List<int> {0, 1, 2, 3, 4};
        var fromStart = list.Take(1..4);
        var fromEnd = list.Take(^2..);
        var last = list.TakeLast(2);

        list.Add(5);
        list[1] = 100;

        await Assert.That(fromStart.SequenceEqual([100, 2, 3])).IsTrue();
        await Assert.That(fromEnd.SequenceEqual([4, 5])).IsTrue();
        await Assert.That(last.SequenceEqual([4, 5])).IsTrue();

        list.Clear();

        await Assert.That(fromStart.Any()).IsFalse();
        await Assert.That(fromEnd.Any()).IsFalse();
        await Assert.That(last.Any()).IsFalse();
    }

    [Test]
    public async Task TakeRange_List_ShrinksWhileEnumerating()
    {
        var list = new List<int> {0, 1, 2, 3, 4, 5};
        var result = new List<int>();
        foreach (var item in list.Take(1..5))
        {
            result.Add(item);
            if (item == 2)
            {
                // the range ends at the new end of the list
                list.RemoveRange(3, 3);
            }
        }

        await Assert.That(result.SequenceEqual([1, 2])).IsTrue();
    }

    [Test]
    public async Task TakeRange_List_ChangesWhileEnumerating()
    {
        var list = new List<int> {0, 1, 2, 3, 4, 5};
        var result = new List<int>();
        foreach (var item in list.Take(1..4))
        {
            result.Add(item);
            if (item == 1)
            {
                // items are read from the list as the range is walked
                list[2] = 200;
                list.Add(6);
            }
        }

        await Assert.That(result.SequenceEqual([1, 200, 3])).IsTrue();
    }

    [Test]
    public async Task TakeRange_FromEnd_List_UsesCountAtFirstMoveNext()
    {
        var list = new List<int> {0, 1, 2, 3, 4, 5};
        var result = new List<int>();
        foreach (var item in list.Take(^4..^1))
        {
            result.Add(item);
            if (item == 2)
            {
                list.RemoveAt(5);
                list.RemoveAt(4);
            }
        }

        // the bounds were fixed as 2..5 when enumeration started, and the list now ends at 4
        await Assert.That(result.SequenceEqual([2, 3])).IsTrue();
    }

    [Test]
    public async Task Except()
    {
        IEnumerable<int> enumerable = [1, 2];
        await Assert.That(enumerable.Except(2).Single()).IsEqualTo(1);
    }

    [Test]
    public async Task TryGetNonEnumeratedCount()
    {
        IEnumerable<int> enumerable = [1, 2];

        await Assert.That(enumerable.TryGetNonEnumeratedCount(out var count)).IsTrue();
        await Assert.That(count).IsEqualTo(2);
    }

    [Test]
    public async Task Index()
    {
        var count = 0;
        IEnumerable<int> enumerable = [3, 4];
        foreach (var (index, item) in enumerable.Index())
        {
            count++;
            if (index == 0)
            {
                await Assert.That(item).IsEqualTo(3);
            }

            if (index == 1)
            {
                await Assert.That(item).IsEqualTo(4);
            }
        }

        await Assert.That(count).IsEqualTo(2);
    }

    [Test]
    public async Task DistinctBy()
    {
        IEnumerable<int> enumerable = [3, 4];
        var distinctBy = enumerable.DistinctBy(_ => _).ToList();
        await Assert.That(distinctBy.Count).IsEqualTo(2);
        await Assert.That(distinctBy[0]).IsEqualTo(3);
        await Assert.That(distinctBy[1]).IsEqualTo(4);
    }

    [Test]
    public async Task CountBy()
    {
        IEnumerable<int> enumerable = [3, 4, 3];
        var list = enumerable.CountBy(_ => _).ToList();
        await Assert.That(list[0].Key).IsEqualTo(3);
        await Assert.That(list[0].Value).IsEqualTo(2);
        await Assert.That(list[1].Key).IsEqualTo(4);
        await Assert.That(list[1].Value).IsEqualTo(1);
        await Assert.That(list.Count).IsEqualTo(2);
    }

    [Test]
    public async Task IEnumerableExceptBy()
    {
        IEnumerable<string> firstList = ["banana", "apple", "cherry"];
        IEnumerable<int> secondList = [6];

        var result = firstList.ExceptBy(secondList, _ => _.Length).ToList();
        await Assert.That(result.SequenceEqual(["apple"])).IsTrue();
    }

    [Test]
    public async Task AggregateBySeed()
    {
        (string id, int score)[] data =
        [
            ("0", 42),
            ("1", 5),
            ("2", 4),
            ("1", 10),
            ("0", 25),
        ];

        var aggregated =
            data.AggregateBy(
                keySelector: entry => entry.id,
                seed: 0,
                (totalScore, curr) => totalScore + curr.score
            ).ToList();

        await Assert.That(aggregated[0].Key).IsEqualTo("0");
        await Assert.That(aggregated[0].Value).IsEqualTo(67);
        await Assert.That(aggregated[1].Key).IsEqualTo("1");
        await Assert.That(aggregated[1].Value).IsEqualTo(15);
        await Assert.That(aggregated[2].Key).IsEqualTo("2");
        await Assert.That(aggregated[2].Value).IsEqualTo(4);
        await Assert.That(aggregated.Count).IsEqualTo(3);
    }

    [Test]
    public async Task AggregateBySeedSelector()
    {
        (string id, int score)[] data =
        [
            ("0", 42),
            ("1", 5),
            ("2", 4),
            ("1", 10),
            ("0", 25),
        ];

        var seed = 0;
        var aggregated =
            data.AggregateBy(
                keySelector: entry => entry.id,
                seedSelector: _ => seed++,
                (totalScore, curr) => totalScore + curr.score
            ).ToList();

        await Assert.That(aggregated[0].Key).IsEqualTo("0");
        await Assert.That(aggregated[0].Value).IsEqualTo(67);
        await Assert.That(aggregated[1].Key).IsEqualTo("1");
        await Assert.That(aggregated[1].Value).IsEqualTo(16);
        await Assert.That(aggregated[2].Key).IsEqualTo("2");
        await Assert.That(aggregated[2].Value).IsEqualTo(6);
        await Assert.That(aggregated.Count).IsEqualTo(3);
    }

    [Test]
    public async Task IEnumerableAppend()
    {
        IEnumerable<string> enumerable = ["a", "b"];

        await Assert.That(enumerable.Append("c").SequenceEqual(["a", "b", "c"])).IsTrue();
    }

    [Test]
    public async Task IEnumerablePrepend()
    {
        IEnumerable<string> enumerable = ["b", "c"];

        await Assert.That(enumerable.Prepend("a").SequenceEqual(["a", "b", "c"])).IsTrue();
    }

    [Test]
    public async Task IEnumerableSkipLast()
    {
        IEnumerable<string> enumerable = ["a", "b"];

        await Assert.That(enumerable.SkipLast(1).SequenceEqual(["a"])).IsTrue();
    }

    [Test]
    public async Task ToHashSet()
    {
        IEnumerable<string> enumerable = ["a", "b"];

        var hashSet = enumerable.ToHashSet();
        await Assert.That(hashSet.Contains("a")).IsTrue();
        await Assert.That(hashSet.Contains("b")).IsTrue();
    }

    [Test]
    public async Task ToHashSetComparer()
    {
        IEnumerable<string> enumerable = ["a", "A", "b"];

        var hashSet = enumerable.ToHashSet(StringComparer.OrdinalIgnoreCase);
        await Assert.That(hashSet.Count).IsEqualTo(2);
        await Assert.That(hashSet.Contains("B")).IsTrue();
    }

    [Test]
    public async Task Zip3()
    {
        var numbers = new List<int> {1};
        var words = new List<string> {"one"};
        var letters = new List<string> {"a"};

        var result = numbers.Zip(words, letters).Single();

        await Assert.That(result.First).IsEqualTo(1);
        await Assert.That(result.Second).IsEqualTo("one");
        await Assert.That(result.Third).IsEqualTo("a");
    }

    [Test]
    public async Task ElementAtIndex()
    {
#pragma warning disable IDE0028
        IEnumerable<int> list = new List<int> {1, 2};
#pragma warning restore IDE0028

        // ReSharper disable ArrangeObjectCreationWhenTypeNotEvident
        await Assert.That(list.ElementAt(new Index(1))).IsEqualTo(2);
        await Assert.That(list.ElementAtOrDefault(new Index(1))).IsEqualTo(2);
        await Assert.That(list.ElementAtOrDefault(new Index(3))).IsEqualTo(0);
        // ReSharper restore ArrangeObjectCreationWhenTypeNotEvident
    }

    [Test]
    public async Task LastOrDefault()
    {
        IEnumerable<int> list = [1, 2];

        await Assert.That(list.LastOrDefault(_ => _ == 2, 3)).IsEqualTo(2);
        await Assert.That(Enumerable.Empty<int>().LastOrDefault(3)).IsEqualTo(3);
    }

    [Test]
    public async Task SingleOrDefault()
    {
        IEnumerable<int> list = [1, 2];

        await Assert.That(list.SingleOrDefault(_ => _ == 2, 3)).IsEqualTo(2);
        await Assert.That(Enumerable.Empty<int>().SingleOrDefault(3)).IsEqualTo(3);
    }

    [Test]
    public async Task FirstOrDefault()
    {
        IEnumerable<int> list = [1, 2];

        await Assert.That(list.FirstOrDefault(_ => _ == 2, 3)).IsEqualTo(2);
        await Assert.That(Enumerable.Empty<int>().FirstOrDefault(3)).IsEqualTo(3);
    }

    [Test]
    public async Task Zip2()
    {
        var numbers = new List<int> {1};
        var words = new List<string> {"one"};

        var result = numbers.Zip(words).Single();

        await Assert.That(result.First).IsEqualTo(1);
        await Assert.That(result.Second).IsEqualTo("one");
    }

    [Test]
    public async Task Chunk_SizeOf3()
    {
        var enumerable = Enumerable.Range(1, 11).ToList();

        var chunks = enumerable.Chunk(3).ToList();

        await Assert.That(chunks[0].SequenceEqual([1, 2, 3])).IsTrue();
        await Assert.That(chunks[1].SequenceEqual([4, 5, 6])).IsTrue();
        await Assert.That(chunks[2].SequenceEqual([7, 8, 9])).IsTrue();
        await Assert.That(chunks[3].SequenceEqual([10, 11])).IsTrue();
    }

    [Test]
    public async Task Chunk_SizeOf8()
    {
        var enumerable = Enumerable.Range(1, 11).ToList();

        var chunks = enumerable.Chunk(8).ToList();

        await Assert.That(chunks[0].SequenceEqual([1, 2, 3, 4, 5, 6, 7, 8])).IsTrue();
        await Assert.That(chunks[1].SequenceEqual([9, 10, 11])).IsTrue();
    }

    [Test]
    public async Task Chunk_SizeOfZero_ExpectedException()
    {
        var enumerable = Enumerable.Range(1, 11).ToList();

        await Assert.That(() => enumerable.Chunk(0).ToList()).Throws<ArgumentOutOfRangeException>();
    }

#if NET7_0_OR_GREATER
    [Test]
    public async Task InfiniteSequence_StartsAtGivenValue()
    {
        var start = 10;
        var step = 2;
        var sequence = Enumerable.InfiniteSequence(start, step)
            .Take(5)
            .ToList();
        await Assert.That(sequence[0]).IsEqualTo(start);
    }

    [Test]
    public async Task InfiniteSequence_IncrementsByStep()
    {
        var start = 1;
        var step = 3;
        var sequence = Enumerable.InfiniteSequence(start, step)
            .Take(4)
            .ToList();
        await Assert.That(sequence.SequenceEqual(new[] {1, 4, 7, 10})).IsTrue();
    }

    [Test]
    public async Task InfiniteSequence_IsInfinite()
    {
        var start = 0;
        var step = 1;
        var sequence = Enumerable.InfiniteSequence(start, step);
        // Only take first 100 elements to avoid infinite loop
        await Assert.That(sequence.Take(100).Count()).IsEqualTo(100);
    }

    [Test]
    public async Task Sequence_Increments_FromStartToEndInclusive()
    {
        var result = Enumerable.Sequence(1, 5, 1).ToList();
        await Assert.That(result.SequenceEqual(new[] {1, 2, 3, 4, 5})).IsTrue();
    }

    [Test]
    public async Task Sequence_Decrements_FromStartToEndInclusive()
    {
        var result = Enumerable.Sequence(5, 1, -1).ToList();
        await Assert.That(result.SequenceEqual(new[] {5, 4, 3, 2, 1})).IsTrue();
    }

    [Test]
    public async Task Sequence_SingleValue_WhenStartEqualsEnd()
    {
        var result = Enumerable.Sequence(3, 3, 1).ToList();
        await Assert.That(result.SequenceEqual(new[] {3})).IsTrue();
    }

    [Test]
    public async Task Sequence_Throws_WhenStepPositiveAndEndLessThanStart() =>
        await Assert.That(() => Enumerable.Sequence(5, 1, 1).ToList()).Throws<ArgumentOutOfRangeException>();

    [Test]
    public async Task Sequence_Throws_WhenStepNegativeAndEndGreaterThanStart() =>
        await Assert.That(() => Enumerable.Sequence(1, 5, -1).ToList()).Throws<ArgumentOutOfRangeException>();

    // The argument checks are not deferred, so no enumeration is needed (or wanted: a NaN used to give an endless sequence)
    [Test]
    public async Task Sequence_Throws_WhenStartIsNaN()
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => Enumerable.Sequence(double.NaN, 1d, 1d));
        await Assert.That(exception.ParamName).IsEqualTo("start");
    }

    [Test]
    public async Task Sequence_Throws_WhenEndIsNaN()
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => Enumerable.Sequence(0d, double.NaN, 1d));
        await Assert.That(exception.ParamName).IsEqualTo("endInclusive");
    }

    [Test]
    public async Task Sequence_Throws_WhenStepIsNaN()
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => Enumerable.Sequence(0d, 1d, double.NaN));
        await Assert.That(exception.ParamName).IsEqualTo("step");
    }

    [Test]
    public async Task Sequence_Throws_WhenStepIsZeroAndStartNotEqualToEnd()
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => Enumerable.Sequence(1, 5, 0));
        await Assert.That(exception.ParamName).IsEqualTo("step");

        exception = Assert.Throws<ArgumentOutOfRangeException>(() => Enumerable.Sequence(1d, 5d, -0d));
        await Assert.That(exception.ParamName).IsEqualTo("step");
    }

    [Test]
    public async Task Sequence_SingleValue_WhenStepIsZeroAndStartEqualsEnd()
    {
        var result = Enumerable.Sequence(3, 3, 0).ToList();
        await Assert.That(result.SequenceEqual(new[] {3})).IsTrue();
    }

    [Test]
    public async Task Sequence_Throws_WithParamName_WhenEndOnWrongSideOfStart()
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => Enumerable.Sequence(5, 1, 1));
        await Assert.That(exception.ParamName).IsEqualTo("endInclusive");

        exception = Assert.Throws<ArgumentOutOfRangeException>(() => Enumerable.Sequence(1, 5, -1));
        await Assert.That(exception.ParamName).IsEqualTo("endInclusive");
    }

#endif

    [Test]
    public async Task IEnumerableUnionBy()
    {
        IEnumerable<string> firstList = ["banana" /*6*/, "apple" /*5*/, "cherry" /*5*/];
        IEnumerable<string> secondList = ["banana" /*6*/, "apple2" /*6*/, "cherry" /*5*/, "strawberry" /*10*/];

        var result = firstList.UnionBy(secondList, _ => _.Length).ToList();
        await Assert.That(result.SequenceEqual(["banana", "apple", "strawberry"])).IsTrue();
    }

    [Test]
    public async Task IEnumerableUnionByWithComparer()
    {
        IEnumerable<string> firstList = ["banana" /*ba*/, "apple" /*ap*/];
        IEnumerable<string> secondList = ["banana" /*ba*/, "Apple2" /*Ap*/];

        var result = firstList.UnionBy(secondList, _ => _[0..1], comparer: StringComparer.OrdinalIgnoreCase).ToList();
        await Assert.That(result.SequenceEqual(["banana", "apple"])).IsTrue();

        result = firstList.UnionBy(secondList, _ => _[0..1], comparer: null).ToList();
        await Assert.That(result.SequenceEqual(["banana", "apple", "Apple2"])).IsTrue();

        result = firstList.UnionBy(secondList, _ => _[0..1], comparer: StringComparer.Ordinal).ToList();
        await Assert.That(result.SequenceEqual(["banana", "apple", "Apple2"])).IsTrue();
    }

    [Test]
    public async Task ArrayReverse()
    {
        int[] array = [1, 2, 3];
        var result = array.Reverse().ToList();
        await Assert.That(result.SequenceEqual([3, 2, 1])).IsTrue();
    }

    [Test]
    public async Task IEnumerableOrder()
    {
        IEnumerable<int> enumerable = [3, 1, 2];
        var result = enumerable.Order().ToList();
        await Assert.That(result.SequenceEqual([1, 2, 3])).IsTrue();
    }

    [Test]
    public async Task IEnumerableOrderWithComparer()
    {
        IEnumerable<string> enumerable = ["banana", "apple", "cherry"];
        var result = enumerable.Order(StringComparer.Ordinal).ToList();
        await Assert.That(result.SequenceEqual(["apple", "banana", "cherry"])).IsTrue();
    }

    [Test]
    public async Task IEnumerableOrderDescending()
    {
        IEnumerable<int> enumerable = [1, 3, 2];
        var result = enumerable.OrderDescending().ToList();
        await Assert.That(result.SequenceEqual([3, 2, 1])).IsTrue();
    }

    [Test]
    public async Task IEnumerableOrderDescendingWithComparer()
    {
        IEnumerable<string> enumerable = ["banana", "apple", "cherry"];
        var result = enumerable.OrderDescending(StringComparer.Ordinal).ToList();
        await Assert.That(result.SequenceEqual(["cherry", "banana", "apple"])).IsTrue();
    }

    [Test]
    public async Task IEnumerableIntersectBy()
    {
        IEnumerable<string> firstList = ["banana", "apple", "cherry"];
        IEnumerable<int> secondList = [6, 5];

        var result = firstList.IntersectBy(secondList, _ => _.Length).ToList();
        await Assert.That(result.SequenceEqual(["banana", "apple"])).IsTrue();
    }

    [Test]
    public async Task IEnumerableIntersectByWithComparer()
    {
        IEnumerable<string> firstList = ["banana", "apple", "cherry"];
        IEnumerable<int> secondList = [6, 5];

        var result = firstList.IntersectBy(secondList, _ => _.Length, EqualityComparer<int>.Default).ToList();
        await Assert.That(result.SequenceEqual(["banana", "apple"])).IsTrue();
    }

    [Test]
    public async Task IEnumerableLeftJoin()
    {
        IEnumerable<string> people = ["Magnus", "Terry", "Charlotte", "Tom"];
        IEnumerable<(string Name, string Owner)> pets =
        [
            ("Barley", "Terry"),
            ("Boots", "Terry"),
            ("Whiskers", "Charlotte"),
            ("Daisy", "Magnus")
        ];

        var result = people.LeftJoin(
            pets,
            person => person,
            pet => pet.Owner,
            (person, pet) => $"{person}-{pet.Name}").ToList();

        await Assert.That(result).Count().IsEqualTo(5);
        await Assert.That(result[0]).IsEqualTo("Magnus-Daisy");
        await Assert.That(result[1]).IsEqualTo("Terry-Barley");
        await Assert.That(result[2]).IsEqualTo("Terry-Boots");
        await Assert.That(result[3]).IsEqualTo("Charlotte-Whiskers");
        await Assert.That(result[4]).IsEqualTo("Tom-");
    }

    [Test]
    public async Task IEnumerableLeftJoinWithComparer()
    {
        IEnumerable<string> outer = ["A", "B", "C"];
        IEnumerable<(string Key, int Value)> inner = [("a", 1), ("b", 2)];

        var result = outer.LeftJoin(
            inner,
            o => o,
            i => i.Key,
            (o, i) => $"{o}-{i.Value}",
            StringComparer.OrdinalIgnoreCase).ToList();

        await Assert.That(result).Count().IsEqualTo(3);
        await Assert.That(result[0]).IsEqualTo("A-1");
        await Assert.That(result[1]).IsEqualTo("B-2");
        await Assert.That(result[2]).IsEqualTo("C-0");
    }

    [Test]
    public async Task IEnumerableRightJoin()
    {
        IEnumerable<string> people = ["Terry", "Charlotte", "Tom"];
        IEnumerable<(string Name, string Owner)> pets =
        [
            ("Barley", "Terry"),
            ("Boots", "Terry"),
            ("Whiskers", "Charlotte"),
            ("Daisy", "Magnus")
        ];

        var result = people.RightJoin(
            pets,
            person => person,
            pet => pet.Owner,
            (person, pet) => $"{person}-{pet.Name}").ToList();

        await Assert.That(result).Count().IsEqualTo(4);
        await Assert.That(result[0]).IsEqualTo("Terry-Barley");
        await Assert.That(result[1]).IsEqualTo("Terry-Boots");
        await Assert.That(result[2]).IsEqualTo("Charlotte-Whiskers");
        await Assert.That(result[3]).IsEqualTo("-Daisy");
    }

    [Test]
    public async Task IEnumerableRightJoinWithComparer()
    {
        IEnumerable<string> outer = ["A", "B"];
        IEnumerable<(string Key, int Value)> inner = [("a", 1), ("b", 2), ("c", 3)];

        var result = outer.RightJoin(
            inner,
            o => o,
            i => i.Key,
            (o, i) => $"{o}-{i.Value}",
            StringComparer.OrdinalIgnoreCase).ToList();

        await Assert.That(result).Count().IsEqualTo(3);
        await Assert.That(result[0]).IsEqualTo("A-1");
        await Assert.That(result[1]).IsEqualTo("B-2");
        await Assert.That(result[2]).IsEqualTo("-3");
    }

    [Test]
    public async Task IEnumerableJoinTuple()
    {
        IEnumerable<string> people = ["Terry", "Charlotte", "Tom"];
        IEnumerable<(string Name, string Owner)> pets =
        [
            ("Barley", "Terry"),
            ("Boots", "Terry"),
            ("Whiskers", "Charlotte"),
            ("Daisy", "Magnus")
        ];

        var result = people.Join(
            pets,
            person => person,
            pet => pet.Owner).ToList();

        await Assert.That(result).Count().IsEqualTo(3);
        await Assert.That(result[0].Outer).IsEqualTo("Terry");
        await Assert.That(result[0].Inner.Name).IsEqualTo("Barley");
        await Assert.That(result[1].Inner.Name).IsEqualTo("Boots");
        await Assert.That(result[2].Outer).IsEqualTo("Charlotte");
        await Assert.That(result[2].Inner.Name).IsEqualTo("Whiskers");
    }

    [Test]
    public async Task IEnumerableLeftJoinTuple()
    {
        IEnumerable<string> people = ["Magnus", "Terry", "Tom"];
        IEnumerable<(string Name, string Owner)> pets =
        [
            ("Daisy", "Magnus"),
            ("Barley", "Terry")
        ];

        var result = people.LeftJoin(
            pets,
            person => person,
            pet => pet.Owner).ToList();

        await Assert.That(result).Count().IsEqualTo(3);
        await Assert.That(result[0].Outer).IsEqualTo("Magnus");
        await Assert.That(result[0].Inner.Name).IsEqualTo("Daisy");
        await Assert.That(result[1].Outer).IsEqualTo("Terry");
        await Assert.That(result[1].Inner.Name).IsEqualTo("Barley");
        await Assert.That(result[2].Outer).IsEqualTo("Tom");
        await Assert.That(result[2].Inner.Name).IsNull();
    }

    [Test]
    public async Task IEnumerableRightJoinTuple()
    {
        IEnumerable<string> people = ["Terry", "Charlotte"];
        IEnumerable<(string Name, string Owner)> pets =
        [
            ("Barley", "Terry"),
            ("Whiskers", "Charlotte"),
            ("Daisy", "Magnus")
        ];

        var result = people.RightJoin(
            pets,
            person => person,
            pet => pet.Owner).ToList();

        await Assert.That(result).Count().IsEqualTo(3);
        await Assert.That(result[0].Outer).IsEqualTo("Terry");
        await Assert.That(result[0].Inner.Name).IsEqualTo("Barley");
        await Assert.That(result[1].Outer).IsEqualTo("Charlotte");
        await Assert.That(result[2].Outer).IsNull();
        await Assert.That(result[2].Inner.Name).IsEqualTo("Daisy");
    }

    [Test]
    public async Task IEnumerableFullJoin()
    {
        IEnumerable<string> people = ["Magnus", "Terry", "Tom"];
        IEnumerable<(string Name, string Owner)> pets =
        [
            ("Daisy", "Magnus"),
            ("Barley", "Terry"),
            ("Stray", "Nobody")
        ];

        var result = people.FullJoin(
            pets,
            person => person,
            pet => pet.Owner,
            (person, pet) => $"{person ?? "?"}-{pet.Name ?? "?"}").ToList();

        await Assert.That(result).Count().IsEqualTo(4);
        await Assert.That(result[0]).IsEqualTo("Magnus-Daisy");
        await Assert.That(result[1]).IsEqualTo("Terry-Barley");
        await Assert.That(result[2]).IsEqualTo("Tom-?");
        await Assert.That(result[3]).IsEqualTo("?-Stray");
    }

    [Test]
    public async Task IEnumerableFullJoinTuple()
    {
        IEnumerable<string> people = ["Magnus", "Tom"];
        IEnumerable<(string Name, string Owner)> pets =
        [
            ("Daisy", "Magnus"),
            ("Stray", "Nobody")
        ];

        var result = people.FullJoin(
            pets,
            person => person,
            pet => pet.Owner).ToList();

        await Assert.That(result).Count().IsEqualTo(3);
        await Assert.That(result[0].Outer).IsEqualTo("Magnus");
        await Assert.That(result[0].Inner.Name).IsEqualTo("Daisy");
        await Assert.That(result[1].Outer).IsEqualTo("Tom");
        await Assert.That(result[1].Inner.Name).IsNull();
        await Assert.That(result[2].Outer).IsNull();
        await Assert.That(result[2].Inner.Name).IsEqualTo("Stray");
    }

    static readonly (int Id, string? Key)[] nullKeyOuter = [(1, "a"), (2, null), (3, "b"), (4, "a"), (5, null)];
    static readonly (int Id, string? Key)[] nullKeyInner = [(10, "c"), (20, null), (30, "a"), (40, null), (50, "c"), (60, "a")];

    [Test]
    public async Task IEnumerableLeftJoinNullKeysDoNotMatch()
    {
        var result = nullKeyOuter.LeftJoin(
                nullKeyInner,
                _ => _.Key,
                _ => _.Key,
                (outer, inner) => $"{outer.Id}-{inner.Id}")
            .ToList();

        await Assert.That(string.Join(" ", result)).IsEqualTo("1-30 1-60 2-0 3-0 4-30 4-60 5-0");
    }

    [Test]
    public async Task IEnumerableRightJoinNullKeysDoNotMatch()
    {
        var result = nullKeyOuter.RightJoin(
                nullKeyInner,
                _ => _.Key,
                _ => _.Key,
                (outer, inner) => $"{outer.Id}-{inner.Id}")
            .ToList();

        await Assert.That(string.Join(" ", result)).IsEqualTo("0-10 0-20 1-30 4-30 0-40 0-50 1-60 4-60");
    }

    [Test]
    public async Task IEnumerableFullJoinNullKeysDoNotMatch()
    {
        var result = nullKeyOuter.FullJoin(
                nullKeyInner,
                _ => _.Key,
                _ => _.Key,
                (outer, inner) => $"{outer.Id}-{inner.Id}")
            .ToList();

        await Assert.That(string.Join(" ", result)).IsEqualTo("1-30 1-60 2-0 3-0 4-30 4-60 5-0 0-10 0-50 0-20 0-40");
    }

    [Test]
    public async Task IEnumerableGroupJoinTuple()
    {
        IEnumerable<string> people = ["Magnus", "Terry", "Tom"];
        IEnumerable<(string Name, string Owner)> pets =
        [
            ("Daisy", "Magnus"),
            ("Barley", "Terry"),
            ("Boots", "Terry")
        ];

        var result = people.GroupJoin(
            pets,
            person => person,
            pet => pet.Owner).ToList();

        await Assert.That(result).Count().IsEqualTo(3);
        await Assert.That(result[0].Key).IsEqualTo("Magnus");
        await Assert.That(result[0].Single().Name).IsEqualTo("Daisy");
        await Assert.That(result[1].Key).IsEqualTo("Terry");
        await Assert.That(result[1].Count()).IsEqualTo(2);
        await Assert.That(result[2].Key).IsEqualTo("Tom");
        await Assert.That(result[2].Any()).IsFalse();
    }
}
