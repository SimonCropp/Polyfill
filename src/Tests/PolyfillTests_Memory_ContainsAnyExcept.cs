partial class PolyfillTests
{
    [Test]
    public Task ReadOnlySpan_ContainsAnyExcept_SingleValue_Found()
    {
        var span = new ReadOnlySpan<int>(new[] { 1, 1, 2, 1 });
        if (!span.ContainsAnyExcept(1))
        {
            throw new("Expected ContainsAnyExcept(1) to be true");
        }

        return Task.CompletedTask;
    }

    [Test]
    public Task ReadOnlySpan_ContainsAnyExcept_SingleValue_NotFound()
    {
        var span = new ReadOnlySpan<int>(new[] { 1, 1, 1 });
        if (span.ContainsAnyExcept(1))
        {
            throw new("Expected ContainsAnyExcept(1) to be false");
        }

        return Task.CompletedTask;
    }

    [Test]
    public Task ReadOnlySpan_ContainsAnyExcept_SingleValue_Empty()
    {
        var span = new ReadOnlySpan<int>(new int[0]);
        if (span.ContainsAnyExcept(1))
        {
            throw new("Expected ContainsAnyExcept on empty span to be false");
        }

        return Task.CompletedTask;
    }

    [Test]
    public Task ReadOnlySpan_ContainsAnyExcept_TwoValues()
    {
        var span = new ReadOnlySpan<int>(new[] { 1, 2, 1, 2 });
        if (span.ContainsAnyExcept(1, 2))
        {
            throw new("Expected ContainsAnyExcept(1, 2) to be false");
        }

        span = new ReadOnlySpan<int>(new[] { 1, 2, 3, 2 });
        if (!span.ContainsAnyExcept(1, 2))
        {
            throw new("Expected ContainsAnyExcept(1, 2) to be true");
        }

        return Task.CompletedTask;
    }

    [Test]
    public Task ReadOnlySpan_ContainsAnyExcept_ThreeValues()
    {
        var span = new ReadOnlySpan<int>(new[] { 1, 2, 3, 2, 1, 3 });
        if (span.ContainsAnyExcept(1, 2, 3))
        {
            throw new("Expected ContainsAnyExcept(1, 2, 3) to be false");
        }

        span = new ReadOnlySpan<int>(new[] { 1, 2, 3, 4 });
        if (!span.ContainsAnyExcept(1, 2, 3))
        {
            throw new("Expected ContainsAnyExcept(1, 2, 3) to be true");
        }

        return Task.CompletedTask;
    }

    [Test]
    public Task ReadOnlySpan_ContainsAnyExcept_ReadOnlySpanValues()
    {
        var span = new ReadOnlySpan<char>(new[] { 'a', 'b', 'c' });
        var values = new ReadOnlySpan<char>(new[] { 'a', 'b', 'c' });
        if (span.ContainsAnyExcept(values))
        {
            throw new("Expected ContainsAnyExcept to be false when all values match");
        }

        values = new ReadOnlySpan<char>(new[] { 'a', 'b' });
        if (!span.ContainsAnyExcept(values))
        {
            throw new("Expected ContainsAnyExcept to be true when 'c' is not excluded");
        }

        return Task.CompletedTask;
    }

    [Test]
    public Task Span_ContainsAnyExcept_SingleValue()
    {
        var span = new Span<int>(new[] { 1, 1, 2, 1 });
        if (!span.ContainsAnyExcept(1))
        {
            throw new("Expected Span ContainsAnyExcept(1) to be true");
        }

        span = new Span<int>(new[] { 1, 1, 1 });
        if (span.ContainsAnyExcept(1))
        {
            throw new("Expected Span ContainsAnyExcept(1) to be false");
        }

        return Task.CompletedTask;
    }
}
