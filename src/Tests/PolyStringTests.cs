// ReSharper disable PartialTypeWithSinglePart

[TestFixture]
partial class PolyStringTest
{
    [Test]
    public void Join()
    {
        Assert.AreEqual("bac", PolyString.Join('a', new []{"b","c"}));
        Assert.AreEqual("bac", PolyString.Join('a', new object[]{"b","c"}));
    }
}