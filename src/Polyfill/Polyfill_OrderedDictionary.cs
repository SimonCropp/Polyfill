#if NET9_0

namespace Polyfills;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

static partial class Polyfill
{
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ordereddictionary-2.tryadd?view=net-11.0#system-collections-generic-ordereddictionary-2-tryadd(-0-1-system-int32@)
    public static bool TryAdd<TKey, TValue>(this OrderedDictionary<TKey, TValue> target, TKey key, TValue value, out int index)
        where TKey : notnull
    {
        var result = target.TryAdd(key, value);
        index = target.IndexOf(key);
        return result;
    }

    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ordereddictionary-2.trygetvalue?view=net-11.0#system-collections-generic-ordereddictionary-2-trygetvalue(-0-1@-system-int32@)
    public static bool TryGetValue<TKey, TValue>(this OrderedDictionary<TKey, TValue> target, TKey key, [MaybeNullWhen(false)] out TValue value, out int index)
        where TKey : notnull
    {
        index = target.IndexOf(key);
        return target.TryGetValue(key, out value);
    }
}
#endif
