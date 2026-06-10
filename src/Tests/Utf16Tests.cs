#if FeatureMemory

using System.Text.Unicode;

public class Utf16Tests
{
    [Test]
    public async Task IsValid_Ascii()
    {
        var result = Utf16.IsValid("Hello".AsSpan());
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsValid_ValidSurrogatePair()
    {
        // U+1F44D (👍) encoded as a high/low surrogate pair
        var result = Utf16.IsValid("👍".AsSpan());
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsValid_UnpairedHighSurrogate()
    {
        var result = Utf16.IsValid("\uD83D".AsSpan());
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task IsValid_UnpairedLowSurrogate()
    {
        var result = Utf16.IsValid("a\uDC4D".AsSpan());
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task IndexOfInvalidSubsequence_Valid()
    {
        var index = Utf16.IndexOfInvalidSubsequence("ab👍cd".AsSpan());
        await Assert.That(index).IsEqualTo(-1);
    }

    [Test]
    public async Task IndexOfInvalidSubsequence_HighSurrogateAtEnd()
    {
        var index = Utf16.IndexOfInvalidSubsequence("ab\uD83D".AsSpan());
        await Assert.That(index).IsEqualTo(2);
    }

    [Test]
    public async Task IndexOfInvalidSubsequence_LoneLowSurrogate()
    {
        var index = Utf16.IndexOfInvalidSubsequence("a\uDC4Db".AsSpan());
        await Assert.That(index).IsEqualTo(1);
    }
}

#endif
