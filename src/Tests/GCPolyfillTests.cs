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
        var array = GC.AllocateUninitializedArray<byte>(5, pinned: true);
        await Assert.That(array.Length).IsEqualTo(5);
    }

    [Test]
    public async Task AllocateUninitializedArrayEmpty()
    {
        var array = GC.AllocateUninitializedArray<double>(0);
        await Assert.That(array.Length).IsEqualTo(0);
    }
}
