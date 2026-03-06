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
        {
            throw new($"Expected destination length 10 but got {destination.Length}");
        }

        var choicesArray = choices.ToArray();
        foreach (var item in destination)
        {
            if (!choicesArray.Contains(item))
            {
                throw new($"Item {item} not found in choices");
            }
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

    [Test]
    public async Task RandomNextInt64()
    {
        var random = new Random();
        var result = random.NextInt64();
        await Assert.That(result).IsGreaterThanOrEqualTo(0);
    }

    [Test]
    public async Task RandomNextInt64_MaxValue()
    {
        var random = new Random();
        var result = random.NextInt64(100);
        await Assert.That(result).IsGreaterThanOrEqualTo(0);
        await Assert.That(result).IsLessThan(100);
    }

    [Test]
    public async Task RandomNextInt64_MinMaxValue()
    {
        var random = new Random();
        var result = random.NextInt64(50, 100);
        await Assert.That(result).IsGreaterThanOrEqualTo(50);
        await Assert.That(result).IsLessThan(100);
    }

    [Test]
    public async Task RandomNextSingle()
    {
        var random = new Random();
        var result = random.NextSingle();
        await Assert.That(result).IsGreaterThanOrEqualTo(0.0f);
        await Assert.That(result).IsLessThan(1.0f);
    }

    [Test]
    public async Task RandomGetHexString()
    {
        var random = new Random();
        var result = random.GetHexString(10);
        await Assert.That(result.Length).IsEqualTo(10);
        await Assert.That(result.All(c => "0123456789ABCDEF".Contains(c))).IsTrue();
    }

    [Test]
    public async Task RandomGetHexString_Lowercase()
    {
        var random = new Random();
        var result = random.GetHexString(10, lowercase: true);
        await Assert.That(result.Length).IsEqualTo(10);
        await Assert.That(result.All(c => "0123456789abcdef".Contains(c))).IsTrue();
    }

#if FeatureMemory

    [Test]
    public Task RandomGetHexString_Span()
    {
        var random = new Random();
        Span<char> destination = stackalloc char[10];
        random.GetHexString(destination);
        var result = destination.ToString();
        if (result.Length != 10)
            throw new($"Expected length 10 but got {result.Length}");
        if (!result.All(c => "0123456789ABCDEF".Contains(c)))
            throw new("Expected only hex characters");
        return Task.CompletedTask;
    }

    [Test]
    public Task RandomGetString()
    {
        var random = new Random();
        ReadOnlySpan<char> choices = "abc".AsSpan();
        var result = random.GetString(choices, 10);
        if (result.Length != 10)
            throw new($"Expected length 10 but got {result.Length}");
        if (!result.All(c => "abc".Contains(c)))
            throw new("Expected only abc characters");
        return Task.CompletedTask;
    }

#endif
}
