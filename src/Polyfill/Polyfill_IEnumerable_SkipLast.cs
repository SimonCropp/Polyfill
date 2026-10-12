#if (NETFRAMEWORK || NETSTANDARD2_0) && !WINDOWS_UWP

namespace Polyfills;

using System;
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
        int count)
    {
        if (count <= 0)
        {
            return target.Skip(0);
        }

        if (target is null)
        {
            throw new ArgumentNullException("source");
        }

        // Streams: holds back at most count items, and stops early when the count of the source is known.
        return TakeRangeFromEndIterator(
            target,
            isStartIndexFromEnd: false, startIndex: 0,
            isEndIndexFromEnd: true, endIndex: count);
    }
}
#endif
