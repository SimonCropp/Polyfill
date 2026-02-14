#if NET46X || NET47

namespace Polyfills;

using System.Collections.Generic;

static partial class Polyfill
{
    /// <summary>
    /// Appends a value to the end of the sequence.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.append?view=net-11.0
    public static IEnumerable<TSource> Append<TSource>(
        this IEnumerable<TSource> target,
        TSource element)
    {
        foreach (var item in target)
        {
            yield return item;
        }

        yield return element;
    }
}
#endif
