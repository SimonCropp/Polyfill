#if !NET6_0_OR_GREATER

namespace Polyfills;

using System.Collections;
using System.Collections.Generic;

static partial class Polyfill
{
    /// <summary>
    ///   Attempts to determine the number of elements in a sequence without forcing an enumeration.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.trygetnonenumeratedcount?view=net-11.0
    public static bool TryGetNonEnumeratedCount<TSource>(this IEnumerable<TSource> target, out int count)
    {
        if (target is ICollection<TSource> genericCollection)
        {
            count = genericCollection.Count;
            return true;
        }

        if (target is ICollection collection)
        {
            count = collection.Count;
            return true;
        }

        count = 0;
        return false;
    }
}
#endif
