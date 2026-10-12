#if FeatureMemory && !NET7_0_OR_GREATER

namespace Polyfills;

using System;
using System.Collections.Generic;

static partial class Polyfill
{
    /// <summary>Searches from the end for the first index of any value other than the specified <paramref name="value"/>.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.lastindexofanyexcept?view=net-11.0#system-memoryextensions-lastindexofanyexcept-1(system-readonlyspan((-0))-0)
    public static int LastIndexOfAnyExcept<T>(this ReadOnlySpan<T> span, T value)
        where T : IEquatable<T>?
    {
        var comparer = EqualityComparer<T>.Default;
        for (var i = span.Length - 1; i >= 0; i--)
        {
            if (!comparer.Equals(span[i], value))
            {
                return i;
            }
        }

        return -1;
    }

    /// <summary>Searches from the end for the first index of any value other than <paramref name="value0"/> or <paramref name="value1"/>.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.lastindexofanyexcept?view=net-11.0#system-memoryextensions-lastindexofanyexcept-1(system-readonlyspan((-0))-0-0)
    public static int LastIndexOfAnyExcept<T>(this ReadOnlySpan<T> span, T value0, T value1)
        where T : IEquatable<T>?
    {
        var comparer = EqualityComparer<T>.Default;
        for (var i = span.Length - 1; i >= 0; i--)
        {
            var item = span[i];
            if (!comparer.Equals(item, value0) && !comparer.Equals(item, value1))
            {
                return i;
            }
        }

        return -1;
    }

    /// <summary>Searches from the end for the first index of any value other than <paramref name="value0"/>, <paramref name="value1"/>, or <paramref name="value2"/>.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.lastindexofanyexcept?view=net-11.0#system-memoryextensions-lastindexofanyexcept-1(system-readonlyspan((-0))-0-0-0)
    public static int LastIndexOfAnyExcept<T>(this ReadOnlySpan<T> span, T value0, T value1, T value2)
        where T : IEquatable<T>?
    {
        var comparer = EqualityComparer<T>.Default;
        for (var i = span.Length - 1; i >= 0; i--)
        {
            var item = span[i];
            if (!comparer.Equals(item, value0) && !comparer.Equals(item, value1) && !comparer.Equals(item, value2))
            {
                return i;
            }
        }

        return -1;
    }

    /// <summary>Searches from the end for the first index of any value other than the specified <paramref name="values"/>.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.lastindexofanyexcept?view=net-11.0#system-memoryextensions-lastindexofanyexcept-1(system-readonlyspan((-0))-system-readonlyspan((-0)))
    public static int LastIndexOfAnyExcept<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> values)
        where T : IEquatable<T>?
    {
        var comparer = EqualityComparer<T>.Default;
        for (var i = span.Length - 1; i >= 0; i--)
        {
            if (!ContainsValue(values, span[i], comparer))
            {
                return i;
            }
        }

        return -1;
    }

    /// <summary>Searches from the end for the first index of any value other than the specified <paramref name="value"/>.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.lastindexofanyexcept?view=net-11.0#system-memoryextensions-lastindexofanyexcept-1(system-span((-0))-0)
    public static int LastIndexOfAnyExcept<T>(this Span<T> span, T value)
        where T : IEquatable<T>? =>
        LastIndexOfAnyExcept((ReadOnlySpan<T>)span, value);

    /// <summary>Searches from the end for the first index of any value other than <paramref name="value0"/> or <paramref name="value1"/>.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.lastindexofanyexcept?view=net-11.0#system-memoryextensions-lastindexofanyexcept-1(system-span((-0))-0-0)
    public static int LastIndexOfAnyExcept<T>(this Span<T> span, T value0, T value1)
        where T : IEquatable<T>? =>
        LastIndexOfAnyExcept((ReadOnlySpan<T>)span, value0, value1);

    /// <summary>Searches from the end for the first index of any value other than <paramref name="value0"/>, <paramref name="value1"/>, or <paramref name="value2"/>.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.lastindexofanyexcept?view=net-11.0#system-memoryextensions-lastindexofanyexcept-1(system-span((-0))-0-0-0)
    public static int LastIndexOfAnyExcept<T>(this Span<T> span, T value0, T value1, T value2)
        where T : IEquatable<T>? =>
        LastIndexOfAnyExcept((ReadOnlySpan<T>)span, value0, value1, value2);

    /// <summary>Searches from the end for the first index of any value other than the specified <paramref name="values"/>.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.lastindexofanyexcept?view=net-11.0#system-memoryextensions-lastindexofanyexcept-1(system-span((-0))-system-readonlyspan((-0)))
    public static int LastIndexOfAnyExcept<T>(this Span<T> span, ReadOnlySpan<T> values)
        where T : IEquatable<T>? =>
        LastIndexOfAnyExcept((ReadOnlySpan<T>)span, values);
}

#endif
