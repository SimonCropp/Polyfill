#if !NET6_0_OR_GREATER

namespace Polyfills;

using System;
using System.Collections.Generic;

static partial class Polyfill
{
    /// <summary>
    /// Split the elements of a sequence into chunks of size at most <paramref name="size"/>.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.chunk?view=net-11.0
    public static IEnumerable<TSource[]> Chunk<TSource>(this IEnumerable<TSource> source, int size)
    {
        if (size < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(size), size, "Size must be greater than 0.");
        }

        using var enumerator = source.GetEnumerator();

        if (!enumerator.MoveNext())
        {
            yield break;
        }

        var arraySize = Math.Min(size, 4);
        int i;
        do
        {
            var array = new TSource[arraySize];

            array[0] = enumerator.Current;
            i = 1;

            if (size != array.Length)
            {
                for (; i < size && enumerator.MoveNext(); i++)
                {
                    if (i >= array.Length)
                    {
                        arraySize = (int) Math.Min((uint) size, 2 * (uint) array.Length);
                        Array.Resize(ref array, arraySize);
                    }

                    array[i] = enumerator.Current;
                }
            }
            else
            {
                var local = array;
                for (; (uint) i < (uint) local.Length && enumerator.MoveNext(); i++)
                {
                    local[i] = enumerator.Current;
                }
            }

            if (i != array.Length)
            {
                Array.Resize(ref array, i);
            }

            yield return array;
        } while (i >= size && enumerator.MoveNext());
    }
}

#endif
