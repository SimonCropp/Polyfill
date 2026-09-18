#if FeatureMemory
using System.Buffers;

partial class PolyfillTests
{
    [Test]
    public async Task ArrayBufferWriter_DefaultCapacity()
    {
        var writer = new ArrayBufferWriter<byte>();

        await Assert.That(writer.WrittenCount).IsEqualTo(0);
        await Assert.That(writer.Capacity).IsEqualTo(0);
        await Assert.That(writer.FreeCapacity).IsEqualTo(0);
        await Assert.That(writer.WrittenMemory.Length).IsEqualTo(0);
    }

    [Test]
    public async Task ArrayBufferWriter_InitialCapacity()
    {
        var writer = new ArrayBufferWriter<byte>(16);

        await Assert.That(writer.WrittenCount).IsEqualTo(0);
        await Assert.That(writer.Capacity).IsEqualTo(16);
        await Assert.That(writer.FreeCapacity).IsEqualTo(16);
    }

    [Test]
    public async Task ArrayBufferWriter_InvalidInitialCapacity()
    {
        await Assert.That(() =>
        {
            var _ = new ArrayBufferWriter<byte>(0);
        }).Throws<ArgumentException>();

        await Assert.That(() =>
        {
            var _ = new ArrayBufferWriter<byte>(-1);
        }).Throws<ArgumentException>();
    }

    [Test]
    public async Task ArrayBufferWriter_WriteThroughSpan()
    {
        var writer = new ArrayBufferWriter<byte>(8);

        var span = writer.GetSpan(3);
        var available = span.Length;
        span[0] = 1;
        span[1] = 2;
        span[2] = 3;
        writer.Advance(3);

        var written = writer.WrittenSpan.ToArray();

        await Assert.That(available).IsGreaterThanOrEqualTo(3);
        await Assert.That(written).IsEquivalentTo(new byte[] {1, 2, 3});
        await Assert.That(writer.WrittenCount).IsEqualTo(3);
        await Assert.That(writer.FreeCapacity).IsEqualTo(writer.Capacity - 3);
        await Assert.That(writer.WrittenMemory.ToArray()).IsEquivalentTo(new byte[] {1, 2, 3});
    }

    [Test]
    public async Task ArrayBufferWriter_WriteThroughMemory()
    {
        var writer = new ArrayBufferWriter<byte>(8);

        var memory = writer.GetMemory(2);
        memory.Span[0] = 10;
        memory.Span[1] = 20;
        writer.Advance(2);

        await Assert.That(memory.Length).IsGreaterThanOrEqualTo(2);
        await Assert.That(writer.WrittenMemory.ToArray()).IsEquivalentTo(new byte[] {10, 20});
    }

    [Test]
    public async Task ArrayBufferWriter_WritesInMultipleChunks()
    {
        var writer = new ArrayBufferWriter<int>(2);

        for (var i = 0; i < 10; i++)
        {
            var span = writer.GetSpan(1);
            span[0] = i;
            writer.Advance(1);
        }

        await Assert.That(writer.WrittenCount).IsEqualTo(10);
        await Assert.That(writer.WrittenMemory.ToArray()).IsEquivalentTo(new[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9});
    }

    [Test]
    public async Task ArrayBufferWriter_GrowsFromEmpty()
    {
        var writer = new ArrayBufferWriter<byte>();

        var length = writer.GetSpan().Length;

        // An empty writer grows to at least the default initial buffer size of 256.
        await Assert.That(length).IsGreaterThanOrEqualTo(256);
        await Assert.That(writer.Capacity).IsGreaterThanOrEqualTo(256);
    }

    [Test]
    public async Task ArrayBufferWriter_GrowsToHonorSizeHint()
    {
        var writer = new ArrayBufferWriter<byte>(4);

        var length = writer.GetSpan(1000).Length;

        await Assert.That(length).IsGreaterThanOrEqualTo(1000);
        await Assert.That(writer.Capacity).IsGreaterThanOrEqualTo(1000);
    }

    [Test]
    public async Task ArrayBufferWriter_GetSpanNeverEmpty()
    {
        var writer = new ArrayBufferWriter<byte>(1);
        writer.GetSpan()[0] = 1;
        writer.Advance(1);

        // Free capacity is exhausted, so even a zero size hint has to grow the buffer.
        var length = writer.GetSpan().Length;

        await Assert.That(length).IsGreaterThan(0);
    }

    [Test]
    public async Task ArrayBufferWriter_AdvanceNegative()
    {
        var writer = new ArrayBufferWriter<byte>(4);

        await Assert.That(() => writer.Advance(-1)).Throws<ArgumentException>();
    }

    [Test]
    public async Task ArrayBufferWriter_AdvancePastEnd()
    {
        var writer = new ArrayBufferWriter<byte>(4);

        await Assert.That(() => writer.Advance(writer.Capacity + 1)).Throws<InvalidOperationException>();
    }

    [Test]
    public async Task ArrayBufferWriter_NegativeSizeHint()
    {
        var writer = new ArrayBufferWriter<byte>(4);

        await Assert.That(() =>
        {
            writer.GetSpan(-1);
        }).Throws<ArgumentException>();

        await Assert.That(() =>
        {
            writer.GetMemory(-1);
        }).Throws<ArgumentException>();
    }

    [Test]
    public async Task ArrayBufferWriter_Clear()
    {
        var writer = new ArrayBufferWriter<byte>(8);
        var span = writer.GetSpan(3);
        span[0] = 1;
        span[1] = 2;
        span[2] = 3;
        writer.Advance(3);
        var capacity = writer.Capacity;

        writer.Clear();
        var reread = writer.GetSpan(3).Slice(0, 3).ToArray();

        await Assert.That(writer.WrittenCount).IsEqualTo(0);
        await Assert.That(writer.Capacity).IsEqualTo(capacity);
        await Assert.That(writer.FreeCapacity).IsEqualTo(capacity);
        // Clear zeroes the written region, unlike ResetWrittenCount.
        await Assert.That(reread).IsEquivalentTo(new byte[] {0, 0, 0});
    }

    [Test]
    public async Task ArrayBufferWriter_ResetWrittenCount()
    {
        var writer = new ArrayBufferWriter<byte>(8);
        var span = writer.GetSpan(3);
        span[0] = 1;
        span[1] = 2;
        span[2] = 3;
        writer.Advance(3);
        var capacity = writer.Capacity;

        writer.ResetWrittenCount();

        await Assert.That(writer.WrittenCount).IsEqualTo(0);
        await Assert.That(writer.Capacity).IsEqualTo(capacity);
        await Assert.That(writer.FreeCapacity).IsEqualTo(capacity);
        await Assert.That(writer.WrittenMemory.Length).IsEqualTo(0);
    }

    [Test]
    public async Task ArrayBufferWriter_ReusableAfterReset()
    {
        var writer = new ArrayBufferWriter<byte>(8);
        writer.GetSpan(1)[0] = 1;
        writer.Advance(1);

        writer.ResetWrittenCount();
        writer.GetSpan(1)[0] = 9;
        writer.Advance(1);

        await Assert.That(writer.WrittenMemory.ToArray()).IsEquivalentTo(new byte[] {9});
    }

    [Test]
    public async Task ArrayBufferWriter_AsIBufferWriter()
    {
        var writer = new ArrayBufferWriter<byte>(8);
        IBufferWriter<byte> bufferWriter = writer;

        bufferWriter.GetSpan(2)[0] = 7;
        bufferWriter.Advance(1);

        await Assert.That(writer.WrittenMemory.ToArray()).IsEquivalentTo(new byte[] {7});
    }

    [Test]
    public async Task ArrayBufferWriter_ReferenceTypeElements()
    {
        var writer = new ArrayBufferWriter<string>(2);
        var span = writer.GetSpan(2);
        span[0] = "one";
        span[1] = "two";
        writer.Advance(2);

        await Assert.That(writer.WrittenMemory.ToArray()).IsEquivalentTo(new[] {"one", "two"});
    }
}
#endif
