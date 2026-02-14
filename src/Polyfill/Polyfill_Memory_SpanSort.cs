#if FeatureMemory && !NET5_0_OR_GREATER

namespace Polyfills;

using System;
using System.Collections.Generic;
using System.Buffers;

static partial class Polyfill
{
    /// <summary>
    /// Sorts the elements in the entire <see cref="Span{T}"/> using the <see cref="IComparable{T}"/> implementation of each element of the <see cref="Span{T}"/>.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.sort?view=net-11.0#system-memoryextensions-sort-1(system-span((-0)))
    public static void Sort<T>(this Span<T> source)
        where T : IComparable<T>
        => Sort(source, (x, y) => x.CompareTo(y));

    /// <summary>
    /// Sorts the elements in the entire <see cref="Span{T}"/> using the specified <see cref="Comparison{T}"/>.
    /// </summary>
    /// Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.sort?view=net-11.0#system-memoryextensions-sort-1(system-span((-0))-system-comparison((-0)))
    public static void Sort<T>(this Span<T> source, Comparison<T> comparison)
    {
        if((Comparison<T>?)comparison is null)
            throw new ArgumentNullException(nameof(comparison));

        var array = ArrayPool<T>.Shared.Rent(source.Length);

        try
        {
            source.CopyTo(array);
            Array.Sort(array, 0, source.Length, Comparer<T>.Create(comparison));

            array.AsSpan(0, source.Length).CopyTo(source);
        }
        finally
        {
            ArrayPool<T>.Shared.Return(array);
        }
    }

    /// <summary>
    /// Sorts a pair of spans (one containing the keys and the other containing the corresponding items) based on the keys in the first <see cref="Span{T}"/> using the <see cref=" IComparable{T}"/> implementation of each key.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.sort?view=net-11.0#system-memoryextensions-sort-2(system-span((-0))-system-span((-1)))
    public static void Sort<TKey, TValue>(this Span<TKey> keys, Span<TValue> values)
        => Sort(keys, values, Comparer<TKey>.Default);


    /// <summary>
    /// Sorts a pair of spans (one containing the keys and the other containing the corresponding items) based on the keys in the first <see cref="Span{T}"/>  using the specified comparer.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.sort?view=net-11.0#system-memoryextensions-sort-3(system-span((-0))-system-span((-1))-2)
    public static void Sort<TKey, TValue, TComparer>(this Span<TKey> keys, Span<TValue> values, TComparer comparer)
        where TComparer : IComparer<TKey>
    {
        comparer = comparer is not null ? comparer : throw new ArgumentNullException(nameof(comparer));

        if(keys.Length != values.Length)
            throw new ArgumentException();

        var keysArray = ArrayPool<TKey>.Shared.Rent(keys.Length);
        var valsArray = ArrayPool<TValue>.Shared.Rent(values.Length);

        try
        {
            keys.CopyTo(keysArray);
            values.CopyTo(valsArray);

            Array.Sort(keysArray, valsArray, 0, keys.Length, comparer);

            keysArray.AsSpan(0, keys.Length).CopyTo(keys);
            valsArray.AsSpan(0, values.Length).CopyTo(values);
        }
        finally
        {
            ArrayPool<TKey>.Shared.Return(keysArray);
            ArrayPool<TValue>.Shared.Return(valsArray);
        }
    }

    /// <summary>
    /// Sorts a pair of spans (one containing the keys and the other containing the corresponding items) based on the keys in the first <see cref="Span{T}"/> using the specified comparison.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.sort?view=net-11.0#system-memoryextensions-sort-2(system-span((-0))-system-span((-1))-system-comparison((-0)))
    public static void Sort<TKey, TValue>(this Span<TKey> keys, Span<TValue> items, Comparison<TKey> comparison)
        => Sort(keys, items, new ComparerWrapper<TKey>(comparison));

    class ComparerWrapper<T> : IComparer<T>
    {
        readonly Comparison<T> comparison;

        internal ComparerWrapper(Comparison<T> comparison)
        {
            if ((Comparison<T>?)comparison is null)
                throw new ArgumentNullException(nameof(comparison));

            this.comparison = comparison;
        }

        public int Compare(T? x, T? y)
        {
            if (x is null || y is null)
                return 0;

            return comparison((T)x, (T)y);
        }
    }
}

#endif
