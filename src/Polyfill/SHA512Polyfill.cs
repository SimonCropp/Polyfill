// <auto-generated />
#pragma warning disable

namespace Polyfills;

using System.Threading.Tasks;
using System.Threading;
using System;
using System.IO;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;

[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
#if PolyPublic
public
#endif
static class SHA512Polyfill
{
    /// <summary>
    /// Computes the hash of data using the SHA-512 algorithm.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha512.hashdata?view=net-10.0#system-security-cryptography-sha512-hashdata(system-byte())
    public static byte[] HashData(byte[] source)
    {
#if NET
        return SHA512.HashData(source);
#else
        using var hasher = SHA512.Create();
        return hasher.ComputeHash(source);
#endif
    }

    /// <summary>
    /// Computes the hash of a stream using the SHA-512 algorithm.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha512.hashdata?view=net-10.0?system-security-cryptography-sha512-hashdata(system-io-stream)
    public static byte[] HashData(Stream source)
    {
#if NET7_0_OR_GREATER
        return SHA512.HashData(source);
#else
        using var hasher = SHA512.Create();
        return hasher.ComputeHash(source);
#endif
    }

#if FeatureValueTask
    /// <summary>
    /// Asynchronously computes the hash of a stream using the SHA-512 algorithm.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha512.hashdataasync?view=net-10.0?system-security-cryptography-sha512-hashdataasync(system-io-stream-system-threading-cancellationtoken)
    public static ValueTask<byte[]> HashDataAsync(Stream source, CancellationToken cancellationToken = default)
    {
#if NET7_0_OR_GREATER
        return SHA512.HashDataAsync(source, cancellationToken);
#else
        cancellationToken.ThrowIfCancellationRequested();
        using var hasher = SHA512.Create();
        return new(hasher.ComputeHash(source));
#endif
    }
#endif

#if FeatureMemory
    /// <summary>
    /// Computes the hash of a stream using the SHA-512 algorithm.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha512.hashdata?view=net-10.0?system-security-cryptography-sha512-hashdata(system-readonlyspan((system-byte)))
    public static byte[] HashData(ReadOnlySpan<byte> source)
    {
#if NET
        return SHA512.HashData(source);
#else
        using var hasher = SHA512.Create();
        return hasher.ComputeHash(source.ToArray());
#endif
    }

    /// <summary>
    /// Computes the hash of a stream using the SHA-512 algorithm.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha512.hashdata?view=net-10.0?system-security-cryptography-sha512-hashdata(system-io-stream-system-span((system-byte)))
    public static int HashData(Stream source, Span<byte> destination)
    {
#if NET7_0_OR_GREATER
        return SHA512.HashData(source, destination);
#else
        var hash = HashData(source);
        hash.CopyTo(destination);
        return hash.Length;
#endif
    }

#if FeatureValueTask
    /// <summary>
    /// Asynchronously computes the hash of a stream using the SHA-512 algorithm.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha512.hashdataasync?view=net-10.0?system-security-cryptography-sha512-hashdataasync(system-io-stream-system-memory((system-byte))-system-threading-cancellationtoken)
    public static ValueTask<int> HashDataAsync(Stream source, Memory<byte> destination, CancellationToken cancellationToken = default)
    {
#if NET7_0_OR_GREATER
        return SHA512.HashDataAsync(source, destination, cancellationToken);
#else
        cancellationToken.ThrowIfCancellationRequested();
        var hash = HashData(source);
        hash.CopyTo(destination);
        return new(hash.Length);
#endif
    }
#endif

    /// <summary>
    /// Computes the hash of a stream using the SHA-512 algorithm.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha512.hashdata?view=net-10.0?system-security-cryptography-sha512-hashdata(system-readonlyspan((system-byte))-system-span((system-byte)))
    public static int HashData(ReadOnlySpan<byte> source, Span<byte> destination)
    {
#if NET
        return SHA512.HashData(source, destination);
#else
        var hash = HashData(source);
        hash.CopyTo(destination);
        return hash.Length;
#endif
    }

    /// <summary>
    /// Attempts to compute the hash of data using the SHA-256 algorithm.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha512.tryhashdata?view=net-10.0
    public static bool TryHashData(ReadOnlySpan<byte> source, Span<byte> destination, out int bytesWritten)
    {
#if NET
        return SHA512.TryHashData(source, destination, out bytesWritten);
#else
        using var hasher = SHA512.Create();
        var hash = hasher.ComputeHash(source.ToArray());

        if (destination.Length < hash.Length)
        {
            bytesWritten = 0;
            return false;
        }

        hash.CopyTo(destination);
        bytesWritten = hash.Length;
        return true;
#endif
    }

#endif
}