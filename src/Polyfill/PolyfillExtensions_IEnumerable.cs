
#pragma warning disable

// ReSharper disable RedundantUsingDirective
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable UnusedMember.Global

using System;
using System.Collections.Generic;
using DescriptionAttribute = System.ComponentModel.DescriptionAttribute;
using System.Linq;
// ReSharper disable RedundantAttributeSuffix

static partial class PolyfillExtensions
{
#if NET46X || NET47

    /// <summary>
    /// Appends a value to the end of the sequence.
    /// </summary>
    /// <param name="source">A sequence of values.</param>
    /// <param name="element">The value to append to source.</param>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <returns>A new sequence that ends with element.</returns>
    [DescriptionAttribute("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.append")]
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
    [DescriptionAttribute("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.skiplast")]
    public static IEnumerable<TSource> SkipLast<TSource>(this IEnumerable<TSource> source, int count) =>
        source.Reverse().Skip(count).Reverse();
#endif
}