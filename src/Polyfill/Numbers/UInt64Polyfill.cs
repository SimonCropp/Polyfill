#if !NET11_0_OR_GREATER

namespace Polyfills;

using System;
using System.Globalization;
// ReSharper disable once RedundantUsingDirective
using System.Text;

static partial class Polyfill
{
    extension(ulong)
    {
#if !NET8_0_OR_GREATER
#if !NET7_0_OR_GREATER

        /// <summary>
        /// Tries to parse a string into a value.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.uint64.tryparse?view=net-11.0#system-uint64-tryparse(system-string-system-iformatprovider-system-uint64@)
        public static bool TryParse(string? s, IFormatProvider? provider, out ulong result) =>
            ulong.TryParse(s, NumberStyles.Integer, provider, out result);

#endif

#if FeatureMemory

#if !NET8_0_OR_GREATER

        /// <summary>
        /// Tries to parse a span of UTF-8 characters into a value.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.uint64.tryparse?view=net-11.0#system-uint64-tryparse(system-readonlyspan((system-byte))-system-iformatprovider-system-uint64@)
        public static bool TryParse(ReadOnlySpan<byte> utf8Text, IFormatProvider? provider, out ulong result) =>
            ulong.TryParse(Encoding.UTF8.GetString(utf8Text), NumberStyles.Integer, provider, out result);

        /// <summary>
        /// Tries to parse a span of UTF-8 characters into a value.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.uint64.tryparse?view=net-11.0#system-uint64-tryparse(system-readonlyspan((system-byte))-system-globalization-numberstyles-system-iformatprovider-system-uint64@)
        public static bool TryParse(ReadOnlySpan<byte>utf8Text, NumberStyles style, IFormatProvider? provider, out ulong result) =>
            ulong.TryParse(Encoding.UTF8.GetString(utf8Text), style, provider, out result);

        /// <summary>
        /// Tries to convert a UTF-8 character span containing the string representation of a number to its ulong equivalent.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.uint64.tryparse?view=net-11.0#system-uint64-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-uint64@)
        public static bool TryParse(ReadOnlySpan<byte> utf8Text, out ulong result) =>
            ulong.TryParse(Encoding.UTF8.GetString(utf8Text), NumberStyles.Integer, null, out result);

#endif

#if !NETCOREAPP2_1_OR_GREATER && !NETSTANDARD2_1_OR_GREATER

        /// <summary>
        /// Converts the span representation of a number in a specified style and culture-specific format to its ulong equivalent. A return value indicates whether the conversion succeeded.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.uint64.tryparse?view=net-11.0#system-uint64-tryparse(system-readonlyspan((system-char))-system-uint64@)
        public static bool TryParse(ReadOnlySpan<char> s, out ulong result) =>
            ulong.TryParse(s.ToString(), out result);

        /// <summary>
        /// Converts the span representation of a number in a specified style and culture-specific format to its ulong equivalent. A return value indicates whether the conversion succeeded.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.uint64.tryparse?view=net-11.0#system-uint64-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-uint64@)
        public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider, out ulong result) =>
            ulong.TryParse(s.ToString(), style, provider, out result);

#endif

#if !NET7_0_OR_GREATER

        /// <summary>
        /// Tries to parse a span of characters into a value.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.uint64.tryparse?view=net-11.0#system-uint64-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-uint64@)
        public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, out ulong result) =>
            ulong.TryParse(s.ToString(), NumberStyles.Integer, provider, out result);

#endif

#endif
#endif

#if NET7_0_OR_GREATER && !NET9_0_OR_GREATER

        /// <summary>
        /// Produces the full product of two unsigned 64-bit numbers.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.uint64.bigmul?view=net-11.0
        //Note: The identical Math.BigMul overload is not polyfilled, since C# emits both static extension members onto the same class and they would collide.
        //Note: Only available on net7.0 and later, since UInt128 does not exist below that.
        public static UInt128 BigMul(ulong left, ulong right) =>
            (UInt128) left * right;

#endif

        /// <summary>
        /// Computes the base-10 logarithm of a value.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.uint64.log10?view=net-11.0
        public static ulong Log10(ulong value) =>
            (ulong) Log10Core(value);
    }
}

#endif
