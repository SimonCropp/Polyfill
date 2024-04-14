partial class PolyfillTests
{
    [Test]
    public void MaxBy()
    {
        var enumerable = (IEnumerable<int>)new List<int> {1, 2};

        Assert.AreEqual(2, enumerable.MaxBy(_ => _));
    }

    [Test]
    public void Except()
    {
        var enumerable = new List<int> {1, 2};
        Assert.AreEqual(1, enumerable.Except(2).Single());
    }

    [Test]
    public void MinBy()
    {
        var enumerable = (IEnumerable<int>)new List<int> {1, 2};

        Assert.AreEqual(1, enumerable.MinBy(_ => _));
    }
    [Test]
    public void TryGetNonEnumeratedCount()
    {
        var enumerable = (IEnumerable<int>)new List<int> {1, 2};

        Assert.True(enumerable.TryGetNonEnumeratedCount(out var count));
        Assert.AreEqual(2, count);
    }

    [Test]
    public void Index()
    {
        var count = 0;
        var enumerable = (IEnumerable<int>)new List<int> {3, 4};
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
        var enumerable = (IEnumerable<int>)new List<int> {3, 4};
        var distinctBy = enumerable.DistinctBy(_=>_).ToList();
        Assert.AreEqual(2, distinctBy.Count);
        Assert.AreEqual(3, distinctBy[0]);
        Assert.AreEqual(4, distinctBy[1]);
    }

    [Test]
    public void CountBy()
    {
        var enumerable = (IEnumerable<int>)new List<int> {3, 4, 3};
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
        var firstList = new List<string> { "banana", "apple", "cherry" };
        var secondList = new List<int> { 6 };

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
        var enumerable = (IEnumerable<string>)new List<string> {"a", "b"};

        Assert.IsTrue(enumerable.Append("c").SequenceEqual(new List<string> {"a", "b", "c"}));
    }

    [Test]
    public void IEnumerableSkipLast()
    {
        var enumerable = (IEnumerable<string>)new List<string> { "a", "b" };

        Assert.IsTrue(enumerable.SkipLast(1).SequenceEqual(new List<string> { "a" }));
    }

    [Test]
    public void ToHashSet ()
    {
        var enumerable = (IEnumerable<string>)new List<string> { "a", "b" };

        var hashSet = enumerable.ToHashSet();
        Assert.IsTrue(hashSet.Contains("a"));
        Assert.IsTrue(hashSet.Contains("b"));
    }

    [Test]
    public void Zip3()
    {
        var numbers = new List<int> { 1 };
        var words = new List<string> { "one" };
        var letters = new List<string> { "a" };

        var result = numbers.Zip(words, letters).Single();

        Assert.AreEqual(1, result.First);
        Assert.AreEqual("one", result.Second);
        Assert.AreEqual("a", result.Third);
    }

    [Test]
    public void Zip2()
    {
        var numbers = new List<int> { 1 };
        var words = new List<string> { "one" };

        var result = numbers.Zip(words).Single();

        Assert.AreEqual(1, result.First);
        Assert.AreEqual("one", result.Second);
    }

    [Test]
    public void Chunk_SizeOf3()
    {
        var enumerable = Enumerable.Range(1, 11).ToList();

        var chunks = enumerable.Chunk(3).ToList();

        Assert.AreEqual(new[] { 1, 2, 3 }, chunks[0]);
        Assert.AreEqual(new[] { 4, 5, 6 }, chunks[1]);
        Assert.AreEqual(new[] { 7, 8, 9 }, chunks[2]);
        Assert.AreEqual(new[] { 10, 11 }, chunks[3]);
    }

    [Test]
    public void Chunk_SizeOf8()
    {
        var enumerable = Enumerable.Range(1, 11).ToList();

        var chunks = enumerable.Chunk(8).ToList();

        Assert.AreEqual(new[] { 1, 2, 3, 4, 5, 6, 7, 8 }, chunks[0]);
        Assert.AreEqual(new[] { 9, 10, 11 }, chunks[1]);
    }

    [Test]
    public void Chunk_SizeOfZero_ExpectedException()
    {
        var enumerable = Enumerable.Range(1, 11).ToList();

        Assert.Throws<ArgumentOutOfRangeException>(() => enumerable.Chunk(0).ToList());
    }

    [Test]
    public void Chunk_Null_ExpectedException()
    {
        IEnumerable<int> values = null!;

        Assert.Throws<ArgumentNullException>(() => values.Chunk(1).ToList());
    }
}
