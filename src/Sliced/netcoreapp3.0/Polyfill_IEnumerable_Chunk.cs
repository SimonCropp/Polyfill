
#pragma warning disable

namespace Polyfills;
using System;
using System.Collections.Generic;
using Link = System.ComponentModel.DescriptionAttribute;

static partial class Polyfill
{

    /// <summary>
    /// Split the elements of a sequence into chunks of size at most <paramref name="size"/>.
    /// </summary>
    /// <remarks>
    /// Every chunk except the last will be of size <paramref name="size"/>.
    /// The last chunk will contain the remaining elements and may be of a smaller size.
    /// </remarks>
    /// <param name="source">An <see cref="IEnumerable{T}"/> whose elements to chunk.</param>
    /// <param name="size">Maximum size of each chunk.</param>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <returns>
    /// An <see cref="IEnumerable{T}"/> that contains the elements the input sequence split into chunks of size <paramref name="size"/>.
    /// </returns>
    /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="size"/> is below 1.</exception>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.chunk")]
    public static IEnumerable<TSource[]> Chunk<TSource>(this IEnumerable<TSource> source, int size)
    {
        if (source is null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        if (size < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(size), size, "Size must be greater than 0.");
        }

        return ChunkIterator(source, size);

        static IEnumerable<TSource[]> ChunkIterator<TSource>(IEnumerable<TSource> source, int size)
        {
            using IEnumerator<TSource> e = source.GetEnumerator();

            
            if (e.MoveNext())
            {
                
                
                
                int arraySize = Math.Min(size, 4);
                int i;
                do
                {
                    var array = new TSource[arraySize];

                    
                    array[0] = e.Current;
                    i = 1;

                    if (size != array.Length)
                    {
                        
                        for (; i < size && e.MoveNext(); i++)
                        {
                            if (i >= array.Length)
                            {
                                arraySize = (int) Math.Min((uint) size, 2 * (uint) array.Length);
                                Array.Resize(ref array, arraySize);
                            }

                            array[i] = e.Current;
                        }
                    }
                    else
                    {
                        
                        
                        
                        TSource[] local = array;
                        for (; (uint) i < (uint) local.Length && e.MoveNext(); i++)
                        {
                            local[i] = e.Current;
                        }
                    }

                    if (i != array.Length)
                    {
                        Array.Resize(ref array, i);
                    }

                    yield return array;
                } while (i >= size && e.MoveNext());
            }
        }
    }

}