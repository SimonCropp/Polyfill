#if NET8_0_OR_GREATER && !NET11_0_OR_GREATER

namespace Polyfills;

using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
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
static class HMACSHA3_512Polyfill
{
    extension(HMACSHA3_512)
    {
        /// <summary>
        /// Determines whether the HMAC-SHA3-512 of <paramref name="source"/> matches <paramref name="hash"/>, using a comparison whose time does not depend on the contents.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.hmacsha3_512.verify?view=net-11.0#system-security-cryptography-hmacsha3_512-verify(system-byte()-system-byte()-system-byte())
        public static bool Verify(byte[] key, byte[] source, byte[] hash)
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

            return HMACSHA3_512.Verify(new ReadOnlySpan<byte>(key), new ReadOnlySpan<byte>(source), new ReadOnlySpan<byte>(hash));
        }

        /// <summary>
        /// Determines whether the HMAC-SHA3-512 of <paramref name="source"/> matches <paramref name="hash"/>, using a comparison whose time does not depend on the contents.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.hmacsha3_512.verify?view=net-11.0#system-security-cryptography-hmacsha3_512-verify(system-readonlyspan((system-byte))-system-readonlyspan((system-byte))-system-readonlyspan((system-byte)))
        public static bool Verify(ReadOnlySpan<byte> key, ReadOnlySpan<byte> source, ReadOnlySpan<byte> hash)
        {
            Polyfill.CheckHmacHashLength(hash.Length, HMACSHA3_512.HashSizeInBytes);

            Span<byte> mac = stackalloc byte[HMACSHA3_512.HashSizeInBytes];
            HMACSHA3_512.HashData(key, source, mac);
            return Polyfill.FixedTimeEqualsAndClear(mac, hash);
        }

        /// <summary>
        /// Determines whether the HMAC-SHA3-512 of the <paramref name="source"/> stream matches <paramref name="hash"/>, using a comparison whose time does not depend on the contents.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.hmacsha3_512.verify?view=net-11.0#system-security-cryptography-hmacsha3_512-verify(system-byte()-system-io-stream-system-byte())
        public static bool Verify(byte[] key, Stream source, byte[] hash)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (hash == null)
            {
                throw new ArgumentNullException(nameof(hash));
            }

            return HMACSHA3_512.Verify(new ReadOnlySpan<byte>(key), source, new ReadOnlySpan<byte>(hash));
        }

        /// <summary>
        /// Determines whether the HMAC-SHA3-512 of the <paramref name="source"/> stream matches <paramref name="hash"/>, using a comparison whose time does not depend on the contents.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.hmacsha3_512.verify?view=net-11.0#system-security-cryptography-hmacsha3_512-verify(system-readonlyspan((system-byte))-system-io-stream-system-readonlyspan((system-byte)))
        public static bool Verify(ReadOnlySpan<byte> key, Stream source, ReadOnlySpan<byte> hash)
        {
            Polyfill.CheckHmacStream(source, hash.Length, HMACSHA3_512.HashSizeInBytes);

            Span<byte> mac = stackalloc byte[HMACSHA3_512.HashSizeInBytes];
            HMACSHA3_512.HashData(key, source, mac);
            return Polyfill.FixedTimeEqualsAndClear(mac, hash);
        }

        /// <summary>
        /// Asynchronously determines whether the HMAC-SHA3-512 of the <paramref name="source"/> stream matches <paramref name="hash"/>, using a comparison whose time does not depend on the contents.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.hmacsha3_512.verifyasync?view=net-11.0#system-security-cryptography-hmacsha3_512-verifyasync(system-byte()-system-io-stream-system-byte()-system-threading-cancellationtoken)
        public static ValueTask<bool> VerifyAsync(byte[] key, Stream source, byte[] hash, CancellationToken cancellationToken = default)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (hash == null)
            {
                throw new ArgumentNullException(nameof(hash));
            }

            return HMACSHA3_512.VerifyAsync(new ReadOnlyMemory<byte>(key), source, new ReadOnlyMemory<byte>(hash), cancellationToken);
        }

        /// <summary>
        /// Asynchronously determines whether the HMAC-SHA3-512 of the <paramref name="source"/> stream matches <paramref name="hash"/>, using a comparison whose time does not depend on the contents.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.hmacsha3_512.verifyasync?view=net-11.0#system-security-cryptography-hmacsha3_512-verifyasync(system-readonlymemory((system-byte))-system-io-stream-system-readonlymemory((system-byte))-system-threading-cancellationtoken)
        public static ValueTask<bool> VerifyAsync(ReadOnlyMemory<byte> key, Stream source, ReadOnlyMemory<byte> hash, CancellationToken cancellationToken = default)
        {
            // the arguments are validated before the returned task starts, as in the BCL
            Polyfill.CheckHmacStream(source, hash.Length, HMACSHA3_512.HashSizeInBytes);
            return VerifyCoreAsync(key, source, hash, cancellationToken);

            static async ValueTask<bool> VerifyCoreAsync(ReadOnlyMemory<byte> key, Stream source, ReadOnlyMemory<byte> hash, CancellationToken cancellationToken)
            {
                var mac = await HMACSHA3_512.HashDataAsync(key, source, cancellationToken).ConfigureAwait(false);
                return Polyfill.FixedTimeEqualsAndClear(mac, hash.Span);
            }
        }
    }
}

#endif
