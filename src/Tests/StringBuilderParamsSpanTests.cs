#if FeatureMemory

using System.Globalization;
using System.Text;

// Runs on every target, so net9.0 and later validate the BCL and everything below the polyfill.
// The array overloads are the oracle: they exist on every framework and produce the text these
// span overloads exist to produce without allocating.
public class StringBuilderParamsSpanTests
{
    [Test]
    public async Task AppendFormat_MatchesTheArrayOverload()
    {
        object?[] args = [1, "two", null, 4.5];

        await Assert.That(AppendFormatSpan("{0}|{1}|{2}|{3}", args))
            .IsEqualTo(new StringBuilder().AppendFormat("{0}|{1}|{2}|{3}", args).ToString());

        await Assert.That(AppendFormatSpan("no items", [])).IsEqualTo("no items");
    }

    [Test]
    public async Task AppendFormat_WithProvider_MatchesTheArrayOverload()
    {
        object?[] args = [1234.5];
        var german = CultureInfo.GetCultureInfo("de-DE");

        await Assert.That(AppendFormatSpan(german, "{0:N2}", args))
            .IsEqualTo(new StringBuilder().AppendFormat(german, "{0:N2}", args).ToString());

        // the provider is honoured rather than ignored
        await Assert.That(AppendFormatSpan(CultureInfo.InvariantCulture, "{0:N2}", args))
            .IsNotEqualTo(AppendFormatSpan(german, "{0:N2}", args));
    }

    [Test]
    public async Task AppendJoin_MatchesTheArrayOverloads()
    {
        object?[] objects = [1, null, "three"];
        string?[] strings = ["a", null, "c"];

        await Assert.That(AppendJoinSpan(',', objects)).IsEqualTo(new StringBuilder().AppendJoin(',', objects).ToString());
        await Assert.That(AppendJoinSpan(',', strings)).IsEqualTo(new StringBuilder().AppendJoin(',', strings).ToString());
        await Assert.That(AppendJoinSpan("--", objects)).IsEqualTo(new StringBuilder().AppendJoin("--", objects).ToString());
        await Assert.That(AppendJoinSpan("--", strings)).IsEqualTo(new StringBuilder().AppendJoin("--", strings).ToString());
    }

    [Test]
    public async Task AppendJoin_EmptyAndSingle()
    {
        await Assert.That(AppendJoinSpan(',', new string?[0])).IsEqualTo(string.Empty);
        await Assert.That(AppendJoinSpan(',', new string?[] { "only" })).IsEqualTo("only");
        await Assert.That(AppendJoinSpan(',', new object?[0])).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task AppendJoin_NullSeparatorBehavesAsEmpty()
    {
        string?[] strings = ["a", "b"];
        await Assert.That(AppendJoinSpan(null, strings)).IsEqualTo(new StringBuilder().AppendJoin(null, strings).ToString());
    }

    // A loose argument list binds to the array overload below net9.0 and to the span overload from
    // net9.0, so this is the regression guard that the two produce the same text either way.
    [Test]
    public async Task LooseArgumentListsStillWork()
    {
        await Assert.That(new StringBuilder().AppendFormat("{0}{1}{2}", 1, 2, 3).ToString()).IsEqualTo("123");
        await Assert.That(new StringBuilder().AppendJoin(',', "a", "b", "c").ToString()).IsEqualTo("a,b,c");
        await Assert.That(new StringBuilder().AppendJoin("-", 1, 2, 3).ToString()).IsEqualTo("1-2-3");
    }

    // the spans stay inside these helpers, so nothing ref struct shaped lives across an await
    static string AppendFormatSpan(string format, object?[] args) =>
        new StringBuilder().AppendFormat(format, (ReadOnlySpan<object?>) args).ToString();

    static string AppendFormatSpan(IFormatProvider provider, string format, object?[] args) =>
        new StringBuilder().AppendFormat(provider, format, (ReadOnlySpan<object?>) args).ToString();

    static string AppendJoinSpan(char separator, object?[] values) =>
        new StringBuilder().AppendJoin(separator, (ReadOnlySpan<object?>) values).ToString();

    static string AppendJoinSpan(char separator, string?[] values) =>
        new StringBuilder().AppendJoin(separator, (ReadOnlySpan<string?>) values).ToString();

    static string AppendJoinSpan(string? separator, object?[] values) =>
        new StringBuilder().AppendJoin(separator, (ReadOnlySpan<object?>) values).ToString();

    static string AppendJoinSpan(string? separator, string?[] values) =>
        new StringBuilder().AppendJoin(separator, (ReadOnlySpan<string?>) values).ToString();
}

#endif
