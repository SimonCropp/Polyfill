#if !NET6_0_OR_GREATER

namespace Polyfills;

using System;
using System.Collections.Generic;

static partial class Polyfill
{
    /// <summary>Returns the only element of a sequence that satisfies a specified condition or a default value if no such element exists; this method throws an exception if more than one element satisfies the condition.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.singleordefault?view=net-11.0#system-linq-enumerable-singleordefault-1(system-collections-generic-ienumerable((-0))-system-func((-0-system-boolean))-0)
    public static TSource SingleOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate, TSource defaultValue)
    {
        var single = source.TryGetSingle(predicate, out var found);
        return found ? single! : defaultValue;
    }

    static TSource? TryGetSingle<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate, out bool found)
    {
        using var e = source.GetEnumerator();
        while (e.MoveNext())
        {
            var result = e.Current;
            if (predicate(result))
            {
                while (e.MoveNext())
                {
                    if (predicate(e.Current))
                    {
                        throw new InvalidOperationException("Sequence contains more than one matching element");
                    }
                }
                found = true;
                return result;
            }
        }

        found = false;
        return default;
    }

    /// <summary>Returns the only element of a sequence, or a default value if the sequence is empty; this method throws an exception if there is more than one element in the sequence.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.singleordefault?view=net-11.0#system-linq-enumerable-singleordefault-1(system-collections-generic-ienumerable((-0))-0)
    public static TSource SingleOrDefault<TSource>(this IEnumerable<TSource> source, TSource defaultValue)
    {
        var single = source.TryGetSingle(out var found);
        return found ? single! : defaultValue;
    }

    static TSource? TryGetSingle<TSource>(this IEnumerable<TSource> source, out bool found)
    {
        if (source is IList<TSource> list)
        {
            switch (list.Count)
            {
                case 0:
                    found = false;
                    return default;
                case 1:
                    found = true;
                    return list[0];
            }
        }
        else
        {
            using var e = source.GetEnumerator();
            if (!e.MoveNext())
            {
                found = false;
                return default;
            }

            var result = e.Current;
            if (!e.MoveNext())
            {
                found = true;
                return result;
            }
        }

        found = false;
        throw new InvalidOperationException("Sequence contains more than one matching element");
    }
}

#endif
