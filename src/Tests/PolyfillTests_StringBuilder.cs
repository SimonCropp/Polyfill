// ReSharper disable PartialTypeWithSinglePart

partial class PolyfillTests
{
    [Test]
    public void StringBuilderCopyTo()
    {
        var builder = new StringBuilder("value");

        var span = new Span<char>(new char[1]);
        builder.CopyTo(0, span, 1);
        Assert.True(span is "v");

        span = new(new char[1]);
        builder.CopyTo(1, span, 1);
        Assert.True(span is "a");

        span = new(new char[2]);
        builder.CopyTo(1, span, 2);
        Assert.True(span is "al");

        span = new(new char[5]);
        builder.CopyTo(0, span, 5);
        Assert.True(span is "value");
    }

    [Test]
    public void Append()
    {
        var builder = new StringBuilder();

        var x = 10;
        Polyfill.Append(builder, $"value{x}");
        Assert.AreEqual("value10", builder.ToString());
    }

    [Test]
    public void AppendWithFormat()
    {
        var builder = new StringBuilder();

        var x = 10;
        Polyfill.Append(builder, null, $"value{x}");
        Assert.AreEqual("value10", builder.ToString());
    }

    [Test]
    public void AppendJoin()
    {
        var builder = new StringBuilder();

        builder.AppendJoin(",", ["value1", "value2"]);
        Assert();
        builder.AppendJoin(",", new object[]{"value1", "value2"});
        Assert();
        builder.AppendJoin(',', ["value1", "value2"]);
        Assert();
        builder.AppendJoin(',', new object[]{"value1", "value2"});
        Assert();
        builder.AppendJoin<string>(',', ["value1", "value2"]);
        Assert();

        void Assert()
        {
            NUnit.Framework.Assert.AreEqual("value1,value2", builder.ToString());
            builder.Clear();
        }
    }
}