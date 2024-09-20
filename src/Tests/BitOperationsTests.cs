// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Numerics;

[TestFixture]
public static class BitOperationsTests
{
    [Theory]
    [TestCase(0, false)]
    [TestCase(0b1, true)]
    [TestCase(0b10, true)]
    [TestCase(0b100, true)]
    [TestCase(0b1000, true)]
    [TestCase(0b10000, true)]
    [TestCase(0b100000, true)]
    [TestCase(0b1000000, true)]
    [TestCase(-0b1000000, false)]
    [TestCase(0b1000001, false)]
    [TestCase(0b1010001, false)]
    [TestCase(0b1111111, false)]
    [TestCase(-1, false)]
    [TestCase(int.MaxValue, false)]
    [TestCase(int.MinValue, false)]
    public static void BitOps_IsPow2_int(int n, bool expected)
    {
        var actual = BitOperations.IsPow2(n);
        Assert.AreEqual(expected, actual);
    }

    [Theory]
    [TestCase(0u, false)]
    [TestCase(0b1u, true)]
    [TestCase(0b10u, true)]
    [TestCase(0b100u, true)]
    [TestCase(0b1000u, true)]
    [TestCase(0b10000u, true)]
    [TestCase(0b100000u, true)]
    [TestCase(0b1000000u, true)]
    [TestCase(0b1000001u, false)]
    [TestCase(0b1010001u, false)]
    [TestCase(0b1111111u, false)]
    [TestCase(uint.MaxValue, false)]
    [TestCase(unchecked((uint)int.MinValue), true)]
    public static void BitOps_IsPow2_uint(uint n, bool expected)
    {
        var actual = BitOperations.IsPow2(n);
        Assert.AreEqual(expected, actual);
    }

    [Theory]
    [TestCase(0L, false)]
    [TestCase(0b1L, true)]
    [TestCase(0b10L, true)]
    [TestCase(0b100L, true)]
    [TestCase(0b1000L, true)]
    [TestCase(0b10000L, true)]
    [TestCase(0b100000L, true)]
    [TestCase(0b1000000L, true)]
    [TestCase(-0b1000000L, false)]
    [TestCase(0b1000001L, false)]
    [TestCase(0b1010001L, false)]
    [TestCase(0b1111111L, false)]
    [TestCase(-1L, false)]
    [TestCase(long.MaxValue, false)]
    [TestCase(long.MinValue, false)]
    public static void BitOps_IsPow2_long(long n, bool expected)
    {
        var actual = BitOperations.IsPow2(n);
        Assert.AreEqual(expected, actual);
    }

    [Theory]
    [TestCase(0ul, false)]
    [TestCase(0b1ul, true)]
    [TestCase(0b10ul, true)]
    [TestCase(0b100ul, true)]
    [TestCase(0b1000ul, true)]
    [TestCase(0b10000ul, true)]
    [TestCase(0b100000ul, true)]
    [TestCase(0b1000000ul, true)]
    [TestCase(0b1000001ul, false)]
    [TestCase(0b1010001ul, false)]
    [TestCase(0b1111111ul, false)]
    [TestCase(ulong.MaxValue, false)]
    [TestCase(unchecked((ulong)long.MinValue), true)]
    public static void BitOps_IsPow2_ulong(ulong n, bool expected)
    {
        var actual = BitOperations.IsPow2(n);
        Assert.AreEqual(expected, actual);
    }

    [TestCase(0, false)]
    [TestCase(0b1, true)]
    [TestCase(0b10, true)]
    [TestCase(0b100, true)]
    [TestCase(0b1000, true)]
    [TestCase(0b10000, true)]
    [TestCase(0b100000, true)]
    [TestCase(0b1000000, true)]
    [TestCase(-0b1000000, false)]
    [TestCase(0b1000001, false)]
    [TestCase(0b1010001, false)]
    [TestCase(0b1111111, false)]
    [TestCase(-1, false)]
    [TestCase(int.MaxValue, false)]
    [TestCase(int.MinValue, false)]
    public static void BitOps_IsPow2_nint_32(int n, bool expected)
    {
        var actual = BitOperations.IsPow2((nint) n);
        Assert.AreEqual(expected, actual);
    }

    [TestCase(0, false)]
    [TestCase(0b1, true)]
    [TestCase(0b10, true)]
    [TestCase(0b100, true)]
    [TestCase(0b1000, true)]
    [TestCase(0b10000, true)]
    [TestCase(0b100000, true)]
    [TestCase(0b1000000, true)]
    [TestCase(-0b1000000, false)]
    [TestCase(0b1000001, false)]
    [TestCase(0b1010001, false)]
    [TestCase(0b1111111, false)]
    [TestCase(-1, false)]
    [TestCase(long.MaxValue, false)]
    [TestCase(long.MinValue, false)]
    public static void BitOps_IsPow2_nint_64(long n, bool expected)
    {
        var actual = BitOperations.IsPow2((nint) n);
        Assert.AreEqual(expected, actual);
    }

    [TestCase(0u, false)]
    [TestCase(0b1u, true)]
    [TestCase(0b10u, true)]
    [TestCase(0b100u, true)]
    [TestCase(0b1000u, true)]
    [TestCase(0b10000u, true)]
    [TestCase(0b100000u, true)]
    [TestCase(0b1000000u, true)]
    [TestCase(0b1000001u, false)]
    [TestCase(0b1010001u, false)]
    [TestCase(0b1111111u, false)]
    [TestCase(unchecked((uint)int.MinValue), true)]
    [TestCase(ulong.MaxValue, false)]
    public static void BitOps_IsPow2_nuint_64(ulong n, bool expected)
    {
        var actual = BitOperations.IsPow2((nuint) n);
        Assert.AreEqual(expected, actual);
    }

    [Theory]
    [TestCase(0u, 32)]
    [TestCase(0b1u, 31)]
    [TestCase(0b10u, 30)]
    [TestCase(0b100u, 29)]
    [TestCase(0b1000u, 28)]
    [TestCase(0b10000u, 27)]
    [TestCase(0b100000u, 26)]
    [TestCase(0b1000000u, 25)]
    [TestCase(byte.MaxValue << 17, 32 - 8 - 17)]
    [TestCase(byte.MaxValue << 9, 32 - 8 - 9)]
    [TestCase(ushort.MaxValue << 11, 32 - 16 - 11)]
    [TestCase(ushort.MaxValue << 2, 32 - 16 - 2)]
    [TestCase(5 << 7, 32 - 3 - 7)]
    [TestCase(uint.MaxValue, 0)]
    public static void BitOps_LeadingZeroCount_uint(uint n, int expected)
    {
        var actual = BitOperations.LeadingZeroCount(n);
        Assert.AreEqual(expected, actual);
    }

    [Theory]
    [TestCase(0ul, 64)]
    [TestCase(0b1ul, 63)]
    [TestCase(0b10ul, 62)]
    [TestCase(0b100ul, 61)]
    [TestCase(0b1000ul, 60)]
    [TestCase(0b10000ul, 59)]
    [TestCase(0b100000ul, 58)]
    [TestCase(0b1000000ul, 57)]
    [TestCase((ulong)byte.MaxValue << 41, 64 - 8 - 41)]
    [TestCase((ulong)byte.MaxValue << 53, 64 - 8 - 53)]
    [TestCase((ulong)ushort.MaxValue << 31, 64 - 16 - 31)]
    [TestCase((ulong)ushort.MaxValue << 15, 64 - 16 - 15)]
    [TestCase(ulong.MaxValue >> 5, 5)]
    [TestCase(1ul << 63, 0)]
    [TestCase(1ul << 62, 1)]
    [TestCase(ulong.MaxValue, 0)]
    public static void BitOps_LeadingZeroCount_ulong(ulong n, int expected)
    {
        var actual = BitOperations.LeadingZeroCount(n);
        Assert.AreEqual(expected, actual);
    }

    [TestCase(0ul, 64)]
    [TestCase(0b1ul, 63)]
    [TestCase(0b10ul, 62)]
    [TestCase(0b100ul, 61)]
    [TestCase(0b1000ul, 60)]
    [TestCase(0b10000ul, 59)]
    [TestCase(0b100000ul, 58)]
    [TestCase(0b1000000ul, 57)]
    [TestCase((ulong)byte.MaxValue << 41, 64 - 8 - 41)]
    [TestCase((ulong)byte.MaxValue << 53, 64 - 8 - 53)]
    [TestCase((ulong)ushort.MaxValue << 31, 64 - 16 - 31)]
    [TestCase((ulong)ushort.MaxValue << 15, 64 - 16 - 15)]
    [TestCase(ulong.MaxValue >> 5, 5)]
    [TestCase(1ul << 63, 0)]
    [TestCase(1ul << 62, 1)]
    [TestCase(ulong.MaxValue, 0)]
    public static void BitOps_LeadingZeroCount_nuint_64(ulong n, int expected)
    {
        var actual = BitOperations.LeadingZeroCount(n);
        Assert.AreEqual(expected, actual);
    }

    [Theory]
    [TestCase(0u, 32)]
    [TestCase(0b1u, 0)]
    [TestCase(0b10u, 1)]
    [TestCase(0b100u, 2)]
    [TestCase(0b1000u, 3)]
    [TestCase(0b10000u, 4)]
    [TestCase(0b100000u, 5)]
    [TestCase(0b1000000u, 6)]
    [TestCase((uint)byte.MaxValue << 24, 24)]
    [TestCase((uint)byte.MaxValue << 22, 22)]
    [TestCase((uint)ushort.MaxValue << 16, 16)]
    [TestCase((uint)ushort.MaxValue << 19, 19)]
    [TestCase(uint.MaxValue << 5, 5)]
    [TestCase(3u << 27, 27)]
    [TestCase(uint.MaxValue, 0)]
    public static void BitOps_TrailingZeroCount_uint(uint n, int expected)
    {
        var actual = BitOperations.TrailingZeroCount(n);
        Assert.AreEqual(expected, actual);
    }

    [Theory]
    [TestCase(0, 32)]
    [TestCase(0b1, 0)]
    [TestCase(0b10, 1)]
    [TestCase(0b100, 2)]
    [TestCase(0b1000, 3)]
    [TestCase(0b10000, 4)]
    [TestCase(0b100000, 5)]
    [TestCase(0b1000000, 6)]
    [TestCase(byte.MaxValue << 24, 24)]
    [TestCase(byte.MaxValue << 22, 22)]
    [TestCase(ushort.MaxValue << 16, 16)]
    [TestCase(ushort.MaxValue << 19, 19)]
    [TestCase(int.MaxValue << 5, 5)]
    [TestCase(3 << 27, 27)]
    [TestCase(int.MaxValue, 0)]
    public static void BitOps_TrailingZeroCount_int(int n, int expected)
    {
        var actual = BitOperations.TrailingZeroCount(n);
        Assert.AreEqual(expected, actual);
    }

    [Theory]
    [TestCase(0ul, 64)]
    [TestCase(0b1ul, 0)]
    [TestCase(0b10ul, 1)]
    [TestCase(0b100ul, 2)]
    [TestCase(0b1000ul, 3)]
    [TestCase(0b10000ul, 4)]
    [TestCase(0b100000ul, 5)]
    [TestCase(0b1000000ul, 6)]
    [TestCase((ulong)byte.MaxValue << 40, 40)]
    [TestCase((ulong)byte.MaxValue << 57, 57)]
    [TestCase((ulong)ushort.MaxValue << 31, 31)]
    [TestCase((ulong)ushort.MaxValue << 15, 15)]
    [TestCase(ulong.MaxValue << 5, 5)]
    [TestCase(3ul << 59, 59)]
    [TestCase(5ul << 63, 63)]
    [TestCase(ulong.MaxValue, 0)]
    public static void BitOps_TrailingZeroCount_ulong(ulong n, int expected)
    {
        var actual = BitOperations.TrailingZeroCount(n);
        Assert.AreEqual(expected, actual);
    }

    [Theory]
    [TestCase(0L, 64)]
    [TestCase(0b1L, 0)]
    [TestCase(0b10L, 1)]
    [TestCase(0b100L, 2)]
    [TestCase(0b1000L, 3)]
    [TestCase(0b10000L, 4)]
    [TestCase(0b100000L, 5)]
    [TestCase(0b1000000L, 6)]
    [TestCase((long)byte.MaxValue << 40, 40)]
    [TestCase((long)byte.MaxValue << 57, 57)]
    [TestCase((long)ushort.MaxValue << 31, 31)]
    [TestCase((long)ushort.MaxValue << 15, 15)]
    [TestCase(long.MaxValue << 5, 5)]
    [TestCase(3L << 59, 59)]
    [TestCase(5L << 63, 63)]
    [TestCase(long.MaxValue, 0)]
    public static void BitOps_TrailingZeroCount_long(long n, int expected)
    {
        var actual = BitOperations.TrailingZeroCount(n);
        Assert.AreEqual(expected, actual);
    }

    [TestCase(0ul, 64)]
    [TestCase(0b1ul, 0)]
    [TestCase(0b10ul, 1)]
    [TestCase(0b100ul, 2)]
    [TestCase(0b1000ul, 3)]
    [TestCase(0b10000ul, 4)]
    [TestCase(0b100000ul, 5)]
    [TestCase(0b1000000ul, 6)]
    [TestCase((ulong)byte.MaxValue << 40, 40)]
    [TestCase((ulong)byte.MaxValue << 57, 57)]
    [TestCase((ulong)ushort.MaxValue << 31, 31)]
    [TestCase((ulong)ushort.MaxValue << 15, 15)]
    [TestCase(ulong.MaxValue << 5, 5)]
    [TestCase(3ul << 59, 59)]
    [TestCase(5ul << 63, 63)]
    [TestCase(ulong.MaxValue, 0)]
    public static void BitOps_TrailingZeroCount_nuint_64(ulong n, int expected)
    {
        var actual = BitOperations.TrailingZeroCount((nuint) n);
        Assert.AreEqual(expected, actual);
    }

    [TestCase(0L, 64)]
    [TestCase(0b1L, 0)]
    [TestCase(0b10L, 1)]
    [TestCase(0b100L, 2)]
    [TestCase(0b1000L, 3)]
    [TestCase(0b10000L, 4)]
    [TestCase(0b100000L, 5)]
    [TestCase(0b1000000L, 6)]
    [TestCase((long)byte.MaxValue << 40, 40)]
    [TestCase((long)byte.MaxValue << 57, 57)]
    [TestCase((long)ushort.MaxValue << 31, 31)]
    [TestCase((long)ushort.MaxValue << 15, 15)]
    [TestCase(long.MaxValue << 5, 5)]
    [TestCase(3L << 59, 59)]
    [TestCase(5L << 63, 63)]
    [TestCase(long.MaxValue, 0)]
    public static void BitOps_TrailingZeroCount_nint_64(long n, int expected)
    {
        var actual = BitOperations.TrailingZeroCount((nint) n);
        Assert.AreEqual(expected, actual);
    }

    [Theory]
    [TestCase(0, 0)]
    [TestCase(1, 0)]
    [TestCase(2, 1)]
    [TestCase(3, 2 - 1)]
    [TestCase(4, 2)]
    [TestCase(5, 3 - 1)]
    [TestCase(6, 3 - 1)]
    [TestCase(7, 3 - 1)]
    [TestCase(8, 3)]
    [TestCase(9, 4 - 1)]
    [TestCase(byte.MaxValue, 8 - 1)]
    [TestCase(ushort.MaxValue, 16 - 1)]
    [TestCase(uint.MaxValue, 32 - 1)]
    public static void BitOps_Log2_uint(uint n, int expected)
    {
        var actual = BitOperations.Log2(n);
        Assert.AreEqual(expected, actual);
    }

    [Theory]
    [TestCase(0, 0)]
    [TestCase(1, 0)]
    [TestCase(2, 1)]
    [TestCase(3, 2 - 1)]
    [TestCase(4, 2)]
    [TestCase(5, 3 - 1)]
    [TestCase(6, 3 - 1)]
    [TestCase(7, 3 - 1)]
    [TestCase(8, 3)]
    [TestCase(9, 4 - 1)]
    [TestCase(byte.MaxValue, 8 - 1)]
    [TestCase(ushort.MaxValue, 16 - 1)]
    [TestCase(uint.MaxValue, 32 - 1)]
    [TestCase(ulong.MaxValue, 64 - 1)]
    public static void BitOps_Log2_ulong(ulong n, int expected)
    {
        var actual = BitOperations.Log2(n);
        Assert.AreEqual(expected, actual);
    }

    [TestCase(0, 0)]
    [TestCase(1, 0)]
    [TestCase(2, 1)]
    [TestCase(3, 2 - 1)]
    [TestCase(4, 2)]
    [TestCase(5, 3 - 1)]
    [TestCase(6, 3 - 1)]
    [TestCase(7, 3 - 1)]
    [TestCase(8, 3)]
    [TestCase(9, 4 - 1)]
    [TestCase(byte.MaxValue, 8 - 1)]
    [TestCase(ushort.MaxValue, 16 - 1)]
    [TestCase(uint.MaxValue, 32 - 1)]
    [TestCase(ulong.MaxValue, 64 - 1)]
    public static void BitOps_Log2_nuint_64(ulong n, int expected)
    {
        var actual = BitOperations.Log2((nuint) n);
        Assert.AreEqual(expected, actual);
    }

    [Theory]
    [TestCase(0b001, 1)]
    [TestCase(0b010, 1)]
    [TestCase(0b011, 2)]
    [TestCase(0b100, 1)]
    [TestCase(0b101, 2)]
    [TestCase(0b110, 2)]
    [TestCase(0b111, 3)]
    [TestCase(0b1101, 3)]
    [TestCase(0b1111, 4)]
    [TestCase(0b10111, 4)]
    [TestCase(0b11111, 5)]
    [TestCase(0b110111, 5)]
    [TestCase(0b111111, 6)]
    [TestCase(0b1111110, 6)]
    [TestCase(byte.MinValue, 0)] // 0
    [TestCase(byte.MaxValue, 8)] // 255
    [TestCase(unchecked((uint)sbyte.MinValue), 25)] // 4294967168
    [TestCase(sbyte.MaxValue, 7)] // 127
    [TestCase(ushort.MaxValue >> 3, 16 - 3)] // 8191
    [TestCase(ushort.MaxValue, 16)] // 65535
    [TestCase(unchecked((uint)short.MinValue), 32 - 15)] // 4294934528
    [TestCase(short.MaxValue, 15)] // 32767
    [TestCase(unchecked((uint)int.MinValue), 1)] // 2147483648
    [TestCase(unchecked((uint)int.MaxValue), 31)] // 4294967168
    [TestCase(uint.MaxValue >> 5, 32 - 5)] // 134217727
    [TestCase(uint.MaxValue << 11, 32 - 11)] // 4294965248
    [TestCase(uint.MaxValue, 32)] // 4294967295
    public static void BitOps_PopCount_uint(uint n, int expected)
    {
        var actual = BitOperations.PopCount(n);
        Assert.AreEqual(expected, actual);
    }

    [Theory]
    [TestCase(0b001, 1)]
    [TestCase(0b010, 1)]
    [TestCase(0b011, 2)]
    [TestCase(0b100, 1)]
    [TestCase(0b101, 2)]
    [TestCase(0b110, 2)]
    [TestCase(0b111, 3)]
    [TestCase(0b1101, 3)]
    [TestCase(0b1111, 4)]
    [TestCase(0b10111, 4)]
    [TestCase(0b11111, 5)]
    [TestCase(0b110111, 5)]
    [TestCase(0b111111, 6)]
    [TestCase(0b1111110, 6)]
    [TestCase(0b1111111, 7)]
    [TestCase(byte.MinValue, 0)] // 0
    [TestCase(byte.MaxValue, 8)] // 255
    [TestCase(unchecked((ulong)sbyte.MinValue), 57)] // 18446744073709551488
    [TestCase(sbyte.MaxValue, 7)] // 127
    [TestCase(ushort.MaxValue, 16)] // 65535
    [TestCase(unchecked((ulong)short.MinValue), 49)] // 18446744073709518848
    [TestCase(short.MaxValue, 15)] // 32767
    [TestCase(unchecked((ulong)int.MinValue), 64 - 31)] // 18446744071562067968
    [TestCase(int.MaxValue, 31)] // 2147483647
    [TestCase(ulong.MaxValue >> 9, 64 - 9)] // 36028797018963967
    [TestCase(ulong.MaxValue << 11, 64 - 11)] // 18446744073709549568
    [TestCase(ulong.MaxValue, 64)]
    public static void BitOps_PopCount_ulong(ulong n, int expected)
    {
        var actual = BitOperations.PopCount(n);
        Assert.AreEqual(expected, actual);
    }

    [TestCase(0b001, 1)]
    [TestCase(0b010, 1)]
    [TestCase(0b011, 2)]
    [TestCase(0b100, 1)]
    [TestCase(0b101, 2)]
    [TestCase(0b110, 2)]
    [TestCase(0b111, 3)]
    [TestCase(0b1101, 3)]
    [TestCase(0b1111, 4)]
    [TestCase(0b10111, 4)]
    [TestCase(0b11111, 5)]
    [TestCase(0b110111, 5)]
    [TestCase(0b111111, 6)]
    [TestCase(0b1111110, 6)]
    [TestCase(0b1111111, 7)]
    [TestCase(byte.MinValue, 0)] // 0
    [TestCase(byte.MaxValue, 8)] // 255
    [TestCase(unchecked((ulong)sbyte.MinValue), 57)] // 18446744073709551488
    [TestCase(sbyte.MaxValue, 7)] // 127
    [TestCase(ushort.MaxValue, 16)] // 65535
    [TestCase(unchecked((ulong)short.MinValue), 49)] // 18446744073709518848
    [TestCase(short.MaxValue, 15)] // 32767
    [TestCase(unchecked((ulong)int.MinValue), 64 - 31)] // 18446744071562067968
    [TestCase(int.MaxValue, 31)] // 2147483647
    [TestCase(ulong.MaxValue >> 9, 64 - 9)] // 36028797018963967
    [TestCase(ulong.MaxValue << 11, 64 - 11)] // 18446744073709549568
    [TestCase(ulong.MaxValue, 64)]
    public static void BitOps_PopCount_nuint_64(ulong n, int expected)
    {
        var actual = BitOperations.PopCount((nuint)n);
        Assert.AreEqual(expected, actual);
    }

    [Theory]
    [TestCase(0b00000000_00000000_00000000_00000001u, int.MaxValue, 0b10000000_00000000_00000000_00000000u)] // % 32 = 31
    [TestCase(0b01000000_00000001_00000000_00000001u, 3, 0b00000000_00001000_00000000_00001010u)]
    [TestCase(0b01000000_00000001_00000000_00000001u, 2, 0b00000000_00000100_00000000_00000101u)]
    [TestCase(0b01010101_01010101_01010101_01010101u, 1, 0b10101010_10101010_10101010_10101010u)]
    [TestCase(0b01010101_11111111_01010101_01010101u, 0, 0b01010101_11111111_01010101_01010101u)]
    [TestCase(0b00000000_00000000_00000000_00000001u, -1, 0b10000000_00000000_00000000_00000000u)]
    [TestCase(0b00000000_00000000_00000000_00000001u, -2, 0b01000000_00000000_00000000_00000000u)]
    [TestCase(0b00000000_00000000_00000000_00000001u, -3, 0b00100000_00000000_00000000_00000000u)]
    [TestCase(0b01010101_11111111_01010101_01010101u, int.MinValue, 0b01010101_11111111_01010101_01010101u)] // % 32 = 0
    public static void BitOps_RotateLeft_uint(uint n, int offset, uint expected) =>
        Assert.AreEqual(expected, BitOperations.RotateLeft(n, offset));

    [Test]
    public static void BitOps_RotateLeft_nuint()
    {
        unchecked
        {
            if (Environment.Is64BitProcess)
            {
                var value = (nuint)0b01010101_01010101_01010101_01010101_01010101_01010101_01010101_01010101ul;
                Assert.AreEqual((nuint)0b10101010_10101010_10101010_10101010_10101010_10101010_10101010_10101010ul,
                    BitOperations.RotateLeft(value, 1));
                Assert.AreEqual((nuint)0b01010101_01010101_01010101_01010101_01010101_01010101_01010101_01010101ul,
                    BitOperations.RotateLeft(value, 2));
                Assert.AreEqual((nuint)0b10101010_10101010_10101010_10101010_10101010_10101010_10101010_10101010ul,
                    BitOperations.RotateLeft(value, 3));
                Assert.AreEqual(value, BitOperations.RotateLeft(value, int.MinValue)); // % 64 = 0
                Assert.AreEqual(BitOperations.RotateLeft(value, 63),
                    BitOperations.RotateLeft(value, int.MaxValue)); // % 64 = 63
            }
            else
            {
                Assert.AreEqual((nuint)0b10000000_00000000_00000000_00000000u,
                    BitOperations.RotateLeft((nuint)0b00000000_00000000_00000000_00000001u,
                        int.MaxValue)); // % 32 = 31
                Assert.AreEqual((nuint)0b00000000_00001000_00000000_00001010u,
                    BitOperations.RotateLeft((nuint)0b01000000_00000001_00000000_00000001u, 3));
                Assert.AreEqual((nuint)0b00000000_00000100_00000000_00000101u,
                    BitOperations.RotateLeft((nuint)0b01000000_00000001_00000000_00000001u, 2));
                Assert.AreEqual((nuint)0b10101010_10101010_10101010_10101010u,
                    BitOperations.RotateLeft((nuint)0b01010101_01010101_01010101_01010101u, 1));
                Assert.AreEqual((nuint)0b01010101_11111111_01010101_01010101u,
                    BitOperations.RotateLeft((nuint)0b01010101_11111111_01010101_01010101u, 0));
                Assert.AreEqual((nuint)0b10000000_00000000_00000000_00000000u,
                    BitOperations.RotateLeft((nuint)0b00000000_00000000_00000000_00000001u, -1));
                Assert.AreEqual((nuint)0b01000000_00000000_00000000_00000000u,
                    BitOperations.RotateLeft((nuint)0b00000000_00000000_00000000_00000001u, -2));
                Assert.AreEqual((nuint)0b00100000_00000000_00000000_00000000u,
                    BitOperations.RotateLeft((nuint)0b00000000_00000000_00000000_00000001u, -3));
                Assert.AreEqual((nuint)0b01010101_11111111_01010101_01010101u,
                    BitOperations.RotateLeft((nuint)0b01010101_11111111_01010101_01010101u,
                        int.MinValue)); // % 32 = 0
            }
        }
    }

    [Test]
    public static void BitOps_RotateLeft_ulong()
    {
        var value = 0b01010101_01010101_01010101_01010101_01010101_01010101_01010101_01010101ul;
        Assert.AreEqual(0b10101010_10101010_10101010_10101010_10101010_10101010_10101010_10101010ul, BitOperations.RotateLeft(value, 1));
        Assert.AreEqual(0b01010101_01010101_01010101_01010101_01010101_01010101_01010101_01010101ul, BitOperations.RotateLeft(value, 2));
        Assert.AreEqual(0b10101010_10101010_10101010_10101010_10101010_10101010_10101010_10101010ul, BitOperations.RotateLeft(value, 3));
        Assert.AreEqual(value, BitOperations.RotateLeft(value, int.MinValue)); // % 64 = 0
        Assert.AreEqual(BitOperations.RotateLeft(value, 63), BitOperations.RotateLeft(value, int.MaxValue)); // % 64 = 63
    }

    [Theory]
    [TestCase(0b10000000_00000000_00000000_00000000u, int.MaxValue, 0b00000000_00000000_00000000_00000001u)] // % 32 = 31
    [TestCase(0b00000000_00001000_00000000_00001010u, 3, 0b01000000_00000001_00000000_00000001u)]
    [TestCase(0b00000000_00000100_00000000_00000101u, 2, 0b01000000_00000001_00000000_00000001u)]
    [TestCase(0b01010101_01010101_01010101_01010101u, 1, 0b10101010_10101010_10101010_10101010u)]
    [TestCase(0b01010101_11111111_01010101_01010101u, 0, 0b01010101_11111111_01010101_01010101u)]
    [TestCase(0b10000000_00000000_00000000_00000000u, -1, 0b00000000_00000000_00000000_00000001u)]
    [TestCase(0b00000000_00000000_00000000_00000001u, -2, 0b00000000_00000000_00000000_00000100u)]
    [TestCase(0b01000000_00000000_00000000_00000000u, -3, 0b00000000_00000000_00000000_00000010u)]
    [TestCase(0b01010101_11111111_01010101_01010101u, int.MinValue, 0b01010101_11111111_01010101_01010101u)] // % 32 = 0
    public static void BitOps_RotateRight_uint(uint n, int offset, uint expected) =>
        Assert.AreEqual(expected, BitOperations.RotateRight(n, offset));

    [Test]
    public static void BitOps_RotateRight_ulong()
    {
        var value = 0b01010101_01010101_01010101_01010101_01010101_01010101_01010101_01010101ul;
        Assert.AreEqual(0b10101010_10101010_10101010_10101010_10101010_10101010_10101010_10101010ul, BitOperations.RotateRight(value, 1));
        Assert.AreEqual(0b01010101_01010101_01010101_01010101_01010101_01010101_01010101_01010101ul, BitOperations.RotateRight(value, 2));
        Assert.AreEqual(0b10101010_10101010_10101010_10101010_10101010_10101010_10101010_10101010ul, BitOperations.RotateRight(value, 3));
        Assert.AreEqual(value, BitOperations.RotateRight(value, int.MinValue)); // % 64 = 0
        Assert.AreEqual(BitOperations.RotateLeft(value, 63), BitOperations.RotateRight(value, int.MaxValue)); // % 64 = 63
    }

    [Test]
    public static void BitOps_RotateRight_nuint()
    {
        unchecked
        {
            if (Environment.Is64BitProcess)
            {
                const ulong value = 0b01010101_01010101_01010101_01010101_01010101_01010101_01010101_01010101ul;
                Assert.AreEqual(0b10101010_10101010_10101010_10101010_10101010_10101010_10101010_10101010ul,
                    BitOperations.RotateRight(value, 1));
                Assert.AreEqual(0b01010101_01010101_01010101_01010101_01010101_01010101_01010101_01010101ul,
                    BitOperations.RotateRight(value, 2));
                Assert.AreEqual(0b10101010_10101010_10101010_10101010_10101010_10101010_10101010_10101010ul,
                    BitOperations.RotateRight(value, 3));
                Assert.AreEqual(value, BitOperations.RotateRight(value, int.MinValue)); // % 64 = 0
                Assert.AreEqual(BitOperations.RotateLeft(value, 63),
                    BitOperations.RotateRight(value, int.MaxValue)); // % 64 = 63
            }
            else
            {
                Assert.AreEqual((nuint)0b00000000_00000000_00000000_00000001u,
                    BitOperations.RotateRight((nuint)0b10000000_00000000_00000000_00000000u,
                        int.MaxValue)); // % 32 = 31
                Assert.AreEqual((nuint)0b01000000_00000001_00000000_00000001u,
                    BitOperations.RotateRight((nuint)0b00000000_00001000_00000000_00001010u, 3));
                Assert.AreEqual((nuint)0b01000000_00000001_00000000_00000001u,
                    BitOperations.RotateRight((nuint)0b00000000_00000100_00000000_00000101u, 2));
                Assert.AreEqual((nuint)0b10101010_10101010_10101010_10101010u,
                    BitOperations.RotateRight((nuint)0b01010101_01010101_01010101_01010101u, 1));
                Assert.AreEqual((nuint)0b01010101_11111111_01010101_01010101u,
                    BitOperations.RotateRight((nuint)0b01010101_11111111_01010101_01010101u, 0));
                Assert.AreEqual((nuint)0b00000000_00000000_00000000_00000001u,
                    BitOperations.RotateRight((nuint)0b10000000_00000000_00000000_00000000u, -1));
                Assert.AreEqual((nuint)0b00000000_00000000_00000001_00000000u,
                    BitOperations.RotateRight((nuint)0b00000000_00000000_00000000_01000000u, -2));
                Assert.AreEqual((nuint)0b00000000_00000000_00000000_00000010u,
                    BitOperations.RotateRight((nuint)0b01000000_00000000_00000000_00000000u, -3));
                Assert.AreEqual((nuint)0b01010101_11111111_01010101_01010101u,
                    BitOperations.RotateRight((nuint)0b01010101_11111111_01010101_01010101u,
                        int.MinValue)); // % 32 = 0
            }
        }
    }

    [Theory]
    [TestCase(0u, 0u)]
    [TestCase(1u, 1u)]
    [TestCase(2u, 2u)]
    [TestCase(0x0096u, 0x0100u)]
    [TestCase(0x05CDu, 0x0800u)]
    [TestCase(0x0932u, 0x1000u)]
    [TestCase(0x0004_C911u, 0x0008_0000u)]
    [TestCase(0x00E0_A2E2u, 0x0100_0000u)]
    [TestCase(0x0988_0713u, 0x1000_0000u)]
    [TestCase(0x30A4_9649u, 0x4000_0000u)]
    [TestCase(0x7FFF_FFFFu, 0x8000_0000u)]
    [TestCase(0x8000_0000u, 0x8000_0000u)]
    [TestCase(0x8000_0001u, 0ul)]
    [TestCase(0xFFFF_FFFFu, 0ul)]
    public static void BitOps_RoundUpToPow2_uint(uint value, uint expected) =>
        Assert.AreEqual(expected, BitOperations.RoundUpToPowerOf2(value));

    [Theory]
    [TestCase(0ul, 0ul)]
    [TestCase(1ul, 1ul)]
    [TestCase(2ul, 2ul)]
    [TestCase(0x0096ul, 0x0100ul)]
    [TestCase(0x05cdul, 0x0800ul)]
    [TestCase(0x0932ul, 0x1000ul)]
    [TestCase(0x0004_c911ul, 0x0008_0000ul)]
    [TestCase(0x00e0_a2b2ul, 0x0100_0000ul)]
    [TestCase(0x0988_0713ul, 0x1000_0000ul)]
    [TestCase(0x30a4_9649ul, 0x4000_0000ul)]
    [TestCase(0x7FFF_FFFFul, 0x8000_0000ul)]
    [TestCase(0x8000_0000ul, 0x8000_0000ul)]
    [TestCase(0x8000_0001ul, 0x1_0000_0000ul)]
    [TestCase(0xFFFF_FFFFul, 0x1_0000_0000ul)]
    [TestCase(0x0000_0003_343B_0D81ul, 0x0000_0004_0000_0000ul)]
    [TestCase(0x0000_0D87_5EE2_8F19ul, 0x0000_1000_0000_0000ul)]
    [TestCase(0x0006_2A08_4A7A_3A2Dul, 0x0008_0000_0000_0000ul)]
    [TestCase(0x0101_BF76_4398_F791ul, 0x0200_0000_0000_0000ul)]
    [TestCase(0x7FFF_FFFF_FFFF_FFFFul, 0x8000_0000_0000_0000ul)]
    [TestCase(0x8000_0000_0000_0000ul, 0x8000_0000_0000_0000ul)]
    [TestCase(0x8000_0000_0000_0001ul, 0ul)]
    [TestCase(0xFFFF_FFFF_FFFF_FFFFul, 0ul)]
    public static void BitOps_RoundUpToPow2_ulong(ulong value, ulong expected) =>
        Assert.AreEqual(expected, BitOperations.RoundUpToPowerOf2(value));

    [TestCase(0ul, 0ul)]
    [TestCase(1ul, 1ul)]
    [TestCase(2ul, 2ul)]
    [TestCase(0x0096ul, 0x0100ul)]
    [TestCase(0x05cdul, 0x0800ul)]
    [TestCase(0x0932ul, 0x1000ul)]
    [TestCase(0x0004_c911ul, 0x0008_0000ul)]
    [TestCase(0x00e0_a2b2ul, 0x0100_0000ul)]
    [TestCase(0x0988_0713ul, 0x1000_0000ul)]
    [TestCase(0x30a4_9649ul, 0x4000_0000ul)]
    [TestCase(0x7FFF_FFFFul, 0x8000_0000ul)]
    [TestCase(0x8000_0000ul, 0x8000_0000ul)]
    [TestCase(0x8000_0001ul, 0x1_0000_0000ul)]
    [TestCase(0xFFFF_FFFFul, 0x1_0000_0000ul)]
    [TestCase(0x0000_0003_343B_0D81ul, 0x0000_0004_0000_0000ul)]
    [TestCase(0x0000_0D87_5EE2_8F19ul, 0x0000_1000_0000_0000ul)]
    [TestCase(0x0006_2A08_4A7A_3A2Dul, 0x0008_0000_0000_0000ul)]
    [TestCase(0x0101_BF76_4398_F791ul, 0x0200_0000_0000_0000ul)]
    [TestCase(0x7FFF_FFFF_FFFF_FFFFul, 0x8000_0000_0000_0000ul)]
    [TestCase(0x8000_0000_0000_0000ul, 0x8000_0000_0000_0000ul)]
    [TestCase(0x8000_0000_0000_0001ul, 0ul)]
    [TestCase(0xFFFF_FFFF_FFFF_FFFFul, 0ul)]
    public static void BitOps_RoundUpToPow2_nuint_64(ulong value, ulong expected) =>
        Assert.AreEqual(expected, BitOperations.RoundUpToPowerOf2((nuint) value));
    //
    // [Theory]
    // [TestCase(0, 0, 0)]
    // [TestCase(0, 120, 4215344322)]
    // [TestCase(0, byte.MaxValue, 2910671697)]
    // [TestCase(123, byte.MaxValue, 1164749927)]
    // public static void BitOps_Crc32C_byte(uint crc, byte data, uint expected)
    // {
    //     var obtained = BitOperations.Crc32C(crc, data);
    //     Assert.AreEqual(expected, obtained);
    // }
    //
    // [Theory]
    // [TestCase(0, 0, 0)]
    // [TestCase(0, 120, 575477567)]
    // [TestCase(0, ushort.MaxValue, 245266386)]
    // [TestCase(123, ushort.MaxValue, 406112372)]
    // public static void BitOps_Crc32C_ushort(uint crc, ushort data, uint expected)
    // {
    //     var obtained = BitOperations.Crc32C(crc, data);
    //     Assert.AreEqual(expected, obtained);
    // }
    //
    // [Theory]
    // [TestCase(0, 0, 0)]
    // [TestCase(0, 120, 1671666103)]
    // [TestCase(0, uint.MaxValue, 3080238136)]
    // [TestCase(123, uint.MaxValue, 3055133878)]
    // public static void BitOps_Crc32C_uint(uint crc, uint data, uint expected)
    // {
    //     var obtained = BitOperations.Crc32C(crc, data);
    //     Assert.AreEqual(expected, obtained);
    // }
    //
    // [Theory]
    // [TestCase(0, 0, 0)]
    // [TestCase(0, 120, 3511526341)]
    // [TestCase(0, ulong.MaxValue, 3293575501)]
    // [TestCase(123, ulong.MaxValue, 3460750817)]
    // public static void BitOps_Crc32C_ulong(uint crc, ulong data, uint expected)
    // {
    //     var obtained = BitOperations.Crc32C(crc, data);
    //     Assert.AreEqual(expected, obtained);
    // }
}