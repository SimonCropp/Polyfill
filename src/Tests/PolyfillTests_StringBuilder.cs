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
    public void Replace()
    {
        var builder = new StringBuilder("a");

        builder.Replace("a".AsSpan(), "b".AsSpan());
        Assert.AreEqual("b", builder.ToString());
    }

#if FeatureMemory

    [Test]
    public void GetChunks()
    {
        var builder = new StringBuilder("a",1);
        builder.Append("bb");
        var list = new List<string>();
        foreach (var chunk in builder.GetChunks())
        {
            list.Add(chunk.ToString());
        }

        Assert.AreEqual("a", list[0]);
        Assert.AreEqual("bb", list[1]);
    }

#endif

    [Test]
    public void Append()
    {
        var builder = new StringBuilder();

        var x = 10;
        Polyfill.Append(builder, $"value{x}");
        Assert.AreEqual("value10", builder.ToString());
    }

    [Test]
    public void AppendLine()
    {
        var builder = new StringBuilder();

        var x = 10;
        Polyfill.AppendLine(builder, $"value{x}");
        Assert.AreEqual("value10" + Environment.NewLine, builder.ToString());
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
    public void AppendLineWithFormat()
    {
        var builder = new StringBuilder();

        var x = 10;
        Polyfill.AppendLine(builder, null, $"value{x}");
        Assert.AreEqual("value10" + Environment.NewLine, builder.ToString());
    }

    [Test]
    public void AppendJoin()
    {
        var builder = new StringBuilder();

        string?[] span = ["value1", "value2"];
        builder.AppendJoin(",", span);
        Assert();
        builder.AppendJoin(",", new object[]{"value1", "value2"});
        Assert();
        builder.AppendJoin(',', span);
        Assert();
        builder.AppendJoin(',', new object[]{"value1", "value2"});
        Assert();
        // ReSharper disable once RedundantExplicitParamsArrayCreation
        builder.AppendJoin<string>(',', ["value1", "value2"]);
        Assert();

        void Assert()
        {
            NUnit.Framework.Assert.AreEqual("value1,value2", builder.ToString());
            builder.Clear();
        }
    }
}