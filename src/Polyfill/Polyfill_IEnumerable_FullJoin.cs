#if !NET11_0_OR_GREATER

namespace Polyfills;

using System;
using System.Collections.Generic;
using System.Linq;

static partial class Polyfill
{
    /// <summary>
    /// Correlates the elements of two sequences based on matching keys,
    /// including elements of each sequence that have no matching element in the other.
    /// A specified <see cref="IEqualityComparer{T}" /> is used to compare keys.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.fulljoin?view=net-11.0#system-linq-enumerable-fulljoin-4(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-1))-system-func((-0-2))-system-func((-1-2))-system-func((-0-1-3))-system-collections-generic-iequalitycomparer((-2)))
    public static IEnumerable<TResult> FullJoin<TOuter, TInner, TKey, TResult>(
        this IEnumerable<TOuter> outer,
        IEnumerable<TInner> inner,
        Func<TOuter, TKey> outerKeySelector,
        Func<TInner, TKey> innerKeySelector,
        Func<TOuter?, TInner?, TResult> resultSelector,
        IEqualityComparer<TKey>? comparer = null) =>
        FullJoinIterator(outer, inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

    static IEnumerable<TResult> FullJoinIterator<TOuter, TInner, TKey, TResult>(
        IEnumerable<TOuter> outer,
        IEnumerable<TInner> inner,
        Func<TOuter, TKey> outerKeySelector,
        Func<TInner, TKey> innerKeySelector,
        Func<TOuter?, TInner?, TResult> resultSelector,
        IEqualityComparer<TKey>? comparer)
    {
        comparer ??= EqualityComparer<TKey>.Default;

        // a null key never matches, not even another null key.
        // so null keyed inner elements are kept out of the lookup, and yielded last as unmatched
        var keyedInner = new List<KeyValuePair<TKey, TInner>>();
        var nullKeyedInner = new List<TInner>();
        foreach (var innerElement in inner)
        {
            var key = innerKeySelector(innerElement);
            if (key is null)
            {
                nullKeyedInner.Add(innerElement);
                continue;
            }

            keyedInner.Add(new(key, innerElement));
        }

        var innerLookup = keyedInner.ToLookup(_ => _.Key, _ => _.Value, comparer);
        var outerKeys = new HashSet<TKey>(comparer);

        foreach (var outerElement in outer)
        {
            var key = outerKeySelector(outerElement);
            var hasMatch = false;

            if (key is not null)
            {
                outerKeys.Add(key);

                foreach (var innerElement in innerLookup[key])
                {
                    hasMatch = true;
                    yield return resultSelector(outerElement, innerElement);
                }
            }

            if (!hasMatch)
            {
                yield return resultSelector(outerElement, default);
            }
        }

        foreach (var innerGroup in innerLookup)
        {
            if (outerKeys.Contains(innerGroup.Key))
            {
                continue;
            }

            foreach (var innerElement in innerGroup)
            {
                yield return resultSelector(default, innerElement);
            }
        }

        foreach (var innerElement in nullKeyedInner)
        {
            yield return resultSelector(default, innerElement);
        }
    }

#if FeatureValueTuple

    /// <summary>
    /// Correlates the elements of two sequences based on matching keys,
    /// including elements of each sequence that have no matching element in the other.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.fulljoin?view=net-11.0#system-linq-enumerable-fulljoin-3(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-1))-system-func((-0-2))-system-func((-1-2))-system-collections-generic-iequalitycomparer((-2)))
    public static IEnumerable<(TOuter? Outer, TInner? Inner)> FullJoin<TOuter, TInner, TKey>(
        this IEnumerable<TOuter> outer,
        IEnumerable<TInner> inner,
        Func<TOuter, TKey> outerKeySelector,
        Func<TInner, TKey> innerKeySelector,
        IEqualityComparer<TKey>? comparer = null) =>
        outer.FullJoin(inner, outerKeySelector, innerKeySelector, (outerElement, innerElement) => (outerElement, innerElement), comparer);

#endif
}

#endif
