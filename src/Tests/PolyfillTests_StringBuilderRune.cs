#if NETCOREAPP3_0_OR_GREATER

using System.Linq;
using System.Text;

partial class PolyfillTests
{
    // U+10400 DESERET CAPITAL LETTER LONG I, which is a surrogate pair
    const string deseret = "\U00010400";

    static StringBuilder Mixed() => new($"a{deseret}b");

    [Test]
    public async Task StringBuilder_EnumerateRunes()
    {
        var runes = Mixed().EnumerateRunes().Select(_ => _.Value).ToList();
        await Assert.That(runes).IsEquivalentTo(new[] { 'a', 0x10400, 'b' });
    }

    [Test]
    public async Task StringBuilder_EnumerateRunes_Empty()
    {
        var runes = new StringBuilder().EnumerateRunes().ToList();
        await Assert.That(runes).IsEmpty();
    }

    [Test]
    public async Task StringBuilder_EnumerateRunes_LoneSurrogate()
    {
        var builder = new StringBuilder().Append('a').Append('\uD800').Append('b');
        var runes = builder.EnumerateRunes().Select(_ => _.Value).ToList();
        // an unpaired surrogate is reported as the replacement character
        await Assert.That(runes).IsEquivalentTo(new[] { 'a', 0xFFFD, 'b' });
    }

    [Test]
    public async Task StringBuilder_GetRuneAt()
    {
        var builder = Mixed();
        await Assert.That(builder.GetRuneAt(0).Value).IsEqualTo('a');
        // the index of the high surrogate yields the whole scalar
        await Assert.That(builder.GetRuneAt(1).Value).IsEqualTo(0x10400);
        await Assert.That(builder.GetRuneAt(3).Value).IsEqualTo('b');
    }

    [Test]
    public async Task StringBuilder_GetRuneAt_NotAScalar()
    {
        var builder = Mixed();
        // index 2 is the low surrogate, so it does not begin a scalar
        await Assert.That(() => { builder.GetRuneAt(2); }).Throws<ArgumentException>();

        var lone = new StringBuilder().Append('a').Append('\uD800');
        await Assert.That(() => { lone.GetRuneAt(1); }).Throws<ArgumentException>();
    }

    [Test]
    public async Task StringBuilder_GetRuneAt_OutOfRange()
    {
        var builder = Mixed();
        await Assert.That(() => { builder.GetRuneAt(4); }).Throws<ArgumentOutOfRangeException>();
        await Assert.That(() => { builder.GetRuneAt(-1); }).Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task StringBuilder_TryGetRuneAt()
    {
        var builder = Mixed();
        var found = builder.TryGetRuneAt(1, out var rune);
        var value = rune.Value;
        await Assert.That(found).IsTrue();
        await Assert.That(value).IsEqualTo(0x10400);

        // a low surrogate returns false rather than throwing
        var midPair = builder.TryGetRuneAt(2, out var none);
        var noneValue = none.Value;
        await Assert.That(midPair).IsFalse();
        await Assert.That(noneValue).IsEqualTo(0);
    }

    // out of range throws even on the Try overload, matching net11
    [Test]
    public async Task StringBuilder_TryGetRuneAt_OutOfRange()
    {
        var builder = Mixed();
        await Assert.That(() => { builder.TryGetRuneAt(4, out _); }).Throws<ArgumentOutOfRangeException>();
        await Assert.That(() => { builder.TryGetRuneAt(-1, out _); }).Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task StringBuilder_Replace_Rune()
    {
        await Assert.That(new StringBuilder("abcabc").Replace(new Rune('b'), new Rune('Z')).ToString())
            .IsEqualTo("aZcaZc");
        // the replacement can be a different utf16 length
        await Assert.That(new StringBuilder("abc").Replace(new Rune('b'), new Rune(0x10400)).ToString())
            .IsEqualTo($"a{deseret}c");
        await Assert.That(new StringBuilder($"a{deseret}c").Replace(new Rune(0x10400), new Rune('Z')).ToString())
            .IsEqualTo("aZc");
    }

    [Test]
    public async Task StringBuilder_Replace_Rune_Range()
    {
        await Assert.That(new StringBuilder("abcabc").Replace(new Rune('b'), new Rune('Z'), 1, 3).ToString())
            .IsEqualTo("aZcabc");
        await Assert.That(() => { new StringBuilder("abc").Replace(new Rune('b'), new Rune('Z'), 1, 99); })
            .Throws<ArgumentOutOfRangeException>();
    }
}

#endif
