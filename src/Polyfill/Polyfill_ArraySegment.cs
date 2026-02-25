#nullable enable

namespace Polyfills;

using System;
using System.Collections;
using System.Collections.Generic;

static partial class Polyfill
{
#if !NETCOREAPP2_0_OR_GREATER && !NETSTANDARD2_1

    /// <summary>
    /// Copies the contents of this instance into the specified destination array segment of the same type T.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.arraysegment-1.copyto?view=net-11.0#system-arraysegment-1-copyto(-0())
    public static void CopyTo<T>(this ArraySegment<T> target, ArraySegment<T> destination)
    {
        if (target.Count > destination.Count)
        {
            throw new ArgumentException("DestinationTooShort", nameof(destination));
        }

        if (target.Array == null)
        {
            throw new InvalidOperationException("InvalidOperation_NullArray");
        }

        Array.Copy(target.Array!, target.Offset, destination.Array, destination.Offset, target.Count);
    }

    /// <summary>
    /// Copies the contents of this instance into the specified destination array of the same type T.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.arraysegment-1.copyto?view=net-11.0#system-arraysegment-1-copyto(-0())
    public static void CopyTo<T>(this ArraySegment<T> target, T[] destination) =>
        CopyTo<T>(target, destination, 0);

    /// <summary>
    /// Copies the contents of this instance into the specified destination array of the same type T, starting at the specified destination index.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.arraysegment-1.copyto?view=net-11.0#system-arraysegment-1-copyto(-0()-system-int32)
    public static void CopyTo<T>(this ArraySegment<T> target, T[] destination, int destinationIndex)
    {
        if (target.Array == null)
        {
            throw new InvalidOperationException("InvalidOperation_NullArray");
        }

        Array.Copy(target.Array, target.Offset, destination, destinationIndex, target.Count);
    }

    /// <summary>
    /// Copies the contents of this instance into the specified destination array of the same type T, starting at the specified destination index.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.arraysegment-1.getenumerator?view=net-11.0
    public static ArraySegmentEnumerator<T> GetEnumerator<T>(this ArraySegment<T> target) =>
        new(target);

    /// <summary>
    /// Provides an enumerator for the elements of an <see cref="ArraySegment{T}"/>.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.arraysegment-1.enumerator?view=net-11.0
    public struct ArraySegmentEnumerator<T> : IEnumerator<T>
    {
        readonly T[]? _array;
        readonly int _start;
        readonly int _end;
        int _current;

        internal ArraySegmentEnumerator(ArraySegment<T> arraySegment)
        {
            _array = arraySegment.Array;
            _start = arraySegment.Offset;
            // cache Offset + Count, since it's a little slow
            _end = arraySegment.Offset + arraySegment.Count;
            _current = arraySegment.Offset - 1;
        }

        public bool MoveNext()
        {
            if (_current < _end)
            {
                _current++;
                return _current < _end;
            }

            return false;
        }

        public T Current
        {
            get
            {
                if (_current < _start)
                {
                    throw new InvalidOperationException("EnumNotStarted");
                }

                if (_current >= _end)
                {
                    throw new InvalidOperationException("EnumEnded");
                }

                return _array![_current];
            }
        }

        object? IEnumerator.Current => Current;

        void IEnumerator.Reset() =>
            _current = _start - 1;

        public void Dispose()
        {
        }
    }
#endif
}
