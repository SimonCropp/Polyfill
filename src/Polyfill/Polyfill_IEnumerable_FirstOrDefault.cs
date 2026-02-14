namespace Polyfills;

using System;
using System.Collections.Generic;

static partial class Polyfill
{
#if !NET6_0_OR_GREATER

    /// <summary>Returns the first element of the sequence that satisfies a condition or a default value if no such element is found.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.firstordefault?view=net-11.0#system-linq-enumerable-firstordefault-1(system-collections-generic-ienumerable((-0))-system-func((-0-system-boolean))-0)
    public static TSource FirstOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate, TSource defaultValue)
    {
        var first = source.TryGetFirst(predicate, out var found);
        return found ? first! : defaultValue;
    }

    /// <summary>Returns the first element of a sequence, or a default value if the sequence contains no elements.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.firstordefault?view=net-11.0#system-linq-enumerable-firstordefault-1(system-collections-generic-ienumerable((-0))-0)
    public static TSource FirstOrDefault<TSource>(this IEnumerable<TSource> source, TSource defaultValue)
    {
        var first = source.TryGetFirst(out var found);
        return found ? first! : defaultValue;
    }

    static TSource? TryGetFirst<TSource>(this IEnumerable<TSource> source, out bool found) =>
        TryGetFirstNonIterator(source, out found);

    static TSource? TryGetFirstNonIterator<TSource>(IEnumerable<TSource> source, out bool found)
    {
        if (source is IList<TSource> list)
        {
            if (list.Count > 0)
            {
                found = true;
                return list[0];
            }
        }
        else
        {
            using var e = source.GetEnumerator();
            if (e.MoveNext())
            {
                found = true;
                return e.Current;
            }
        }

        found = false;
        return default;
    }

    static TSource? TryGetFirst<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate, out bool found)
    {
        foreach (var element in source)
        {
            if (predicate(element))
            {
                found = true;
                return element;
            }
        }

        found = false;
        return default;
    }
#endif
}
