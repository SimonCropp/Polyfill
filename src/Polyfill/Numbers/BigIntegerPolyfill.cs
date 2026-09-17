#if FeatureNumerics && FeatureMemory && !NET11_0_OR_GREATER

namespace Polyfills;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;
// ReSharper disable once RedundantUsingDirective
using System.Text;

static partial class Polyfill
{
    extension(BigInteger)
    {
        /// <summary>
        /// Parses a span of UTF-8 characters into a value.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.numerics.biginteger.parse?view=net-11.0#system-numerics-biginteger-parse(system-readonlyspan((system-byte))-system-globalization-numberstyles-system-iformatprovider)
        public static BigInteger Parse(ReadOnlySpan<byte> utf8Text, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = null) =>
            BigInteger.Parse(Encoding.UTF8.GetString(utf8Text), style, provider);

        /// <summary>
        /// Parses a span of UTF-8 characters into a value.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.numerics.biginteger.parse?view=net-11.0#system-numerics-biginteger-parse(system-readonlyspan((system-byte))-system-iformatprovider)
        public static BigInteger Parse(ReadOnlySpan<byte> utf8Text, IFormatProvider? provider) =>
            BigInteger.Parse(Encoding.UTF8.GetString(utf8Text), NumberStyles.Integer, provider);

        /// <summary>
        /// Tries to parse a span of UTF-8 characters into a value.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.numerics.biginteger.tryparse?view=net-11.0#system-numerics-biginteger-tryparse(system-readonlyspan((system-byte))-system-numerics-biginteger@)
        public static bool TryParse(ReadOnlySpan<byte> utf8Text, out BigInteger result) =>
            BigInteger.TryParse(Encoding.UTF8.GetString(utf8Text), out result);

        /// <summary>
        /// Tries to parse a span of UTF-8 characters into a value.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.numerics.biginteger.tryparse?view=net-11.0#system-numerics-biginteger-tryparse(system-readonlyspan((system-byte))-system-globalization-numberstyles-system-iformatprovider-system-numerics-biginteger@)
        public static bool TryParse(ReadOnlySpan<byte> utf8Text, NumberStyles style, IFormatProvider? provider, out BigInteger result) =>
            BigInteger.TryParse(Encoding.UTF8.GetString(utf8Text), style, provider, out result);

        /// <summary>
        /// Tries to parse a span of UTF-8 characters into a value.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.numerics.biginteger.tryparse?view=net-11.0#system-numerics-biginteger-tryparse(system-readonlyspan((system-byte))-system-iformatprovider-system-numerics-biginteger@)
        public static bool TryParse(ReadOnlySpan<byte> utf8Text, IFormatProvider? provider, out BigInteger result) =>
            BigInteger.TryParse(Encoding.UTF8.GetString(utf8Text), NumberStyles.Integer, provider, out result);
    }

    extension(BigInteger target)
    {
        /// <summary>
        /// Tries to format the value of the current instance as UTF-8 into the provided span of bytes.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.numerics.biginteger.tryformat?view=net-11.0#system-numerics-biginteger-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider)
        public bool TryFormat(Span<byte> utf8Destination, out int bytesWritten, [StringSyntax(StringSyntaxAttribute.NumericFormat)] ReadOnlySpan<char> format = default, IFormatProvider? provider = null)
        {
            string result;
            if (format.Length == 0)
            {
                result = target.ToString(null, provider);
            }
            else
            {
                result = target.ToString(format.ToString(), provider);
            }

            return Encoding.UTF8.TryGetBytes(result.AsSpan(), utf8Destination, out bytesWritten);
        }
    }
}
#endif
