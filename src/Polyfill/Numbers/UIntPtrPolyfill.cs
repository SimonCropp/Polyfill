#if !NET8_0_OR_GREATER

namespace Polyfills;

using System;
using System.Globalization;
// ReSharper disable once RedundantUsingDirective
using System.Text;

static partial class Polyfill
{
    extension(nuint)
    {
#if !NET5_0_OR_GREATER

        /// <summary>
        /// Tries to convert the string representation of a number to its native unsigned integer equivalent. A return value indicates whether the conversion succeeded.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.uintptr.tryparse?view=net-11.0#system-uintptr-tryparse(system-string-system-uintptr@)
        public static bool TryParse(string? s, out nuint result)
        {
            if (UIntPtr.Size == 4)
            {
                var success = uint.TryParse(s, out var uintResult);
                result = uintResult;
                return success;
            }

            var ulongSuccess = ulong.TryParse(s, out var ulongResult);
            result = (nuint)ulongResult;
            return ulongSuccess;
        }

        /// <summary>
        /// Tries to convert the string representation of a number in a specified style and culture-specific format to its native unsigned integer equivalent. A return value indicates whether the conversion succeeded.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.uintptr.tryparse?view=net-11.0#system-uintptr-tryparse(system-string-system-globalization-numberstyles-system-iformatprovider-system-uintptr@)
        public static bool TryParse(string? s, NumberStyles style, IFormatProvider? provider, out nuint result)
        {
            if (UIntPtr.Size == 4)
            {
                var success = uint.TryParse(s, style, provider, out var uintResult);
                result = uintResult;
                return success;
            }

            var ulongSuccess = ulong.TryParse(s, style, provider, out var ulongResult);
            result = (nuint)ulongResult;
            return ulongSuccess;
        }

#endif

#if !NET7_0_OR_GREATER

        /// <summary>
        /// Tries to parse a string into a value.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.uintptr.tryparse?view=net-11.0#system-uintptr-tryparse(system-string-system-iformatprovider-system-uintptr@)
        public static bool TryParse(string? s, IFormatProvider? provider, out nuint result) =>
            nuint.TryParse(s, NumberStyles.Integer, provider, out result);

#endif

#if FeatureMemory

#if !NET8_0_OR_GREATER

        /// <summary>
        /// Tries to parse a span of UTF-8 characters into a value.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.uintptr.tryparse?view=net-11.0#system-uintptr-tryparse(system-readonlyspan((system-byte))-system-iformatprovider-system-uintptr@)
        public static bool TryParse(ReadOnlySpan<byte> utf8Text, IFormatProvider? provider, out nuint result) =>
            nuint.TryParse(Encoding.UTF8.GetString(utf8Text), NumberStyles.Integer, provider, out result);

        /// <summary>
        /// Tries to parse a span of UTF-8 characters into a value.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.uintptr.tryparse?view=net-11.0#system-uintptr-tryparse(system-readonlyspan((system-byte))-system-globalization-numberstyles-system-iformatprovider-system-uintptr@)
        public static bool TryParse(ReadOnlySpan<byte> utf8Text, NumberStyles style, IFormatProvider? provider, out nuint result) =>
            nuint.TryParse(Encoding.UTF8.GetString(utf8Text), style, provider, out result);

        /// <summary>
        /// Tries to parse a span of UTF-8 characters into a value.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.uintptr.tryparse?view=net-11.0#system-uintptr-tryparse(system-readonlyspan((system-byte))-system-uintptr@)
        public static bool TryParse(ReadOnlySpan<byte> utf8Text, out nuint result) =>
            nuint.TryParse(Encoding.UTF8.GetString(utf8Text), NumberStyles.Integer, null, out result);

#endif

#if !NET7_0_OR_GREATER

        /// <summary>
        /// Tries to convert the span representation of a number to its native unsigned integer equivalent. A return value indicates whether the conversion succeeded.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.uintptr.tryparse?view=net-11.0#system-uintptr-tryparse(system-readonlyspan((system-char))-system-uintptr@)
        public static bool TryParse(ReadOnlySpan<char> s, out nuint result) =>
            nuint.TryParse(s.ToString(), out result);

        /// <summary>
        /// Tries to convert the span representation of a number in a specified style and culture-specific format to its native unsigned integer equivalent. A return value indicates whether the conversion succeeded.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.uintptr.tryparse?view=net-11.0#system-uintptr-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-uintptr@)
        public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider, out nuint result) =>
            nuint.TryParse(s.ToString(), style, provider, out result);

        /// <summary>
        /// Tries to parse a span of characters into a value.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.uintptr.tryparse?view=net-11.0#system-uintptr-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-uintptr@)
        public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, out nuint result) =>
            nuint.TryParse(s.ToString(), NumberStyles.Integer, provider, out result);

#endif

#endif
    }
}
#endif
