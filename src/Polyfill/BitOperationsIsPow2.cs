#pragma warning disable

// BitOperations exists in the BCL from netcoreapp3.0, but IsPow2 was added in net6.0
// and its nint/nuint overloads in net7.0. Where the type is present but an overload is
// missing, the overload is added as a static extension member. For earlier targets all
// the overloads live on the recreated type in BitOperations.cs.
#if NETCOREAPP3_0_OR_GREATER && !NET7_0_OR_GREATER

namespace Polyfills;

using System.Numerics;

static partial class Polyfill
{
    extension(BitOperations)
    {
#if !NET6_0_OR_GREATER

        /// <summary>
        /// Evaluates whether the specified <see cref="int"/> value is a power of two.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.numerics.bitoperations.ispow2?view=net-11.0#system-numerics-bitoperations-ispow2(system-int32)
        public static bool IsPow2(int value) =>
            (value & (value - 1)) == 0 &&
            value > 0;

        /// <summary>
        /// Evaluates whether the specified <see cref="uint"/> value is a power of two.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.numerics.bitoperations.ispow2?view=net-11.0#system-numerics-bitoperations-ispow2(system-uint32)
        public static bool IsPow2(uint value) =>
            (value & (value - 1)) == 0 &&
            value != 0;

        /// <summary>
        /// Evaluates whether the specified <see cref="long"/> value is a power of two.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.numerics.bitoperations.ispow2?view=net-11.0#system-numerics-bitoperations-ispow2(system-int64)
        public static bool IsPow2(long value) =>
            (value & (value - 1)) == 0 &&
            value > 0;

        /// <summary>
        /// Evaluates whether the specified <see cref="ulong"/> value is a power of two.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.numerics.bitoperations.ispow2?view=net-11.0#system-numerics-bitoperations-ispow2(system-uint64)
        public static bool IsPow2(ulong value) =>
            (value & (value - 1)) == 0 &&
            value != 0;

#endif

        /// <summary>
        /// Determines whether the specified <see cref="nint"/> value is a power of 2.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.numerics.bitoperations.ispow2?view=net-11.0#system-numerics-bitoperations-ispow2(system-intptr)
        public static bool IsPow2(nint value) =>
            (value & (value - 1)) == 0 &&
            value > 0;

        /// <summary>
        /// Determines whether the specified <see cref="nuint"/> value is a power of 2.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.numerics.bitoperations.ispow2?view=net-11.0#system-numerics-bitoperations-ispow2(system-uintptr)
        public static bool IsPow2(nuint value) =>
            (value & (value - 1)) == 0 &&
            value != 0;
    }
}

#endif
