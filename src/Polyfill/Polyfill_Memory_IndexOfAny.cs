#if FeatureMemory && NET8_0_OR_GREATER && !NET10_0_OR_GREATER

namespace Polyfills;

using System;
using System.Collections.Generic;

static partial class Polyfill
{
    /// <summary>
    /// Searches for the first index of any of the specified values similar to calling IndexOf several times with the logical OR operator. If not found, returns -1.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.countany?view=net-11.0#system-memoryextensions-countany-1(system-readonlyspan((-0))-system-buffers-searchvalues((-0)))
    public static int IndexOfAny<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> values, IEqualityComparer<T>? comparer = default)
    {
        comparer ??= EqualityComparer<T>.Default;

        for (var i = 0; i < span.Length; i++)
        {
            if (values.Contains(span[i], comparer))
            {
                return i;
            }
        }

        return -1;
    }
}
#endif
