
#pragma warning disable

namespace Polyfills;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using Link = System.ComponentModel.DescriptionAttribute;

static partial class Polyfill
{
    /// <summary>
    /// Returns a read-only <see cref="ReadOnlyDictionary{TKey,TValue}"/> wrapper for the current dictionary.
    /// </summary>
    /// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
    /// <param name="dictionary">The dictionary to wrap.</param>
    /// <returns>An object that acts as a read-only wrapper around the current <see cref="IDictionary{TKey, TValue}"/>.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="dictionary"/> is null.</exception>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.asreadonly#system-collections-generic-collectionextensions-asreadonly-2(system-collections-generic-idictionary((-0-1)))")]
    public static ReadOnlyDictionary<TKey, TValue> AsReadOnly<TKey, TValue>(this IDictionary<TKey, TValue> target)
        where TKey : notnull =>
        new(target);


}
