#if !NET7_0_OR_GREATER

namespace Polyfills;

using System.Collections.Generic;

static partial class Polyfill
{
    /// <summary>
    /// Gets the key corresponding to the specified index.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.sortedlist-2.getkeyatindex?view=net-11.0
    public static TKey GetKeyAtIndex<TKey, TValue>(
        this SortedList<TKey, TValue> target, int index) =>
        target.Keys[index];

    /// <summary>
    /// Gets the value corresponding to the specified index.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.sortedlist-2.getvalueatindex?view=net-11.0
    public static TValue GetValueAtIndex<TKey, TValue>(
        this SortedList<TKey, TValue> target, int index) =>
        target.Values[index];
}
#endif
