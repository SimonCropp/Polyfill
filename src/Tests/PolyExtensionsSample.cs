[TestFixture]
[DebuggerNonUserCode]
class PolyExtensionsSample
{
#if MEMORYREFERENCED
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

    [Test]
    public async Task StreamReaderReadAsync()
    {
        using var stream = new MemoryStream("value"u8.ToArray());
        var result = new char[5];
        var memory = new Memory<char>(result);
        using var reader = new StreamReader(stream);
        var read = await reader.ReadAsync(memory);
        Assert.AreEqual(5, read);
        Assert.IsTrue("value".SequenceEqual(result));
    }

    [Test]
    public async Task StreamReadAsync()
    {
        var input = new byte[]{1,2};
        using var stream = new MemoryStream(input);
        var result = new byte[2];
        var memory = new Memory<byte>(result);
        var read = await stream.ReadAsync(memory);
        Assert.AreEqual(2, read);
        Assert.IsTrue(input.SequenceEqual(result));
    }

#endif

#if (NET46X && VALUETUPLEREFERENCED) || NET47X ||NET48X || NETSTANDARD2_0 || NETCOREAPP2X

    [Test]
    public void Deconstruct()
    {
        var (a, b) = MethodWithNamedTuple();
        Assert.AreEqual("", a);
        Assert.AreEqual(0, b);
    }

    static (string a, int b) MethodWithNamedTuple() =>
        ("", 0);

#endif

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
