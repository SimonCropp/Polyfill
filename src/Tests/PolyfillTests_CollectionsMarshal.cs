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
    public async Task CollectionsMarshal_SetCount_GrowKeepsItemsAndDefaultsTheRest()
    {
        var list = new List<string> {"a", "b"};
        CollectionsMarshal.SetCount(list, 2);
        CollectionsMarshal.SetCount(list, 3);
        list[2] = "c";
        CollectionsMarshal.SetCount(list, 40);
        list[39] = "z";

        await Assert.That(list.Count).IsEqualTo(40);
        await Assert.That(list.Capacity >= 40).IsTrue();
        await Assert.That(list[0]).IsEqualTo("a");
        await Assert.That(list[1]).IsEqualTo("b");
        await Assert.That(list[2]).IsEqualTo("c");
        await Assert.That(list[39]).IsEqualTo("z");
        await Assert.That(list.IndexOf("z")).IsEqualTo(39);
    }

    [Test]
    public async Task CollectionsMarshal_SetCount_GrowFromEmpty()
    {
        var list = new List<int>();
        CollectionsMarshal.SetCount(list, 0);
        await Assert.That(list.Count).IsEqualTo(0);

        CollectionsMarshal.SetCount(list, 1);
        list[0] = 7;
        await Assert.That(list.Count).IsEqualTo(1);
        await Assert.That(list[0]).IsEqualTo(7);

        CollectionsMarshal.SetCount(list, 1000);
        await Assert.That(list.Count).IsEqualTo(1000);
        await Assert.That(list[0]).IsEqualTo(7);
        await Assert.That(list.Capacity >= 1000).IsTrue();
    }

    [Test]
    public async Task CollectionsMarshal_SetCount_GrowWithinCapacity()
    {
        var list = new List<int>(16) {1, 2};
        CollectionsMarshal.SetCount(list, 10);

        await Assert.That(list.Count).IsEqualTo(10);
        await Assert.That(list.Capacity).IsEqualTo(16);
        await Assert.That(list[0]).IsEqualTo(1);
        await Assert.That(list[1]).IsEqualTo(2);
    }

    [Test]
    public async Task CollectionsMarshal_SetCount_GrowsCapacityLikeList()
    {
        // an exact fit when doubling is not enough, otherwise double
        var list = new List<int>();
        CollectionsMarshal.SetCount(list, 5);
        var afterFive = list.Capacity;
        CollectionsMarshal.SetCount(list, 6);
        var afterSix = list.Capacity;
        CollectionsMarshal.SetCount(list, 7);
        var afterSeven = list.Capacity;
        CollectionsMarshal.SetCount(list, 25);
        var afterTwentyFive = list.Capacity;

        var empty = new List<int>();
        CollectionsMarshal.SetCount(empty, 1);
        var afterOne = empty.Capacity;

        await Assert.That(afterFive).IsEqualTo(5);
        await Assert.That(afterSix).IsEqualTo(10);
        await Assert.That(afterSeven).IsEqualTo(10);
        await Assert.That(afterTwentyFive).IsEqualTo(25);
        await Assert.That(afterOne).IsEqualTo(4);
    }

    [Test]
    public async Task CollectionsMarshal_SetCount_IncrementalGrowth()
    {
        var list = new List<int>();
        for (var count = 1; count <= 20000; count++)
        {
            CollectionsMarshal.SetCount(list, count);
            list[count - 1] = count;
        }

        var mismatches = 0;
        for (var index = 0; index < list.Count; index++)
        {
            if (list[index] != index + 1)
            {
                mismatches++;
            }
        }

        await Assert.That(list.Count).IsEqualTo(20000);
        await Assert.That(mismatches).IsEqualTo(0);
        await Assert.That(list.Capacity >= 20000).IsTrue();
        await Assert.That(list.Capacity <= 40000).IsTrue();
    }

    [Test]
    public async Task CollectionsMarshal_SetCount_ShrinkThenGrow()
    {
        var list = new List<int> {1, 2, 3, 4};
        var capacity = list.Capacity;
        CollectionsMarshal.SetCount(list, 1);
        CollectionsMarshal.SetCount(list, 3);
        list[1] = 20;
        list[2] = 30;

        await Assert.That(list.Count).IsEqualTo(3);
        await Assert.That(list.Capacity).IsEqualTo(capacity);
        await Assert.That(list[0]).IsEqualTo(1);
        await Assert.That(list[1]).IsEqualTo(20);
        await Assert.That(list[2]).IsEqualTo(30);
    }

    [Test]
    public async Task CollectionsMarshal_SetCount_InvalidArguments()
    {
        await Assert.That(() => CollectionsMarshal.SetCount(new List<int>(), -1)).Throws<ArgumentOutOfRangeException>();
        await Assert.That(() => CollectionsMarshal.SetCount<int>(null!, 1)).Throws<Exception>();
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
