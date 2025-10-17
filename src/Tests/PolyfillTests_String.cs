partial class PolyfillTests
{
    [Test]
    public void GetHashCodeStringComparison()
    {
        var hash1 = "value".GetHashCode(StringComparison.Ordinal);
        Assert.AreNotEqual(0, hash1);

        var hash2 = string.GetHashCode("value".AsSpan());
        Assert.AreNotEqual(0, hash2);

        var hash3 = string.GetHashCode("value".AsSpan(), StringComparison.Ordinal);
        Assert.AreNotEqual(0, hash3);
    }

    [Test]
    public void EndsWith()
    {
        Assert.True("value".EndsWith('e'));
        Assert.True("e".EndsWith('e'));
        Assert.False("".EndsWith('e'));
    }

    [Test]
    [Platform("Win")]
    public void ReplaceLineEndings()
    {
        Assert.AreEqual("a\r\nb\r\nc\r\nd", "a\rb\nc\r\nd".ReplaceLineEndings());
        Assert.AreEqual("a_b_c_d", "a\rb\nc\r\nd".ReplaceLineEndings("_"));
    }

    [Test]
    public void CopyTo()
    {
        var span = new Span<char>(new char[1]);
        "a".CopyTo(span);
        Assert.AreEqual("a", span.ToString());
    }

    [Test]
    public void TryCopyTo()
    {
        var span = new Span<char>(new char[1]);
        Assert.IsTrue("a".TryCopyTo(span));
        Assert.AreEqual("a", span.ToString());
    }

    [Test]
    public void StringContainsStringComparison() =>
        Assert.True("value".Contains("E", StringComparison.OrdinalIgnoreCase));

    [Test]
    public void StartsWith()
    {
        Assert.True("value".StartsWith('v'));
        Assert.True("v".StartsWith('v'));
        Assert.False("".StartsWith('v'));
    }

    [Test]
    public void Split()
    {
        Assert.AreEqual(new []{"a","b"}, "a b".Split(' ', StringSplitOptions.RemoveEmptyEntries));
        Assert.AreEqual(new []{"a","b"}, "a b".Split(' ', 2, StringSplitOptions.RemoveEmptyEntries));

        Assert.AreEqual(new []{"a","b"}, "a b".Split(" ", StringSplitOptions.RemoveEmptyEntries));
        Assert.AreEqual(new []{"a","b"}, "a b".Split(" ", 2, StringSplitOptions.RemoveEmptyEntries));
    }

    [Test]
    public void ContainsChar() =>
        Assert.True("value".Contains('v'));
}
