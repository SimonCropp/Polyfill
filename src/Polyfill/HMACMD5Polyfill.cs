#if !NET11_0_OR_GREATER

namespace Polyfills;

// ReSharper disable RedundantUsingDirective
using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
// ReSharper restore RedundantUsingDirective
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
static class HMACMD5Polyfill
{
    extension(HMACMD5)
    {
        /// <summary>
        /// Determines whether the HMAC-MD5 of <paramref name="source"/> matches <paramref name="hash"/>, using a comparison whose time does not depend on the contents.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.hmacmd5.verify?view=net-11.0#system-security-cryptography-hmacmd5-verify(system-byte()-system-byte()-system-byte())
        public static bool Verify(byte[] key, byte[] source, byte[] hash) =>
            CryptographicOperations.VerifyHmac(HashAlgorithmName.MD5, key, source, hash);

        /// <summary>
        /// Determines whether the HMAC-MD5 of the <paramref name="source"/> stream matches <paramref name="hash"/>, using a comparison whose time does not depend on the contents.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.hmacmd5.verify?view=net-11.0#system-security-cryptography-hmacmd5-verify(system-byte()-system-io-stream-system-byte())
        public static bool Verify(byte[] key, Stream source, byte[] hash) =>
            CryptographicOperations.VerifyHmac(HashAlgorithmName.MD5, key, source, hash);

#if FeatureValueTask

        /// <summary>
        /// Asynchronously determines whether the HMAC-MD5 of the <paramref name="source"/> stream matches <paramref name="hash"/>, using a comparison whose time does not depend on the contents.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.hmacmd5.verifyasync?view=net-11.0#system-security-cryptography-hmacmd5-verifyasync(system-byte()-system-io-stream-system-byte()-system-threading-cancellationtoken)
        public static ValueTask<bool> VerifyAsync(byte[] key, Stream source, byte[] hash, CancellationToken cancellationToken = default) =>
            CryptographicOperations.VerifyHmacAsync(HashAlgorithmName.MD5, key, source, hash, cancellationToken);

#endif

#if FeatureMemory

        /// <summary>
        /// Determines whether the HMAC-MD5 of <paramref name="source"/> matches <paramref name="hash"/>, using a comparison whose time does not depend on the contents.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.hmacmd5.verify?view=net-11.0#system-security-cryptography-hmacmd5-verify(system-readonlyspan((system-byte))-system-readonlyspan((system-byte))-system-readonlyspan((system-byte)))
        public static bool Verify(ReadOnlySpan<byte> key, ReadOnlySpan<byte> source, ReadOnlySpan<byte> hash) =>
            CryptographicOperations.VerifyHmac(HashAlgorithmName.MD5, key, source, hash);

        /// <summary>
        /// Determines whether the HMAC-MD5 of the <paramref name="source"/> stream matches <paramref name="hash"/>, using a comparison whose time does not depend on the contents.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.hmacmd5.verify?view=net-11.0#system-security-cryptography-hmacmd5-verify(system-readonlyspan((system-byte))-system-io-stream-system-readonlyspan((system-byte)))
        public static bool Verify(ReadOnlySpan<byte> key, Stream source, ReadOnlySpan<byte> hash) =>
            CryptographicOperations.VerifyHmac(HashAlgorithmName.MD5, key, source, hash);

#if FeatureValueTask

        /// <summary>
        /// Asynchronously determines whether the HMAC-MD5 of the <paramref name="source"/> stream matches <paramref name="hash"/>, using a comparison whose time does not depend on the contents.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.hmacmd5.verifyasync?view=net-11.0#system-security-cryptography-hmacmd5-verifyasync(system-readonlymemory((system-byte))-system-io-stream-system-readonlymemory((system-byte))-system-threading-cancellationtoken)
        public static ValueTask<bool> VerifyAsync(ReadOnlyMemory<byte> key, Stream source, ReadOnlyMemory<byte> hash, CancellationToken cancellationToken = default) =>
            CryptographicOperations.VerifyHmacAsync(HashAlgorithmName.MD5, key, source, hash, cancellationToken);

#endif

#endif
    }
}

#endif
