#if FeatureCompression
using System.IO.Compression;

partial class PolyfillTests
{
    [Test]
    public async Task ZLibStream_CompressDecompress_Roundtrip()
    {
        var original = "Hello, ZLibStream polyfill!"u8.ToArray();

        using var compressedStream = new MemoryStream();
        using (var zlibCompress = new ZLibStream(compressedStream, CompressionMode.Compress, true))
        {
            zlibCompress.Write(original, 0, original.Length);
        }

        compressedStream.Position = 0;

        using var decompressedStream = new MemoryStream();
        using (var zlibDecompress = new ZLibStream(compressedStream, CompressionMode.Decompress, true))
        {
            zlibDecompress.CopyTo(decompressedStream);
        }

        var result = decompressedStream.ToArray();
        await Assert.That(result).IsEquivalentTo(original);
    }

    [Test]
    public async Task ZLibStream_CompressWithLevel_Roundtrip()
    {
        var original = "Compressed with specific level"u8.ToArray();

        using var compressedStream = new MemoryStream();
        using (var zlibCompress = new ZLibStream(compressedStream, CompressionLevel.Fastest, true))
        {
            zlibCompress.Write(original, 0, original.Length);
        }

        compressedStream.Position = 0;

        using var decompressedStream = new MemoryStream();
        using (var zlibDecompress = new ZLibStream(compressedStream, CompressionMode.Decompress, true))
        {
            zlibDecompress.CopyTo(decompressedStream);
        }

        var result = decompressedStream.ToArray();
        await Assert.That(result).IsEquivalentTo(original);
    }

    [Test]
    public async Task ZLibStream_EmptyData_Roundtrip()
    {
        var original = Array.Empty<byte>();

        using var compressedStream = new MemoryStream();
        using (var zlibCompress = new ZLibStream(compressedStream, CompressionMode.Compress, true))
        {
            // Write nothing
        }

        compressedStream.Position = 0;

        using var decompressedStream = new MemoryStream();
        using (var zlibDecompress = new ZLibStream(compressedStream, CompressionMode.Decompress, true))
        {
            zlibDecompress.CopyTo(decompressedStream);
        }

        var result = decompressedStream.ToArray();
        await Assert.That(result).IsEmpty();
    }

    [Test]
    public async Task ZLibStream_LeaveOpen_True_DoesNotCloseBaseStream()
    {
        using var baseStream = new MemoryStream();
        using (var zlibStream = new ZLibStream(baseStream, CompressionMode.Compress, leaveOpen: true))
        {
            zlibStream.Write([1, 2, 3], 0, 3);
        }

        // Base stream should still be usable
        await Assert.That(baseStream.CanRead).IsTrue();
    }

    [Test]
    public async Task ZLibStream_LeaveOpen_False_ClosesBaseStream()
    {
        var baseStream = new MemoryStream();
        using (var zlibStream = new ZLibStream(baseStream, CompressionMode.Compress, leaveOpen: false))
        {
            zlibStream.Write([1, 2, 3], 0, 3);
        }

        await Assert.That(baseStream.CanRead).IsFalse();
    }

    [Test]
    public async Task ZLibStream_AsyncRoundtrip()
    {
        var original = "Async roundtrip test data"u8.ToArray();

        using var compressedStream = new MemoryStream();
        using (var zlibCompress = new ZLibStream(compressedStream, CompressionMode.Compress, true))
        {
            await zlibCompress.WriteAsync(original, 0, original.Length);
        }

        compressedStream.Position = 0;

        using var decompressedStream = new MemoryStream();
        using (var zlibDecompress = new ZLibStream(compressedStream, CompressionMode.Decompress, true))
        {
            await zlibDecompress.CopyToAsync(decompressedStream);
        }

        var result = decompressedStream.ToArray();
        await Assert.That(result).IsEquivalentTo(original);
    }

    // The zlib trailer is the Adler-32 of the uncompressed data, big-endian, in the last four bytes.
    // Returns 0 (never a valid Adler-32) when the stream produced no output at all.
    static uint ZLibTrailer(Action<ZLibStream> write, out byte[] compressed)
    {
        using var compressedStream = new MemoryStream();
        using (var zlib = new ZLibStream(compressedStream, CompressionLevel.Fastest, true))
        {
            write(zlib);
        }

        compressed = compressedStream.ToArray();
        var length = compressed.Length;
        if (length == 0)
        {
            return 0;
        }

        return ((uint) compressed[length - 4] << 24) |
               ((uint) compressed[length - 3] << 16) |
               ((uint) compressed[length - 2] << 8) |
               compressed[length - 1];
    }

    // Adler-32 as defined by RFC 1950, reducing after every byte
    static uint ReferenceAdler32(byte[] data)
    {
        uint a = 1;
        uint b = 0;
        foreach (var value in data)
        {
            a = (a + value) % 65521;
            b = (b + a) % 65521;
        }

        return (b << 16) | a;
    }

    static byte[] ZLibDecompress(byte[] compressed)
    {
        using var decompressed = new MemoryStream();
        using (var zlib = new ZLibStream(new MemoryStream(compressed), CompressionMode.Decompress))
        {
            zlib.CopyTo(decompressed);
        }

        return decompressed.ToArray();
    }

    [Test]
    [Arguments("", 0x00000001u)]
    [Arguments("a", 0x00620062u)]
    [Arguments("abc", 0x024D0127u)]
    [Arguments("message digest", 0x29750586u)]
    [Arguments("abcdefghijklmnopqrstuvwxyz", 0x90860B20u)]
    [Arguments("Wikipedia", 0x11E60398u)]
    public async Task ZLibStream_Adler32_KnownVectors(string text, uint expected)
    {
        var data = System.Text.Encoding.ASCII.GetBytes(text);

        var trailer = ZLibTrailer(_ => _.Write(data, 0, data.Length), out var compressed);

        // Newer runtimes emit nothing at all, not even the header, when no data was written
        if (data.Length == 0 && compressed.Length == 0)
        {
            return;
        }

        await Assert.That(trailer).IsEqualTo(expected);
        await Assert.That(ZLibDecompress(compressed).AsSpan().SequenceEqual(data)).IsTrue();
    }

    // Sizes around the 5552 byte block after which the sums have to be reduced. All 0xFF is the
    // input that overflows soonest if the reduction is deferred for too long.
    [Test]
    [Arguments(1)]
    [Arguments(5551)]
    [Arguments(5552)]
    [Arguments(5553)]
    [Arguments(11103)]
    [Arguments(11104)]
    [Arguments(11105)]
    [Arguments(65521)]
    [Arguments(1000003)]
    public async Task ZLibStream_Adler32_AllOnes_MatchesReference(int length)
    {
        var data = new byte[length];
        for (var index = 0; index < data.Length; index++)
        {
            data[index] = 0xFF;
        }

        var trailer = ZLibTrailer(_ => _.Write(data, 0, data.Length), out var compressed);

        await Assert.That(trailer).IsEqualTo(ReferenceAdler32(data));
        await Assert.That(ZLibDecompress(compressed).AsSpan().SequenceEqual(data)).IsTrue();
    }

    [Test]
    public async Task ZLibStream_Adler32_AllOnes_KnownValues()
    {
        // 5552 and 5553 bytes of 0xFF: the last block size that fits a uint, and one past it
        var data = new byte[5553];
        for (var index = 0; index < data.Length; index++)
        {
            data[index] = 0xFF;
        }

        await Assert.That(ZLibTrailer(_ => _.Write(data, 0, 5552), out _)).IsEqualTo(0xF18F9B8Cu);
        await Assert.That(ZLibTrailer(_ => _.Write(data, 0, 5553), out _)).IsEqualTo(0x8E299C8Bu);
    }

    [Test]
    public async Task ZLibStream_Adler32_RandomData_MatchesReference()
    {
        var random = new Random(8675309);
        foreach (var length in new[] {1, 2, 255, 4096, 5552, 5553, 70000, 300001})
        {
            var data = new byte[length];
            random.NextBytes(data);

            var trailer = ZLibTrailer(_ => _.Write(data, 0, data.Length), out var compressed);

            await Assert.That(trailer).IsEqualTo(ReferenceAdler32(data));
            await Assert.That(ZLibDecompress(compressed).AsSpan().SequenceEqual(data)).IsTrue();
        }
    }

    [Test]
    public async Task ZLibStream_Adler32_OffsetAndCountAreHonoured()
    {
        var random = new Random(42);
        var buffer = new byte[20000];
        random.NextBytes(buffer);
        var data = buffer.AsSpan(3, 12345).ToArray();

        var trailer = ZLibTrailer(_ => _.Write(buffer, 3, 12345), out var compressed);

        await Assert.That(trailer).IsEqualTo(ReferenceAdler32(data));
        await Assert.That(ZLibDecompress(compressed).AsSpan().SequenceEqual(data)).IsTrue();
    }

    // The running checksum is carried from one write to the next, whatever the write sizes are
    [Test]
    [Arguments(1)]
    [Arguments(7)]
    [Arguments(5551)]
    [Arguments(5552)]
    [Arguments(5553)]
    [Arguments(8192)]
    public async Task ZLibStream_Adler32_SplitWrites_MatchReference(int chunk)
    {
        var data = new byte[chunk == 1 ? 12000 : 100003];
        for (var index = 0; index < data.Length; index++)
        {
            data[index] = (byte) (255 - index % 3);
        }

        var trailer = ZLibTrailer(
            zlib =>
            {
                for (var offset = 0; offset < data.Length; offset += chunk)
                {
                    zlib.Write(data, offset, Math.Min(chunk, data.Length - offset));
                }
            },
            out var compressed);

        await Assert.That(trailer).IsEqualTo(ReferenceAdler32(data));
        await Assert.That(ZLibDecompress(compressed).AsSpan().SequenceEqual(data)).IsTrue();
    }

    [Test]
    public async Task ZLibStream_Adler32_WriteByteAndEmptyWrites_MatchReference()
    {
        var data = new byte[6000];
        new Random(7).NextBytes(data);

        var trailer = ZLibTrailer(
            zlib =>
            {
                foreach (var value in data)
                {
                    zlib.WriteByte(value);
                    zlib.Write(data, 0, 0);
                }
            },
            out var compressed);

        await Assert.That(trailer).IsEqualTo(ReferenceAdler32(data));
        await Assert.That(ZLibDecompress(compressed).AsSpan().SequenceEqual(data)).IsTrue();
    }

    [Test]
    public async Task ZLibStream_Adler32_WriteAsync_MatchesReference()
    {
        var data = new byte[50000];
        new Random(99).NextBytes(data);

        using var compressedStream = new MemoryStream();
        using (var zlib = new ZLibStream(compressedStream, CompressionLevel.Fastest, true))
        {
            await zlib.WriteAsync(data, 0, 20000);
            await zlib.WriteAsync(data, 20000, 30000);
        }

        var compressed = compressedStream.ToArray();
        var length = compressed.Length;
        var trailer = ((uint) compressed[length - 4] << 24) |
                      ((uint) compressed[length - 3] << 16) |
                      ((uint) compressed[length - 2] << 8) |
                      compressed[length - 1];

        await Assert.That(trailer).IsEqualTo(ReferenceAdler32(data));
        await Assert.That(ZLibDecompress(compressed).AsSpan().SequenceEqual(data)).IsTrue();
    }

    [Test]
    public async Task ZLibStream_Properties()
    {
        using var baseStream = new MemoryStream();
        using var zlibCompress = new ZLibStream(baseStream, CompressionMode.Compress, true);

        await Assert.That(zlibCompress.CanWrite).IsTrue();
        await Assert.That(zlibCompress.CanRead).IsFalse();
        await Assert.That(zlibCompress.CanSeek).IsFalse();
        await Assert.That(zlibCompress.BaseStream).IsEqualTo(baseStream);
    }
}
#endif
