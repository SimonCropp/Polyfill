#pragma warning disable

#if !NET5_0_OR_GREATER && FeatureMemory

namespace Polyfills;

using System;
using System.Buffers.Binary;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

static partial class Polyfill
{
    extension(BinaryPrimitives)
    {
        /// <summary>
        /// Reads a <see cref="double"/> from the beginning of a read-only span of bytes, as big endian.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.binary.binaryprimitives.readdoublebigendian?view=net-11.0
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ReadDoubleBigEndian(ReadOnlySpan<byte> source) =>
            !BitConverter.IsLittleEndian ?
                MemoryMarshal.Read<double>(source) :
                BitConverter.Int64BitsToDouble(BinaryPrimitives.ReverseEndianness(MemoryMarshal.Read<long>(source)));

        /// <summary>
        /// Reads a <see cref="double"/> from the beginning of a read-only span of bytes, as little endian.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.binary.binaryprimitives.readdoublelittleendian?view=net-11.0
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ReadDoubleLittleEndian(ReadOnlySpan<byte> source) =>
            BitConverter.IsLittleEndian ?
                MemoryMarshal.Read<double>(source) :
                BitConverter.Int64BitsToDouble(BinaryPrimitives.ReverseEndianness(MemoryMarshal.Read<long>(source)));

        /// <summary>
        /// Reads a <see cref="float"/> from the beginning of a read-only span of bytes, as big endian.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.binary.binaryprimitives.readsinglebigendian?view=net-11.0
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float ReadSingleBigEndian(ReadOnlySpan<byte> source) =>
            !BitConverter.IsLittleEndian ?
                MemoryMarshal.Read<float>(source) :
                BitConverter.Int32BitsToSingle(BinaryPrimitives.ReverseEndianness(MemoryMarshal.Read<int>(source)));

        /// <summary>
        /// Reads a <see cref="float"/> from the beginning of a read-only span of bytes, as little endian.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.binary.binaryprimitives.readsinglelittleendian?view=net-11.0
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float ReadSingleLittleEndian(ReadOnlySpan<byte> source) =>
            BitConverter.IsLittleEndian ?
                MemoryMarshal.Read<float>(source) :
                BitConverter.Int32BitsToSingle(BinaryPrimitives.ReverseEndianness(MemoryMarshal.Read<int>(source)));

        /// <summary>
        /// Reads a <see cref="double"/> from the beginning of a read-only span of bytes, as big endian.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.binary.binaryprimitives.tryreaddoublebigendian?view=net-11.0
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryReadDoubleBigEndian(ReadOnlySpan<byte> source, out double value)
        {
            if (source.Length < sizeof(double))
            {
                value = default;
                return false;
            }

            value = BinaryPrimitives.ReadDoubleBigEndian(source);
            return true;
        }

        /// <summary>
        /// Reads a <see cref="double"/> from the beginning of a read-only span of bytes, as little endian.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.binary.binaryprimitives.tryreaddoublelittleendian?view=net-11.0
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryReadDoubleLittleEndian(ReadOnlySpan<byte> source, out double value)
        {
            if (source.Length < sizeof(double))
            {
                value = default;
                return false;
            }

            value = BinaryPrimitives.ReadDoubleLittleEndian(source);
            return true;
        }

        /// <summary>
        /// Reads a <see cref="float"/> from the beginning of a read-only span of bytes, as big endian.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.binary.binaryprimitives.tryreadsinglebigendian?view=net-11.0
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryReadSingleBigEndian(ReadOnlySpan<byte> source, out float value)
        {
            if (source.Length < sizeof(float))
            {
                value = default;
                return false;
            }

            value = BinaryPrimitives.ReadSingleBigEndian(source);
            return true;
        }

        /// <summary>
        /// Reads a <see cref="float"/> from the beginning of a read-only span of bytes, as little endian.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.binary.binaryprimitives.tryreadsinglelittleendian?view=net-11.0
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryReadSingleLittleEndian(ReadOnlySpan<byte> source, out float value)
        {
            if (source.Length < sizeof(float))
            {
                value = default;
                return false;
            }

            value = BinaryPrimitives.ReadSingleLittleEndian(source);
            return true;
        }

        /// <summary>
        /// Writes a <see cref="double"/> into a span of bytes, as big endian.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.binary.binaryprimitives.writedoublebigendian?view=net-11.0
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteDoubleBigEndian(Span<byte> destination, double value)
        {
            if (!BitConverter.IsLittleEndian)
            {
                MemoryMarshal.Write(destination, ref value);
            }
            else
            {
                var tmp = BinaryPrimitives.ReverseEndianness(BitConverter.DoubleToInt64Bits(value));
                MemoryMarshal.Write(destination, ref tmp);
            }
        }

        /// <summary>
        /// Writes a <see cref="double"/> into a span of bytes, as little endian.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.binary.binaryprimitives.writedoublelittleendian?view=net-11.0
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteDoubleLittleEndian(Span<byte> destination, double value)
        {
            if (BitConverter.IsLittleEndian)
            {
                MemoryMarshal.Write(destination, ref value);
            }
            else
            {
                var tmp = BinaryPrimitives.ReverseEndianness(BitConverter.DoubleToInt64Bits(value));
                MemoryMarshal.Write(destination, ref tmp);
            }
        }

        /// <summary>
        /// Writes a <see cref="float"/> into a span of bytes, as big endian.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.binary.binaryprimitives.writesinglebigendian?view=net-11.0
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteSingleBigEndian(Span<byte> destination, float value)
        {
            if (!BitConverter.IsLittleEndian)
            {
                MemoryMarshal.Write(destination, ref value);
            }
            else
            {
                var tmp = BinaryPrimitives.ReverseEndianness(BitConverter.SingleToInt32Bits(value));
                MemoryMarshal.Write(destination, ref tmp);
            }
        }

        /// <summary>
        /// Writes a <see cref="float"/> into a span of bytes, as little endian.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.binary.binaryprimitives.writesinglelittleendian?view=net-11.0
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteSingleLittleEndian(Span<byte> destination, float value)
        {
            if (BitConverter.IsLittleEndian)
            {
                MemoryMarshal.Write(destination, ref value);
            }
            else
            {
                var tmp = BinaryPrimitives.ReverseEndianness(BitConverter.SingleToInt32Bits(value));
                MemoryMarshal.Write(destination, ref tmp);
            }
        }

        /// <summary>
        /// Writes a <see cref="double"/> into a span of bytes, as big endian.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.binary.binaryprimitives.trywritedoublebigendian?view=net-11.0
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWriteDoubleBigEndian(Span<byte> destination, double value)
        {
            if (destination.Length < sizeof(double))
            {
                return false;
            }

            BinaryPrimitives.WriteDoubleBigEndian(destination, value);
            return true;
        }

        /// <summary>
        /// Writes a <see cref="double"/> into a span of bytes, as little endian.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.binary.binaryprimitives.trywritedoublelittleendian?view=net-11.0
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWriteDoubleLittleEndian(Span<byte> destination, double value)
        {
            if (destination.Length < sizeof(double))
            {
                return false;
            }

            BinaryPrimitives.WriteDoubleLittleEndian(destination, value);
            return true;
        }

        /// <summary>
        /// Writes a <see cref="float"/> into a span of bytes, as big endian.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.binary.binaryprimitives.trywritesinglebigendian?view=net-11.0
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWriteSingleBigEndian(Span<byte> destination, float value)
        {
            if (destination.Length < sizeof(float))
            {
                return false;
            }

            BinaryPrimitives.WriteSingleBigEndian(destination, value);
            return true;
        }

        /// <summary>
        /// Writes a <see cref="float"/> into a span of bytes, as little endian.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.binary.binaryprimitives.trywritesinglelittleendian?view=net-11.0
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWriteSingleLittleEndian(Span<byte> destination, float value)
        {
            if (destination.Length < sizeof(float))
            {
                return false;
            }

            BinaryPrimitives.WriteSingleLittleEndian(destination, value);
            return true;
        }
    }
}

#endif
