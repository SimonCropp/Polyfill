#if !NET8_0_OR_GREATER

namespace Polyfills;

using System.Collections.Generic;
using System.Linq;

static partial class Polyfill
{
    /// <summary>
    /// Creates a <see cref="Dictionary{TKey, TValue}"/> from an <see cref="IEnumerable{T}"/> of <see cref="KeyValuePair{TKey, TValue}"/>.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.todictionary?view=net-11.0#system-linq-enumerable-todictionary-2(system-collections-generic-ienumerable(system-collections-generic-keyvaluepair(-0-1))-system-collections-generic-iequalitycomparer(-0))
    public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(
        this IEnumerable<KeyValuePair<TKey, TValue>> source,
        IEqualityComparer<TKey>? comparer = null)
        where TKey : notnull
    {
        // Size the dictionary once when the count is known, instead of growing it while adding
        var capacity = 0;
        if (source is ICollection<KeyValuePair<TKey, TValue>> collection)
        {
            capacity = collection.Count;
        }
        else if (source is IReadOnlyCollection<KeyValuePair<TKey, TValue>> readOnlyCollection)
        {
            capacity = readOnlyCollection.Count;
        }

        var result = new Dictionary<TKey, TValue>(capacity, comparer);
        foreach (var pair in source)
        {
            result.Add(pair.Key, pair.Value);
        }
        return result;
    }
}

#endif
