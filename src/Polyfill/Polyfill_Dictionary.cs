namespace Polyfills;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

static partial class Polyfill
{
#if !NET7_0_OR_GREATER
    /// <summary>
    /// Returns a read-only <see cref="ReadOnlyDictionary{TKey,TValue}"/> wrapper for the current dictionary.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.asreadonly?view=net-11.0#system-collections-generic-collectionextensions-asreadonly-2(system-collections-generic-idictionary((-0-1)))
    public static ReadOnlyDictionary<TKey, TValue> AsReadOnly<TKey, TValue>(this IDictionary<TKey, TValue> target)
        where TKey : notnull =>
        new(target);
#endif

#if (NETFRAMEWORK || NETSTANDARD2_0) && !WINDOWS_UWP

    /// <summary>
    /// Attempts to add the specified key and value to the <see cref="IDictionary{TKey,TValue}"/>.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.tryadd?view=net-11.0
    public static bool TryAdd<TKey, TValue>(this IDictionary<TKey, TValue> target, TKey key, TValue value)
        where TKey : notnull
    {
        if (!target.ContainsKey(key))
        {
            target.Add(key, value);
            return true;
        }

        return false;
    }

    /// <summary>
    /// Removes the value with the specified key from the <see cref="IDictionary{TKey,TValue}"/>, and copies the element
    /// to the value parameter.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.remove?view=net-11.0
    public static bool Remove<TKey, TValue>(
        this IDictionary<TKey, TValue> target,
        TKey key,
        [MaybeNullWhen(false)] out TValue value)
        where TKey : notnull
    {
        target.TryGetValue(key, out value);
        return target.Remove(key);
    }

#endif

#if !NETCOREAPP2_1_OR_GREATER && !NETSTANDARD2_1_OR_GREATER

    /// <summary>
    /// Ensures that the capacity of this dictionary is at least the specified capacity. If the current capacity is less than capacity, it is increased to at least the specified capacity.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2.ensurecapacity?view=net-11.0
    public static void EnsureCapacity<TKey, TValue>(this Dictionary<TKey, TValue> target, int capacity)
    {
    }

    /// <summary>
    /// Sets the capacity of this dictionary to hold up a specified number of entries without any further expansion of its backing storage.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2.trimexcess?view=net-11.0#system-collections-generic-dictionary-2-trimexcess(system-int32)
    public static void TrimExcess<TKey, TValue>(this Dictionary<TKey, TValue> target, int capacity)
    {
    }

    /// <summary>
    /// Sets the capacity of this dictionary to what it would be if it had been originally initialized with all its entries.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2.trimexcess?view=net-11.0#system-collections-generic-dictionary-2-trimexcess
    public static void TrimExcess<TKey, TValue>(this Dictionary<TKey, TValue> target)
    {
    }

#endif
}
