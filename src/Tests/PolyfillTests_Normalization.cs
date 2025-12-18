partial class PolyfillTests
{
    [Test]
    public async Task GetNormalizedLength_ShouldReturnCorrectLength()
    {
        var span = "Café".AsSpan();
        var normalizedLength = span.GetNormalizedLength(NormalizationForm.FormC);

        await Assert.That(normalizedLength).IsEqualTo(4);
    }

    [Test]
    public Task TryNormalize_ShouldNormalizeSuccessfully()
    {
        var source = "Café".AsSpan();
        Span<char> destination = new char[10];
        var success = source.TryNormalize(destination, out var charsWritten, NormalizationForm.FormC);

        if (!success)
            throw new Exception("Expected success to be true");
        if (charsWritten != 4)
            throw new Exception($"Expected charsWritten 4 but got {charsWritten}");
        if (destination.Slice(0, charsWritten).ToString() != "Café")
            throw new Exception("Expected 'Café'");
        return Task.CompletedTask;
    }

    [Test]
    public Task TryNormalize_ShouldFailWhenDestinationTooSmall()
    {
        var source = "Café".AsSpan();
        Span<char> destination = new char[2];
        var success = source.TryNormalize(destination, out var charsWritten, NormalizationForm.FormC);

        if (success)
            throw new Exception("Expected success to be false");
        if (charsWritten != 0)
            throw new Exception($"Expected charsWritten 0 but got {charsWritten}");
        return Task.CompletedTask;
    }
}
