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
using System.Runtime.InteropServices;
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
    // Incremental cursor into the sequence's segments, kept in sync with the absolute position.
    // Advancing from this cursor avoids re-walking the segment list from the start on every read.
    SequencePosition cursor;
    long position;
    bool disposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="ReadOnlySequenceStream"/> class over the specified <see cref="ReadOnlySequence{Byte}"/>.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.readonlysequencestream.-ctor?view=net-11.0
    public ReadOnlySequenceStream(ReadOnlySequence<byte> source)
    {
        sequence = source;
        cursor = source.Start;
        position = 0;
    }

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

            MoveTo(value);
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

        var remaining = sequence.Slice(cursor);
        var toRead = (int)Math.Min(remaining.Length, count);
        if (toRead <= 0)
        {
            return 0;
        }

        remaining.Slice(0, toRead).CopyTo(buffer.AsSpan(offset, toRead));
        cursor = sequence.GetPosition(toRead, cursor);
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

        var result = sequence.Slice(cursor, 1).First.Span[0];
        cursor = sequence.GetPosition(1, cursor);
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

        if (offset > long.MaxValue - basePosition)
        {
            throw new ArgumentOutOfRangeException(nameof(offset));
        }

        var newPosition = basePosition + offset;
        if (newPosition < 0)
        {
            throw new IOException("An attempt was made to move the position before the beginning of the stream.");
        }

        MoveTo(newPosition);
        return position;
    }

    // Repositions the segment cursor to the given absolute position, advancing forward from the
    // current cursor when possible and only walking from the start for backward jumps.
    void MoveTo(long value)
    {
        if (value >= sequence.Length)
        {
            cursor = sequence.End;
        }
        else if (value >= position)
        {
            cursor = sequence.GetPosition(value - position, cursor);
        }
        else
        {
            cursor = sequence.GetPosition(value, sequence.Start);
        }

        position = value;
    }

#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
    // Stream.CopyTo(Stream, int) only became virtual in netcoreapp2.1/netstandard2.1. On older
    // targets it cannot be overridden, so the base implementation (which routes through the
    // cursor-based Read above) is used instead.
    /// <inheritdoc/>
    public override void CopyTo(Stream destination, int bufferSize)
    {
        GuardCopyTo(destination, bufferSize);
        ThrowIfDisposed();

        if (position >= sequence.Length)
        {
            return;
        }

        foreach (var segment in sequence.Slice(cursor))
        {
            destination.Write(segment.Span);
        }

        cursor = sequence.End;
        position = sequence.Length;
    }
#endif

    /// <inheritdoc/>
    public override Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
    {
        GuardCopyTo(destination, bufferSize);
        ThrowIfDisposed();

        if (cancellationToken.IsCancellationRequested)
        {
            return Task.FromCanceled(cancellationToken);
        }

        if (position >= sequence.Length)
        {
            return Task.CompletedTask;
        }

        return CopyToAsyncCore(destination, cancellationToken);
    }

    async Task CopyToAsyncCore(Stream destination, CancellationToken cancellationToken)
    {
        foreach (var segment in sequence.Slice(cursor))
        {
            await WriteSegmentAsync(destination, segment, cancellationToken).ConfigureAwait(false);
        }

        cursor = sequence.End;
        position = sequence.Length;
    }

    static Task WriteSegmentAsync(Stream destination, ReadOnlyMemory<byte> segment, CancellationToken cancellationToken)
    {
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        return destination.WriteAsync(segment, cancellationToken).AsTask();
#else
        var array = GetSegmentArray(segment, out var offset, out var count);
        return destination.WriteAsync(array, offset, count, cancellationToken);
#endif
    }

#if !(NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER)
    static byte[] GetSegmentArray(ReadOnlyMemory<byte> segment, out int offset, out int count)
    {
        if (MemoryMarshal.TryGetArray(segment, out var arraySegment) &&
            arraySegment.Array != null)
        {
            offset = arraySegment.Offset;
            count = arraySegment.Count;
            return arraySegment.Array;
        }

        var array = segment.ToArray();
        offset = 0;
        count = array.Length;
        return array;
    }
#endif

    static void GuardCopyTo(Stream destination, int bufferSize)
    {
        if (destination == null)
        {
            throw new ArgumentNullException(nameof(destination));
        }

        if (bufferSize <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(bufferSize));
        }

        if (!destination.CanWrite)
        {
            if (destination.CanRead)
            {
                throw new NotSupportedException("Stream does not support writing.");
            }

            throw new ObjectDisposedException(null, "Cannot access a closed stream.");
        }
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
        cursor = default;
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
