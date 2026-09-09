namespace Polyfills;

static partial class Polyfill
{
    extension(char)
    {
#if !NET6_0_OR_GREATER
        /// <summary>
        /// Indicates whether a character is categorized as an ASCII character.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.char.isascii?view=net-11.0
        public static bool IsAscii(char c) =>
            c is >= '\u0000' and <= '\u007F';
#endif

#if !NET7_0_OR_GREATER
        /// <summary>
        /// Indicates whether a character is categorized as an ASCII letter or an ASCII digit.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.char.isasciiletterordigit?view=net-11.0
        public static bool IsAsciiLetterOrDigit(char c) =>
            char.IsAsciiLetter(c) || char.IsAsciiDigit(c);

        /// <summary>
        /// Indicates whether a character is categorized as an ASCII digit.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.char.isasciidigit?view=net-11.0
        public static bool IsAsciiDigit(char c) =>
            c is >= '\u0030' and <= '\u0039';

        /// <summary>
        /// Indicates whether a character is categorized as an ASCII hexadecimal digit.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.char.isasciihexdigit?view=net-11.0
        public static bool IsAsciiHexDigit(char c) =>
            char.IsAsciiHexDigitUpper(c) || char.IsAsciiHexDigitLower(c);

        /// <summary>
        /// Indicates whether a character is categorized as an ASCII lower-case hexadecimal digit.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.char.isasciihexdigitlower?view=net-11.0
        public static bool IsAsciiHexDigitLower(char c) =>
            char.IsAsciiDigit(c) || c is >= '\u0061' and <= '\u0066';

        /// <summary>
        /// Indicates whether a character is categorized as an ASCII upper-case hexadecimal digit.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.char.isasciihexdigitupper?view=net-11.0
        public static bool IsAsciiHexDigitUpper(char c) =>
            char.IsAsciiDigit(c) || c is >= '\u0041' and <= '\u0046';

        /// <summary>
        /// Indicates whether a character is categorized as an ASCII letter.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.char.isasciiletter?view=net-11.0
        public static bool IsAsciiLetter(char c) =>
            char.IsAsciiLetterUpper(c) || char.IsAsciiLetterLower(c);

        /// <summary>
        /// Indicates whether a character is categorized as an upper case ASCII letter.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.char.isasciiletterupper?view=net-11.0
        public static bool IsAsciiLetterUpper(char c) =>
            c is >= '\u0041' and <= '\u005a';

        /// <summary>
        /// Indicates whether a character is categorized as a lower case ASCII letter.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.char.isasciiletterlower?view=net-11.0
        public static bool IsAsciiLetterLower(char c) =>
            c is >= '\u0061' and <= '\u007a';
#endif

#if !NET8_0_OR_GREATER
        /// <summary>
        /// Indicates whether a character is within the specified inclusive range.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.char.isbetween?view=net-11.0
        public static bool IsBetween(char c, char minInclusive, char maxInclusive) =>
            (uint)(c - minInclusive) <= (uint)(maxInclusive - minInclusive);
#endif

#if !NET11_0_OR_GREATER
        /// <summary>
        /// Converts the value of a character to its lowercase equivalent using ordinal casing rules.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.char.tolowerordinal?view=net-11.0
        //Note: Derived from invariant casing, so the mapping follows the Unicode version of the running framework rather than the one net11 is built against.
        public static char ToLowerOrdinal(char c) =>
            ToLowerOrdinalChar(c);

        /// <summary>
        /// Converts the value of a character to its uppercase equivalent using ordinal casing rules.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.char.toupperordinal?view=net-11.0
        //Note: Derived from invariant casing, so the mapping follows the Unicode version of the running framework rather than the one net11 is built against.
        public static char ToUpperOrdinal(char c) =>
            ToUpperOrdinalChar(c);
#endif
    }

#if !NET11_0_OR_GREATER

    internal static char ToUpperOrdinalChar(char c) =>
        // ordinal casing leaves the long s uncased, where invariant casing maps it to S
        c == 'ſ' ? c : char.ToUpperInvariant(c);

    internal static char ToLowerOrdinalChar(char c)
    {
        var lower = char.ToLowerInvariant(c);
        if (lower == c)
        {
            return c;
        }

        // Ordinal lower casing must never move a character out of its ordinal upper-casing class, otherwise it
        // stops being consistent with OrdinalIgnoreCase. The Kelvin, Ohm and Angstrom signs are the usual examples.
        return ToUpperOrdinalChar(lower) == ToUpperOrdinalChar(c) ? lower : c;
    }

    /// <summary>
    /// Applies ordinal casing to <paramref name="chars"/> in place, treating surrogate pairs as a single scalar.
    /// </summary>
    internal static void ToOrdinalCase(char[] chars, bool toUpper)
    {
        for (var index = 0; index < chars.Length; index++)
        {
            var current = chars[index];
            if (char.IsHighSurrogate(current) &&
                index + 1 < chars.Length &&
                char.IsLowSurrogate(chars[index + 1]))
            {
                var pair = new string(chars, index, 2);
                var cased = ToOrdinalCaseSurrogatePair(pair, toUpper);
                chars[index] = cased[0];
                chars[index + 1] = cased[1];
                index++;
                continue;
            }

            chars[index] = toUpper ? ToUpperOrdinalChar(current) : ToLowerOrdinalChar(current);
        }
    }

    internal static string ToOrdinalCaseSurrogatePair(string pair, bool toUpper)
    {
        if (toUpper)
        {
            return ToUpperOrdinalSurrogatePair(pair);
        }

        var lower = pair.ToLowerInvariant();
        if (lower.Length != 2 ||
            lower == pair)
        {
            return pair;
        }

        return ToUpperOrdinalSurrogatePair(lower) == ToUpperOrdinalSurrogatePair(pair) ? lower : pair;
    }

    static string ToUpperOrdinalSurrogatePair(string pair)
    {
        var upper = pair.ToUpperInvariant();
        return upper.Length == 2 ? upper : pair;
    }

#endif
}
