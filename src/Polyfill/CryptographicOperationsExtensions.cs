#pragma warning disable

// Fills in members missing from the BCL CryptographicOperations on targets where
// the type exists (netcoreapp2.1/netstandard2.1 onwards) but predates the member:
//   HashData family added in net8, HmacData family in net9, VerifyHmac family and
//   FixedTimeEquals(ReadOnlySpan<byte>, byte) in net10.
// For targets where the type is entirely absent the full surface is recreated in
// CryptographicOperations.cs instead. Span/ValueTask/ReadOnlyMemory are intrinsic
// to every target in this window, so no Feature* gates are needed here.
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER

namespace Polyfills;

using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

static partial class Polyfill
{
    extension(CryptographicOperations)
    {
#if !NET9_0_OR_GREATER

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

#if NET5_0_OR_GREATER
            return CryptographicOperationsHashData(hashAlgorithm, source);
#else
            using var hasher = CryptographicOperationsCreateHashAlgorithm(hashAlgorithm);
            return hasher.ComputeHash(source);
#endif
        }

        /// <summary>
        /// Computes the hash of data using the specified algorithm.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.cryptographicoperations.hashdata?view=net-11.0#system-security-cryptography-cryptographicoperations-hashdata(system-security-cryptography-hashalgorithmname-system-readonlyspan((system-byte)))
        public static byte[] HashData(HashAlgorithmName hashAlgorithm, ReadOnlySpan<byte> source)
        {
#if NET5_0_OR_GREATER
            return CryptographicOperationsHashData(hashAlgorithm, source);
#else
            using var hasher = CryptographicOperationsCreateHashAlgorithm(hashAlgorithm);
            var hash = new byte[hasher.HashSize / 8];
            hasher.TryComputeHash(source, hash, out _);
            return hash;
#endif
        }

        /// <summary>
        /// Computes the hash of data into the provided destination using the specified algorithm.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.cryptographicoperations.hashdata?view=net-11.0#system-security-cryptography-cryptographicoperations-hashdata(system-security-cryptography-hashalgorithmname-system-readonlyspan((system-byte))-system-span((system-byte)))
        public static int HashData(HashAlgorithmName hashAlgorithm, ReadOnlySpan<byte> source, Span<byte> destination)
        {
            if (!CryptographicOperations.TryHashData(hashAlgorithm, source, destination, out var bytesWritten))
            {
                throw new ArgumentException("Destination is too short.", nameof(destination));
            }

            return bytesWritten;
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

            using var hasher = CryptographicOperationsCreateHashAlgorithm(hashAlgorithm);
            return hasher.ComputeHash(source);
        }

        /// <summary>
        /// Computes the hash of a stream into the provided destination using the specified algorithm.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.cryptographicoperations.hashdata?view=net-11.0#system-security-cryptography-cryptographicoperations-hashdata(system-security-cryptography-hashalgorithmname-system-io-stream-system-span((system-byte)))
        public static int HashData(HashAlgorithmName hashAlgorithm, Stream source, Span<byte> destination)
        {
            var hash = CryptographicOperations.HashData(hashAlgorithm, source);
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
#if NET5_0_OR_GREATER
            return CryptographicOperationsTryHashData(hashAlgorithm, source, destination, out bytesWritten);
#else
            using var hasher = CryptographicOperationsCreateHashAlgorithm(hashAlgorithm);
            return hasher.TryComputeHash(source, destination, out bytesWritten);
#endif
        }

        /// <summary>
        /// Asynchronously computes the hash of a stream using the specified algorithm.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.cryptographicoperations.hashdataasync?view=net-11.0#system-security-cryptography-cryptographicoperations-hashdataasync(system-security-cryptography-hashalgorithmname-system-io-stream-system-threading-cancellationtoken)
        public static ValueTask<byte[]> HashDataAsync(HashAlgorithmName hashAlgorithm, Stream source, CancellationToken cancellationToken = default) =>
            new(HashStreamAsync(CryptographicOperationsCreateHashAlgorithm(hashAlgorithm), source, cancellationToken));

        /// <summary>
        /// Asynchronously computes the hash of a stream into the provided destination using the specified algorithm.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.cryptographicoperations.hashdataasync?view=net-11.0#system-security-cryptography-cryptographicoperations-hashdataasync(system-security-cryptography-hashalgorithmname-system-io-stream-system-memory((system-byte))-system-threading-cancellationtoken)
        public static ValueTask<int> HashDataAsync(HashAlgorithmName hashAlgorithm, Stream source, Memory<byte> destination, CancellationToken cancellationToken = default) =>
            new(HashStreamAsync(CryptographicOperationsCreateHashAlgorithm(hashAlgorithm), source, destination, cancellationToken));

#endif

#if !NET9_0_OR_GREATER

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

#if NET6_0_OR_GREATER
            return CryptographicOperationsHmacData(hashAlgorithm, key, source);
#else
            using var hmac = CryptographicOperationsCreateHmac(hashAlgorithm, key);
            return hmac.ComputeHash(source);
#endif
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

            using var hmac = CryptographicOperationsCreateHmac(hashAlgorithm, key);
            return hmac.ComputeHash(source);
        }

        /// <summary>
        /// Computes the HMAC of data using the specified key and algorithm.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.cryptographicoperations.hmacdata?view=net-11.0#system-security-cryptography-cryptographicoperations-hmacdata(system-security-cryptography-hashalgorithmname-system-readonlyspan((system-byte))-system-readonlyspan((system-byte)))
        public static byte[] HmacData(HashAlgorithmName hashAlgorithm, ReadOnlySpan<byte> key, ReadOnlySpan<byte> source)
        {
#if NET6_0_OR_GREATER
            return CryptographicOperationsHmacData(hashAlgorithm, key, source);
#else
            using var hmac = CryptographicOperationsCreateHmac(hashAlgorithm, key.ToArray());
            var mac = new byte[hmac.HashSize / 8];
            hmac.TryComputeHash(source, mac, out _);
            return mac;
#endif
        }

        /// <summary>
        /// Computes the HMAC of data into the provided destination using the specified key and algorithm.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.cryptographicoperations.hmacdata?view=net-11.0#system-security-cryptography-cryptographicoperations-hmacdata(system-security-cryptography-hashalgorithmname-system-readonlyspan((system-byte))-system-readonlyspan((system-byte))-system-span((system-byte)))
        public static int HmacData(HashAlgorithmName hashAlgorithm, ReadOnlySpan<byte> key, ReadOnlySpan<byte> source, Span<byte> destination)
        {
            if (!CryptographicOperations.TryHmacData(hashAlgorithm, key, source, destination, out var bytesWritten))
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

            using var hmac = CryptographicOperationsCreateHmac(hashAlgorithm, key.ToArray());
            return hmac.ComputeHash(source);
        }

        /// <summary>
        /// Computes the HMAC of a stream into the provided destination using the specified key and algorithm.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.cryptographicoperations.hmacdata?view=net-11.0#system-security-cryptography-cryptographicoperations-hmacdata(system-security-cryptography-hashalgorithmname-system-readonlyspan((system-byte))-system-io-stream-system-span((system-byte)))
        public static int HmacData(HashAlgorithmName hashAlgorithm, ReadOnlySpan<byte> key, Stream source, Span<byte> destination)
        {
            var mac = CryptographicOperations.HmacData(hashAlgorithm, key, source);
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
#if NET6_0_OR_GREATER
            return CryptographicOperationsTryHmacData(hashAlgorithm, key, source, destination, out bytesWritten);
#else
            using var hmac = CryptographicOperationsCreateHmac(hashAlgorithm, key.ToArray());
            return hmac.TryComputeHash(source, destination, out bytesWritten);
#endif
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

            return new(HashStreamAsync(CryptographicOperationsCreateHmac(hashAlgorithm, key), source, cancellationToken));
        }

        /// <summary>
        /// Asynchronously computes the HMAC of a stream using the specified key and algorithm.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.cryptographicoperations.hmacdataasync?view=net-11.0#system-security-cryptography-cryptographicoperations-hmacdataasync(system-security-cryptography-hashalgorithmname-system-readonlymemory((system-byte))-system-io-stream-system-threading-cancellationtoken)
        public static ValueTask<byte[]> HmacDataAsync(HashAlgorithmName hashAlgorithm, ReadOnlyMemory<byte> key, Stream source, CancellationToken cancellationToken = default) =>
            new(HashStreamAsync(CryptographicOperationsCreateHmac(hashAlgorithm, key.ToArray()), source, cancellationToken));

        /// <summary>
        /// Asynchronously computes the HMAC of a stream into the provided destination using the specified key and algorithm.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.cryptographicoperations.hmacdataasync?view=net-11.0#system-security-cryptography-cryptographicoperations-hmacdataasync(system-security-cryptography-hashalgorithmname-system-readonlymemory((system-byte))-system-io-stream-system-memory((system-byte))-system-threading-cancellationtoken)
        public static ValueTask<int> HmacDataAsync(HashAlgorithmName hashAlgorithm, ReadOnlyMemory<byte> key, Stream source, Memory<byte> destination, CancellationToken cancellationToken = default) =>
            new(HashStreamAsync(CryptographicOperationsCreateHmac(hashAlgorithm, key.ToArray()), source, destination, cancellationToken));

#endif

#if !NET11_0_OR_GREATER

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

            return CryptographicOperations.VerifyHmac(hashAlgorithm, new ReadOnlySpan<byte>(key), new ReadOnlySpan<byte>(source), new ReadOnlySpan<byte>(hash));
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

            if (hash == null)
            {
                throw new ArgumentNullException(nameof(hash));
            }

            return CryptographicOperations.VerifyHmac(hashAlgorithm, new ReadOnlySpan<byte>(key), source, new ReadOnlySpan<byte>(hash));
        }

        /// <summary>
        /// Determines whether the HMAC of <paramref name="source"/> matches <paramref name="hash"/>, using a comparison whose time does not depend on the contents.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.cryptographicoperations.verifyhmac?view=net-11.0#system-security-cryptography-cryptographicoperations-verifyhmac(system-security-cryptography-hashalgorithmname-system-readonlyspan((system-byte))-system-readonlyspan((system-byte))-system-readonlyspan((system-byte)))
        public static bool VerifyHmac(HashAlgorithmName hashAlgorithm, ReadOnlySpan<byte> key, ReadOnlySpan<byte> source, ReadOnlySpan<byte> hash)
        {
            var hashSizeInBytes = CryptographicOperationsHashSize(hashAlgorithm);
            if (hash.Length != hashSizeInBytes)
            {
                throw new ArgumentException($"The hash must be {hashSizeInBytes} bytes in length.", nameof(hash));
            }

            Span<byte> mac = stackalloc byte[64];
            CryptographicOperations.TryHmacData(hashAlgorithm, key, source, mac, out var bytesWritten);
            return CryptographicOperations.FixedTimeEquals(mac.Slice(0, bytesWritten), hash);
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

            using var hmac = CryptographicOperationsCreateHmac(hashAlgorithm, key.ToArray());
            var hashSizeInBytes = hmac.HashSize / 8;
            if (hash.Length != hashSizeInBytes)
            {
                throw new ArgumentException($"The hash must be {hashSizeInBytes} bytes in length.", nameof(hash));
            }

            var mac = hmac.ComputeHash(source);
            return CryptographicOperations.FixedTimeEquals(mac, hash);
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

            return new(VerifyHashStreamAsync(CryptographicOperationsCreateHmac(hashAlgorithm, key), source, hash, cancellationToken));
        }

        /// <summary>
        /// Asynchronously determines whether the HMAC of the <paramref name="source"/> stream matches <paramref name="hash"/>.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.cryptographicoperations.verifyhmacasync?view=net-11.0#system-security-cryptography-cryptographicoperations-verifyhmacasync(system-security-cryptography-hashalgorithmname-system-readonlymemory((system-byte))-system-io-stream-system-readonlymemory((system-byte))-system-threading-cancellationtoken)
        public static ValueTask<bool> VerifyHmacAsync(HashAlgorithmName hashAlgorithm, ReadOnlyMemory<byte> key, Stream source, ReadOnlyMemory<byte> hash, CancellationToken cancellationToken = default) =>
            new(VerifyHashStreamAsync(CryptographicOperationsCreateHmac(hashAlgorithm, key.ToArray()), source, hash.ToArray(), cancellationToken));

#endif
    }

#if !NET9_0_OR_GREATER

    static HashAlgorithm CryptographicOperationsCreateHashAlgorithm(HashAlgorithmName hashAlgorithm)
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

#endif

#if NET5_0_OR_GREATER && !NET9_0_OR_GREATER

    // The static one-shots hash without creating an algorithm instance or copying the input
    static byte[] CryptographicOperationsHashData(HashAlgorithmName hashAlgorithm, ReadOnlySpan<byte> source)
    {
        var name = hashAlgorithm.Name;
        return name switch
        {
            "SHA256" => SHA256.HashData(source),
            "SHA1" => SHA1.HashData(source),
            "SHA384" => SHA384.HashData(source),
            "SHA512" => SHA512.HashData(source),
            "MD5" => MD5.HashData(source),
            _ => throw new CryptographicException($"'{name}' is not a known hash algorithm.")
        };
    }

    static bool CryptographicOperationsTryHashData(HashAlgorithmName hashAlgorithm, ReadOnlySpan<byte> source, Span<byte> destination, out int bytesWritten)
    {
        var name = hashAlgorithm.Name;
        switch (name)
        {
            case "SHA256":
                return SHA256.TryHashData(source, destination, out bytesWritten);
            case "SHA1":
                return SHA1.TryHashData(source, destination, out bytesWritten);
            case "SHA384":
                return SHA384.TryHashData(source, destination, out bytesWritten);
            case "SHA512":
                return SHA512.TryHashData(source, destination, out bytesWritten);
            case "MD5":
                return MD5.TryHashData(source, destination, out bytesWritten);
            default:
                throw new CryptographicException($"'{name}' is not a known hash algorithm.");
        }
    }

#endif

#if NET6_0_OR_GREATER && !NET9_0_OR_GREATER

    static byte[] CryptographicOperationsHmacData(HashAlgorithmName hashAlgorithm, ReadOnlySpan<byte> key, ReadOnlySpan<byte> source)
    {
        var name = hashAlgorithm.Name;
        return name switch
        {
            "SHA256" => HMACSHA256.HashData(key, source),
            "SHA1" => HMACSHA1.HashData(key, source),
            "SHA384" => HMACSHA384.HashData(key, source),
            "SHA512" => HMACSHA512.HashData(key, source),
            "MD5" => HMACMD5.HashData(key, source),
            _ => throw new CryptographicException($"'{name}' is not a known hash algorithm.")
        };
    }

    static bool CryptographicOperationsTryHmacData(HashAlgorithmName hashAlgorithm, ReadOnlySpan<byte> key, ReadOnlySpan<byte> source, Span<byte> destination, out int bytesWritten)
    {
        var name = hashAlgorithm.Name;
        switch (name)
        {
            case "SHA256":
                return HMACSHA256.TryHashData(key, source, destination, out bytesWritten);
            case "SHA1":
                return HMACSHA1.TryHashData(key, source, destination, out bytesWritten);
            case "SHA384":
                return HMACSHA384.TryHashData(key, source, destination, out bytesWritten);
            case "SHA512":
                return HMACSHA512.TryHashData(key, source, destination, out bytesWritten);
            case "MD5":
                return HMACMD5.TryHashData(key, source, destination, out bytesWritten);
            default:
                throw new CryptographicException($"'{name}' is not a known hash algorithm.");
        }
    }

#endif

#if !NET11_0_OR_GREATER

    static int CryptographicOperationsHashSize(HashAlgorithmName hashAlgorithm)
    {
        var name = hashAlgorithm.Name;
        return name switch
        {
            "SHA256" => 32,
            "SHA1" => 20,
            "SHA384" => 48,
            "SHA512" => 64,
            "MD5" => 16,
            _ => throw new CryptographicException($"'{name}' is not a known hash algorithm.")
        };
    }

    static HMAC CryptographicOperationsCreateHmac(HashAlgorithmName hashAlgorithm, byte[] key)
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

#endif
}

#endif
