#if !NET7_0_OR_GREATER

namespace Polyfills;

using System;
using System.Collections.Generic;
using System.Linq;

static partial class Polyfill
{
    /// <summary>Sorts the elements of a sequence in ascending order.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.order?view=net-11.0#system-linq-enumerable-order-1(system-collections-generic-ienumerable((-0)))
    public static IOrderedEnumerable<T> Order<T>(this IEnumerable<T> source) =>
        source.OrderBy(static item => item);

    /// <summary>Sorts the elements of a sequence in ascending order.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.order?view=net-11.0#system-linq-enumerable-order-1(system-collections-generic-ienumerable((-0))-system-collections-generic-icomparer((-0)))
    public static IOrderedEnumerable<T> Order<T>(this IEnumerable<T> source, IComparer<T>? comparer) =>
        source.OrderBy(static item => item, comparer);

    /// <summary>Sorts the elements of a sequence in descending order.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.orderdescending?view=net-11.0#system-linq-enumerable-orderdescending-1(system-collections-generic-ienumerable((-0)))
    public static IOrderedEnumerable<T> OrderDescending<T>(this IEnumerable<T> source) =>
        source.OrderByDescending(static item => item);

    /// <summary>Sorts the elements of a sequence in descending order.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.orderdescending?view=net-11.0#system-linq-enumerable-orderdescending-1(system-collections-generic-ienumerable((-0))-system-collections-generic-icomparer((-0)))
    public static IOrderedEnumerable<T> OrderDescending<T>(this IEnumerable<T> source, IComparer<T>? comparer) =>
        source.OrderByDescending(static item => item, comparer);
}

#endif
