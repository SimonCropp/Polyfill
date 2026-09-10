using System.Numerics;

// Every case is checked against a BigInteger product, so on net9.0 and later these
// validate the BCL and below it the polyfill.
public class BigMulTests
{
    static int[] ints = [0, 1, -1, 2, -2, 65536, -65537, 123456789, int.MaxValue, int.MinValue];
    static uint[] uints = [0, 1, 2, 65536, 123456789, 0x8000_0000, uint.MaxValue];
    static long[] longs = [0, 1, -1, 2, -2, 0x0123456789ABCDEF, unchecked((long) 0xFEDCBA9876543210), long.MaxValue, long.MinValue];
    static ulong[] ulongs = [0, 1, 2, 0x0123456789ABCDEF, 0x8000_0000_0000_0000, ulong.MaxValue];

    [Test]
    public async Task Int32()
    {
        foreach (var left in ints)
        foreach (var right in ints)
        {
            var actual = (BigInteger) int.BigMul(left, right);
            await Assert.That(actual).IsEqualTo((BigInteger) left * right);
        }
    }

    [Test]
    public async Task UInt32()
    {
        foreach (var left in uints)
        foreach (var right in uints)
        {
            var actual = (BigInteger) uint.BigMul(left, right);
            await Assert.That(actual).IsEqualTo((BigInteger) left * right);
        }
    }

    [Test]
    public async Task Math_Signed()
    {
        foreach (var left in longs)
        foreach (var right in longs)
        {
            var upper = Math.BigMul(left, right, out var lower);
            var actual = ((BigInteger) upper << 64) + unchecked((ulong) lower);
            await Assert.That(actual).IsEqualTo((BigInteger) left * right);
        }
    }

    [Test]
    public async Task Math_Unsigned()
    {
        foreach (var left in ulongs)
        foreach (var right in ulongs)
        {
            var upper = Math.BigMul(left, right, out var lower);
            var actual = ((BigInteger) upper << 64) + lower;
            await Assert.That(actual).IsEqualTo((BigInteger) left * right);
        }
    }

    [Test]
    public async Task IntPtr()
    {
        var bits = System.IntPtr.Size * 8;
        nint[] values = [0, 1, -1, 2, -2, 123456789, (nint) int.MaxValue, (nint) int.MinValue];
        foreach (var left in values)
        foreach (var right in values)
        {
            var upper = nint.BigMul(left, right, out var lower);
            var lowerUnsigned = System.IntPtr.Size == 8
                ? (BigInteger) unchecked((ulong) lower)
                : (BigInteger) unchecked((uint) lower);
            var actual = ((BigInteger) upper << bits) + lowerUnsigned;
            await Assert.That(actual).IsEqualTo((BigInteger) left * right);
        }
    }

    [Test]
    public async Task UIntPtr()
    {
        var bits = System.UIntPtr.Size * 8;
        nuint[] values = [0, 1, 2, 123456789, (nuint) uint.MaxValue];
        foreach (var left in values)
        foreach (var right in values)
        {
            var upper = nuint.BigMul(left, right, out var lower);
            var actual = ((BigInteger) upper << bits) + lower;
            await Assert.That(actual).IsEqualTo((BigInteger) left * right);
        }
    }

#if NET7_0_OR_GREATER

    [Test]
    public async Task Int64()
    {
        foreach (var left in longs)
        foreach (var right in longs)
        {
            var actual = ToBig(long.BigMul(left, right));
            await Assert.That(actual).IsEqualTo((BigInteger) left * right);
        }
    }

    [Test]
    public async Task UInt64()
    {
        foreach (var left in ulongs)
        foreach (var right in ulongs)
        {
            var actual = ToBig(ulong.BigMul(left, right));
            await Assert.That(actual).IsEqualTo((BigInteger) left * right);
        }
    }

    [Test]
    public async Task Int128_()
    {
        Int128[] values =
        [
            0,
            1,
            -1,
            (Int128) long.MaxValue * 3,
            -((Int128) long.MaxValue * 7),
            Int128.MaxValue,
            Int128.MinValue
        ];
        foreach (var left in values)
        foreach (var right in values)
        {
            var upper = Int128.BigMul(left, right, out var lower);
            var actual = (ToBig(upper) << 128) + ToBig((UInt128) lower);
            await Assert.That(actual).IsEqualTo(ToBig(left) * ToBig(right));
        }
    }

    [Test]
    public async Task UInt128_()
    {
        UInt128[] values =
        [
            0,
            1,
            2,
            ulong.MaxValue,
            (UInt128) ulong.MaxValue + 1,
            UInt128.MaxValue
        ];
        foreach (var left in values)
        foreach (var right in values)
        {
            var upper = UInt128.BigMul(left, right, out var lower);
            var actual = (ToBig(upper) << 128) + ToBig(lower);
            await Assert.That(actual).IsEqualTo(ToBig(left) * ToBig(right));
        }
    }

    // built from the two 64 bit halves, so no Int128 to BigInteger conversion is assumed
    static BigInteger ToBig(UInt128 value) =>
        ((BigInteger) (ulong) (value >> 64) << 64) + (ulong) value;

    static BigInteger ToBig(Int128 value)
    {
        var unsigned = ToBig((UInt128) value);
        return value < 0 ? unsigned - (BigInteger.One << 128) : unsigned;
    }

#endif
}
