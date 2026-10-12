#if !NET7_0_OR_GREATER

namespace Polyfills;

// ReSharper disable RedundantUsingDirective
using System.Threading.Tasks;
using System.Threading;
using System;
// ReSharper restore RedundantUsingDirective
using System.IO;
using System.Security.Cryptography;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
#if PolyPublic
public
#endif
static class SHA256Polyfill
{
    extension(SHA256)
    {
#if !NET

        /// <summary>
        /// Computes the hash of data using the SHA-256 algorithm.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha256.hashdata?view=net-11.0#system-security-cryptography-sha256-hashdata(system-byte())
        public static byte[] HashData(byte[] source)
        {
            using var hasher = SHA256.Create();
            return hasher.ComputeHash(source);
        }

#endif

#if !NET7_0_OR_GREATER

        /// <summary>
        /// Computes the hash of a stream using the SHA-256 algorithm.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha256.hashdata?view=net-11.0#system-security-cryptography-sha256-hashdata(system-io-stream)
        public static byte[] HashData(Stream source)
        {
            using var hasher = SHA256.Create();
            return hasher.ComputeHash(source);
        }

#endif

#if FeatureValueTask && !NET7_0_OR_GREATER
        /// <summary>
        /// Asynchronously computes the hash of a stream using the SHA-256 algorithm.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha256.hashdataasync?view=net-11.0#system-security-cryptography-sha256-hashdataasync(system-io-stream-system-threading-cancellationtoken)
        public static ValueTask<byte[]> HashDataAsync(Stream source, CancellationToken cancellationToken = default) =>
            new(Polyfill.HashStreamAsync(SHA256.Create(), source, cancellationToken));
#endif

#if FeatureMemory

#if !NET7_0_OR_GREATER

        /// <summary>
        /// Computes the hash of a stream using the SHA-256 algorithm.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha256.hashdata?view=net-11.0#system-security-cryptography-sha256-hashdata(system-io-stream-system-span((system-byte)))
        public static int HashData(Stream source, Span<byte> destination)
        {
            var hash = HashData(source);
            hash.CopyTo(destination);
            return hash.Length;
        }

#endif

#if !NET

        /// <summary>
        /// Computes the hash of a stream using the SHA-256 algorithm.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha256.hashdata?view=net-11.0#system-security-cryptography-sha256-hashdata(system-readonlyspan((system-byte)))
        public static byte[] HashData(ReadOnlySpan<byte> source)
        {
            using var hasher = SHA256.Create();
            return Polyfill.HashSpan(hasher, source);
        }

#endif

#if FeatureValueTask && !NET7_0_OR_GREATER

        /// <summary>
        /// Asynchronously computes the hash of a stream using the SHA-256 algorithm.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha256.hashdataasync?view=net-11.0#system-security-cryptography-sha256-hashdataasync(system-io-stream-system-memory((system-byte))-system-threading-cancellationtoken)
        public static ValueTask<int> HashDataAsync(Stream source, Memory<byte> destination, CancellationToken cancellationToken = default) =>
            new(Polyfill.HashStreamAsync(SHA256.Create(), source, destination, cancellationToken));

#endif

#if !NET

        /// <summary>
        /// Computes the hash of a stream using the SHA-256 algorithm.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha256.hashdata?view=net-11.0#system-security-cryptography-sha256-hashdata(system-readonlyspan((system-byte))-system-span((system-byte)))
        public static int HashData(ReadOnlySpan<byte> source, Span<byte> destination)
        {
            using var hasher = SHA256.Create();
            return Polyfill.HashSpan(hasher, source, destination);
        }

        /// <summary>
        /// Attempts to compute the hash of data using the SHA-256 algorithm.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha256.tryhashdata?view=net-11.0
        public static bool TryHashData(ReadOnlySpan<byte> source, Span<byte> destination, out int bytesWritten)
        {
            using var hasher = SHA256.Create();
            return Polyfill.TryHashSpan(hasher, source, destination, out bytesWritten);
        }

#endif
#endif
    }
}
#endif
