#if FeatureMemory

// net11.0 runs the BCL, everything below the polyfill. GetAscii(string) and GetUnicode(string) are the oracle:
// they exist on every target, and the BCL's span overloads produce the same text.
public class IdnMappingPolyfillTests
{
    static string[] unicodeNames =
    [
        "example.com",
        "bücher.example",
        "münchen.de",
        "例え.テスト",
        "xn--bcher-kva.example"
    ];

    static string[] asciiNames =
    [
        "example.com",
        "xn--bcher-kva.example",
        "xn--mnchen-3ya.de",
        "xn--r8jz45g.xn--zckzah"
    ];

    [Test]
    public async Task TryGetAscii_MatchesGetAscii()
    {
        var mapping = new IdnMapping();
        var failures = new List<string>();
        foreach (var name in unicodeNames)
        {
            var expected = mapping.GetAscii(name);
            for (var length = 0; length <= expected.Length + 2; length++)
            {
                var buffer = new char[length];
                var success = mapping.TryGetAscii(name.AsSpan(), buffer, out var written);
                Check(failures, name, expected, length, success, written, buffer);
            }
        }

        await Assert.That(failures).IsEmpty();
    }

    [Test]
    public async Task TryGetUnicode_MatchesGetUnicode()
    {
        var mapping = new IdnMapping();
        var failures = new List<string>();
        foreach (var name in asciiNames)
        {
            var expected = mapping.GetUnicode(name);
            for (var length = 0; length <= expected.Length + 2; length++)
            {
                var buffer = new char[length];
                var success = mapping.TryGetUnicode(name.AsSpan(), buffer, out var written);
                Check(failures, name, expected, length, success, written, buffer);
            }
        }

        await Assert.That(failures).IsEmpty();
    }

    [Test]
    public async Task TryGetAscii_InvalidInput_Throws()
    {
        var mapping = new IdnMapping();
        await Assert.That(() => mapping.TryGetAscii(ReadOnlySpan<char>.Empty, new char[10], out _)).Throws<ArgumentException>();
        await Assert.That(() => mapping.TryGetAscii("abc\0".AsSpan(), new char[10], out _)).Throws<ArgumentException>();
        await Assert.That(() => mapping.TryGetAscii("a..b".AsSpan(), new char[10], out _)).Throws<ArgumentException>();
    }

    [Test]
    public async Task TryGetUnicode_InvalidInput_Throws()
    {
        var mapping = new IdnMapping();
        await Assert.That(() => mapping.TryGetUnicode("abc\0".AsSpan(), new char[10], out _)).Throws<ArgumentException>();
        await Assert.That(() => mapping.TryGetUnicode("xn--".AsSpan(), new char[10], out _)).Throws<ArgumentException>();
    }

    [Test]
    public async Task OverlappingBuffers_Throw()
    {
        var mapping = new IdnMapping();
        await Assert.That(() =>
        {
            var buffer = "example.com".PadRight(40).ToCharArray();
            mapping.TryGetAscii(buffer.AsSpan(0, 11), buffer.AsSpan(5), out _);
        }).Throws<ArgumentException>();
        await Assert.That(() =>
        {
            var buffer = "example.com".PadRight(40).ToCharArray();
            mapping.TryGetUnicode(buffer.AsSpan(0, 11), buffer.AsSpan(5), out _);
        }).Throws<ArgumentException>();
    }

    static void Check(List<string> failures, string name, string expected, int length, bool success, int written, char[] buffer)
    {
        if (length < expected.Length)
        {
            if (success || written != 0)
            {
                failures.Add($"'{name}' into {length}: expected false/0, got {success}/{written}");
            }

            return;
        }

        var text = new string(buffer, 0, Math.Min(written, length));
        if (!success || written != expected.Length || text != expected)
        {
            failures.Add($"'{name}' into {length}: expected '{expected}', got {success}/{written}/'{text}'");
        }
    }
}

#endif
