partial class PolyExtensionsSample
{
    [Test]
    public void SpanContains() =>
        Assert.True("value".AsSpan().Contains('e'));

    [Test]
    public void SpanSequenceEqual() =>
        Assert.True("value".AsSpan().SequenceEqual("value"));

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
