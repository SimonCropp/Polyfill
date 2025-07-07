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
        Assert.AreEqual(2, read);
        Assert.IsTrue(input.SequenceEqual(result));
    }

    [Test]
    public void Read_FillsBuffer_ReturnsBufferLength()
    {
        var data = new byte[] {1, 2, 3, 4};
        using var stream = new MemoryStream(data);
        var buffer = new byte[4];

        var read = stream.Read(buffer);

        Assert.AreEqual(4, read);
        Assert.AreEqual(data, buffer);
    }

    [Test]
    public void Read_PartialBuffer_ReturnsBytesRead()
    {
        var data = new byte[] {1, 2, 3, 4};
        using var stream = new MemoryStream(data);
        var buffer = new byte[2];

        var read = stream.Read(buffer);

        Assert.AreEqual(2, read);
        Assert.AreEqual(new byte[] {1, 2}, buffer);
    }

    [Test]
    public void Read_EmptyBuffer_ReturnsZero()
    {
        var data = new byte[] {1, 2, 3, 4};
        using var stream = new MemoryStream(data);
        var buffer = Array.Empty<byte>();

        var read = stream.Read(buffer);

        Assert.AreEqual(0, read);
    }

    [Test]
    public void Read_EmptyStream_ReturnsZero()
    {
        using var stream = new MemoryStream([]);
        var buffer = new byte[4];

        var read = stream.Read(buffer);

        Assert.AreEqual(0, read);
    }

    [Test]
    public void ReadExactly_ReadsExactBytes()
    {
        var data = new byte[] {1, 2, 3, 4, 5};
        using var stream = new MemoryStream(data);
        var buffer = new byte[5];

        stream.ReadExactly(buffer, 0, 5);

        Assert.AreEqual(data, buffer);
    }

    [Test]
    public async Task ReadExactlyAsync_ReadsExactBytes()
    {
        var data = new byte[] {1, 2, 3, 4, 5};
        using var stream = new MemoryStream(data);
        var buffer = new byte[5];

        await stream.ReadExactlyAsync(buffer, 0, 5);

        Assert.AreEqual(data, buffer);
    }

    [Test]
    public void ReadExactly_WithOffsetAndCount_ReadsCorrectBytes()
    {
        var data = new byte[] {10, 20, 30, 40, 50};
        using var stream = new MemoryStream(data);
        var buffer = new byte[10];
        stream.ReadExactly(buffer, 2, 5);

        Assert.AreEqual(new byte[] {0, 0, 10, 20, 30, 40, 50, 0, 0, 0}, buffer);
    }

    [Test]
    public async Task ReadExactlyAsync_WithOffsetAndCount_ReadsCorrectBytes()
    {
        var data = new byte[] {10, 20, 30, 40, 50};
        using var stream = new MemoryStream(data);
        var buffer = new byte[10];
        await stream.ReadExactlyAsync(buffer, 2, 5);

        Assert.AreEqual(new byte[] {0, 0, 10, 20, 30, 40, 50, 0, 0, 0}, buffer);
    }

    [Test]
    public void ReadExactly_ThrowsOnEndOfStream()
    {
        var data = new byte[] {1, 2, 3};
        using var stream = new MemoryStream(data);
        var buffer = new byte[5];

        Assert.Throws<EndOfStreamException>(() =>
            stream.ReadExactly(buffer, 0, 5));
        Assert.ThrowsAsync<EndOfStreamException>(async () =>
            await stream.ReadExactlyAsync(buffer, 0, 5));
    }

    [Test]
    public void ReadExactly_ThrowsOnNullBuffer()
    {
        using var stream = new MemoryStream(new byte[1]);
        Assert.Throws<ArgumentNullException>(() =>
            stream.ReadExactly(null!, 0, 1));
        Assert.ThrowsAsync<ArgumentNullException>(async () =>
            await stream.ReadExactlyAsync(null!, 0, 1));
    }

    [Test]
    public void ReadExactly_ThrowsOnNegativeOffset()
    {
        using var stream = new MemoryStream(new byte[1]);
        var buffer = new byte[1];
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            stream.ReadExactly(buffer, -1, 1));
        Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
            await stream.ReadExactlyAsync(buffer, -1, 1));
    }

    [Test]
    public void ReadExactly_ThrowsOnInvalidCount()
    {
        using var stream = new MemoryStream(new byte[1]);
        var buffer = new byte[1];
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            stream.ReadExactly(buffer, 0, 2));
        Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
            await stream.ReadExactlyAsync(buffer, 0, 2));
    }

#if FeatureMemory
    [Test]
    public void ReadExactlySpan_ReadsExactBytes()
    {
        var data = new byte[] {1, 2, 3, 4, 5};
        using var stream = new MemoryStream(data);
        var buffer = new byte[5].AsSpan();

        stream.ReadExactly(buffer);

        Assert.IsTrue(buffer.SequenceEqual(data));
    }

    [Test]
    public void ReadExactlySpan_ThrowsOnEndOfStream()
    {
        var data = new byte[] {1, 2, 3};
        using var stream = new MemoryStream(data);

        Assert.Throws<EndOfStreamException>(() =>
        {
            var buffer = new byte[5].AsSpan();
            stream.ReadExactly(buffer);
        });
        Assert.ThrowsAsync<EndOfStreamException>(async () =>
        {
            var buffer = new byte[5].AsMemory();
            await stream.ReadExactlyAsync(buffer);
        });
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
    public void ReadAtLeast_ReadsMinimumBytes()
    {
        var data = new byte[] {1, 2, 3, 4, 5};
        using var stream = new MemoryStream(data);
        var buffer = new byte[5];

        var read = stream.ReadAtLeast(buffer, 3);

        Assert.AreEqual(5, read);
        Assert.IsTrue(buffer.AsSpan().SequenceEqual(data));
    }

    [Test]
    public async Task ReadAtLeastAsync_ReadsMinimumBytes()
    {
        var data = new byte[] {1, 2, 3, 4, 5};
        using var stream = new MemoryStream(data);
        var buffer = new byte[5];

        var read = await stream.ReadAtLeastAsync(buffer, 3);

        Assert.AreEqual(5, read);
        Assert.IsTrue(buffer.AsSpan().SequenceEqual(data));
    }

    [Test]
    public void ReadAtLeast_ReadsPartialAndReturnsTotalRead()
    {
        var data = new byte[] {1, 2, 3};
        using var stream = new MemoryStream(data);
        var buffer = new byte[5];

        var read = stream.ReadAtLeast(buffer, 2, throwOnEndOfStream: false);

        Assert.AreEqual(3, read);
        Assert.IsTrue(buffer.AsSpan(0, 3).SequenceEqual(data));
    }

    [Test]
    public async Task ReadAtLeastAsync_ReadsPartialAndReturnsTotalRead()
    {
        var data = new byte[] {1, 2, 3};
        using var stream = new MemoryStream(data);
        var buffer = new byte[5];

        var read = await stream.ReadAtLeastAsync(buffer, 2, throwOnEndOfStream: false);

        Assert.AreEqual(3, read);
        Assert.IsTrue(buffer.AsSpan(0, 3).SequenceEqual(data));
    }

    [Test]
    public void ReadAtLeast_ThrowsOnEndOfStream_WhenThrowOnEndOfStreamTrue()
    {
        var data = new byte[] {1, 2, 3};
        using var stream = new MemoryStream(data);
        var buffer = new byte[5];

        Assert.Throws<EndOfStreamException>(() =>
            stream.ReadAtLeast(buffer, 4, throwOnEndOfStream: true));
        Assert.ThrowsAsync<EndOfStreamException>(async () =>
            await stream.ReadAtLeastAsync(buffer, 4, throwOnEndOfStream: true));
    }

    [Test]
    public async Task ReadAtLeast_EmptyBuffer_ZeroMinimum_ReturnsZero()
    {
        using var stream = new MemoryStream([]);
        var buffer = Array.Empty<byte>();

        var read = stream.ReadAtLeast(buffer, 0);

        Assert.AreEqual(0, read);

        await stream.ReadAtLeastAsync(buffer, 0);

        Assert.AreEqual(0, read);
    }

    [Test]
    public void ReadAtLeast_ThrowsOnNegativeMinimumBytes()
    {
        using var stream = new MemoryStream(new byte[1]);
        var buffer = new byte[1];

        Assert.Throws<ArgumentOutOfRangeException>(() =>
            stream.ReadAtLeast(buffer, -1));
        Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
            await stream.ReadAtLeastAsync(buffer, -1));
    }

    [Test]
    public void ReadAtLeast_ThrowsWhenMinimumGreaterThanBufferLength()
    {
        using var stream = new MemoryStream(new byte[1]);
        var buffer = new byte[1];

        Assert.Throws<ArgumentOutOfRangeException>(() =>
            stream.ReadAtLeast(buffer, 2));

        Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
            await stream.ReadAtLeastAsync(buffer, 2));
    }
#endif
}