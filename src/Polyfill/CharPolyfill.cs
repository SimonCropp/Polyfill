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
        /// <returns>Link: https://learn.microsoft.com/en-us/dotnet/api/system.char.isasciiletterordigit?view=net-11.0</returns>
        public static bool IsAsciiLetterOrDigit(char c) =>
            char.IsAsciiLetter(c) || char.IsAsciiDigit(c);

        /// <summary>
        /// Indicates whether a character is categorized as an ASCII digit.
        /// </summary>
        /// <returns>Link: https://learn.microsoft.com/en-us/dotnet/api/system.char.isasciidigit?view=net-11.0</returns>
        public static bool IsAsciiDigit(char c) =>
            c is >= '\u0030' and <= '\u0039';

        /// <summary>
        /// Indicates whether a character is categorized as an ASCII hexadecimal digit.
        /// </summary>
        /// <returns>Link: https://learn.microsoft.com/en-us/dotnet/api/system.char.isasciihexdigit?view=net-11.0</returns>
        public static bool IsAsciiHexDigit(char c) =>
            char.IsAsciiHexDigitUpper(c) || char.IsAsciiHexDigitLower(c);

        /// <summary>
        /// Indicates whether a character is categorized as an ASCII lower-case hexadecimal digit.
        /// </summary>
        /// <returns>Link: https://learn.microsoft.com/en-us/dotnet/api/system.char.isasciihexdigitlower?view=net-11.0</returns>
        public static bool IsAsciiHexDigitLower(char c) =>
            char.IsAsciiDigit(c) || c is >= '\u0061' and <= '\u0066';

        /// <summary>
        /// Indicates whether a character is categorized as an ASCII upper-case hexadecimal digit.
        /// </summary>
        /// <returns>Link: https://learn.microsoft.com/en-us/dotnet/api/system.char.isasciihexdigitupper?view=net-11.0</returns>
        public static bool IsAsciiHexDigitUpper(char c) =>
            char.IsAsciiDigit(c) || c is >= '\u0041' and <= '\u0046';

        /// <summary>
        /// Indicates whether a character is categorized as an ASCII letter.
        /// </summary>
        /// <returns>Link: https://learn.microsoft.com/en-us/dotnet/api/system.char.isasciiletter?view=net-11.0</returns>
        public static bool IsAsciiLetter(char c) =>
            char.IsAsciiLetterUpper(c) || char.IsAsciiLetterLower(c);

        /// <summary>
        /// Indicates whether a character is categorized as an upper case ASCII letter.
        /// </summary>
        /// <returns>Link: https://learn.microsoft.com/en-us/dotnet/api/system.char.isasciiletterupper?view=net-11.0</returns>
        public static bool IsAsciiLetterUpper(char c) =>
            c is >= '\u0041' and <= '\u005a';

        /// <summary>
        /// Indicates whether a character is categorized as a lower case ASCII letter.
        /// </summary>
        /// <returns>Link: https://learn.microsoft.com/en-us/dotnet/api/system.char.isasciiletterlower?view=net-11.0</returns>
        public static bool IsAsciiLetterLower(char c) =>
            c is >= '\u0061' and <= '\u007a';
#endif
    }
}
