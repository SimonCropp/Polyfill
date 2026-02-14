#if FeatureMemory

namespace Polyfills;

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System;
using System.Text;

static partial class Polyfill
{
#if !NET9_0_OR_GREATER

    /// <summary>
    /// Determines whether the specified value appears at the start of the span.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.endswith?view=net-11.0#system-memoryextensions-endswith-1(system-readonlyspan((-0))-0)
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool StartsWith<T>(this ReadOnlySpan<T> target, T value)
        where T : IEquatable<T>? =>
        target.Length != 0 && (target[0]?.Equals(value) ?? (object?)value is null);

#endif

#if !NETCOREAPP3_0_OR_GREATER

    /// <summary>
    /// Determines whether a read-only character span begins with a specified value when compared using a specified <see cref="StringComparison"/> value.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.startswith?view=net-11.0#system-memoryextensions-startswith-1(system-readonlyspan((-0))-system-readonlyspan((-0)))
    public static bool StartsWith(
        this ReadOnlySpan<char> target,
        string other,
        StringComparison comparison = StringComparison.CurrentCulture) =>
        target.StartsWith(other.AsSpan(), comparison);

    /// <summary>
    /// Determines whether a specified sequence appears at the start of a span.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.startswith?view=net-11.0#system-memoryextensions-startswith-1(system-span((-0))-system-readonlyspan((-0)))
    public static bool StartsWith(
        this Span<char> target,
        string other) =>
        target.StartsWith(other.AsSpan());

#endif

}

#endif
