partial class PolyfillTests
{
    [Test]
    public async Task GetHashCodeStringComparison()
    {
        var hash1 = "value".GetHashCode(StringComparison.Ordinal);
        await Assert.That(hash1).IsNotEqualTo(0);

        var hash2 = string.GetHashCode("value".AsSpan());
        await Assert.That(hash2).IsNotEqualTo(0);

        var hash3 = string.GetHashCode("value".AsSpan(), StringComparison.Ordinal);
        await Assert.That(hash3).IsNotEqualTo(0);
    }

    [Test]
    public async Task EndsWith()
    {
        await Assert.That("value".EndsWith('e')).IsTrue();
        await Assert.That("e".EndsWith('e')).IsTrue();
        await Assert.That("".EndsWith('e')).IsFalse();
    }

    [Test]
    public async Task ReplaceLineEndings()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            await Assert.That("a\rb\nc\r\nd".ReplaceLineEndings()).IsEqualTo("a\r\nb\r\nc\r\nd");
        }
        await Assert.That("a\rb\nc\r\nd".ReplaceLineEndings("_")).IsEqualTo("a_b_c_d");
    }

    [Test]
    public Task CopyTo()
    {
        var span = new Span<char>(new char[1]);
        "a".CopyTo(span);
        if (span.ToString() != "a")
            throw new Exception($"Expected 'a' but got '{span.ToString()}'");
        return Task.CompletedTask;
    }

    [Test]
    public Task TryCopyTo()
    {
        var span = new Span<char>(new char[1]);
        if (!"a".TryCopyTo(span))
            throw new Exception("Expected TryCopyTo to return true");
        if (span.ToString() != "a")
            throw new Exception($"Expected 'a' but got '{span.ToString()}'");
        return Task.CompletedTask;
    }

    [Test]
    public async Task StringContainsStringComparison() =>
        await Assert.That("value".Contains("E", StringComparison.OrdinalIgnoreCase)).IsTrue();

    [Test]
    public async Task StartsWith()
    {
        await Assert.That("value".StartsWith('v')).IsTrue();
        await Assert.That("v".StartsWith('v')).IsTrue();
        await Assert.That("".StartsWith('v')).IsFalse();
    }

    [Test]
    public async Task Split()
    {
        await Assert.That("a b".Split(' ', StringSplitOptions.RemoveEmptyEntries).SequenceEqual(new []{"a","b"})).IsTrue();
        await Assert.That("a b".Split(' ', 2, StringSplitOptions.RemoveEmptyEntries).SequenceEqual(new []{"a","b"})).IsTrue();

        await Assert.That("a b".Split(" ", StringSplitOptions.RemoveEmptyEntries).SequenceEqual(new []{"a","b"})).IsTrue();
        await Assert.That("a b".Split(" ", 2, StringSplitOptions.RemoveEmptyEntries).SequenceEqual(new []{"a","b"})).IsTrue();
    }

    [Test]
    public async Task ContainsChar() =>
        await Assert.That("value".Contains('v')).IsTrue();
}
