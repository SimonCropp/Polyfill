#if !NET9_0_OR_GREATER

namespace Polyfills;

using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Provides a type that may be used to perform operations on a <see cref="HashSet{T}"/>
/// using a <typeparamref name="TAlternate"/> instead of a <typeparamref name="T"/>.
/// </summary>
/// <remarks>
/// This polyfill performs O(n) lookups by linearly scanning the set and invoking
/// <see cref="IAlternateEqualityComparer{TAlternate,T}.Equals"/> on each element. The native .NET 9+
/// counterpart is O(1) by using the set's internal hash buckets.
/// </remarks>
//Link: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1.alternatelookup-1?view=net-11.0
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
#if PolyPublic
public
#endif
readonly struct HashSetAlternateLookup<T, TAlternate>
{
    readonly HashSet<T> set;
    readonly IAlternateEqualityComparer<TAlternate, T> comparer;

    internal HashSetAlternateLookup(
        HashSet<T> set,
        IAlternateEqualityComparer<TAlternate, T> comparer)
    {
        this.set = set;
        this.comparer = comparer;
    }

    /// <summary>Gets the <see cref="HashSet{T}"/> against which this instance performs operations.</summary>
    public HashSet<T> Set => set;

    /// <summary>Adds an item that is created from the specified alternate value, if no equal value already exists.</summary>
    public bool Add(TAlternate item)
    {
        if (TryFindValue(item, out _))
        {
            return false;
        }

        set.Add(comparer.Create(item));
        return true;
    }

    /// <summary>Determines whether the set contains a value equal to the specified alternate value.</summary>
    public bool Contains(TAlternate item) => TryFindValue(item, out _);

    /// <summary>Removes the value equal to the specified alternate value from the set.</summary>
    public bool Remove(TAlternate item)
    {
        if (TryFindValue(item, out var actual))
        {
            set.Remove(actual);
            return true;
        }

        return false;
    }

    /// <summary>Searches the set for the value equal to the specified alternate value and returns it.</summary>
    public bool TryGetValue(TAlternate equalValue, [MaybeNullWhen(false)] out T actualValue) =>
        TryFindValue(equalValue, out actualValue);

    bool TryFindValue(TAlternate alternate, [MaybeNullWhen(false)] out T actualValue)
    {
        foreach (var existing in set)
        {
            if (comparer.Equals(alternate, existing))
            {
                actualValue = existing;
                return true;
            }
        }

        actualValue = default;
        return false;
    }
}

#endif
