#if !NET9_0_OR_GREATER && FeatureValueTuple
namespace Polyfills;

using System.Collections.Generic;

static partial class Polyfill
{
    /// <summary>
    /// https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-9/overview#linq
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.index?view=net-11.0#system-linq-enumerable-index-1(system-collections-generic-ienumerable((-0)))
    public static IEnumerable<(int Index, TSource Item)> Index<TSource>(this IEnumerable<TSource> source)
    {
        var index = 0;
        foreach (var item in source)
        {
            yield return (index, item);
            index++;
        }
    }
}

#endif
