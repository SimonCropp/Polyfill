#region IndexRange

using NUnit.Framework;

[TestFixture]
class IndexRangeSample
{
    [Test]
    public void Usage()
    {
        var substring = "value"[2..];
        Assert.AreEqual("lue", substring);
    }
}

#endregion