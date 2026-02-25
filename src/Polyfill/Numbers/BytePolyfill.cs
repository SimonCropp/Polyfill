#nullable enable

#if !NET8_0_OR_GREATER

namespace Polyfills;

using System;
using System.Globalization;
// ReSharper disable once RedundantUsingDirective
using System.Text;

static partial class Polyfill
{
    extension(Byte)
    {
#if !NET7_0_OR_GREATER

        /// <summary>
        /// Tries to parse a string into a value.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.byte.tryparse?view=net-11.0#system-byte-tryparse(system-string-system-iformatprovider-system-byte@)
        public static bool TryParse(string? s, IFormatProvider? provider, out byte result) =>
            byte.TryParse(s, NumberStyles.Integer, provider, out result);

#endif

#if FeatureMemory

#if !NET7_0_OR_GREATER

        /// <summary>
        /// Tries to parse a span of characters into a value.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.byte.tryparse?view=net-11.0#system-byte-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-byte@)
        public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, out byte result) =>
            byte.TryParse(s.ToString(), NumberStyles.Integer, provider, out result);

#endif

#if !NET8_0_OR_GREATER

        /// <summary>
        /// Tries to parse a span of UTF-8 characters into a value.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.byte.tryparse?view=net-11.0#system-byte-tryparse(system-readonlyspan((system-byte))-system-iformatprovider-system-byte@)
        public static bool TryParse(ReadOnlySpan<byte> utf8Text, IFormatProvider? provider, out byte result) =>
            byte.TryParse(Encoding.UTF8.GetString(utf8Text), NumberStyles.Integer, provider, out result);

        /// <summary>
        /// Tries to parse a span of UTF-8 characters into a value.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.byte.tryparse?view=net-11.0#system-byte-tryparse(system-readonlyspan((system-byte))-system-globalization-numberstyles-system-iformatprovider-system-byte@)
        public static bool TryParse(ReadOnlySpan<byte> utf8Text, NumberStyles style, IFormatProvider? provider, out byte result) =>
            byte.TryParse(Encoding.UTF8.GetString(utf8Text), style, provider, out result);

        /// <summary>
        /// Tries to convert a UTF-8 character span containing the string representation of a number to its byte equivalent.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.byte.tryparse?view=net-11.0#system-byte-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-byte@)
        public static bool TryParse(ReadOnlySpan<byte> utf8Text, out byte result) =>
            byte.TryParse(Encoding.UTF8.GetString(utf8Text), NumberStyles.Integer, null, out result);

#endif

#if !NETCOREAPP2_1_OR_GREATER && !NETSTANDARD2_1_OR_GREATER

        /// <summary>
        /// Converts the span representation of a number in a specified style and culture-specific format to its byte equivalent. A return value indicates whether the conversion succeeded.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.byte.tryparse?view=net-11.0#system-byte-tryparse(system-readonlyspan((system-char))-system-byte@)
        public static bool TryParse(ReadOnlySpan<char> s, out byte result) =>
            byte.TryParse(s.ToString(), out result);

        /// <summary>
        /// Converts the span representation of a number in a specified style and culture-specific format to its byte equivalent. A return value indicates whether the conversion succeeded.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.byte.tryparse?view=net-11.0#system-byte-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-byte@)
        public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider, out byte result) =>
            byte.TryParse(s.ToString(), style, provider, out result);

#endif
#endif
    }
}
#endif
