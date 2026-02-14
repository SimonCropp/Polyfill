#if FeatureMemory

namespace Polyfills;

using System;
using System.Runtime.CompilerServices;
using System.Buffers;
using System.Collections.Generic;

static partial class Polyfill
{
#if NET8_0_OR_GREATER && !NET10_0_OR_GREATER

    /// <summary>
    /// Searches for the specified value and returns the index of its first occurrence. If not found, returns -1. Values are compared using IEquatable{T}.Equals(T).
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.indexof?view=net-11.0#system-memoryextensions-indexof-1(system-readonlyspan((-0))-0-system-collections-generic-iequalitycomparer((-0)))
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int IndexOf<T>(this ReadOnlySpan<T> span, T value, IEqualityComparer<T>? comparer = null)
    {
        comparer ??= EqualityComparer<T>.Default;
        for (var i = 0; i < span.Length; i++)
        {
            if (comparer.Equals(span[i], value))
            {
                return i;
            }
        }

        return -1;
    }

#endif
}
#endif
