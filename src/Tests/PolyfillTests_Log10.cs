partial class PolyfillTests
{
    [Test]
    [Arguments(0, 0)]
    [Arguments(1, 0)]
    [Arguments(9, 0)]
    [Arguments(10, 1)]
    [Arguments(11, 1)]
    [Arguments(99, 1)]
    [Arguments(100, 2)]
    [Arguments(999, 2)]
    [Arguments(1000, 3)]
    [Arguments(int.MaxValue, 9)]
    public async Task Int32_Log10(int value, int expected) =>
        await Assert.That(int.Log10(value)).IsEqualTo(expected);

    [Test]
    public async Task Log10_Unsigned()
    {
        await Assert.That(byte.Log10(0)).IsEqualTo((byte) 0);
        await Assert.That(byte.Log10(9)).IsEqualTo((byte) 0);
        await Assert.That(byte.Log10(byte.MaxValue)).IsEqualTo((byte) 2);
        await Assert.That(ushort.Log10(ushort.MaxValue)).IsEqualTo((ushort) 4);
        await Assert.That(uint.Log10(uint.MaxValue)).IsEqualTo(9u);
        await Assert.That(ulong.Log10(ulong.MaxValue)).IsEqualTo(19ul);
        await Assert.That(nuint.Log10(1000)).IsEqualTo((nuint) 3);
    }

    [Test]
    public async Task Log10_Signed()
    {
        await Assert.That(sbyte.Log10(sbyte.MaxValue)).IsEqualTo((sbyte) 2);
        await Assert.That(short.Log10(short.MaxValue)).IsEqualTo((short) 4);
        await Assert.That(long.Log10(long.MaxValue)).IsEqualTo(18L);
        await Assert.That(nint.Log10(1000)).IsEqualTo((nint) 3);
    }

    [Test]
    public async Task Log10_Negative_Throws()
    {
        await Assert.That(() => { sbyte.Log10(-1); }).Throws<ArgumentOutOfRangeException>();
        await Assert.That(() => { short.Log10(-1); }).Throws<ArgumentOutOfRangeException>();
        await Assert.That(() => { int.Log10(-1); }).Throws<ArgumentOutOfRangeException>();
        await Assert.That(() => { int.Log10(int.MinValue); }).Throws<ArgumentOutOfRangeException>();
        await Assert.That(() => { long.Log10(-1); }).Throws<ArgumentOutOfRangeException>();
        await Assert.That(() => { nint.Log10(-1); }).Throws<ArgumentOutOfRangeException>();
    }

    // every power of ten, and the value either side of it, is where an off by one shows up
    [Test]
    public async Task Int64_Log10_PowersOfTen()
    {
        var power = 1L;
        for (var exponent = 0; exponent < 19; exponent++)
        {
            await Assert.That(long.Log10(power)).IsEqualTo((long) exponent);
            if (power > 1)
            {
                await Assert.That(long.Log10(power - 1)).IsEqualTo((long) (exponent - 1));
            }

            if (exponent < 18)
            {
                await Assert.That(long.Log10(power + 1)).IsEqualTo((long) exponent);
                power *= 10;
            }
        }
    }

#if NET7_0_OR_GREATER
    [Test]
    public async Task Int128_Log10()
    {
        await Assert.That(Int128.Log10(Int128.Zero)).IsEqualTo(Int128.Zero);
        await Assert.That(Int128.Log10(999)).IsEqualTo((Int128) 2);
        await Assert.That(Int128.Log10(1000)).IsEqualTo((Int128) 3);
        await Assert.That(Int128.Log10(Int128.MaxValue)).IsEqualTo((Int128) 38);
        await Assert.That(UInt128.Log10(UInt128.Zero)).IsEqualTo(UInt128.Zero);
        await Assert.That(UInt128.Log10(UInt128.MaxValue)).IsEqualTo((UInt128) 38);
        await Assert.That(() => { Int128.Log10(Int128.NegativeOne); }).Throws<ArgumentOutOfRangeException>();
    }
#endif
}
