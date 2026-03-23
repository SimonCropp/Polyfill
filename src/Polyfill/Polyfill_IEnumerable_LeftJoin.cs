#if !NET10_0_OR_GREATER

namespace Polyfills;

using System;
using System.Collections.Generic;
using System.Linq;

static partial class Polyfill
{
    /// <summary>
    /// Correlates the elements of two sequences based on matching keys.
    /// The default equality comparer is used to compare keys.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.leftjoin?view=net-11.0#system-linq-enumerable-leftjoin-4(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-1))-system-func((-0-2))-system-func((-1-2))-system-func((-0-1-3)))
    public static IEnumerable<TResult> LeftJoin<TOuter, TInner, TKey, TResult>(
        this IEnumerable<TOuter> outer,
        IEnumerable<TInner> inner,
        Func<TOuter, TKey> outerKeySelector,
        Func<TInner, TKey> innerKeySelector,
        Func<TOuter, TInner?, TResult> resultSelector) =>
        LeftJoin(outer, inner, outerKeySelector, innerKeySelector, resultSelector, comparer: null);

    /// <summary>
    /// Correlates the elements of two sequences based on matching keys.
    /// A specified <see cref="IEqualityComparer{T}" /> is used to compare keys.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.leftjoin?view=net-11.0#system-linq-enumerable-leftjoin-4(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-1))-system-func((-0-2))-system-func((-1-2))-system-func((-0-1-3))-system-collections-generic-iequalitycomparer((-2)))
    public static IEnumerable<TResult> LeftJoin<TOuter, TInner, TKey, TResult>(
        this IEnumerable<TOuter> outer,
        IEnumerable<TInner> inner,
        Func<TOuter, TKey> outerKeySelector,
        Func<TInner, TKey> innerKeySelector,
        Func<TOuter, TInner?, TResult> resultSelector,
        IEqualityComparer<TKey>? comparer) =>
        LeftJoinIterator(outer, inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

    static IEnumerable<TResult> LeftJoinIterator<TOuter, TInner, TKey, TResult>(
        IEnumerable<TOuter> outer,
        IEnumerable<TInner> inner,
        Func<TOuter, TKey> outerKeySelector,
        Func<TInner, TKey> innerKeySelector,
        Func<TOuter, TInner?, TResult> resultSelector,
        IEqualityComparer<TKey>? comparer)
    {
        var innerLookup = inner.ToLookup(innerKeySelector, comparer);

        foreach (var outerElement in outer)
        {
            var key = outerKeySelector(outerElement);
            var innerGroup = innerLookup[key];
            var hasMatch = false;

            foreach (var innerElement in innerGroup)
            {
                hasMatch = true;
                yield return resultSelector(outerElement, innerElement);
            }

            if (!hasMatch)
            {
                yield return resultSelector(outerElement, default);
            }
        }
    }
}

#endif
