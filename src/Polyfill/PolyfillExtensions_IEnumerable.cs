
#pragma warning disable

// ReSharper disable RedundantUsingDirective
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable UnusedMember.Global

using System;
using System.Collections.Generic;
using System.Linq;

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
      public static IEnumerable<T> SkipLast<T>(this IEnumerable<T> source, int count) =>
                source.Reverse().Skip(count).Reverse();
#endif
}