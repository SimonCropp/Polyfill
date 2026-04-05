#if FeatureMemory

public class StringConcatPolyfillTests
{
    [Test]
    public async Task Concat_StringSpan()
    {
        ReadOnlySpan<string?> values = new[] { "hello", " ", "world" };
        var result = string.Concat(values);
        await Assert.That(result).IsEqualTo("hello world");
    }

    [Test]
    public async Task Concat_ObjectSpan()
    {
        ReadOnlySpan<object?> values = new object[] { "count: ", 42 };
        var result = string.Concat(values);
        await Assert.That(result).IsEqualTo("count: 42");
    }
}

#endif
