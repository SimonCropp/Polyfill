[TestFixture]
public class GuidPolyfillTests
{
    [Test]
    public void TryParse_ReturnsTrue_ForValidGuid()
    {
        var guidString = Guid.NewGuid().ToString();
        var result = Guid.TryParse(guidString, null, out var guid);
        Assert.IsTrue(result);
        Assert.AreNotEqual(Guid.Empty, guid);
    }

    [Test]
    public void TryParse_ReturnsFalse_ForInvalidGuid()
    {
        var result = Guid.TryParse("not-a-guid", null, out var guid);
        Assert.IsFalse(result);
        Assert.AreEqual(Guid.Empty, guid);
    }

#if FeatureMemory
    [Test]
    public void TryParse_Span_ReturnsTrue_ForValidGuid()
    {
        var guid = Guid.NewGuid();
        var span = guid.ToString().AsSpan();
        var result = Guid.TryParse(span, null, out var parsed);
        Assert.IsTrue(result);
        Assert.AreEqual(guid, parsed);
    }

    [Test]
    public void TryParse_Span_ReturnsFalse_ForInvalidGuid()
    {
        var span = "invalid".AsSpan();
        var result = Guid.TryParse(span, null, out var parsed);
        Assert.IsFalse(result);
        Assert.AreEqual(Guid.Empty, parsed);
    }

    [Test]
    public void TryParseExact_Span_ReturnsTrue_ForExactFormat()
    {
        var guid = Guid.NewGuid();
        var span = guid.ToString("N").AsSpan();
        var format = "N".AsSpan();
        var result = Guid.TryParseExact(span, format, out var parsed);
        Assert.IsTrue(result);
        Assert.AreEqual(guid, parsed);
    }
#endif

    [Test]
    public void CreateVersion7_ReturnsUniqueGuids()
    {
        var guid1 = Guid.CreateVersion7();
        var guid2 = Guid.CreateVersion7();
        Assert.AreNotEqual(guid1, guid2);
    }

    [Test]
    public void CreateVersion7_WithTimestamp_ReturnsDeterministicPrefix()
    {
        var timestamp = DateTimeOffset.UtcNow;
        var guid1 = Guid.CreateVersion7(timestamp);
        var guid2 = Guid.CreateVersion7(timestamp);
        // The time-based prefix should match, but random part will differ
        Assert.AreNotEqual(guid1, guid2);
        Assert.AreEqual(guid1.ToByteArray().Take(6), guid2.ToByteArray().Take(6));
    }
}