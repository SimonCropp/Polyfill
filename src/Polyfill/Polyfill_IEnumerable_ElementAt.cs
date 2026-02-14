#if FeatureValueTuple && !NET6_0_OR_GREATER

namespace Polyfills;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

static partial class Polyfill
{
    /// <summary>Returns the element at a specified index in a sequence.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.elementat?view=net-11.0#system-linq-enumerable-elementat-1(system-collections-generic-ienumerable((-0))-system-index)
    public static TSource ElementAt<TSource>(this IEnumerable<TSource> source, Index index)
    {
        if (!index.IsFromEnd)
        {
            return Enumerable.ElementAt(source, index.Value);
        }

        if (source.TryGetNonEnumeratedCount(out var count))
        {
            return Enumerable.ElementAt(source, count - index.Value);
        }

        if (!TryGetElementFromEnd(source, index.Value, out var element))
        {
            throw new ArgumentOutOfRangeException("index");
        }

        return element;
    }

    static bool TryGetElementFromEnd<TSource>(IEnumerable<TSource> source, int indexFromEnd, [MaybeNullWhen(false)] out TSource element)
    {
        if (indexFromEnd > 0)
        {
            using var e = source.GetEnumerator();
            if (e.MoveNext())
            {
                Queue<TSource> queue = new();
                queue.Enqueue(e.Current);
                while (e.MoveNext())
                {
                    if (queue.Count == indexFromEnd)
                    {
                        queue.Dequeue();
                    }

                    queue.Enqueue(e.Current);
                }

                if (queue.Count == indexFromEnd)
                {
                    element = queue.Dequeue();
                    return true;
                }
            }
        }

        element = default;
        return false;
    }

    /// <summary>Returns the element at a specified index in a sequence or a default value if the index is out of range.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.elementatordefault?view=net-11.0#system-linq-enumerable-elementatordefault-1(system-collections-generic-ienumerable((-0))-system-index)
    public static TSource? ElementAtOrDefault<TSource>(this IEnumerable<TSource> source, Index index)
    {
        if (!index.IsFromEnd)
        {
            return Enumerable.ElementAtOrDefault(source, index.Value);
        }

        if (source.TryGetNonEnumeratedCount(out var count))
        {
            return Enumerable.ElementAtOrDefault(source, count - index.Value);
        }

        TryGetElementFromEnd(source, index.Value, out var element);
        return element;
    }
}
#endif
