#if FeatureValueTuple

namespace Polyfills;

using System.Collections.Generic;

static partial class Polyfill
{
#if !NET6_0_OR_GREATER

    /// <summary>
    /// Produces a sequence of tuples with elements from the three specified sequences.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.zip?view=net-11.0#system-linq-enumerable-zip-3(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-1))-system-collections-generic-ienumerable((-2)))
    public static IEnumerable<(TFirst First, TSecond Second, TThird Third)> Zip<TFirst, TSecond, TThird>(this IEnumerable<TFirst> first, IEnumerable<TSecond> second, IEnumerable<TThird> third)
    {
        using var e1 = first.GetEnumerator();
        using var e2 = second.GetEnumerator();
        using var e3 = third.GetEnumerator();
        while (e1.MoveNext() && e2.MoveNext() && e3.MoveNext())
        {
            yield return (e1.Current, e2.Current, e3.Current);
        }
    }
#endif

#if !NETCOREAPP3_1_OR_GREATER

    /// <summary>
    /// Produces a sequence of tuples with elements from the two specified sequences.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.zip?view=net-11.0#system-linq-enumerable-zip-2(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-1)))
    public static IEnumerable<(TFirst First, TSecond Second)> Zip<TFirst, TSecond>(this IEnumerable<TFirst> first, IEnumerable<TSecond> second)
    {
        using var e1 = first.GetEnumerator();
        using var e2 = second.GetEnumerator();
        while (e1.MoveNext() && e2.MoveNext())
        {
            yield return (e1.Current, e2.Current);
        }
    }

#endif

}
#endif
