using System.Net;
using System.Text;

// Runs on every target: net11.0 validates the expectations against the BCL, netcoreapp3.0 to net10.0 have
// the BCL string and char parsers and the polyfilled UTF-8 and TryFormat members, and everything below has
// all of it polyfilled.
public class IPEndPointPolyfillTests
{
    // Address is null when the input is invalid. A record rather than a tuple, since NoRefsTests has no
    // System.ValueTuple on net462.
    record ParseCase(string Input, string? Address, int Port);

    static ParseCase[] corpus =
    [
        new("127.0.0.1", "127.0.0.1", 0),
        new("127.0.0.1:0", "127.0.0.1", 0),
        new("127.0.0.1:8080", "127.0.0.1", 8080),
        new("1.2.3.4:65535", "1.2.3.4", 65535),
        new("1.2.3.4:080", "1.2.3.4", 80),
        new("::1", "::1", 0),
        new("[::1]", "::1", 0),
        new("[::1]:443", "::1", 443),
        // without brackets the last group is part of the IPv6 address, not a port
        new("::1:80", "::1:80", 0),
        new("2001:db8::1", "2001:db8::1", 0),
        new("[2001:db8::1]:8080", "2001:db8::1", 8080),
        new("[::ffff:1.2.3.4]:80", "::ffff:1.2.3.4", 80),
        new("", null, 0),
        new("abc", null, 0),
        new("abc:80", null, 0),
        new(":80", null, 0),
        new("1.2.3.4:", null, 0),
        new("1.2.3.4:65536", null, 0),
        new("1.2.3.4:4294967296", null, 0),
        new("1.2.3.4:-1", null, 0),
        new("1.2.3.4:+1", null, 0),
        new("1.2.3.4: 1", null, 0),
        new("1.2.3.4:1 ", null, 0),
        new("1.2.3.4:0x10", null, 0),
        new("1.2.3.4:80:80", null, 0),
        new("[::1]:", null, 0),
        new("[::1]:abc", null, 0),
        new("[::1]:65536", null, 0)
    ];

    [Test]
    public async Task TryParse_String()
    {
        var failures = new List<string>();
        foreach (var (input, address, port) in corpus)
        {
            var success = IPEndPoint.TryParse(input, out var endpoint);
            Check(failures, "TryParse(string)", input, address, port, success, endpoint);
        }

        await Assert.That(failures).IsEmpty();
    }

    [Test]
    public async Task Parse_String()
    {
        var failures = new List<string>();
        foreach (var (input, address, port) in corpus)
        {
            var endpoint = ParseOrNull(() => IPEndPoint.Parse(input));
            Check(failures, "Parse(string)", input, address, port, endpoint is not null, endpoint);
        }

        await Assert.That(failures).IsEmpty();
    }

    [Test]
    public async Task Parse_NullString_Throws() =>
        await Assert.That(() => IPEndPoint.Parse((string) null!)).Throws<ArgumentNullException>();

    [Test]
    public async Task TryParse_NullString_ReturnsFalse()
    {
        var success = IPEndPoint.TryParse((string) null!, out var endpoint);
        await Assert.That(success).IsFalse();
        await Assert.That(endpoint).IsNull();
    }

#if FeatureMemory

    [Test]
    public async Task TryParse_CharSpan()
    {
        var failures = new List<string>();
        foreach (var (input, address, port) in corpus)
        {
            var success = IPEndPoint.TryParse(input.AsSpan(), out var endpoint);
            Check(failures, "TryParse(ReadOnlySpan<char>)", input, address, port, success, endpoint);
        }

        await Assert.That(failures).IsEmpty();
    }

    [Test]
    public async Task Parse_CharSpan()
    {
        var failures = new List<string>();
        foreach (var (input, address, port) in corpus)
        {
            var endpoint = ParseOrNull(() => IPEndPoint.Parse(input.AsSpan()));
            Check(failures, "Parse(ReadOnlySpan<char>)", input, address, port, endpoint is not null, endpoint);
        }

        await Assert.That(failures).IsEmpty();
    }

    [Test]
    public async Task TryParse_Utf8()
    {
        var failures = new List<string>();
        foreach (var (input, address, port) in corpus)
        {
            var success = IPEndPoint.TryParse(Encoding.UTF8.GetBytes(input).AsSpan(), out var endpoint);
            Check(failures, "TryParse(ReadOnlySpan<byte>)", input, address, port, success, endpoint);
        }

        await Assert.That(failures).IsEmpty();
    }

    [Test]
    public async Task Parse_Utf8()
    {
        var failures = new List<string>();
        foreach (var (input, address, port) in corpus)
        {
            var endpoint = ParseOrNull(() => IPEndPoint.Parse(Encoding.UTF8.GetBytes(input).AsSpan()));
            Check(failures, "Parse(ReadOnlySpan<byte>)", input, address, port, endpoint is not null, endpoint);
        }

        await Assert.That(failures).IsEmpty();
    }

    [Test]
    public async Task TryParse_Utf8_RejectsMalformedUtf8()
    {
        byte[][] malformed =
        [
            [0xFF],
            [0x31, 0x2E, 0x32, 0x2E, 0x33, 0x2E, 0x34, 0x3A, 0xFF],
            [0x31, 0x2E, 0x32, 0x2E, 0x33, 0x2E, 0x34, 0x3A, 0x38, 0xC3],
            [0xED, 0xA0, 0x80, 0x3A, 0x38, 0x30]
        ];

        var failures = new List<string>();
        foreach (var bytes in malformed)
        {
            if (IPEndPoint.TryParse(bytes.AsSpan(), out var endpoint))
            {
                failures.Add($"{BitConverter.ToString(bytes)} parsed as {endpoint}");
            }
        }

        await Assert.That(failures).IsEmpty();
    }

    static IPEndPoint[] formatCases =
    [
        new(IPAddress.Loopback, 8080),
        new(IPAddress.Any, 0),
        new(IPAddress.Broadcast, 65535),
        new(IPAddress.IPv6Loopback, 443),
        new(IPAddress.IPv6Any, 0),
        new(IPAddress.Parse("2001:db8::ff00:42:8329"), 65535),
        new(IPAddress.Parse("::ffff:1.2.3.4"), 80),
        new(new IPAddress(IPAddress.Parse("fe80::1").GetAddressBytes(), 12), 80),
        new(IPAddress.Parse("ffff:ffff:ffff:ffff:ffff:ffff:ffff:ffff"), 65535)
    ];

    // IPEndPoint.ToString() is the oracle: it exists on every target, and the BCL's TryFormat writes the same text.
    [Test]
    public async Task TryFormat_Chars_MatchesToString()
    {
        var failures = new List<string>();
        foreach (var endpoint in formatCases)
        {
            var expected = endpoint.ToString();
            for (var length = 0; length <= expected.Length + 2; length++)
            {
                var buffer = new char[length];
                var success = endpoint.TryFormat(buffer.AsSpan(), out var written);
                var text = new string(buffer, 0, Math.Min(written, length));
                CheckFormat(failures, "chars", expected, expected.Length, length, success, written, text);
            }
        }

        await Assert.That(failures).IsEmpty();
    }

    [Test]
    public async Task TryFormat_Utf8_MatchesToString()
    {
        var failures = new List<string>();
        foreach (var endpoint in formatCases)
        {
            var expected = endpoint.ToString();
            var expectedLength = Encoding.UTF8.GetByteCount(expected);
            for (var length = 0; length <= expectedLength + 2; length++)
            {
                var buffer = new byte[length];
                var success = endpoint.TryFormat(buffer.AsSpan(), out var written);
                var text = Encoding.UTF8.GetString(buffer, 0, Math.Min(written, length));
                CheckFormat(failures, "utf8", expected, expectedLength, length, success, written, text);
            }
        }

        await Assert.That(failures).IsEmpty();
    }

    [Test]
    public async Task TryFormat_IgnoresToStringOverride()
    {
        var endpoint = new CustomToStringEndPoint(IPAddress.Loopback, 80);
        var chars = new char[64];
        var charSuccess = endpoint.TryFormat(chars.AsSpan(), out var charsWritten);
        var bytes = new byte[64];
        var byteSuccess = endpoint.TryFormat(bytes.AsSpan(), out var bytesWritten);

        await Assert.That(charSuccess).IsTrue();
        await Assert.That(new string(chars, 0, charsWritten)).IsEqualTo("127.0.0.1:80");
        await Assert.That(byteSuccess).IsTrue();
        await Assert.That(Encoding.UTF8.GetString(bytes, 0, bytesWritten)).IsEqualTo("127.0.0.1:80");
    }

    class CustomToStringEndPoint(IPAddress address, int port) :
        IPEndPoint(address, port)
    {
        public override string ToString() => "custom";
    }

    static void CheckFormat(List<string> failures, string kind, string expected, int expectedLength, int length, bool success, int written, string text)
    {
        if (length < expectedLength)
        {
            if (success || written != 0)
            {
                failures.Add($"{kind} {expected} into {length}: expected false/0, got {success}/{written}");
            }

            return;
        }

        if (!success || written != expectedLength || text != expected)
        {
            failures.Add($"{kind} {expected} into {length}: got {success}/{written}/'{text}'");
        }
    }

#endif

    // null when Parse throws FormatException; any other exception fails the test
    static IPEndPoint? ParseOrNull(Func<IPEndPoint> parse)
    {
        try
        {
            return parse();
        }
        catch (FormatException)
        {
            return null;
        }
    }

    static void Check(List<string> failures, string method, string input, string? address, int port, bool success, IPEndPoint? endpoint)
    {
        if (address is null)
        {
            if (success || endpoint is not null)
            {
                failures.Add($"{method} '{input}': expected failure, got {endpoint}");
            }

            return;
        }

        if (!success || endpoint is null)
        {
            failures.Add($"{method} '{input}': expected {address} port {port}, got failure");
            return;
        }

        var expectedAddress = IPAddress.Parse(address);
        if (!endpoint.Address.Equals(expectedAddress) || endpoint.Port != port)
        {
            failures.Add($"{method} '{input}': expected {expectedAddress} port {port}, got {endpoint.Address} port {endpoint.Port}");
        }

        if (endpoint.AddressFamily != expectedAddress.AddressFamily)
        {
            failures.Add($"{method} '{input}': expected {expectedAddress.AddressFamily}, got {endpoint.AddressFamily}");
        }
    }
}
