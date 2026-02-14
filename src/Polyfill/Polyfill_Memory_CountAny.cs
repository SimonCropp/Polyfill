#if FeatureMemory && NET8_0_OR_GREATER && !NET10_0_OR_GREATER
namespace Polyfills;

using System;
using System.Buffers;
using System.Collections.Generic;

static partial class Polyfill
{
    /// <summary>Counts the number of times any of the specified <paramref name="values"/> occurs in the <paramref name="span"/>.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.countany?view=net-11.0#system-memoryextensions-countany-1(system-readonlyspan((-0))-system-buffers-searchvalues((-0)))
    public static int CountAny<T>(this ReadOnlySpan<T> span, SearchValues<T> values)
        where T : IEquatable<T>?
    {
        int count = 0;

        int pos;
        while ((pos = span.IndexOfAny(values)) >= 0)
        {
            count++;
            span = span.Slice(pos + 1);
        }

        return count;
    }

    /// <summary>Counts the number of times any of the specified <paramref name="values"/> occurs in the <paramref name="span"/>.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.countany?view=net-11.0#system-memoryextensions-countany-1(system-readonlyspan((-0))-system-readonlyspan((-0)))
    public static int CountAny<T>(this ReadOnlySpan<T> span, params ReadOnlySpan<T> values) where T : IEquatable<T>?
    {
        int count = 0;

        int pos;
        while ((pos = span.IndexOfAny(values)) >= 0)
        {
            count++;
            span = span.Slice(pos + 1);
        }

        return count;
    }

    /// <summary>Counts the number of times any of the specified <paramref name="values"/> occurs in the <paramref name="span"/>.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.countany?view=net-11.0#system-memoryextensions-countany-1(system-readonlyspan((-0))-system-readonlyspan((-0))-system-collections-generic-iequalitycomparer((-0)))
    public static int CountAny<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> values, IEqualityComparer<T>? comparer = null)
    {
        var count = 0;

        int pos;
        while ((pos = span.IndexOfAny(values, comparer)) >= 0)
        {
            count++;
            span = span.Slice(pos + 1);
        }

        return count;
    }
}
#endif
