partial class PolyfillTests
{
    [Test]
    public Task ReadOnlySpan_ContainsAnyInRange_Found()
    {
        var span = new ReadOnlySpan<int>(new[] { 1, 5, 10 });
        if (!span.ContainsAnyInRange(3, 7))
        {
            throw new("Expected ContainsAnyInRange(3, 7) to be true");
        }

        return Task.CompletedTask;
    }

    [Test]
    public Task ReadOnlySpan_ContainsAnyInRange_NotFound()
    {
        var span = new ReadOnlySpan<int>(new[] { 1, 2, 8, 9 });
        if (span.ContainsAnyInRange(3, 7))
        {
            throw new("Expected ContainsAnyInRange(3, 7) to be false");
        }

        return Task.CompletedTask;
    }

    [Test]
    public Task ReadOnlySpan_ContainsAnyInRange_BoundaryInclusive()
    {
        var span = new ReadOnlySpan<int>(new[] { 3 });
        if (!span.ContainsAnyInRange(3, 7))
        {
            throw new("Expected lower bound to be inclusive");
        }

        span = new ReadOnlySpan<int>(new[] { 7 });
        if (!span.ContainsAnyInRange(3, 7))
        {
            throw new("Expected upper bound to be inclusive");
        }

        return Task.CompletedTask;
    }

    [Test]
    public Task ReadOnlySpan_ContainsAnyInRange_Empty()
    {
        var span = new ReadOnlySpan<int>(new int[0]);
        if (span.ContainsAnyInRange(3, 7))
        {
            throw new("Expected ContainsAnyInRange on empty span to be false");
        }

        return Task.CompletedTask;
    }

    [Test]
    public Task Span_ContainsAnyInRange()
    {
        var span = new Span<int>(new[] { 1, 5, 10 });
        if (!span.ContainsAnyInRange(3, 7))
        {
            throw new("Expected Span ContainsAnyInRange(3, 7) to be true");
        }

        return Task.CompletedTask;
    }

    [Test]
    public Task ReadOnlySpan_ContainsAnyExceptInRange_Found()
    {
        var span = new ReadOnlySpan<int>(new[] { 3, 5, 7 });
        if (span.ContainsAnyExceptInRange(3, 7))
        {
            throw new("Expected ContainsAnyExceptInRange(3, 7) to be false");
        }

        span = new ReadOnlySpan<int>(new[] { 3, 5, 8 });
        if (!span.ContainsAnyExceptInRange(3, 7))
        {
            throw new("Expected ContainsAnyExceptInRange(3, 7) to be true");
        }

        return Task.CompletedTask;
    }

    [Test]
    public Task ReadOnlySpan_ContainsAnyExceptInRange_BelowRange()
    {
        var span = new ReadOnlySpan<int>(new[] { 2, 5, 6 });
        if (!span.ContainsAnyExceptInRange(3, 7))
        {
            throw new("Expected value below range to be detected");
        }

        return Task.CompletedTask;
    }

    [Test]
    public Task ReadOnlySpan_ContainsAnyExceptInRange_Empty()
    {
        var span = new ReadOnlySpan<int>(new int[0]);
        if (span.ContainsAnyExceptInRange(3, 7))
        {
            throw new("Expected ContainsAnyExceptInRange on empty span to be false");
        }

        return Task.CompletedTask;
    }

    [Test]
    public Task Span_ContainsAnyExceptInRange()
    {
        var span = new Span<int>(new[] { 3, 5, 8 });
        if (!span.ContainsAnyExceptInRange(3, 7))
        {
            throw new("Expected Span ContainsAnyExceptInRange(3, 7) to be true");
        }

        return Task.CompletedTask;
    }

    [Test]
    public Task ReadOnlySpan_ContainsAnyInRange_Char()
    {
        var span = new ReadOnlySpan<char>(new[] { 'a', 'z', '5' });
        if (!span.ContainsAnyInRange('0', '9'))
        {
            throw new("Expected to find digit in range");
        }

        span = new ReadOnlySpan<char>(new[] { 'a', 'z' });
        if (span.ContainsAnyInRange('0', '9'))
        {
            throw new("Expected no digit in range");
        }

        return Task.CompletedTask;
    }
}
