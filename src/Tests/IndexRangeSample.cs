#region IndexRange

class IndexRangeSample
{
    [Test]
    public async Task Range()
    {
        var substring = "value"[2..];
        await Assert.That(substring).IsEqualTo("lue");
    }

    [Test]
    public async Task Index()
    {
        var ch = "value"[^2];
        await Assert.That(ch).IsEqualTo('u');
    }

    [Test]
    public async Task ArrayIndex()
    {
        var array = new[]
        {
            "value1",
            "value2"
        };

        var value = array[^2];

        await Assert.That(value).IsEqualTo("value1");
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