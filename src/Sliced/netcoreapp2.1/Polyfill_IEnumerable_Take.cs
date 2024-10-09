



#pragma warning disable

namespace Polyfills;
using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using Link = System.ComponentModel.DescriptionAttribute;

static partial class Polyfill
{
#if !NET6_0_OR_GREATER

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static bool IsEmptyArray<TSource>(IEnumerable<TSource> source) =>
        source is TSource[] { Length: 0 };

    static IEnumerable<TSource> TakeRangeFromEndIterator<TSource>(IEnumerable<TSource> source, bool isStartIndexFromEnd, int startIndex, bool isEndIndexFromEnd, int endIndex)
    {
        
        
        
        
        
        if (source.TryGetNonEnumeratedCount(out int count))
        {
            startIndex = CalculateStartIndex(isStartIndexFromEnd, startIndex, count);
            endIndex = CalculateEndIndex(isEndIndexFromEnd, endIndex, count);

            if (startIndex < endIndex)
            {
                foreach (TSource element in TakeRangeIterator(source, startIndex, endIndex))
                {
                    yield return element;
                }
            }

            yield break;
        }

        Queue<TSource> queue;

        if (isStartIndexFromEnd)
        {
            
            using (IEnumerator<TSource> e = source.GetEnumerator())
            {
                if (!e.MoveNext())
                {
                    yield break;
                }

                queue = new Queue<TSource>();
                queue.Enqueue(e.Current);
                count = 1;

                while (e.MoveNext())
                {
                    if (count < startIndex)
                    {
                        queue.Enqueue(e.Current);
                        ++count;
                    }
                    else
                    {
                        do
                        {
                            queue.Dequeue();
                            queue.Enqueue(e.Current);
                            checked
                            {
                                ++count;
                            }
                        } while (e.MoveNext());

                        break;
                    }
                }
            }

            startIndex = CalculateStartIndex(isStartIndexFromEnd: true, startIndex, count);
            endIndex = CalculateEndIndex(isEndIndexFromEnd, endIndex, count);

            for (int rangeIndex = startIndex; rangeIndex < endIndex; rangeIndex++)
            {
                yield return queue.Dequeue();
            }
        }
        else
        {
            
            using IEnumerator<TSource> e = source.GetEnumerator();

            count = 0;
            while (count < startIndex && e.MoveNext())
            {
                ++count;
            }

            if (count == startIndex)
            {
                queue = new Queue<TSource>();
                while (e.MoveNext())
                {
                    if (queue.Count == endIndex)
                    {
                        do
                        {
                            queue.Enqueue(e.Current);
                            yield return queue.Dequeue();
                        } while (e.MoveNext());

                        break;
                    }
                    else
                    {
                        queue.Enqueue(e.Current);
                    }
                }
            }
        }

        static int CalculateStartIndex(bool isStartIndexFromEnd, int startIndex, int count) =>
            Math.Max(0, isStartIndexFromEnd ? count - startIndex : startIndex);

        static int CalculateEndIndex(bool isEndIndexFromEnd, int endIndex, int count) =>
            Math.Min(count, isEndIndexFromEnd ? count - endIndex : endIndex);
    }

    static IEnumerable<TSource> TakeRangeIterator<TSource>(IEnumerable<TSource> source, int startIndex, int endIndex)
    {
        using IEnumerator<TSource> e = source.GetEnumerator();

        int index = 0;
        while (index < startIndex && e.MoveNext())
        {
            ++index;
        }

        if (index < startIndex)
        {
            yield break;
        }

        while (index < endIndex && e.MoveNext())
        {
            yield return e.Current;
            ++index;
        }
    }

#if FeatureValueTuple
#endif
#endif

}