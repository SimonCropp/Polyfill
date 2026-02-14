namespace Polyfills;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

static partial class Polyfill
{
#if !NET6_0_OR_GREATER

    /// <summary>
    /// Ensures that the capacity of this Stack is at least the specified capacity. If the current capacity is less than capacity, it is increased to at least the specified capacity.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.stack-1.ensurecapacity?view=net-11.0
    public static void EnsureCapacity<T>(this Stack<T> target, int capacity)
    {
    }

#endif

#if !NET9_0_OR_GREATER

    /// <summary>
    /// Sets the capacity of a Stack object to a specified number of entries.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.stack-1.trimexcess?view=net-11.0#system-collections-generic-stack-1-trimexcess(system-int32)
    public static void TrimExcess<T>(this Stack<T> target, int capacity)
    {
    }

#endif

#if NETSTANDARD2_0 || NETFRAMEWORK

    /// <summary>
    /// Returns a value that indicates whether there is an object at the top of the <see cref="Stack{T}"/>, and if one is present, copies it to the result parameter. The object is not removed from the <see cref="Stack{T}"/>.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.stack-1.trypeek?view=net-11.0
    public static bool TryPeek<T>(this Stack<T> target, [MaybeNullWhen(false)] out T result)
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
    /// Returns a value that indicates whether there is an object at the top of the <see cref="Stack{T}"/>, and if one is present, copies it to the result parameter, and removes it from the <see cref="Stack{T}"/>.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.stack-1.trypop?view=net-11.0
    public static bool TryPop<T>(this Stack<T> target, [MaybeNullWhen(false)] out T result)
    {
        if (target.Count > 0)
        {
            result = target.Pop();
            return true;
        }

        result = default;
        return false;
    }

#endif
}
