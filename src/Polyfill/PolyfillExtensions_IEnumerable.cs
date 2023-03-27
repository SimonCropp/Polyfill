#if NET46X || NET47 

#pragma warning disable

// ReSharper disable RedundantUsingDirective
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable UnusedMember.Global

using System;
using System.Collections.Generic;

static partial class PolyfillExtensions
{
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
}
#endif