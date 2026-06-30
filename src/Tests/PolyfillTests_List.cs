partial class PolyfillTests
{
    [Test]
    public async Task List_CopyTo_TooShort_Throws_AndLeavesDestinationUntouched()
    {
        var list = new List<int> {10, 20, 30, 40, 50};

        var destination = new int[3];
        await Assert.That(() => list.CopyTo(new Span<int>(destination))).Throws<ArgumentException>();
        await Assert.That(destination.All(_ => _ == 0)).IsTrue();

        var ok = new int[5];
        list.CopyTo(new Span<int>(ok));
        await Assert.That(string.Join(",", ok)).IsEqualTo("10,20,30,40,50");
    }

    [Test]
    public async Task List_InsertRange_InvalidIndex_Throws()
    {
        var list = new List<int> {1, 2, 3};
        await Assert.That(() => list.InsertRange(10, ReadOnlySpan<int>.Empty)).Throws<ArgumentOutOfRangeException>();
    }

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

    [Test]
    public async Task List_EnsureCapacity()
    {
        var list = new List<int>();
        list.EnsureCapacity(100);
        // Should not throw - capacity is a hint
        await Assert.That(list.Count).IsEqualTo(0);
    }

    [Test]
    public async Task List_TrimExcess()
    {
        var list = new List<int> { 1, 2, 3 };
        list.TrimExcess();
        // Should not throw
        await Assert.That(list.Count).IsEqualTo(3);
    }
}
