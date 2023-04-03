#if NETFRAMEWORK || NETSTANDARD2_0

#pragma warning disable

// ReSharper disable RedundantUsingDirective
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable UnusedMember.Global

using System;
using System.Collections.Generic;
using DescriptionAttribute = System.ComponentModel.DescriptionAttribute;
// ReSharper disable RedundantAttributeSuffix

static partial class PolyfillExtensions
{
    /// <summary>
    /// Tries to get the value associated with the specified key in the dictionary.
    /// </summary>
    /// <param name="target">A dictionary with keys of type TKey and values of type TValue.</param>
    /// <param name="key">The key of the value to get.</param>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    /// <returns>
    /// A TValue instance. When the method is successful, the returned object is the value associated with
    /// the specified key. When the method fails, it returns the default value for TValue.
    /// </returns>
    [DescriptionAttribute("https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.getvalueordefault")]
    public static TValue? GetValueOrDefault<TKey, TValue>(
        this IReadOnlyDictionary<TKey, TValue> target,
        TKey key)
    {
        if (target.TryGetValue(key, out var result))
        {
            return result;
        }

        return default;
    }

    /// <summary>
    /// Tries to get the value associated with the specified key in the dictionary.
    /// </summary>
    /// <param name="target">A dictionary with keys of type TKey and values of type TValue.</param>
    /// <param name="key">The key of the value to get.</param>
    /// <param name="defaultValue">A dictionary with keys of type TKey and values of type TValue.</param>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    /// <returns>
    /// A TValue instance. When the method is successful, the returned object is the value associated with
    /// the specified key. When the method fails, it returns the default value for TValue.
    /// </returns>
    [DescriptionAttribute("https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.getvalueordefault#system-collections-generic-collectionextensions-getvalueordefault-2(system-collections-generic-ireadonlydictionary((-0-1))-0-1)")]
    public static TValue GetValueOrDefault<TKey, TValue>(
        this IReadOnlyDictionary<TKey, TValue> target,
        TKey key,
        TValue defaultValue = default)
    {
        if (target.TryGetValue(key, out var result))
        {
            return result!;
        }

        return defaultValue;
    }
}
#endif