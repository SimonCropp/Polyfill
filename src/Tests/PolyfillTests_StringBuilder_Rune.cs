#if NETCOREAPP3_0_OR_GREATER

using System.Text;

partial class PolyfillTests
{
    [Test]
    public async Task StringBuilderAppendRune()
    {
        var builder = new StringBuilder("hello");
        builder.Append(new Rune('!'));
        await Assert.That(builder.ToString()).IsEqualTo("hello!");
    }

    [Test]
    public async Task StringBuilderInsertRune()
    {
        var builder = new StringBuilder("hllo");
        builder.Insert(1, new Rune('e'));
        await Assert.That(builder.ToString()).IsEqualTo("hello");
    }

    [Test]
    public async Task StringBuilderReplaceRune()
    {
        var builder = new StringBuilder("hello");
        builder.Replace(new Rune('l'), new Rune('r'));
        await Assert.That(builder.ToString()).IsEqualTo("herro");
    }
}

#endif
