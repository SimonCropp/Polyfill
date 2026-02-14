// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


#if (NETSTANDARD2_0 || NETFRAMEWORK) && !WINDOWS_UWP

namespace Polyfills;

using System.Collections.Generic;

static partial class Polyfill
{
    /// <summary>
    /// Returns a new enumerable collection that contains the last count elements from source.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.takelast?view=net-11.0
    public static IEnumerable<TSource> TakeLast<TSource>(this IEnumerable<TSource> source, int count)
    {
        if (count <= 0 || IsEmptyArray(source))
        {
            return [];
        }

        return TakeRangeFromEndIterator(
            source,
            isStartIndexFromEnd: true, startIndex: count,
            isEndIndexFromEnd: true, endIndex: 0);
    }
}
#endif
