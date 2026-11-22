#if FeatureMemory

using System.Net;
using System.Text;

// Runs on every target, so net10.0 and later validate the BCL and everything below the polyfill.
// IPAddress.TryParse(string) is the oracle: it has been on every framework Polyfill supports and
// the BCL's own IsValid agrees with it on every input tried here.
public class IPAddressPolyfillTests
{
    static string[] corpus =
    [
        "",
        " ",
        "1.2.3.4",
        " 1.2.3.4",
        "1.2.3.4 ",
        "1.2.3",
        "1.2",
        "1",
        "0.0.0.0",
        "255.255.255.255",
        "256.1.1.1",
        "1.2.3.4.5",
        "01.02.03.04",
        "1.2.3.-4",
        "1..2.3",
        "1.2.3.",
        "::1",
        "::",
        "fe80::1",
        "fe80::1%eth0",
        "fe80::1%12",
        "2001:db8::ff00:42:8329",
        ":::",
        "1:2:3:4:5:6:7:8:9",
        "::ffff:1.2.3.4",
        "[::1]",
        "abc",
        "1.2.3.4:80",
        "999.999.999.999",
        "١.٢.٣.٤",
        "１.２.３.４"
    ];

    [Test]
    public async Task IsValid_MatchesTryParse()
    {
        foreach (var value in corpus)
        {
            var expected = IPAddress.TryParse(value, out _);
            await Assert.That(IsValid(value)).IsEqualTo(expected);
        }
    }

    [Test]
    public async Task IsValidUtf8_MatchesIsValid()
    {
        foreach (var value in corpus)
        {
            var expected = IPAddress.TryParse(value, out _);
            await Assert.That(IsValidUtf8(Encoding.UTF8.GetBytes(value))).IsEqualTo(expected);
        }
    }

    [Test]
    public async Task ParseUtf8_MatchesTryParse()
    {
        var failures = new List<string>();
        foreach (var value in corpus)
        {
            var expected = IPAddress.TryParse(value, out var address) ? address : null;
            var actual = ParseUtf8OrNull(Encoding.UTF8.GetBytes(value));
            if (!Equals(expected, actual))
            {
                failures.Add($"'{value}': expected {Describe(expected)}, got {Describe(actual)}");
            }
        }

        await Assert.That(failures).IsEmpty();
    }

    [Test]
    public async Task ParseUtf8_RejectsMalformedUtf8()
    {
        byte[][] malformed =
        [
            [0xFF],
            [0xC3],
            [0xC3, 0x28],
            [0x31, 0xFF, 0x2E],
            [0xED, 0xA0, 0x80],
            []
        ];

        var failures = new List<string>();
        foreach (var bytes in malformed)
        {
            var address = ParseUtf8OrNull(bytes);
            if (address is not null)
            {
                failures.Add($"{BitConverter.ToString(bytes)} parsed as {address}");
            }
        }

        await Assert.That(failures).IsEmpty();
    }

    [Test]
    public async Task IsValidUtf8_RejectsMalformedUtf8()
    {
        byte[][] malformed =
        [
            [0xFF],
            [0xC3],
            [0xC3, 0x28],
            [0x31, 0xFF, 0x2E],
            [0xED, 0xA0, 0x80]
        ];

        foreach (var bytes in malformed)
        {
            await Assert.That(IsValidUtf8(bytes)).IsFalse();
        }
    }

    [Test]
    public async Task EmptySpansAreNotValid()
    {
        await Assert.That(IsValid(string.Empty)).IsFalse();
        await Assert.That(IsValidUtf8([])).IsFalse();
    }

    // the spans stay inside these helpers, so nothing ref struct shaped lives across an await
    static bool IsValid(string value) =>
        IPAddress.IsValid(value.AsSpan());

    static bool IsValidUtf8(byte[] utf8) =>
        IPAddress.IsValidUtf8(utf8);

    // null when Parse throws FormatException; any other exception fails the test
    static IPAddress? ParseUtf8OrNull(byte[] utf8)
    {
        try
        {
            return IPAddress.Parse(utf8);
        }
        catch (FormatException)
        {
            return null;
        }
    }

    static string Describe(IPAddress? address) =>
        address?.ToString() ?? "FormatException";
}

#endif
