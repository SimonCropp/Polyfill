#if FeatureMemory && !NET11_0_OR_GREATER

namespace Polyfills;

using System;
using System.Collections.Generic;

static partial class Polyfill
{
    /// <summary>
    /// Returns the minimum value in the span.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.min?view=net-11.0#system-memoryextensions-min-1(system-readonlyspan((-0)))
    public static T? Min<T>(this ReadOnlySpan<T> span) =>
        MinMax(span, Comparer<T>.Default, true);

    /// <summary>
    /// Returns the minimum value in the span.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.min?view=net-11.0#system-memoryextensions-min-1(system-readonlyspan((-0))-system-collections-generic-icomparer((-0)))
    public static T? Min<T>(this ReadOnlySpan<T> span, IComparer<T>? comparer) =>
        MinMax(span, comparer ?? Comparer<T>.Default, true);

    /// <summary>
    /// Returns the maximum value in the span.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.max?view=net-11.0#system-memoryextensions-max-1(system-readonlyspan((-0)))
    public static T? Max<T>(this ReadOnlySpan<T> span) =>
        MinMax(span, Comparer<T>.Default, false);

    /// <summary>
    /// Returns the maximum value in the span.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.max?view=net-11.0#system-memoryextensions-max-1(system-readonlyspan((-0))-system-collections-generic-icomparer((-0)))
    public static T? Max<T>(this ReadOnlySpan<T> span, IComparer<T>? comparer) =>
        MinMax(span, comparer ?? Comparer<T>.Default, false);

    // Simplified from https://github.com/dotnet/runtime/blob/main/src/libraries/System.Private.CoreLib/src/System/MemoryExtensions.MinMax.cs
    static T? MinMax<T>(ReadOnlySpan<T> span, IComparer<T> comparer, bool min)
    {
        T? value = default;

        if (value is null)
        {
            // T is a reference type or a nullable value type, so nulls are skipped
            // and an empty span yields null
            var index = 0;
            for (; index < span.Length; index++)
            {
                value = span[index];

                if (value is not null)
                {
                    index++;
                    break;
                }
            }

            for (; index < span.Length; index++)
            {
                var next = span[index];
                if (next is not null &&
                    IsBetter(comparer.Compare(next, value!), min))
                {
                    value = next;
                }
            }

            return value;
        }

        if (span.IsEmpty)
        {
            throw new InvalidOperationException("Sequence contains no elements.");
        }

        value = span[0];
        for (var index = 1; index < span.Length; index++)
        {
            var next = span[index];
            if (IsBetter(comparer.Compare(next, value!), min))
            {
                value = next;
            }
        }

        return value;
    }

    static bool IsBetter(int comparison, bool min) =>
        min ? comparison < 0 : comparison > 0;
}

#endif
