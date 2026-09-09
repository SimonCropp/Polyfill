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
    }
}

#endif
