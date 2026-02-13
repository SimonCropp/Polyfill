#if NETCOREAPP3_0_OR_GREATER

using System.Text;

partial class PolyfillTests
{
    [Test]
    public async Task StringContainsRune()
    {
        var rune = new Rune('l');
        await Assert.That("hello".Contains(rune)).IsTrue();
        await Assert.That("hello".Contains(new Rune('z'))).IsFalse();
    }

    [Test]
    public async Task StringContainsRuneComparison()
    {
        var rune = new Rune('H');
        await Assert.That("hello".Contains(rune, StringComparison.OrdinalIgnoreCase)).IsTrue();
        await Assert.That("hello".Contains(rune, StringComparison.Ordinal)).IsFalse();
    }

    [Test]
    public async Task StringIndexOfRune()
    {
        var rune = new Rune('l');
        await Assert.That("hello".IndexOf(rune)).IsEqualTo(2);
        await Assert.That("hello".IndexOf(new Rune('z'))).IsEqualTo(-1);
    }

    [Test]
    public async Task StringStartsWithRune()
    {
        await Assert.That("hello".StartsWith(new Rune('h'))).IsTrue();
        await Assert.That("hello".StartsWith(new Rune('z'))).IsFalse();
    }

    [Test]
    public async Task StringEndsWithRune()
    {
        await Assert.That("hello".EndsWith(new Rune('o'))).IsTrue();
        await Assert.That("hello".EndsWith(new Rune('z'))).IsFalse();
    }

    [Test]
    public async Task StringReplaceRune()
    {
        var result = "hello".Replace(new Rune('l'), new Rune('r'));
        await Assert.That(result).IsEqualTo("herro");
    }

    [Test]
    public async Task StringSplitRune()
    {
        var result = "a-b-c".Split(new Rune('-'));
        await Assert.That(result.Length).IsEqualTo(3);
        await Assert.That(result[0]).IsEqualTo("a");
        await Assert.That(result[1]).IsEqualTo("b");
        await Assert.That(result[2]).IsEqualTo("c");
    }

    [Test]
    public async Task StringTrimRune()
    {
        var result = "---hello---".Trim(new Rune('-'));
        await Assert.That(result).IsEqualTo("hello");
    }
}

#endif
