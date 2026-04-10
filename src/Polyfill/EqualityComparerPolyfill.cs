#if !NET8_0_OR_GREATER

namespace Polyfills;

using System;
using System.Collections.Generic;

static partial class Polyfill
{
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
            Func<T, int>? getHashCode = null) =>
            new DelegateEqualityComparer<T>(equals, getHashCode);
    }
}

#endif
