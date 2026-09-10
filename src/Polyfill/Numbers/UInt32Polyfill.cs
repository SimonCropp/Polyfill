#if !NET11_0_OR_GREATER

namespace Polyfills;

using System;
using System.Globalization;
// ReSharper disable once RedundantUsingDirective
using System.Text;

static partial class Polyfill
{
    extension(uint)
    {
#if !NET8_0_OR_GREATER
#if !NET7_0_OR_GREATER

        /// <summary>
        /// Tries to parse a string into a value.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.uint32.tryparse?view=net-11.0#system-uint32-tryparse(system-string-system-iformatprovider-system-uint32@)
        public static bool TryParse(string? s, IFormatProvider? provider, out uint result) =>
            uint.TryParse(s, NumberStyles.Integer, provider, out result);

#endif

#if FeatureMemory

#if !NET8_0_OR_GREATER

        /// <summary>
        /// Tries to parse a span of UTF-8 characters into a value.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.uint32.tryparse?view=net-11.0#system-uint32-tryparse(system-readonlyspan((system-byte))-system-iformatprovider-system-uint32@)
        public static bool TryParse(ReadOnlySpan<byte> utf8Text, IFormatProvider? provider, out uint result) =>
            uint.TryParse(Encoding.UTF8.GetString(utf8Text), NumberStyles.Integer, provider, out result);

        /// <summary>
        /// Tries to parse a span of UTF-8 characters into a value.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.uint32.tryparse?view=net-11.0#system-uint32-tryparse(system-readonlyspan((system-byte))-system-globalization-numberstyles-system-iformatprovider-system-uint32@)
        public static bool TryParse(ReadOnlySpan<byte> utf8Text, NumberStyles style, IFormatProvider? provider, out uint result) =>
            uint.TryParse(Encoding.UTF8.GetString(utf8Text), style, provider, out result);

        /// <summary>
        /// Tries to convert a UTF-8 character span containing the string representation of a number to its uint equivalent.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.uint32.tryparse?view=net-11.0#system-uint32-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-uint32@)
        public static bool TryParse(ReadOnlySpan<byte> utf8Text, out uint result) =>
            uint.TryParse(Encoding.UTF8.GetString(utf8Text), NumberStyles.Integer, null, out result);

#endif

#if !NETCOREAPP2_1_OR_GREATER && !NETSTANDARD2_1_OR_GREATER

        /// <summary>
        /// Converts the span representation of a number in a specified style and culture-specific format to its uint equivalent. A return value indicates whether the conversion succeeded.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.uint32.tryparse?view=net-11.0#system-uint32-tryparse(system-readonlyspan((system-char))-system-uint32@)
        public static bool TryParse(ReadOnlySpan<char> s, out uint result) =>
            uint.TryParse(s.ToString(), out result);

        /// <summary>
        /// Converts the span representation of a number in a specified style and culture-specific format to its uint equivalent. A return value indicates whether the conversion succeeded.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.uint32.tryparse?view=net-11.0#system-uint32-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-uint32@)
        public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider, out uint result) =>
            uint.TryParse(s.ToString(), style, provider, out result);

#endif

#if !NET7_0_OR_GREATER

        /// <summary>
        /// Tries to parse a span of characters into a value.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.uint32.tryparse?view=net-11.0#system-uint32-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-uint32@)
        public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, out uint result) =>
            uint.TryParse(s.ToString(), NumberStyles.Integer, provider, out result);

#endif

#endif
#endif

#if !NET9_0_OR_GREATER

        /// <summary>
        /// Produces the full product of two unsigned 32-bit numbers.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.uint32.bigmul?view=net-11.0
        //Note: The identical Math.BigMul overload is not polyfilled, since C# emits both static extension members onto the same class and they would collide.
        public static ulong BigMul(uint left, uint right) =>
            (ulong) left * right;

#endif

        /// <summary>
        /// Computes the base-10 logarithm of a value.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.uint32.log10?view=net-11.0
        public static uint Log10(uint value) =>
            (uint) Log10Core(value);
    }
}
#endif
