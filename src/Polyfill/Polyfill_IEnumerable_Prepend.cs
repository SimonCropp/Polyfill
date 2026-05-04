#if NET46X || NET47

namespace Polyfills;

using System.Collections.Generic;

static partial class Polyfill
{
    /// <summary>
    /// Adds a value to the beginning of the sequence.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.prepend?view=net-11.0
    public static IEnumerable<TSource> Prepend<TSource>(
        this IEnumerable<TSource> target,
        TSource element)
    {
        yield return element;

        foreach (var item in target)
        {
            yield return item;
        }
    }
}
#endif
