partial class PolyfillTests
{
#if FeatureMemory && !WINDOWS_UWP

    [Test]
    public async Task CollectionsMarshal_AsSpan()
    {
        var list = new List<int> {1, 2, 3};
        var span = CollectionsMarshal.AsSpan(list);
        var length = span.Length;
        var first = span[0];
        var third = span[2];

        // Writing through the span must mutate the backing list.
        span[1] = 20;

        await Assert.That(length).IsEqualTo(3);
        await Assert.That(first).IsEqualTo(1);
        await Assert.That(third).IsEqualTo(3);
        await Assert.That(list[1]).IsEqualTo(20);
    }

    [Test]
    public async Task CollectionsMarshal_AsSpan_Empty()
    {
        var length = CollectionsMarshal.AsSpan(new List<int>()).Length;
        await Assert.That(length).IsEqualTo(0);
    }

    [Test]
    public async Task CollectionsMarshal_AsSpan_Null()
    {
        var length = CollectionsMarshal.AsSpan<int>(null).Length;
        await Assert.That(length).IsEqualTo(0);
    }

#endif

    [Test]
    public async Task CollectionsMarshal_SetCount_Grow()
    {
        var list = new List<int> {1, 2};
        CollectionsMarshal.SetCount(list, 4);
        await Assert.That(list.Count).IsEqualTo(4);
        await Assert.That(list[0]).IsEqualTo(1);
        await Assert.That(list[1]).IsEqualTo(2);
    }

    [Test]
    public async Task CollectionsMarshal_SetCount_Shrink()
    {
        var list = new List<int> {1, 2, 3, 4};
        CollectionsMarshal.SetCount(list, 2);
        await Assert.That(list.Count).IsEqualTo(2);
        await Assert.That(list[0]).IsEqualTo(1);
        await Assert.That(list[1]).IsEqualTo(2);
    }

    [Test]
    public async Task CollectionsMarshal_SetCount_Same()
    {
        var list = new List<int> {1, 2, 3};
        CollectionsMarshal.SetCount(list, 3);
        await Assert.That(list.Count).IsEqualTo(3);
        await Assert.That(list[2]).IsEqualTo(3);
    }
}
