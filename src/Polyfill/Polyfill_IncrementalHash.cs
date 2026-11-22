// IncrementalHash ships in .NET Framework from 4.7.1
#if !NET11_0_OR_GREATER && (!NETFRAMEWORK || NET471_OR_GREATER)

namespace Polyfills;

using System;
using System.Security.Cryptography;

static partial class Polyfill
{
    /// <summary>
    /// Verifies the hash or HMAC for the data accumulated from prior calls to the AppendData methods, and resets the object to its initial state.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.incrementalhash.verifyhashandreset?view=net-11.0#system-security-cryptography-incrementalhash-verifyhashandreset(system-byte())
    public static bool VerifyHashAndReset(this IncrementalHash target, byte[] hash)
    {
        if (hash == null)
        {
            throw new ArgumentNullException(nameof(hash));
        }

#if FeatureMemory
        return target.VerifyHashAndReset(new ReadOnlySpan<byte>(hash));
#else
        CheckIncrementalHashLength(target, hash.Length);

        var computed = target.GetHashAndReset();

        // The time taken must not depend on the contents
        var accumulated = 0;
        for (var i = 0; i < computed.Length; i++)
        {
            accumulated |= computed[i] ^ hash[i];
        }

        Array.Clear(computed, 0, computed.Length);
        return accumulated == 0;
#endif
    }

#if FeatureMemory

    /// <summary>
    /// Verifies the hash or HMAC for the data accumulated from prior calls to the AppendData methods, and resets the object to its initial state.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.incrementalhash.verifyhashandreset?view=net-11.0#system-security-cryptography-incrementalhash-verifyhashandreset(system-readonlyspan((system-byte)))
    public static bool VerifyHashAndReset(this IncrementalHash target, ReadOnlySpan<byte> hash)
    {
        CheckIncrementalHashLength(target, hash.Length);

#if NET5_0_OR_GREATER
        // 64 bytes holds the longest hash IncrementalHash produces (SHA-512 and SHA3-512)
        Span<byte> computed = stackalloc byte[64];
        computed = computed.Slice(0, target.GetHashAndReset(computed));
#else
        var computed = target.GetHashAndReset();
#endif

        return FixedTimeEqualsAndClear(computed, hash);
    }

#endif

#if NET5_0_OR_GREATER

    /// <summary>
    /// Verifies the hash or HMAC for the data accumulated from prior calls to the AppendData methods, without resetting the object to its initial state.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.incrementalhash.verifycurrenthash?view=net-11.0#system-security-cryptography-incrementalhash-verifycurrenthash(system-byte())
    public static bool VerifyCurrentHash(this IncrementalHash target, byte[] hash)
    {
        if (hash == null)
        {
            throw new ArgumentNullException(nameof(hash));
        }

        return target.VerifyCurrentHash(new ReadOnlySpan<byte>(hash));
    }

    /// <summary>
    /// Verifies the hash or HMAC for the data accumulated from prior calls to the AppendData methods, without resetting the object to its initial state.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.incrementalhash.verifycurrenthash?view=net-11.0#system-security-cryptography-incrementalhash-verifycurrenthash(system-readonlyspan((system-byte)))
    public static bool VerifyCurrentHash(this IncrementalHash target, ReadOnlySpan<byte> hash)
    {
        CheckIncrementalHashLength(target, hash.Length);

        // 64 bytes holds the longest hash IncrementalHash produces (SHA-512 and SHA3-512)
        Span<byte> computed = stackalloc byte[64];
        computed = computed.Slice(0, target.GetCurrentHash(computed));
        return FixedTimeEqualsAndClear(computed, hash);
    }

#endif

    // Checked before the hash is computed, so a wrong length leaves the accumulated data in place, as in the BCL
    static void CheckIncrementalHashLength(IncrementalHash target, int hashLength)
    {
#if NET5_0_OR_GREATER
        var expected = target.HashLengthInBytes;
#else
        // IncrementalHash only supports these algorithms on targets without HashLengthInBytes.
        // On .NET Core an HMAC instance reports its algorithm with an HMAC prefix.
        var expected = target.AlgorithmName.Name switch
        {
            "MD5" or "HMACMD5" => 16,
            "SHA1" or "HMACSHA1" => 20,
            "SHA256" or "HMACSHA256" => 32,
            "SHA384" or "HMACSHA384" => 48,
            "SHA512" or "HMACSHA512" => 64,
            _ => hashLength
        };
#endif

        if (hashLength != expected)
        {
            throw new ArgumentException($"The hash must be {expected} bytes in length.", "hash");
        }
    }
}

#endif
