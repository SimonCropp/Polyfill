#if FeatureMemory && !NET7_0_OR_GREATER

namespace Polyfills;

using System;

static partial class Polyfill
{
    /// <summary>Searches from the end for the first index of any value other than the specified <paramref name="value"/>.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.lastindexofanyexcept?view=net-11.0#system-memoryextensions-lastindexofanyexcept-1(system-readonlyspan((-0))-0)
    public static int LastIndexOfAnyExcept<T>(this ReadOnlySpan<T> span, T value)
        where T : IEquatable<T>
    {
        for (var i = span.Length - 1; i >= 0; i--)
        {
            if (!span[i].Equals(value))
            {
                return i;
            }
        }

        return -1;
    }

    /// <summary>Searches from the end for the first index of any value other than <paramref name="value0"/> or <paramref name="value1"/>.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.lastindexofanyexcept?view=net-11.0#system-memoryextensions-lastindexofanyexcept-1(system-readonlyspan((-0))-0-0)
    public static int LastIndexOfAnyExcept<T>(this ReadOnlySpan<T> span, T value0, T value1)
        where T : IEquatable<T>
    {
        for (var i = span.Length - 1; i >= 0; i--)
        {
            var item = span[i];
            if (!item.Equals(value0) && !item.Equals(value1))
            {
                return i;
            }
        }

        return -1;
    }

    /// <summary>Searches from the end for the first index of any value other than <paramref name="value0"/>, <paramref name="value1"/>, or <paramref name="value2"/>.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.lastindexofanyexcept?view=net-11.0#system-memoryextensions-lastindexofanyexcept-1(system-readonlyspan((-0))-0-0-0)
    public static int LastIndexOfAnyExcept<T>(this ReadOnlySpan<T> span, T value0, T value1, T value2)
        where T : IEquatable<T>
    {
        for (var i = span.Length - 1; i >= 0; i--)
        {
            var item = span[i];
            if (!item.Equals(value0) && !item.Equals(value1) && !item.Equals(value2))
            {
                return i;
            }
        }

        return -1;
    }

    /// <summary>Searches from the end for the first index of any value other than the specified <paramref name="values"/>.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.lastindexofanyexcept?view=net-11.0#system-memoryextensions-lastindexofanyexcept-1(system-readonlyspan((-0))-system-readonlyspan((-0)))
    public static int LastIndexOfAnyExcept<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> values)
        where T : IEquatable<T>
    {
        for (var i = span.Length - 1; i >= 0; i--)
        {
            if (!values.Contains(span[i]))
            {
                return i;
            }
        }

        return -1;
    }

    /// <summary>Searches from the end for the first index of any value other than the specified <paramref name="value"/>.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.lastindexofanyexcept?view=net-11.0#system-memoryextensions-lastindexofanyexcept-1(system-span((-0))-0)
    public static int LastIndexOfAnyExcept<T>(this Span<T> span, T value)
        where T : IEquatable<T> =>
        LastIndexOfAnyExcept((ReadOnlySpan<T>)span, value);

    /// <summary>Searches from the end for the first index of any value other than <paramref name="value0"/> or <paramref name="value1"/>.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.lastindexofanyexcept?view=net-11.0#system-memoryextensions-lastindexofanyexcept-1(system-span((-0))-0-0)
    public static int LastIndexOfAnyExcept<T>(this Span<T> span, T value0, T value1)
        where T : IEquatable<T> =>
        LastIndexOfAnyExcept((ReadOnlySpan<T>)span, value0, value1);

    /// <summary>Searches from the end for the first index of any value other than <paramref name="value0"/>, <paramref name="value1"/>, or <paramref name="value2"/>.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.lastindexofanyexcept?view=net-11.0#system-memoryextensions-lastindexofanyexcept-1(system-span((-0))-0-0-0)
    public static int LastIndexOfAnyExcept<T>(this Span<T> span, T value0, T value1, T value2)
        where T : IEquatable<T> =>
        LastIndexOfAnyExcept((ReadOnlySpan<T>)span, value0, value1, value2);

    /// <summary>Searches from the end for the first index of any value other than the specified <paramref name="values"/>.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.lastindexofanyexcept?view=net-11.0#system-memoryextensions-lastindexofanyexcept-1(system-span((-0))-system-readonlyspan((-0)))
    public static int LastIndexOfAnyExcept<T>(this Span<T> span, ReadOnlySpan<T> values)
        where T : IEquatable<T> =>
        LastIndexOfAnyExcept((ReadOnlySpan<T>)span, values);
}

#endif
