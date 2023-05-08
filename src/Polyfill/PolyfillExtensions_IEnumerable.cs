
#pragma warning disable

// ReSharper disable RedundantUsingDirective
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable UnusedMember.Global

using System;
using System.Collections.Generic;
using Link = System.ComponentModel.DescriptionAttribute;
using System.Linq;
// ReSharper disable RedundantAttributeSuffix

static partial class PolyfillExtensions
{
#if NETSTANDARD || NETCOREAPP || NETFRAMEWORK || NET5_0


    /// <summary>
    /// Returns the maximum value in a generic sequence according to a specified key selector function.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
    /// <typeparam name="TKey">The type of key to compare elements by.</typeparam>
    /// <param name="source">A sequence of values to determine the maximum value of.</param>
    /// <param name="keySelector">A function to extract the key for each element.</param>
    /// <returns>The value with the maximum key in the sequence.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="source" /> is <see langword="null" />.</exception>
    /// <exception cref="ArgumentException">No key extracted from <paramref name="source" /> implements the <see cref="IComparable" /> or <see cref="System.IComparable{TKey}" /> interface.</exception>
    /// <remarks>
    /// <para>If <typeparamref name="TKey" /> is a reference type and the source sequence is empty or contains only values that are <see langword="null" />, this method returns <see langword="null" />.</para>
    /// </remarks>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.maxby#system-linq-enumerable-maxby-2(system-collections-generic-ienumerable((-0))-system-func((-0-1)))")]
    public static TSource? MaxBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector) =>
        MaxBy(source, keySelector, null);

    /// <summary>Returns the maximum value in a generic sequence according to a specified key selector function.</summary>
    /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
    /// <typeparam name="TKey">The type of key to compare elements by.</typeparam>
    /// <param name="source">A sequence of values to determine the maximum value of.</param>
    /// <param name="keySelector">A function to extract the key for each element.</param>
    /// <param name="comparer">The <see cref="IComparer{TKey}" /> to compare keys.</param>
    /// <returns>The value with the maximum key in the sequence.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="source" /> is <see langword="null" />.</exception>
    /// <exception cref="ArgumentException">No key extracted from <paramref name="source" /> implements the <see cref="IComparable" /> or <see cref="IComparable{TKey}" /> interface.</exception>
    /// <remarks>
    /// <para>If <typeparamref name="TKey" /> is a reference type and the source sequence is empty or contains only values that are <see langword="null" />, this method returns <see langword="null" />.</para>
    /// </remarks>
    public static TSource? MaxBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey>? comparer) =>
        source.OrderBy(keySelector, comparer).FirstOrDefault();

#endif

#if NET46X || NET47

    /// <summary>
    /// Appends a value to the end of the sequence.
    /// </summary>
    /// <param name="source">A sequence of values.</param>
    /// <param name="element">The value to append to source.</param>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <returns>A new sequence that ends with element.</returns>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.append")]
    public static IEnumerable<TSource> Append<TSource>(
        this IEnumerable<TSource> source,
        TSource element)
    {
        foreach (var item in source)
        {
            yield return item;
        }

        yield return element;
    }
#endif

#if NETFRAMEWORK || NETSTANDARD2_0
    /// <summary>
    /// Returns a new enumerable collection that contains the elements from source with the last count elements of the
    /// source collection omitted.
    /// </summary>
    /// <param name="source">An enumerable collection instance.</param>
    /// <param name="count">The number of elements to omit from the end of the collection.</param>
    /// <typeparam name="TSource">The type of the elements in the enumerable collection.</typeparam>
    /// <returns>A new enumerable collection that contains the elements from source minus count elements from the end
    /// of the collection.</returns>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.skiplast")]
    public static IEnumerable<TSource> SkipLast<TSource>(this IEnumerable<TSource> source, int count) =>
        source.Reverse().Skip(count).Reverse();
#endif
}