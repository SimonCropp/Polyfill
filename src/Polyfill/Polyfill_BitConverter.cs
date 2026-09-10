#if FeatureMemory

namespace Polyfills;

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

static partial class Polyfill
{

#if !AllowUnsafeBlocks && !NETSTANDARD2_1_OR_GREATER && !NETCOREAPP2_0_OR_GREATER

    [StructLayout(LayoutKind.Explicit)]
    struct BitConverter_Int32_Float
    {
        [FieldOffset(0)]
        public int _int;
        [FieldOffset(0)]
        public float _float;
    }

#endif

    extension(BitConverter)
    {

#if !NETSTANDARD2_1_OR_GREATER && !NETCOREAPP2_0_OR_GREATER

        /// <summary>
        /// Reinterprets the specified 32-bit integer as a single-precision floating-point value.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.bitconverter.int32bitstosingle?view=net-11.0
#if AllowUnsafeBlocks
        public static unsafe float Int32BitsToSingle(int value)
        {
            return Unsafe.AsRef<float>(Unsafe.AsPointer(ref value));
        }
#else
        public static float Int32BitsToSingle(int value)
        {
            var i = new BitConverter_Int32_Float();
            i._int = value;
            return i._float;
        }
#endif

#endif

#if !NET6_0_OR_GREATER

        /// <summary>
        /// Converts the specified 32-bit unsigned integer to a single-precision floating point number.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.bitconverter.uint32bitstosingle?view=net-11.0
        public static float UInt32BitsToSingle(uint value)
        {
            return BitConverter.Int32BitsToSingle((int)value);
        }

#endif

#if !NET6_0_OR_GREATER

        /// <summary>
        /// Converts the specified 64-bit unsigned integer to a double-precision floating point number.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.bitconverter.uint64bitstodouble?view=net-11.0
        public static double UInt64BitsToDouble(ulong value)
        {
            return BitConverter.Int64BitsToDouble((long)value);
        }

#endif

#if !NETSTANDARD2_1_OR_GREATER && !NETCOREAPP2_0_OR_GREATER

        /// <summary>
        /// Converts a single-precision floating-point value into an integer.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.bitconverter.singletoint32bits?view=net-11.0
#if AllowUnsafeBlocks
        public static unsafe int SingleToInt32Bits(float value)
        {
            return Unsafe.AsRef<int>(Unsafe.AsPointer(ref value));
        }
#else
        public static int SingleToInt32Bits(float value)
        {
            var i = new BitConverter_Int32_Float();
            i._float = value;
            return i._int;
        }
#endif

#endif

#if !NET6_0_OR_GREATER

        /// <summary>
        /// Converts the specified single-precision floating point number to a 32-bit unsigned integer.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.bitconverter.singletouint32bits?view=net-11.0
        public static uint SingleToUInt32Bits(float value)
        {
            return (uint)BitConverter.SingleToInt32Bits(value);
        }

#endif

#if !NET6_0_OR_GREATER

        /// <summary>
        /// Converts the specified double-precision floating point number to a 64-bit unsigned integer.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.bitconverter.doubletouint64bits?view=net-11.0
        public static ulong DoubleToUInt64Bits(double value)
        {
            return (ulong)BitConverter.DoubleToInt64Bits(value);
        }

#endif

#if NET7_0_OR_GREATER && !NET9_0_OR_GREATER

        /// <summary>
        /// Returns a 128-bit signed integer converted from sixteen bytes at a specified position in a byte array.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.bitconverter.toint128?view=net-11.0#system-bitconverter-toint128(system-byte()-system-int32)
        public static Int128 ToInt128(byte[] value, int startIndex)
        {
            GuardSixteenBytes(value, startIndex);
            return unchecked((Int128) ReadUInt128(value.AsSpan(startIndex)));
        }

        /// <summary>
        /// Converts a read-only byte span into a 128-bit signed integer.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.bitconverter.toint128?view=net-11.0#system-bitconverter-toint128(system-readonlyspan((system-byte)))
        public static Int128 ToInt128(ReadOnlySpan<byte> value)
        {
            GuardSixteenBytes(value);
            return unchecked((Int128) ReadUInt128(value));
        }

        /// <summary>
        /// Returns a 128-bit unsigned integer converted from sixteen bytes at a specified position in a byte array.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.bitconverter.touint128?view=net-11.0#system-bitconverter-touint128(system-byte()-system-int32)
        public static UInt128 ToUInt128(byte[] value, int startIndex)
        {
            GuardSixteenBytes(value, startIndex);
            return ReadUInt128(value.AsSpan(startIndex));
        }

        /// <summary>
        /// Converts a read-only byte span into a 128-bit unsigned integer.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.bitconverter.touint128?view=net-11.0#system-bitconverter-touint128(system-readonlyspan((system-byte)))
        public static UInt128 ToUInt128(ReadOnlySpan<byte> value)
        {
            GuardSixteenBytes(value);
            return ReadUInt128(value);
        }

        /// <summary>
        /// Returns the specified 128-bit signed integer value as an array of bytes.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.bitconverter.getbytes?view=net-11.0#system-bitconverter-getbytes(system-int128)
        public static byte[] GetBytes(Int128 value) =>
            BytesOf(unchecked((UInt128) value));

        /// <summary>
        /// Returns the specified 128-bit unsigned integer value as an array of bytes.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.bitconverter.getbytes?view=net-11.0#system-bitconverter-getbytes(system-uint128)
        public static byte[] GetBytes(UInt128 value) =>
            BytesOf(value);

        /// <summary>
        /// Converts a 128-bit signed integer into a span of bytes.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.bitconverter.trywritebytes?view=net-11.0#system-bitconverter-trywritebytes(system-span((system-byte))-system-int128)
        public static bool TryWriteBytes(Span<byte> destination, Int128 value) =>
            TryWriteUInt128(destination, unchecked((UInt128) value));

        /// <summary>
        /// Converts a 128-bit unsigned integer into a span of bytes.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.bitconverter.trywritebytes?view=net-11.0#system-bitconverter-trywritebytes(system-span((system-byte))-system-uint128)
        public static bool TryWriteBytes(Span<byte> destination, UInt128 value) =>
            TryWriteUInt128(destination, value);

#endif

    }

#if NET7_0_OR_GREATER && !NET9_0_OR_GREATER

    // BitConverter reads and writes in the platform's byte order, so each 64 bit half goes
    // through the existing ulong overloads and only their order depends on endianness.
    static UInt128 ReadUInt128(ReadOnlySpan<byte> value)
    {
        var first = BitConverter.ToUInt64(value);
        var second = BitConverter.ToUInt64(value.Slice(8));
        return BitConverter.IsLittleEndian ? new(second, first) : new(first, second);
    }

    static byte[] BytesOf(UInt128 value)
    {
        var bytes = new byte[16];
        WriteUInt128(bytes, value);
        return bytes;
    }

    static bool TryWriteUInt128(Span<byte> destination, UInt128 value)
    {
        if (destination.Length < 16)
        {
            return false;
        }

        WriteUInt128(destination, value);
        return true;
    }

    static void WriteUInt128(Span<byte> destination, UInt128 value)
    {
        ulong lower;
        ulong upper;
        unchecked
        {
            lower = (ulong) value;
            upper = (ulong) (value >> 64);
        }

        if (BitConverter.IsLittleEndian)
        {
            BitConverter.TryWriteBytes(destination, lower);
            BitConverter.TryWriteBytes(destination.Slice(8), upper);
        }
        else
        {
            BitConverter.TryWriteBytes(destination, upper);
            BitConverter.TryWriteBytes(destination.Slice(8), lower);
        }
    }

    // matches the validation the BCL uses for every other BitConverter array overload
    static void GuardSixteenBytes(byte[] value, int startIndex)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        if (startIndex < 0 ||
            startIndex >= value.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(startIndex), "Index was out of range. Must be non-negative and less than the size of the collection.");
        }

        if (startIndex > value.Length - 16)
        {
            throw new ArgumentException("The array starting from the specified index is not long enough to read a value of the specified type.", nameof(value));
        }
    }

    static void GuardSixteenBytes(ReadOnlySpan<byte> value)
    {
        if (value.Length < 16)
        {
            throw new ArgumentOutOfRangeException(nameof(value));
        }
    }

#endif

}

#endif
