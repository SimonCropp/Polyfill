#if !NET11_0_OR_GREATER

namespace Polyfills;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

static partial class Polyfill
{
    /// <summary>
    /// Correlates the elements of two sequences based on key equality and groups the results.
    /// Each result is an <see cref="IGrouping{TKey,TElement}" /> keyed by the outer element.
    /// A specified <see cref="IEqualityComparer{T}" /> is used to compare keys.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.groupjoin?view=net-11.0#system-linq-enumerable-groupjoin-3(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-1))-system-func((-0-2))-system-func((-1-2))-system-collections-generic-iequalitycomparer((-2)))
    public static IEnumerable<IGrouping<TOuter, TInner>> GroupJoin<TOuter, TInner, TKey>(
        this IEnumerable<TOuter> outer,
        IEnumerable<TInner> inner,
        Func<TOuter, TKey> outerKeySelector,
        Func<TInner, TKey> innerKeySelector,
        IEqualityComparer<TKey>? comparer = null) =>
        Enumerable.GroupJoin(
            outer,
            inner,
            outerKeySelector,
            innerKeySelector,
            (outerElement, innerElements) => (IGrouping<TOuter, TInner>)new GroupJoinGrouping<TOuter, TInner>(outerElement, innerElements),
            comparer);

    sealed class GroupJoinGrouping<TKey, TElement>(TKey key, IEnumerable<TElement> elements) :
        IGrouping<TKey, TElement>
    {
        public TKey Key => key;

        public IEnumerator<TElement> GetEnumerator() =>
            elements.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();
    }
}

#endif
