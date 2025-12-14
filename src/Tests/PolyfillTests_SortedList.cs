partial class PolyfillTests
{
    [Test]
    public async Task SortedListGetKeyAtIndex()
    {
        var list = new SortedList<int, char>
        {
            {
                3, 'x'
            }
        };
        var key = list.GetKeyAtIndex(0);
        await Assert.That(key).IsEqualTo(3);
    }

    [Test]
    public async Task SortedListGetValueAtIndex()
    {
        var list = new SortedList<int, char>
        {
            {
                3, 'x'
            }
        };
        var value = list.GetValueAtIndex(0);
        await Assert.That(value).IsEqualTo('x');
    }
}