#if !NET10_0_OR_GREATER

namespace Polyfills;

using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

static partial class Polyfill
{
    /// <summary>
    /// Removes a key and its value from the table, and returns the removed value.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.conditionalweaktable-2.remove?view=net-11.0#system-runtime-compilerservices-conditionalweaktable-2-remove(-0-1@)
    //Note: Lookup and removal are not performed under the table lock, so the operation is not atomic with regard to concurrent mutations.
    public static bool Remove<TKey, TValue>(
        this ConditionalWeakTable<TKey, TValue> target,
        TKey key,
        [MaybeNullWhen(false)] out TValue value)
        where TKey : class
        where TValue : class
    {
        if (target.TryGetValue(key, out value) &&
            target.Remove(key))
        {
            return true;
        }

        value = null;
        return false;
    }
}
#endif
