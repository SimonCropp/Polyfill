#if FeatureMemory

#if NET8_0_OR_GREATER
using MemoryExtensions = System.MemoryExtensions;
#else
using MemoryExtensions = Polyfills.Polyfill;
#endif

partial class PolyfillTests
{
    [Test]
    public async Task Split_Char_Basic()
    {
        var ranges = new Range[8];
        var count = MemoryExtensions.Split("a,b,c".AsSpan(), ranges, ',');
        await Assert.That(count).IsEqualTo(3);
        await Assert.That(SliceOf("a,b,c", ranges, count)).IsEquivalentTo(new[] {"a", "b", "c"});
    }

    [Test]
    public async Task Split_Char_RemoveEmpty()
    {
        var ranges = new Range[8];
        var count = MemoryExtensions.Split(",a,,b,".AsSpan(), ranges, ',', StringSplitOptions.RemoveEmptyEntries);
        await Assert.That(count).IsEqualTo(2);
        await Assert.That(SliceOf(",a,,b,", ranges, count)).IsEquivalentTo(new[] {"a", "b"});
    }

    [Test]
    public async Task Split_Char_TrimEntries()
    {
        var ranges = new Range[8];
        var count = MemoryExtensions.Split("  a , b , c ".AsSpan(), ranges, ',', (StringSplitOptions)2);
        await Assert.That(count).IsEqualTo(3);
        await Assert.That(SliceOf("  a , b , c ", ranges, count)).IsEquivalentTo(new[] {"a", "b", "c"});
    }

    [Test]
    public async Task Split_Char_TrimAndRemoveEmpty()
    {
        var ranges = new Range[8];
        const StringSplitOptions opts = StringSplitOptions.RemoveEmptyEntries | (StringSplitOptions)2;
        var count = MemoryExtensions.Split(" , a , , b , ".AsSpan(), ranges, ',', opts);
        await Assert.That(count).IsEqualTo(2);
        await Assert.That(SliceOf(" , a , , b , ", ranges, count)).IsEquivalentTo(new[] {"a", "b"});
    }

    [Test]
    public async Task Split_Char_LastSlotGetsRemainder()
    {
        var ranges = new Range[2];
        var count = MemoryExtensions.Split("a,b,c,d".AsSpan(), ranges, ',');
        await Assert.That(count).IsEqualTo(2);
        await Assert.That(SliceOf("a,b,c,d", ranges, count)).IsEquivalentTo(new[] {"a", "b,c,d"});
    }

    [Test]
    public async Task Split_Char_EmptySource()
    {
        var ranges = new Range[4];
        var count = MemoryExtensions.Split("".AsSpan(), ranges, ',');
        await Assert.That(count).IsEqualTo(1);
        await Assert.That(ranges[0].Start.Value).IsEqualTo(0);
        await Assert.That(ranges[0].End.Value).IsEqualTo(0);
    }

    [Test]
    public async Task Split_Char_EmptySource_RemoveEmpty()
    {
        var ranges = new Range[4];
        var count = MemoryExtensions.Split("".AsSpan(), ranges, ',', StringSplitOptions.RemoveEmptyEntries);
        await Assert.That(count).IsEqualTo(0);
    }

    [Test]
    public async Task Split_Char_EmptyDestination()
    {
        var count = MemoryExtensions.Split("a,b,c".AsSpan(), Span<Range>.Empty, ',');
        await Assert.That(count).IsEqualTo(0);
    }

    [Test]
    public async Task Split_Char_SingleSlot()
    {
        var ranges = new Range[1];
        var count = MemoryExtensions.Split("a,b,c".AsSpan(), ranges, ',');
        await Assert.That(count).IsEqualTo(1);
        await Assert.That(SliceOf("a,b,c", ranges, count)).IsEquivalentTo(new[] {"a,b,c"});
    }

    [Test]
    public async Task Split_Sequence()
    {
        var ranges = new Range[8];
        var count = MemoryExtensions.Split("a<>b<>c".AsSpan(), ranges, "<>".AsSpan());
        await Assert.That(count).IsEqualTo(3);
        await Assert.That(SliceOf("a<>b<>c", ranges, count)).IsEquivalentTo(new[] {"a", "b", "c"});
    }

    [Test]
    public async Task Split_Sequence_Empty()
    {
        var ranges = new Range[8];
        var count = MemoryExtensions.Split("abc".AsSpan(), ranges, ReadOnlySpan<char>.Empty);
        await Assert.That(count).IsEqualTo(1);
        await Assert.That(SliceOf("abc", ranges, count)).IsEquivalentTo(new[] {"abc"});
    }

    [Test]
    public async Task Split_Sequence_Empty_RemoveEmpty_EmptySource()
    {
        var ranges = new Range[8];
        var count = MemoryExtensions.Split("".AsSpan(), ranges, ReadOnlySpan<char>.Empty, StringSplitOptions.RemoveEmptyEntries);
        await Assert.That(count).IsEqualTo(0);
    }

    [Test]
    public async Task SplitAny_Chars()
    {
        var ranges = new Range[8];
        var count = MemoryExtensions.SplitAny("a,b;c.d".AsSpan(), ranges, ",;.".AsSpan());
        await Assert.That(count).IsEqualTo(4);
        await Assert.That(SliceOf("a,b;c.d", ranges, count)).IsEquivalentTo(new[] {"a", "b", "c", "d"});
    }

    [Test]
    public async Task SplitAny_Chars_EmptySeparators_UsesWhitespace()
    {
        var ranges = new Range[8];
        var count = MemoryExtensions.SplitAny("a b\tc\nd".AsSpan(), ranges, ReadOnlySpan<char>.Empty);
        await Assert.That(count).IsEqualTo(4);
        await Assert.That(SliceOf("a b\tc\nd", ranges, count)).IsEquivalentTo(new[] {"a", "b", "c", "d"});
    }

    [Test]
    public async Task SplitAny_Chars_RemoveEmpty()
    {
        var ranges = new Range[8];
        var count = MemoryExtensions.SplitAny(",a;,b.".AsSpan(), ranges, ",;.".AsSpan(), StringSplitOptions.RemoveEmptyEntries);
        await Assert.That(count).IsEqualTo(2);
        await Assert.That(SliceOf(",a;,b.", ranges, count)).IsEquivalentTo(new[] {"a", "b"});
    }

    [Test]
    public async Task SplitAny_Strings()
    {
        var ranges = new Range[8];
        string[] separators = ["::", "->"];
        var count = MemoryExtensions.SplitAny("a::b->c::d".AsSpan(), ranges, separators);
        await Assert.That(count).IsEqualTo(4);
        await Assert.That(SliceOf("a::b->c::d", ranges, count)).IsEquivalentTo(new[] {"a", "b", "c", "d"});
    }

    [Test]
    public async Task SplitAny_Strings_VariableLength()
    {
        var ranges = new Range[8];
        string[] separators = ["--", "+"];
        var count = MemoryExtensions.SplitAny("a--b+c--d".AsSpan(), ranges, separators);
        await Assert.That(count).IsEqualTo(4);
        await Assert.That(SliceOf("a--b+c--d", ranges, count)).IsEquivalentTo(new[] {"a", "b", "c", "d"});
    }

    [Test]
    public async Task SplitAny_Strings_EmptySeparators_UsesWhitespace()
    {
        var ranges = new Range[8];
        var count = MemoryExtensions.SplitAny("a b\tc".AsSpan(), ranges, ReadOnlySpan<string>.Empty);
        await Assert.That(count).IsEqualTo(3);
        await Assert.That(SliceOf("a b\tc", ranges, count)).IsEquivalentTo(new[] {"a", "b", "c"});
    }

    [Test]
    public async Task Split_Char_TruncatedDestinationRemoveEmpty()
    {
        // Last slot should hold remainder; intermediate empties skipped.
        var ranges = new Range[2];
        var count = MemoryExtensions.Split(",,a,,b,,c".AsSpan(), ranges, ',', StringSplitOptions.RemoveEmptyEntries);
        await Assert.That(count).IsEqualTo(2);
        await Assert.That(SliceOf(",,a,,b,,c", ranges, count)).IsEquivalentTo(new[] {"a", "b,,c"});
    }

    [Test]
    public async Task SplitAny_Chars_SeparatorCounts_MatchStringSplit()
    {
        string[] inputs =
        [
            "a",
            ",",
            "/",
            "abc",
            "a,b;c|d-e+f/g",
            "a/b/c/d",
            "/a/b/",
            ",;|-+/",
            "//,,;;",
            "a,,b;;c//d",
            "no separators here",
            "trailing/",
            ",leading",
        ];
        const string allSeparators = ",;|-+/";
        var failures = new List<string>();

        // 1 to 3 separators take the fixed arity path below net7, 4 or more are matched a char at a time
        for (var separatorCount = 1; separatorCount <= allSeparators.Length; separatorCount++)
        {
            char[][] separatorSets =
            [
                allSeparators.Substring(0, separatorCount).ToCharArray(),
                allSeparators.Substring(allSeparators.Length - separatorCount).ToCharArray()
            ];
            foreach (var separators in separatorSets)
            {
                foreach (var input in inputs)
                {
                    foreach (var options in new[] {StringSplitOptions.None, StringSplitOptions.RemoveEmptyEntries})
                    {
                        var expected = input.Split(separators, options);
                        var ranges = new Range[input.Length + 2];
                        var count = MemoryExtensions.SplitAny(input.AsSpan(), ranges, separators, options);
                        var actual = SliceOf(input, ranges, count);
                        if (string.Join("\n", actual) != string.Join("\n", expected) ||
                            count != expected.Length)
                        {
                            failures.Add($"'{input}' split on '{new string(separators)}' ({options})");
                        }
                    }
                }
            }
        }

        await Assert.That(failures).IsEmpty();
    }

    [Test]
    public async Task SplitAny_Chars_LastSlotGetsRemainder()
    {
        var ranges = new Range[3];
        var count = MemoryExtensions.SplitAny("a,b;c|d-e".AsSpan(), ranges, ",;|-".AsSpan());
        await Assert.That(count).IsEqualTo(3);
        await Assert.That(SliceOf("a,b;c|d-e", ranges, count)).IsEquivalentTo(new[] {"a", "b", "c|d-e"});
    }

    [Test]
    public async Task SplitAny_Chars_ManyWords()
    {
        var builder = new StringBuilder();
        for (var index = 0; index < 4000; index++)
        {
            builder.Append("word").Append(index).Append(' ');
        }

        var input = builder.ToString();
        var ranges = new Range[5000];

        var twoSeparators = MemoryExtensions.SplitAny(input.AsSpan(), ranges, "\t ".AsSpan());
        var first = input[ranges[0]];
        var last = input[ranges[3999]];
        var fiveSeparators = MemoryExtensions.SplitAny(input.AsSpan(), ranges, "\t;,| ".AsSpan());

        await Assert.That(twoSeparators).IsEqualTo(4001);
        await Assert.That(fiveSeparators).IsEqualTo(4001);
        await Assert.That(first).IsEqualTo("word0");
        await Assert.That(last).IsEqualTo("word3999");
    }

    static string[] SliceOf(string source, Range[] ranges, int count)
    {
        var result = new string[count];
        for (var i = 0; i < count; i++)
        {
            result[i] = source[ranges[i]];
        }

        return result;
    }
}

#endif
