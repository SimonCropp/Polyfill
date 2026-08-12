#nullable enable

// Ships in the BCL from net11 (System.IO.ReadOnlyMemoryStream, dotnet/runtime#126669). This polyfill covers
// pre-net11 targets; on net11+ the runtime provides the type and it is forwarded at the end of this file.
#if FeatureMemory && !NET11_0_OR_GREATER

#pragma warning disable

namespace System.IO;

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Provides a seekable, read-only <see cref="Stream"/> over a <see cref="ReadOnlyMemory{Byte}"/>.
/// </summary>
/// <remarks>
/// The underlying memory is not copied; reads are served directly from it.
/// The stream cannot be written to. <see cref="CanWrite"/> always returns <see langword="false"/>.
/// </remarks>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
//Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.readonlymemorystream?view=net-11.0
#if PolyPublic
public
#endif
sealed class ReadOnlyMemoryStream :
    Stream
{
    ReadOnlyMemory<byte> memory;
    long position;
    bool disposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="ReadOnlyMemoryStream"/> class over the specified <see cref="ReadOnlyMemory{Byte}"/>.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.readonlymemorystream.-ctor?view=net-11.0
    public ReadOnlyMemoryStream(ReadOnlyMemory<byte> source) =>
        memory = source;

    /// <inheritdoc/>
    public override bool CanRead => !disposed;

    /// <inheritdoc/>
    public override bool CanSeek => !disposed;

    /// <inheritdoc/>
    public override bool CanWrite => false;

    /// <inheritdoc/>
    public override long Length
    {
        get
        {
            ThrowIfDisposed();
            return memory.Length;
        }
    }

    /// <inheritdoc/>
    public override long Position
    {
        get
        {
            ThrowIfDisposed();
            return position;
        }
        set
        {
            ThrowIfDisposed();
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            position = value;
        }
    }

    /// <inheritdoc/>
    public override int Read(byte[] buffer, int offset, int count)
    {
        GuardRange(buffer, offset, count);
        ThrowIfDisposed();

        var toRead = Remaining(count);
        if (toRead == 0)
        {
            return 0;
        }

        memory.Span.Slice((int)position, toRead).CopyTo(buffer.AsSpan(offset, toRead));
        position += toRead;
        return toRead;
    }

#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
    // Stream only exposes the span based overloads as virtuals from netcoreapp2.1/netstandard2.1. On older
    // targets the Polyfill extension methods route through the array based Read above instead.
    /// <inheritdoc/>
    public override int Read(Span<byte> buffer)
    {
        ThrowIfDisposed();

        var toRead = Remaining(buffer.Length);
        if (toRead == 0)
        {
            return 0;
        }

        memory.Span.Slice((int)position, toRead).CopyTo(buffer);
        position += toRead;
        return toRead;
    }
#endif

    /// <inheritdoc/>
    public override int ReadByte()
    {
        ThrowIfDisposed();

        if (position >= memory.Length)
        {
            return -1;
        }

        var result = memory.Span[(int)position];
        position++;
        return result;
    }

    /// <inheritdoc/>
    public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
    {
        GuardRange(buffer, offset, count);
        ThrowIfDisposed();

        if (cancellationToken.IsCancellationRequested)
        {
            return Task.FromCanceled<int>(cancellationToken);
        }

        return Task.FromResult(Read(buffer, offset, count));
    }

    // The number of bytes a read of the requested size can serve from the current position.
    int Remaining(int count)
    {
        if (position >= memory.Length)
        {
            return 0;
        }

        return (int)Math.Min(memory.Length - position, count);
    }

    /// <inheritdoc/>
    public override long Seek(long offset, SeekOrigin origin)
    {
        ThrowIfDisposed();

        var basePosition = origin switch
        {
            SeekOrigin.Begin => 0L,
            SeekOrigin.Current => position,
            SeekOrigin.End => memory.Length,
            _ => throw new ArgumentException("Invalid seek origin.", nameof(origin))
        };

        if (offset > long.MaxValue - basePosition)
        {
            throw new ArgumentOutOfRangeException(nameof(offset));
        }

        var newPosition = basePosition + offset;
        if (newPosition < 0)
        {
            throw new IOException("An attempt was made to move the position before the beginning of the stream.");
        }

        position = newPosition;
        return position;
    }

    /// <inheritdoc/>
    public override void Flush()
    {
    }

    /// <inheritdoc/>
    public override Task FlushAsync(CancellationToken cancellationToken) =>
        cancellationToken.IsCancellationRequested ? Task.FromCanceled(cancellationToken) : Task.CompletedTask;

    /// <inheritdoc/>
    public override void SetLength(long value) =>
        throw new NotSupportedException("Stream does not support writing.");

    /// <inheritdoc/>
    public override void Write(byte[] buffer, int offset, int count) =>
        throw new NotSupportedException("Stream does not support writing.");

    /// <inheritdoc/>
    protected override void Dispose(bool disposing)
    {
        disposed = true;
        memory = default;
        base.Dispose(disposing);
    }

    void ThrowIfDisposed()
    {
        if (disposed)
        {
            throw new ObjectDisposedException(GetType().FullName);
        }
    }

    static void GuardRange(byte[] buffer, int offset, int count)
    {
        if (buffer == null)
        {
            throw new ArgumentNullException(nameof(buffer));
        }

        if (offset < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(offset));
        }

        if (count < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(count));
        }

        if (buffer.Length - offset < count)
        {
            throw new ArgumentException("The sum of offset and count is larger than the buffer length.");
        }
    }
}

#endif

#if NET11_0_OR_GREATER
[assembly: System.Runtime.CompilerServices.TypeForwardedTo(typeof(System.IO.ReadOnlyMemoryStream))]
#endif
