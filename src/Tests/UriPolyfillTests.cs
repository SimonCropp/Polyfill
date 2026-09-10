#if FeatureMemory

// The span overloads must agree with the string overloads on the framework they run on.
// Uri.EscapeDataString itself escapes a different set of characters on .NET Framework than
// on .NET, so comparing the two overloads is the invariant that holds everywhere, rather
// than any fixed expected output.
public class UriPolyfillTests
{
    static string[] samples =
    [
        "",
        "abc",
        "a b",
        "a/b?c=d&e=f",
        "%",
        "%%",
        "%4",
        "%41",
        "%zz",
        "%25",
        "-._~",
        "!*'()",
        "+",
        "é",
        "你好",
        "%E4%BD%A0%E5%A5%BD",
        "abc%20def"
    ];

    [Test]
    public async Task EscapeDataString_MatchesStringOverload()
    {
        foreach (var sample in samples)
        {
            var fromSpan = Capture(() => Uri.EscapeDataString(sample.AsSpan()));
            var fromString = Capture(() => Uri.EscapeDataString(sample));
            await Assert.That(fromSpan).IsEqualTo(fromString);
        }
    }

    [Test]
    public async Task UnescapeDataString_MatchesStringOverload()
    {
        foreach (var sample in samples)
        {
            var fromSpan = Capture(() => Uri.UnescapeDataString(sample.AsSpan()));
            var fromString = Capture(() => Uri.UnescapeDataString(sample));
            await Assert.That(fromSpan).IsEqualTo(fromString);
        }
    }

    [Test]
    public async Task EscapeDataString_KnownValues()
    {
        await Assert.That(Uri.EscapeDataString("a b".AsSpan())).IsEqualTo("a%20b");
        await Assert.That(Uri.EscapeDataString("abc".AsSpan())).IsEqualTo("abc");
        await Assert.That(Uri.EscapeDataString(default(ReadOnlySpan<char>))).IsEqualTo(string.Empty);
        await Assert.That(Uri.UnescapeDataString("a%20b".AsSpan())).IsEqualTo("a b");
        await Assert.That(Uri.UnescapeDataString(default(ReadOnlySpan<char>))).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task TryEscapeDataString_Sizing()
    {
        foreach (var sample in samples)
        {
            var expected = Uri.EscapeDataString(sample);
            for (var size = 0; size <= expected.Length + 1; size++)
            {
                var result = TryEscape(sample, size);

                await Assert.That(result.Succeeded).IsEqualTo(size >= expected.Length);
                if (result.Succeeded)
                {
                    await Assert.That(result.Written).IsEqualTo(expected.Length);
                    await Assert.That(result.Content).IsEqualTo(expected);
                }
                else
                {
                    // a failed attempt reports nothing written
                    await Assert.That(result.Written).IsEqualTo(0);
                }
            }
        }
    }

    [Test]
    public async Task TryUnescapeDataString_Sizing()
    {
        foreach (var sample in samples)
        {
            var expected = Uri.UnescapeDataString(sample);
            for (var size = SmallestTestableSize(sample); size <= expected.Length + 1; size++)
            {
                var result = TryUnescape(sample, size);

                await Assert.That(result.Succeeded).IsEqualTo(size >= expected.Length);
                if (result.Succeeded)
                {
                    await Assert.That(result.Written).IsEqualTo(expected.Length);
                    await Assert.That(result.Content).IsEqualTo(expected);
                }
                else
                {
                    await Assert.That(result.Written).IsEqualTo(0);
                }
            }
        }
    }

    [Test]
    public async Task TryUnescapeDataString_DestinationSmallerThanLiteralPrefix()
    {
#if !NET9_0_OR_GREATER || NET11_0_OR_GREATER
        // only net9.0 and net10.0 are allowed to throw here
        await Assert.That(throwsOnShortDestination).IsFalse();
#endif

        if (throwsOnShortDestination)
        {
            await Assert.That(() => TryUnescape("abc%20def", 2)).Throws<ArgumentOutOfRangeException>();
            return;
        }

        var result = TryUnescape("abc%20def", 2);
        await Assert.That(result.Succeeded).IsFalse();
        await Assert.That(result.Written).IsEqualTo(0);
    }

    // unescaping only ever shrinks, so a destination that overlaps the source is safe
    [Test]
    public async Task TryUnescapeDataString_InPlace()
    {
        var buffer = "a%20b".ToCharArray();
        var succeeded = Uri.TryUnescapeDataString(buffer, buffer, out var written);
        var writtenCount = written;
        var content = new string(buffer, 0, written);

        await Assert.That(succeeded).IsTrue();
        await Assert.That(writtenCount).IsEqualTo(3);
        await Assert.That(content).IsEqualTo("a b");
    }

    static int SmallestTestableSize(string sample)
    {
        if (!throwsOnShortDestination)
        {
            return 0;
        }

        // skip the sizes covered by TryUnescapeDataString_DestinationSmallerThanLiteralPrefix
        var index = sample.IndexOf('%');
        return index < 0 ? 0 : index;
    }

    // net9.0 and net10.0 shipped Uri.TryUnescapeDataString throwing ArgumentOutOfRangeException
    // instead of returning false when the destination cannot hold the literal text preceding the
    // first escape sequence (dotnet/runtime#124654, fixed for net11 by dotnet/runtime#124655).
    // The backport to release/10.0, dotnet/runtime#128610, was written and then deferred, so the
    // behaviour is probed rather than inferred from the target framework: a serviced net9.0 or
    // net10.0 runtime would otherwise turn these tests red. The polyfill never throws.
    static bool throwsOnShortDestination = ProbeThrowsOnShortDestination();

    static bool ProbeThrowsOnShortDestination()
    {
        try
        {
            Uri.TryUnescapeDataString("abc%20def".AsSpan(), new char[2], out _);
            return false;
        }
        catch (ArgumentOutOfRangeException)
        {
            return true;
        }
    }

    // the spans stay inside these helpers, so nothing ref struct shaped has to live across an await
    static (bool Succeeded, int Written, string? Content) TryEscape(string value, int destinationSize)
    {
        var buffer = new char[destinationSize];
        var succeeded = Uri.TryEscapeDataString(value.AsSpan(), buffer, out var written);
        return (succeeded, written, succeeded ? new string(buffer, 0, written) : null);
    }

    static (bool Succeeded, int Written, string? Content) TryUnescape(string value, int destinationSize)
    {
        var buffer = new char[destinationSize];
        var succeeded = Uri.TryUnescapeDataString(value.AsSpan(), buffer, out var written);
        return (succeeded, written, succeeded ? new string(buffer, 0, written) : null);
    }

    static string Capture(Func<string> func)
    {
        try
        {
            return func();
        }
        catch (Exception exception)
        {
            return exception.GetType().Name;
        }
    }
}

#endif
