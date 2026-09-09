#if FeatureMemory

partial class PolyfillTests
{
    [Test]
    public async Task Span_ContainsAnyWhiteSpace()
    {
        await Assert.That("ab cd".AsSpan().ContainsAnyWhiteSpace()).IsTrue();
        await Assert.That("abcd".AsSpan().ContainsAnyWhiteSpace()).IsFalse();
        await Assert.That("\t".AsSpan().ContainsAnyWhiteSpace()).IsTrue();
        await Assert.That(ReadOnlySpan<char>.Empty.ContainsAnyWhiteSpace()).IsFalse();
    }

    [Test]
    public async Task Span_IndexOfAnyWhiteSpace()
    {
        await Assert.That("ab cd ef".AsSpan().IndexOfAnyWhiteSpace()).IsEqualTo(2);
        await Assert.That("abc".AsSpan().IndexOfAnyWhiteSpace()).IsEqualTo(-1);
        await Assert.That("   ".AsSpan().IndexOfAnyWhiteSpace()).IsEqualTo(0);
        await Assert.That(ReadOnlySpan<char>.Empty.IndexOfAnyWhiteSpace()).IsEqualTo(-1);
    }

    [Test]
    public async Task Span_IndexOfAnyExceptWhiteSpace()
    {
        await Assert.That("ab cd ef".AsSpan().IndexOfAnyExceptWhiteSpace()).IsEqualTo(0);
        await Assert.That("  abc".AsSpan().IndexOfAnyExceptWhiteSpace()).IsEqualTo(2);
        await Assert.That("   ".AsSpan().IndexOfAnyExceptWhiteSpace()).IsEqualTo(-1);
        await Assert.That(ReadOnlySpan<char>.Empty.IndexOfAnyExceptWhiteSpace()).IsEqualTo(-1);
    }

    [Test]
    public async Task Span_LastIndexOfAnyWhiteSpace()
    {
        await Assert.That("ab cd ef".AsSpan().LastIndexOfAnyWhiteSpace()).IsEqualTo(5);
        await Assert.That("abc".AsSpan().LastIndexOfAnyWhiteSpace()).IsEqualTo(-1);
        await Assert.That("   ".AsSpan().LastIndexOfAnyWhiteSpace()).IsEqualTo(2);
        await Assert.That(ReadOnlySpan<char>.Empty.LastIndexOfAnyWhiteSpace()).IsEqualTo(-1);
    }

    [Test]
    public async Task Span_LastIndexOfAnyExceptWhiteSpace()
    {
        await Assert.That("ab cd ef".AsSpan().LastIndexOfAnyExceptWhiteSpace()).IsEqualTo(7);
        await Assert.That("abc  ".AsSpan().LastIndexOfAnyExceptWhiteSpace()).IsEqualTo(2);
        await Assert.That("   ".AsSpan().LastIndexOfAnyExceptWhiteSpace()).IsEqualTo(-1);
        await Assert.That(ReadOnlySpan<char>.Empty.LastIndexOfAnyExceptWhiteSpace()).IsEqualTo(-1);
    }

    [Test]
    [Arguments(' ')]
    [Arguments('\t')]
    [Arguments('\n')]
    [Arguments('\r')]
    [Arguments('')] // vertical tab
    [Arguments(' ')] // no-break space
    [Arguments(' ')] // em space
    [Arguments('　')] // ideographic space
    [Arguments('a')]
    [Arguments('​')] // zero width space, which is not white-space
    public async Task Span_WhiteSpace_MatchesCharIsWhiteSpace(char value)
    {
        var contains = value.ToString().AsSpan().ContainsAnyWhiteSpace();
        var index = value.ToString().AsSpan().IndexOfAnyWhiteSpace();
        var except = value.ToString().AsSpan().IndexOfAnyExceptWhiteSpace();
        var isWhiteSpace = char.IsWhiteSpace(value);
        await Assert.That(contains).IsEqualTo(isWhiteSpace);
        await Assert.That(index).IsEqualTo(isWhiteSpace ? 0 : -1);
        await Assert.That(except).IsEqualTo(isWhiteSpace ? -1 : 0);
    }
}

#endif
