namespace Polyfills;

using System;
using System.Collections.Generic;
using System.Linq;

static partial class Polyfill
{
    /// <summary>
    /// Produces a set items excluding <paramref name="item"/> by using the default equality comparer to compare values.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.except?view=net-11.0#system-linq-enumerable-except-1(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-0)))
    public static IEnumerable<TSource> Except<TSource>(
        this IEnumerable<TSource> target,
        TSource item) =>
        Except<TSource>(target, item, null);

    //TODO: removed due to R# and Rider issue https://youtrack.jetbrains.com/issue/RSRP-496127/Intelisense-incorrectly-ignores-strong-name-mismatch
    // /// <summary>
    // /// Produces the set difference of two sequences by using the default equality comparer to compare values.
    // /// </summary>
    // //Link: https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.except?view=net-11.0#system-linq-enumerable-except-1(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-0)))
    // public static IEnumerable<TSource> Except<TSource>(
    //     this IEnumerable<TSource> target,
    //     params TSource[] items) =>
    //     target.Except((IEnumerable<TSource>)items);

    /// <summary>
    /// Produces a set items excluding <paramref name="item"/> by using <paramref name="comparer"/> to compare values.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.except?view=net-11.0#system-linq-enumerable-except-1(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-0))-system-collections-generic-iequalitycomparer((-0)))
    public static IEnumerable<TSource> Except<TSource>(
        this IEnumerable<TSource> target,
        TSource item,
        IEqualityComparer<TSource>? comparer)
    {
        var set = new HashSet<TSource>(comparer) {item};
        foreach (var element in target)
        {
            if (set.Add(element))
            {
                yield return element;
            }
        }
    }

    /// <summary>
    /// Produces the set difference of two sequences by <paramref name="comparer"/> to compare values.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.except?view=net-11.0#system-linq-enumerable-except-1(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-0))-system-collections-generic-iequalitycomparer((-0)))
    public static IEnumerable<TSource> Except<TSource>(
        this IEnumerable<TSource> target,
        IEqualityComparer<TSource>? comparer,
        params TSource[] items) =>
        target.Except((IEnumerable<TSource>)items, comparer);

#if !NET6_0_OR_GREATER

    /// <summary>
    /// Produces the set difference of two sequences according to a specified key selector function.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.exceptby?view=net-11.0#system-linq-enumerable-exceptby-2(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-1))-system-func((-0-1)))
    public static IEnumerable<TSource> ExceptBy<TSource, TKey>(this IEnumerable<TSource> first, IEnumerable<TKey> second, Func<TSource, TKey> keySelector) =>
        ExceptBy(first, second, keySelector, null);

    /// <summary>
    /// Produces the set difference of two sequences according to a specified key selector function.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.exceptby?view=net-11.0#system-linq-enumerable-exceptby-2(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-1))-system-func((-0-1))-system-collections-generic-iequalitycomparer((-1)))
    public static IEnumerable<TSource> ExceptBy<TSource, TKey>(this IEnumerable<TSource> first, IEnumerable<TKey> second, Func<TSource, TKey> keySelector, IEqualityComparer<TKey>? comparer) =>
        ExceptByIterator(first, second, keySelector, comparer);

    static IEnumerable<TSource> ExceptByIterator<TSource, TKey>(IEnumerable<TSource> first, IEnumerable<TKey> second, Func<TSource, TKey> keySelector, IEqualityComparer<TKey>? comparer)
    {
        var set = new HashSet<TKey>(second, comparer);

        foreach (var element in first)
        {
            if (set.Add(keySelector(element)))
            {
                yield return element;
            }
        }
    }
#endif

}
