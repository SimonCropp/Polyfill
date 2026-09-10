#if NET7_0_OR_GREATER && !NET11_0_OR_GREATER

namespace Polyfills;

using System;

static partial class Polyfill
{
    extension(UInt128)
    {
        /// <summary>
        /// Computes the base-10 logarithm of a value.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.uint128.log10?view=net-11.0
        public static UInt128 Log10(UInt128 value) =>
            (UInt128) Log10Core(value);

        /// <summary>
        /// Produces the full product of two unsigned 128-bit numbers.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.uint128.bigmul?view=net-11.0
        public static UInt128 BigMul(UInt128 left, UInt128 right, out UInt128 lower) =>
            BigMulCore(left, right, out lower);
    }

    // Splits each operand into 64-bit limbs and accumulates the four partial products.
    // Every partial product of two 64-bit limbs is exact in 128 bits, so no wider
    // intermediate type is needed.
    static UInt128 BigMulCore(UInt128 left, UInt128 right, out UInt128 lower)
    {
        unchecked
        {
            UInt128 leftLower = (ulong) left;
            UInt128 leftUpper = (ulong) (left >> 64);
            UInt128 rightLower = (ulong) right;
            UInt128 rightUpper = (ulong) (right >> 64);

            var lowerLower = leftLower * rightLower;
            var middle = leftUpper * rightLower + (lowerLower >> 64);
            var middleLower = leftLower * rightUpper + (ulong) middle;

            lower = (middleLower << 64) | (ulong) lowerLower;
            return leftUpper * rightUpper + (middle >> 64) + (middleLower >> 64);
        }
    }
}

#endif
