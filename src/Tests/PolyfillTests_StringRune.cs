#if NETCOREAPP3_0_OR_GREATER

using System.Text;

partial class PolyfillTests
{
    // U+10400 DESERET CAPITAL LETTER LONG I, encoded as the surrogate pair D801 DC00
    static readonly Rune deseretRune = new(0x10400);

    // a, high, low, b, high, low
    static readonly string runeMixed = $"a{deseret}b{deseret}";

    [Test]
    public async Task String_Rune_Contains()
    {
        await Assert.That(runeMixed.Contains(deseretRune)).IsTrue();
        await Assert.That("ab".Contains(deseretRune)).IsFalse();
        await Assert.That("abc".Contains(new Rune('b'))).IsTrue();
        await Assert.That("abc".Contains(new Rune('x'))).IsFalse();
        // a lone high surrogate is not the rune
        await Assert.That("a\uD801".Contains(deseretRune)).IsFalse();
    }

    [Test]
    public async Task String_Rune_Contains_StringComparison()
    {
        await Assert.That("ABC".Contains(new Rune('b'), StringComparison.OrdinalIgnoreCase)).IsTrue();
        await Assert.That("ABC".Contains(new Rune('b'), StringComparison.Ordinal)).IsFalse();
        await Assert.That(runeMixed.Contains(deseretRune, StringComparison.Ordinal)).IsTrue();
        await Assert.That("ab".Contains(deseretRune, StringComparison.Ordinal)).IsFalse();
    }

    [Test]
    public async Task String_Rune_StartsWith()
    {
        await Assert.That($"{deseret}a".StartsWith(deseretRune)).IsTrue();
        await Assert.That($"a{deseret}".StartsWith(deseretRune)).IsFalse();
        await Assert.That("\uD801".StartsWith(deseretRune)).IsFalse();
        await Assert.That("".StartsWith(deseretRune)).IsFalse();
        await Assert.That("abc".StartsWith(new Rune('a'))).IsTrue();
        await Assert.That("abc".StartsWith(new Rune('b'))).IsFalse();
    }

    [Test]
    public async Task String_Rune_StartsWith_StringComparison()
    {
        await Assert.That("ABC".StartsWith(new Rune('a'), StringComparison.OrdinalIgnoreCase)).IsTrue();
        await Assert.That("ABC".StartsWith(new Rune('a'), StringComparison.Ordinal)).IsFalse();
        await Assert.That($"{deseret}a".StartsWith(deseretRune, StringComparison.Ordinal)).IsTrue();
        await Assert.That("".StartsWith(deseretRune, StringComparison.Ordinal)).IsFalse();
    }

    [Test]
    public async Task String_Rune_EndsWith()
    {
        await Assert.That($"a{deseret}".EndsWith(deseretRune)).IsTrue();
        await Assert.That($"{deseret}a".EndsWith(deseretRune)).IsFalse();
        await Assert.That("\uDC00".EndsWith(deseretRune)).IsFalse();
        await Assert.That("".EndsWith(deseretRune)).IsFalse();
        await Assert.That("abc".EndsWith(new Rune('c'))).IsTrue();
        await Assert.That("abc".EndsWith(new Rune('b'))).IsFalse();
    }

    [Test]
    public async Task String_Rune_EndsWith_StringComparison()
    {
        await Assert.That("ABC".EndsWith(new Rune('c'), StringComparison.OrdinalIgnoreCase)).IsTrue();
        await Assert.That("ABC".EndsWith(new Rune('c'), StringComparison.Ordinal)).IsFalse();
        await Assert.That($"a{deseret}".EndsWith(deseretRune, StringComparison.Ordinal)).IsTrue();
        await Assert.That("".EndsWith(deseretRune, StringComparison.Ordinal)).IsFalse();
    }

    [Test]
    public async Task String_Rune_IndexOf()
    {
        await Assert.That(runeMixed.IndexOf(deseretRune)).IsEqualTo(1);
        await Assert.That(runeMixed.IndexOf(new Rune('b'))).IsEqualTo(3);
        await Assert.That("ab".IndexOf(deseretRune)).IsEqualTo(-1);
    }

    [Test]
    public async Task String_Rune_IndexOf_StartIndex()
    {
        await Assert.That(runeMixed.IndexOf(deseretRune, 1)).IsEqualTo(1);
        await Assert.That(runeMixed.IndexOf(deseretRune, 2)).IsEqualTo(4);
        // startIndex equal to the length is allowed
        await Assert.That(runeMixed.IndexOf(deseretRune, runeMixed.Length)).IsEqualTo(-1);
        await Assert.That(() => runeMixed.IndexOf(deseretRune, -1)).Throws<ArgumentOutOfRangeException>();
        await Assert.That(() => runeMixed.IndexOf(deseretRune, runeMixed.Length + 1)).Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task String_Rune_IndexOf_StartIndexCount()
    {
        await Assert.That(runeMixed.IndexOf(deseretRune, 1, 2)).IsEqualTo(1);
        // the window holds only the high surrogate
        await Assert.That(runeMixed.IndexOf(deseretRune, 1, 1)).IsEqualTo(-1);
        await Assert.That(runeMixed.IndexOf(deseretRune, 2, 4)).IsEqualTo(4);
        await Assert.That(() => runeMixed.IndexOf(deseretRune, 0, -1)).Throws<ArgumentOutOfRangeException>();
        await Assert.That(() => runeMixed.IndexOf(deseretRune, 2, 5)).Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task String_Rune_IndexOf_StringComparison()
    {
        await Assert.That(runeMixed.IndexOf(new Rune('B'), StringComparison.OrdinalIgnoreCase)).IsEqualTo(3);
        await Assert.That(runeMixed.IndexOf(new Rune('B'), StringComparison.Ordinal)).IsEqualTo(-1);
        await Assert.That(runeMixed.IndexOf(deseretRune, StringComparison.Ordinal)).IsEqualTo(1);
    }

    [Test]
    public async Task String_Rune_IndexOf_StartIndexStringComparison()
    {
        await Assert.That(runeMixed.IndexOf(deseretRune, 2, StringComparison.Ordinal)).IsEqualTo(4);
        await Assert.That("aBcB".IndexOf(new Rune('b'), 2, StringComparison.OrdinalIgnoreCase)).IsEqualTo(3);
        await Assert.That(() => runeMixed.IndexOf(deseretRune, -1, StringComparison.Ordinal)).Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task String_Rune_IndexOf_StartIndexCountStringComparison()
    {
        await Assert.That(runeMixed.IndexOf(deseretRune, 0, 3, StringComparison.Ordinal)).IsEqualTo(1);
        await Assert.That(runeMixed.IndexOf(deseretRune, 0, 2, StringComparison.Ordinal)).IsEqualTo(-1);
        await Assert.That("aBcB".IndexOf(new Rune('b'), 2, 2, StringComparison.OrdinalIgnoreCase)).IsEqualTo(3);
        await Assert.That(() => runeMixed.IndexOf(deseretRune, 4, 3, StringComparison.Ordinal)).Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task String_Rune_LastIndexOf()
    {
        await Assert.That(runeMixed.LastIndexOf(deseretRune)).IsEqualTo(4);
        await Assert.That(runeMixed.LastIndexOf(new Rune('a'))).IsEqualTo(0);
        await Assert.That("ab".LastIndexOf(deseretRune)).IsEqualTo(-1);
        await Assert.That("".LastIndexOf(deseretRune)).IsEqualTo(-1);
    }

    [Test]
    public async Task String_Rune_LastIndexOf_StartIndex()
    {
        await Assert.That(runeMixed.LastIndexOf(deseretRune, 5)).IsEqualTo(4);
        // the window ends on the high surrogate at index 4, so the pair at 4 is excluded
        await Assert.That(runeMixed.LastIndexOf(deseretRune, 4)).IsEqualTo(1);
        // an empty string reports not found before validating startIndex
        await Assert.That("".LastIndexOf(deseretRune, 5)).IsEqualTo(-1);
        await Assert.That(() => runeMixed.LastIndexOf(deseretRune, runeMixed.Length)).Throws<ArgumentOutOfRangeException>();
        await Assert.That(() => runeMixed.LastIndexOf(deseretRune, -1)).Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task String_Rune_LastIndexOf_StartIndexCount()
    {
        await Assert.That(runeMixed.LastIndexOf(deseretRune, 5, 2)).IsEqualTo(4);
        await Assert.That(runeMixed.LastIndexOf(deseretRune, 5, 1)).IsEqualTo(-1);
        await Assert.That(runeMixed.LastIndexOf(deseretRune, 3, 3)).IsEqualTo(1);
        await Assert.That(() => runeMixed.LastIndexOf(deseretRune, 2, 4)).Throws<ArgumentOutOfRangeException>();
        await Assert.That(() => runeMixed.LastIndexOf(deseretRune, 2, -1)).Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task String_Rune_LastIndexOf_StringComparison()
    {
        await Assert.That(runeMixed.LastIndexOf(new Rune('A'), StringComparison.OrdinalIgnoreCase)).IsEqualTo(0);
        await Assert.That(runeMixed.LastIndexOf(new Rune('A'), StringComparison.Ordinal)).IsEqualTo(-1);
        await Assert.That(runeMixed.LastIndexOf(deseretRune, StringComparison.Ordinal)).IsEqualTo(4);
        await Assert.That("".LastIndexOf(deseretRune, StringComparison.Ordinal)).IsEqualTo(-1);
    }

    [Test]
    public async Task String_Rune_LastIndexOf_StartIndexStringComparison()
    {
        await Assert.That(runeMixed.LastIndexOf(deseretRune, 3, StringComparison.Ordinal)).IsEqualTo(1);
        await Assert.That("BaBc".LastIndexOf(new Rune('b'), 1, StringComparison.OrdinalIgnoreCase)).IsEqualTo(0);
        await Assert.That(() => runeMixed.LastIndexOf(deseretRune, runeMixed.Length, StringComparison.Ordinal)).Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task String_Rune_LastIndexOf_StartIndexCountStringComparison()
    {
        await Assert.That(runeMixed.LastIndexOf(deseretRune, 5, 3, StringComparison.Ordinal)).IsEqualTo(4);
        await Assert.That(runeMixed.LastIndexOf(deseretRune, 3, 2, StringComparison.Ordinal)).IsEqualTo(-1);
        await Assert.That("BaBc".LastIndexOf(new Rune('b'), 3, 2, StringComparison.OrdinalIgnoreCase)).IsEqualTo(2);
        await Assert.That(() => runeMixed.LastIndexOf(deseretRune, 1, 3, StringComparison.Ordinal)).Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task String_Rune_Replace()
    {
        await Assert.That(runeMixed.Replace(deseretRune, new Rune('x'))).IsEqualTo("axbx");
        await Assert.That("abc".Replace(new Rune('b'), deseretRune)).IsEqualTo($"a{deseret}c");
        await Assert.That("abc".Replace(new Rune('b'), new Rune('x'))).IsEqualTo("axc");
        await Assert.That("abc".Replace(deseretRune, new Rune('x'))).IsEqualTo("abc");
    }

    [Test]
    public async Task String_Rune_Split()
    {
        await Assert.That(runeMixed.Split(deseretRune)).IsEquivalentTo(new[] { "a", "b", "" });
        await Assert.That(runeMixed.Split(deseretRune, StringSplitOptions.RemoveEmptyEntries)).IsEquivalentTo(new[] { "a", "b" });
        await Assert.That("a,b".Split(new Rune(','))).IsEquivalentTo(new[] { "a", "b" });
        await Assert.That("ab".Split(deseretRune)).IsEquivalentTo(new[] { "ab" });
    }

    [Test]
    public async Task String_Rune_Split_Count()
    {
        await Assert.That(runeMixed.Split(deseretRune, 2)).IsEquivalentTo(new[] { "a", $"b{deseret}" });
        await Assert.That(runeMixed.Split(deseretRune, 1)).IsEquivalentTo(new[] { runeMixed });
        await Assert.That(runeMixed.Split(deseretRune, 0)).IsEmpty();
        await Assert.That("a,b,c".Split(new Rune(','), 2)).IsEquivalentTo(new[] { "a", "b,c" });
        await Assert.That(() => runeMixed.Split(deseretRune, -1)).Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task String_Rune_Trim()
    {
        await Assert.That($"{deseret}{deseret}a{deseret}".Trim(deseretRune)).IsEqualTo("a");
        await Assert.That($"{deseret}{deseret}".Trim(deseretRune)).IsEqualTo("");
        await Assert.That("".Trim(deseretRune)).IsEqualTo("");
        // a lone surrogate is not trimmed
        await Assert.That("\uD801a\uDC00".Trim(deseretRune)).IsEqualTo("\uD801a\uDC00");
        await Assert.That("xxaxx".Trim(new Rune('x'))).IsEqualTo("a");
    }

    [Test]
    public async Task String_Rune_TrimStart()
    {
        await Assert.That($"{deseret}{deseret}a{deseret}".TrimStart(deseretRune)).IsEqualTo($"a{deseret}");
        await Assert.That($"{deseret}".TrimStart(deseretRune)).IsEqualTo("");
        await Assert.That("\uD801a".TrimStart(deseretRune)).IsEqualTo("\uD801a");
        await Assert.That("xxax".TrimStart(new Rune('x'))).IsEqualTo("ax");
    }

    [Test]
    public async Task String_Rune_TrimEnd()
    {
        await Assert.That($"{deseret}a{deseret}{deseret}".TrimEnd(deseretRune)).IsEqualTo($"{deseret}a");
        await Assert.That($"{deseret}".TrimEnd(deseretRune)).IsEqualTo("");
        await Assert.That("a\uDC00".TrimEnd(deseretRune)).IsEqualTo("a\uDC00");
        await Assert.That("xaxx".TrimEnd(new Rune('x'))).IsEqualTo("xa");
    }
}

#endif
