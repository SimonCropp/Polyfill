
#pragma warning disable

namespace Polyfills;
using System;
using System.Collections.Concurrent;
using Link = System.ComponentModel.DescriptionAttribute;

static partial class Polyfill
{


    /// <summary>
    /// Adds a key/value pair to the <see cref="ConcurrentDictionary{TKey,TValue}"/>
    /// if the key does not already exist.
    /// </summary>
    /// <param name="key">The key of the element to add.</param>
    /// <param name="valueFactory">The function used to generate a value for the key</param>
    /// <param name="factoryArgument">An argument value to pass into <paramref name="valueFactory"/>.</param>
    /// <exception cref="ArgumentNullException"><paramref name="key"/> is a null reference
    /// (Nothing in Visual Basic).</exception>
    /// <exception cref="ArgumentNullException"><paramref name="valueFactory"/> is a null reference
    /// (Nothing in Visual Basic).</exception>
    /// <exception cref="OverflowException">The dictionary contains too many
    /// elements.</exception>
    /// <returns>The value for the key.  This will be either the existing value for the key if the
    /// key is already in the dictionary, or the new value for the key as returned by valueFactory
    /// if the key was not in the dictionary.</returns>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.collections.concurrent.concurrentdictionary-2.getoradd#system-collections-concurrent-concurrentdictionary-2-getoradd-1(-0-system-func((-0-0-1))-0)")]
    public static TValue GetOrAdd<TKey,TValue, TArg>(this ConcurrentDictionary<TKey,TValue> target, TKey key, Func<TKey, TArg, TValue> valueFactory, TArg factoryArgument)
        where TKey : notnull =>
        target.GetOrAdd(key, _ => valueFactory(_, factoryArgument));
}