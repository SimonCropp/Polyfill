partial class PolyfillExtensionsTests
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
    public void IEnumerableAppend()
    {
        var enumerable = (IEnumerable<string>)new List<string> {"a", "b"};

        Assert.IsTrue(enumerable.Append("c").SequenceEqual(new List<string> {"a", "b", "c"}));
    }

    [Test]
    public void IEnumerableSkipLast()
    {
        var enumerable = (IEnumerable<string>)new List<string> {"a", "b"};

        Assert.IsTrue(enumerable.SkipLast(1).SequenceEqual(new List<string> {"a"}));
    }
}
