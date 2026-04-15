public class GCPolyfillTests
{
    [Test]
    public async Task AllocateUninitializedArray()
    {
        var array = GC.AllocateUninitializedArray<int>(10);
        await Assert.That(array.Length).IsEqualTo(10);
    }

    [Test]
    public async Task AllocateUninitializedArrayPinned()
    {
#if NET5_0_OR_GREATER
        var array = GC.AllocateUninitializedArray<byte>(5, pinned: true);
        await Assert.That(array.Length).IsEqualTo(5);
#else
        await Assert.That(() => GC.AllocateUninitializedArray<byte>(5, pinned: true))
            .Throws<NotSupportedException>();
#endif
    }

    [Test]
    public async Task AllocateUninitializedArrayEmpty()
    {
        var array = GC.AllocateUninitializedArray<double>(0);
        await Assert.That(array.Length).IsEqualTo(0);
    }
}
