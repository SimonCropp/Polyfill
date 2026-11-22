// Generic math (IBinaryInteger<T>, IBinaryFloatingPointIeee754<T>) arrived in net7, so these can't go lower.
#if NET7_0_OR_GREATER && !NET11_0_OR_GREATER

namespace Polyfills;

using System;
using System.Buffers;
using System.Numerics;
using System.Runtime.InteropServices;

// Ports of the BCL's Random.NextInteger and Random.NextBinaryFloat (release/11.0). They draw from the same virtual
// members (Next, NextInt64, NextBytes, NextSingle, NextDouble) in the same order, so a seeded or derived Random
// produces the same values as the BCL.
static partial class Polyfill
{
    const int randomStackallocThreshold = 256;

    /// <summary>
    /// Returns a non-negative random integer of type <typeparamref name="T"/>, in the inclusive range [0, <c>T.MaxValue</c>].
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.random.nextinteger?view=net-11.0#system-random-nextinteger-1
    public static T NextInteger<T>(this Random target)
        where T : IBinaryInteger<T>, IMinMaxValue<T>
    {
        if (T.MaxValue == T.Zero)
        {
            return T.Zero;
        }

        if (T.IsNegative(T.MaxValue))
        {
            ThrowRandomNegative(T.MaxValue, "T.MaxValue");
        }

        var bitLength = T.MaxValue.GetShortestBitLength();
        var byteCount = (bitLength + 7) >> 3;

        // mask the top byte to avoid negative values for signed types and to reduce the rejection rate
        var topBits = bitLength & 7;
        var topMask = topBits == 0 ? byte.MaxValue : (byte) ((1 << topBits) - 1);

        byte[]? rented = null;
        Span<byte> bytes = byteCount <= randomStackallocThreshold
            ? stackalloc byte[randomStackallocThreshold]
            : rented = ArrayPool<byte>.Shared.Rent(byteCount);
        bytes = bytes.Slice(0, byteCount);

        try
        {
            while (true)
            {
                target.NextBytes(bytes);
                bytes[^1] &= topMask;

                // custom IBinaryInteger<T> implementations can have unrepresentable values within the masked width
                var value = T.ReadLittleEndian(bytes, isUnsigned: true);
                if (value <= T.MaxValue)
                {
                    return value;
                }
            }
        }
        finally
        {
            if (rented is not null)
            {
                ArrayPool<byte>.Shared.Return(rented);
            }
        }
    }

    /// <summary>
    /// Returns a non-negative random integer that is less than the specified maximum. If <paramref name="maxValue"/> is zero, zero is returned.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.random.nextinteger?view=net-11.0#system-random-nextinteger-1(-0)
    public static T NextInteger<T>(this Random target, T maxValue)
        where T : IBinaryInteger<T>
    {
        if (T.IsNegative(maxValue))
        {
            ThrowRandomNegative(maxValue, nameof(maxValue));
        }

        return NextBinaryIntegerInRange(target, maxValue);
    }

    /// <summary>
    /// Returns a random integer that is within a specified range: greater than or equal to <paramref name="minValue"/>
    /// and less than <paramref name="maxValue"/>. If <paramref name="minValue"/> equals <paramref name="maxValue"/>,
    /// <paramref name="minValue"/> is returned.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.random.nextinteger?view=net-11.0#system-random-nextinteger-1(-0-0)
    public static T NextInteger<T>(this Random target, T minValue, T maxValue)
        where T : IBinaryInteger<T>
    {
        if (minValue > maxValue)
        {
            throw new ArgumentOutOfRangeException(nameof(minValue), $"'{nameof(minValue)}' cannot be greater than {nameof(maxValue)}.");
        }

        // for signed types the subtraction overflows when the range exceeds T.MaxValue, so fall back to full width
        var range = maxValue - minValue;
        return T.IsNegative(range) ?
            NextBinaryIntegerFullRange(target, minValue, maxValue) :
            NextBinaryIntegerInRange(target, range) + minValue;
    }

    /// <summary>
    /// Returns a random binary floating-point number of type <typeparamref name="T"/> that is greater than or equal to 0.0, and less than 1.0.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.random.nextbinaryfloat?view=net-11.0
    public static T NextBinaryFloat<T>(this Random target)
        where T : IBinaryFloatingPointIeee754<T>
    {
        if (typeof(T) == typeof(float))
        {
            return T.CreateTruncating(target.NextSingle());
        }

        if (typeof(T) == typeof(double))
        {
            return T.CreateTruncating(target.NextDouble());
        }

        if (typeof(T) == typeof(NFloat))
        {
            return nint.Size == 8
                ? T.CreateTruncating(target.NextDouble())
                : T.CreateTruncating(target.NextSingle());
        }

        // For Half and other low precision types, converting from NextSingle() can round up to 1.0, so build the
        // value from the type's significand bit length instead.
        var significandBitLength = T.Zero.GetSignificandBitLength();

        // 1L << significandBitLength would overflow from 63 bits, so build the significand in chunks of up to 62
        // random bits. No built-in type gets here (double has 53); this is for custom wider types.
        if (significandBitLength >= 63)
        {
            var value = T.Zero;
            var bitsRemaining = significandBitLength;
            while (bitsRemaining > 0)
            {
                var chunk = Math.Min(bitsRemaining, 62);
                var randomChunk = target.NextInt64(1L << chunk);
                value = T.ScaleB(value, chunk) + T.CreateTruncating(randomChunk);
                bitsRemaining -= chunk;
            }

            return T.ScaleB(value, -significandBitLength);
        }

        var randomBits = target.NextInt64(1L << significandBitLength);
        return T.ScaleB(T.CreateTruncating(randomBits), -significandBitLength);
    }

    // a random value in [0, maxExclusive), where maxExclusive is non-negative
    static T NextBinaryIntegerInRange<T>(Random target, T maxExclusive)
        where T : IBinaryInteger<T>
    {
        // fast paths through the existing Next and NextInt64; the JIT drops the dead branches
        if (typeof(T) == typeof(sbyte) ||
            typeof(T) == typeof(byte) ||
            typeof(T) == typeof(short) ||
            typeof(T) == typeof(ushort) ||
            typeof(T) == typeof(char) ||
            typeof(T) == typeof(int) ||
            (typeof(T) == typeof(nint) && nint.Size == 4))
        {
            return T.CreateTruncating(target.Next(int.CreateTruncating(maxExclusive)));
        }

        if (typeof(T) == typeof(uint) ||
            typeof(T) == typeof(nint) ||
            typeof(T) == typeof(long) ||
            (typeof(T) == typeof(nuint) && nint.Size == 4))
        {
            return T.CreateTruncating(target.NextInt64(long.CreateTruncating(maxExclusive)));
        }

        // when maxExclusive fits in a long, generate a long; the round trip check stops silent truncation
        if (typeof(T) == typeof(ulong) ||
            (typeof(T) == typeof(nuint) && nint.Size == 8) ||
            typeof(T) == typeof(Int128) ||
            typeof(T) == typeof(UInt128))
        {
            var maxExclusiveUlong = ulong.CreateTruncating(maxExclusive);
            if (maxExclusiveUlong <= long.MaxValue &&
                T.CreateTruncating(maxExclusiveUlong) == maxExclusive)
            {
                return T.CreateTruncating(target.NextInt64((long) maxExclusiveUlong));
            }
        }

        // generic fallback for large ulong, nuint, Int128, UInt128, BigInteger and the like
        if (maxExclusive == T.Zero)
        {
            return T.Zero;
        }

        var bitLength = maxExclusive.GetShortestBitLength();
        var byteCount = (int) (((uint) bitLength + 7) / 8);

        // mask the top byte to reduce the rejection rate
        var topBits = bitLength & 7;
        var topMask = topBits == 0 ? byte.MaxValue : (byte) ((1 << topBits) - 1);

        byte[]? rented = null;
        Span<byte> bytes = byteCount <= randomStackallocThreshold
            ? stackalloc byte[randomStackallocThreshold]
            : rented = ArrayPool<byte>.Shared.Rent(byteCount);
        bytes = bytes.Slice(0, byteCount);

        try
        {
            while (true)
            {
                target.NextBytes(bytes);
                bytes[^1] &= topMask;

                var value = T.ReadLittleEndian(bytes, isUnsigned: true);
                if (value < maxExclusive)
                {
                    return value;
                }
            }
        }
        finally
        {
            if (rented is not null)
            {
                ArrayPool<byte>.Shared.Return(rented);
            }
        }
    }

    // the range overflows a signed T, so generate across the full width of T and keep values in bounds;
    // the range is more than T.MaxValue, so over half are kept
    static T NextBinaryIntegerFullRange<T>(Random target, T minValue, T maxValue)
        where T : IBinaryInteger<T>
    {
        var byteCount = Math.Max(minValue.GetByteCount(), maxValue.GetByteCount());

        byte[]? rented = null;
        Span<byte> bytes = byteCount <= randomStackallocThreshold
            ? stackalloc byte[randomStackallocThreshold]
            : rented = ArrayPool<byte>.Shared.Rent(byteCount);
        bytes = bytes.Slice(0, byteCount);

        try
        {
            while (true)
            {
                target.NextBytes(bytes);

                var value = T.ReadLittleEndian(bytes, isUnsigned: false);
                if (value >= minValue && value < maxValue)
                {
                    return value;
                }
            }
        }
        finally
        {
            if (rented is not null)
            {
                ArrayPool<byte>.Shared.Return(rented);
            }
        }
    }

    // matches ArgumentOutOfRangeException.ThrowIfNegative, which is net8+
    static void ThrowRandomNegative<T>(T value, string paramName) =>
        throw new ArgumentOutOfRangeException(paramName, value, $"{paramName} ('{value}') must be a non-negative value.");
}

#endif
