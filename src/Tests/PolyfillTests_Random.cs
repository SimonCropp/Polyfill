partial class PolyfillTests
{

#if FeatureMemory

    [Test]
    public void RandomNextBytesSpan()
    {
        var random = new Random();
        Span<byte> buffer = new byte[10];
        random.NextBytes(buffer);
        Assert.IsTrue(buffer.ToArray().Any(_ => _ != 0));
    }

    [Test]
    public void RandomGetItems_ReadOnlySpan_Span()
    {
        var random = new Random();
        ReadOnlySpan<int> choices = [1, 2, 3, 4, 5];
        Span<int> destination = new int[10];
        random.GetItems(choices, destination);

        Assert.IsTrue(destination.Length == 10);
        foreach (var item in destination)
        {
            Assert.IsTrue(choices.Contains(item));
        }
    }

    [Test]
    public void RandomGetItems_ReadOnlySpan_Int()
    {
        var random = new Random();
        ReadOnlySpan<int> choices = [1, 2, 3, 4, 5];
        var length = 10;
        var result = random.GetItems(choices, length);

        Assert.AreEqual(length, result.Length);
        foreach (var item in result)
        {
            Assert.Contains(item, choices.ToArray());
        }
    }

    [Test]
    public void RandomGetItems_Array_Int()
    {
        var random = new Random();
        int[] choices = [1, 2, 3, 4, 5];
        var length = 10;
        var result = random.GetItems(choices, length);

        Assert.AreEqual(length, result.Length);
        foreach (var item in result)
        {
            Assert.Contains(item, choices);
        }
    }

    [Test]
    public void RandomShuffleSpan()
    {
        var random = new Random();
        Span<byte> buffer = new byte[10];
        random.NextBytes(buffer);
        random.Shuffle(buffer);
        Assert.IsTrue(buffer.ToArray().Any(b => b != 0));
    }

#endif

    [Test]
    public void RandomShuffleArray()
    {
        var random = new Random();
        var buffer = new byte[10];
        random.NextBytes(buffer);
        random.Shuffle(buffer);
        Assert.IsTrue(buffer.ToArray().Any(b => b != 0));
    }
}