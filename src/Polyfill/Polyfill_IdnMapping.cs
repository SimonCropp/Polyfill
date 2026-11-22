#if FeatureMemory && !NET11_0_OR_GREATER

namespace Polyfills;

using System;
using System.Globalization;

static partial class Polyfill
{
    /// <summary>
    /// Encodes a Unicode domain name to its ASCII (Punycode) equivalent.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.globalization.idnmapping.trygetascii?view=net-11.0#system-globalization-idnmapping-trygetascii(system-readonlyspan((system-char))-system-span((system-char))-system-int32@)
    //Note: Copies the span to a string and calls GetAscii(string), so this allocates where the BCL works straight from the span.
    public static bool TryGetAscii(this IdnMapping target, ReadOnlySpan<char> unicode, Span<char> destination, out int charsWritten)
    {
        // the BCL rejects an empty or null terminated name before checking for overlap; GetAscii does the same
        if (unicode.Length > 0 &&
            unicode[unicode.Length - 1] != 0)
        {
            ThrowIfIdnOverlaps(unicode, destination);
        }

        return TryCopyIdnResult(target.GetAscii(unicode.ToString()), destination, out charsWritten);
    }

    /// <summary>
    /// Decodes one or more encoded domain name labels to a string of Unicode characters.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.globalization.idnmapping.trygetunicode?view=net-11.0#system-globalization-idnmapping-trygetunicode(system-readonlyspan((system-char))-system-span((system-char))-system-int32@)
    //Note: Copies the span to a string and calls GetUnicode(string), so this allocates where the BCL works straight from the span.
    public static bool TryGetUnicode(this IdnMapping target, ReadOnlySpan<char> ascii, Span<char> destination, out int charsWritten)
    {
        // the BCL rejects a null terminated name before checking for overlap; GetUnicode does the same
        if (ascii.Length == 0 ||
            ascii[ascii.Length - 1] != 0)
        {
            ThrowIfIdnOverlaps(ascii, destination);
        }

        return TryCopyIdnResult(target.GetUnicode(ascii.ToString()), destination, out charsWritten);
    }

    static void ThrowIfIdnOverlaps(ReadOnlySpan<char> source, Span<char> destination)
    {
        if (source.Overlaps(destination))
        {
            throw new ArgumentException("This operation is invalid on overlapping buffers.");
        }
    }

    static bool TryCopyIdnResult(string result, Span<char> destination, out int charsWritten)
    {
        if (result.AsSpan().TryCopyTo(destination))
        {
            charsWritten = result.Length;
            return true;
        }

        charsWritten = 0;
        return false;
    }
}

#endif
