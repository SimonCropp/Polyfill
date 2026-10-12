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

    static int CountBits(BitArray target)
    {
        var count = 0;
        for (var index = 0; index < target.Length; index++)
        {
            if (target[index])
            {
                count++;
            }
        }

        return count;
    }

    [Test]
    public async Task BitArray_SingleBitAtEachPosition()
    {
        var failures = new List<string>();
        foreach (var length in new[] {1, 2, 31, 32, 33, 63, 64, 65, 96, 97, 130})
        {
            for (var position = 0; position < length; position++)
            {
                var oneSet = new BitArray(length)
                {
                    [position] = true
                };
                if (oneSet.PopCount() != 1 ||
                    !oneSet.HasAnySet() ||
                    oneSet.HasAllSet() != (length == 1))
                {
                    failures.Add($"length {length}, only {position} set");
                }

                var oneClear = new BitArray(length, true)
                {
                    [position] = false
                };
                if (oneClear.PopCount() != length - 1 ||
                    oneClear.HasAnySet() != (length != 1) ||
                    oneClear.HasAllSet())
                {
                    failures.Add($"length {length}, only {position} clear");
                }
            }
        }

        await Assert.That(failures).IsEmpty();
    }

    [Test]
    public async Task BitArray_RandomBits()
    {
        var random = new Random(1234);
        var failures = new List<string>();
        foreach (var length in new[] {1, 7, 31, 32, 33, 64, 65, 255, 256, 257, 1000, 4097, 100003})
        {
            for (var iteration = 0; iteration < 4; iteration++)
            {
                var target = new BitArray(length);
                // from sparse to dense
                var threshold = 0.05 + iteration * 0.3;
                for (var index = 0; index < length; index++)
                {
                    target[index] = random.NextDouble() < threshold;
                }

                var expected = CountBits(target);
                if (target.PopCount() != expected ||
                    target.HasAnySet() != (expected != 0) ||
                    target.HasAllSet() != (expected == length))
                {
                    failures.Add($"length {length}, iteration {iteration}");
                }
            }
        }

        await Assert.That(failures).IsEmpty();
    }

    // Not flips whole words, and a BitArray built from ints or bytes keeps whatever was in them, so the bits of the
    // last word that lie past Length can be set
    [Test]
    [Arguments(1)]
    [Arguments(5)]
    [Arguments(31)]
    [Arguments(32)]
    [Arguments(33)]
    [Arguments(40)]
    [Arguments(64)]
    [Arguments(70)]
    public async Task BitArray_BitsPastLengthAreIgnored(int length)
    {
        var allSet = new BitArray(length).Not();
        await Assert.That(allSet.PopCount()).IsEqualTo(length);
        await Assert.That(allSet.HasAllSet()).IsTrue();
        await Assert.That(allSet.HasAnySet()).IsTrue();

        var allClear = new BitArray(length, true).Not();
        await Assert.That(allClear.PopCount()).IsEqualTo(0);
        await Assert.That(allClear.HasAllSet()).IsFalse();
        await Assert.That(allClear.HasAnySet()).IsFalse();

        var shrunk = new BitArray(new[] {-1, -1, -1})
        {
            Length = length
        };
        await Assert.That(shrunk.PopCount()).IsEqualTo(length);
        await Assert.That(shrunk.HasAllSet()).IsTrue();

        // Growing again. Whether the bits dropped by the shrink come back cleared depends on the runtime, so the
        // expectation is read back a bit at a time.
        shrunk.Length = length + 40;
        var grownCount = CountBits(shrunk);
        await Assert.That(grownCount >= length).IsTrue();
        await Assert.That(shrunk.PopCount()).IsEqualTo(grownCount);
        await Assert.That(shrunk.HasAllSet()).IsFalse();
        await Assert.That(shrunk.HasAnySet()).IsTrue();

        shrunk.Length = 0;
        await Assert.That(shrunk.PopCount()).IsEqualTo(0);
        await Assert.That(shrunk.HasAllSet()).IsTrue();
        await Assert.That(shrunk.HasAnySet()).IsFalse();
    }

    [Test]
    public async Task BitArray_FromBytesAndBools()
    {
        var fromBytes = new BitArray(new byte[] {0xFF, 0x0F, 0x00, 0x80, 0x01});
        await Assert.That(fromBytes.Length).IsEqualTo(40);
        await Assert.That(fromBytes.PopCount()).IsEqualTo(14);
        await Assert.That(fromBytes.HasAllSet()).IsFalse();
        await Assert.That(fromBytes.HasAnySet()).IsTrue();

        var fromBools = new BitArray(new[] {true, false, true});
        await Assert.That(fromBools.PopCount()).IsEqualTo(2);

        var fromInts = new BitArray(new[] {unchecked((int) 0x80000001), 0, 3});
        await Assert.That(fromInts.PopCount()).IsEqualTo(4);
    }

    [Test]
    public async Task BitArray_DoesNotModifyTheArray()
    {
        var target = new BitArray(70);
        target[0] = true;
        target[33] = true;
        target[69] = true;
        var before = new bool[70];
        target.CopyTo(before, 0);

        target.PopCount();
        target.HasAllSet();
        target.HasAnySet();

        var after = new bool[70];
        target.CopyTo(after, 0);
        await Assert.That(after.SequenceEqual(before)).IsTrue();
        await Assert.That(target.Length).IsEqualTo(70);
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
