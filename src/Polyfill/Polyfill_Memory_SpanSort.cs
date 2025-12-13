//#if FeatureMemory

using System.Buffers;

namespace Polyfill;

static partial class Polyfill
{
#if !NET5_0_OR_GREATER
    public static void Sort<T>(this ref Span<T> source)
        where T : IComparable<T>
        => Sort(ref source, (x, y) => x.CompareTo(y));

    public static void Sort<T>(this ref Span<T> source, Comparison<T> comparison)
    {
        ArgumentNullException.ThrowIfNull(comparison);
        var array = ArrayPool<T>.Shared.Rent(source.Length);

        try
        {
            source.CopyTo(array);

            Array.Sort(array, comparison);

            array.AsSpan(0, source.Length).CopyTo(source);
        }
        finally
        {
            ArrayPool<T>.Shared.Return(array);
        }
    }

    public static void Sort<TKey, TValue>(this ref Span<TKey> keys, ref Span<TValue> values)
        => Sort(ref keys, ref values, Comparer<TKey>.Default);

    public static void Sort<TKey, TValue, TComparer>(this ref Span<TKey> keys, ref Span<TValue> values, TComparer comparer)
        where TComparer : IComparer<TKey>
    {
        ArgumentNullException.ThrowIfNull(comparer);

        if(keys.Length != values.Length)
            throw new ArgumentException();

        var keysArray = ArrayPool<TKey>.Shared.Rent(keys.Length);
        var valsArray = ArrayPool<TValue>.Shared.Rent(values.Length);

        try
        {
            keys.CopyTo(keysArray);
            values.CopyTo(valsArray);

            Array.Sort(keysArray, valsArray, comparer);

            keysArray.AsSpan(0, keys.Length).CopyTo(keys);
            valsArray.AsSpan(0, values.Length).CopyTo(values);
        }
        finally
        {
            ArrayPool<TKey>.Shared.Return(keysArray);
            ArrayPool<TValue>.Shared.Return(valsArray);
        }
    }

    public static void Sort<TKey, TValue>(this ref Span<TKey> keys, ref Span<TValue> values, Comparison<TKey> comparison)
        => Sort(ref keys, ref values, new ComparerWrapper<TKey>(comparison));

    private class ComparerWrapper<T> : IComparer<T>
    {
        readonly Comparison<T> comparison;

        internal ComparerWrapper(Comparison<T> comparison)
        {
            ArgumentNullException.ThrowIfNull(comparison);
            this.comparison = comparison;
        }

        public int Compare(T x, T y) =>
            comparison(x, y);
    }
#endif
}

//#endif