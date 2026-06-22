#pragma warning disable

// CryptographicOperations was introduced in netcoreapp2.1/netstandard2.1.
// For targets where the type is entirely absent it is recreated here.
// For targets where the type exists but is missing members (members were added
// in net8/net9/net10) the missing members are added via extension in
// CryptographicOperationsExtensions.cs.
#if !NETCOREAPP2_1_OR_GREATER && !NETSTANDARD2_1_OR_GREATER

namespace System.Security.Cryptography;

// ReSharper disable RedundantUsingDirective
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
// ReSharper restore RedundantUsingDirective
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Provides methods for common cryptographic operations and reducing side-channel information leakage.
/// </summary>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
//Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.cryptographicoperations?view=net-11.0
#if PolyPublic
public
#endif
static class CryptographicOperations
{
    /// <summary>
    /// Computes the hash of data using the specified algorithm.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.cryptographicoperations.hashdata?view=net-11.0#system-security-cryptography-cryptographicoperations-hashdata(system-security-cryptography-hashalgorithmname-system-byte())
    public static byte[] HashData(HashAlgorithmName hashAlgorithm, byte[] source)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        using var hasher = CreateHashAlgorithm(hashAlgorithm);
        return hasher.ComputeHash(source);
    }

    /// <summary>
    /// Computes the hash of a stream using the specified algorithm.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.cryptographicoperations.hashdata?view=net-11.0#system-security-cryptography-cryptographicoperations-hashdata(system-security-cryptography-hashalgorithmname-system-io-stream)
    public static byte[] HashData(HashAlgorithmName hashAlgorithm, Stream source)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        using var hasher = CreateHashAlgorithm(hashAlgorithm);
        return hasher.ComputeHash(source);
    }

    /// <summary>
    /// Computes the HMAC of data using the specified key and algorithm.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.cryptographicoperations.hmacdata?view=net-11.0#system-security-cryptography-cryptographicoperations-hmacdata(system-security-cryptography-hashalgorithmname-system-byte()-system-byte())
    public static byte[] HmacData(HashAlgorithmName hashAlgorithm, byte[] key, byte[] source)
    {
        if (key == null)
        {
            throw new ArgumentNullException(nameof(key));
        }

        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        using var hmac = CreateHmac(hashAlgorithm, key);
        return hmac.ComputeHash(source);
    }

    /// <summary>
    /// Computes the HMAC of a stream using the specified key and algorithm.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.cryptographicoperations.hmacdata?view=net-11.0#system-security-cryptography-cryptographicoperations-hmacdata(system-security-cryptography-hashalgorithmname-system-byte()-system-io-stream)
    public static byte[] HmacData(HashAlgorithmName hashAlgorithm, byte[] key, Stream source)
    {
        if (key == null)
        {
            throw new ArgumentNullException(nameof(key));
        }

        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        using var hmac = CreateHmac(hashAlgorithm, key);
        return hmac.ComputeHash(source);
    }

    /// <summary>
    /// Determines whether the HMAC of <paramref name="source"/> matches <paramref name="hash"/>, using a comparison whose time does not depend on the contents.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.cryptographicoperations.verifyhmac?view=net-11.0#system-security-cryptography-cryptographicoperations-verifyhmac(system-security-cryptography-hashalgorithmname-system-byte()-system-byte()-system-byte())
    public static bool VerifyHmac(HashAlgorithmName hashAlgorithm, byte[] key, byte[] source, byte[] hash)
    {
        if (key == null)
        {
            throw new ArgumentNullException(nameof(key));
        }

        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        if (hash == null)
        {
            throw new ArgumentNullException(nameof(hash));
        }

        using var hmac = CreateHmac(hashAlgorithm, key);
        var hashSizeInBytes = hmac.HashSize / 8;
        if (hash.Length != hashSizeInBytes)
        {
            throw new ArgumentException($"The hash must be {hashSizeInBytes} bytes in length.", nameof(hash));
        }

        var mac = hmac.ComputeHash(source);
        return FixedTimeEqualsCore(mac, hash);
    }

    /// <summary>
    /// Determines whether the HMAC of the <paramref name="source"/> stream matches <paramref name="hash"/>, using a comparison whose time does not depend on the contents.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.cryptographicoperations.verifyhmac?view=net-11.0#system-security-cryptography-cryptographicoperations-verifyhmac(system-security-cryptography-hashalgorithmname-system-byte()-system-io-stream-system-byte())
    public static bool VerifyHmac(HashAlgorithmName hashAlgorithm, byte[] key, Stream source, byte[] hash)
    {
        if (key == null)
        {
            throw new ArgumentNullException(nameof(key));
        }

        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        if (hash == null)
        {
            throw new ArgumentNullException(nameof(hash));
        }

        using var hmac = CreateHmac(hashAlgorithm, key);
        var hashSizeInBytes = hmac.HashSize / 8;
        if (hash.Length != hashSizeInBytes)
        {
            throw new ArgumentException($"The hash must be {hashSizeInBytes} bytes in length.", nameof(hash));
        }

        var mac = hmac.ComputeHash(source);
        return FixedTimeEqualsCore(mac, hash);
    }

#if FeatureValueTask

    /// <summary>
    /// Asynchronously computes the hash of a stream using the specified algorithm.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.cryptographicoperations.hashdataasync?view=net-11.0#system-security-cryptography-cryptographicoperations-hashdataasync(system-security-cryptography-hashalgorithmname-system-io-stream-system-threading-cancellationtoken)
    public static ValueTask<byte[]> HashDataAsync(HashAlgorithmName hashAlgorithm, Stream source, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return new(HashData(hashAlgorithm, source));
    }

    /// <summary>
    /// Asynchronously computes the HMAC of a stream using the specified key and algorithm.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.cryptographicoperations.hmacdataasync?view=net-11.0#system-security-cryptography-cryptographicoperations-hmacdataasync(system-security-cryptography-hashalgorithmname-system-byte()-system-io-stream-system-threading-cancellationtoken)
    public static ValueTask<byte[]> HmacDataAsync(HashAlgorithmName hashAlgorithm, byte[] key, Stream source, CancellationToken cancellationToken = default)
    {
        if (key == null)
        {
            throw new ArgumentNullException(nameof(key));
        }

        cancellationToken.ThrowIfCancellationRequested();
        return new(HmacData(hashAlgorithm, key, source));
    }

    /// <summary>
    /// Asynchronously determines whether the HMAC of the <paramref name="source"/> stream matches <paramref name="hash"/>.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.cryptographicoperations.verifyhmacasync?view=net-11.0#system-security-cryptography-cryptographicoperations-verifyhmacasync(system-security-cryptography-hashalgorithmname-system-byte()-system-io-stream-system-byte()-system-threading-cancellationtoken)
    public static ValueTask<bool> VerifyHmacAsync(HashAlgorithmName hashAlgorithm, byte[] key, Stream source, byte[] hash, CancellationToken cancellationToken = default)
    {
        if (key == null)
        {
            throw new ArgumentNullException(nameof(key));
        }

        if (hash == null)
        {
            throw new ArgumentNullException(nameof(hash));
        }

        cancellationToken.ThrowIfCancellationRequested();
        return new(VerifyHmac(hashAlgorithm, key, source, hash));
    }

#endif

#if FeatureMemory

    /// <summary>
    /// Determines the equality of two byte sequences in an amount of time that depends on the length of the sequences, but not their values.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.cryptographicoperations.fixedtimeequals?view=net-11.0#system-security-cryptography-cryptographicoperations-fixedtimeequals(system-readonlyspan((system-byte))-system-readonlyspan((system-byte)))
    //Note: Best-effort constant-time comparison; a managed loop cannot offer the same guarantees as the BCL intrinsic.
    public static bool FixedTimeEquals(ReadOnlySpan<byte> left, ReadOnlySpan<byte> right)
    {
        if (left.Length != right.Length)
        {
            return false;
        }

        var length = left.Length;
        var accum = 0;
        for (var i = 0; i < length; i++)
        {
            accum |= left[i] - right[i];
        }

        return accum == 0;
    }

    /// <summary>
    /// Determines whether every byte in <paramref name="source"/> is equal to <paramref name="value"/>, in an amount of time that depends on the length of <paramref name="source"/>, but not its values.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.cryptographicoperations.fixedtimeequals?view=net-11.0#system-security-cryptography-cryptographicoperations-fixedtimeequals(system-readonlyspan((system-byte))-system-byte)
    //Note: Best-effort constant-time comparison; a managed loop cannot offer the same guarantees as the BCL intrinsic.
    public static bool FixedTimeEquals(ReadOnlySpan<byte> source, byte value)
    {
        var length = source.Length;
        var accum = 0;
        for (var i = 0; i < length; i++)
        {
            accum |= source[i] - value;
        }

        return accum == 0;
    }

    /// <summary>
    /// Fills the provided buffer with zeros.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.cryptographicoperations.zeromemory?view=net-11.0
    //Note: Best-effort zeroing; unlike the BCL intrinsic the JIT may elide the clear if the buffer is not observed afterwards.
    public static void ZeroMemory(Span<byte> buffer) =>
        buffer.Clear();

    /// <summary>
    /// Computes the hash of data using the specified algorithm.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.cryptographicoperations.hashdata?view=net-11.0#system-security-cryptography-cryptographicoperations-hashdata(system-security-cryptography-hashalgorithmname-system-readonlyspan((system-byte)))
    public static byte[] HashData(HashAlgorithmName hashAlgorithm, ReadOnlySpan<byte> source)
    {
        using var hasher = CreateHashAlgorithm(hashAlgorithm);
        return hasher.ComputeHash(source.ToArray());
    }

    /// <summary>
    /// Computes the hash of data into the provided destination using the specified algorithm.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.cryptographicoperations.hashdata?view=net-11.0#system-security-cryptography-cryptographicoperations-hashdata(system-security-cryptography-hashalgorithmname-system-readonlyspan((system-byte))-system-span((system-byte)))
    public static int HashData(HashAlgorithmName hashAlgorithm, ReadOnlySpan<byte> source, Span<byte> destination)
    {
        if (!TryHashData(hashAlgorithm, source, destination, out var bytesWritten))
        {
            throw new ArgumentException("Destination is too short.", nameof(destination));
        }

        return bytesWritten;
    }

    /// <summary>
    /// Computes the hash of a stream into the provided destination using the specified algorithm.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.cryptographicoperations.hashdata?view=net-11.0#system-security-cryptography-cryptographicoperations-hashdata(system-security-cryptography-hashalgorithmname-system-io-stream-system-span((system-byte)))
    public static int HashData(HashAlgorithmName hashAlgorithm, Stream source, Span<byte> destination)
    {
        var hash = HashData(hashAlgorithm, source);
        if (destination.Length < hash.Length)
        {
            throw new ArgumentException("Destination is too short.", nameof(destination));
        }

        hash.CopyTo(destination);
        return hash.Length;
    }

    /// <summary>
    /// Attempts to compute the hash of data into the provided destination using the specified algorithm.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.cryptographicoperations.tryhashdata?view=net-11.0
    public static bool TryHashData(HashAlgorithmName hashAlgorithm, ReadOnlySpan<byte> source, Span<byte> destination, out int bytesWritten)
    {
        using var hasher = CreateHashAlgorithm(hashAlgorithm);
        var hash = hasher.ComputeHash(source.ToArray());
        if (destination.Length < hash.Length)
        {
            bytesWritten = 0;
            return false;
        }

        hash.CopyTo(destination);
        bytesWritten = hash.Length;
        return true;
    }

    /// <summary>
    /// Computes the HMAC of data using the specified key and algorithm.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.cryptographicoperations.hmacdata?view=net-11.0#system-security-cryptography-cryptographicoperations-hmacdata(system-security-cryptography-hashalgorithmname-system-readonlyspan((system-byte))-system-readonlyspan((system-byte)))
    public static byte[] HmacData(HashAlgorithmName hashAlgorithm, ReadOnlySpan<byte> key, ReadOnlySpan<byte> source)
    {
        using var hmac = CreateHmac(hashAlgorithm, key.ToArray());
        return hmac.ComputeHash(source.ToArray());
    }

    /// <summary>
    /// Computes the HMAC of data into the provided destination using the specified key and algorithm.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.cryptographicoperations.hmacdata?view=net-11.0#system-security-cryptography-cryptographicoperations-hmacdata(system-security-cryptography-hashalgorithmname-system-readonlyspan((system-byte))-system-readonlyspan((system-byte))-system-span((system-byte)))
    public static int HmacData(HashAlgorithmName hashAlgorithm, ReadOnlySpan<byte> key, ReadOnlySpan<byte> source, Span<byte> destination)
    {
        if (!TryHmacData(hashAlgorithm, key, source, destination, out var bytesWritten))
        {
            throw new ArgumentException("Destination is too short.", nameof(destination));
        }

        return bytesWritten;
    }

    /// <summary>
    /// Computes the HMAC of a stream using the specified key and algorithm.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.cryptographicoperations.hmacdata?view=net-11.0#system-security-cryptography-cryptographicoperations-hmacdata(system-security-cryptography-hashalgorithmname-system-readonlyspan((system-byte))-system-io-stream)
    public static byte[] HmacData(HashAlgorithmName hashAlgorithm, ReadOnlySpan<byte> key, Stream source)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        using var hmac = CreateHmac(hashAlgorithm, key.ToArray());
        return hmac.ComputeHash(source);
    }

    /// <summary>
    /// Computes the HMAC of a stream into the provided destination using the specified key and algorithm.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.cryptographicoperations.hmacdata?view=net-11.0#system-security-cryptography-cryptographicoperations-hmacdata(system-security-cryptography-hashalgorithmname-system-readonlyspan((system-byte))-system-io-stream-system-span((system-byte)))
    public static int HmacData(HashAlgorithmName hashAlgorithm, ReadOnlySpan<byte> key, Stream source, Span<byte> destination)
    {
        var mac = HmacData(hashAlgorithm, key, source);
        if (destination.Length < mac.Length)
        {
            throw new ArgumentException("Destination is too short.", nameof(destination));
        }

        mac.CopyTo(destination);
        return mac.Length;
    }

    /// <summary>
    /// Attempts to compute the HMAC of data into the provided destination using the specified key and algorithm.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.cryptographicoperations.tryhmacdata?view=net-11.0
    public static bool TryHmacData(HashAlgorithmName hashAlgorithm, ReadOnlySpan<byte> key, ReadOnlySpan<byte> source, Span<byte> destination, out int bytesWritten)
    {
        using var hmac = CreateHmac(hashAlgorithm, key.ToArray());
        var mac = hmac.ComputeHash(source.ToArray());
        if (destination.Length < mac.Length)
        {
            bytesWritten = 0;
            return false;
        }

        mac.CopyTo(destination);
        bytesWritten = mac.Length;
        return true;
    }

    /// <summary>
    /// Determines whether the HMAC of <paramref name="source"/> matches <paramref name="hash"/>, using a comparison whose time does not depend on the contents.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.cryptographicoperations.verifyhmac?view=net-11.0#system-security-cryptography-cryptographicoperations-verifyhmac(system-security-cryptography-hashalgorithmname-system-readonlyspan((system-byte))-system-readonlyspan((system-byte))-system-readonlyspan((system-byte)))
    public static bool VerifyHmac(HashAlgorithmName hashAlgorithm, ReadOnlySpan<byte> key, ReadOnlySpan<byte> source, ReadOnlySpan<byte> hash)
    {
        using var hmac = CreateHmac(hashAlgorithm, key.ToArray());
        var hashSizeInBytes = hmac.HashSize / 8;
        if (hash.Length != hashSizeInBytes)
        {
            throw new ArgumentException($"The hash must be {hashSizeInBytes} bytes in length.", nameof(hash));
        }

        var mac = hmac.ComputeHash(source.ToArray());
        return FixedTimeEquals(mac, hash);
    }

    /// <summary>
    /// Determines whether the HMAC of the <paramref name="source"/> stream matches <paramref name="hash"/>, using a comparison whose time does not depend on the contents.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.cryptographicoperations.verifyhmac?view=net-11.0#system-security-cryptography-cryptographicoperations-verifyhmac(system-security-cryptography-hashalgorithmname-system-readonlyspan((system-byte))-system-io-stream-system-readonlyspan((system-byte)))
    public static bool VerifyHmac(HashAlgorithmName hashAlgorithm, ReadOnlySpan<byte> key, Stream source, ReadOnlySpan<byte> hash)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        using var hmac = CreateHmac(hashAlgorithm, key.ToArray());
        var hashSizeInBytes = hmac.HashSize / 8;
        if (hash.Length != hashSizeInBytes)
        {
            throw new ArgumentException($"The hash must be {hashSizeInBytes} bytes in length.", nameof(hash));
        }

        var mac = hmac.ComputeHash(source);
        return FixedTimeEquals(mac, hash);
    }

#if FeatureValueTask

    /// <summary>
    /// Asynchronously computes the hash of a stream into the provided destination using the specified algorithm.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.cryptographicoperations.hashdataasync?view=net-11.0#system-security-cryptography-cryptographicoperations-hashdataasync(system-security-cryptography-hashalgorithmname-system-io-stream-system-memory((system-byte))-system-threading-cancellationtoken)
    public static ValueTask<int> HashDataAsync(HashAlgorithmName hashAlgorithm, Stream source, Memory<byte> destination, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return new(HashData(hashAlgorithm, source, destination.Span));
    }

    /// <summary>
    /// Asynchronously computes the HMAC of a stream using the specified key and algorithm.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.cryptographicoperations.hmacdataasync?view=net-11.0#system-security-cryptography-cryptographicoperations-hmacdataasync(system-security-cryptography-hashalgorithmname-system-readonlymemory((system-byte))-system-io-stream-system-threading-cancellationtoken)
    public static ValueTask<byte[]> HmacDataAsync(HashAlgorithmName hashAlgorithm, ReadOnlyMemory<byte> key, Stream source, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return new(HmacData(hashAlgorithm, key.Span, source));
    }

    /// <summary>
    /// Asynchronously computes the HMAC of a stream into the provided destination using the specified key and algorithm.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.cryptographicoperations.hmacdataasync?view=net-11.0#system-security-cryptography-cryptographicoperations-hmacdataasync(system-security-cryptography-hashalgorithmname-system-readonlymemory((system-byte))-system-io-stream-system-memory((system-byte))-system-threading-cancellationtoken)
    public static ValueTask<int> HmacDataAsync(HashAlgorithmName hashAlgorithm, ReadOnlyMemory<byte> key, Stream source, Memory<byte> destination, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return new(HmacData(hashAlgorithm, key.Span, source, destination.Span));
    }

    /// <summary>
    /// Asynchronously determines whether the HMAC of the <paramref name="source"/> stream matches <paramref name="hash"/>.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.cryptographicoperations.verifyhmacasync?view=net-11.0#system-security-cryptography-cryptographicoperations-verifyhmacasync(system-security-cryptography-hashalgorithmname-system-readonlymemory((system-byte))-system-io-stream-system-readonlymemory((system-byte))-system-threading-cancellationtoken)
    public static ValueTask<bool> VerifyHmacAsync(HashAlgorithmName hashAlgorithm, ReadOnlyMemory<byte> key, Stream source, ReadOnlyMemory<byte> hash, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return new(VerifyHmac(hashAlgorithm, key.Span, source, hash.Span));
    }

#endif
#endif

    static HashAlgorithm CreateHashAlgorithm(HashAlgorithmName hashAlgorithm)
    {
        var name = hashAlgorithm.Name;
        return name switch
        {
            "SHA256" => SHA256.Create(),
            "SHA1" => SHA1.Create(),
            "SHA384" => SHA384.Create(),
            "SHA512" => SHA512.Create(),
            "MD5" => MD5.Create(),
            _ => throw new CryptographicException($"'{name}' is not a known hash algorithm.")
        };
    }

    static HMAC CreateHmac(HashAlgorithmName hashAlgorithm, byte[] key)
    {
        var name = hashAlgorithm.Name;
        return name switch
        {
            "SHA256" => new HMACSHA256(key),
            "SHA1" => new HMACSHA1(key),
            "SHA384" => new HMACSHA384(key),
            "SHA512" => new HMACSHA512(key),
            "MD5" => new HMACMD5(key),
            _ => throw new CryptographicException($"'{name}' is not a known hash algorithm.")
        };
    }

    static bool FixedTimeEqualsCore(byte[] left, byte[] right)
    {
        if (left.Length != right.Length)
        {
            return false;
        }

        var accum = 0;
        for (var i = 0; i < left.Length; i++)
        {
            accum |= left[i] - right[i];
        }

        return accum == 0;
    }
}

#else
[assembly: System.Runtime.CompilerServices.TypeForwardedTo(typeof(System.Security.Cryptography.CryptographicOperations))]
#endif
