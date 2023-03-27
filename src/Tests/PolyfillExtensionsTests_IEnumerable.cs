partial class PolyfillExtensionsTests
{
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
