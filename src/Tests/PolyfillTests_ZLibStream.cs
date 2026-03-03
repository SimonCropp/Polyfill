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
