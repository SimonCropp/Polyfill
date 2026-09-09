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
