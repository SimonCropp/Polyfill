#if FeatureMemory

namespace Polyfills;

using System;
using System.Buffers;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

static partial class Polyfill
{
#if NET8_0_OR_GREATER && !NET10_0_OR_GREATER

    /// <summary>
    /// Indicates whether a specified value occurs within a read-only span.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.contains?view=net-11.0#system-memoryextensions-contains-1(system-readonlyspan((-0))-0-system-collections-generic-iequalitycomparer((-0)))
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Contains<T>(this ReadOnlySpan<T> span, T value, IEqualityComparer<T>? comparer = null) =>
        span.IndexOf(value, comparer) >= 0;

#endif

#if !NETCOREAPP3_0_OR_GREATER

    /// <summary>
    /// Indicates whether a specified value is found in a read-only span. Values are compared using IEquatable{T}.Equals(T).
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.contains?view=net-11.0#system-memoryextensions-contains-1(system-readonlyspan((-0))-0)
    public static bool Contains<T>(this ReadOnlySpan<T> target, T value)
        where T : IEquatable<T>
    {
        for (var index = 0; index < target.Length; index++)
        {
            if (target[index].Equals(value))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Indicates whether a specified value is found in a only span. Values are compared using IEquatable{T}.Equals(T).
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.contains?view=net-11.0#system-memoryextensions-contains-1(system-span((-0))-0)
    public static bool Contains<T>(this Span<T> target, T value)
        where T : IEquatable<T>
    {
        for (var index = 0; index < target.Length; index++)
        {
            if (target[index].Equals(value))
            {
                return true;
            }
        }

        return false;
    }

#endif
}

#endif
