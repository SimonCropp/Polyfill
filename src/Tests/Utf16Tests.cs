#if FeatureMemory

using System.Text;
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

    const char high = '\uD83D';
    const char low = '\uDC4D';

    // one char at a time, with no shortcuts
    static int ReferenceIndexOfInvalidSubsequence(string value)
    {
        for (var index = 0; index < value.Length; index++)
        {
            var current = value[index];
            if (current is >= '\uD800' and <= '\uDBFF')
            {
                if (index + 1 == value.Length ||
                    value[index + 1] is < '\uDC00' or > '\uDFFF')
                {
                    return index;
                }

                index++;
            }
            else if (current is >= '\uDC00' and <= '\uDFFF')
            {
                return index;
            }
        }

        return -1;
    }

    static void Check(string value, List<string> failures)
    {
        var expected = ReferenceIndexOfInvalidSubsequence(value);
        var index = Utf16.IndexOfInvalidSubsequence(value.AsSpan());
        var valid = Utf16.IsValid(value.AsSpan());
        if (index != expected ||
            valid != (expected < 0))
        {
            failures.Add($"length {value.Length}: expected {expected} but got {index} (IsValid {valid})");
        }
    }

    [Test]
    public async Task IndexOfInvalidSubsequence_SurrogateAtEachPosition()
    {
        var failures = new List<string>();

        // long enough to cover runs with no surrogates either side of where the scan starts skipping ahead
        for (var length = 0; length <= 80; length++)
        {
            var text = new string('a', length);
            Check(text, failures);
            for (var position = 0; position <= length; position++)
            {
                Check(text.Insert(position, high.ToString()), failures);
                Check(text.Insert(position, low.ToString()), failures);
                Check(text.Insert(position, new(new[] {high, low})), failures);
                Check(text.Insert(position, new(new[] {low, high})), failures);
                Check(text.Insert(position, new(new[] {high, high, low})), failures);
                Check(text.Insert(position, new(new[] {high, low, low})), failures);
                Check(text.Insert(position, new(new[] {high, low, high, low})), failures);
            }
        }

        await Assert.That(failures).IsEmpty();
    }

    [Test]
    public async Task IndexOfInvalidSubsequence_PairsAtRegularIntervals()
    {
        var failures = new List<string>();
        var pair = new string(new[] {high, low});

        for (var interval = 0; interval <= 40; interval++)
        {
            var builder = new StringBuilder();
            for (var count = 0; count < 12; count++)
            {
                builder.Append('b', interval);
                builder.Append(pair);
            }

            var text = builder.ToString();
            Check(text, failures);

            // break the text at every position, by dropping a char and by adding a lone surrogate
            for (var position = 0; position < text.Length; position++)
            {
                Check(text.Remove(position, 1), failures);
                Check(text.Insert(position, low.ToString()), failures);
                Check(text.Insert(position, high.ToString()), failures);
            }
        }

        await Assert.That(failures).IsEmpty();
    }

    [Test]
    public async Task IndexOfInvalidSubsequence_Random()
    {
        var failures = new List<string>();
        var random = new Random(4321);
        for (var iteration = 0; iteration < 3000; iteration++)
        {
            var length = random.Next(0, 200);
            // from mostly surrogates to almost none
            var surrogateChance = random.Next(1, 60) / 100.0;
            var chars = new char[length];
            for (var index = 0; index < length; index++)
            {
                if (random.NextDouble() >= surrogateChance)
                {
                    chars[index] = (char) random.Next('a', 'z' + 1);
                }
                else if (random.Next(2) == 0)
                {
                    chars[index] = (char) random.Next(0xD800, 0xDC00);
                }
                else
                {
                    chars[index] = (char) random.Next(0xDC00, 0xE000);
                }
            }

            Check(new(chars), failures);
        }

        await Assert.That(failures).IsEmpty();
    }

    [Test]
    public async Task IndexOfInvalidSubsequence_Boundaries()
    {
        var failures = new List<string>();

        // the chars either side of the surrogate range are not surrogates
        Check(new string('\uD7FF', 100), failures);
        Check(new string('\uE000', 100), failures);
        Check(new string('\uFFFF', 100), failures);
        Check(new string('\0', 100), failures);
        // the first and last of each surrogate range
        Check(new string('a', 40) + "\U00010000", failures);
        Check(new string('a', 40) + "\U0010FFFF", failures);
        Check(new string('a', 40) + "\uD800", failures);
        Check(new string('a', 40) + "\uDBFF", failures);
        Check(new string('a', 40) + "\uDC00", failures);
        Check(new string('a', 40) + "\uDFFF", failures);
        Check(new string('a', 40) + "\uD800\uD7FF", failures);
        Check(new string('a', 40) + "\uD800\uE000", failures);

        var longValid = new string('a', 100000) + "\U0001F44D" + new string('b', 100000);
        Check(longValid, failures);
        Check(longValid + "\uD83D", failures);
        Check(longValid.Remove(100000, 1), failures);

        await Assert.That(failures).IsEmpty();
    }
}

#endif
