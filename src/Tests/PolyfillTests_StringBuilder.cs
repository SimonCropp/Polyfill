// ReSharper disable PartialTypeWithSinglePart

partial class PolyfillTests
{
    [Test]
    public void StringBuilderCopyTo()
    {
        var builder = new StringBuilder("value");

        var span = new Span<char>(new char[1]);
        builder.CopyTo(0, span, 1);
        Assert.True(span.SequenceEqual("v"));

        span = new(new char[1]);
        builder.CopyTo(1, span, 1);
        Assert.True(span.SequenceEqual("a"));

        span = new(new char[2]);
        builder.CopyTo(1, span, 2);
        Assert.True(span.SequenceEqual("al"));

        span = new(new char[5]);
        builder.CopyTo(0, span, 5);
        Assert.True(span.SequenceEqual("value"));
    }

    [Test]
    public void Append()
    {
        var builder = new StringBuilder();

        var x = 10;
        PolyfillExtensions.Append(builder, $"value{x}");
        Assert.AreEqual("value10", builder.ToString());
    }

    [Test]
    public void AppendWithFormat()
    {
        var builder = new StringBuilder();

        var x = 10;
        PolyfillExtensions.Append(builder, null, $"value{x}");
        Assert.AreEqual("value10", builder.ToString());
    }
}