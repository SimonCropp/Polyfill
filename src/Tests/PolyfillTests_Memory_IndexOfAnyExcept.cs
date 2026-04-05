partial class PolyfillTests
{
    [Test]
    public async Task ReadOnlySpan_IndexOfAnyExcept_SingleValue_Found()
    {
        var span = new ReadOnlySpan<int>(new[] { 1, 1, 2, 1 });
        var result = span.IndexOfAnyExcept(1);
        await Assert.That(result).IsEqualTo(2);
    }

    [Test]
    public async Task ReadOnlySpan_IndexOfAnyExcept_SingleValue_NotFound()
    {
        var span = new ReadOnlySpan<int>(new[] { 1, 1, 1 });
        var result = span.IndexOfAnyExcept(1);
        await Assert.That(result).IsEqualTo(-1);
    }

    [Test]
    public async Task ReadOnlySpan_IndexOfAnyExcept_SingleValue_Empty()
    {
        var span = new ReadOnlySpan<int>(new int[0]);
        var result = span.IndexOfAnyExcept(1);
        await Assert.That(result).IsEqualTo(-1);
    }

    [Test]
    public async Task ReadOnlySpan_IndexOfAnyExcept_TwoValues_NotFound()
    {
        var span = new ReadOnlySpan<int>(new[] { 1, 2, 1, 2 });
        var result = span.IndexOfAnyExcept(1, 2);
        await Assert.That(result).IsEqualTo(-1);
    }

    [Test]
    public async Task ReadOnlySpan_IndexOfAnyExcept_TwoValues_Found()
    {
        var span = new ReadOnlySpan<int>(new[] { 1, 2, 3, 2 });
        var result = span.IndexOfAnyExcept(1, 2);
        await Assert.That(result).IsEqualTo(2);
    }

    [Test]
    public async Task ReadOnlySpan_IndexOfAnyExcept_ThreeValues_NotFound()
    {
        var span = new ReadOnlySpan<int>(new[] { 1, 2, 3, 2, 1, 3 });
        var result = span.IndexOfAnyExcept(1, 2, 3);
        await Assert.That(result).IsEqualTo(-1);
    }

    [Test]
    public async Task ReadOnlySpan_IndexOfAnyExcept_ThreeValues_Found()
    {
        var span = new ReadOnlySpan<int>(new[] { 1, 2, 3, 4 });
        var result = span.IndexOfAnyExcept(1, 2, 3);
        await Assert.That(result).IsEqualTo(3);
    }

    [Test]
    public async Task ReadOnlySpan_IndexOfAnyExcept_ReadOnlySpanValues_AllExcluded()
    {
        var span = new ReadOnlySpan<char>(new[] { 'a', 'b', 'c' });
        var values = new ReadOnlySpan<char>(new[] { 'a', 'b', 'c' });
        var result = span.IndexOfAnyExcept(values);
        await Assert.That(result).IsEqualTo(-1);
    }

    [Test]
    public async Task ReadOnlySpan_IndexOfAnyExcept_ReadOnlySpanValues_PartialExcluded()
    {
        var span = new ReadOnlySpan<char>(new[] { 'a', 'b', 'c' });
        var values = new ReadOnlySpan<char>(new[] { 'a', 'b' });
        var result = span.IndexOfAnyExcept(values);
        await Assert.That(result).IsEqualTo(2);
    }

    [Test]
    public async Task Span_IndexOfAnyExcept_SingleValue_Found()
    {
        var span = new Span<int>(new[] { 1, 1, 2, 1 });
        var result = span.IndexOfAnyExcept(1);
        await Assert.That(result).IsEqualTo(2);
    }

    [Test]
    public async Task Span_IndexOfAnyExcept_SingleValue_NotFound()
    {
        var span = new Span<int>(new[] { 1, 1, 1 });
        var result = span.IndexOfAnyExcept(1);
        await Assert.That(result).IsEqualTo(-1);
    }
}
