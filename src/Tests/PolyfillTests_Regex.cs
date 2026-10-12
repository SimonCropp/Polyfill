partial class PolyfillTests
{
    [Test]
    public async Task RegexCount()
    {
        var regex = new Regex(@"\d+");
        await Assert.That(regex.Count("a1b2c3")).IsEqualTo(3);
        await Assert.That(regex.Count("abc")).IsEqualTo(0);
    }

    [Test]
    public async Task RegexCountStatic()
    {
        await Assert.That(Regex.Count("a1b2c3", @"\d+")).IsEqualTo(3);
        await Assert.That(Regex.Count("ABC", "[a-z]+", RegexOptions.IgnoreCase)).IsEqualTo(1);
        await Assert.That(Regex.Count("a1b2", @"\d+", RegexOptions.None, TimeSpan.FromSeconds(10))).IsEqualTo(2);
    }

    [Test]
    public async Task RegexIsMatch()
    {
        var regex = new Regex(@"\d+");
        var match = regex.Match("a55a");
        await Assert.That(match.Success).IsTrue();
    }

    [Test]
    public Task EnumerateMatches()
    {
        var regex = new Regex(@"\d+");
        var span = "a55a".AsSpan();
        var found = false;
        foreach (var match in regex.EnumerateMatches(span))
        {
            found = true;
            if (match.Index != 1)
            {
                throw new($"Expected Index 1 but got {match.Index}");
            }

            if (match.Length != 2)
            {
                throw new($"Expected Length 2 but got {match.Length}");
            }
        }

        if (!found)
        {
            throw new("Expected to find a match");
        }

        return Task.CompletedTask;
    }

    static string DescribeMatches(MatchCollection matches)
    {
        var results = new List<string>();
        for (var index = 0; index < matches.Count; index++)
        {
            var match = matches[index];
            results.Add($"{match.Index}:{match.Length}");
        }

        return string.Join(" ", results);
    }

    [Test]
    public async Task EnumerateMatches_SameAsMatches()
    {
        (string pattern, RegexOptions options)[] patterns =
        [
            (@"\d+", RegexOptions.None),
            (@"\w+", RegexOptions.None),
            // empty matches, which have to advance the scan by one char
            ("a*", RegexOptions.None),
            ("", RegexOptions.None),
            (@"\b", RegexOptions.None),
            ("$", RegexOptions.None),
            ("^", RegexOptions.Multiline),
            // anchors and lookbehind see the text before startat
            ("^a", RegexOptions.None),
            (@"\Ga", RegexOptions.None),
            ("(?<=a)b", RegexOptions.None),
            ("(?<!a)b", RegexOptions.None),
            ("ab|b", RegexOptions.IgnoreCase),
            (@"\d+", RegexOptions.RightToLeft),
            ("a*", RegexOptions.RightToLeft),
            ("", RegexOptions.RightToLeft),
            ("zzz", RegexOptions.None),
        ];
        string[] inputs =
        [
            "",
            "a",
            "a55a",
            "12 345 6",
            "aaa",
            "abab ab\nab aab bab",
            "no digits here",
            "AB aB b",
        ];
        var failures = new List<string>();

        foreach (var (pattern, options) in patterns)
        {
            var regex = new Regex(pattern, options);
            foreach (var input in inputs)
            {
                var results = new List<string>();
                foreach (var match in regex.EnumerateMatches(input.AsSpan()))
                {
                    results.Add($"{match.Index}:{match.Length}");
                }

                var expected = DescribeMatches(regex.Matches(input));
                if (string.Join(" ", results) != expected)
                {
                    failures.Add($"'{pattern}' ({options}) on '{input}': '{string.Join(" ", results)}' expected '{expected}'");
                }

                for (var startAt = 0; startAt <= input.Length; startAt++)
                {
                    results.Clear();
                    foreach (var match in regex.EnumerateMatches(input.AsSpan(), startAt))
                    {
                        results.Add($"{match.Index}:{match.Length}");
                    }

                    expected = DescribeMatches(regex.Matches(input, startAt));
                    if (string.Join(" ", results) != expected)
                    {
                        failures.Add($"'{pattern}' ({options}) on '{input}' from {startAt}: '{string.Join(" ", results)}' expected '{expected}'");
                    }
                }
            }
        }

        await Assert.That(failures).IsEmpty();
    }

    [Test]
    public async Task EnumerateMatches_FindsMatchesLazily()
    {
        // The second match attempt backtracks catastrophically on runtimes without a non-backtracking
        // optimization for it, and would hit the timeout. Reading only the first match must not run it.
        var regex = new Regex("ab|(a+)+$", RegexOptions.None, TimeSpan.FromSeconds(5));
        var input = "ab " + new string('a', 60) + "!";

        var enumerator = regex.EnumerateMatches(input.AsSpan());
        var moved = enumerator.MoveNext();
        var index = enumerator.Current.Index;
        var length = enumerator.Current.Length;

        await Assert.That(moved).IsTrue();
        await Assert.That(index).IsEqualTo(0);
        await Assert.That(length).IsEqualTo(2);
    }

    [Test]
    public async Task EnumerateMatches_MoveNextAfterEnd()
    {
        var regex = new Regex(@"\d");
        var enumerator = regex.EnumerateMatches("a1b2".AsSpan());

        var first = enumerator.MoveNext();
        var firstIndex = enumerator.Current.Index;
        var second = enumerator.MoveNext();
        var secondIndex = enumerator.Current.Index;
        var third = enumerator.MoveNext();
        var fourth = enumerator.MoveNext();

        await Assert.That(first).IsTrue();
        await Assert.That(firstIndex).IsEqualTo(1);
        await Assert.That(second).IsTrue();
        await Assert.That(secondIndex).IsEqualTo(3);
        await Assert.That(third).IsFalse();
        await Assert.That(fourth).IsFalse();
    }

    [Test]
    public async Task EnumerateMatches_NoMatch()
    {
        var regex = new Regex(@"\d");
        var enumerator = regex.EnumerateMatches("abc".AsSpan());

        var first = enumerator.MoveNext();
        var second = enumerator.MoveNext();

        await Assert.That(first).IsFalse();
        await Assert.That(second).IsFalse();
    }

    [Test]
    public async Task EnumerateMatches_InvalidStartAt()
    {
        var regex = new Regex(@"\d");

        await Assert.That(() =>
        {
            foreach (var match in regex.EnumerateMatches("abc".AsSpan(), -1))
            {
            }
        }).Throws<ArgumentOutOfRangeException>();

        // Past the end the BCL (net7 and up) yields no matches, where the polyfill has always thrown.
        var count = 0;
        var threw = false;
        try
        {
            foreach (var match in regex.EnumerateMatches("ab1".AsSpan(), 4))
            {
                count++;
            }
        }
        catch (ArgumentOutOfRangeException)
        {
            threw = true;
        }

        await Assert.That(count).IsEqualTo(0);
        if (Environment.Version.Major < 7)
        {
            await Assert.That(threw).IsTrue();
        }
    }

    [Test]
    public async Task EnumerateMatchesStatic_WithOptions()
    {
        var results = new List<string>();
        foreach (var match in Regex.EnumerateMatches("a55B7b".AsSpan(), @"b\d*", RegexOptions.IgnoreCase))
        {
            results.Add($"{match.Index}:{match.Length}");
        }

        await Assert.That(string.Join(" ", results)).IsEqualTo("3:2 5:1");
    }

    [Test]
    public async Task EnumerateMatchesStatic_WithOptionsAndTimeout()
    {
        var results = new List<string>();
        foreach (var match in Regex.EnumerateMatches("a55B7b".AsSpan(), @"b\d*", RegexOptions.IgnoreCase, TimeSpan.FromSeconds(10)))
        {
            results.Add($"{match.Index}:{match.Length}");
        }

        await Assert.That(string.Join(" ", results)).IsEqualTo("3:2 5:1");
    }

    [Test]
    public Task EnumerateMatchesStatic()
    {
        var span = "a55a".AsSpan();
        var found = false;
        foreach (var match in Regex.EnumerateMatches(span, @"\d+"))
        {
            found = true;
            if (match.Index != 1)
            {
                throw new($"Expected Index 1 but got {match.Index}");
            }

            if (match.Length != 2)
            {
                throw new($"Expected Length 2 but got {match.Length}");
            }
        }

        if (!found)
        {
            throw new("Expected to find a match");
        }

        return Task.CompletedTask;
    }

#if FeatureMemory

    [Test]
    public async Task RegexCountSpan()
    {
        var regex = new Regex(@"\d+");
        await Assert.That(regex.Count("a1b2c3".AsSpan())).IsEqualTo(3);
        await Assert.That(regex.Count("a1b2c3".AsSpan(), 2)).IsEqualTo(2);
    }

    [Test]
    public async Task RegexCountSpanStatic()
    {
        await Assert.That(Regex.Count("a1b2c3".AsSpan(), @"\d+")).IsEqualTo(3);
        await Assert.That(Regex.Count("ABC".AsSpan(), "[a-z]+", RegexOptions.IgnoreCase)).IsEqualTo(1);
        await Assert.That(Regex.Count("a1b2".AsSpan(), @"\d+", RegexOptions.None, TimeSpan.FromSeconds(10))).IsEqualTo(2);
    }

    [Test]
    public async Task CaptureValueSpan()
    {
        var match = Regex.Match("hello 42 world", @"\d+");
        await Assert.That(match.Success).IsTrue();
        await Assert.That(match.ValueSpan.SequenceEqual("42".AsSpan())).IsTrue();
    }

    static string SplitRanges(Regex regex, string input)
    {
        var results = new List<string>();
        foreach (var range in regex.EnumerateSplits(input.AsSpan()))
        {
            results.Add(range.ToString());
        }

        return string.Join(" ", results);
    }

    static string SplitRanges(Regex regex, string input, int count)
    {
        var results = new List<string>();
        foreach (var range in regex.EnumerateSplits(input.AsSpan(), count))
        {
            results.Add(range.ToString());
        }

        return string.Join(" ", results);
    }

    static string SplitRanges(Regex regex, string input, int count, int startat)
    {
        var results = new List<string>();
        foreach (var range in regex.EnumerateSplits(input.AsSpan(), count, startat))
        {
            results.Add(range.ToString());
        }

        return string.Join(" ", results);
    }

    [Test]
    public async Task EnumerateSplits_CaptureGroups_NotIncluded()
    {
        await Assert.That(SplitRanges(new("(-)"), "a-b")).IsEqualTo("0..1 2..3");
        await Assert.That(SplitRanges(new(@"(\d)"), "a1b2c")).IsEqualTo("0..1 2..3 4..5");
        await Assert.That(SplitRanges(new(@"(\d)", RegexOptions.RightToLeft), "a1b2c")).IsEqualTo("4..5 2..3 0..1");
    }

    [Test]
    public async Task EnumerateSplits_RightToLeft()
    {
        var regex = new Regex(",", RegexOptions.RightToLeft);
        await Assert.That(SplitRanges(regex, "a,b,c")).IsEqualTo("4..5 2..3 0..1");
        await Assert.That(SplitRanges(regex, "a,b,c", 2)).IsEqualTo("4..5 0..3");
        await Assert.That(SplitRanges(regex, "a,b,c", 0, 3)).IsEqualTo("2..5 0..1");
        await Assert.That(SplitRanges(regex, "a,b,c", 0, 0)).IsEqualTo("0..5");
        await Assert.That(SplitRanges(regex, "a,b,c", 2, 5)).IsEqualTo("4..5 0..3");

        var results = new List<string>();
        foreach (var range in Regex.EnumerateSplits("a,b,c".AsSpan(), ",", RegexOptions.RightToLeft))
        {
            results.Add(range.ToString());
        }

        await Assert.That(string.Join(" ", results)).IsEqualTo("4..5 2..3 0..1");
    }

    [Test]
    public async Task EnumerateSplits_CountAndStartat_Ranges()
    {
        var regex = new Regex(",");
        await Assert.That(SplitRanges(regex, "a,b,c,d", 2)).IsEqualTo("0..1 2..7");
        await Assert.That(SplitRanges(regex, "a,b,c,d", 3)).IsEqualTo("0..1 2..3 4..7");
        await Assert.That(SplitRanges(regex, "a,b,c,d", 0, 2)).IsEqualTo("0..3 4..5 6..7");
        await Assert.That(SplitRanges(regex, "a,b,c,d", 0, 7)).IsEqualTo("0..7");
        await Assert.That(SplitRanges(regex, "a,b,c,d", 2, 2)).IsEqualTo("0..3 4..7");
        await Assert.That(SplitRanges(regex, "a,b,c,d", 2, 4)).IsEqualTo("0..5 6..7");
    }

    [Test]
    public async Task EnumerateSplits_EmptyMatches()
    {
        await Assert.That(SplitRanges(new(""), "abc")).IsEqualTo("0..0 0..1 1..2 2..3 3..3");
        await Assert.That(SplitRanges(new("", RegexOptions.RightToLeft), "abc")).IsEqualTo("3..3 2..3 1..2 0..1 0..0");
        await Assert.That(SplitRanges(new(""), "abc", 3)).IsEqualTo("0..0 0..1 1..3");
        await Assert.That(SplitRanges(new("(?<=,)"), "a,b,c,d")).IsEqualTo("0..2 2..4 4..6 6..7");
        await Assert.That(SplitRanges(new(""), "")).IsEqualTo("0..0 0..0");
        await Assert.That(SplitRanges(new(@"\b"), "ab")).IsEqualTo("0..0 0..2 2..2");
    }

    [Test]
    public async Task EnumerateSplits_InstanceMethod_BasicSplit()
    {
        var regex = new Regex(@"[,\s]+");
        var input = "one, two  three,four".AsSpan();

        var results = new List<string>();
        foreach (var range in regex.EnumerateSplits(input))
        {
            results.Add(input[range].ToString());
        }

        await Assert.That(results.SequenceEqual(["one", "two", "three", "four"])).IsTrue();
    }

    [Test]
    public async Task EnumerateSplits_InstanceMethod_WithCount()
    {
        var regex = new Regex(@"[,\s]+");
        var input = "one, two  three,four".AsSpan();

        var results = new List<string>();
        foreach (var range in regex.EnumerateSplits(input, 2))
        {
            results.Add(input[range].ToString());
        }

        await Assert.That(results.Count).IsEqualTo(2);
    }

    [Test]
    public async Task EnumerateSplits_InstanceMethod_WithCountAndStartat()
    {
        var regex = new Regex(@"[,\s]+");
        var input = "one, two  three,four".AsSpan();

        var results = new List<string>();
        foreach (var range in regex.EnumerateSplits(input, 2, 5))
        {
            results.Add(input[range].ToString());
        }

        await Assert.That(results.Count).IsEqualTo(2);
    }

    [Test]
    public async Task EnumerateSplits_InstanceMethod_EmptyInput()
    {
        var regex = new Regex(",");
        var input = "".AsSpan();

        var results = new List<string>();
        foreach (var range in regex.EnumerateSplits(input))
        {
            results.Add(input[range].ToString());
        }

        await Assert.That(results.Count).IsEqualTo(1);
        await Assert.That(results[0]).IsEqualTo("");
    }

    [Test]
    public async Task EnumerateSplits_InstanceMethod_NoMatch()
    {
        var regex = new Regex("xyz");
        var input = "hello world".AsSpan();

        var results = new List<string>();
        foreach (var range in regex.EnumerateSplits(input))
        {
            results.Add(input[range].ToString());
        }

        await Assert.That(results.Count).IsEqualTo(1);
        await Assert.That(results[0]).IsEqualTo("hello world");
    }

    [Test]
    public async Task EnumerateSplits_InstanceMethod_NegativeCount_ThrowsArgumentOutOfRangeException()
    {
        var regex = new Regex(",");

        await Assert.That(() =>
        {
            var input = "test".AsSpan();
            foreach (var _ in regex.EnumerateSplits(input, -1))
            {
            }
        }).Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task EnumerateSplits_InstanceMethod_InvalidStartat_ThrowsArgumentOutOfRangeException()
    {
        var regex = new Regex(",");

        await Assert.That(() =>
        {
            var input = "test".AsSpan();
            foreach (var _ in regex.EnumerateSplits(input, 0, 100))
            {
            }
        }).Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task EnumerateSplits_StaticMethod_BasicSplit()
    {
        var input = "one,two,three".AsSpan();

        var results = new List<string>();
        foreach (var range in Regex.EnumerateSplits(input, ","))
        {
            results.Add(input[range].ToString());
        }

        await Assert.That(results.SequenceEqual(["one", "two", "three"])).IsTrue();
    }

    [Test]
    public async Task EnumerateSplits_StaticMethod_WithOptions()
    {
        var input = "ONE,two,THREE".AsSpan();

        var results = new List<string>();
        foreach (var range in Regex.EnumerateSplits(input, "[ot]", RegexOptions.IgnoreCase))
        {
            results.Add(input[range].ToString());
        }

        await Assert.That(results.Count).IsGreaterThan(2);
    }

    [Test]
    public async Task EnumerateSplits_StaticMethod_WithTimeout()
    {
        var input = "one,two,three".AsSpan();

        var results = new List<string>();
        foreach (var range in Regex.EnumerateSplits(input, ",", RegexOptions.None, TimeSpan.FromSeconds(1)))
        {
            results.Add(input[range].ToString());
        }

        await Assert.That(results.SequenceEqual(new[]
        {
            "one",
            "two",
            "three"
        })).IsTrue();
    }

    [Test]
    public async Task EnumerateSplits_Digits_SplitCorrectly()
    {
        var input = "abc123def456ghi".AsSpan();

        var results = new List<string>();
        foreach (var range in Regex.EnumerateSplits(input, @"\d+"))
        {
            results.Add(input[range].ToString());
        }

        await Assert.That(results.SequenceEqual(new[]
        {
            "abc",
            "def",
            "ghi"
        })).IsTrue();
    }

    [Test]
    public async Task EnumerateSplits_Whitespace_SplitCorrectly()
    {
        var input = "Hello   World  Test".AsSpan();

        var results = new List<string>();
        foreach (var range in Regex.EnumerateSplits(input, @"\s+"))
        {
            results.Add(input[range].ToString());
        }

        await Assert.That(results.SequenceEqual(new[]
        {
            "Hello",
            "World",
            "Test"
        })).IsTrue();
    }

    [Test]
    public async Task EnumerateSplits_ConsecutiveMatches_ProducesEmptyStrings()
    {
        var input = "a,,b".AsSpan();

        var results = new List<string>();
        foreach (var range in Regex.EnumerateSplits(input, ","))
        {
            results.Add(input[range].ToString());
        }

        await Assert.That(results.SequenceEqual(new[]
        {
            "a",
            "",
            "b"
        })).IsTrue();
    }

    [Test]
    public async Task EnumerateSplits_MatchAtBeginning()
    {
        var input = ",a,b".AsSpan();

        var results = new List<string>();
        foreach (var range in Regex.EnumerateSplits(input, ","))
        {
            results.Add(input[range].ToString());
        }

        await Assert.That(results.SequenceEqual(new[]
        {
            "",
            "a",
            "b"
        })).IsTrue();
    }

    [Test]
    public async Task EnumerateSplits_MatchAtEnd()
    {
        var input = "a,b,".AsSpan();

        var results = new List<string>();
        foreach (var range in Regex.EnumerateSplits(input, ","))
        {
            results.Add(input[range].ToString());
        }

        await Assert.That(results.SequenceEqual(new[]
        {
            "a",
            "b",
            ""
        })).IsTrue();
    }

    [Test]
    public async Task EnumerateSplits_ComplexPattern()
    {
        var input = "user@example.com;admin@test.org;guest@domain.net".AsSpan();

        var results = new List<string>();
        foreach (var range in Regex.EnumerateSplits(input, ";"))
        {
            results.Add(input[range].ToString());
        }

        await Assert.That(results.SequenceEqual(new[]
        {
            "user@example.com",
            "admin@test.org",
            "guest@domain.net"
        })).IsTrue();
    }

    [Test]
    public async Task EnumerateSplits_ZeroCount_ReturnsAllSplits()
    {
        var regex = new Regex(",");
        var input = "a,b,c,d".AsSpan();

        var results = new List<string>();
        foreach (var range in regex.EnumerateSplits(input, 0))
        {
            results.Add(input[range].ToString());
        }

        await Assert.That(results.SequenceEqual(new[]
        {
            "a",
            "b",
            "c",
            "d"
        })).IsTrue();
    }

    [Test]
    public async Task EnumerateSplits_MultiCharacterDelimiter()
    {
        var input = "one::two::three".AsSpan();

        var results = new List<string>();
        foreach (var range in Regex.EnumerateSplits(input, "::"))
        {
            results.Add(input[range].ToString());
        }

        await Assert.That(results.SequenceEqual(new[]
        {
            "one",
            "two",
            "three"
        })).IsTrue();
    }

    [Test]
    public async Task EnumerateSplits_CaseInsensitive()
    {
        var input = "HelloWORLDhello".AsSpan();

        var results = new List<string>();
        foreach (var range in Regex.EnumerateSplits(input, "hello", RegexOptions.IgnoreCase))
        {
            results.Add(input[range].ToString());
        }

        await Assert.That(results.Count).IsEqualTo(3);
    }

    [Test]
    public async Task EnumerateSplits_UnicodeCharacters()
    {
        var input = "α,β,γ,δ".AsSpan();

        var results = new List<string>();
        foreach (var range in Regex.EnumerateSplits(input, ","))
        {
            results.Add(input[range].ToString());
        }

        await Assert.That(results.SequenceEqual(new[]
        {
            "α",
            "β",
            "γ",
            "δ"
        })).IsTrue();
    }

#endif
}
