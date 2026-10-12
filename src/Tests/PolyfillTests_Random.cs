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
    public async Task RandomNextSingle_NeverReturnsOne()
    {
        // With this seed a double that rounds to 1.0f is produced on call 5,800,140
        var random = new Random(0);
        var max = 0f;
        var min = 1f;
        for (var index = 0; index < 5_900_000; index++)
        {
            var value = random.NextSingle();
            max = Math.Max(max, value);
            min = Math.Min(min, value);
        }

        await Assert.That(min).IsGreaterThanOrEqualTo(0.0f);

        // The seeded Random in the .NET 6 and .NET 7 runtimes casts a double to a float,
        // so the real NextSingle can return 1.0 there. That was fixed in .NET 8.
        if (Environment.Version.Major is 6 or 7)
        {
            await Assert.That(max).IsLessThanOrEqualTo(1.0f);
            return;
        }

        await Assert.That(max).IsLessThan(1.0f);
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

    [Test]
    public async Task RandomNextInt64_NegativeRange()
    {
        var random = new Random();
        for (var i = 0; i < 100; i++)
        {
            var result = random.NextInt64(-10, -5);
            await Assert.That(result).IsGreaterThanOrEqualTo(-10);
            await Assert.That(result).IsLessThan(-5);
        }
    }

    [Test]
    public async Task RandomNextInt64_FullRange()
    {
        var random = new Random();
        for (var i = 0; i < 100; i++)
        {
            var result = random.NextInt64(long.MinValue, long.MaxValue);
            await Assert.That(result).IsLessThan(long.MaxValue);
        }
    }

    // NextInt64 is part of Random from net6, with an algorithm of its own
    static readonly bool randomNextInt64IsPolyfill = typeof(Random).GetMethod("NextInt64", Type.EmptyTypes) == null;

    // What the NextInt64 polyfill has always returned: unbiased rejection sampling over 8 bytes from NextBytes
    static long ReferenceNextInt64(Random random, long minValue, long maxValue)
    {
        if (minValue == maxValue)
        {
            return minValue;
        }

        var range = unchecked((ulong) (maxValue - minValue));
        var limit = ulong.MaxValue - ulong.MaxValue % range;
        var buffer = new byte[8];
        ulong result;
        do
        {
            random.NextBytes(buffer);
            result = BitConverter.ToUInt64(buffer, 0);
        } while (result >= limit);

        return unchecked((long) (result % range) + minValue);
    }

    [Test]
    public async Task RandomNextInt64_Seeded_SameSequence()
    {
        var failures = new List<string>();
        var ranges = new (long min, long max)[]
        {
            (0, 1),
            (0, 3),
            (0, 1000),
            (-5, 5),
            (0, long.MaxValue),
            (long.MinValue, long.MaxValue),
            (long.MinValue, 0),
            (-5, long.MaxValue / 2 + 7),
            // more than half of all values are rejected
            (long.MinValue, 1),
            (7, 7)
        };

        foreach (var (min, max) in ranges)
        {
            var random = new Random(1234);
            var reference = new Random(1234);

            for (var i = 0; i < 200; i++)
            {
                var value = random.NextInt64(min, max);

                if (value < min || (value >= max && min != max))
                {
                    failures.Add($"{value} is outside of {min}..{max}");
                }

                if (randomNextInt64IsPolyfill && value != ReferenceNextInt64(reference, min, max))
                {
                    failures.Add($"{value} is not the next value for {min}..{max}");
                }
            }

            // the generator is left in the same state
            if (randomNextInt64IsPolyfill && random.Next() != reference.Next())
            {
                failures.Add($"state after {min}..{max}");
            }
        }

        await Assert.That(failures).IsEmpty();
    }

    [Test]
    public async Task RandomNextInt64_Overloads_SameSequence()
    {
        var random = new Random(99);
        var reference = new Random(99);
        var first = random.NextInt64();
        var second = random.NextInt64(1000);

        await Assert.That(first).IsGreaterThanOrEqualTo(0);
        await Assert.That(second).IsGreaterThanOrEqualTo(0);
        await Assert.That(second).IsLessThan(1000);

        if (randomNextInt64IsPolyfill)
        {
            await Assert.That(first).IsEqualTo(ReferenceNextInt64(reference, 0, long.MaxValue));
            await Assert.That(second).IsEqualTo(ReferenceNextInt64(reference, 0, 1000));
        }
    }

    [Test]
    public async Task RandomNextInt64_Invalid_Throws()
    {
        var random = new Random(1);
        await Assert.That(() => random.NextInt64(5, 4)).Throws<ArgumentOutOfRangeException>();
        await Assert.That(random.NextInt64(5, 5)).IsEqualTo(5);
    }

    [Test]
    public async Task RandomNextInt64_CoversSmallRange()
    {
        var random = new Random(5);
        var counts = new int[5];
        for (var i = 0; i < 5000; i++)
        {
            counts[random.NextInt64(10, 15) - 10]++;
        }

        // 1000 expected for each, with a standard deviation of 28
        await Assert.That(counts.All(_ => _ is > 800 and < 1200)).IsTrue();
    }

    [Test]
    public async Task RandomNextInt64_Concurrent_SameSequences()
    {
        // each thread has a generator of its own, and must get the sequence it would get alone
        var expected = new long[200];
        var single = new Random(77);
        for (var i = 0; i < expected.Length; i++)
        {
            expected[i] = single.NextInt64(-1000, long.MaxValue / 3);
        }

        var tasks = new List<Task<bool>>();
        for (var t = 0; t < 8; t++)
        {
            tasks.Add(Task.Run(() =>
            {
                for (var round = 0; round < 50; round++)
                {
                    var random = new Random(77);
                    for (var i = 0; i < expected.Length; i++)
                    {
                        if (random.NextInt64(-1000, long.MaxValue / 3) != expected[i])
                        {
                            return false;
                        }
                    }
                }

                return true;
            }));
        }

        var results = await Task.WhenAll(tasks);
        await Assert.That(results.All(_ => _)).IsTrue();
    }

#if FeatureMemory

    // NextBytes(Span<byte>) is part of Random from netcoreapp2.1, where it does not go through NextBytes(byte[])
    static readonly bool randomNextBytesSpanIsPolyfill = typeof(Random).GetMethod("NextBytes", [typeof(Span<byte>)]) == null;

    [Test]
    [Arguments(0)]
    [Arguments(1)]
    [Arguments(7)]
    [Arguments(255)]
    [Arguments(256)]
    [Arguments(257)]
    [Arguments(512)]
    [Arguments(1000)]
    [Arguments(100_000)]
    public async Task RandomNextBytesSpan_Seeded_SameAsArray(int length)
    {
        var random = new Random(4321);
        var reference = new Random(4321);

        // a margin either side, to check nothing outside the span is written
        var buffer = new byte[length + 16];
        random.NextBytes(buffer.AsSpan(8, length));
        var expected = new byte[length];
        reference.NextBytes(expected);

        await Assert.That(buffer.AsSpan(8, length).SequenceEqual(expected)).IsTrue();
        await Assert.That(buffer.Take(8).All(_ => _ == 0)).IsTrue();
        await Assert.That(buffer.Skip(8 + length).All(_ => _ == 0)).IsTrue();

        // the generator is left in the same state
        await Assert.That(random.Next()).IsEqualTo(reference.Next());

        // and a second fill carries on from there
        random.NextBytes(buffer.AsSpan(8, length));
        reference.NextBytes(expected);
        await Assert.That(buffer.AsSpan(8, length).SequenceEqual(expected)).IsTrue();
    }

    [Test]
    public async Task RandomNextBytesSpan_Unseeded_Fills()
    {
        var random = new Random();
        var buffer = new byte[1000];
        random.NextBytes(buffer.AsSpan());
        await Assert.That(buffer.Distinct().Count()).IsGreaterThan(200);
        await Assert.That(buffer.Skip(900).Any(_ => _ != 0)).IsTrue();
    }

    [Test]
    public async Task RandomNextBytesSpan_DerivedType()
    {
        var random = new CountingRandom();
        var buffer = new byte[1000];
        random.NextBytes(buffer.AsSpan());

        if (randomNextBytesSpanIsPolyfill)
        {
            // a derived type still gets a single call for the whole of the span
            await Assert.That(random.ArrayCalls).IsEqualTo(1);
            await Assert.That(buffer.All(_ => _ == 42)).IsTrue();
        }
    }

    [ExcludeFromCodeCoverage]
    [System.Diagnostics.DebuggerNonUserCode]
    class CountingRandom : Random
    {
        public int ArrayCalls;

        public override void NextBytes(byte[] buffer)
        {
            ArrayCalls++;
            for (var i = 0; i < buffer.Length; i++)
            {
                buffer[i] = 42;
            }
        }
    }

#endif
}
