partial class PolyfillTests
{
    [Test]
    public async Task ReadOnlySpan_LastIndexOfAnyExcept_SingleValue_Found()
    {
        var span = new ReadOnlySpan<int>(new[] { 1, 2, 1, 1 });
        var result = span.LastIndexOfAnyExcept(1);
        await Assert.That(result).IsEqualTo(1);
    }

    [Test]
    public async Task ReadOnlySpan_LastIndexOfAnyExcept_SingleValue_NotFound()
    {
        var span = new ReadOnlySpan<int>(new[] { 1, 1, 1 });
        var result = span.LastIndexOfAnyExcept(1);
        await Assert.That(result).IsEqualTo(-1);
    }

    [Test]
    public async Task ReadOnlySpan_LastIndexOfAnyExcept_SingleValue_Empty()
    {
        var span = new ReadOnlySpan<int>(new int[0]);
        var result = span.LastIndexOfAnyExcept(1);
        await Assert.That(result).IsEqualTo(-1);
    }

    [Test]
    public async Task ReadOnlySpan_LastIndexOfAnyExcept_TwoValues_NotFound()
    {
        var span = new ReadOnlySpan<int>(new[] { 1, 2, 1, 2 });
        var result = span.LastIndexOfAnyExcept(1, 2);
        await Assert.That(result).IsEqualTo(-1);
    }

    [Test]
    public async Task ReadOnlySpan_LastIndexOfAnyExcept_TwoValues_Found()
    {
        var span = new ReadOnlySpan<int>(new[] { 1, 3, 2, 1 });
        var result = span.LastIndexOfAnyExcept(1, 2);
        await Assert.That(result).IsEqualTo(1);
    }

    [Test]
    public async Task ReadOnlySpan_LastIndexOfAnyExcept_ThreeValues_NotFound()
    {
        var span = new ReadOnlySpan<int>(new[] { 1, 2, 3, 2, 1, 3 });
        var result = span.LastIndexOfAnyExcept(1, 2, 3);
        await Assert.That(result).IsEqualTo(-1);
    }

    [Test]
    public async Task ReadOnlySpan_LastIndexOfAnyExcept_ThreeValues_Found()
    {
        var span = new ReadOnlySpan<int>(new[] { 4, 1, 2, 3 });
        var result = span.LastIndexOfAnyExcept(1, 2, 3);
        await Assert.That(result).IsEqualTo(0);
    }

    [Test]
    public async Task ReadOnlySpan_LastIndexOfAnyExcept_ReadOnlySpanValues_AllExcluded()
    {
        var span = new ReadOnlySpan<char>(new[] { 'a', 'b', 'c' });
        var values = new ReadOnlySpan<char>(new[] { 'a', 'b', 'c' });
        var result = span.LastIndexOfAnyExcept(values);
        await Assert.That(result).IsEqualTo(-1);
    }

    [Test]
    public async Task ReadOnlySpan_LastIndexOfAnyExcept_ReadOnlySpanValues_PartialExcluded()
    {
        var span = new ReadOnlySpan<char>(new[] { 'a', 'b', 'c' });
        var values = new ReadOnlySpan<char>(new[] { 'b', 'c' });
        var result = span.LastIndexOfAnyExcept(values);
        await Assert.That(result).IsEqualTo(0);
    }

    [Test]
    public async Task Span_LastIndexOfAnyExcept_SingleValue_Found()
    {
        var span = new Span<int>(new[] { 1, 2, 1, 1 });
        var result = span.LastIndexOfAnyExcept(1);
        await Assert.That(result).IsEqualTo(1);
    }

    [Test]
    public async Task Span_LastIndexOfAnyExcept_SingleValue_NotFound()
    {
        var span = new Span<int>(new[] { 1, 1, 1 });
        var result = span.LastIndexOfAnyExcept(1);
        await Assert.That(result).IsEqualTo(-1);
    }
}
