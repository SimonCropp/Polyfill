namespace Polyfills;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

static partial class Polyfill
{
#if !NET6_0_OR_GREATER

    /// <summary>
    /// Ensures that the capacity of this queue is at least the specified capacity. If the current capacity is less than capacity, it is increased to at least the specified capacity.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.queue-1.ensurecapacity?view=net-11.0#system-collections-generic-queue-1-ensurecapacity(system-int32)
    //Note: No-op on older targets; the BCL grows the backing storage.
    public static void EnsureCapacity<T>(this Queue<T> target, int capacity)
    {
    }

#endif

#if !NET9_0_OR_GREATER

    /// <summary>
    /// Sets the capacity of a Queue object to the specified number of entries.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.queue-1.trimexcess?view=net-11.0#system-collections-generic-queue-1-trimexcess(system-int32)
    //Note: No-op on older targets; the BCL shrinks the backing storage.
    public static void TrimExcess<T>(this Queue<T> target, int capacity)
    {
    }

#endif

#if NETSTANDARD2_0 || NETFRAMEWORK

    /// <summary>
    /// Returns a value that indicates whether there is an object at the beginning of the <see cref="Queue{T}"/>, and if one is present, copies it to the result parameter. The object is not removed from the <see cref="Queue{T}"/>.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.queue-1.trypeek?view=net-11.0
    public static bool TryPeek<T>(this Queue<T> target, [MaybeNullWhen(false)] out T result)
    {
        if (target.Count > 0)
        {
            result = target.Peek();
            return true;
        }

        result = default;
        return false;
    }

    /// <summary>
    /// Removes the object at the beginning of the <see cref="Queue{T}"/>, and copies it to the result parameter.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.queue-1.trydequeue?view=net-11.0
    public static bool TryDequeue<T>(this Queue<T> target, [MaybeNullWhen(false)] out T result)
    {
        if (target.Count > 0)
        {
            result = target.Dequeue();
            return true;
        }

        result = default;
        return false;
    }

#endif
}
