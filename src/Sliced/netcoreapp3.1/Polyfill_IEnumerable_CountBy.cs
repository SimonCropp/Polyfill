
#pragma warning disable

namespace Polyfills;
using System;
using System.Collections.Generic;
using Link = System.ComponentModel.DescriptionAttribute;

static partial class Polyfill
{

    /// <summary>
    /// https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-9/overview#linq
    /// </summary>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.countby")]
    public static IEnumerable<KeyValuePair<TKey, int>> CountBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey>? keyComparer = null)
        where TKey : notnull
    {
        if (source is TSource[] { Length: 0 })
        {
            return [];
        }

        return CountByIterator(source, keySelector, keyComparer);
    }

    static IEnumerable<KeyValuePair<TKey, int>> CountByIterator<TSource, TKey>(IEnumerable<TSource> source, Func<TSource, TKey> selector, IEqualityComparer<TKey>? comparer)
        where TKey : notnull
    {
        using var enumerator = source.GetEnumerator();

        if (!enumerator.MoveNext())
        {
            yield break;
        }

        foreach (var item in BuildCountDictionary(enumerator, selector, comparer))
        {
            yield return item;
        }
    }

    static Dictionary<TKey, int> BuildCountDictionary<TSource, TKey>(IEnumerator<TSource> enumerator, Func<TSource, TKey> selector, IEqualityComparer<TKey>? comparer)
        where TKey : notnull
    {
        Dictionary<TKey, int> countsBy = new(comparer);

        do
        {
            var value = enumerator.Current;
            var key = selector(value);

            if (!countsBy.TryGetValue(key, out var count))
            {
                count = 0;
            }

            count++;
            countsBy[key] = count;
        }
        while (enumerator.MoveNext());

        return countsBy;
    }

}