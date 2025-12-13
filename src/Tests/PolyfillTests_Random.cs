partial class PolyfillTests
{

#if FeatureMemory

    [Test]
    public async Task RandomNextBytesSpan()
    {
        var random = new Random();
        Span<byte> buffer = new byte[10];
        random.NextBytes(buffer);
        await Assert.That(buffer.ToArray().Any(_ => _ != 0)).IsTrue();
    }

    [Test]
    public Task RandomGetItems_ReadOnlySpan_Span()
    {
        var random = new Random();
        ReadOnlySpan<int> choices = [1, 2, 3, 4, 5];
        Span<int> destination = new int[10];
        random.GetItems(choices, destination);

        if (destination.Length != 10)
            throw new Exception($"Expected destination length 10 but got {destination.Length}");
        var choicesArray = choices.ToArray();
        foreach (var item in destination)
        {
            if (!choicesArray.Contains(item))
                throw new Exception($"Item {item} not found in choices");
        }
        return Task.CompletedTask;
    }

    [Test]
    public Task RandomGetItems_ReadOnlySpan_Int()
    {
        var random = new Random();
        ReadOnlySpan<int> choices = [1, 2, 3, 4, 5];
        var length = 10;
        var result = random.GetItems(choices, length);

        if (result.Length != length)
            throw new Exception($"Expected length {length} but got {result.Length}");
        var choicesArray = choices.ToArray();
        foreach (var item in result)
        {
            if (!choicesArray.Contains(item))
                throw new Exception($"Item {item} not found in choices");
        }
        return Task.CompletedTask;
    }

    [Test]
    public async Task RandomGetItems_Array_Int()
    {
        var random = new Random();
        int[] choices = [1, 2, 3, 4, 5];
        var length = 10;
        var result = random.GetItems(choices, length);

        await Assert.That(result.Length).IsEqualTo(length);
        foreach (var item in result)
        {
            await Assert.That(choices).Contains(item);
        }
    }

    [Test]
    public async Task RandomShuffleSpan()
    {
        var random = new Random();
        Span<byte> buffer = new byte[10];
        random.NextBytes(buffer);
        random.Shuffle(buffer);
        await Assert.That(buffer.ToArray().Any(b => b != 0)).IsTrue();
    }

#endif

    [Test]
    public async Task RandomShuffleArray()
    {
        var random = new Random();
        var buffer = new byte[10];
        random.NextBytes(buffer);
        random.Shuffle(buffer);
        await Assert.That(buffer.ToArray().Any(b => b != 0)).IsTrue();
    }
}
