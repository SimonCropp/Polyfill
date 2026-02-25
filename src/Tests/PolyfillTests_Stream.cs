partial class PolyfillTests
{
    [Test]
    public async Task StreamReadAsyncMemory()
    {
        var input = new byte[] {1, 2};
        using var stream = new MemoryStream(input);
        var result = new byte[2];
        var memory = new Memory<byte>(result);
        var read = await stream.ReadAsync(memory);
        await Assert.That(read).IsEqualTo(2);
        await Assert.That(input.SequenceEqual(result)).IsTrue();
    }

    [Test]
    public async Task Read_FillsBuffer_ReturnsBufferLength()
    {
        var data = new byte[] {1, 2, 3, 4};
        using var stream = new MemoryStream(data);
        var buffer = new byte[4];

        var read = stream.Read(buffer);

        await Assert.That(read).IsEqualTo(4);
        await Assert.That(buffer.SequenceEqual(data)).IsTrue();
    }

    [Test]
    public async Task Read_PartialBuffer_ReturnsBytesRead()
    {
        var data = new byte[] {1, 2, 3, 4};
        using var stream = new MemoryStream(data);
        var buffer = new byte[2];

        var read = stream.Read(buffer);

        await Assert.That(read).IsEqualTo(2);
        await Assert.That(buffer.SequenceEqual(new byte[] {1, 2})).IsTrue();
    }

    [Test]
    public async Task Read_EmptyBuffer_ReturnsZero()
    {
        var data = new byte[] {1, 2, 3, 4};
        using var stream = new MemoryStream(data);
        var buffer = Array.Empty<byte>();

        var read = stream.Read(buffer);

        await Assert.That(read).IsEqualTo(0);
    }

    [Test]
    public async Task Read_EmptyStream_ReturnsZero()
    {
        using var stream = new MemoryStream([]);
        var buffer = new byte[4];

        var read = stream.Read(buffer);

        await Assert.That(read).IsEqualTo(0);
    }

    [Test]
    public async Task ReadExactly_ReadsExactBytes()
    {
        var data = new byte[] {1, 2, 3, 4, 5};
        using var stream = new MemoryStream(data);
        var buffer = new byte[5];

        // ReSharper disable once MethodHasAsyncOverload
        stream.ReadExactly(buffer, 0, 5);

        await Assert.That(buffer.SequenceEqual(data)).IsTrue();
    }

    [Test]
    public async Task ReadExactlyAsync_ReadsExactBytes()
    {
        var data = new byte[] {1, 2, 3, 4, 5};
        using var stream = new MemoryStream(data);
        var buffer = new byte[5];

        await stream.ReadExactlyAsync(buffer, 0, 5);

        await Assert.That(buffer.SequenceEqual(data)).IsTrue();
    }

    [Test]
    public async Task ReadExactly_WithOffsetAndCount_ReadsCorrectBytes()
    {
        var data = new byte[] {10, 20, 30, 40, 50};
        using var stream = new MemoryStream(data);
        var buffer = new byte[10];
        // ReSharper disable once MethodHasAsyncOverload
        stream.ReadExactly(buffer, 2, 5);

        await Assert.That(buffer.SequenceEqual(new byte[] {0, 0, 10, 20, 30, 40, 50, 0, 0, 0})).IsTrue();
    }

    [Test]
    public async Task ReadExactlyAsync_WithOffsetAndCount_ReadsCorrectBytes()
    {
        var data = new byte[] {10, 20, 30, 40, 50};
        using var stream = new MemoryStream(data);
        var buffer = new byte[10];
        await stream.ReadExactlyAsync(buffer, 2, 5);

        await Assert.That(buffer.SequenceEqual(new byte[] {0, 0, 10, 20, 30, 40, 50, 0, 0, 0})).IsTrue();
    }

    [Test]
    public async Task ReadExactly_ThrowsOnEndOfStream()
    {
        var data = new byte[] {1, 2, 3};
        using var stream = new MemoryStream(data);
        var buffer = new byte[5];

        await Assert.That(() =>
            stream.ReadExactly(buffer, 0, 5)).Throws<EndOfStreamException>();
        await Assert.That(async () =>
            await stream.ReadExactlyAsync(buffer, 0, 5)).Throws<EndOfStreamException>();
    }

    [Test]
    public async Task ReadExactly_ThrowsOnNullBuffer()
    {
        using var stream = new MemoryStream(new byte[1]);
        await Assert.That(() =>
            stream.ReadExactly(null!, 0, 1)).Throws<ArgumentNullException>();
        await Assert.That(async () =>
            await stream.ReadExactlyAsync(null!, 0, 1)).Throws<ArgumentNullException>();
    }

    [Test]
    public async Task ReadExactly_ThrowsOnNegativeOffset()
    {
        using var stream = new MemoryStream(new byte[1]);
        var buffer = new byte[1];
        await Assert.That(() =>
            stream.ReadExactly(buffer, -1, 1)).Throws<ArgumentOutOfRangeException>();
        await Assert.That(async () =>
            await stream.ReadExactlyAsync(buffer, -1, 1)).Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task ReadExactly_ThrowsOnInvalidCount()
    {
        using var stream = new MemoryStream(new byte[1]);
        var buffer = new byte[1];
        await Assert.That(() =>
            stream.ReadExactly(buffer, 0, 2)).Throws<ArgumentOutOfRangeException>();
        await Assert.That(async () =>
            await stream.ReadExactlyAsync(buffer, 0, 2)).Throws<ArgumentOutOfRangeException>();
    }

#if FeatureMemory
    [Test]
    public async Task ReadExactlySpan_ReadsExactBytes()
    {
        var data = new byte[] {1, 2, 3, 4, 5};
        using var stream = new MemoryStream(data);
        var buffer = new byte[5].AsSpan();

        stream.ReadExactly(buffer);

        await Assert.That(buffer.SequenceEqual(data)).IsTrue();
    }

    [Test]
    public async Task ReadExactlySpan_ThrowsOnEndOfStream()
    {
        var data = new byte[] {1, 2, 3};
        using var stream = new MemoryStream(data);

        await Assert.That(() =>
        {
            var buffer = new byte[5].AsSpan();
            stream.ReadExactly(buffer);
        }).Throws<EndOfStreamException>();
        await Assert.That(async () =>
        {
            var buffer = new byte[5].AsMemory();
            await stream.ReadExactlyAsync(buffer);
        }).Throws<EndOfStreamException>();
    }

    [Test]
    public async Task ReadExactly_EmptyBuffer_DoesNotThrow()
    {
        var data = new byte[] {1, 2, 3};
        using var stream = new MemoryStream(data);

        stream.ReadExactly(Span<byte>.Empty);
        await stream.ReadExactlyAsync(Memory<byte>.Empty);
    }

    [Test]
    public async Task ReadAtLeast_ReadsMinimumBytes()
    {
        var data = new byte[] {1, 2, 3, 4, 5};
        using var stream = new MemoryStream(data);
        var buffer = new byte[5];

        var read = stream.ReadAtLeast(buffer, 3);

        await Assert.That(read).IsEqualTo(5);
        await Assert.That(buffer.AsSpan().SequenceEqual(data)).IsTrue();
    }

    [Test]
    public async Task ReadAtLeastAsync_ReadsMinimumBytes()
    {
        var data = new byte[] {1, 2, 3, 4, 5};
        using var stream = new MemoryStream(data);
        var buffer = new byte[5];

        var read = await stream.ReadAtLeastAsync(buffer, 3);

        await Assert.That(read).IsEqualTo(5);
        await Assert.That(buffer.AsSpan().SequenceEqual(data)).IsTrue();
    }

    [Test]
    public async Task ReadAtLeast_ReadsPartialAndReturnsTotalRead()
    {
        var data = new byte[] {1, 2, 3};
        using var stream = new MemoryStream(data);
        var buffer = new byte[5];

        var read = stream.ReadAtLeast(buffer, 2, throwOnEndOfStream: false);

        await Assert.That(read).IsEqualTo(3);
        await Assert.That(buffer.AsSpan(0, 3).SequenceEqual(data)).IsTrue();
    }

    [Test]
    public async Task ReadAtLeastAsync_ReadsPartialAndReturnsTotalRead()
    {
        var data = new byte[] {1, 2, 3};
        using var stream = new MemoryStream(data);
        var buffer = new byte[5];

        var read = await stream.ReadAtLeastAsync(buffer, 2, throwOnEndOfStream: false);

        await Assert.That(read).IsEqualTo(3);
        await Assert.That(buffer.AsSpan(0, 3).SequenceEqual(data)).IsTrue();
    }

    [Test]
    public async Task ReadAtLeast_ThrowsOnEndOfStream_WhenThrowOnEndOfStreamTrue()
    {
        var data = new byte[] {1, 2, 3};
        using var stream = new MemoryStream(data);
        var buffer = new byte[5];

        await Assert.That(() =>
            stream.ReadAtLeast(buffer, 4, throwOnEndOfStream: true)).Throws<EndOfStreamException>();
        await Assert.That(async () =>
            await stream.ReadAtLeastAsync(buffer, 4, throwOnEndOfStream: true)).Throws<EndOfStreamException>();
    }

    [Test]
    public async Task ReadAtLeast_EmptyBuffer_ZeroMinimum_ReturnsZero()
    {
        using var stream = new MemoryStream([]);
        var buffer = Array.Empty<byte>();

        var read = stream.ReadAtLeast(buffer, 0);

        await Assert.That(read).IsEqualTo(0);

        await stream.ReadAtLeastAsync(buffer, 0);

        await Assert.That(read).IsEqualTo(0);
    }

    [Test]
    public async Task ReadAtLeast_ThrowsOnNegativeMinimumBytes()
    {
        using var stream = new MemoryStream(new byte[1]);
        var buffer = new byte[1];

        await Assert.That(() =>
            stream.ReadAtLeast(buffer, -1)).Throws<ArgumentOutOfRangeException>();
        await Assert.That(async () =>
            await stream.ReadAtLeastAsync(buffer, -1)).Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task ReadAtLeast_ThrowsWhenMinimumGreaterThanBufferLength()
    {
        using var stream = new MemoryStream(new byte[1]);
        var buffer = new byte[1];

        await Assert.That(() =>
            stream.ReadAtLeast(buffer, 2)).Throws<ArgumentOutOfRangeException>();

        await Assert.That(async () =>
            await stream.ReadAtLeastAsync(buffer, 2)).Throws<ArgumentOutOfRangeException>();
    }
#endif
}