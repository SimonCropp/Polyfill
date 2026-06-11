#if FeatureValueTuple && !NET11_0_OR_GREATER

namespace Polyfills;

using System;
using System.Collections.Generic;
using System.Linq;

static partial class Polyfill
{
    /// <summary>
    /// Correlates the elements of two sequences based on matching keys.
    /// A specified <see cref="IEqualityComparer{T}" /> is used to compare keys.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.join?view=net-11.0#system-linq-enumerable-join-3(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-1))-system-func((-0-2))-system-func((-1-2))-system-collections-generic-iequalitycomparer((-2)))
    public static IEnumerable<(TOuter Outer, TInner Inner)> Join<TOuter, TInner, TKey>(
        this IEnumerable<TOuter> outer,
        IEnumerable<TInner> inner,
        Func<TOuter, TKey> outerKeySelector,
        Func<TInner, TKey> innerKeySelector,
        IEqualityComparer<TKey>? comparer = null) =>
        Enumerable.Join(outer, inner, outerKeySelector, innerKeySelector, (outerElement, innerElement) => (outerElement, innerElement), comparer);
}

#endif
