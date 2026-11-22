using System.IO;
using System.Linq;
using System.Security.Cryptography;

// Covers the static Verify and VerifyAsync members added to the HMAC types in net11,
// and the VerifyCurrentHash and VerifyHashAndReset members added to IncrementalHash and the KMAC types.
public class HmacVerifyPolyfillTests
{
    static readonly byte[] macKey = [10, 20, 30, 40, 50, 60];
    static readonly byte[] message = [1, 2, 3, 4, 5];

    static IEnumerable<string> Algorithms()
    {
        yield return "MD5";
        yield return "SHA1";
        yield return "SHA256";
        yield return "SHA384";
        yield return "SHA512";
#if NET8_0_OR_GREATER
        if (HMACSHA3_256.IsSupported)
        {
            yield return "SHA3_256";
        }

        if (HMACSHA3_384.IsSupported)
        {
            yield return "SHA3_384";
        }

        if (HMACSHA3_512.IsSupported)
        {
            yield return "SHA3_512";
        }
#endif
    }

    static HMAC CreateHmac(string algorithm) =>
        algorithm switch
        {
            "MD5" => new HMACMD5(macKey),
            "SHA1" => new HMACSHA1(macKey),
            "SHA256" => new HMACSHA256(macKey),
            "SHA384" => new HMACSHA384(macKey),
            "SHA512" => new HMACSHA512(macKey),
#if NET8_0_OR_GREATER
            "SHA3_256" => new HMACSHA3_256(macKey),
            "SHA3_384" => new HMACSHA3_384(macKey),
            "SHA3_512" => new HMACSHA3_512(macKey),
#endif
            _ => throw new ArgumentException(algorithm)
        };

    static byte[] ExpectedMac(string algorithm)
    {
        using var hmac = CreateHmac(algorithm);
        return hmac.ComputeHash(message);
    }

    static byte[] Tampered(byte[] mac)
    {
        var tampered = (byte[]) mac.Clone();
        tampered[0] ^= 1;
        return tampered;
    }

    static bool Throws<TException>(Func<object?> action)
        where TException : Exception
    {
        try
        {
            action();
            return false;
        }
        catch (TException)
        {
            return true;
        }
    }

    static bool Verify(string algorithm, byte[] key, byte[] source, byte[] hash) =>
        algorithm switch
        {
            "MD5" => HMACMD5.Verify(key, source, hash),
            "SHA1" => HMACSHA1.Verify(key, source, hash),
            "SHA256" => HMACSHA256.Verify(key, source, hash),
            "SHA384" => HMACSHA384.Verify(key, source, hash),
            "SHA512" => HMACSHA512.Verify(key, source, hash),
#if NET8_0_OR_GREATER
            "SHA3_256" => HMACSHA3_256.Verify(key, source, hash),
            "SHA3_384" => HMACSHA3_384.Verify(key, source, hash),
            "SHA3_512" => HMACSHA3_512.Verify(key, source, hash),
#endif
            _ => throw new ArgumentException(algorithm)
        };

    static bool Verify(string algorithm, byte[] key, Stream source, byte[] hash) =>
        algorithm switch
        {
            "MD5" => HMACMD5.Verify(key, source, hash),
            "SHA1" => HMACSHA1.Verify(key, source, hash),
            "SHA256" => HMACSHA256.Verify(key, source, hash),
            "SHA384" => HMACSHA384.Verify(key, source, hash),
            "SHA512" => HMACSHA512.Verify(key, source, hash),
#if NET8_0_OR_GREATER
            "SHA3_256" => HMACSHA3_256.Verify(key, source, hash),
            "SHA3_384" => HMACSHA3_384.Verify(key, source, hash),
            "SHA3_512" => HMACSHA3_512.Verify(key, source, hash),
#endif
            _ => throw new ArgumentException(algorithm)
        };

    [Test]
    public async Task Verify_Arrays()
    {
        var failures = new List<string>();

        foreach (var algorithm in Algorithms())
        {
            var mac = ExpectedMac(algorithm);

            void Check(string name, bool condition)
            {
                if (!condition)
                {
                    failures.Add($"{algorithm} {name}");
                }
            }

            Check("valid", Verify(algorithm, macKey, message, mac));
            Check("tampered", !Verify(algorithm, macKey, message, Tampered(mac)));
            Check("other key", !Verify(algorithm, [1, 2, 3], message, mac));
            Check("short hash", Throws<ArgumentException>(() => Verify(algorithm, macKey, message, new byte[mac.Length - 1])));
            Check("long hash", Throws<ArgumentException>(() => Verify(algorithm, macKey, message, new byte[mac.Length + 1])));
            Check("null key", Throws<ArgumentNullException>(() => Verify(algorithm, null!, message, mac)));
            Check("null source", Throws<ArgumentNullException>(() => Verify(algorithm, macKey, (byte[]) null!, mac)));
            Check("null hash", Throws<ArgumentNullException>(() => Verify(algorithm, macKey, message, null!)));
        }

        await Assert.That(failures).IsEmpty();
    }

    [Test]
    public async Task Verify_Stream()
    {
        var failures = new List<string>();

        foreach (var algorithm in Algorithms())
        {
            var mac = ExpectedMac(algorithm);

            void Check(string name, bool condition)
            {
                if (!condition)
                {
                    failures.Add($"{algorithm} {name}");
                }
            }

            Check("valid", Verify(algorithm, macKey, new MemoryStream(message), mac));
            Check("tampered", !Verify(algorithm, macKey, new MemoryStream(message), Tampered(mac)));
            Check("short hash", Throws<ArgumentException>(() => Verify(algorithm, macKey, new MemoryStream(message), new byte[mac.Length - 1])));
            Check("null key", Throws<ArgumentNullException>(() => Verify(algorithm, null!, new MemoryStream(message), mac)));
            Check("null source", Throws<ArgumentNullException>(() => Verify(algorithm, macKey, (Stream) null!, mac)));
            Check("null hash", Throws<ArgumentNullException>(() => Verify(algorithm, macKey, new MemoryStream(message), null!)));
        }

        await Assert.That(failures).IsEmpty();
    }

#if FeatureValueTask

    static ValueTask<bool> VerifyAsync(string algorithm, byte[] key, Stream source, byte[] hash) =>
        algorithm switch
        {
            "MD5" => HMACMD5.VerifyAsync(key, source, hash),
            "SHA1" => HMACSHA1.VerifyAsync(key, source, hash),
            "SHA256" => HMACSHA256.VerifyAsync(key, source, hash),
            "SHA384" => HMACSHA384.VerifyAsync(key, source, hash),
            "SHA512" => HMACSHA512.VerifyAsync(key, source, hash),
#if NET8_0_OR_GREATER
            "SHA3_256" => HMACSHA3_256.VerifyAsync(key, source, hash),
            "SHA3_384" => HMACSHA3_384.VerifyAsync(key, source, hash),
            "SHA3_512" => HMACSHA3_512.VerifyAsync(key, source, hash),
#endif
            _ => throw new ArgumentException(algorithm)
        };

    [Test]
    public async Task VerifyAsync_Arrays()
    {
        var failures = new List<string>();

        foreach (var algorithm in Algorithms())
        {
            var mac = ExpectedMac(algorithm);

            void Check(string name, bool condition)
            {
                if (!condition)
                {
                    failures.Add($"{algorithm} {name}");
                }
            }

            Check("valid", await VerifyAsync(algorithm, macKey, new MemoryStream(message), mac));
            Check("tampered", !await VerifyAsync(algorithm, macKey, new MemoryStream(message), Tampered(mac)));
            // the arguments are validated before a task is returned
            Check("short hash", Throws<ArgumentException>(() => VerifyAsync(algorithm, macKey, new MemoryStream(message), new byte[mac.Length - 1])));
            Check("null key", Throws<ArgumentNullException>(() => VerifyAsync(algorithm, null!, new MemoryStream(message), mac)));
            Check("null source", Throws<ArgumentNullException>(() => VerifyAsync(algorithm, macKey, null!, mac)));
            Check("null hash", Throws<ArgumentNullException>(() => VerifyAsync(algorithm, macKey, new MemoryStream(message), null!)));
        }

        await Assert.That(failures).IsEmpty();
    }

#endif

#if FeatureMemory

    static bool VerifySpan(string algorithm, ReadOnlySpan<byte> key, ReadOnlySpan<byte> source, ReadOnlySpan<byte> hash) =>
        algorithm switch
        {
            "MD5" => HMACMD5.Verify(key, source, hash),
            "SHA1" => HMACSHA1.Verify(key, source, hash),
            "SHA256" => HMACSHA256.Verify(key, source, hash),
            "SHA384" => HMACSHA384.Verify(key, source, hash),
            "SHA512" => HMACSHA512.Verify(key, source, hash),
#if NET8_0_OR_GREATER
            "SHA3_256" => HMACSHA3_256.Verify(key, source, hash),
            "SHA3_384" => HMACSHA3_384.Verify(key, source, hash),
            "SHA3_512" => HMACSHA3_512.Verify(key, source, hash),
#endif
            _ => throw new ArgumentException(algorithm)
        };

    static bool VerifySpan(string algorithm, ReadOnlySpan<byte> key, Stream source, ReadOnlySpan<byte> hash) =>
        algorithm switch
        {
            "MD5" => HMACMD5.Verify(key, source, hash),
            "SHA1" => HMACSHA1.Verify(key, source, hash),
            "SHA256" => HMACSHA256.Verify(key, source, hash),
            "SHA384" => HMACSHA384.Verify(key, source, hash),
            "SHA512" => HMACSHA512.Verify(key, source, hash),
#if NET8_0_OR_GREATER
            "SHA3_256" => HMACSHA3_256.Verify(key, source, hash),
            "SHA3_384" => HMACSHA3_384.Verify(key, source, hash),
            "SHA3_512" => HMACSHA3_512.Verify(key, source, hash),
#endif
            _ => throw new ArgumentException(algorithm)
        };

    [Test]
    public async Task Verify_Spans()
    {
        var failures = new List<string>();

        foreach (var algorithm in Algorithms())
        {
            var mac = ExpectedMac(algorithm);

            void Check(string name, bool condition)
            {
                if (!condition)
                {
                    failures.Add($"{algorithm} {name}");
                }
            }

            // a slice of a larger buffer, so the source is not a whole array
            var buffer = new byte[message.Length + 4];
            message.CopyTo(buffer, 2);

            Check("valid", VerifySpan(algorithm, macKey, new ReadOnlySpan<byte>(buffer, 2, message.Length), mac));
            Check("tampered", !VerifySpan(algorithm, macKey, new ReadOnlySpan<byte>(buffer, 2, message.Length), Tampered(mac)));
            Check("empty key", !VerifySpan(algorithm, ReadOnlySpan<byte>.Empty, message, mac));
            Check("short hash", Throws<ArgumentException>(() => VerifySpan(algorithm, macKey, message, new byte[mac.Length - 1])));
            Check("stream valid", VerifySpan(algorithm, macKey, new MemoryStream(message), mac));
            Check("stream tampered", !VerifySpan(algorithm, macKey, new MemoryStream(message), Tampered(mac)));
            Check("stream short hash", Throws<ArgumentException>(() => VerifySpan(algorithm, macKey, new MemoryStream(message), new byte[mac.Length - 1])));
            Check("stream null source", Throws<ArgumentNullException>(() => VerifySpan(algorithm, macKey, (Stream) null!, mac)));
        }

        await Assert.That(failures).IsEmpty();
    }

#if FeatureValueTask

    static ValueTask<bool> VerifyMemoryAsync(string algorithm, ReadOnlyMemory<byte> key, Stream source, ReadOnlyMemory<byte> hash) =>
        algorithm switch
        {
            "MD5" => HMACMD5.VerifyAsync(key, source, hash),
            "SHA1" => HMACSHA1.VerifyAsync(key, source, hash),
            "SHA256" => HMACSHA256.VerifyAsync(key, source, hash),
            "SHA384" => HMACSHA384.VerifyAsync(key, source, hash),
            "SHA512" => HMACSHA512.VerifyAsync(key, source, hash),
#if NET8_0_OR_GREATER
            "SHA3_256" => HMACSHA3_256.VerifyAsync(key, source, hash),
            "SHA3_384" => HMACSHA3_384.VerifyAsync(key, source, hash),
            "SHA3_512" => HMACSHA3_512.VerifyAsync(key, source, hash),
#endif
            _ => throw new ArgumentException(algorithm)
        };

    [Test]
    public async Task VerifyAsync_Memory()
    {
        var failures = new List<string>();

        foreach (var algorithm in Algorithms())
        {
            var mac = ExpectedMac(algorithm);

            void Check(string name, bool condition)
            {
                if (!condition)
                {
                    failures.Add($"{algorithm} {name}");
                }
            }

            Check("valid", await VerifyMemoryAsync(algorithm, macKey, new MemoryStream(message), mac));
            Check("tampered", !await VerifyMemoryAsync(algorithm, macKey, new MemoryStream(message), Tampered(mac)));
            Check("short hash", Throws<ArgumentException>(() => VerifyMemoryAsync(algorithm, macKey, new MemoryStream(message), new byte[mac.Length - 1])));
            Check("null source", Throws<ArgumentNullException>(() => VerifyMemoryAsync(algorithm, macKey, (Stream) null!, mac)));
        }

        await Assert.That(failures).IsEmpty();
    }

#endif

#endif

#if !NETFRAMEWORK || NET471_OR_GREATER

    static IncrementalHash CreateIncrementalHmac() =>
        IncrementalHash.CreateHMAC(HashAlgorithmName.SHA256, macKey);

    [Test]
    public async Task IncrementalHash_VerifyHashAndReset()
    {
        var mac = ExpectedMac("SHA256");

        using var hmac = CreateIncrementalHmac();
        hmac.AppendData(message);
        await Assert.That(hmac.VerifyHashAndReset(mac)).IsTrue();

        // the previous call reset the state, so nothing has been accumulated
        await Assert.That(hmac.VerifyHashAndReset(mac)).IsFalse();

        hmac.AppendData(message);
        await Assert.That(hmac.VerifyHashAndReset(Tampered(mac))).IsFalse();
    }

    [Test]
    public async Task IncrementalHash_VerifyHashAndReset_Hash()
    {
        byte[] expected;
        using (var sha1 = SHA1.Create())
        {
            expected = sha1.ComputeHash(message);
        }

        using var hash = IncrementalHash.CreateHash(HashAlgorithmName.SHA1);
        hash.AppendData(message);
        await Assert.That(() => hash.VerifyHashAndReset(new byte[19])).Throws<ArgumentException>();
        await Assert.That(hash.VerifyHashAndReset(expected)).IsTrue();
    }

    [Test]
    public async Task IncrementalHash_VerifyHashAndReset_WrongLength()
    {
        var mac = ExpectedMac("SHA256");

        using var hmac = CreateIncrementalHmac();
        hmac.AppendData(message);
        await Assert.That(() => hmac.VerifyHashAndReset(new byte[31])).Throws<ArgumentException>();
        await Assert.That(() => hmac.VerifyHashAndReset((byte[]) null!)).Throws<ArgumentNullException>();

        // a rejected hash does not reset the accumulated data
        await Assert.That(hmac.VerifyHashAndReset(mac)).IsTrue();
    }

#if FeatureMemory

    [Test]
    public async Task IncrementalHash_VerifyHashAndReset_Span()
    {
        var mac = ExpectedMac("SHA256");

        using var hmac = CreateIncrementalHmac();
        hmac.AppendData(message);
        var valid = hmac.VerifyHashAndReset(new ReadOnlySpan<byte>(mac));
        hmac.AppendData(message);
        var tampered = hmac.VerifyHashAndReset(new ReadOnlySpan<byte>(Tampered(mac)));

        await Assert.That(valid).IsTrue();
        await Assert.That(tampered).IsFalse();
        await Assert.That(() => hmac.VerifyHashAndReset(new ReadOnlySpan<byte>(new byte[33]))).Throws<ArgumentException>();
    }

#endif

#endif

#if NET5_0_OR_GREATER

    [Test]
    public async Task IncrementalHash_VerifyCurrentHash()
    {
        var mac = ExpectedMac("SHA256");

        using var hmac = CreateIncrementalHmac();
        hmac.AppendData(message);
        await Assert.That(hmac.VerifyCurrentHash(mac)).IsTrue();

        // the state is not reset, so the same data verifies again
        await Assert.That(hmac.VerifyCurrentHash(new ReadOnlySpan<byte>(mac))).IsTrue();
        await Assert.That(hmac.VerifyCurrentHash(Tampered(mac))).IsFalse();
        await Assert.That(() => hmac.VerifyCurrentHash(new byte[31])).Throws<ArgumentException>();
        await Assert.That(() => hmac.VerifyCurrentHash((byte[]) null!)).Throws<ArgumentNullException>();

        hmac.AppendData(message);
        await Assert.That(hmac.VerifyCurrentHash(mac)).IsFalse();
    }

#endif

#if NET9_0_OR_GREATER

    static readonly byte[] kmacKey = [.. Enumerable.Range(0, 32).Select(_ => (byte) _)];

    [Test]
    public async Task Kmac128_Verify()
    {
        if (!Kmac128.IsSupported)
        {
            return;
        }

        using var kmac = new Kmac128(kmacKey);
        kmac.AppendData(message);
        var expected = kmac.GetCurrentHash(32);
        var shorter = kmac.GetCurrentHash(16);

        await Assert.That(kmac.VerifyCurrentHash(expected)).IsTrue();
        await Assert.That(kmac.VerifyCurrentHash(new ReadOnlySpan<byte>(shorter))).IsTrue();
        await Assert.That(kmac.VerifyCurrentHash(Tampered(expected))).IsFalse();
        await Assert.That(() => kmac.VerifyCurrentHash(Array.Empty<byte>())).Throws<ArgumentException>();
        await Assert.That(() => kmac.VerifyCurrentHash((byte[]) null!)).Throws<ArgumentNullException>();
        await Assert.That(kmac.VerifyHashAndReset(expected)).IsTrue();
        await Assert.That(kmac.VerifyHashAndReset(new ReadOnlySpan<byte>(expected))).IsFalse();
        await Assert.That(() => kmac.VerifyHashAndReset(Array.Empty<byte>())).Throws<ArgumentException>();
    }

    [Test]
    public async Task Kmac256_Verify()
    {
        if (!Kmac256.IsSupported)
        {
            return;
        }

        using var kmac = new Kmac256(kmacKey);
        kmac.AppendData(message);
        var expected = kmac.GetCurrentHash(64);

        await Assert.That(kmac.VerifyCurrentHash(expected)).IsTrue();
        await Assert.That(kmac.VerifyCurrentHash(new ReadOnlySpan<byte>(Tampered(expected)))).IsFalse();
        await Assert.That(() => kmac.VerifyCurrentHash(ReadOnlySpan<byte>.Empty)).Throws<ArgumentException>();
        await Assert.That(kmac.VerifyHashAndReset(new ReadOnlySpan<byte>(expected))).IsTrue();
        await Assert.That(kmac.VerifyHashAndReset(expected)).IsFalse();
        await Assert.That(() => kmac.VerifyHashAndReset((byte[]) null!)).Throws<ArgumentNullException>();
    }

    [Test]
    public async Task KmacXof128_Verify()
    {
        if (!KmacXof128.IsSupported)
        {
            return;
        }

        using var kmac = new KmacXof128(kmacKey);
        kmac.AppendData(message);
        var expected = kmac.GetCurrentHash(48);

        await Assert.That(kmac.VerifyCurrentHash(expected)).IsTrue();
        await Assert.That(kmac.VerifyCurrentHash(new ReadOnlySpan<byte>(Tampered(expected)))).IsFalse();
        await Assert.That(() => kmac.VerifyCurrentHash(Array.Empty<byte>())).Throws<ArgumentException>();
        await Assert.That(kmac.VerifyHashAndReset(expected)).IsTrue();
        await Assert.That(kmac.VerifyHashAndReset(new ReadOnlySpan<byte>(expected))).IsFalse();
        await Assert.That(() => kmac.VerifyHashAndReset((byte[]) null!)).Throws<ArgumentNullException>();
    }

    [Test]
    public async Task KmacXof256_Verify()
    {
        if (!KmacXof256.IsSupported)
        {
            return;
        }

        using var kmac = new KmacXof256(kmacKey);
        kmac.AppendData(message);
        var expected = kmac.GetCurrentHash(48);

        await Assert.That(kmac.VerifyCurrentHash(new ReadOnlySpan<byte>(expected))).IsTrue();
        await Assert.That(kmac.VerifyCurrentHash(Tampered(expected))).IsFalse();
        await Assert.That(() => kmac.VerifyCurrentHash((byte[]) null!)).Throws<ArgumentNullException>();
        await Assert.That(kmac.VerifyHashAndReset(new ReadOnlySpan<byte>(expected))).IsTrue();
        await Assert.That(kmac.VerifyHashAndReset(expected)).IsFalse();
        await Assert.That(() => kmac.VerifyHashAndReset(ReadOnlySpan<byte>.Empty)).Throws<ArgumentException>();
    }

#endif
}
