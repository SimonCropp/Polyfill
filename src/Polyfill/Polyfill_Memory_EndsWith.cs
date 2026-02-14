#if FeatureMemory

namespace Polyfills;

using System;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;

static partial class Polyfill
{

#if !NET9_0_OR_GREATER

    /// <summary>
    /// Determines whether the specified value appears at the end of the span.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.endswith?view=net-11.0#system-memoryextensions-endswith-1(system-readonlyspan((-0))-0)
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool EndsWith<T>(this ReadOnlySpan<T> target, T value)
        where T : IEquatable<T>?
    {
        if (target.Length == 0)
        {
            return false;
        }

        var last = target[target.Length - 1];
        return last?.Equals(value) ?? (object?) value is null;
    }

#endif

#if !NETCOREAPP3_0_OR_GREATER

    /// <summary>
    /// Determines whether the end of the span matches the specified value when compared using the specified <paramref name="comparison"/> option.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.endswith?view=net-11.0#system-memoryextensions-endswith-1(system-readonlyspan((-0))-system-readonlyspan((-0)))
    public static bool EndsWith(
        this ReadOnlySpan<char> target,
        string other,
        StringComparison comparison = StringComparison.CurrentCulture) =>
        target.EndsWith(other.AsSpan(), comparison);

    /// <summary>
    /// Determines whether the specified sequence appears at the end of a span.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.endswith?view=net-11.0#system-memoryextensions-endswith-1(system-span((-0))-system-readonlyspan((-0)))
    public static bool EndsWith(
        this Span<char> target,
        string other) =>
        target.EndsWith(other.AsSpan());

#endif

}

#endif
