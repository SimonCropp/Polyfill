namespace Polyfills;

using System;
using System.Collections.Generic;

static partial class Polyfill
{
#if !NET8_0_OR_GREATER
    class DelegateEqualityComparer<T>(Func<T?, T?, bool> equals, Func<T, int>? getHashCode)
        : EqualityComparer<T>
    {
        public override bool Equals(T? x, T? y) => equals(x, y);

        public override int GetHashCode(T obj) =>
            getHashCode is not null ? getHashCode(obj) : throw new NotSupportedException();
    }

    extension<T>(EqualityComparer<T>)
    {
        /// <summary>
        /// Creates an <see cref="EqualityComparer{T}"/> by using the specified delegates as the implementation of the comparer's <see cref="EqualityComparer{T}.Equals(T,T)"/> and <see cref="EqualityComparer{T}.GetHashCode(T)"/> methods.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.equalitycomparer-1.create?view=net-11.0
        public static EqualityComparer<T> Create(
            Func<T?, T?, bool> equals,
            Func<T, int>? getHashCode = null)
        {
            if (equals is null)
            {
                throw new ArgumentNullException(nameof(equals));
            }

            return new DelegateEqualityComparer<T>(equals, getHashCode);
        }
    }
#endif

#if !NET11_0_OR_GREATER
    class KeySelectorEqualityComparer<T, TKey>(Func<T?, TKey?> keySelector, IEqualityComparer<TKey>? keyComparer)
        : EqualityComparer<T>
    {
        readonly IEqualityComparer<TKey> comparer = keyComparer ?? EqualityComparer<TKey>.Default;

        public override bool Equals(T? x, T? y) =>
            comparer.Equals(keySelector(x)!, keySelector(y)!);

        public override int GetHashCode(T obj)
        {
            var key = keySelector(obj);
            return key is null ? 0 : comparer.GetHashCode(key);
        }
    }

    extension<T>(EqualityComparer<T>)
    {
        /// <summary>
        /// Creates an <see cref="EqualityComparer{T}"/> that determines equality by projecting each value through <paramref name="keySelector"/> and comparing the resulting keys.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.equalitycomparer-1.create?view=net-11.0#system-collections-generic-equalitycomparer-1-create-1(system-func((-0-0))-system-collections-generic-iequalitycomparer((-0)))
        public static EqualityComparer<T> Create<TKey>(
            Func<T?, TKey?> keySelector,
            IEqualityComparer<TKey>? keyComparer = null)
        {
            if (keySelector is null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }

            return new KeySelectorEqualityComparer<T, TKey>(keySelector, keyComparer);
        }
    }
#endif
}
