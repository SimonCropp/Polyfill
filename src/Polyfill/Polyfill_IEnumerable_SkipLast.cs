#if (NETFRAMEWORK || NETSTANDARD2_0) && !WINDOWS_UWP

namespace Polyfills;

using System.Collections.Generic;
using System.Linq;

static partial class Polyfill
{
    /// <summary>
    /// Returns a new enumerable collection that contains the elements from source with the last count elements of the
    /// source collection omitted.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.skiplast?view=net-11.0
    public static IEnumerable<TSource> SkipLast<TSource>(
        this IEnumerable<TSource> target,
        int count) =>
        target.Reverse().Skip(count).Reverse();
}
#endif
