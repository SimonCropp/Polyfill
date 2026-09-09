using System.Collections;

partial class PolyfillTests
{
    [Test]
    public async Task BitArray_Empty()
    {
        var target = new BitArray(0);
        // all set is vacuously true for an empty BitArray
        await Assert.That(target.HasAllSet()).IsTrue();
        await Assert.That(target.HasAnySet()).IsFalse();
        await Assert.That(target.PopCount()).IsEqualTo(0);
    }

    [Test]
    public async Task BitArray_AllClear()
    {
        var target = new BitArray(8);
        await Assert.That(target.HasAllSet()).IsFalse();
        await Assert.That(target.HasAnySet()).IsFalse();
        await Assert.That(target.PopCount()).IsEqualTo(0);
    }

    [Test]
    public async Task BitArray_AllSet()
    {
        var target = new BitArray(8, true);
        await Assert.That(target.HasAllSet()).IsTrue();
        await Assert.That(target.HasAnySet()).IsTrue();
        await Assert.That(target.PopCount()).IsEqualTo(8);
    }

    [Test]
    public async Task BitArray_Mixed()
    {
        var target = new BitArray(8)
        {
            [3] = true,
            [7] = true
        };
        await Assert.That(target.HasAllSet()).IsFalse();
        await Assert.That(target.HasAnySet()).IsTrue();
        await Assert.That(target.PopCount()).IsEqualTo(2);
    }

    [Test]
    public async Task BitArray_Single()
    {
        var clear = new BitArray(1);
        await Assert.That(clear.HasAllSet()).IsFalse();
        await Assert.That(clear.HasAnySet()).IsFalse();
        await Assert.That(clear.PopCount()).IsEqualTo(0);

        var set = new BitArray(1, true);
        await Assert.That(set.HasAllSet()).IsTrue();
        await Assert.That(set.HasAnySet()).IsTrue();
        await Assert.That(set.PopCount()).IsEqualTo(1);
    }

    // the last word is only partly used, so the unused high bits must not be counted
    [Test]
    [Arguments(31)]
    [Arguments(32)]
    [Arguments(33)]
    [Arguments(64)]
    [Arguments(100)]
    public async Task BitArray_WordBoundaries(int length)
    {
        var target = new BitArray(length, true);
        await Assert.That(target.HasAllSet()).IsTrue();
        await Assert.That(target.HasAnySet()).IsTrue();
        await Assert.That(target.PopCount()).IsEqualTo(length);

        target[length - 1] = false;
        await Assert.That(target.HasAllSet()).IsFalse();
        await Assert.That(target.HasAnySet()).IsTrue();
        await Assert.That(target.PopCount()).IsEqualTo(length - 1);
    }
}
