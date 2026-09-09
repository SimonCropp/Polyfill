#pragma warning disable

#if !NETCOREAPP3_0_OR_GREATER

namespace System.Numerics;

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Utility methods for intrinsic bit-twiddling operations.
/// </summary>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
#if PolyPublic
public
#endif
static class BitOperations
{
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

#else
[assembly: System.Runtime.CompilerServices.TypeForwardedTo(typeof(System.Numerics.BitOperations))]
#endif
