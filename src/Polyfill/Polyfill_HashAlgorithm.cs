#if !NET11_0_OR_GREATER

namespace Polyfills;

using System;
#if FeatureMemory
using System.Buffers;
#endif
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

static partial class Polyfill
{
#if FeatureValueTask

    // Shared by the HashDataAsync, HmacDataAsync and VerifyHmacAsync polyfills.
    // The arguments are validated synchronously, then the stream is read asynchronously.
    // Each method takes ownership of, and disposes, the algorithm.
    internal static Task<byte[]> HashStreamAsync(
        HashAlgorithm algorithm,
        Stream source,
        CancellationToken cancellationToken)
    {
        if (source == null)
        {
            algorithm.Dispose();
            throw new ArgumentNullException(nameof(source));
        }

        return HashStreamCoreAsync(algorithm, source, cancellationToken);
    }

    static async Task<byte[]> HashStreamCoreAsync(
        HashAlgorithm algorithm,
        Stream source,
        CancellationToken cancellationToken)
    {
        using (algorithm)
        {
            // Throwing here results in a cancelled task for an already cancelled token
            cancellationToken.ThrowIfCancellationRequested();

            var buffer = new byte[4096];
            while (true)
            {
                var read = await source.ReadAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(false);
                if (read <= 0)
                {
                    break;
                }

                algorithm.TransformBlock(buffer, 0, read, null, 0);
            }

            algorithm.TransformFinalBlock(buffer, 0, 0);
            return algorithm.Hash!;
        }
    }

    internal static Task<bool> VerifyHashStreamAsync(
        HashAlgorithm algorithm,
        Stream source,
        byte[] hash,
        CancellationToken cancellationToken)
    {
        var hashSizeInBytes = algorithm.HashSize / 8;
        if (hash.Length != hashSizeInBytes)
        {
            algorithm.Dispose();
            throw new ArgumentException($"The hash must be {hashSizeInBytes} bytes in length.", nameof(hash));
        }

        var task = HashStreamAsync(algorithm, source, cancellationToken);
        return Verify(task, hash);

        static async Task<bool> Verify(Task<byte[]> task, byte[] hash)
        {
            var computed = await task.ConfigureAwait(false);

            // The time taken must not depend on the contents
            var accum = 0;
            for (var i = 0; i < computed.Length; i++)
            {
                accum |= computed[i] - hash[i];
            }

            return accum == 0;
        }
    }

#if FeatureMemory

    internal static Task<int> HashStreamAsync(
        HashAlgorithm algorithm,
        Stream source,
        Memory<byte> destination,
        CancellationToken cancellationToken)
    {
        if (destination.Length < algorithm.HashSize / 8)
        {
            algorithm.Dispose();
            throw new ArgumentException("Destination is too short.", nameof(destination));
        }

        var task = HashStreamAsync(algorithm, source, cancellationToken);
        return CopyTo(task, destination);

        static async Task<int> CopyTo(Task<byte[]> task, Memory<byte> destination)
        {
            var hash = await task.ConfigureAwait(false);
            hash.CopyTo(destination);
            return hash.Length;
        }
    }

#endif

#endif

#if FeatureMemory && !NET

    // Shared by the HashData, TryHashData and HmacData polyfills that take a span.

    // Throws when the destination is too short.
    internal static int HashSpan(HashAlgorithm algorithm, ReadOnlySpan<byte> source, Span<byte> destination)
    {
        if (TryHashSpan(algorithm, source, destination, out var bytesWritten))
        {
            return bytesWritten;
        }

        // Copying a hash to the destination throws the exception for one that is too short
        new byte[algorithm.HashSize / 8].AsSpan().CopyTo(destination);
        throw new ArgumentException("Destination is too short.", nameof(destination));
    }

#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER

    internal static byte[] HashSpan(HashAlgorithm algorithm, ReadOnlySpan<byte> source)
    {
        var hash = new byte[algorithm.HashSize / 8];
        algorithm.TryComputeHash(source, hash, out _);
        return hash;
    }

    internal static bool TryHashSpan(HashAlgorithm algorithm, ReadOnlySpan<byte> source, Span<byte> destination, out int bytesWritten) =>
        algorithm.TryComputeHash(source, destination, out bytesWritten);

#else

    // HashAlgorithm has no span overloads on these targets. The input goes through a pooled buffer
    // in chunks, rather than being copied to a new array of the same size.
    internal static byte[] HashSpan(HashAlgorithm algorithm, ReadOnlySpan<byte> source)
    {
        var rented = ArrayPool<byte>.Shared.Rent(Math.Min(source.Length, 64 * 1024));

        try
        {
            if (source.Length <= rented.Length)
            {
                source.CopyTo(rented);
                return algorithm.ComputeHash(rented, 0, source.Length);
            }

            while (!source.IsEmpty)
            {
                var count = Math.Min(source.Length, rented.Length);
                source.Slice(0, count).CopyTo(rented);
                algorithm.TransformBlock(rented, 0, count, null, 0);
                source = source.Slice(count);
            }

            algorithm.TransformFinalBlock(rented, 0, 0);
            return algorithm.Hash!;
        }
        finally
        {
            // The input may be sensitive, so it is not left behind in the pool
            ArrayPool<byte>.Shared.Return(rented, clearArray: true);
        }
    }

    internal static bool TryHashSpan(HashAlgorithm algorithm, ReadOnlySpan<byte> source, Span<byte> destination, out int bytesWritten)
    {
        if (destination.Length < algorithm.HashSize / 8)
        {
            bytesWritten = 0;
            return false;
        }

        var hash = HashSpan(algorithm, source);
        hash.CopyTo(destination);
        bytesWritten = hash.Length;
        return true;
    }

#endif

#endif
}

#endif