#if !NET6_0_OR_GREATER

namespace Polyfills;

using System;
using System.Collections.Generic;

static partial class Polyfill
{
    /// <summary>Produces the set union of two sequences according to a specified key selector function.</summary>
    //Link: https://learn.microsoft.com/de-de/dotnet/api/system.linq.enumerable.unionby?view=net-11.0#system-linq-enumerable-unionby-2(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-0))-system-func((-0-1)))
    public static IEnumerable<TSource> UnionBy<TSource, TKey>(
        this IEnumerable<TSource> first,
        IEnumerable<TSource> second,
        Func<TSource, TKey> keySelector
    ) => UnionBy(first, second, keySelector, null);

    /// <summary>Produces the set union of two sequences according to a specified key selector function.</summary>
    //Link: https://learn.microsoft.com/de-de/dotnet/api/system.linq.enumerable.unionby?view=net-11.0#system-linq-enumerable-unionby-2(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-0))-system-func((-0-1))-system-collections-generic-iequalitycomparer((-1)))
    public static IEnumerable<TSource> UnionBy<TSource, TKey>(
        this IEnumerable<TSource> first,
        IEnumerable<TSource> second,
        Func<TSource, TKey> keySelector,
        IEqualityComparer<TKey>? comparer) =>
        UnionByIterator(first, second, keySelector, comparer);

    static IEnumerable<TSource> UnionByIterator<TSource, TKey>(
        IEnumerable<TSource> first,
        IEnumerable<TSource> second,
        Func<TSource, TKey> keySelector,
        IEqualityComparer<TKey>? comparer)
    {
        var set = new HashSet<TKey>(comparer);

        foreach (var item in first)
        {
            if (set.Add(keySelector(item)))
                yield return item;
        }

        foreach (var item in second)
        {
            if (set.Add(keySelector(item)))
                yield return item;
        }
    }
}

#endif
