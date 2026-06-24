#nullable enable

// === Polyfill target window =====================================================================
// System.Buffers.ReadOnlySequenceStream was approved and merged into dotnet/runtime for net11
// (https://github.com/dotnet/runtime/pull/126669) but is NOT in the current net11 preview/RC SDK,
// so this polyfill is intentionally ACTIVE on net11 as well.
// When you upgrade to a net11 SDK that actually ships this type, the build will collide (CS0436)
// and the ApiBuilderTests "StreamWrapperBclDetectionTests" test FAILS with these exact steps.
// To retire this polyfill for net11:
//   1. change `#if FeatureMemory` below to `#if FeatureMemory && !NET11_0_OR_GREATER`
//   2. add at the bottom of this file (after the final #endif):
//        #if NET11_0_OR_GREATER
//        [assembly: System.Runtime.CompilerServices.TypeForwardedTo(typeof(System.Buffers.ReadOnlySequenceStream))]
//        #endif
//   3. re-run ApiBuilderTests in Debug to regenerate Split + api_list.
// ================================================================================================
#if FeatureMemory

#pragma warning disable

namespace System.Buffers;

using System.IO;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Provides a seekable, read-only <see cref="Stream"/> over a <see cref="ReadOnlySequence{Byte}"/>.
/// </summary>
/// <remarks>
/// The underlying sequence is not copied; reads are served directly from its segments.
/// The stream cannot be written to. <see cref="CanWrite"/> always returns <see langword="false"/>.
/// </remarks>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
//Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.readonlysequencestream?view=net-11.0
#if PolyPublic
public
#endif
sealed class ReadOnlySequenceStream :
    Stream
{
    ReadOnlySequence<byte> sequence;
    long position;
    bool disposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="ReadOnlySequenceStream"/> class over the specified <see cref="ReadOnlySequence{Byte}"/>.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.readonlysequencestream.-ctor?view=net-11.0
    public ReadOnlySequenceStream(ReadOnlySequence<byte> source) =>
        sequence = source;

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
            return sequence.Length;
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

        if (position >= sequence.Length)
        {
            return 0;
        }

        var remaining = sequence.Slice(position);
        var toRead = (int)Math.Min(remaining.Length, count);
        if (toRead <= 0)
        {
            return 0;
        }

        remaining.Slice(0, toRead).CopyTo(buffer.AsSpan(offset, toRead));
        position += toRead;
        return toRead;
    }

    /// <inheritdoc/>
    public override int ReadByte()
    {
        ThrowIfDisposed();

        if (position >= sequence.Length)
        {
            return -1;
        }

        var result = sequence.Slice(position, 1).First.Span[0];
        position++;
        return result;
    }

    /// <inheritdoc/>
    public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
    {
        GuardRange(buffer, offset, count);

        if (cancellationToken.IsCancellationRequested)
        {
            return Task.FromCanceled<int>(cancellationToken);
        }

        try
        {
            return Task.FromResult(Read(buffer, offset, count));
        }
        catch (Exception exception)
        {
            return Task.FromException<int>(exception);
        }
    }

    /// <inheritdoc/>
    public override long Seek(long offset, SeekOrigin origin)
    {
        ThrowIfDisposed();

        var basePosition = origin switch
        {
            SeekOrigin.Begin => 0L,
            SeekOrigin.Current => position,
            SeekOrigin.End => sequence.Length,
            _ => throw new ArgumentException("Invalid seek origin.", nameof(origin))
        };

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
        sequence = default;
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
