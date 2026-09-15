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
            Complex.TryParse(Encoding.UTF8.GetString(utf8Text), style, provider, out result);

        /// <summary>
        /// Tries to parse a span of UTF-8 characters into a value.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.numerics.complex.tryparse?view=net-11.0#system-numerics-complex-tryparse(system-readonlyspan((system-byte))-system-iformatprovider-system-numerics-complex@)
        public static bool TryParse(ReadOnlySpan<byte> utf8Text, IFormatProvider? provider, out Complex result) =>
            Complex.TryParse(Encoding.UTF8.GetString(utf8Text), provider, out result);
    }
}
#endif
