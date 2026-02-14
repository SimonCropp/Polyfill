#if !NETCOREAPP && !NETSTANDARD2_1_OR_GREATER && !NET472_OR_GREATER

namespace Polyfills;

using System;
using System.Collections.Concurrent;

static partial class Polyfill
{
    /// <summary>
    /// Adds a key/value pair to the <see cref="ConcurrentDictionary{TKey,TValue}"/>
    /// if the key does not already exist.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.collections.concurrent.concurrentdictionary-2.getoradd?view=net-11.0#system-collections-concurrent-concurrentdictionary-2-getoradd-1(-0-system-func((-0-0-1))-0)
    public static TValue GetOrAdd<TKey, TValue, TArg>(this ConcurrentDictionary<TKey, TValue> target, TKey key, Func<TKey, TArg, TValue> valueFactory, TArg factoryArgument)
        where TKey : notnull
    {
        while (true)
        {
            TValue value;
            if (target.TryGetValue(key, out value))
            {
                return value;
            }

            value = valueFactory(key, factoryArgument);
            if (target.TryAdd(key, value))
            {
                return value;
            }
        }
    }
}
#endif
