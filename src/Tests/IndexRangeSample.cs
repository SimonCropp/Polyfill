#region IndexRange

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
        var ch = "value"[^2];
        Assert.AreEqual('u', ch);
    }

    [Test]
    public void ArrayIndex()
    {
        var array = new[]
        {
            "value1",
            "value2"
        };

        var value = array[^2];

        Assert.AreEqual("value1", value);
    }
}

#endregion

//Array not supported due to no RuntimeHelpers.GetSubArray
// [Test]
// public void ArrayRange()
// {
//     var array = new[]
//     {
//         "value1",
//         "value2"
//     };
//
//     var subArray = array[..1];
//
//     Assert.AreEqual(1, subArray.Length);
//     Assert.AreEqual("value1", subArray[0]);
// }