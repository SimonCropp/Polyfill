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
        if (source.IsEmpty)
        {
            return;
        }

        var length = source.Length;

        // List<T>.AddRange block copies an ICollection<T>. On .NET Framework it goes through an extra
        // temporary array, which only pays off when the items can be block copied without GC bookkeeping.
        if (length >= blockCopyThreshold
#if !NETCOREAPP
            && typeof(T).IsPrimitive
#endif
           )
        {
            var pool = System.Buffers.ArrayPool<T>.Shared;
            var buffer = pool.Rent(length);
            try
            {
                source.CopyTo(buffer);
                target.AddRange(new ArraySegment<T>(buffer, 0, length));
            }
            finally
            {
                ReturnToPool(pool, buffer, length);
            }

            return;
        }

        // Grow once, using the same doubling as List<T>, instead of several times while adding
        var required = target.Count + length;
        var capacity = target.Capacity;
        if (required > capacity)
        {
            var newCapacity = capacity * 2;
            if ((uint) newCapacity > 0x7FEFFFFF)
            {
                newCapacity = 0x7FEFFFFF;
            }

            if (newCapacity < required)
            {
                newCapacity = required;
            }

            target.Capacity = newCapacity;
        }

        foreach (var item in source)
        {
            target.Add(item);
        }
    }

    // Below this the cost of renting a buffer outweighs the block copy
    const int blockCopyThreshold = 64;

    static void ReturnToPool<T>(System.Buffers.ArrayPool<T> pool, T[] buffer, int length)
    {
        // Do not keep references alive in the pool
        if (!typeof(T).IsPrimitive)
        {
            Array.Clear(buffer, 0, length);
        }

        pool.Return(buffer);
    }

    /// <summary>Inserts the elements of a span into the <see cref="List{T}"/> at the specified index.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.insertrange?view=net-11.0
    public static void InsertRange<T>(this List<T> target, int index, ReadOnlySpan<T> source)
    {
        if ((uint)index > (uint)target.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        if (source.IsEmpty)
        {
            return;
        }

        if (source.Length == 1)
        {
            target.Insert(index, source[0]);
            return;
        }

        // List<T>.InsertRange moves the tail once and block copies an ICollection<T>,
        // where inserting one item at a time moves the tail for every item.
        var length = source.Length;
        var pool = System.Buffers.ArrayPool<T>.Shared;
        var buffer = pool.Rent(length);
        try
        {
            source.CopyTo(buffer);
            target.InsertRange(index, new ArraySegment<T>(buffer, 0, length));
        }
        finally
        {
            // Do not keep references alive in the pool
            if (!typeof(T).IsPrimitive)
            {
                Array.Clear(buffer, 0, length);
            }

            pool.Return(buffer);
        }
    }

    /// <summary>Copies the entire <see cref="List{T}"/> to a span.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.copyto?view=net-11.0
    public static void CopyTo<T>(this List<T> target, Span<T> destination)
    {
        if (target.Count > destination.Length)
        {
            throw new ArgumentException("Destination is too short.", nameof(destination));
        }

#if NET5_0_OR_GREATER
        System.Runtime.InteropServices.CollectionsMarshal.AsSpan(target).CopyTo(destination);
#else
        var count = target.Count;
        if (count < blockCopyThreshold)
        {
            for (var index = 0; index < count; index++)
            {
                destination[index] = target[index];
            }

            return;
        }

        var pool = System.Buffers.ArrayPool<T>.Shared;
        var buffer = pool.Rent(count);
        try
        {
            target.CopyTo(0, buffer, 0, count);
            new ReadOnlySpan<T>(buffer, 0, count).CopyTo(destination);
        }
        finally
        {
            ReturnToPool(pool, buffer, count);
        }
#endif
    }
#endif

#if !NET6_0_OR_GREATER

    /// <summary>
    /// Ensures that the capacity of this list is at least the specified capacity. If the current capacity is less than capacity, it is increased to at least the specified capacity.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.ensurecapacity?view=net-11.0#system-collections-generic-list-1-ensurecapacity(system-int32)
    //Note: No-op on older targets; the BCL grows the backing storage.
    //Note: Returns void on older targets; the BCL returns int (the new capacity).
    public static void EnsureCapacity<T>(this List<T> target, int capacity)
    {
    }

    /// <summary>
    /// Sets the capacity to the actual number of elements in the <see cref="List{T}"/>, if that number is less than a threshold value.
    /// </summary>
    //Link:https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.trimexcess?view=net-11.0
    //Note: No-op on older targets; the BCL shrinks the backing storage.
    public static void TrimExcess<T>(this List<T> target)
    {
    }

#endif
}
