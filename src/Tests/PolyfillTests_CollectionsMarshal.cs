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
    [Test]
    public async Task CollectionsMarshal_AsSpan_SeveralElementTypes()
    {
        var strings = new List<string?> {"a", null, "c"};
        var stringSpan = CollectionsMarshal.AsSpan(strings);
        var stringsJoined = string.Join(",", stringSpan.ToArray());
        stringSpan[1] = "b";

        var pairs = new List<KeyValuePair<int, string>> {new(1, "one"), new(2, "two")};
        var pairSpan = CollectionsMarshal.AsSpan(pairs);
        var pairLength = pairSpan.Length;
        var secondPair = pairSpan[1];
        pairSpan[0] = new(10, "ten");

        var bytes = new List<byte> {1, 2, 3, 4};
        var byteSum = 0;
        foreach (var value in CollectionsMarshal.AsSpan(bytes))
        {
            byteSum += value;
        }

        var arrays = new List<int[]> {new[] {1}, new[] {2, 3}};
        var arraySpan = CollectionsMarshal.AsSpan(arrays);
        var secondArray = arraySpan[1];

        await Assert.That(stringsJoined).IsEqualTo("a,,c");
        await Assert.That(strings[1]).IsEqualTo("b");
        await Assert.That(pairLength).IsEqualTo(2);
        await Assert.That(secondPair.Value).IsEqualTo("two");
        await Assert.That(pairs[0].Key).IsEqualTo(10);
        await Assert.That(byteSum).IsEqualTo(10);
        await Assert.That(ReferenceEquals(secondArray, arrays[1])).IsTrue();
    }

    [Test]
    public async Task CollectionsMarshal_AsSpan_LengthIsCountNotCapacity()
    {
        var list = new List<int>(100) {1, 2, 3};
        var length = CollectionsMarshal.AsSpan(list).Length;
        await Assert.That(length).IsEqualTo(3);

        list.Clear();
        length = CollectionsMarshal.AsSpan(list).Length;
        await Assert.That(length).IsEqualTo(0);
    }

    [Test]
    public async Task CollectionsMarshal_AsSpan_FollowsTheListAsItGrows()
    {
        // growing replaces the backing array, so every call must read the current one
        var list = new List<int>();
        for (var count = 1; count <= 200; count++)
        {
            list.Add(count);
            var span = CollectionsMarshal.AsSpan(list);
            var length = span.Length;
            var last = span[count - 1];
            span[count - 1] = -count;
            var written = list[count - 1];
            list[count - 1] = count;

            await Assert.That(length).IsEqualTo(count);
            await Assert.That(last).IsEqualTo(count);
            await Assert.That(written).IsEqualTo(-count);
        }

        var copy = CollectionsMarshal.AsSpan(list).ToArray();
        await Assert.That(copy.SequenceEqual(Enumerable.Range(1, 200))).IsTrue();
    }

    [Test]
    public async Task CollectionsMarshal_AsSpan_SeparateListsOfTheSameType()
    {
        var first = new List<string> {"a", "b"};
        var second = new List<string> {"c"};

        var firstCopy = CollectionsMarshal.AsSpan(first).ToArray();
        var secondCopy = CollectionsMarshal.AsSpan(second).ToArray();
        var firstAgain = CollectionsMarshal.AsSpan(first).ToArray();

        await Assert.That(string.Join(",", firstCopy)).IsEqualTo("a,b");
        await Assert.That(string.Join(",", secondCopy)).IsEqualTo("c");
        await Assert.That(string.Join(",", firstAgain)).IsEqualTo("a,b");
    }

    [ExcludeFromCodeCoverage]
    [DebuggerNonUserCode]
    sealed class DerivedList : List<int>;

    [Test]
    public async Task CollectionsMarshal_AsSpan_DerivedList()
    {
        var list = new DerivedList {1, 2, 3};
        var span = CollectionsMarshal.AsSpan(list);
        var length = span.Length;
        span[0] = 100;

        await Assert.That(length).IsEqualTo(3);
        await Assert.That(list[0]).IsEqualTo(100);
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
