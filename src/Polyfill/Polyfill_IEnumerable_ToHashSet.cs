#if NET471 || NET46X || NETSTANDARD2_0

namespace Polyfills;

using System.Collections.Generic;

static partial class Polyfill
{
    /// <summary>
    /// Creates a <see cref="HashSet{T}"/> from an <see cref="IEnumerable{T}"/> using the comparer to compare keys.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.tohashset?view=net-11.0#system-linq-enumerable-tohashset-1(system-collections-generic-ienumerable((-0))-system-collections-generic-iequalitycomparer((-0)))
    public static HashSet<TSource> ToHashSet<TSource>(
        this IEnumerable<TSource> target,
        IEqualityComparer<TSource>? comparer = null) =>
        new(target, comparer);

}
#endif
