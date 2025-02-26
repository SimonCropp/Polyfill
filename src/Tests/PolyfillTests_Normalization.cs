partial class PolyfillTests
{
    [Test]
    public void GetNormalizedLength_ShouldReturnCorrectLength()
    {
        var span = "Café".AsSpan();
        var normalizedLength = span.GetNormalizedLength(NormalizationForm.FormC);

        Assert.AreEqual(4, normalizedLength);
    }

    [Test]
    public void TryNormalize_ShouldNormalizeSuccessfully()
    {
        var source = "Café".AsSpan();
        Span<char> destination = new char[10];
        var success = source.TryNormalize(destination, out var charsWritten, NormalizationForm.FormC);

        Assert.IsTrue(success);
        Assert.AreEqual(4, charsWritten);
        Assert.AreEqual("Café", destination.Slice(0, charsWritten).ToString());
    }

    [Test]
    public void TryNormalize_ShouldFailWhenDestinationTooSmall()
    {
        var source = "Café".AsSpan();
        Span<char> destination = new char[2];
        var success = source.TryNormalize(destination, out var charsWritten, NormalizationForm.FormC);

        Assert.IsFalse(success);
        Assert.AreEqual(0, charsWritten);
    }
}