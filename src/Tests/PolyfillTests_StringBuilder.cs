partial class PolyfillTests
{
    [Test]
    public async Task StringBuilderCopyTo()
    {
        var builder = new StringBuilder("value");

        var span = new Span<char>(new char[1]);
        builder.CopyTo(0, span, 1);
        await Assert.That(span is "v").IsTrue();

        span = new(new char[1]);
        builder.CopyTo(1, span, 1);
        await Assert.That(span is "a").IsTrue();

        span = new(new char[2]);
        builder.CopyTo(1, span, 2);
        await Assert.That(span is "al").IsTrue();

        span = new(new char[5]);
        builder.CopyTo(0, span, 5);
        await Assert.That(span is "value").IsTrue();
    }

    [Test]
    public async Task Replace()
    {
        var builder = new StringBuilder("a");

        builder.Replace("a".AsSpan(), "b".AsSpan());
        await Assert.That(builder.ToString()).IsEqualTo("b");
    }

#if FeatureMemory

    [Test]
    public async Task GetChunks()
    {
        var builder = new StringBuilder("a", 1);
        builder.Append("bb");
        var list = new List<string>();
        foreach (var chunk in builder.GetChunks())
        {
            list.Add(chunk.ToString());
        }

        await Assert.That(list[0]).IsEqualTo("a");
        await Assert.That(list[1]).IsEqualTo("bb");
    }

#endif

    [Test]
    public async Task Append()
    {
        var builder = new StringBuilder();

        var x = 10;
        Polyfill.Append(builder, $"value{x}");
        await Assert.That(builder.ToString()).IsEqualTo("value10");
    }

    [Test]
    public async Task AppendLine()
    {
        var builder = new StringBuilder();

        var x = 10;
        Polyfill.AppendLine(builder, $"value{x}");
        await Assert.That(builder.ToString()).IsEqualTo("value10" + Environment.NewLine);
    }

    [Test]
    public async Task AppendWithFormat()
    {
        var builder = new StringBuilder();

        var x = 10;
        Polyfill.Append(builder, null, $"value{x}");
        await Assert.That(builder.ToString()).IsEqualTo("value10");
    }

    [Test]
    public async Task AppendLineWithFormat()
    {
        var builder = new StringBuilder();

        var x = 10;
        Polyfill.AppendLine(builder, null, $"value{x}");
        await Assert.That(builder.ToString()).IsEqualTo("value10" + Environment.NewLine);
    }

    [Test]
    public async Task AppendJoin()
    {
        var builder = new StringBuilder();

        string?[] span = ["value1", "value2"];
        builder.AppendJoin(",", span);
        await AssertAsync();
        builder.AppendJoin(",", new object[] {"value1", "value2"});
        await AssertAsync();
        builder.AppendJoin(',', span);
        await AssertAsync();
        builder.AppendJoin(',', new object[] {"value1", "value2"});
        await AssertAsync();
        builder.AppendJoin(",", new object[] {"value1", "value2"}.Select(_ => _));
        await AssertAsync();
        builder.AppendJoin(',', new object[] {"value1", "value2"}.Select(_ => _));
        await AssertAsync();
        // ReSharper disable once RedundantExplicitParamsArrayCreation
        builder.AppendJoin<string>(',', ["value1", "value2"]);
        await AssertAsync();

        async Task AssertAsync()
        {
            await Assert.That(builder.ToString()).IsEqualTo("value1,value2");
            builder.Clear();
        }
    }
}
