namespace Polyfills;

// ReSharper disable once RedundantUsingDirective
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

static partial class Polyfill
{
#if !NET7_0_OR_GREATER

    /// <summary>Returns a read-only <see cref="ReadOnlyCollection{T}" /> wrapper for the current collection.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.asreadonly?view=net-11.0#system-collections-generic-collectionextensions-asreadonly-1(system-collections-generic-ilist((-0)))
    public static ReadOnlyCollection<T> AsReadOnly<T>(this IList<T> target) =>
        new(target);
#endif

#if !NET8_0_OR_GREATER && FeatureMemory
    /// <summary>Adds the elements of the specified span to the end of the <see cref="List{T}"/>.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.addrange?view=net-11.0
    public static void AddRange<T>(this List<T> target, ReadOnlySpan<T> source)
    {
        foreach (var item in source)
        {
            target.Add(item);
        }
    }

    /// <summary>Inserts the elements of a span into the <see cref="List{T}"/> at the specified index.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.insertrange?view=net-11.0
    public static void InsertRange<T>(this List<T> target, int index, ReadOnlySpan<T> source)
    {
        for (var i = 0; i < source.Length; i++)
        {
            var item = source[i];
            target.Insert(i + index, item);
        }
    }

    /// <summary>Copies the entire <see cref="List{T}"/> to a span.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.copyto?view=net-11.0
    public static void CopyTo<T>(this List<T> target, Span<T> destination)
    {
        for (var index = 0; index < target.Count; index++)
        {
            destination[index] = target[index];
        }
    }
#endif

#if !NET6_0_OR_GREATER

    /// <summary>
    /// Ensures that the capacity of this list is at least the specified capacity. If the current capacity is less than capacity, it is increased to at least the specified capacity.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.ensurecapacity?view=net-11.0#system-collections-generic-list-1-ensurecapacity(system-int32)
    public static void EnsureCapacity<T>(this List<T> target, int capacity)
    {
    }

    /// <summary>
    /// Sets the capacity to the actual number of elements in the <see cref="List{T}"/>, if that number is less than a threshold value.
    /// </summary>
    //Link:https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.trimexcess?view=net-11.0
    public static void TrimExcess<T>(this List<T> target)
    {
    }

#endif
}
