namespace Tests;

public class CharTests
{
    [Test]
    public async Task Char_IsAscii()
    {
        await Assert.That(char.IsAscii('\u0000')).IsEqualTo(true);
        await Assert.That(char.IsAscii('a')).IsEqualTo(true);
    }

    [Test]
    public async Task Char_IsAsciiLetter()
    {
        await Assert.That(char.IsAsciiLetter('\u0000')).IsEqualTo(false);
        await Assert.That(char.IsAsciiLetter('a')).IsEqualTo(true);
        await Assert.That(char.IsAsciiLetter('B')).IsEqualTo(true);
        await Assert.That(char.IsAsciiLetter('3')).IsEqualTo(false);
    }

    [Test]
    public async Task Char_IsAsciiLetterLower()
    {
        await Assert.That(char.IsAsciiLetterLower('b')).IsEqualTo(true);
        await Assert.That(char.IsAsciiLetterLower('B')).IsEqualTo(false);
        await Assert.That(char.IsAsciiLetterLower('z')).IsEqualTo(true);
        await Assert.That(char.IsAsciiLetterLower('Z')).IsEqualTo(false);
    }

    [Test]
    public async Task Char_IsAsciiLetterUpper()
    {
        await Assert.That(char.IsAsciiLetterUpper('b')).IsEqualTo(false);
        await Assert.That(char.IsAsciiLetterUpper('B')).IsEqualTo(true);
        await Assert.That(char.IsAsciiLetterUpper('z')).IsEqualTo(false);
        await Assert.That(char.IsAsciiLetterUpper('Z')).IsEqualTo(true);
    }

    [Test]
    public async Task Char_IsAsciiDigit()
    {
        await Assert.That(char.IsAsciiDigit('0')).IsEqualTo(true);
        await Assert.That(char.IsAsciiDigit('3')).IsEqualTo(true);
        await Assert.That(char.IsAsciiDigit('a')).IsEqualTo(false);
        await Assert.That(char.IsAsciiDigit('F')).IsEqualTo(false);
    }

    [Test]
    public async Task Char_IsAsciiHexDigit()
    {
        await Assert.That(char.IsAsciiHexDigit('0')).IsEqualTo(true);
        await Assert.That(char.IsAsciiHexDigit('3')).IsEqualTo(true);
        await Assert.That(char.IsAsciiHexDigit('a')).IsEqualTo(true);
        await Assert.That(char.IsAsciiHexDigit('F')).IsEqualTo(true);

        await Assert.That(char.IsAsciiHexDigit('G')).IsEqualTo(false);
        await Assert.That(char.IsAsciiHexDigit('h')).IsEqualTo(false);
    }

    [Test]
    public async Task Char_IsAsciiHexDigitLower()
    {
        await Assert.That(char.IsAsciiHexDigitLower('0')).IsEqualTo(true);
        await Assert.That(char.IsAsciiHexDigitLower('3')).IsEqualTo(true);
        await Assert.That(char.IsAsciiHexDigitLower('a')).IsEqualTo(true);
        await Assert.That(char.IsAsciiHexDigitLower('F')).IsEqualTo(false);

        await Assert.That(char.IsAsciiHexDigitLower('G')).IsEqualTo(false);
        await Assert.That(char.IsAsciiHexDigitLower('h')).IsEqualTo(false);
    }

    [Test]
    public async Task Char_IsAsciiHexDigitUpper()
    {
        await Assert.That(char.IsAsciiHexDigitUpper('0')).IsEqualTo(true);
        await Assert.That(char.IsAsciiHexDigitUpper('3')).IsEqualTo(true);
        await Assert.That(char.IsAsciiHexDigitUpper('a')).IsEqualTo(false);
        await Assert.That(char.IsAsciiHexDigitUpper('F')).IsEqualTo(true);

        await Assert.That(char.IsAsciiHexDigitUpper('G')).IsEqualTo(false);
        await Assert.That(char.IsAsciiHexDigitUpper('h')).IsEqualTo(false);
    }

    [Test]
    public async Task Char_IsBetween()
    {
        await Assert.That(char.IsBetween('c', 'a', 'z')).IsEqualTo(true);
        await Assert.That(char.IsBetween('a', 'a', 'z')).IsEqualTo(true);
        await Assert.That(char.IsBetween('z', 'a', 'z')).IsEqualTo(true);
        await Assert.That(char.IsBetween('A', 'a', 'z')).IsEqualTo(false);
        await Assert.That(char.IsBetween('0', 'a', 'z')).IsEqualTo(false);
    }
}
