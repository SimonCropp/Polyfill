[TestFixture]
[DebuggerNonUserCode]
class PolyExtensionsSample
{
#if !PolyOmitMemoryExtensions
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
#endif

    [Test]
    public void Deconstruct()
    {
        var (a, b) = MethodWithNamedTuple();
        Assert.AreEqual("", a);
        Assert.AreEqual(0, b);
    }

    static (string a, int b) MethodWithNamedTuple() =>
        ("", 0);

    [Test]
    public void EndsWith()
    {
        Assert.True("value".EndsWith('e'));
        Assert.False("".EndsWith('e'));
    }

    [Test]
    public void StringContainsStringComparison() =>
        Assert.True("value".Contains("E", StringComparison.OrdinalIgnoreCase));

    [Test]
    public void StartsWith()
    {
        Assert.True("value".StartsWith('v'));
        Assert.False("".StartsWith('v'));
    }
}
