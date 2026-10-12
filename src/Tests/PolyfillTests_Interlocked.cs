#if FeatureMemory && FeatureUnsafe

partial class PolyfillTests
{
    [Flags]
    enum Permissions
    {
        None = 0,
        Read = 1,
        Write = 2,
        Execute = 4
    }

    [Test]
    public async Task InterlockedAndInt()
    {
        var value = 0xFF;
        var original = Interlocked.And(ref value, 0x0F);
        await Assert.That(original).IsEqualTo(0xFF);
        await Assert.That(value).IsEqualTo(0x0F);
    }

    [Test]
    public async Task InterlockedOrInt()
    {
        var value = 0x0F;
        var original = Interlocked.Or(ref value, 0xF0);
        await Assert.That(original).IsEqualTo(0x0F);
        await Assert.That(value).IsEqualTo(0xFF);
    }

    [Test]
    public async Task InterlockedAndLong()
    {
        var value = 0xFFL;
        var original = Interlocked.And(ref value, 0x0FL);
        await Assert.That(original).IsEqualTo(0xFFL);
        await Assert.That(value).IsEqualTo(0x0FL);
    }

    [Test]
    public async Task InterlockedOrLong()
    {
        var value = 0x0FL;
        var original = Interlocked.Or(ref value, 0xF0L);
        await Assert.That(original).IsEqualTo(0x0FL);
        await Assert.That(value).IsEqualTo(0xFFL);
    }

    [Test]
    public async Task InterlockedAndEnum()
    {
        var permissions = Permissions.Read | Permissions.Write | Permissions.Execute;
        var original = Interlocked.And(ref permissions, ~Permissions.Execute);
        await Assert.That(original).IsEqualTo(Permissions.Read | Permissions.Write | Permissions.Execute);
        await Assert.That(permissions).IsEqualTo(Permissions.Read | Permissions.Write);
    }

    [Test]
    public async Task InterlockedOrEnum()
    {
        var permissions = Permissions.Read;
        var original = Interlocked.Or(ref permissions, Permissions.Write);
        await Assert.That(original).IsEqualTo(Permissions.Read);
        await Assert.That(permissions).IsEqualTo(Permissions.Read | Permissions.Write);
    }

    [Flags]
    enum ByteFlags : byte
    {
        None = 0,
        A = 1,
        B = 2,
        High = 0x80
    }

    [Flags]
    enum ShortFlags : short
    {
        None = 0,
        A = 1,
        B = 2,
        High = 0x4000
    }

    [Test]
    public async Task InterlockedAndByteEnum()
    {
        var value = ByteFlags.A | ByteFlags.B | ByteFlags.High;
        var original = Interlocked.And(ref value, ByteFlags.A | ByteFlags.High);
        await Assert.That(original).IsEqualTo(ByteFlags.A | ByteFlags.B | ByteFlags.High);
        await Assert.That(value).IsEqualTo(ByteFlags.A | ByteFlags.High);
    }

    [Test]
    public async Task InterlockedOrByteEnum()
    {
        var value = ByteFlags.A;
        var original = Interlocked.Or(ref value, ByteFlags.High);
        await Assert.That(original).IsEqualTo(ByteFlags.A);
        await Assert.That(value).IsEqualTo(ByteFlags.A | ByteFlags.High);
    }

    [Test]
    public async Task InterlockedAndShortEnum()
    {
        var value = ShortFlags.A | ShortFlags.B | ShortFlags.High;
        var original = Interlocked.And(ref value, ShortFlags.B | ShortFlags.High);
        await Assert.That(original).IsEqualTo(ShortFlags.A | ShortFlags.B | ShortFlags.High);
        await Assert.That(value).IsEqualTo(ShortFlags.B | ShortFlags.High);
    }

    [Test]
    public async Task InterlockedOrShortEnum()
    {
        var value = ShortFlags.A;
        var original = Interlocked.Or(ref value, ShortFlags.High);
        await Assert.That(original).IsEqualTo(ShortFlags.A);
        await Assert.That(value).IsEqualTo(ShortFlags.A | ShortFlags.High);
    }

    [Test]
    public async Task InterlockedAndOrSmallIntegers()
    {
        byte byteValue = 0xFF;
        await Assert.That(Interlocked.And<byte>(ref byteValue, 0x0F)).IsEqualTo((byte) 0xFF);
        await Assert.That(byteValue).IsEqualTo((byte) 0x0F);
        await Assert.That(Interlocked.Or<byte>(ref byteValue, 0xF0)).IsEqualTo((byte) 0x0F);
        await Assert.That(byteValue).IsEqualTo((byte) 0xFF);

        sbyte sbyteValue = -1;
        await Assert.That(Interlocked.And<sbyte>(ref sbyteValue, 0x0F)).IsEqualTo((sbyte) -1);
        await Assert.That(sbyteValue).IsEqualTo((sbyte) 0x0F);
        await Assert.That(Interlocked.Or<sbyte>(ref sbyteValue, -128)).IsEqualTo((sbyte) 0x0F);
        await Assert.That(sbyteValue).IsEqualTo((sbyte) -113);

        short shortValue = -1;
        await Assert.That(Interlocked.And<short>(ref shortValue, 0x0FF0)).IsEqualTo((short) -1);
        await Assert.That(shortValue).IsEqualTo((short) 0x0FF0);
        await Assert.That(Interlocked.Or<short>(ref shortValue, 0x000F)).IsEqualTo((short) 0x0FF0);
        await Assert.That(shortValue).IsEqualTo((short) 0x0FFF);

        ushort ushortValue = 0xFFFF;
        await Assert.That(Interlocked.And<ushort>(ref ushortValue, 0x0FF0)).IsEqualTo((ushort) 0xFFFF);
        await Assert.That(ushortValue).IsEqualTo((ushort) 0x0FF0);
        await Assert.That(Interlocked.Or<ushort>(ref ushortValue, 0xF000)).IsEqualTo((ushort) 0x0FF0);
        await Assert.That(ushortValue).IsEqualTo((ushort) 0xFFF0);
    }

    [Test]
    public async Task InterlockedAndDoesNotDisturbAdjacentBytes()
    {
        var values = new byte[] {0xAA, 0xFF, 0xBB};
        var original = Interlocked.And<byte>(ref values[1], 0x0F);
        await Assert.That(original).IsEqualTo((byte) 0xFF);
        await Assert.That(values).IsEquivalentTo(new byte[] {0xAA, 0x0F, 0xBB});
    }
}

#endif
