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
    public static void EnsureCapacity<T>(this Queue<T> target, int capacity)
    {
    }

#endif

#if !NET9_0_OR_GREATER

    /// <summary>
    /// Sets the capacity of a Queue object to the specified number of entries.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.queue-1.trimexcess?view=net-11.0#system-collections-generic-queue-1-trimexcess(system-int32)
    public static void TrimExcess<T>(this Queue<T> target, int capacity)
    {
    }

#endif
}
