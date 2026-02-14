namespace Polyfills;

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

static partial class Polyfill
{
#if !NETCOREAPP2_1_OR_GREATER && !NETSTANDARD2_1 && FeatureMemory
    /// <summary>
    /// Reads a sequence of bytes from the current stream and advances the position within the stream by the number of bytes read.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.read?view=net-11.0#system-io-stream-read(system-span((system-byte)))
    public static int Read(this Stream target, Span<byte> buffer)
    {
        byte[] sharedBuffer = new byte[buffer.Length];
        int numRead = target.Read(sharedBuffer, 0, buffer.Length);
        if ((uint)numRead > (uint)buffer.Length)
        {
            throw new IOException("StreamTooLong");
        }

        new ReadOnlySpan<byte>(sharedBuffer, 0, numRead).CopyTo(buffer);
        return numRead;
    }
#endif

#if !NET7_0_OR_GREATER
#if FeatureValueTask
    /// <summary>
    /// Asynchronously reads count number of bytes from the current stream, advances the position within the stream, and monitors cancellation requests.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.readexactlyasync?view=net-11.0#system-io-stream-readexactlyasync(system-byte()-system-int32-system-int32-system-threading-cancellationtoken)
    public static async ValueTask ReadExactlyAsync(this Stream target, byte[] buffer, int offset, int count, CancellationToken cancellationToken = default)
    {
        if (buffer is null)
        {
            throw new ArgumentNullException(nameof(buffer));
        }

        if (offset < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(offset), "NeedNonNegNum");
        }

        if ((uint)count > buffer.Length - offset)
        {
            throw new ArgumentOutOfRangeException(nameof(count), "InvalidOffLen");
        }

        int totalRead = 0;
        while (totalRead < count)
        {
            int read = await target.ReadAsync(buffer, offset + totalRead, count - totalRead, cancellationToken);

            if (read == 0)
            {
                if (true)
                {
                    throw new EndOfStreamException();
                }

                return;
            }

            totalRead += read;
        }
    }
#endif

    /// <summary>
    /// Reads count number of bytes from the current stream and advances the position within the stream.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.readexactly?view=net-11.0#system-io-stream-readexactly(system-byte()-system-int32-system-int32)
    public static void ReadExactly(this Stream target, byte[] buffer, int offset, int count)
    {
        if (buffer is null)
        {
            throw new ArgumentNullException(nameof(buffer));
        }

        if (offset < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(offset), "NeedNonNegNum");
        }

        if ((uint)count > buffer.Length - offset)
        {
            throw new ArgumentOutOfRangeException(nameof(count), "InvalidOffLen");
        }

        var totalRead = 0;
        while (totalRead < count)
        {
            var read = target.Read(buffer, offset + totalRead, count - totalRead);

            if (read == 0)
            {
                throw new EndOfStreamException();
            }

            totalRead += read;
        }
    }

#if FeatureMemory
    /// <summary>
    /// Reads bytes from the current stream and advances the position within the stream until the buffer is filled.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.readexactly?view=net-11.0#system-io-stream-readexactly(system-span((system-byte)))
    public static void ReadExactly(this Stream target, Span<byte> buffer) =>
        _ = ReadAtLeast(target, buffer, buffer.Length, throwOnEndOfStream: true);

    /// <summary>
    /// Reads at least a minimum number of bytes from the current stream and advances the position within the stream by the number of bytes read.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.readatleast?view=net-11.0
    public static int ReadAtLeast(this Stream target, Span<byte> buffer, int minimumBytes, bool throwOnEndOfStream = true)
    {
        if (minimumBytes < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(minimumBytes), "NeedNonNegNum");
        }

        if (buffer.Length < minimumBytes)
        {
            throw new ArgumentOutOfRangeException(nameof(minimumBytes), "NotGreaterThanBufferLength");
        }

        int totalRead = 0;
        while (totalRead < minimumBytes)
        {
            int read = target.Read(buffer.Slice(totalRead));
            if (read == 0)
            {
                if (throwOnEndOfStream)
                {
                    throw new EndOfStreamException();
                }

                return totalRead;
            }

            totalRead += read;
        }

        return totalRead;
    }

#if FeatureValueTask
    /// <summary>
    /// Asynchronously reads bytes from the current stream, advances the position within the stream until the buffer is filled, and monitors cancellation requests
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.readatleastasync?view=net-11.0
    public static async ValueTask ReadExactlyAsync(this Stream target, Memory<byte> buffer, CancellationToken cancellationToken = default) =>
        await target.ReadAtLeastAsync(buffer, buffer.Length, true, cancellationToken);

    /// <summary>
    /// Asynchronously reads at least a minimum number of bytes from the current stream, advances the position within the stream by the number of bytes read, and monitors cancellation requests.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.readatleastasync?view=net-11.0
    public static async ValueTask<int> ReadAtLeastAsync(this Stream target, Memory<byte> buffer, int minimumBytes, bool throwOnEndOfStream = true, CancellationToken cancellationToken = default)
    {
        if (minimumBytes < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(minimumBytes), "NeedNonNegNum");
        }

        if (buffer.Length < minimumBytes)
        {
            throw new ArgumentOutOfRangeException(nameof(minimumBytes), "NotGreaterThanBufferLength");
        }

        int totalRead = 0;
        while (totalRead < minimumBytes)
        {
            int read = await target.ReadAsync(buffer.Slice(totalRead), cancellationToken).ConfigureAwait(false);
            if (read == 0)
            {
                if (throwOnEndOfStream)
                {
                    throw new EndOfStreamException();
                }

                return totalRead;
            }

            totalRead += read;
        }

        return totalRead;
    }

#endif
#endif
#endif
}
