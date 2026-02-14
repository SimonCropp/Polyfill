#pragma warning disable

#if !NET11_0_OR_GREATER && FeatureUnsafe && FeatureMemory

namespace Polyfills;

using System.Runtime.CompilerServices;
using System.Threading;
using System;
using System.Runtime.InteropServices;

static partial class Polyfill
{
    extension(Interlocked)
    {
        /// <summary>
        /// Bitwise "ands" two values and replaces the first value with the result, as an atomic operation.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.threading.interlocked.and?view=net-11.0
        public static T And<T>(ref T location1, T value)
            where T : struct
        {
            if (Unsafe.SizeOf<T>() == sizeof(int))
            {
                ref var loc = ref Unsafe.As<T, int>(ref location1);
                var val = Unsafe.As<T, int>(ref value);
                var result = InterlockedAnd(ref loc, val);
                return Unsafe.As<int, T>(ref result);
            }

            if (Unsafe.SizeOf<T>() == sizeof(long))
            {
                ref var loc = ref Unsafe.As<T, long>(ref location1);
                var val = Unsafe.As<T, long>(ref value);
                var result = InterlockedAnd(ref loc, val);
                return Unsafe.As<long, T>(ref result);
            }

            throw new System.NotSupportedException();
        }

        /// <summary>
        /// Bitwise "ors" two values and replaces the first value with the result, as an atomic operation.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.threading.interlocked.or?view=net-11.0
        public static T Or<T>(ref T location1, T value)
            where T : struct
        {
            if (Unsafe.SizeOf<T>() == sizeof(int))
            {
                ref var loc = ref Unsafe.As<T, int>(ref location1);
                var val = Unsafe.As<T, int>(ref value);
                var result = InterlockedOr(ref loc, val);
                return Unsafe.As<int, T>(ref result);
            }

            if (Unsafe.SizeOf<T>() == sizeof(long))
            {
                ref var loc = ref Unsafe.As<T, long>(ref location1);
                var val = Unsafe.As<T, long>(ref value);
                var result = InterlockedOr(ref loc, val);
                return Unsafe.As<long, T>(ref result);
            }

            throw new System.NotSupportedException();
        }
    }

    static int InterlockedAnd(ref int location, int value)
    {
        var current = location;
        while (true)
        {
            var newValue = current & value;
            var oldValue = Interlocked.CompareExchange(ref location, newValue, current);
            if (oldValue == current)
            {
                return oldValue;
            }

            current = oldValue;
        }
    }

    static long InterlockedAnd(ref long location, long value)
    {
        var current = location;
        while (true)
        {
            var newValue = current & value;
            var oldValue = Interlocked.CompareExchange(ref location, newValue, current);
            if (oldValue == current)
            {
                return oldValue;
            }

            current = oldValue;
        }
    }

    static int InterlockedOr(ref int location, int value)
    {
        var current = location;
        while (true)
        {
            var newValue = current | value;
            var oldValue = Interlocked.CompareExchange(ref location, newValue, current);
            if (oldValue == current)
            {
                return oldValue;
            }

            current = oldValue;
        }
    }

    static long InterlockedOr(ref long location, long value)
    {
        var current = location;
        while (true)
        {
            var newValue = current | value;
            var oldValue = Interlocked.CompareExchange(ref location, newValue, current);
            if (oldValue == current)
            {
                return oldValue;
            }

            current = oldValue;
        }
    }
}

#endif
