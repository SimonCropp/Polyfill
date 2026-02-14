#if !NET6_0_OR_GREATER

namespace Polyfills;

using System.Collections.Generic;

static partial class Polyfill
{
    /// <summary>Returns the minimum value in a generic sequence.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.min?view=net-11.0#system-linq-enumerable-min-1(system-collections-generic-ienumerable((-0))-system-collections-generic-icomparer((-0)))
    public static TSource? Min<TSource>(
        this IEnumerable<TSource> source,
        IComparer<TSource>? comparer) =>
        source
            .MinBy(_ => _, comparer);
}
#endif
