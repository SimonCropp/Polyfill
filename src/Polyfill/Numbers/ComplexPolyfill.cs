#if FeatureNumerics && NET7_0_OR_GREATER && !NET11_0_OR_GREATER

namespace Polyfills;

using System;
using System.Globalization;
using System.Numerics;
using System.Text;

static partial class Polyfill
{
    extension(Complex)
    {
        /// <summary>
        /// Tries to parse a span of UTF-8 characters into a value.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.numerics.complex.tryparse?view=net-11.0#system-numerics-complex-tryparse(system-readonlyspan((system-byte))-system-globalization-numberstyles-system-iformatprovider-system-numerics-complex@)
        //Note: The matching Parse(ReadOnlySpan<byte>, ...) overloads are not polyfilled, since they would collide with BigInteger.Parse(ReadOnlySpan<byte>, ...); two static extension members with the same signature cannot coexist on one class.
        public static bool TryParse(ReadOnlySpan<byte> utf8Text, NumberStyles style, IFormatProvider? provider, out Complex result) =>
#if NET7_0
            TryParseComplex(utf8Text, style, provider, out result);
#else
            Complex.TryParse(Encoding.UTF8.GetString(utf8Text), style, provider, out result);
#endif

        /// <summary>
        /// Tries to parse a span of UTF-8 characters into a value.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.numerics.complex.tryparse?view=net-11.0#system-numerics-complex-tryparse(system-readonlyspan((system-byte))-system-iformatprovider-system-numerics-complex@)
        public static bool TryParse(ReadOnlySpan<byte> utf8Text, IFormatProvider? provider, out Complex result) =>
#if NET7_0
            TryParseComplex(utf8Text, NumberStyles.Float | NumberStyles.AllowThousands, provider, out result);
#else
            Complex.TryParse(Encoding.UTF8.GetString(utf8Text), provider, out result);
#endif

#if NET7_0
        // net7 slices the real and the imaginary parts by index instead of by count, so its Complex.TryParse includes a delimiter in both and never succeeds. Fixed in net8, so parse here to match that behavior.
        static bool TryParseComplex(ReadOnlySpan<byte> utf8Text, NumberStyles style, IFormatProvider? provider, out Complex result)
        {
            // Validates the style the way Complex.TryParse does, throwing for hex and other unsupported styles
            _ = double.TryParse(ReadOnlySpan<char>.Empty, style, provider, out _);

            var text = Encoding.UTF8.GetString(utf8Text).AsSpan();

            var openBracket = text.IndexOf('<');
            var semicolon = text.IndexOf(';');
            var closeBracket = text.IndexOf('>');

            // At least 5 characters are needed for `<0;0>`, with the brackets and the semicolon in that order
            if (text.Length < 5 ||
                openBracket == -1 ||
                semicolon == -1 ||
                closeBracket == -1 ||
                openBracket > semicolon ||
                openBracket > closeBracket ||
                semicolon > closeBracket)
            {
                result = default;
                return false;
            }

            if (openBracket != 0 &&
                ((style & NumberStyles.AllowLeadingWhite) == 0 ||
                 !text[..openBracket].IsWhiteSpace()))
            {
                result = default;
                return false;
            }

            if (!double.TryParse(text.Slice(openBracket + 1, semicolon - openBracket - 1), style, provider, out var real))
            {
                result = default;
                return false;
            }

            if (char.IsWhiteSpace(text[semicolon + 1]))
            {
                // A single whitespace after the semicolon is allowed regardless of style, so that the output of ToString round trips
                semicolon++;
            }

            if (!double.TryParse(text.Slice(semicolon + 1, closeBracket - semicolon - 1), style, provider, out var imaginary))
            {
                result = default;
                return false;
            }

            // Matches the BCL, which slices from the closing bracket rather than past it, and so rejects trailing whitespace
            if (closeBracket != text.Length - 1 &&
                ((style & NumberStyles.AllowTrailingWhite) == 0 ||
                 !text[closeBracket..].IsWhiteSpace()))
            {
                result = default;
                return false;
            }

            result = new(real, imaginary);
            return true;
        }
#endif
    }
}
#endif
