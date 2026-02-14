#if FeatureMemory

namespace Polyfills;

using System;
using System.Buffers;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

static partial class Polyfill
{
#if !NET7_0_OR_GREATER

        /// <summary>Finds the length of any common prefix shared between <paramref name="span"/> and <paramref name="other"/>.</summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.commonprefixlength?view=net-11.0#system-memoryextensions-commonprefixlength-1(system-span((-0))-system-readonlyspan((-0)))
        [OverloadResolutionPriority(-1)]
        public static int CommonPrefixLength<T>(this Span<T> span, ReadOnlySpan<T> other) =>
            CommonPrefixLength((ReadOnlySpan<T>)span, other);

        /// <summary>Finds the length of any common prefix shared between <paramref name="span"/> and <paramref name="other"/>.</summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.commonprefixlength?view=net-11.0#system-memoryextensions-commonprefixlength-1(system-span((-0))-system-readonlyspan((-0))-system-collections-generic-iequalitycomparer((-0)))
        [OverloadResolutionPriority(-1)]
        public static int CommonPrefixLength<T>(this Span<T> span, ReadOnlySpan<T> other, IEqualityComparer<T>? comparer) =>
            CommonPrefixLength((ReadOnlySpan<T>)span, other, comparer);

        /// <summary>Finds the length of any common prefix shared between <paramref name="span"/> and <paramref name="other"/>.</summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.commonprefixlength?view=net-11.0#system-memoryextensions-commonprefixlength-1(system-readonlyspan((-0))-system-readonlyspan((-0)))
        public static int CommonPrefixLength<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> other)
        {
            SliceLongerSpanToMatchShorterLength(ref span, ref other);

            for (int i = 0; i < span.Length; i++)
            {
                if (!EqualityComparer<T>.Default.Equals(span[i], other[i]))
                {
                    return i;
                }
            }

            return span.Length;
        }

        /// <summary>Determines the length of any common prefix shared between <paramref name="span"/> and <paramref name="other"/>.</summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.commonprefixlength?view=net-11.0#system-memoryextensions-commonprefixlength-1(system-span((-0))-system-readonlyspan((-0))-system-collections-generic-iequalitycomparer((-0)))
        public static int CommonPrefixLength<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> other, IEqualityComparer<T>? comparer)
        {
            if (typeof(T).IsValueType && (comparer is null || comparer == EqualityComparer<T>.Default))
            {
                return CommonPrefixLength(span, other);
            }

            SliceLongerSpanToMatchShorterLength(ref span, ref other);

            comparer ??= EqualityComparer<T>.Default;
            for (int i = 0; i < span.Length; i++)
            {
                if (!comparer.Equals(span[i], other[i]))
                {
                    return i;
                }
            }

            return span.Length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void SliceLongerSpanToMatchShorterLength<T>(ref ReadOnlySpan<T> span, ref ReadOnlySpan<T> other)
        {
            if (other.Length > span.Length)
            {
                other = other.Slice(0, span.Length);
            }
            else if (span.Length > other.Length)
            {
                span = span.Slice(0, other.Length);
            }
        }

#endif
}

#endif
