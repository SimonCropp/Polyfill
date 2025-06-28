
partial class PolyfillTests
{
    [Test]
    public void CopyTo_ArraySegment_CopiesElements()
    {
        var source = new ArraySegment<int>([1, 2, 3, 4], 1, 2); // [2,3]
        var destArr = new int[2];
        var dest = new ArraySegment<int>(destArr);

        source.CopyTo(dest);

        Assert.AreEqual(new[]
        {
            2,
            3
        }, destArr);
    }

    [Test]
    public void CopyTo_Array_CopiesElements()
    {
        var source = new ArraySegment<string>(["a", "b", "c"], 1, 2); // ["b","c"]
        var dest = new string[2];

        source.CopyTo(dest);

        Assert.AreEqual(new[]
        {
            "b",
            "c"
        }, dest);
    }

    [Test]
    public void CopyTo_Array_WithOffset_CopiesElements()
    {
        var source = new ArraySegment<char>(['x', 'y', 'z'], 1, 2); // ['y','z']
        var dest = new char[4];

        source.CopyTo(dest, 1);

        Assert.AreEqual(new[]
        {
            '\0',
            'y',
            'z',
            '\0'
        }, dest);
    }

    [Test]
    public void CopyTo_ArraySegment_ThrowsIfDestinationTooShort()
    {
        var source = new ArraySegment<int>([1, 2, 3]);
        var dest = new ArraySegment<int>(new int[2]);

        Assert.Throws<ArgumentException>(() => source.CopyTo(dest));
    }

    [Test]
    public void Enumerates_All_Elements()
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

        Assert.That(result, Is.EqualTo(new[]
        {
            2,
            3
        }));
    }

    [Test]
    public void Empty_Segment_Enumerates_Nothing()
    {
        var arr = new[]
        {
            1,
            2,
            3
        };
        var segment = new ArraySegment<int>(arr, 1, 0);
        using var enumerator = segment.GetEnumerator();

        Assert.IsFalse(enumerator.MoveNext());
    }

    [Test]
    public void Current_Before_MoveNext_Throws()
    {
        var arr = new[]
        {
            1,
            2
        };
        var segment = new ArraySegment<int>(arr);
        using var enumerator = segment.GetEnumerator();

        Assert.Throws<InvalidOperationException>(() =>
        {
            var _ = enumerator.Current;
        });
    }

    [Test]
    public void Current_After_Enumeration_Throws()
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

        Assert.Throws<InvalidOperationException>(() =>
        {
            var _ = enumerator.Current;
        });
    }

    [Test]
    public void Reset_Allows_ReEnumeration()
    {
        var arr = new[]
        {
            5,
            6,
            7
        };
        var segment = new ArraySegment<int>(arr);
        using var enumerator = segment.GetEnumerator();

        Assert.IsTrue(enumerator.MoveNext());
        Assert.AreEqual(5, enumerator.Current);

        ((IEnumerator) enumerator).Reset();

        Assert.AreEqual(5, enumerator.Current);
        Assert.IsTrue(enumerator.MoveNext());
        Assert.AreEqual(6, enumerator.Current);
    }
}