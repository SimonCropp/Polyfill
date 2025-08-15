// ReSharper disable ReturnValueOfPureMethodIsNotUsed
partial class PolyfillTests
{
    [Test]
    public void TakeRange()
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
        Assert.IsTrue(taken.SequenceEqual(['b', 'c']));
    }

    [Test]
    public void TakeLast()
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
        Assert.IsTrue(taken.SequenceEqual(['d', 'e']));
    }

    [Test]
    public void Except()
    {
        IEnumerable<int> enumerable = [1, 2];
        Assert.AreEqual(1, enumerable.Except(2).Single());
    }

    [Test]
    public void TryGetNonEnumeratedCount()
    {
        IEnumerable<int> enumerable = [1, 2];

        Assert.True(enumerable.TryGetNonEnumeratedCount(out var count));
        Assert.AreEqual(2, count);
    }

    [Test]
    public void Index()
    {
        var count = 0;
        IEnumerable<int> enumerable = [3, 4];
        foreach (var (index, item) in enumerable.Index())
        {
            count++;
            if (index == 0)
            {
                Assert.AreEqual(3, item);
            }

            if (index == 1)
            {
                Assert.AreEqual(4, item);
            }
        }

        Assert.AreEqual(2, count);
    }

    [Test]
    public void DistinctBy()
    {
        IEnumerable<int> enumerable = [3, 4];
        var distinctBy = enumerable.DistinctBy(_ => _).ToList();
        Assert.AreEqual(2, distinctBy.Count);
        Assert.AreEqual(3, distinctBy[0]);
        Assert.AreEqual(4, distinctBy[1]);
    }

    [Test]
    public void CountBy()
    {
        IEnumerable<int> enumerable = [3, 4, 3];
        var list = enumerable.CountBy(_ => _).ToList();
        Assert.AreEqual(3, list[0].Key);
        Assert.AreEqual(2, list[0].Value);
        Assert.AreEqual(4, list[1].Key);
        Assert.AreEqual(1, list[1].Value);
        Assert.AreEqual(2, list.Count);
    }

    [Test]
    public void IEnumerableExceptBy()
    {
        IEnumerable<string> firstList = ["banana", "apple", "cherry"];
        IEnumerable<int> secondList = [6];

        var result = firstList.ExceptBy(secondList, _ => _.Length).ToList();
        Assert.IsTrue(result.SequenceEqual(["apple"]));
    }

    [Test]
    public void AggregateBySeed()
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

        Assert.AreEqual("0", aggregated[0].Key);
        Assert.AreEqual(67, aggregated[0].Value);
        Assert.AreEqual("1", aggregated[1].Key);
        Assert.AreEqual(15, aggregated[1].Value);
        Assert.AreEqual("2", aggregated[2].Key);
        Assert.AreEqual(4, aggregated[2].Value);
        Assert.AreEqual(3, aggregated.Count);
    }

    [Test]
    public void AggregateBySeedSelector()
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

        Assert.AreEqual("0", aggregated[0].Key);
        Assert.AreEqual(67, aggregated[0].Value);
        Assert.AreEqual("1", aggregated[1].Key);
        Assert.AreEqual(16, aggregated[1].Value);
        Assert.AreEqual("2", aggregated[2].Key);
        Assert.AreEqual(6, aggregated[2].Value);
        Assert.AreEqual(3, aggregated.Count);
    }

    [Test]
    public void IEnumerableAppend()
    {
        IEnumerable<string> enumerable = ["a", "b"];

        Assert.IsTrue(enumerable.Append("c").SequenceEqual(["a", "b", "c"]));
    }

    [Test]
    public void IEnumerableSkipLast()
    {
        IEnumerable<string> enumerable = ["a", "b"];

        Assert.IsTrue(enumerable.SkipLast(1).SequenceEqual(["a"]));
    }

    [Test]
    public void ToHashSet()
    {
        IEnumerable<string> enumerable = ["a", "b"];

        var hashSet = enumerable.ToHashSet();
        Assert.IsTrue(hashSet.Contains("a"));
        Assert.IsTrue(hashSet.Contains("b"));
    }

    [Test]
    public void Zip3()
    {
        var numbers = new List<int> {1};
        var words = new List<string> {"one"};
        var letters = new List<string> {"a"};

        var result = numbers.Zip(words, letters).Single();

        Assert.AreEqual(1, result.First);
        Assert.AreEqual("one", result.Second);
        Assert.AreEqual("a", result.Third);
    }

    [Test]
    public void ElementAtIndex()
    {
#pragma warning disable IDE0028
        IEnumerable<int> list = new List<int> {1, 2};
#pragma warning restore IDE0028

        // ReSharper disable ArrangeObjectCreationWhenTypeNotEvident
        Assert.AreEqual(2, list.ElementAt(new Index(1)));
        Assert.AreEqual(2, list.ElementAtOrDefault(new Index(1)));
        Assert.AreEqual(0, list.ElementAtOrDefault(new Index(3)));
        // ReSharper restore ArrangeObjectCreationWhenTypeNotEvident
    }

    [Test]
    public void LastOrDefault()
    {
        IEnumerable<int> list = [1, 2];

        Assert.AreEqual(2, list.LastOrDefault(_ => _ == 2, 3));
        Assert.AreEqual(3, Enumerable.Empty<int>().LastOrDefault(3));
    }

    [Test]
    public void SingleOrDefault()
    {
        IEnumerable<int> list = [1, 2];

        Assert.AreEqual(2, list.SingleOrDefault(_ => _ == 2, 3));
        Assert.AreEqual(3, Enumerable.Empty<int>().SingleOrDefault(3));
    }

    [Test]
    public void FirstOrDefault()
    {
        IEnumerable<int> list = [1, 2];

        Assert.AreEqual(2, list.FirstOrDefault(_ => _ == 2, 3));
        Assert.AreEqual(3, Enumerable.Empty<int>().FirstOrDefault(3));
    }

    [Test]
    public void Zip2()
    {
        var numbers = new List<int> {1};
        var words = new List<string> {"one"};

        var result = numbers.Zip(words).Single();

        Assert.AreEqual(1, result.First);
        Assert.AreEqual("one", result.Second);
    }

    [Test]
    public void Chunk_SizeOf3()
    {
        var enumerable = Enumerable.Range(1, 11).ToList();

        var chunks = enumerable.Chunk(3).ToList();

        Assert.AreEqual(new[] {1, 2, 3}, chunks[0]);
        Assert.AreEqual(new[] {4, 5, 6}, chunks[1]);
        Assert.AreEqual(new[] {7, 8, 9}, chunks[2]);
        Assert.AreEqual(new[] {10, 11}, chunks[3]);
    }

    [Test]
    public void Chunk_SizeOf8()
    {
        var enumerable = Enumerable.Range(1, 11).ToList();

        var chunks = enumerable.Chunk(8).ToList();

        Assert.AreEqual(new[] {1, 2, 3, 4, 5, 6, 7, 8}, chunks[0]);
        Assert.AreEqual(new[] {9, 10, 11}, chunks[1]);
    }

    [Test]
    public void Chunk_SizeOfZero_ExpectedException()
    {
        var enumerable = Enumerable.Range(1, 11).ToList();

        Assert.Throws<ArgumentOutOfRangeException>(() => enumerable.Chunk(0).ToList());
    }

#if NET7_0_OR_GREATER
    [Test]
    public void InfiniteSequence_StartsAtGivenValue()
    {
        var start = 10;
        var step = 2;
        var sequence = Enumerable.InfiniteSequence(start, step)
            .Take(5)
            .ToList();
        Assert.AreEqual(start, sequence[0]);
    }

    [Test]
    public void InfiniteSequence_IncrementsByStep()
    {
        var start = 1;
        var step = 3;
        var sequence = Enumerable.InfiniteSequence(start, step)
            .Take(4)
            .ToList();
        Assert.AreEqual(new[] {1, 4, 7, 10}, sequence);
    }

    [Test]
    public void InfiniteSequence_IsInfinite()
    {
        var start = 0;
        var step = 1;
        var sequence = Enumerable.InfiniteSequence(start, step);
        // Only take first 100 elements to avoid infinite loop
        Assert.AreEqual(100, sequence.Take(100).Count());
    }

    [Test]
    public void Sequence_Increments_FromStartToEndInclusive()
    {
        var result = Enumerable.Sequence(1, 5, 1).ToList();
        Assert.AreEqual(new[] {1, 2, 3, 4, 5}, result);
    }

    [Test]
    public void Sequence_Decrements_FromStartToEndInclusive()
    {
        var result = Enumerable.Sequence(5, 1, -1).ToList();
        Assert.AreEqual(new[] {5, 4, 3, 2, 1}, result);
    }

    [Test]
    public void Sequence_SingleValue_WhenStartEqualsEnd()
    {
        var result = Enumerable.Sequence(3, 3, 1).ToList();
        Assert.AreEqual(new[] {3}, result);
    }

    [Test]
    public void Sequence_Throws_WhenStepPositiveAndEndLessThanStart() =>
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            Enumerable.Sequence(5, 1, 1).ToList());

    [Test]
    public void Sequence_Throws_WhenStepNegativeAndEndGreaterThanStart() =>
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            Enumerable.Sequence(1, 5, -1).ToList());

    [Test]
    public void IEnumerableUnionBy()
    {
        IEnumerable<string> firstList = ["banana" /*6*/, "apple" /*5*/, "cherry" /*5*/];
        IEnumerable<string> secondList = ["banana" /*6*/, "apple2" /*6*/, "cherry" /*5*/, "strawberry" /*10*/];

        var result = firstList.UnionBy(secondList, _ => _.Length).ToList();
        Assert.IsTrue(result.SequenceEqual(["banana", "apple", "strawberry"]));
    }

    [Test]
    public void IEnumerableUnionByWithComparer()
    {
        IEnumerable<string> firstList = ["banana" /*ba*/, "apple" /*ap*/];
        IEnumerable<string> secondList = ["banana" /*ba*/, "Apple2" /*Ap*/];

        var result = firstList.UnionBy(secondList, _ => _[0..1], comparer: StringComparer.OrdinalIgnoreCase).ToList();
        Assert.IsTrue(result.SequenceEqual(["banana", "apple"]));

        result = firstList.UnionBy(secondList, _ => _[0..1], comparer: null).ToList();
        Assert.IsTrue(result.SequenceEqual(["banana", "apple", "Apple2"]));

        result = firstList.UnionBy(secondList, _ => _[0..1], comparer: StringComparer.Ordinal).ToList();
        Assert.IsTrue(result.SequenceEqual(["banana", "apple", "Apple2"]));
    }
}