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
}
