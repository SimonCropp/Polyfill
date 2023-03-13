#region IndexRange

using NUnit.Framework;

[TestFixture]
class IndexRangeSample
{
    [Test]
    public void Range()
    {
        var substring = "value"[2..];
        Assert.AreEqual("lue", substring);
    }

    [Test]
    public void Index()
    {
        var substring = "value"[^2];
        Assert.AreEqual("u", substring);
    }
}

#endregion