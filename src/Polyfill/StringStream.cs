#nullable enable

// Ships in the BCL from net11 (System.IO.StringStream, dotnet/runtime#126669). This polyfill covers
// pre-net11 targets; on net11+ the runtime provides the type and it is forwarded at the end of this file.
#if FeatureMemory && !NET11_0_OR_GREATER

#pragma warning disable

namespace System.IO;

using System.Buffers;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Provides a read-only, non-seekable <see cref="Stream"/> that encodes a <see cref="string"/> or
/// <see cref="ReadOnlyMemory{Char}"/> into bytes using a specified <see cref="System.Text.Encoding"/>.
/// </summary>
/// <remarks>
/// This stream never emits a byte order mark (BOM). Callers who need a BOM can prepend it themselves.
/// The text is encoded incrementally as the stream is read rather than being buffered up front, so the
/// peak memory cost is bounded by the caller's read buffer rather than the full encoded length. A
/// stateful <see cref="Encoder"/> preserves conversion state across reads, so multi-byte characters
/// (including surrogate pairs) that straddle a buffer boundary are encoded correctly.
/// </remarks>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
//Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.stringstream?view=net-11.0
#if PolyPublic
public
#endif
sealed class StringStream :
    Stream
{
    ReadOnlyMemory<char> text;
    Encoding encoding;
    int maxBytesPerChar;
    // Lazily created on the encoder slow path. The single-shot fast path in ReadCore
    // uses stateless Encoding.GetBytes and never touches this field.
    Encoder? encoder;
    int charPosition;
    bool encoderFlushed;
    bool disposed;

    // Spillover buffer for multi-byte encodings: when the caller's buffer is too small to hold even
    // one encoded scalar (for example ReadByte with UTF-16), the bytes are encoded into this buffer
    // and served across subsequent reads. Also holds final encoder flush bytes when the caller's
    // buffer had no room left.
    byte[]? pendingBytes;
    int pendingOffset;
    int pendingCount;
    byte[]? singleByteBuffer;

    /// <summary>
    /// Initializes a new instance of the <see cref="StringStream"/> class with the specified string and encoding.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.stringstream.-ctor?view=net-11.0#system-io-stringstream-ctor(system-string-system-text-encoding)
    public StringStream(string text, Encoding encoding)
    {
        if (text == null)
        {
            throw new ArgumentNullException(nameof(text));
        }

        if (encoding == null)
        {
            throw new ArgumentNullException(nameof(encoding));
        }

        this.text = text.AsMemory();
        this.encoding = encoding;
        maxBytesPerChar = encoding.GetMaxByteCount(1);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="StringStream"/> class with the specified character memory and encoding.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.stringstream.-ctor?view=net-11.0#system-io-stringstream-ctor(system-readonlymemory((system-char))-system-text-encoding)
    public StringStream(ReadOnlyMemory<char> text, Encoding encoding)
    {
        if (encoding == null)
        {
            throw new ArgumentNullException(nameof(encoding));
        }

        this.text = text;
        this.encoding = encoding;
        maxBytesPerChar = encoding.GetMaxByteCount(1);
    }

    /// <summary>
    /// Gets the encoding used by this stream.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.stringstream.encoding?view=net-11.0
    public Encoding Encoding => encoding;

    /// <inheritdoc/>
    public override bool CanRead => !disposed;

    /// <inheritdoc/>
    public override bool CanSeek => false;

    /// <inheritdoc/>
    public override bool CanWrite => false;

    /// <inheritdoc/>
    public override long Length => throw new NotSupportedException("Stream does not support seeking.");

    /// <inheritdoc/>
    public override long Position
    {
        get => throw new NotSupportedException("Stream does not support seeking.");
        set => throw new NotSupportedException("Stream does not support seeking.");
    }

    /// <inheritdoc/>
    public override int Read(byte[] buffer, int offset, int count)
    {
        GuardRange(buffer, offset, count);
        return ReadCore(buffer, offset, count);
    }

    /// <inheritdoc/>
    public override int ReadByte()
    {
        var single = singleByteBuffer ??= new byte[1];
        return ReadCore(single, 0, 1) > 0 ? single[0] : -1;
    }

    int ReadCore(byte[] buffer, int offset, int count)
    {
        ThrowIfDisposed();

        if (count == 0 ||
            (charPosition >= text.Length && pendingCount == 0 && encoderFlushed))
        {
            return 0;
        }

        // Fast path: nothing emitted yet and the caller's buffer is guaranteed large enough to hold
        // the entire encoded payload in a single shot. Encoding.GetBytes is stateless and emits any
        // reset/shift sequences required by stateful encodings for a complete conversion, so the
        // encoder can be marked flushed without ever being allocated. The overflow guard keeps
        // Encoding.GetMaxByteCount from overflowing int for very large inputs.
        if (charPosition == 0 &&
            pendingCount == 0 &&
            text.Length <= (int.MaxValue / maxBytesPerChar) - 1 &&
            count >= encoding.GetMaxByteCount(text.Length))
        {
            var written = EncodeAll(buffer, offset, count);
            charPosition = text.Length;
            encoderFlushed = true;
            return written;
        }

        var totalBytesWritten = 0;

        // Drain any pending bytes left over from a previous partial read.
        if (pendingCount > 0)
        {
            var toCopy = Math.Min(pendingCount, count);
            Array.Copy(pendingBytes!, pendingOffset, buffer, offset, toCopy);
            pendingOffset += toCopy;
            pendingCount -= toCopy;
            totalBytesWritten += toCopy;

            if (totalBytesWritten == count)
            {
                return totalBytesWritten;
            }
        }

        if (charPosition < text.Length)
        {
            var remainingChars = text.Length - charPosition;
            var availableBytes = count - totalBytesWritten;

            // If the caller's buffer may be too small for even one encoded scalar, encode into the
            // spillover buffer first, then copy what fits. The array based Encoder.Convert throws
            // when the output cannot hold a single complete encoded character.
            if (availableBytes < maxBytesPerChar)
            {
                pendingBytes ??= new byte[encoding.GetMaxByteCount(2)];
                var charsToEncode = Math.Min(2, remainingChars);
                Convert(charPosition, charsToEncode, pendingBytes, 0, pendingBytes.Length, flush: false, out var charsUsed, out var bytesUsed);
                charPosition += charsUsed;

                var toCopy = Math.Min(bytesUsed, availableBytes);
                Array.Copy(pendingBytes, 0, buffer, offset + totalBytesWritten, toCopy);
                totalBytesWritten += toCopy;

                pendingOffset = toCopy;
                pendingCount = bytesUsed - toCopy;
            }
            else
            {
                // Encode directly into the caller's buffer. Only flush on the final block so encoder
                // state is preserved for stateful encodings.
                Convert(charPosition, remainingChars, buffer, offset + totalBytesWritten, availableBytes, flush: false, out var charsUsed, out var bytesUsed);
                charPosition += charsUsed;
                totalBytesWritten += bytesUsed;
            }
        }

        // All input chars are consumed but the encoder has not been flushed: emit any remaining
        // encoder state (for example stateful reset sequences). Flush into the spillover buffer,
        // which is always large enough, then copy what fits into whatever room the caller has left.
        if (charPosition >= text.Length &&
            !encoderFlushed &&
            pendingCount == 0)
        {
            pendingBytes ??= new byte[encoding.GetMaxByteCount(2)];
            Convert(text.Length, 0, pendingBytes, 0, pendingBytes.Length, flush: true, out _, out var flushBytes);
            encoderFlushed = true;

            if (flushBytes > 0)
            {
                var available = count - totalBytesWritten;
                var toCopy = Math.Min(flushBytes, available);
                if (toCopy > 0)
                {
                    Array.Copy(pendingBytes, 0, buffer, offset + totalBytesWritten, toCopy);
                    totalBytesWritten += toCopy;
                }

                if (toCopy < flushBytes)
                {
                    pendingOffset = toCopy;
                    pendingCount = flushBytes - toCopy;
                }
            }
        }

        return totalBytesWritten;
    }

    Encoder GetEncoder() => encoder ??= encoding.GetEncoder();

    // Stateless single-shot encode of the whole text into the caller's buffer at byteIndex.
    int EncodeAll(byte[] bytes, int byteIndex, int byteCount)
    {
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        return encoding.GetBytes(text.Span, bytes.AsSpan(byteIndex, byteCount));
#else
        // The span based Encoding.GetBytes does not exist here, so reach the backing string or array
        // rather than copying the text.
        if (MemoryMarshal.TryGetString(text, out var value, out var start, out var length))
        {
            return encoding.GetBytes(value, start, length, bytes, byteIndex);
        }

        if (MemoryMarshal.TryGetArray(text, out var segment))
        {
            if (segment.Count == 0)
            {
                return encoding.GetBytes(Array.Empty<char>(), 0, 0, bytes, byteIndex);
            }

            return encoding.GetBytes(segment.Array!, segment.Offset, segment.Count, bytes, byteIndex);
        }

        var rented = ArrayPool<char>.Shared.Rent(text.Length);
        try
        {
            text.Span.CopyTo(rented);
            return encoding.GetBytes(rented, 0, text.Length, bytes, byteIndex);
        }
        finally
        {
            ArrayPool<char>.Shared.Return(rented);
        }
#endif
    }

    // Stateful incremental encode of text[charIndex..charIndex+charCount] into bytes[byteIndex..byteIndex+byteCount].
    void Convert(int charIndex, int charCount, byte[] bytes, int byteIndex, int byteCount, bool flush, out int charsUsed, out int bytesUsed)
    {
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        GetEncoder().Convert(text.Span.Slice(charIndex, charCount), bytes.AsSpan(byteIndex, byteCount), flush, out charsUsed, out bytesUsed, out _);
#else
        // The array based Encoder.Convert overload (available on every TFM) requires a char[]. Cap the
        // slice at byteCount chars: a complete char never encodes to fewer than one byte, so the
        // encoder can never consume more chars than there are output bytes, and bounding the slice
        // this way keeps a streamed read linear rather than quadratic.
        charCount = Math.Min(charCount, byteCount);
        if (charCount == 0)
        {
            GetEncoder().Convert(Array.Empty<char>(), 0, 0, bytes, byteIndex, byteCount, flush, out charsUsed, out bytesUsed, out _);
            return;
        }

        // Array backed text is handed to the encoder as is.
        if (MemoryMarshal.TryGetArray(text, out var segment))
        {
            GetEncoder().Convert(segment.Array!, segment.Offset + charIndex, charCount, bytes, byteIndex, byteCount, flush, out charsUsed, out bytesUsed, out _);
            return;
        }

        // Otherwise (a string or a MemoryManager) copy the slice into a pooled buffer, so that a
        // read does not allocate a char[] of its own.
        var rented = ArrayPool<char>.Shared.Rent(charCount);
        try
        {
            text.Span.Slice(charIndex, charCount).CopyTo(rented);
            GetEncoder().Convert(rented, 0, charCount, bytes, byteIndex, byteCount, flush, out charsUsed, out bytesUsed, out _);
        }
        finally
        {
            ArrayPool<char>.Shared.Return(rented);
        }
#endif
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
            return Task.FromResult(ReadCore(buffer, offset, count));
        }
        catch (OperationCanceledException exception)
        {
            return Task.FromCanceled<int>(exception.CancellationToken);
        }
        catch (Exception exception)
        {
            return Task.FromException<int>(exception);
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
    public override long Seek(long offset, SeekOrigin origin) =>
        throw new NotSupportedException("Stream does not support seeking.");

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
[assembly: System.Runtime.CompilerServices.TypeForwardedTo(typeof(System.IO.StringStream))]
#endif
