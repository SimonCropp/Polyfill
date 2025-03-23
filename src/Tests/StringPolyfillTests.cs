// ReSharper disable PartialTypeWithSinglePart

[TestFixture]
partial class StringPolyfillTest
{
    [Test]
    public void Join()
    {
        Assert.AreEqual("bac", StringPolyfill.Join('a', ["b","c"]));
        Assert.AreEqual("ba1c", StringPolyfill.Join("a1", ["b","c"]));
        Assert.AreEqual("bac", StringPolyfill.Join('a', new object[]{"b","c"}));
    }
}