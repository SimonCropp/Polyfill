
partial class PolyfillTests
{
    [Test]
    public async Task CopyTo_ArraySegment_CopiesElements()
    {
        var source = new ArraySegment<int>([1, 2, 3, 4], 1, 2); // [2,3]
        var destArr = new int[2];
        var dest = new ArraySegment<int>(destArr);

        source.CopyTo(dest);

        await Assert.That(destArr.SequenceEqual(new[] { 2, 3 })).IsTrue();
    }

    [Test]
    public async Task CopyTo_Array_CopiesElements()
    {
        var source = new ArraySegment<string>(["a", "b", "c"], 1, 2); // ["b","c"]
        var dest = new string[2];

        source.CopyTo(dest);

        await Assert.That(dest.SequenceEqual(new[] { "b", "c" })).IsTrue();
    }

    [Test]
    public async Task CopyTo_Array_WithOffset_CopiesElements()
    {
        var source = new ArraySegment<char>(['x', 'y', 'z'], 1, 2); // ['y','z']
        var dest = new char[4];

        source.CopyTo(dest, 1);

        await Assert.That(dest.SequenceEqual(new[] { '\0', 'y', 'z', '\0' })).IsTrue();
    }

    [Test]
    public async Task CopyTo_ArraySegment_ThrowsIfDestinationTooShort()
    {
        var source = new ArraySegment<int>([1, 2, 3]);
        var dest = new ArraySegment<int>(new int[2]);

        await Assert.That(() => source.CopyTo(dest)).Throws<ArgumentException>();
    }

    [Test]
    public async Task Enumerates_All_Elements()
    {
        var arr = new[]
        {
            1,
            2,
            3,
            4
        };
        var segment = new ArraySegment<int>(arr, 1, 2); // [2,3]
        using var enumerator = segment.GetEnumerator();

        var result = new List<int>();
        while (enumerator.MoveNext())
        {
            result.Add(enumerator.Current);
        }

        await Assert.That(result.SequenceEqual(new[] { 2, 3 })).IsTrue();
    }

    [Test]
    public async Task Empty_Segment_Enumerates_Nothing()
    {
        var arr = new[]
        {
            1,
            2,
            3
        };
        var segment = new ArraySegment<int>(arr, 1, 0);
        using var enumerator = segment.GetEnumerator();

        await Assert.That(enumerator.MoveNext()).IsFalse();
    }

    [Test]
    public async Task Current_Before_MoveNext_Throws()
    {
        var arr = new[]
        {
            1,
            2
        };
        var segment = new ArraySegment<int>(arr);
        using var enumerator = segment.GetEnumerator();

        await Assert.That(() =>
        {
            var _ = enumerator.Current;
        }).Throws<InvalidOperationException>();
    }

    [Test]
    public async Task Current_After_Enumeration_Throws()
    {
        var arr = new[]
        {
            1,
            2
        };
        var segment = new ArraySegment<int>(arr);
        using var enumerator = segment.GetEnumerator();

        while (enumerator.MoveNext())
        {
        }

        await Assert.That(() =>
        {
            var _ = enumerator.Current;
        }).Throws<InvalidOperationException>();
    }

    [Test]
    public async Task Reset_Allows_ReEnumeration()
    {
        var arr = new[]
        {
            5,
            6,
            7
        };
        var segment = new ArraySegment<int>(arr);
        using var enumerator = segment.GetEnumerator();

        await Assert.That(enumerator.MoveNext()).IsTrue();
        await Assert.That(enumerator.Current).IsEqualTo(5);

        ((IEnumerator) enumerator).Reset();

        await Assert.That(enumerator.Current).IsEqualTo(5);
        await Assert.That(enumerator.MoveNext()).IsTrue();
        await Assert.That(enumerator.Current).IsEqualTo(6);
    }
}