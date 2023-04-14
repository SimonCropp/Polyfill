partial class PolyfillExtensionsTests
{
    [Test]
    public void SpanContains()
    {
        Assert.True("value".AsSpan().Contains('e'));
        var span = new Span<char>("value".ToCharArray());
        Assert.True(span.Contains('e'));
    }

    [Test]
    public void SpanSequenceEqual()
    {
        Assert.True("value".AsSpan().SequenceEqual("value"));
        Assert.False("value".AsSpan().SequenceEqual("value2"));
        Assert.False("value".AsSpan().SequenceEqual("v"));
        var span = new Span<char>("value".ToCharArray());
        Assert.True(span.SequenceEqual("value"));
        Assert.False(span.SequenceEqual("value2"));
        Assert.False(span.SequenceEqual("v"));
    }

    [Test]
    public void SpanStartsWith()
    {
        Assert.True("value".AsSpan().StartsWith("value"));
        Assert.False("value".AsSpan().StartsWith("value2"));
        Assert.True("value".AsSpan().StartsWith("v"));
        var span = new Span<char>("value".ToCharArray());
        Assert.True(span.StartsWith("value"));
        Assert.False(span.StartsWith("value2"));
        Assert.True(span.StartsWith("val"));
    }

    [Test]
    public void SpanEndsWith()
    {
        Assert.True("value".AsSpan().EndsWith("value"));
        Assert.False("value".AsSpan().EndsWith("value2"));
        Assert.True("value".AsSpan().EndsWith("e"));
        var span = new Span<char>("value".ToCharArray());
        Assert.True(span.EndsWith("value"));
        Assert.False(span.EndsWith("value2"));
        Assert.True(span.EndsWith("lue"));
    }

    [Test]
    public void SpanStringBuilderAppend()
    {
        var builder = new StringBuilder();
        builder.Append("value".AsSpan());
        Assert.AreEqual("value", builder.ToString());
    }

    [Test]
    public void StringEqualsSpan()
    {
        var builder = new StringBuilder("value");
        Assert.IsTrue(builder.Equals("value".AsSpan()));
    }
}
