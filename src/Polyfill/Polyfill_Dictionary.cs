// <auto-generated />
#pragma warning disable

namespace Polyfills;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using Link = System.ComponentModel.DescriptionAttribute;

static partial class Polyfill
{
#if !NET7_0_OR_GREATER
    /// <summary>
    /// Returns a read-only <see cref="ReadOnlyDictionary{TKey,TValue}"/> wrapper for the current dictionary.
    /// </summary>
    /// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
    /// <param name="dictionary">The dictionary to wrap.</param>
    /// <returns>An object that acts as a read-only wrapper around the current <see cref="IDictionary{TKey, TValue}"/>.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="dictionary"/> is null.</exception>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.asreadonly#system-collections-generic-collectionextensions-asreadonly-2(system-collections-generic-idictionary((-0-1)))")]
    public static ReadOnlyDictionary<TKey, TValue> AsReadOnly<TKey, TValue>(this IDictionary<TKey, TValue> target) =>
        new(target);
#endif
#if NETFRAMEWORK || NETSTANDARD2_0 || NETCOREAPP2X
    /// <summary>
    /// Removes the value with the specified key from the <see cref="Dictionary{TKey,TValue}"/>, and copies the element
    /// to the value parameter.
    /// </summary>
    /// <param name="target">A dictionary with keys of type TKey and values of type TValue.</param>
    /// <param name="key">The key of the element to remove.</param>
    /// <param name="value">The removed element.</param>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    /// <returns><code>true</code> if the element is successfully found and removed; otherwise, <code>false</code>.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="key"/> is <code>null</code>.</exception>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2.remove")]
    public static bool Remove<TKey, TValue>(
        this Dictionary<TKey, TValue> target,
        TKey key,
        [MaybeNullWhen(false)] out TValue value)
    {
        target.TryGetValue(key, out value);
        return target.Remove(key);
    }
#endif
}