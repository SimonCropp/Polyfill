#if !NET6_0_OR_GREATER

namespace Polyfills;

using System.Collections.Generic;

static partial class Polyfill
{
    /// <summary>Returns the maximum value in a generic sequence.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.max?view=net-11.0#system-linq-enumerable-max-1(system-collections-generic-ienumerable((-0))-system-collections-generic-icomparer((-0)))
    public static TSource? Max<TSource>(
        this IEnumerable<TSource> source,
        IComparer<TSource>? comparer) =>
        source
            .MaxBy(_ => _, comparer);
}

#endif
