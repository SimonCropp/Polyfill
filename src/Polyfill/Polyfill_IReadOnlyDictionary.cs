#if (NETFRAMEWORK || NETSTANDARD2_0) && !WINDOWS_UWP
#pragma warning disable CS8714

namespace Polyfills;

using System.Collections.Generic;

static partial class Polyfill
{
    /// <summary>
    /// Tries to get the value associated with the specified key in the dictionary.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.getvalueordefault?view=net-11.0#system-collections-generic-collectionextensions-getvalueordefault-2(system-collections-generic-ireadonlydictionary((-0-1))-0)
    public static TValue? GetValueOrDefault<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> target, TKey key) =>
        target.GetValueOrDefault(key, default!);

    /// <summary>
    /// Tries to get the value associated with the specified key in the dictionary.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.getvalueordefault?view=net-11.0#system-collections-generic-collectionextensions-getvalueordefault-2(system-collections-generic-ireadonlydictionary((-0-1))-0-1)
    public static TValue GetValueOrDefault<TKey, TValue>(
        this IReadOnlyDictionary<TKey, TValue> target,
        TKey key,
        TValue defaultValue)
        where TKey : notnull
    {
        if (target.TryGetValue(key, out var result))
        {
            return result!;
        }

        return defaultValue;
    }
}
#endif
