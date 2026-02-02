partial class PolyfillTests
{
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
