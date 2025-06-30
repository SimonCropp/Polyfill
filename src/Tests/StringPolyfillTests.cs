// ReSharper disable PartialTypeWithSinglePart

[TestFixture]
partial class StringPolyfillTest
{
    [Test]
    public void Join()
    {
        Assert.AreEqual("bac", string.Join('a', ["b", "c"]));
        Assert.AreEqual("ba1c", string.Join("a1", ["b", "c"]));
        Assert.AreEqual("ba1a1c", string.Join("a1", ["b", null, "c"]));
        Assert.AreEqual("bac", string.Join('a', new object[] {"b", "c"}));
        Assert.AreEqual("baac", string.Join('a', new object?[] {"b", null, "c"}));
        // ReSharper disable once RedundantCast
        Assert.AreEqual("bac", StringPolyfill.Join('a', (IEnumerable<string>) new List<string>{"b", "c"}));
    }
}