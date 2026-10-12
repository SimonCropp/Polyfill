using System.Text;

partial class PolyfillTests
{
    // ordinal casing deliberately leaves these uncased, where invariant casing maps them,
    // so that it stays consistent with OrdinalIgnoreCase
    const char kelvin = 'K';
    const char ohm = 'Ω';
    const char angstrom = 'Å';
    const char capitalSharpS = 'ẞ';
    const char capitalThetaSymbol = 'ϴ';
    const char longS = 'ſ';

    // U+10400 DESERET CAPITAL LETTER LONG I, lowercases to U+10428
    const string deseretCapital = "\U00010400";
    const string deseretSmall = "\U00010428";

    [Test]
    public async Task Char_ToLowerOrdinal()
    {
        await Assert.That(char.ToLowerOrdinal('A')).IsEqualTo('a');
        await Assert.That(char.ToLowerOrdinal('a')).IsEqualTo('a');
        await Assert.That(char.ToLowerOrdinal('1')).IsEqualTo('1');
        await Assert.That(char.ToLowerOrdinal('Å')).IsEqualTo('å');
    }

    [Test]
    [Arguments(kelvin)]
    [Arguments(ohm)]
    [Arguments(angstrom)]
    [Arguments(capitalSharpS)]
    [Arguments(capitalThetaSymbol)]
    public async Task Char_ToLowerOrdinal_PreservesOrdinalClass(char value) =>
        await Assert.That(char.ToLowerOrdinal(value)).IsEqualTo(value);

    [Test]
    public async Task Char_ToUpperOrdinal()
    {
        await Assert.That(char.ToUpperOrdinal('a')).IsEqualTo('A');
        await Assert.That(char.ToUpperOrdinal('A')).IsEqualTo('A');
        await Assert.That(char.ToUpperOrdinal('1')).IsEqualTo('1');
        await Assert.That(char.ToUpperOrdinal('å')).IsEqualTo('Å');
        // invariant casing would map the long s to S
        await Assert.That(char.ToUpperOrdinal(longS)).IsEqualTo(longS);
    }

    [Test]
    public async Task Char_Equals_OrdinalIgnoreCase_UsesOrdinalCasing()
    {
        await Assert.That('A'.Equals('a', StringComparison.OrdinalIgnoreCase)).IsTrue();
        await Assert.That('A'.Equals('a', StringComparison.Ordinal)).IsFalse();
        await Assert.That('A'.Equals('a', StringComparison.InvariantCultureIgnoreCase)).IsTrue();
        // ordinal casing keeps these out of each other's casing class, unlike invariant casing
        await Assert.That(kelvin.Equals('k', StringComparison.OrdinalIgnoreCase)).IsFalse();
        await Assert.That(longS.Equals('s', StringComparison.OrdinalIgnoreCase)).IsFalse();
    }

    [Test]
    public async Task String_ToLowerOrdinal()
    {
        await Assert.That("ABC".ToLowerOrdinal()).IsEqualTo("abc");
        await Assert.That(string.Empty.ToLowerOrdinal()).IsEqualTo(string.Empty);
        await Assert.That("Straße".ToLowerOrdinal()).IsEqualTo("straße");
        await Assert.That($"a{kelvin}b".ToLowerOrdinal()).IsEqualTo($"a{kelvin}b");
        // casing applies per scalar, so surrogate pairs are mapped
        await Assert.That(deseretCapital.ToLowerOrdinal()).IsEqualTo(deseretSmall);
    }

    [Test]
    public async Task String_ToUpperOrdinal()
    {
        await Assert.That("abc".ToUpperOrdinal()).IsEqualTo("ABC");
        await Assert.That(string.Empty.ToUpperOrdinal()).IsEqualTo(string.Empty);
        await Assert.That($"a{longS}b".ToUpperOrdinal()).IsEqualTo($"A{longS}B");
        await Assert.That(deseretSmall.ToUpperOrdinal()).IsEqualTo(deseretCapital);
    }

    [Test]
    public async Task String_OrdinalCasing_MatchesCharCasingForEveryBmpChar()
    {
        var failures = new List<string>();
        var all = new StringBuilder();
        var allUpper = new StringBuilder();
        var allLower = new StringBuilder();
        for (var value = 0; value <= char.MaxValue; value++)
        {
            var current = (char) value;
            var upper = current;
            var lower = current;
            // a surrogate on its own is not a scalar, and is left alone
            if (!char.IsSurrogate(current))
            {
                upper = char.ToUpperOrdinal(current);
                lower = char.ToLowerOrdinal(current);
                all.Append(current);
                allUpper.Append(upper);
                allLower.Append(lower);
            }

            var text = current.ToString();
            if (text.ToUpperOrdinal() != upper.ToString() ||
                text.ToLowerOrdinal() != lower.ToString())
            {
                failures.Add($"U+{value:X4} alone");
            }

            var inContext = "a" + current + "Z" + current;
            if (inContext.ToUpperOrdinal() != "A" + upper + "Z" + upper ||
                inContext.ToLowerOrdinal() != "a" + lower + "z" + lower)
            {
                failures.Add($"U+{value:X4} in context");
            }
        }

        // every char at once, so that a char changing length or position would show up
        if (all.ToString().ToUpperOrdinal() != allUpper.ToString())
        {
            failures.Add("all chars to upper");
        }

        if (all.ToString().ToLowerOrdinal() != allLower.ToString())
        {
            failures.Add("all chars to lower");
        }

        await Assert.That(failures).IsEmpty();
    }

    [Test]
    public async Task String_ToUpperOrdinal_LongS()
    {
        // each long s stays where it is, whatever surrounds it
        await Assert.That(longS.ToString().ToUpperOrdinal()).IsEqualTo(longS.ToString());
        await Assert.That($"{longS}{longS}".ToUpperOrdinal()).IsEqualTo($"{longS}{longS}");
        await Assert.That($"{longS}a{longS}b{longS}".ToUpperOrdinal()).IsEqualTo($"{longS}A{longS}B{longS}");
        await Assert.That($"s{longS}S".ToUpperOrdinal()).IsEqualTo($"S{longS}S");
        await Assert.That($"abc{longS}".ToUpperOrdinal()).IsEqualTo($"ABC{longS}");
        await Assert.That($"{longS}abc".ToUpperOrdinal()).IsEqualTo($"{longS}ABC");
        await Assert.That($"{deseretSmall}{longS}{deseretSmall}".ToUpperOrdinal()).IsEqualTo($"{deseretCapital}{longS}{deseretCapital}");
        // lower casing does not involve it
        await Assert.That($"A{longS}B".ToLowerOrdinal()).IsEqualTo($"a{longS}b");
    }

    [Test]
    public async Task String_ToLowerOrdinal_PreservesOrdinalClass()
    {
        // these would be lower cased by invariant casing, and have to be put back
        foreach (var value in new[] {kelvin, ohm, angstrom, capitalSharpS, capitalThetaSymbol})
        {
            var expected = char.ToLowerOrdinal(value);
            await Assert.That(value.ToString().ToLowerOrdinal()).IsEqualTo(expected.ToString());
            await Assert.That($"AB{value}CD{value}".ToLowerOrdinal()).IsEqualTo($"ab{expected}cd{expected}");
            await Assert.That($"{value}{deseretCapital}{value}".ToLowerOrdinal()).IsEqualTo($"{expected}{deseretSmall}{expected}");
        }
    }

    [Test]
    public async Task String_OrdinalCasing_Ascii()
    {
        // every ASCII char, repeated so the string is long enough to be cased a vector at a time
        var builder = new StringBuilder();
        var expectedUpper = new StringBuilder();
        var expectedLower = new StringBuilder();
        for (var repeat = 0; repeat < 5; repeat++)
        {
            for (var value = 0; value < 128; value++)
            {
                var current = (char) value;
                builder.Append(current);
                if (current is >= 'a' and <= 'z')
                {
                    expectedUpper.Append((char) (current - 32));
                }
                else
                {
                    expectedUpper.Append(current);
                }

                if (current is >= 'A' and <= 'Z')
                {
                    expectedLower.Append((char) (current + 32));
                }
                else
                {
                    expectedLower.Append(current);
                }
            }
        }

        var text = builder.ToString();
        await Assert.That(text.ToUpperOrdinal()).IsEqualTo(expectedUpper.ToString());
        await Assert.That(text.ToLowerOrdinal()).IsEqualTo(expectedLower.ToString());

        // one char that is not ASCII, at either end and in the middle of ASCII text
        var e = 'é';
        var upperE = 'É';
        await Assert.That($"{e}content-type".ToUpperOrdinal()).IsEqualTo($"{upperE}CONTENT-TYPE");
        await Assert.That($"content{e}type".ToUpperOrdinal()).IsEqualTo($"CONTENT{upperE}TYPE");
        await Assert.That($"content-type{e}".ToUpperOrdinal()).IsEqualTo($"CONTENT-TYPE{upperE}");
        await Assert.That($"{upperE}CONTENT-TYPE".ToLowerOrdinal()).IsEqualTo($"{e}content-type");
        await Assert.That($"CONTENT{upperE}TYPE".ToLowerOrdinal()).IsEqualTo($"content{e}type");
        await Assert.That($"CONTENT-TYPE{upperE}".ToLowerOrdinal()).IsEqualTo($"content-type{e}");
    }

    [Test]
    public async Task String_OrdinalCasing_Unchanged()
    {
        await Assert.That("ABC 123 !?".ToUpperOrdinal()).IsEqualTo("ABC 123 !?");
        await Assert.That("abc 123 !?".ToLowerOrdinal()).IsEqualTo("abc 123 !?");
        await Assert.That("1".ToUpperOrdinal()).IsEqualTo("1");
        await Assert.That("1".ToLowerOrdinal()).IsEqualTo("1");
        await Assert.That(deseretCapital.ToUpperOrdinal()).IsEqualTo(deseretCapital);
        await Assert.That(deseretSmall.ToLowerOrdinal()).IsEqualTo(deseretSmall);

        // the source string is never modified
        var source = "MiXeD";
        source.ToUpperOrdinal();
        source.ToLowerOrdinal();
        await Assert.That(source).IsEqualTo("MiXeD");
    }

    [Test]
    public async Task String_OrdinalCasing_Surrogates()
    {
        var high = deseretCapital[0];
        var low = deseretCapital[1];
        var smallLow = deseretSmall[1];

        // a pair is cased as one scalar, including when pairs sit next to each other or to other text
        await Assert.That($"a{deseretCapital}B{deseretCapital}{deseretCapital}c".ToLowerOrdinal())
            .IsEqualTo($"a{deseretSmall}b{deseretSmall}{deseretSmall}c");
        await Assert.That($"a{deseretSmall}B{deseretSmall}{deseretSmall}c".ToUpperOrdinal())
            .IsEqualTo($"A{deseretCapital}B{deseretCapital}{deseretCapital}C");

        // half a pair is left alone
        await Assert.That($"A{high}".ToLowerOrdinal()).IsEqualTo($"a{high}");
        await Assert.That($"{low}A".ToLowerOrdinal()).IsEqualTo($"{low}a");
        await Assert.That($"{low}{high}A".ToLowerOrdinal()).IsEqualTo($"{low}{high}a");
        await Assert.That($"a{high}".ToUpperOrdinal()).IsEqualTo($"A{high}");
        await Assert.That($"{smallLow}a".ToUpperOrdinal()).IsEqualTo($"{smallLow}A");
        await Assert.That($"{smallLow}{high}a".ToUpperOrdinal()).IsEqualTo($"{smallLow}{high}A");

        // a high surrogate followed by a whole pair
        await Assert.That($"{high}{deseretCapital}A".ToLowerOrdinal()).IsEqualTo($"{high}{deseretSmall}a");
        await Assert.That($"{high}{deseretSmall}a".ToUpperOrdinal()).IsEqualTo($"{high}{deseretCapital}A");
    }

#if FeatureMemory
    [Test]
    public async Task Span_ToLowerOrdinal()
    {
        var destination = new char[3];
        var written = "ABC".AsSpan().ToLowerOrdinal(destination);
        var result = new string(destination);
        await Assert.That(written).IsEqualTo(3);
        await Assert.That(result).IsEqualTo("abc");
    }

    [Test]
    public async Task Span_ToUpperOrdinal()
    {
        var destination = new char[3];
        var written = "abc".AsSpan().ToUpperOrdinal(destination);
        var result = new string(destination);
        await Assert.That(written).IsEqualTo(3);
        await Assert.That(result).IsEqualTo("ABC");
    }

    [Test]
    public async Task Span_ToLowerOrdinal_SurrogatePair()
    {
        var destination = new char[2];
        var written = deseretCapital.AsSpan().ToLowerOrdinal(destination);
        var result = new string(destination);
        await Assert.That(written).IsEqualTo(2);
        await Assert.That(result).IsEqualTo(deseretSmall);
    }

    [Test]
    public async Task String_OrdinalCasing_MatchesSpanCasingForSupplementaryPlanes()
    {
        // every scalar of plane 1, where the cased supplementary scripts live, and a sample of the other planes
        var builder = new StringBuilder();
        for (var codePoint = 0x10000; codePoint <= 0x1FFFF; codePoint++)
        {
            builder.Append(char.ConvertFromUtf32(codePoint));
        }

        for (var codePoint = 0x20000; codePoint <= 0x10FFFF; codePoint += 97)
        {
            builder.Append(char.ConvertFromUtf32(codePoint));
        }

        // interleave some chars that the two casings treat differently
        builder.Append(longS).Append(deseretSmall).Append(kelvin).Append(deseretCapital).Append(longS);
        var text = builder.ToString();

        var destination = new char[text.Length];
        var upperWritten = text.AsSpan().ToUpperOrdinal(destination);
        var spanUpper = new string(destination);
        var lowerWritten = text.AsSpan().ToLowerOrdinal(destination);
        var spanLower = new string(destination);

        var upper = text.ToUpperOrdinal();
        var lower = text.ToLowerOrdinal();

        await Assert.That(upperWritten).IsEqualTo(text.Length);
        await Assert.That(lowerWritten).IsEqualTo(text.Length);
        await Assert.That(upper == spanUpper).IsTrue();
        await Assert.That(lower == spanLower).IsTrue();
        // some of those scalars are cased, so this is not comparing three copies of the input
        await Assert.That(upper == text).IsFalse();
        await Assert.That(lower == text).IsFalse();
    }

    [Test]
    public async Task Span_ToLowerOrdinal_DestinationTooSmall()
    {
        var destination = new char[2];
        var written = "ABC".AsSpan().ToLowerOrdinal(destination);
        await Assert.That(written).IsEqualTo(-1);
    }

    [Test]
    public async Task Span_ToLowerOrdinal_Empty()
    {
        var written = ReadOnlySpan<char>.Empty.ToLowerOrdinal([]);
        await Assert.That(written).IsEqualTo(0);
    }

    [Test]
    public async Task Span_ToLowerOrdinal_Overlapping()
    {
        var buffer = "ABCD".ToCharArray();
        await Assert.That(() => { buffer.AsSpan().ToLowerOrdinal(buffer.AsSpan()); })
            .Throws<InvalidOperationException>();
    }
#endif

#if NETCOREAPP3_0_OR_GREATER
    [Test]
    public async Task Rune_ToLowerOrdinal()
    {
        await Assert.That(Rune.ToLowerOrdinal(new('A'))).IsEqualTo(new Rune('a'));
        await Assert.That(Rune.ToLowerOrdinal(new(kelvin))).IsEqualTo(new Rune(kelvin));
        await Assert.That(Rune.ToLowerOrdinal(Rune.GetRuneAt(deseretCapital, 0))).IsEqualTo(Rune.GetRuneAt(deseretSmall, 0));
    }

    [Test]
    public async Task Rune_ToUpperOrdinal()
    {
        await Assert.That(Rune.ToUpperOrdinal(new('a'))).IsEqualTo(new Rune('A'));
        await Assert.That(Rune.ToUpperOrdinal(new(longS))).IsEqualTo(new Rune(longS));
        await Assert.That(Rune.ToUpperOrdinal(Rune.GetRuneAt(deseretSmall, 0))).IsEqualTo(Rune.GetRuneAt(deseretCapital, 0));
    }

    [Test]
    public async Task Rune_Equals_StringComparison()
    {
        await Assert.That(new Rune('A').Equals(new('a'), StringComparison.OrdinalIgnoreCase)).IsTrue();
        await Assert.That(new Rune('A').Equals(new('a'), StringComparison.Ordinal)).IsFalse();
        await Assert.That(new Rune('A').Equals(new('a'), StringComparison.InvariantCultureIgnoreCase)).IsTrue();
        await Assert.That(new Rune(kelvin).Equals(new('k'), StringComparison.OrdinalIgnoreCase)).IsFalse();
        await Assert.That(new Rune(longS).Equals(new('s'), StringComparison.OrdinalIgnoreCase)).IsFalse();
    }
#endif
}
