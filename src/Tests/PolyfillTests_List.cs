partial class PolyfillTests
{
    [Test]
    public async Task IListAsReadOnly()
    {
        IList<char> list = ['a'];
        var readOnly = list.AsReadOnly();
        await Assert.That(readOnly[0]).IsEqualTo('a');
    }

    [Test]
    public async Task ListAddRangeReadOnlySpan()
    {
        var list = new List<char>();
        list.AddRange("ab".AsSpan());
        await Assert.That(list[0]).IsEqualTo('a');
        await Assert.That(list[1]).IsEqualTo('b');
    }

    [Test]
    public async Task ListInsertRangeReadOnlySpan()
    {
        var list = new List<char>
        {
            'a'
        };
        list.InsertRange(1, "bc".AsSpan());
        await Assert.That(list[1]).IsEqualTo('b');
        await Assert.That(list[2]).IsEqualTo('c');
    }

    [Test]
    public async Task ListCopyToSpan()
    {
        var list = new List<char>
        {
            'a'
        };
        var array = new char[1];
        list.CopyTo(array.AsSpan());
        await Assert.That(array[0]).IsEqualTo('a');
    }
}
