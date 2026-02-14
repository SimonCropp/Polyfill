#if !NET6_0_OR_GREATER

namespace Polyfills;

using System;
using System.Collections.Generic;

static partial class Polyfill
{

    /// <summary>Returns distinct elements from a sequence according to a specified key selector function.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.distinctby?view=net-11.0#system-linq-enumerable-distinctby-2(system-collections-generic-ienumerable((-0))-system-func((-0-1)))
    public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector) =>
        DistinctBy(source, keySelector, null);

    /// <summary>Returns distinct elements from a sequence according to a specified key selector function.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.distinctby?view=net-11.0#system-linq-enumerable-distinctby-2(system-collections-generic-ienumerable((-0))-system-func((-0-1))-system-collections-generic-iequalitycomparer((-1)))
    public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey>? comparer)
    {
        using var enumerator = source.GetEnumerator();

        if (!enumerator.MoveNext())
        {
            yield break;
        }

        var set = new HashSet<TKey>(comparer);
        do
        {
            var element = enumerator.Current;
            if (set.Add(keySelector(element)))
            {
                yield return element;
            }
        }
        while (enumerator.MoveNext());
    }
}

#endif
