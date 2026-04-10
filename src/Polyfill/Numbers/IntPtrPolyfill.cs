#if !NET8_0_OR_GREATER

namespace Polyfills;

using System;
using System.Globalization;
// ReSharper disable once RedundantUsingDirective
using System.Text;

static partial class Polyfill
{
    extension(nint)
    {
#if !NET5_0_OR_GREATER

        /// <summary>
        /// Tries to convert the string representation of a number to its native signed integer equivalent. A return value indicates whether the conversion succeeded.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.intptr.tryparse?view=net-11.0#system-intptr-tryparse(system-string-system-intptr@)
        public static bool TryParse(string? s, out nint result)
        {
            if (IntPtr.Size == 4)
            {
                var success = int.TryParse(s, out var intResult);
                result = intResult;
                return success;
            }

            var longSuccess = long.TryParse(s, out var longResult);
            result = (nint)longResult;
            return longSuccess;
        }

        /// <summary>
        /// Tries to convert the string representation of a number in a specified style and culture-specific format to its native signed integer equivalent. A return value indicates whether the conversion succeeded.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.intptr.tryparse?view=net-11.0#system-intptr-tryparse(system-string-system-globalization-numberstyles-system-iformatprovider-system-intptr@)
        public static bool TryParse(string? s, NumberStyles style, IFormatProvider? provider, out nint result)
        {
            if (IntPtr.Size == 4)
            {
                var success = int.TryParse(s, style, provider, out var intResult);
                result = intResult;
                return success;
            }

            var longSuccess = long.TryParse(s, style, provider, out var longResult);
            result = (nint)longResult;
            return longSuccess;
        }

#endif

#if !NET7_0_OR_GREATER

        /// <summary>
        /// Tries to parse a string into a value.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.intptr.tryparse?view=net-11.0#system-intptr-tryparse(system-string-system-iformatprovider-system-intptr@)
        public static bool TryParse(string? s, IFormatProvider? provider, out nint result) =>
            nint.TryParse(s, NumberStyles.Integer, provider, out result);

#endif

#if FeatureMemory

#if !NET8_0_OR_GREATER

        /// <summary>
        /// Tries to parse a span of UTF-8 characters into a value.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.intptr.tryparse?view=net-11.0#system-intptr-tryparse(system-readonlyspan((system-byte))-system-iformatprovider-system-intptr@)
        public static bool TryParse(ReadOnlySpan<byte> utf8Text, IFormatProvider? provider, out nint result) =>
            nint.TryParse(Encoding.UTF8.GetString(utf8Text), NumberStyles.Integer, provider, out result);

        /// <summary>
        /// Tries to parse a span of UTF-8 characters into a value.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.intptr.tryparse?view=net-11.0#system-intptr-tryparse(system-readonlyspan((system-byte))-system-globalization-numberstyles-system-iformatprovider-system-intptr@)
        public static bool TryParse(ReadOnlySpan<byte> utf8Text, NumberStyles style, IFormatProvider? provider, out nint result) =>
            nint.TryParse(Encoding.UTF8.GetString(utf8Text), style, provider, out result);

        /// <summary>
        /// Tries to parse a span of UTF-8 characters into a value.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.intptr.tryparse?view=net-11.0#system-intptr-tryparse(system-readonlyspan((system-byte))-system-intptr@)
        public static bool TryParse(ReadOnlySpan<byte> utf8Text, out nint result) =>
            nint.TryParse(Encoding.UTF8.GetString(utf8Text), NumberStyles.Integer, null, out result);

#endif

#if !NET7_0_OR_GREATER

        /// <summary>
        /// Tries to convert the span representation of a number to its native signed integer equivalent. A return value indicates whether the conversion succeeded.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.intptr.tryparse?view=net-11.0#system-intptr-tryparse(system-readonlyspan((system-char))-system-intptr@)
        public static bool TryParse(ReadOnlySpan<char> s, out nint result) =>
            nint.TryParse(s.ToString(), out result);

        /// <summary>
        /// Tries to convert the span representation of a number in a specified style and culture-specific format to its native signed integer equivalent. A return value indicates whether the conversion succeeded.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.intptr.tryparse?view=net-11.0#system-intptr-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-intptr@)
        public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider, out nint result) =>
            nint.TryParse(s.ToString(), style, provider, out result);

        /// <summary>
        /// Tries to parse a span of characters into a value.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.intptr.tryparse?view=net-11.0#system-intptr-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-intptr@)
        public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, out nint result) =>
            nint.TryParse(s.ToString(), NumberStyles.Integer, provider, out result);

#endif

#endif
    }
}
#endif
