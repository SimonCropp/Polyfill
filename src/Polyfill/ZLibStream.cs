#nullable enable

#if !NET6_0_OR_GREATER && FeatureCompression

#pragma warning disable

namespace System.IO.Compression;

using Diagnostics;
using Diagnostics.CodeAnalysis;
using Threading;
using Threading.Tasks;

/// <summary>
/// Provides methods and properties used to compress and decompress streams by using the zlib data format specification.
/// </summary>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
//Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.zlibstream?view=net-11.0
#if PolyPublic
public
#endif
sealed class ZLibStream : Stream
{
    Stream baseStream;
    DeflateStream? deflateStream;
    readonly CompressionMode mode;
    readonly CompressionLevel compressionLevel;
    readonly bool leaveOpen;
    bool closed;
    bool headerWritten;
    bool headerRead;
    uint adler32 = 1;

    /// <summary>
    /// Initializes a new instance of the <see cref="ZLibStream"/> class by using the specified stream and compression mode.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.zlibstream.-ctor?view=net-11.0#system-io-compression-zlibstream-ctor(system-io-stream-system-io-compression-compressionmode)
    public ZLibStream(Stream stream, CompressionMode mode)
        : this(stream, mode, false)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ZLibStream"/> class by using the specified stream, compression mode, and whether to leave the stream open.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.zlibstream.-ctor?view=net-11.0#system-io-compression-zlibstream-ctor(system-io-stream-system-io-compression-compressionmode-system-boolean)
    public ZLibStream(Stream stream, CompressionMode mode, bool leaveOpen)
    {
        baseStream = stream;
        this.mode = mode;
        this.leaveOpen = leaveOpen;
        compressionLevel = CompressionLevel.Optimal;

        if (mode == CompressionMode.Decompress)
        {
            deflateStream = new DeflateStream(stream, CompressionMode.Decompress, true);
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ZLibStream"/> class by using the specified stream and compression level.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.zlibstream.-ctor?view=net-11.0#system-io-compression-zlibstream-ctor(system-io-stream-system-io-compression-compressionlevel)
    public ZLibStream(Stream stream, CompressionLevel compressionLevel)
        : this(stream, compressionLevel, false)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ZLibStream"/> class by using the specified stream, compression level, and whether to leave the stream open.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.zlibstream.-ctor?view=net-11.0#system-io-compression-zlibstream-ctor(system-io-stream-system-io-compression-compressionlevel-system-boolean)
    public ZLibStream(Stream stream, CompressionLevel compressionLevel, bool leaveOpen)
    {
        baseStream = stream;
        mode = CompressionMode.Compress;
        this.compressionLevel = compressionLevel;
        this.leaveOpen = leaveOpen;
    }

    /// <summary>
    /// Gets a reference to the underlying stream.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.zlibstream.basestream?view=net-11.0
    public Stream BaseStream => baseStream;

    /// <inheritdoc />
    public override bool CanRead => mode == CompressionMode.Decompress && !closed;

    /// <inheritdoc />
    public override bool CanWrite => mode == CompressionMode.Compress && !closed;

    /// <inheritdoc />
    public override bool CanSeek => false;

    /// <inheritdoc />
    public override long Length => throw new NotSupportedException();

    /// <inheritdoc />
    public override long Position
    {
        get => throw new NotSupportedException();
        set => throw new NotSupportedException();
    }

    /// <inheritdoc />
    public override long Seek(long offset, SeekOrigin origin) =>
        throw new NotSupportedException();

    /// <inheritdoc />
    public override void SetLength(long value) =>
        throw new NotSupportedException();

    /// <inheritdoc />
    public override int Read(byte[] buffer, int offset, int count)
    {
        ThrowIfClosed();

        if (mode != CompressionMode.Decompress)
        {
            throw new InvalidOperationException("Reading from a compression stream is not supported.");
        }

        ReadHeader();

        return deflateStream!.Read(buffer, offset, count);
    }

    /// <inheritdoc />
    public override int ReadByte()
    {
        var buffer = new byte[1];
        var bytesRead = Read(buffer, 0, 1);
        return bytesRead == 0 ? -1 : buffer[0];
    }

    /// <inheritdoc />
    public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
    {
        ThrowIfClosed();

        if (mode != CompressionMode.Decompress)
        {
            throw new InvalidOperationException("Reading from a compression stream is not supported.");
        }

        ReadHeader();

        return deflateStream!.ReadAsync(buffer, offset, count, cancellationToken);
    }

    /// <inheritdoc />
    public override void Write(byte[] buffer, int offset, int count)
    {
        ThrowIfClosed();

        if (mode != CompressionMode.Compress)
        {
            throw new InvalidOperationException("Writing to a decompression stream is not supported.");
        }

        WriteHeader();

        UpdateAdler32(buffer, offset, count);
        deflateStream!.Write(buffer, offset, count);
    }

    /// <inheritdoc />
    public override void WriteByte(byte value) =>
        Write([value], 0, 1);

    /// <inheritdoc />
    public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
    {
        ThrowIfClosed();

        if (mode != CompressionMode.Compress)
        {
            throw new InvalidOperationException("Writing to a decompression stream is not supported.");
        }

        WriteHeader();

        UpdateAdler32(buffer, offset, count);
        return deflateStream!.WriteAsync(buffer, offset, count, cancellationToken);
    }

    /// <inheritdoc />
    public override void Flush()
    {
        ThrowIfClosed();
        deflateStream?.Flush();
    }

    /// <inheritdoc />
    public override Task FlushAsync(CancellationToken cancellationToken)
    {
        ThrowIfClosed();
        return deflateStream?.FlushAsync(cancellationToken) ?? Task.CompletedTask;
    }

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        if (disposing && !closed)
        {
            closed = true;

            if (mode == CompressionMode.Compress)
            {
                WriteHeader();
                deflateStream!.Dispose();
                deflateStream = null;

                WriteTrailer();
            }
            else
            {
                deflateStream?.Dispose();
                deflateStream = null;
            }

            if (!leaveOpen)
            {
                baseStream.Dispose();
            }
        }

        base.Dispose(disposing);
    }

    void ReadHeader()
    {
        if (headerRead)
        {
            return;
        }

        headerRead = true;

        // zlib header is 2 bytes: CMF and FLG
        var cmf = baseStream.ReadByte();
        var flg = baseStream.ReadByte();

        if (cmf == -1 || flg == -1)
        {
            throw new InvalidDataException("Unexpected end of zlib stream header.");
        }

        // Validate: CMF * 256 + FLG must be divisible by 31
        if ((cmf * 256 + flg) % 31 != 0)
        {
            throw new InvalidDataException("Invalid zlib stream header.");
        }

        // Check for preset dictionary (FDICT bit 5 of FLG)
        if ((flg & 0x20) != 0)
        {
            // Skip 4-byte dictionary ID
            for (var i = 0; i < 4; i++)
            {
                if (baseStream.ReadByte() == -1)
                {
                    throw new InvalidDataException("Unexpected end of zlib stream header.");
                }
            }
        }
    }

    void WriteHeader()
    {
        if (headerWritten)
        {
            return;
        }

        headerWritten = true;

        // CMF: CM=8 (deflate), CINFO=7 (32K window) => 0x78
        // FLG: Standard default: CMF=0x78, FLG=0x9C => (0x78*256 + 0x9C) = 30876, 30876 % 31 = 0
        baseStream.WriteByte(0x78);
        baseStream.WriteByte(0x9C);

        // Create DeflateStream after writing the zlib header
        deflateStream = new DeflateStream(baseStream, compressionLevel, true);
    }

    void WriteTrailer()
    {
        // Write Adler-32 checksum in big-endian
        var checksum = adler32;
        baseStream.WriteByte((byte)(checksum >> 24));
        baseStream.WriteByte((byte)(checksum >> 16));
        baseStream.WriteByte((byte)(checksum >> 8));
        baseStream.WriteByte((byte)checksum);
        baseStream.Flush();
    }

    void UpdateAdler32(byte[] buffer, int offset, int count)
    {
        const uint modAdler = 65521;
        var a = adler32 & 0xFFFF;
        var b = (adler32 >> 16) & 0xFFFF;

        for (var i = offset; i < offset + count; i++)
        {
            a = (a + buffer[i]) % modAdler;
            b = (b + a) % modAdler;
        }

        adler32 = (b << 16) | a;
    }

    void ThrowIfClosed()
    {
        if (closed)
        {
            throw new ObjectDisposedException(nameof(ZLibStream));
        }
    }
}

#endif

#if NET6_0_OR_GREATER
[assembly: System.Runtime.CompilerServices.TypeForwardedTo(typeof(System.IO.Compression.ZLibStream))]
#endif
