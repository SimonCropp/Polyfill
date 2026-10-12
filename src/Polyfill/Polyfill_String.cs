namespace Polyfills;

using System;
using System.Text;

static partial class Polyfill
{
#if FeatureMemory && !NET6_0_OR_GREATER

    /// <summary>
    /// Copies the contents of this string into the destination span.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.copyto?view=net-11.0
    public static void CopyTo(this string target, Span<char> destination) =>
        target.AsSpan().CopyTo(destination);

    /// <summary>
    /// Copies the contents of this string into the destination span.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.trycopyto?view=net-11.0
    public static bool TryCopyTo(this string target, Span<char> destination) =>
        target.AsSpan().TryCopyTo(destination);
#endif

#if NETFRAMEWORK || NETSTANDARD2_0

    /// <summary>
    /// Returns the hash code for this string using the specified rules.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.gethashcode?view=net-11.0#system-string-gethashcode(system-stringcomparison)
    public static int GetHashCode(this string target, StringComparison comparisonType) =>
        FromComparison(comparisonType).GetHashCode(target);

    static StringComparer FromComparison(StringComparison comparison) =>
        comparison switch
        {
            StringComparison.CurrentCulture => StringComparer.CurrentCulture,
            StringComparison.CurrentCultureIgnoreCase => StringComparer.CurrentCultureIgnoreCase,
            StringComparison.InvariantCulture => StringComparer.InvariantCulture,
            StringComparison.InvariantCultureIgnoreCase => StringComparer.InvariantCultureIgnoreCase,
            StringComparison.Ordinal => StringComparer.Ordinal,
            StringComparison.OrdinalIgnoreCase => StringComparer.OrdinalIgnoreCase,
            _ => throw new ArgumentOutOfRangeException(nameof(comparison), comparison, null)
        };

    /// <summary>
    /// Determines whether this string instance starts with the specified character.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.startswith?view=net-11.0#system-string-startswith(system-char)
    public static bool StartsWith(this string target, char value)
    {
        if (target.Length == 0)
        {
            return false;
        }

        return target[0] == value;
    }

    /// <summary>
    /// Returns a value indicating whether a specified character occurs within this string.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.endswith?view=net-11.0#system-string-endswith(system-char)
    public static bool EndsWith(this string target, char value)
    {
        if (target.Length == 0)
        {
            return false;
        }

        var lastPos = target.Length - 1;
        return target[lastPos] == value;
    }

    /// <summary>
    /// Splits a string into substrings based on a specified delimiting character and, optionally, options.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.split?view=net-11.0#system-string-split(system-char-system-stringsplitoptions)
    public static string[] Split(this string target, char separator, StringSplitOptions options = StringSplitOptions.None) =>
        target.Split([separator], options);

    /// <summary>
    /// Splits a string into a maximum number of substrings based on a specified delimiting character and, optionally,
    /// options. Splits a string into a maximum number of substrings based on the provided character separator,
    /// optionally omitting empty substrings from the result.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.split?view=net-11.0#system-string-split(system-char-system-int32-system-stringsplitoptions)
    public static string[] Split(this string target, char separator, int count, StringSplitOptions options = StringSplitOptions.None) =>
        target.Split([separator], count, options);

    /// <summary>
    /// Splits a string into substrings that are based on the provided string separator.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.split?view=net-11.0#system-string-split(system-string-system-stringsplitoptions)
    public static string[] Split(this string target, string separator, StringSplitOptions options = StringSplitOptions.None) =>
        target.Split([separator], options);

    /// <summary>
    /// Splits a string into a maximum number of substrings based on a specified delimiting string and, optionally, options.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.split?view=net-11.0#system-string-split(system-string-system-int32-system-stringsplitoptions)
    public static string[] Split(this string target, string separator, int count, StringSplitOptions options = StringSplitOptions.None) =>
        target.Split([separator], count, options);
#endif

#if !NETCOREAPP2_1_OR_GREATER && !NETSTANDARD2_1_OR_GREATER
    /// <summary>
    /// Returns a value indicating whether a specified character occurs within this string.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.contains?view=net-11.0#system-string-contains(system-char)
    public static bool Contains(this string target, char value) =>
        target.IndexOf(value) >= 0;

    /// <summary>
    /// Returns a value indicating whether a specified character occurs within this string, using the specified comparison rules.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.contains?view=net-11.0#system-string-contains(system-char-system-stringcomparison)
    public static bool Contains(this string target, char value, StringComparison comparisonType) =>
        target.IndexOf(value, comparisonType) >= 0;

    /// <summary>
    /// Reports the zero-based index of the first occurrence of the specified Unicode character in this string. A parameter specifies the type of search to use for the specified character.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.indexof?view=net-11.0#system-string-indexof(system-char-system-stringcomparison)
    public static int IndexOf(this string target, char value, StringComparison comparisonType) =>
        target.IndexOf(value.ToString(), comparisonType);

    /// <summary>
    /// Returns a value indicating whether a specified string occurs within this string, using the specified comparison rules.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.contains?view=net-11.0#system-string-contains(system-string-system-stringcomparison)
    public static bool Contains(this string target, string value, StringComparison comparisonType) =>
        target.IndexOf(value, comparisonType) >= 0;
#endif

#if !NET11_0_OR_GREATER

    /// <summary>
    /// Determines whether this string instance starts with the specified character when compared using the specified comparison option.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.startswith?view=net-11.0#system-string-startswith(system-char-system-stringcomparison)
    public static bool StartsWith(this string target, char value, StringComparison comparisonType) =>
        target.Length != 0 && target.StartsWith(value.ToString(), comparisonType);

    /// <summary>
    /// Determines whether the end of this string instance matches the specified character when compared using the specified comparison option.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.endswith?view=net-11.0#system-string-endswith(system-char-system-stringcomparison)
    public static bool EndsWith(this string target, char value, StringComparison comparisonType) =>
        target.Length != 0 && target.EndsWith(value.ToString(), comparisonType);

    /// <summary>
    /// Reports the zero-based index of the first occurrence of the specified character in this string.
    /// The search starts at a specified character position.
    /// A parameter specifies the type of search to use.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.indexof?view=net-11.0#system-string-indexof(system-char-system-int32-system-stringcomparison)
    public static int IndexOf(this string target, char value, int startIndex, StringComparison comparisonType) =>
        target.IndexOf(value.ToString(), startIndex, comparisonType);

    /// <summary>
    /// Reports the zero-based index of the first occurrence of the specified character in this string.
    /// The search starts at a specified character position and examines a specified number of character positions.
    /// A parameter specifies the type of search to use.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.indexof?view=net-11.0#system-string-indexof(system-char-system-int32-system-int32-system-stringcomparison)
    public static int IndexOf(this string target, char value, int startIndex, int count, StringComparison comparisonType) =>
        target.IndexOf(value.ToString(), startIndex, count, comparisonType);

    /// <summary>
    /// Reports the zero-based index of the last occurrence of a specified character within the current string.
    /// A parameter specifies the type of search to use.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.lastindexof?view=net-11.0#system-string-lastindexof(system-char-system-stringcomparison)
    public static int LastIndexOf(this string target, char value, StringComparison comparisonType) =>
        target.LastIndexOf(value.ToString(), comparisonType);

    /// <summary>
    /// Reports the zero-based index of the last occurrence of a specified character within the current string.
    /// The search starts at a specified character position and proceeds backward toward the beginning of the string.
    /// A parameter specifies the type of search to use.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.lastindexof?view=net-11.0#system-string-lastindexof(system-char-system-int32-system-stringcomparison)
    public static int LastIndexOf(this string target, char value, int startIndex, StringComparison comparisonType) =>
        target.LastIndexOf(value.ToString(), startIndex, comparisonType);

    /// <summary>
    /// Reports the zero-based index of the last occurrence of a specified character within the current string.
    /// The search starts at a specified character position and proceeds backward toward the beginning of the string for a specified number of character positions.
    /// A parameter specifies the type of search to use.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.lastindexof?view=net-11.0#system-string-lastindexof(system-char-system-int32-system-int32-system-stringcomparison)
    public static int LastIndexOf(this string target, char value, int startIndex, int count, StringComparison comparisonType) =>
        target.LastIndexOf(value.ToString(), startIndex, count, comparisonType);

#endif

#if !NET6_0_OR_GREATER
    /// <summary>
    /// Replaces all newline sequences in the current string with <paramref name="replacementText"/>.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.replacelineendings?view=net-11.0#system-string-replacelineendings(system-string)
    public static string ReplaceLineEndings(this string target, string replacementText)
    {
        var index = IndexOfNewlineChar(target, 0, out var stride);
        if (index < 0)
        {
            // No line endings to replace; matches the BCL fast path.
            return target;
        }

        var builder = new StringBuilder(target.Length);
        var position = 0;
        while (true)
        {
            builder.Append(target, position, index - position);
            builder.Append(replacementText);
            position = index + stride;
            index = IndexOfNewlineChar(target, position, out stride);
            if (index < 0)
            {
                break;
            }
        }

        builder.Append(target, position, target.Length - position);
        return builder.ToString();
    }

    // Mirrors the line-ending set recognised by string.ReplaceLineEndings:
    // CR, LF, CRLF, FF (U+000C), NEL (U+0085), LS (U+2028) and PS (U+2029).
    static int IndexOfNewlineChar(string text, int startIndex, out int stride)
    {
        stride = 0;
        for (var index = startIndex; index < text.Length; index++)
        {
            switch (text[index])
            {
                case '\r':
                    stride = index + 1 < text.Length && text[index + 1] == '\n' ? 2 : 1;
                    return index;
                case '\n':
                case '\f':
                case '\u0085':
                case '\u2028':
                case '\u2029':
                    stride = 1;
                    return index;
            }
        }

        return -1;
    }

    /// <summary>
    /// Replaces all newline sequences in the current string with <see cref="Environment.NewLine"/>.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.replacelineendings?view=net-11.0#system-string-replacelineendings
    public static string ReplaceLineEndings(this string target) =>
        ReplaceLineEndings(target, Environment.NewLine);
#endif

#if !NET11_0_OR_GREATER

    /// <summary>
    /// Returns a copy of this string converted to lowercase using ordinal casing rules.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.tolowerordinal?view=net-11.0
    //Note: Derived from invariant casing, so the mapping follows the Unicode version of the running framework rather than the one net11 is built against.
    public static string ToLowerOrdinal(this string target)
    {
#if !NETFRAMEWORK
        // ASCII has no chars that ordinal casing treats differently, and invariant casing of an ASCII string is
        // vectorized. Any other string is cased a char at a time: off .NET Framework that is the quicker of the
        // two for text that is mostly ASCII and Latin-1.
        if (IsAsciiString(target))
        {
            return target.ToLowerInvariant();
        }

        return ToOrdinalCase(target, toUpper: false);
#else
        // Invariant casing does the bulk of the work in one call. It maps char for char, so only the chars it
        // changed need the ordinal check, and only the ones failing it are put back.
        var lower = target.ToLowerInvariant();
        if (lower.Length != target.Length)
        {
            return ToOrdinalCase(target, toUpper: false);
        }

        char[]? chars = null;
        for (var index = 0; index < target.Length; index++)
        {
            var current = target[index];
            var lowered = lower[index];

            // An unchanged char needs no check, and nor does an ASCII one, which always stays in its class.
            if (lowered == current ||
                current < '\u0080')
            {
                continue;
            }

            if (char.IsSurrogate(current))
            {
                // One half of a surrogate pair changed, so check the pair as the scalar it encodes. A low
                // surrogate that follows a high surrogate is always the second half of a pair.
                var start = -1;
                if (char.IsLowSurrogate(current))
                {
                    if (index > 0 &&
                        char.IsHighSurrogate(target[index - 1]))
                    {
                        start = index - 1;
                    }
                }
                else if (index + 1 < target.Length &&
                         char.IsLowSurrogate(target[index + 1]))
                {
                    start = index;
                }

                if (start < 0)
                {
                    // a surrogate on its own is not a scalar, and is left alone
                    chars ??= lower.ToCharArray();
                    chars[index] = current;
                    continue;
                }

                var cased = ToOrdinalCaseSurrogatePair(target.Substring(start, 2), toUpper: false);
                if (cased[0] != lower[start] ||
                    cased[1] != lower[start + 1])
                {
                    chars ??= lower.ToCharArray();
                    chars[start] = cased[0];
                    chars[start + 1] = cased[1];
                }

                continue;
            }

            // Ordinal lower casing must never move a character out of its ordinal upper-casing class.
            // See ToLowerOrdinalChar.
            if (ToUpperOrdinalChar(lowered) != ToUpperOrdinalChar(current))
            {
                chars ??= lower.ToCharArray();
                chars[index] = current;
            }
        }

        if (chars == null)
        {
            return lower;
        }

        return new(chars);
#endif
    }

    /// <summary>
    /// Returns a copy of this string converted to uppercase using ordinal casing rules.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.toupperordinal?view=net-11.0
    //Note: Derived from invariant casing, so the mapping follows the Unicode version of the running framework rather than the one net11 is built against.
    public static string ToUpperOrdinal(this string target)
    {
#if !NETFRAMEWORK
        // See ToLowerOrdinal
        if (IsAsciiString(target))
        {
            return target.ToUpperInvariant();
        }

        return ToOrdinalCase(target, toUpper: true);
#else
        // Invariant casing does the bulk of the work in one call, and maps char for char.
        var upper = target.ToUpperInvariant();
        if (upper.Length != target.Length)
        {
            return ToOrdinalCase(target, toUpper: true);
        }

        // ordinal casing leaves the long s uncased, where invariant casing maps it to S
        const char longS = '\u017F';
        var index = target.IndexOf(longS);
        if (index < 0)
        {
            return upper;
        }

        var chars = upper.ToCharArray();
        for (; index < chars.Length; index++)
        {
            if (target[index] == longS)
            {
                chars[index] = longS;
            }
        }

        return new(chars);
#endif
    }

#if !NETFRAMEWORK
    static bool IsAsciiString(string value)
    {
#if NET8_0_OR_GREATER
        return Ascii.IsValid(value);
#else
        foreach (var current in value)
        {
            if (current > '\u007F')
            {
                return false;
            }
        }

        return true;
#endif
    }
#endif

    static string ToOrdinalCase(string target, bool toUpper)
    {
        if (target.Length == 0)
        {
            return target;
        }

        var chars = target.ToCharArray();
        ToOrdinalCase(chars, toUpper);
        return new(chars);
    }
#endif
}
