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
