// ReSharper disable PartialTypeWithSinglePart

[TestFixture]
partial class StringPolyfillTest
{
    [Test]
    public void Join()
    {
        Assert.AreEqual("bac", StringPolyfill.Join('a', new []{"b","c"}));
        Assert.AreEqual("bac", StringPolyfill.Join('a', new object[]{"b","c"}));
    }
}