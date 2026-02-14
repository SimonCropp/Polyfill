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
}

#endif
