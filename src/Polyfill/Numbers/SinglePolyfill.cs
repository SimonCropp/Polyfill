#if !NET8_0_OR_GREATER

namespace Polyfills;

using System;
using System.Globalization;
// ReSharper disable once RedundantUsingDirective
using System.Text;

static partial class Polyfill
{
    extension(float)
    {
#if !NET7_0_OR_GREATER

        /// <summary>
        /// Tries to parse a string into a value.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.single.tryparse?view=net-11.0#system-single-tryparse(system-string-system-iformatprovider-system-single@)
        public static bool TryParse(string? s, IFormatProvider? provider, out float result) =>
            float.TryParse(s, NumberStyles.Float, provider, out result);

#endif

#if FeatureMemory
#if !NET8_0_OR_GREATER

        /// <summary>
        /// Tries to parse a span of UTF-8 characters into a value.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.single.tryparse?view=net-11.0#system-single-tryparse(system-readonlyspan((system-byte))-system-iformatprovider-system-single@)
        public static bool TryParse(ReadOnlySpan<byte> utf8Text, IFormatProvider? provider, out float result) =>
            float.TryParse(Encoding.UTF8.GetString(utf8Text), NumberStyles.Float, provider, out result);

        /// <summary>
        /// Tries to parse a span of UTF-8 characters into a value.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.single.tryparse?view=net-11.0#system-single-tryparse(system-readonlyspan((system-byte))-system-globalization-numberstyles-system-iformatprovider-system-single@)
        public static bool TryParse(ReadOnlySpan<byte> utf8Text, NumberStyles style, IFormatProvider? provider, out float result) =>
            float.TryParse(Encoding.UTF8.GetString(utf8Text), style, provider, out result);

        /// <summary>
        /// Tries to convert a UTF-8 character span containing the string representation of a number to its single-precision floating-point number equivalent.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.single.tryparse?view=net-11.0#system-single-tryparse(system-readonlyspan((system-byte))-system-single@)
        public static bool TryParse(ReadOnlySpan<byte> utf8Text, out float result) =>
            float.TryParse(Encoding.UTF8.GetString(utf8Text), NumberStyles.Float, null, out result);

#endif

#if !NETCOREAPP2_1_OR_GREATER && !NETSTANDARD2_1_OR_GREATER

        /// <summary>
        /// Converts the span representation of a number in a specified style and culture-specific format to its single-precision floating-point number equivalent. A return value indicates whether the conversion succeeded or failed.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.single.tryparse?view=net-11.0#system-single-tryparse(system-readonlyspan((system-char))-system-single@)
        public static bool TryParse(ReadOnlySpan<char> s, out float result) =>
            float.TryParse(s.ToString(), out result);

        /// <summary>
        /// Converts the string representation of a number in a specified style and culture-specific format to its single-precision floating-point number equivalent. A return value indicates whether the conversion succeeded or failed.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.single.tryparse?view=net-11.0#system-single-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-single@)
        public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider, out float result) =>
            float.TryParse(s.ToString(), style, provider, out result);

#endif

#if !NET7_0_OR_GREATER

        /// <summary>
        /// Tries to parse a span of characters into a value.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.single.tryparse?view=net-11.0#system-single-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-single@)
        public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, out float result) =>
            float.TryParse(s.ToString(), NumberStyles.Float, provider, out result);

#endif

#endif
    }
}
#endif
