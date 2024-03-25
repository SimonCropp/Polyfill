partial class PolyfillTests
{
    [Test]
    public void SortedListGetKeyAtIndex()
    {
        var list = new SortedList<int, char>
        {
            {
                3, 'x'
            }
        };
        var key = list.GetKeyAtIndex(0);
        Assert.AreEqual(3, key);
    }

    [Test]
    public void SortedListGetValueAtIndex()
    {
        var list = new SortedList<int, char>
        {
            {
                3, 'x'
            }
        };
        var value = list.GetValueAtIndex(0);
        Assert.AreEqual('x', value);
    }
}