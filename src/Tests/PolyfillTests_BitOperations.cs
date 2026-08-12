using System.Numerics;

partial class PolyfillTests
{
    [Test]
    public async Task BitOperations_IsPow2Int()
    {
        await Assert.That(BitOperations.IsPow2(1)).IsTrue();
        await Assert.That(BitOperations.IsPow2(2)).IsTrue();
        await Assert.That(BitOperations.IsPow2(1 << 30)).IsTrue();
        await Assert.That(BitOperations.IsPow2(0)).IsFalse();
        await Assert.That(BitOperations.IsPow2(3)).IsFalse();
        await Assert.That(BitOperations.IsPow2(-2)).IsFalse();
        await Assert.That(BitOperations.IsPow2(int.MinValue)).IsFalse();
        await Assert.That(BitOperations.IsPow2(int.MaxValue)).IsFalse();
    }

    [Test]
    public async Task BitOperations_IsPow2Uint()
    {
        await Assert.That(BitOperations.IsPow2(1u)).IsTrue();
        await Assert.That(BitOperations.IsPow2(2u)).IsTrue();
        await Assert.That(BitOperations.IsPow2(1u << 31)).IsTrue();
        await Assert.That(BitOperations.IsPow2(0u)).IsFalse();
        await Assert.That(BitOperations.IsPow2(3u)).IsFalse();
        await Assert.That(BitOperations.IsPow2(uint.MaxValue)).IsFalse();
    }

    [Test]
    public async Task BitOperations_IsPow2Long()
    {
        await Assert.That(BitOperations.IsPow2(1L)).IsTrue();
        await Assert.That(BitOperations.IsPow2(1L << 62)).IsTrue();
        await Assert.That(BitOperations.IsPow2(0L)).IsFalse();
        await Assert.That(BitOperations.IsPow2(3L)).IsFalse();
        await Assert.That(BitOperations.IsPow2(-2L)).IsFalse();
        await Assert.That(BitOperations.IsPow2(long.MinValue)).IsFalse();
        await Assert.That(BitOperations.IsPow2(long.MaxValue)).IsFalse();
    }

    [Test]
    public async Task BitOperations_IsPow2Ulong()
    {
        await Assert.That(BitOperations.IsPow2(1ul)).IsTrue();
        await Assert.That(BitOperations.IsPow2(1ul << 63)).IsTrue();
        await Assert.That(BitOperations.IsPow2(0ul)).IsFalse();
        await Assert.That(BitOperations.IsPow2(3ul)).IsFalse();
        await Assert.That(BitOperations.IsPow2(ulong.MaxValue)).IsFalse();
    }

    [Test]
    public async Task BitOperations_IsPow2Nint()
    {
        await Assert.That(BitOperations.IsPow2((nint) 1)).IsTrue();
        await Assert.That(BitOperations.IsPow2((nint) 1 << 30)).IsTrue();
        await Assert.That(BitOperations.IsPow2((nint) 0)).IsFalse();
        await Assert.That(BitOperations.IsPow2((nint) 3)).IsFalse();
        await Assert.That(BitOperations.IsPow2((nint) (-2))).IsFalse();
    }

    [Test]
    public async Task BitOperations_IsPow2Nuint()
    {
        await Assert.That(BitOperations.IsPow2((nuint) 1)).IsTrue();
        await Assert.That(BitOperations.IsPow2((nuint) 1 << 30)).IsTrue();
        await Assert.That(BitOperations.IsPow2((nuint) 0)).IsFalse();
        await Assert.That(BitOperations.IsPow2((nuint) 3)).IsFalse();
    }
}
