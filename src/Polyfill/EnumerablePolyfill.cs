#if NET7_0_OR_GREATER && !NET10_0_OR_GREATER
namespace Polyfills;

using System;
using System.Collections.Generic;
using System.Numerics;
using System.Linq;

static partial class Polyfill
{
    extension(Enumerable)
    {
        /// <summary>
        /// Generates an infinite sequence that begins with <paramref name="start"/> and yields additional values each incremented by <paramref name="step"/>.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.infinitesequence?view=net-11.0
        public static IEnumerable<T> InfiniteSequence<T>(T start, T step)
            where T : IAdditionOperators<T, T, T>
        {
            while (true)
            {
                yield return start;
                start += step;
            }
        }

        /// <summary>
        /// Generates a sequence that begins with <paramref name="start"/> and yields additional values each incremented by <paramref name="step"/> until <paramref name="endInclusive"/> is reached.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.sequence?view=net-11.0
        public static IEnumerable<T> Sequence<T>(T start, T endInclusive, T step)
            where T : INumber<T>
        {
            if (T.IsPositive(step))
            {
                // Presumed to be the most common case, step > 0. Validate that endInclusive >= start, as otherwise we can't easily
                // guarantee that the sequence will terminate.
                if (endInclusive < start)
                {
                    throw new ArgumentOutOfRangeException("endInclusive < start");
                }

                return IncrementingIterator(start, endInclusive, step);
            }
            else
            {
                // step < 0. Validate that endInclusive <= start, as otherwise we can't easily guarantee that the sequence will terminate.
                if (endInclusive > start)
                {
                    throw new ArgumentOutOfRangeException("endInclusive > start");
                }

                return DecrementingIterator(start, endInclusive, step);
            }

            static IEnumerable<T> IncrementingIterator(T current, T endInclusive, T step)
            {
                yield return current;

                while (true)
                {
                    T next = current + step;
                    // handle overflow and saturation
                    if (next >= endInclusive || next <= current)
                    {
                        if (next == endInclusive && current != next)
                        {
                            yield return next;
                        }

                        yield break;
                    }

                    yield return next;
                    current = next;
                }
            }

            static IEnumerable<T> DecrementingIterator(T current, T endInclusive, T step)
            {
                yield return current;

                while (true)
                {
                    T next = current + step;
                    // handle overflow and saturation
                    if (next <= endInclusive || next >= current)
                    {
                        if (next == endInclusive && current != next)
                        {
                            yield return next;
                        }

                        yield break;
                    }

                    yield return next;
                    current = next;
                }
            }
        }
    }
}

#endif
