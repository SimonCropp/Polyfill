using System.Security.Cryptography;

public class RandomNumberGeneratorPolyfillTests
{
    [Test]
    public async Task GetInt32_ReturnsFullRange()
    {
        var seen = new HashSet<int>();
        for (var i = 0; i < 200; i++)
        {
            seen.Add(RandomNumberGenerator.GetInt32(0, 3));
        }

        await Assert.That(seen.Contains(0)).IsTrue();
        await Assert.That(seen.Contains(1)).IsTrue();
        // The exclusive-upper-bound-minus-one value was previously never produced.
        await Assert.That(seen.Contains(2)).IsTrue();
        await Assert.That(seen.All(_ => _ is >= 0 and < 3)).IsTrue();
    }

    [Test]
    public async Task GetInt32_Range_Valid()
    {
        var min = 5;
        var max = 10;
        for (var i = 0; i < 100; i++)
        {
            var value = RandomNumberGenerator.GetInt32(min, max);
            await Assert.That(value).IsGreaterThanOrEqualTo(min);
            await Assert.That(value).IsLessThan(max);
        }
    }

    [Test]
    public async Task GetInt32_Range_Invalid_Throws()
    {
        await Assert.That(() => RandomNumberGenerator.GetInt32(10, 5)).Throws<ArgumentException>();
        await Assert.That(() => RandomNumberGenerator.GetInt32(5, 5)).Throws<ArgumentException>();
    }

    [Test]
    public async Task GetInt32_Exclusive_Valid()
    {
        var max = 10;
        for (var i = 0; i < 100; i++)
        {
            var value = RandomNumberGenerator.GetInt32(max);
            await Assert.That(value).IsGreaterThanOrEqualTo(0);
            await Assert.That(value).IsLessThan(max);
        }
    }

    [Test]
    public async Task GetInt32_Exclusive_Invalid_Throws()
    {
        await Assert.That(() => RandomNumberGenerator.GetInt32(0)).Throws<ArgumentOutOfRangeException>();
        await Assert.That(() => RandomNumberGenerator.GetInt32(-1)).Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task GetBytes_ReturnsArrayOfCorrectLength()
    {
        var bytes = RandomNumberGenerator.GetBytes(16);
        await Assert.That(bytes.Length).IsEqualTo(16);
        await Assert.That(bytes.Any(b => b != 0)).IsTrue();
    }

    [Test]
    public async Task GetBytes_Invalid_Throws() =>
        await Assert.That(() => RandomNumberGenerator.GetBytes(-1)).Throws<ArgumentOutOfRangeException>();

#if FeatureMemory
    [Test]
    public async Task Fill_FillsSpan()
    {
        Span<byte> data = new byte[8];
        RandomNumberGenerator.Fill(data);
        await Assert.That(data.ToArray().Any(b => b != 0)).IsTrue();
    }

    [Test]
    public async Task GetHexString_Span_FillsWithHex()
    {
        Span<char> chars = new char[8];
        RandomNumberGenerator.GetHexString(chars, lowercase: true);
        await Assert.That(chars.ToArray().All(c => "0123456789abcdef".Contains(c))).IsTrue();
    }
#endif

    [Test]
    public async Task GetHexString_StringLength_Valid()
    {
        var hex = RandomNumberGenerator.GetHexString(12, lowercase: false);
        await Assert.That(hex.Length).IsEqualTo(12);
        await Assert.That(hex.All(c => "0123456789ABCDEF".Contains(c))).IsTrue();
    }

    [Test]
    public async Task GetHexString_StringLength_Invalid_Throws() =>
        await Assert.That(() => RandomNumberGenerator.GetHexString(-1)).Throws<ArgumentOutOfRangeException>();

#if FeatureMemory
    [Test]
    [Arguments(1)]
    [Arguments(511)]
    [Arguments(512)]
    [Arguments(513)]
    // large enough that a stack buffer proportional to the destination would overflow the stack
    [Arguments(8 * 1024 * 1024 + 1)]
    public async Task GetHexString_Span_AnyLength(int length)
    {
        var chars = new char[length];
        RandomNumberGenerator.GetHexString(chars.AsSpan());
        await Assert.That(chars.All(c => c is (>= '0' and <= '9') or (>= 'A' and <= 'F'))).IsTrue();
        if (length > 4096)
        {
            // every hex digit shows up, at both ends of the span
            await Assert.That(chars.Take(length / 2).Distinct().Count()).IsEqualTo(16);
            await Assert.That(chars.Skip(length / 2).Distinct().Count()).IsEqualTo(16);
        }
    }
#endif

    [Test]
    public async Task GetInt32_Range_Negative()
    {
        for (var i = 0; i < 100; i++)
        {
            var value = RandomNumberGenerator.GetInt32(-10, -5);
            await Assert.That(value).IsGreaterThanOrEqualTo(-10);
            await Assert.That(value).IsLessThan(-5);
        }
    }

    [Test]
    public async Task GetInt32_Range_Full()
    {
        for (var i = 0; i < 100; i++)
        {
            var value = RandomNumberGenerator.GetInt32(int.MinValue, int.MaxValue);
            await Assert.That(value).IsLessThan(int.MaxValue);
        }
    }

    [Test]
    public async Task GetInt32_IsUniform()
    {
        // 3 is not a power of two, so values are rejected and drawn again
        var counts = new int[3];
        for (var i = 0; i < 30000; i++)
        {
            counts[RandomNumberGenerator.GetInt32(3)]++;
        }

        // 10000 expected, with a standard deviation of 82
        await Assert.That(counts.All(_ => _ is > 9000 and < 11000)).IsTrue();
    }

    [Test]
    public async Task GetInt32_Concurrent()
    {
        var tasks = new List<Task<bool>>();
        for (var t = 0; t < 8; t++)
        {
            tasks.Add(Task.Run(() =>
            {
                var seen = new HashSet<int>();
                for (var i = 0; i < 5000; i++)
                {
                    var value = RandomNumberGenerator.GetInt32(-3, 5);
                    if (value is < -3 or >= 5)
                    {
                        return false;
                    }

                    seen.Add(value);
                }

                return seen.Count == 8;
            }));
        }

        var results = await Task.WhenAll(tasks);
        await Assert.That(results.All(_ => _)).IsTrue();
    }

    [Test]
    [Arguments(0)]
    [Arguments(1)]
    [Arguments(1000)]
    public async Task GetBytes_AnyLength(int count)
    {
        var bytes = RandomNumberGenerator.GetBytes(count);
        await Assert.That(bytes.Length).IsEqualTo(count);
        if (count >= 1000)
        {
            await Assert.That(bytes.Distinct().Count()).IsGreaterThan(200);
        }
    }

    [Test]
    public async Task GetBytes_DiffersBetweenCalls()
    {
        var first = RandomNumberGenerator.GetBytes(32);
        var second = RandomNumberGenerator.GetBytes(32);
        await Assert.That(first.SequenceEqual(second)).IsFalse();
    }

    [Test]
    [Arguments(0)]
    [Arguments(1)]
    [Arguments(2)]
    [Arguments(255)]
    [Arguments(256)]
    [Arguments(257)]
    [Arguments(1001)]
    public async Task GetHexString_AnyLength(int length)
    {
        var upper = RandomNumberGenerator.GetHexString(length);
        var lower = RandomNumberGenerator.GetHexString(length, lowercase: true);
        await Assert.That(upper.Length).IsEqualTo(length);
        await Assert.That(lower.Length).IsEqualTo(length);
        await Assert.That(upper.All(_ => _ is (>= '0' and <= '9') or (>= 'A' and <= 'F'))).IsTrue();
        await Assert.That(lower.All(_ => _ is (>= '0' and <= '9') or (>= 'a' and <= 'f'))).IsTrue();
        if (length >= 255)
        {
            await Assert.That(upper.Distinct().Count()).IsEqualTo(16);
            await Assert.That(upper).IsNotEqualTo(RandomNumberGenerator.GetHexString(length));
        }
    }

#if FeatureMemory
    [Test]
    [Arguments(0)]
    [Arguments(1)]
    [Arguments(31)]
    [Arguments(4095)]
    [Arguments(4096)]
    [Arguments(4097)]
    [Arguments(100_000)]
    public async Task Fill_AnyLength(int length)
    {
        // a margin either side, to check nothing outside the span is written
        var data = new byte[length + 32];
        RandomNumberGenerator.Fill(data.AsSpan(16, length));

        await Assert.That(data.Take(16).All(_ => _ == 0)).IsTrue();
        await Assert.That(data.Skip(16 + length).All(_ => _ == 0)).IsTrue();

        if (length >= 31)
        {
            // every part of the span is filled, including the tail after the last full chunk
            await Assert.That(data.Skip(16).Take(31).Any(_ => _ != 0)).IsTrue();
            await Assert.That(data.Skip(16 + length - 31).Take(31).Any(_ => _ != 0)).IsTrue();
        }

        if (length >= 4095)
        {
            await Assert.That(data.Skip(16).Take(length).Distinct().Count()).IsGreaterThan(200);

            // no run of 64 bytes is left unfilled
            var zeroRun = 0;
            var longestZeroRun = 0;
            for (var i = 16; i < 16 + length; i++)
            {
                if (data[i] == 0)
                {
                    zeroRun++;
                    longestZeroRun = Math.Max(longestZeroRun, zeroRun);
                    continue;
                }

                zeroRun = 0;
            }

            await Assert.That(longestZeroRun).IsLessThan(8);
        }
    }

    [Test]
    public async Task Fill_Concurrent()
    {
        var tasks = new List<Task<bool>>();
        for (var t = 0; t < 8; t++)
        {
            tasks.Add(Task.Run(() =>
            {
                var data = new byte[64];
                var previous = new byte[64];
                for (var i = 0; i < 2000; i++)
                {
                    RandomNumberGenerator.Fill(data);
                    if (data.AsSpan().SequenceEqual(previous))
                    {
                        return false;
                    }

                    data.CopyTo(previous, 0);
                }

                return true;
            }));
        }

        var results = await Task.WhenAll(tasks);
        await Assert.That(results.All(_ => _)).IsTrue();
    }

    [Test]
    [Arguments(1)]
    [Arguments(2)]
    [Arguments(3)]
    [Arguments(62)]
    [Arguments(256)]
    [Arguments(300)]
    public async Task GetItems_IsUniform(int choiceCount)
    {
        var choices = Enumerable.Range(1000, choiceCount).ToArray();
        var perChoice = 400;
        var items = new int[choiceCount * perChoice];
        RandomNumberGenerator.GetItems<int>(choices, items);

        var counts = new int[choiceCount];
        foreach (var item in items)
        {
            counts[item - 1000]++;
        }

        if (choiceCount == 1)
        {
            await Assert.That(counts[0]).IsEqualTo(perChoice);
            return;
        }

        // 400 expected, with a standard deviation of at most 20
        await Assert.That(counts.All(_ => _ is > 250 and < 550)).IsTrue();
    }

    [Test]
    public async Task GetItems_Lengths()
    {
        char[] choices = ['a', 'b', 'c'];

        RandomNumberGenerator.GetItems<char>(choices, Span<char>.Empty);

        foreach (var length in new[] {0, 1, 63, 64, 65, 1000})
        {
            var items = RandomNumberGenerator.GetItems<char>(choices, length);
            await Assert.That(items.Length).IsEqualTo(length);
            await Assert.That(items.All(_ => _ is 'a' or 'b' or 'c')).IsTrue();
            if (length >= 63)
            {
                await Assert.That(items.Distinct().Count()).IsEqualTo(3);
            }
        }
    }

    [Test]
    public async Task GetItems_EmptyChoices_Throws()
    {
        await Assert.That(() => RandomNumberGenerator.GetItems(ReadOnlySpan<int>.Empty, new int[4])).Throws<ArgumentException>();
        await Assert.That(() => RandomNumberGenerator.GetItems(ReadOnlySpan<int>.Empty, 4)).Throws<ArgumentException>();
        await Assert.That(() => RandomNumberGenerator.GetString(ReadOnlySpan<char>.Empty, 4)).Throws<ArgumentException>();
    }

    [Test]
    [Arguments(0)]
    [Arguments(1)]
    [Arguments(32)]
    [Arguments(256)]
    [Arguments(257)]
    [Arguments(5000)]
    public async Task GetString_AnyLength(int length)
    {
        const string choices = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var value = RandomNumberGenerator.GetString(choices.AsSpan(), length);
        await Assert.That(value.Length).IsEqualTo(length);
        await Assert.That(value.All(_ => choices.Contains(_))).IsTrue();
        if (length >= 32)
        {
            await Assert.That(value).IsNotEqualTo(RandomNumberGenerator.GetString(choices.AsSpan(), length));
        }

        if (length >= 5000)
        {
            await Assert.That(value.Distinct().Count()).IsEqualTo(choices.Length);
        }
    }

    [Test]
    [Arguments(0)]
    [Arguments(1)]
    [Arguments(2)]
    [Arguments(63)]
    [Arguments(64)]
    [Arguments(65)]
    [Arguments(2000)]
    public async Task Shuffle_IsPermutation(int length)
    {
        var values = Enumerable.Range(0, length).ToArray();
        RandomNumberGenerator.Shuffle<int>(values);

        await Assert.That(values.OrderBy(_ => _).SequenceEqual(Enumerable.Range(0, length))).IsTrue();
        if (length >= 63)
        {
            await Assert.That(values.SequenceEqual(Enumerable.Range(0, length))).IsFalse();
        }
    }

    [Test]
    public async Task Shuffle_IsUniform()
    {
        var counts = new Dictionary<string, int>();
        var values = new char[3];
        for (var i = 0; i < 6000; i++)
        {
            values[0] = 'a';
            values[1] = 'b';
            values[2] = 'c';
            RandomNumberGenerator.Shuffle<char>(values);
            var key = new string(values);
            counts.TryGetValue(key, out var count);
            counts[key] = count + 1;
        }

        // 1000 expected for each of the 6 orders, with a standard deviation of 29
        await Assert.That(counts.Count).IsEqualTo(6);
        await Assert.That(counts.Values.All(_ => _ is > 800 and < 1200)).IsTrue();
    }
#endif
}
