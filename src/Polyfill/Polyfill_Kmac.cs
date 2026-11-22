// The KMAC types ship from net9
#if NET9_0_OR_GREATER && !NET11_0_OR_GREATER

namespace Polyfills;

using System;
using System.Security.Cryptography;

static partial class Polyfill
{
    /// <summary>
    /// Verifies the hash for the data accumulated from prior calls to the AppendData methods, without resetting the object to its initial state.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.kmac128.verifycurrenthash?view=net-11.0#system-security-cryptography-kmac128-verifycurrenthash(system-readonlyspan((system-byte)))
    public static bool VerifyCurrentHash(this Kmac128 target, ReadOnlySpan<byte> hash)
    {
        CheckKmacHash(hash);
        return FixedTimeEqualsAndClear(target.GetCurrentHash(hash.Length), hash);
    }

    /// <summary>
    /// Verifies the hash for the data accumulated from prior calls to the AppendData methods, without resetting the object to its initial state.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.kmac128.verifycurrenthash?view=net-11.0#system-security-cryptography-kmac128-verifycurrenthash(system-byte())
    public static bool VerifyCurrentHash(this Kmac128 target, byte[] hash) =>
        target.VerifyCurrentHash(KmacHashSpan(hash));

    /// <summary>
    /// Verifies the hash for the data accumulated from prior calls to the AppendData methods, and resets the object to its initial state.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.kmac128.verifyhashandreset?view=net-11.0#system-security-cryptography-kmac128-verifyhashandreset(system-readonlyspan((system-byte)))
    public static bool VerifyHashAndReset(this Kmac128 target, ReadOnlySpan<byte> hash)
    {
        CheckKmacHash(hash);
        return FixedTimeEqualsAndClear(target.GetHashAndReset(hash.Length), hash);
    }

    /// <summary>
    /// Verifies the hash for the data accumulated from prior calls to the AppendData methods, and resets the object to its initial state.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.kmac128.verifyhashandreset?view=net-11.0#system-security-cryptography-kmac128-verifyhashandreset(system-byte())
    public static bool VerifyHashAndReset(this Kmac128 target, byte[] hash) =>
        target.VerifyHashAndReset(KmacHashSpan(hash));

    /// <summary>
    /// Verifies the hash for the data accumulated from prior calls to the AppendData methods, without resetting the object to its initial state.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.kmac256.verifycurrenthash?view=net-11.0#system-security-cryptography-kmac256-verifycurrenthash(system-readonlyspan((system-byte)))
    public static bool VerifyCurrentHash(this Kmac256 target, ReadOnlySpan<byte> hash)
    {
        CheckKmacHash(hash);
        return FixedTimeEqualsAndClear(target.GetCurrentHash(hash.Length), hash);
    }

    /// <summary>
    /// Verifies the hash for the data accumulated from prior calls to the AppendData methods, without resetting the object to its initial state.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.kmac256.verifycurrenthash?view=net-11.0#system-security-cryptography-kmac256-verifycurrenthash(system-byte())
    public static bool VerifyCurrentHash(this Kmac256 target, byte[] hash) =>
        target.VerifyCurrentHash(KmacHashSpan(hash));

    /// <summary>
    /// Verifies the hash for the data accumulated from prior calls to the AppendData methods, and resets the object to its initial state.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.kmac256.verifyhashandreset?view=net-11.0#system-security-cryptography-kmac256-verifyhashandreset(system-readonlyspan((system-byte)))
    public static bool VerifyHashAndReset(this Kmac256 target, ReadOnlySpan<byte> hash)
    {
        CheckKmacHash(hash);
        return FixedTimeEqualsAndClear(target.GetHashAndReset(hash.Length), hash);
    }

    /// <summary>
    /// Verifies the hash for the data accumulated from prior calls to the AppendData methods, and resets the object to its initial state.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.kmac256.verifyhashandreset?view=net-11.0#system-security-cryptography-kmac256-verifyhashandreset(system-byte())
    public static bool VerifyHashAndReset(this Kmac256 target, byte[] hash) =>
        target.VerifyHashAndReset(KmacHashSpan(hash));

    /// <summary>
    /// Verifies the hash for the data accumulated from prior calls to the AppendData methods, without resetting the object to its initial state.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.kmacxof128.verifycurrenthash?view=net-11.0#system-security-cryptography-kmacxof128-verifycurrenthash(system-readonlyspan((system-byte)))
    public static bool VerifyCurrentHash(this KmacXof128 target, ReadOnlySpan<byte> hash)
    {
        CheckKmacHash(hash);
        return FixedTimeEqualsAndClear(target.GetCurrentHash(hash.Length), hash);
    }

    /// <summary>
    /// Verifies the hash for the data accumulated from prior calls to the AppendData methods, without resetting the object to its initial state.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.kmacxof128.verifycurrenthash?view=net-11.0#system-security-cryptography-kmacxof128-verifycurrenthash(system-byte())
    public static bool VerifyCurrentHash(this KmacXof128 target, byte[] hash) =>
        target.VerifyCurrentHash(KmacHashSpan(hash));

    /// <summary>
    /// Verifies the hash for the data accumulated from prior calls to the AppendData methods, and resets the object to its initial state.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.kmacxof128.verifyhashandreset?view=net-11.0#system-security-cryptography-kmacxof128-verifyhashandreset(system-readonlyspan((system-byte)))
    public static bool VerifyHashAndReset(this KmacXof128 target, ReadOnlySpan<byte> hash)
    {
        CheckKmacHash(hash);
        return FixedTimeEqualsAndClear(target.GetHashAndReset(hash.Length), hash);
    }

    /// <summary>
    /// Verifies the hash for the data accumulated from prior calls to the AppendData methods, and resets the object to its initial state.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.kmacxof128.verifyhashandreset?view=net-11.0#system-security-cryptography-kmacxof128-verifyhashandreset(system-byte())
    public static bool VerifyHashAndReset(this KmacXof128 target, byte[] hash) =>
        target.VerifyHashAndReset(KmacHashSpan(hash));

    /// <summary>
    /// Verifies the hash for the data accumulated from prior calls to the AppendData methods, without resetting the object to its initial state.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.kmacxof256.verifycurrenthash?view=net-11.0#system-security-cryptography-kmacxof256-verifycurrenthash(system-readonlyspan((system-byte)))
    public static bool VerifyCurrentHash(this KmacXof256 target, ReadOnlySpan<byte> hash)
    {
        CheckKmacHash(hash);
        return FixedTimeEqualsAndClear(target.GetCurrentHash(hash.Length), hash);
    }

    /// <summary>
    /// Verifies the hash for the data accumulated from prior calls to the AppendData methods, without resetting the object to its initial state.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.kmacxof256.verifycurrenthash?view=net-11.0#system-security-cryptography-kmacxof256-verifycurrenthash(system-byte())
    public static bool VerifyCurrentHash(this KmacXof256 target, byte[] hash) =>
        target.VerifyCurrentHash(KmacHashSpan(hash));

    /// <summary>
    /// Verifies the hash for the data accumulated from prior calls to the AppendData methods, and resets the object to its initial state.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.kmacxof256.verifyhashandreset?view=net-11.0#system-security-cryptography-kmacxof256-verifyhashandreset(system-readonlyspan((system-byte)))
    public static bool VerifyHashAndReset(this KmacXof256 target, ReadOnlySpan<byte> hash)
    {
        CheckKmacHash(hash);
        return FixedTimeEqualsAndClear(target.GetHashAndReset(hash.Length), hash);
    }

    /// <summary>
    /// Verifies the hash for the data accumulated from prior calls to the AppendData methods, and resets the object to its initial state.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.kmacxof256.verifyhashandreset?view=net-11.0#system-security-cryptography-kmacxof256-verifyhashandreset(system-byte())
    public static bool VerifyHashAndReset(this KmacXof256 target, byte[] hash) =>
        target.VerifyHashAndReset(KmacHashSpan(hash));

    // The KMAC output depends on the requested length, so the hash's own length is what is computed and compared
    static void CheckKmacHash(ReadOnlySpan<byte> hash)
    {
        if (hash.IsEmpty)
        {
            throw new ArgumentException("The hash must not be empty.", nameof(hash));
        }
    }

    static ReadOnlySpan<byte> KmacHashSpan(byte[] hash)
    {
        if (hash == null)
        {
            throw new ArgumentNullException(nameof(hash));
        }

        return hash;
    }
}

#endif
