// <auto-generated />
#pragma warning disable

#if NET46X || NET47  || NET471 || NETSTANDARD2_0

namespace Polyfills;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

static partial class Polyfill
{
    /// <summary>
    ///  Searches the set for a given value and returns the equal value it finds, if any.
    /// </summary>
    /// <param name="equalValue">The value to search for.</param>
    /// <param name="actualValue">The value from the set that the search found, or the default value of T when the search yielded no match.</param>
    /// <returns>A value indicating whether the search was successful.</returns>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1.trygetvalue
    public static bool TryGetValue<T>(
        this HashSet<T> target,
        T equalValue,
        [MaybeNullWhen(false)] out T actualValue)
    {
        var comparer = target.Comparer;
        var hashCode = comparer.GetHashCode(equalValue);
        foreach (var item in target)
        {
            if (comparer.GetHashCode(item) == hashCode &&
                comparer.Equals(item, equalValue))
            {
                actualValue = item;
                return true;
            }
        }

        actualValue = default;
        return false;
    }
}
#endif