#if !NET6_0_OR_GREATER

namespace Polyfills;

using System;
using System.Collections.Generic;

static partial class Polyfill
{
    /// <summary>
    /// Produces the set intersection of two sequences according to a specified key selector function.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.intersectby?view=net-11.0#system-linq-enumerable-intersectby-2(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-1))-system-func((-0-1)))
    public static IEnumerable<TSource> IntersectBy<TSource, TKey>(this IEnumerable<TSource> first, IEnumerable<TKey> second, Func<TSource, TKey> keySelector) =>
        IntersectBy(first, second, keySelector, null);

    /// <summary>
    /// Produces the set intersection of two sequences according to a specified key selector function.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.intersectby?view=net-11.0#system-linq-enumerable-intersectby-2(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-1))-system-func((-0-1))-system-collections-generic-iequalitycomparer((-1)))
    public static IEnumerable<TSource> IntersectBy<TSource, TKey>(this IEnumerable<TSource> first, IEnumerable<TKey> second, Func<TSource, TKey> keySelector, IEqualityComparer<TKey>? comparer) =>
        IntersectByIterator(first, second, keySelector, comparer);

    static IEnumerable<TSource> IntersectByIterator<TSource, TKey>(IEnumerable<TSource> first, IEnumerable<TKey> second, Func<TSource, TKey> keySelector, IEqualityComparer<TKey>? comparer)
    {
        var set = new HashSet<TKey>(second, comparer);

        foreach (var element in first)
        {
            if (set.Remove(keySelector(element)))
            {
                yield return element;
            }
        }
    }
}

#endif
