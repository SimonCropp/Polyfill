#if FeatureMemory && !NET8_0_OR_GREATER

namespace Polyfills;

using System;

static partial class Polyfill
{
    /// <summary>Searches for any value in the range between <paramref name="lowInclusive"/> and <paramref name="highInclusive"/>, inclusive.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.containsanyinrange?view=net-11.0#system-memoryextensions-containsanyinrange-1(system-readonlyspan((-0))-0-0)
    public static bool ContainsAnyInRange<T>(this ReadOnlySpan<T> span, T lowInclusive, T highInclusive)
        where T : IComparable<T>
    {
        for (var i = 0; i < span.Length; i++)
        {
            var item = span[i];
            if (item.CompareTo(lowInclusive) >= 0 && item.CompareTo(highInclusive) <= 0)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>Searches for any value in the range between <paramref name="lowInclusive"/> and <paramref name="highInclusive"/>, inclusive.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.containsanyinrange?view=net-11.0#system-memoryextensions-containsanyinrange-1(system-span((-0))-0-0)
    public static bool ContainsAnyInRange<T>(this Span<T> span, T lowInclusive, T highInclusive)
        where T : IComparable<T> =>
        ContainsAnyInRange((ReadOnlySpan<T>)span, lowInclusive, highInclusive);

    /// <summary>Searches for any value outside of the range between <paramref name="lowInclusive"/> and <paramref name="highInclusive"/>, inclusive.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.containsanyexceptinrange?view=net-11.0#system-memoryextensions-containsanyexceptinrange-1(system-readonlyspan((-0))-0-0)
    public static bool ContainsAnyExceptInRange<T>(this ReadOnlySpan<T> span, T lowInclusive, T highInclusive)
        where T : IComparable<T>
    {
        for (var i = 0; i < span.Length; i++)
        {
            var item = span[i];
            if (item.CompareTo(lowInclusive) < 0 || item.CompareTo(highInclusive) > 0)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>Searches for any value outside of the range between <paramref name="lowInclusive"/> and <paramref name="highInclusive"/>, inclusive.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.containsanyexceptinrange?view=net-11.0#system-memoryextensions-containsanyexceptinrange-1(system-span((-0))-0-0)
    public static bool ContainsAnyExceptInRange<T>(this Span<T> span, T lowInclusive, T highInclusive)
        where T : IComparable<T> =>
        ContainsAnyExceptInRange((ReadOnlySpan<T>)span, lowInclusive, highInclusive);
}

#endif
