

#pragma warning disable

namespace Polyfills;
using System.Collections;
using System.Collections.Generic;
using Link = System.ComponentModel.DescriptionAttribute;
using System.Linq;

static partial class Polyfill
{
    /// <summary>
    /// Creates a HashSet<T> from an IEnumerable<T> using the comparer to compare keys.
    /// </summary>
    /// <param name="source">An IEnumerable<T> to create a HashSet<T> from.</param>
    /// <param name="comparer">An IEqualityComparer<T> to compare keys.</param>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <returns>A HashSet<T> that contains values of type TSource selected from the input sequence.</returns>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.tohashset#system-linq-enumerable-tohashset-1(system-collections-generic-ienumerable((-0))-system-collections-generic-iequalitycomparer((-0)))")]
    public static HashSet<TSource> ToHashSet<TSource>(
        this IEnumerable<TSource> target,
        IEqualityComparer<TSource>? comparer = null) =>
        new HashSet<TSource>(target, comparer);

}