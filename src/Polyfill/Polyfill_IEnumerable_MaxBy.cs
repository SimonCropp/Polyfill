#if !NET6_0_OR_GREATER

namespace Polyfills;

using System;
using System.Collections.Generic;

static partial class Polyfill
{
    /// <summary>
    /// Returns the maximum value in a generic sequence according to a specified key selector function.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.maxby?view=net-11.0#system-linq-enumerable-maxby-2(system-collections-generic-ienumerable((-0))-system-func((-0-1)))
    public static TSource? MaxBy<TSource, TKey>(
        this IEnumerable<TSource> source,
        Func<TSource, TKey> keySelector) =>
        MaxBy(source, keySelector, null);

    /// <summary>Returns the maximum value in a generic sequence according to a specified key selector function.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.maxby?view=net-11.0#system-linq-enumerable-maxby-2(system-collections-generic-ienumerable((-0))-system-func((-0-1))-system-collections-generic-icomparer((-1)))
    public static TSource? MaxBy<TSource, TKey>(
        this IEnumerable<TSource> source,
        Func<TSource, TKey> keySelector,
        IComparer<TKey>? comparer)
    {
        // Simplified from https://github.com/dotnet/runtime/blob/5d09a8f94c72ca4ef0a9c79eb9c58d06198e3ba9/src/libraries/System.Linq/src/System/Linq/Max.cs#L445-L526
        comparer ??= Comparer<TKey>.Default;

        using var e = source.GetEnumerator();

        if (!e.MoveNext())
        {
            if (default(TSource) is null)
            {
                return default;
            }

            ThrowNoElementsException();
        }

        var value = e.Current;
        var key = keySelector(value);

        if (default(TKey) is null)
        {
            if (key is null)
            {
                var firstValue = value;

                do
                {
                    if (!e.MoveNext())
                    {
                        return firstValue;
                    }

                    value = e.Current;
                    key = keySelector(value);
                }
                while (key is null);
            }

            while (e.MoveNext())
            {
                var nextValue = e.Current;
                var nextKey = keySelector(nextValue);
                if (nextKey is not null && comparer.Compare(nextKey, key) > 0)
                {
                    key = nextKey;
                    value = nextValue;
                }
            }
        }
        else
        {
            while (e.MoveNext())
            {
                var nextValue = e.Current;
                var nextKey = keySelector(nextValue);
                if (comparer.Compare(nextKey, key) > 0)
                {
                    key = nextKey;
                    value = nextValue;
                }
            }
        }

        return value;

    }
}
#endif
