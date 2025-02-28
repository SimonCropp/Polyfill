partial class PolyfillTests
{
    [Test]
    public void IListAsReadOnly()
    {
        IList<char> list = ['a'];
        var readOnly = list.AsReadOnly();
        Assert.AreEqual('a', readOnly[0]);
    }

    [Test]
    public void ListAddRangeReadOnlySpan()
    {
        var list = new List<char>();
        list.AddRange("ab".AsSpan());
        Assert.AreEqual('a', list[0]);
        Assert.AreEqual('b', list[1]);
    }

    [Test]
    public void ListInsertRangeReadOnlySpan()
    {
        var list = new List<char>
        {
            'a'
        };
        list.InsertRange(1, "bc".AsSpan());
        Assert.AreEqual('b', list[1]);
        Assert.AreEqual('c', list[2]);
    }

    [Test]
    public void ListCopyToSpan()
    {
        var list = new List<char>
        {
            'a'
        };
        var array = new char[1];
        list.CopyTo(array.AsSpan());
        Assert.AreEqual('a', array[0]);
    }
}
