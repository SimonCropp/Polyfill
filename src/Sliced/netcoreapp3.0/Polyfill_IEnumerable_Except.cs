
#pragma warning disable

namespace Polyfills;
using System;
using System.Collections.Generic;
using Link = System.ComponentModel.DescriptionAttribute;
using System.Linq;

static partial class Polyfill
{
    /// <summary>
    /// Produces a set items excluding <paramref name="item"/> by using the default equality comparer to compare values.
    /// </summary>
    /// <param name="target">An <see cref="IEnumerable{T}"/> whose elements that are not equal to <paramref name="item"/> will be returned.</param>
    /// <param name="item">An <see cref="TSource"/> that is elements equal it will cause those elements to be removed from the returned sequence.</param>
    /// <typeparam name="TSource">The type of the elements of <paramref name="target" />.</typeparam>
    /// <returns>A sequence that contains the items of <paramref name="target"/> but excluding <paramref name="item"/>.</returns>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.except#system-linq-enumerable-except-1(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-0)))")]
    public static IEnumerable<TSource> Except<TSource>(
        this IEnumerable<TSource> target,
        TSource item) =>
        Except<TSource>(target, item, null);

    /// <summary>
    /// Produces the set difference of two sequences by using the default equality comparer to compare values.
    /// </summary>
    /// <param name="target">An <see cref="IEnumerable{T}"/> whose elements that are not equal to <paramref name="item"/> will be returned.</param>
    /// <param name="item">An <see cref="TSource"/> that is elements equal it will cause those elements to be removed from the returned sequence.</param>
    /// <typeparam name="TSource">The type of the elements of <paramref name="target" />.</typeparam>
    /// <returns>A sequence that contains the items of <paramref name="target"/> but excluding <paramref name="item"/>.</returns>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.except#system-linq-enumerable-except-1(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-0)))")]
    public static IEnumerable<TSource> Except<TSource>(
        this IEnumerable<TSource> target,
        params TSource[] items) =>
        target.Except((IEnumerable<TSource>)items);

    /// <summary>
    /// Produces a set items excluding <paramref name="item"/> by using <paramref name="comparer"/> to compare values.
    /// </summary>
    /// <param name="target">An <see cref="IEnumerable{T}"/> whose elements that are not equal to <paramref name="item"/> will be returned.</param>
    /// <param name="item">An <see cref="TSource"/> that is elements equal it will cause those elements to be removed from the returned sequence.</param>
    /// <param name="comparer">An <see cref="IEqualityComparer{T}"/> to compare values.</param>
    /// <typeparam name="TSource">The type of the elements of <paramref name="target" />.</typeparam>
    /// <returns>A sequence that contains the items of <paramref name="target"/> but excluding <paramref name="item"/>.</returns>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.except#system-linq-enumerable-except-1(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-0))-system-collections-generic-iequalitycomparer((-0)))")]
    public static IEnumerable<TSource> Except<TSource>(
        this IEnumerable<TSource> target,
        TSource item,
        IEqualityComparer<TSource>? comparer)
    {
        var set = new HashSet<TSource>(comparer);
        set.Add(item);
        foreach (TSource element in target)
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
    /// <param name="target">An <see cref="IEnumerable{T}"/> whose elements that are not equal to <paramref name="item"/> will be returned.</param>
    /// <param name="item">An <see cref="TSource"/> that is elements equal it will cause those elements to be removed from the returned sequence.</param>
    /// <typeparam name="TSource">The type of the elements of <paramref name="target" />.</typeparam>
    /// <returns>A sequence that contains the items of <paramref name="target"/> but excluding <paramref name="item"/>.</returns>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.except#system-linq-enumerable-except-1(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-0))-system-collections-generic-iequalitycomparer((-0)))")]
    public static IEnumerable<TSource> Except<TSource>(
        this IEnumerable<TSource> target,
        IEqualityComparer<TSource> comparer,
        params TSource[] items) =>
        target.Except((IEnumerable<TSource>)items, comparer);


    /// <summary>
    /// Produces the set difference of two sequences according to a specified key selector function.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of the input sequence.</typeparam>
    /// <typeparam name="TKey">The type of key to identify elements by.</typeparam>
    /// <param name="first">An <see cref="IEnumerable{TSource}" /> whose keys that are not also in <paramref name="second"/> will be returned.</param>
    /// <param name="second">An <see cref="IEnumerable{TKey}" /> whose keys that also occur in the first sequence will cause those elements to be removed from the returned sequence.</param>
    /// <param name="keySelector">A function to extract the key for each element.</param>
    /// <returns>A sequence that contains the set difference of the elements of two sequences.</returns>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.exceptby#system-linq-enumerable-exceptby-2(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-1))-system-func((-0-1)))")]
    public static IEnumerable<TSource> ExceptBy<TSource, TKey>(this IEnumerable<TSource> first, IEnumerable<TKey> second, Func<TSource, TKey> keySelector) =>
        ExceptBy(first, second, keySelector, null);

    /// <summary>
    /// Produces the set difference of two sequences according to a specified key selector function.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of the input sequence.</typeparam>
    /// <typeparam name="TKey">The type of key to identify elements by.</typeparam>
    /// <param name="first">An <see cref="IEnumerable{TSource}" /> whose keys that are not also in <paramref name="second"/> will be returned.</param>
    /// <param name="second">An <see cref="IEnumerable{TKey}" /> whose keys that also occur in the first sequence will cause those elements to be removed from the returned sequence.</param>
    /// <param name="keySelector">A function to extract the key for each element.</param>
    /// <param name="comparer">The <see cref="IEqualityComparer{TKey}" /> to compare values.</param>
    /// <returns>A sequence that contains the set difference of the elements of two sequences.</returns>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.exceptby#system-linq-enumerable-exceptby-2(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-1))-system-func((-0-1))-system-collections-generic-iequalitycomparer((-1)))")]
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

}