
partial class PolyfillTests
{
    [Test]
    public void CopyTo_ArraySegment_CopiesElements()
    {
        var source = new ArraySegment<int>(new[] { 1, 2, 3, 4 }, 1, 2); // [2,3]
        var destArr = new int[2];
        var dest = new ArraySegment<int>(destArr);

        source.CopyTo(dest);

        Assert.AreEqual(new[] { 2, 3 }, destArr);
    }

    [Test]
    public void CopyTo_Array_CopiesElements()
    {
        var source = new ArraySegment<string>(new[] { "a", "b", "c" }, 1, 2); // ["b","c"]
        var dest = new string[2];

        source.CopyTo(dest);

        Assert.AreEqual(new[] { "b", "c" }, dest);
    }

    [Test]
    public void CopyTo_Array_WithOffset_CopiesElements()
    {
        var source = new ArraySegment<char>(new[] { 'x', 'y', 'z' }, 1, 2); // ['y','z']
        var dest = new char[4];

        source.CopyTo(dest, 1);

        Assert.AreEqual(new[] { '\0', 'y', 'z', '\0' }, dest);
    }

    [Test]
    public void CopyTo_ArraySegment_ThrowsIfDestinationTooShort()
    {
        var source = new ArraySegment<int>(new[] { 1, 2, 3 });
        var dest = new ArraySegment<int>(new int[2]);

        Assert.Throws<ArgumentException>(() => source.CopyTo(dest));
    }
}