partial class PolyfillTests
{
    [Test]
    public void RegexIsMatch()
    {
        var regex = new Regex(@"\d+");
        var match = regex.Match("a55a");
        Assert.IsTrue(match.Success);
    }

    [Test]
    public void EnumerateMatches()
    {
        var regex = new Regex(@"\d+");
        var span = "a55a".AsSpan();
        var found = false;
        foreach (var match in regex.EnumerateMatches(span))
        {
            found = true;
            Assert.AreEqual(1, match.Index);
            Assert.AreEqual(2, match.Length);
        }

        Assert.IsTrue(found);
    }

    [Test]
    public void EnumerateMatchesStatic()
    {
        var span = "a55a".AsSpan();
        var found = false;
        foreach (var match in Regex.EnumerateMatches(span, @"\d+"))
        {
            found = true;
            Assert.AreEqual(1, match.Index);
            Assert.AreEqual(2, match.Length);
        }

        Assert.IsTrue(found);
    }

#if FeatureMemory

   [Test]
    public void EnumerateSplits_InstanceMethod_BasicSplit()
    {
        var regex = new Regex(@"[,\s]+");
        var input = "one, two  three,four".AsSpan();

        var results = new List<string>();
        foreach (var range in regex.EnumerateSplits(input))
        {
            results.Add(input[range].ToString());
        }

        Assert.That(results, Is.EqualTo(new[] { "one", "two", "three", "four" }));
    }

    [Test]
    public void EnumerateSplits_InstanceMethod_WithCount()
    {
        var regex = new Regex(@"[,\s]+");
        var input = "one, two  three,four".AsSpan();

        var results = new List<string>();
        foreach (var range in regex.EnumerateSplits(input, 2))
        {
            results.Add(input[range].ToString());
        }

        Assert.That(results, Has.Count.EqualTo(2));
    }

    [Test]
    public void EnumerateSplits_InstanceMethod_WithCountAndStartat()
    {
        var regex = new Regex(@"[,\s]+");
        var input = "one, two  three,four".AsSpan();

        var results = new List<string>();
        foreach (var range in regex.EnumerateSplits(input, 2, 5))
        {
            results.Add(input[range].ToString());
        }

        Assert.That(results, Has.Count.EqualTo(2));
    }

    [Test]
    public void EnumerateSplits_InstanceMethod_EmptyInput()
    {
        var regex = new Regex(",");
        var input = "".AsSpan();

        var results = new List<string>();
        foreach (var range in regex.EnumerateSplits(input))
        {
            results.Add(input[range].ToString());
        }

        Assert.That(results, Has.Count.EqualTo(1));
        Assert.That(results[0], Is.EqualTo(""));
    }

    [Test]
    public void EnumerateSplits_InstanceMethod_NoMatch()
    {
        var regex = new Regex("xyz");
        var input = "hello world".AsSpan();

        var results = new List<string>();
        foreach (var range in regex.EnumerateSplits(input))
        {
            results.Add(input[range].ToString());
        }

        Assert.That(results, Has.Count.EqualTo(1));
        Assert.That(results[0], Is.EqualTo("hello world"));
    }

    [Test]
    public void EnumerateSplits_InstanceMethod_NegativeCount_ThrowsArgumentOutOfRangeException()
    {
        var regex = new Regex(",");

        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            var input = "test".AsSpan();
            foreach (var _ in regex.EnumerateSplits(input, -1))
            {
            }
        });
    }

    [Test]
    public void EnumerateSplits_InstanceMethod_InvalidStartat_ThrowsArgumentOutOfRangeException()
    {
        var regex = new Regex(",");

        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            var input = "test".AsSpan();
            foreach (var _ in regex.EnumerateSplits(input, 0, 100))
            {
            }
        });
    }

    [Test]
    public void EnumerateSplits_StaticMethod_BasicSplit()
    {
        var input = "one,two,three".AsSpan();

        var results = new List<string>();
        foreach (var range in Regex.EnumerateSplits(input, ","))
        {
            results.Add(input[range].ToString());
        }

        Assert.That(results, Is.EqualTo(new[] { "one", "two", "three" }));
    }

    [Test]
    public void EnumerateSplits_StaticMethod_WithOptions()
    {
        var input = "ONE,two,THREE".AsSpan();

        var results = new List<string>();
        foreach (var range in Regex.EnumerateSplits(input, "[ot]", RegexOptions.IgnoreCase))
        {
            results.Add(input[range].ToString());
        }

        Assert.That(results.Count, Is.GreaterThan(2));
    }

    [Test]
    public void EnumerateSplits_StaticMethod_WithTimeout()
    {
        var input = "one,two,three".AsSpan();

        var results = new List<string>();
        foreach (var range in Regex.EnumerateSplits(input, ",", RegexOptions.None, TimeSpan.FromSeconds(1)))
        {
            results.Add(input[range].ToString());
        }

        Assert.That(results, Is.EqualTo(new[] { "one", "two", "three" }));
    }

    [Test]
    public void EnumerateSplits_Digits_SplitCorrectly()
    {
        var input = "abc123def456ghi".AsSpan();

        var results = new List<string>();
        foreach (var range in Regex.EnumerateSplits(input, @"\d+"))
        {
            results.Add(input[range].ToString());
        }

        Assert.That(results, Is.EqualTo(new[] { "abc", "def", "ghi" }));
    }

    [Test]
    public void EnumerateSplits_Whitespace_SplitCorrectly()
    {
        var input = "Hello   World  Test".AsSpan();

        var results = new List<string>();
        foreach (var range in Regex.EnumerateSplits(input, @"\s+"))
        {
            results.Add(input[range].ToString());
        }

        Assert.That(results, Is.EqualTo(new[] { "Hello", "World", "Test" }));
    }

    [Test]
    public void EnumerateSplits_ConsecutiveMatches_ProducesEmptyStrings()
    {
        var input = "a,,b".AsSpan();

        var results = new List<string>();
        foreach (var range in Regex.EnumerateSplits(input, ","))
        {
            results.Add(input[range].ToString());
        }

        Assert.That(results, Is.EqualTo(new[] { "a", "", "b" }));
    }

    [Test]
    public void EnumerateSplits_MatchAtBeginning()
    {
        var input = ",a,b".AsSpan();

        var results = new List<string>();
        foreach (var range in Regex.EnumerateSplits(input, ","))
        {
            results.Add(input[range].ToString());
        }

        Assert.That(results, Is.EqualTo(new[] { "", "a", "b" }));
    }

    [Test]
    public void EnumerateSplits_MatchAtEnd()
    {
        var input = "a,b,".AsSpan();

        var results = new List<string>();
        foreach (var range in Regex.EnumerateSplits(input, ","))
        {
            results.Add(input[range].ToString());
        }

        Assert.That(results, Is.EqualTo(new[] { "a", "b", "" }));
    }

    [Test]
    public void EnumerateSplits_ComplexPattern()
    {
        var input = "user@example.com;admin@test.org;guest@domain.net".AsSpan();

        var results = new List<string>();
        foreach (var range in Regex.EnumerateSplits(input, ";"))
        {
            results.Add(input[range].ToString());
        }

        Assert.That(results, Is.EqualTo(new[] { "user@example.com", "admin@test.org", "guest@domain.net" }));
    }

    [Test]
    public void EnumerateSplits_ZeroCount_ReturnsAllSplits()
    {
        var regex = new Regex(",");
        var input = "a,b,c,d".AsSpan();

        var results = new List<string>();
        foreach (var range in regex.EnumerateSplits(input, 0))
        {
            results.Add(input[range].ToString());
        }

        Assert.That(results, Is.EqualTo(new[] { "a", "b", "c", "d" }));
    }

    [Test]
    public void EnumerateSplits_MultiCharacterDelimiter()
    {
        var input = "one::two::three".AsSpan();

        var results = new List<string>();
        foreach (var range in Regex.EnumerateSplits(input, "::"))
        {
            results.Add(input[range].ToString());
        }

        Assert.That(results, Is.EqualTo(new[] { "one", "two", "three" }));
    }

    [Test]
    public void EnumerateSplits_CaseInsensitive()
    {
        var input = "HelloWORLDhello".AsSpan();

        var results = new List<string>();
        foreach (var range in Regex.EnumerateSplits(input, "hello", RegexOptions.IgnoreCase))
        {
            results.Add(input[range].ToString());
        }

        Assert.That(results, Has.Count.EqualTo(3));
    }

    [Test]
    public void EnumerateSplits_UnicodeCharacters()
    {
        var input = "α,β,γ,δ".AsSpan();

        var results = new List<string>();
        foreach (var range in Regex.EnumerateSplits(input, ","))
        {
            results.Add(input[range].ToString());
        }

        Assert.That(results, Is.EqualTo(new[] { "α", "β", "γ", "δ" }));
    }

#endif
}