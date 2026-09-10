#if NET7_0_OR_GREATER && !NET11_0_OR_GREATER

namespace Polyfills;

using System;

static partial class Polyfill
{
    extension(Int128)
    {
        /// <summary>
        /// Computes the base-10 logarithm of a value.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.int128.log10?view=net-11.0
        public static Int128 Log10(Int128 value)
        {
            if (value < Int128.Zero)
            {
                throw new ArgumentOutOfRangeException(nameof(value), value, "value must be a non-negative number.");
            }

            return Log10Core((UInt128) value);
        }

        /// <summary>
        /// Produces the full product of two 128-bit numbers.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.int128.bigmul?view=net-11.0
        public static Int128 BigMul(Int128 left, Int128 right, out Int128 lower)
        {
            unchecked
            {
                var upper = BigMulCore((UInt128) left, (UInt128) right, out var unsignedLower);
                lower = (Int128) unsignedLower;
                // reinterpreting the unsigned product as signed overshoots by one operand for
                // each negative operand, so subtract the other operand for each sign bit set
                return (Int128) upper - ((left >> 127) & right) - ((right >> 127) & left);
            }
        }
    }

    static Int128 Log10Core(UInt128 value)
    {
        var result = Int128.Zero;
        while (value >= 10)
        {
            value /= 10;
            result++;
        }

        return result;
    }
}

#endif
