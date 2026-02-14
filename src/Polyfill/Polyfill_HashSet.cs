namespace Polyfills;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

static partial class Polyfill
{
#if !NETCOREAPP && !NETSTANDARD2_1_OR_GREATER && !NET472_OR_GREATER
    /// <summary>
    ///  Searches the set for a given value and returns the equal value it finds, if any.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1.trygetvalue?view=net-11.0
    public static bool TryGetValue<T>(
        this HashSet<T> target,
        T equalValue,
        [MaybeNullWhen(false)] out T actualValue)
    {
        var comparer = target.Comparer;
        var hashCode = comparer.GetHashCode(equalValue);
        foreach (var item in target)
        {
            if (comparer.GetHashCode(item) == hashCode &&
                comparer.Equals(item, equalValue))
            {
                actualValue = item;
                return true;
            }
        }

        actualValue = default;
        return false;
    }
#endif

#if !NET6_0_OR_GREATER

    /// <summary>
    /// Ensures that the capacity of this HashSet is at least the specified capacity. If the current capacity is less than capacity, it is increased to at least the specified capacity.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1.ensurecapacity?view=net-11.0#system-collections-generic-hashset-1-ensurecapacity(system-int32)
    public static void EnsureCapacity<T>(this HashSet<T> target, int capacity)
    {
    }

#endif

#if !NET9_0_OR_GREATER

    /// <summary>
    /// Sets the capacity of a HashSet object to the specified number of entries, rounded up to a nearby, implementation-specific value.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1.trimexcess?view=net-11.0#system-collections-generic-hashset-1-trimexcess(system-int32)
    public static void TrimExcess<T>(this HashSet<T> target, int capacity)
    {
    }

#endif
}
