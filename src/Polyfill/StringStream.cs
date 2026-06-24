#nullable enable

// === Polyfill target window =====================================================================
// System.IO.StringStream was approved and merged into dotnet/runtime for net11
// (https://github.com/dotnet/runtime/pull/126669) but is NOT in the current net11 preview/RC SDK,
// so this polyfill is intentionally ACTIVE on net11 as well.
// When you upgrade to a net11 SDK that actually ships this type, the build will collide (CS0436)
// and the ApiBuilderTests "StreamWrapperBclDetectionTests" test FAILS with these exact steps.
// To retire this polyfill for net11:
//   1. change `#if FeatureMemory` below to `#if FeatureMemory && !NET11_0_OR_GREATER`
//   2. add at the bottom of this file (after the final #endif):
//        #if NET11_0_OR_GREATER
//        [assembly: System.Runtime.CompilerServices.TypeForwardedTo(typeof(System.IO.StringStream))]
//        #endif
//   3. re-run ApiBuilderTests in Debug to regenerate Split + api_list.
// ================================================================================================
#if FeatureMemory

#pragma warning disable

namespace System.IO;

using System.Text;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Provides a read-only, non-seekable <see cref="Stream"/> that encodes a <see cref="string"/> or
/// <see cref="ReadOnlyMemory{Char}"/> into bytes using a specified <see cref="System.Text.Encoding"/>.
/// </summary>
/// <remarks>
/// This stream never emits a byte order mark (BOM). Callers who need a BOM can prepend it themselves.
/// The polyfill encodes the whole text into a single buffer on first read rather than encoding
/// on-the-fly; this is a non-observable performance difference from the BCL implementation.
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
    byte[]? encoded;
    int position;
    bool disposed;

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

    byte[] GetEncoded()
    {
        if (encoded == null)
        {
            var chars = text.ToArray();
            encoded = encoding.GetBytes(chars, 0, chars.Length);
        }

        return encoded;
    }

    /// <inheritdoc/>
    public override int Read(byte[] buffer, int offset, int count)
    {
        GuardRange(buffer, offset, count);
        ThrowIfDisposed();

        var all = GetEncoded();
        var remaining = all.Length - position;
        if (remaining <= 0 ||
            count == 0)
        {
            return 0;
        }

        var toRead = Math.Min(remaining, count);
        Array.Copy(all, position, buffer, offset, toRead);
        position += toRead;
        return toRead;
    }

    /// <inheritdoc/>
    public override int ReadByte()
    {
        ThrowIfDisposed();

        var all = GetEncoded();
        if (position < all.Length)
        {
            return all[position++];
        }

        return -1;
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
