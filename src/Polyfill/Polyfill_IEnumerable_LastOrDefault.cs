#if !NET6_0_OR_GREATER

namespace Polyfills;

using System;
using System.Collections.Generic;

static partial class Polyfill
{
    /// <summary>Returns the last element of a sequence, or a default value if the sequence contains no elements.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.lastordefault?view=net-11.0#system-linq-enumerable-lastordefault-1(system-collections-generic-ienumerable((-0))-0)
    public static TSource LastOrDefault<TSource>(this IEnumerable<TSource> source, TSource defaultValue)
    {
        var last = source.TryGetLast(out var found);
        return found ? last! : defaultValue;
    }

    /// <summary>Returns the last element of a sequence that satisfies a condition or a default value if no such element is found.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.lastordefault?view=net-11.0#system-linq-enumerable-lastordefault-1(system-collections-generic-ienumerable((-0))-system-func((-0-system-boolean))-0)
    public static TSource LastOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate, TSource defaultValue)
    {
        var last = source.TryGetLast(predicate, out var found);
        return found ? last! : defaultValue;
    }

    static TSource? TryGetLast<TSource>(this IEnumerable<TSource> source, out bool found) =>
        TryGetLastNonIterator(source, out found);

    static TSource? TryGetLastNonIterator<TSource>(IEnumerable<TSource> source, out bool found)
    {
        if (source is IList<TSource> list)
        {
            var count = list.Count;
            if (count > 0)
            {
                found = true;
                return list[count - 1];
            }
        }
        else
        {
            using var e = source.GetEnumerator();
            if (e.MoveNext())
            {
                TSource result;
                do
                {
                    result = e.Current;
                }
                while (e.MoveNext());

                found = true;
                return result;
            }
        }

        found = false;
        return default;
    }

    static TSource? TryGetLast<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate, out bool found)
    {
        if (source is IList<TSource> list)
        {
            for (var i = list.Count - 1; i >= 0; --i)
            {
                var result = list[i];
                if (predicate(result))
                {
                    found = true;
                    return result;
                }
            }
        }
        else
        {
            using var e = source.GetEnumerator();
            while (e.MoveNext())
            {
                var result = e.Current;
                if (predicate(result))
                {
                    while (e.MoveNext())
                    {
                        var element = e.Current;
                        if (predicate(element))
                        {
                            result = element;
                        }
                    }

                    found = true;
                    return result;
                }
            }
        }

        found = false;
        return default;
    }
}
#endif
