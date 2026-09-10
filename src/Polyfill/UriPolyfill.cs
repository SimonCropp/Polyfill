#if !NET11_0_OR_GREATER

namespace Polyfills;

using System;

static partial class Polyfill
{
    extension(Uri)
    {
        /// <summary>
        /// Provides the scheme name for the <c>data</c> URI scheme (RFC 2397). This field is read-only.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.uri.urischemedata?view=net-11.0
        public static string UriSchemeData => "data";

#if FeatureMemory && !NET9_0_OR_GREATER

        /// <summary>
        /// Converts a span of characters to its escaped representation.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.uri.escapedatastring?view=net-11.0#system-uri-escapedatastring(system-readonlyspan((system-char)))
        //Note: Copies the span to a string first, so this allocates where the BCL escapes straight from the span.
        public static string EscapeDataString(ReadOnlySpan<char> charsToEscape) =>
            Uri.EscapeDataString(charsToEscape.ToString());

        /// <summary>
        /// Converts a span of characters to its unescaped representation.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.uri.unescapedatastring?view=net-11.0#system-uri-unescapedatastring(system-readonlyspan((system-char)))
        //Note: Copies the span to a string first, so this allocates where the BCL unescapes straight from the span.
        public static string UnescapeDataString(ReadOnlySpan<char> charsToUnescape) =>
            Uri.UnescapeDataString(charsToUnescape.ToString());

        /// <summary>
        /// Attempts to convert a span of characters to its escaped representation.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.uri.tryescapedatastring?view=net-11.0
        //Note: Escapes through an intermediate string, so this allocates where the BCL writes straight to the destination.
        public static bool TryEscapeDataString(ReadOnlySpan<char> charsToEscape, Span<char> destination, out int charsWritten) =>
            TryWriteTo(Uri.EscapeDataString(charsToEscape.ToString()), destination, out charsWritten);

        /// <summary>
        /// Attempts to convert a span of characters to its unescaped representation.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.uri.tryunescapedatastring?view=net-11.0
        //Note: Unescapes through an intermediate string, so this allocates where the BCL writes straight to the destination.
        public static bool TryUnescapeDataString(ReadOnlySpan<char> charsToUnescape, Span<char> destination, out int charsWritten) =>
            TryWriteTo(Uri.UnescapeDataString(charsToUnescape.ToString()), destination, out charsWritten);

#endif
    }

#if FeatureMemory && !NET9_0_OR_GREATER

    static bool TryWriteTo(string value, Span<char> destination, out int charsWritten)
    {
        if (value.Length > destination.Length)
        {
            charsWritten = 0;
            return false;
        }

        value.AsSpan().CopyTo(destination);
        charsWritten = value.Length;
        return true;
    }

#endif
}

#endif
