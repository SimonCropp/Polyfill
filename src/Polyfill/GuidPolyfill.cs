
#if !NET10_0_OR_GREATER

namespace Polyfills;

// ReSharper disable once RedundantUsingDirective
using System.Text;
using System;
using System.Security.Cryptography;

static partial class Polyfill
{
    static readonly Guid AllBitsSetValue = new(-1, -1, -1, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF);

    extension(Guid)
    {
        /// <summary>A read-only instance of the <see cref="Guid" /> structure whose value is all ones.</summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.guid.allbitsset?view=net-11.0
        public static Guid AllBitsSet => AllBitsSetValue;

#if !NET7_0_OR_GREATER

        /// <summary>
        /// Tries to parse a string into a value.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.guid.tryparse?view=net-11.0#system-guid-tryparse(system-string-system-iformatprovider-system-guid@)
        public static bool TryParse(string? s, IFormatProvider? provider, out Guid result) =>
            Guid.TryParse(s, out result);

#endif

#if !NET9_0_OR_GREATER

        /// <summary>Creates a new <see cref="Guid" /> according to RFC 9562, following the Version 7 format.</summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.guid.createversion7?view=net-11.0#system-guid-createversion7
        public static Guid CreateVersion7() => CreateVersion7(DateTimeOffset.UtcNow);

        /// <summary>Creates a new <see cref="Guid" /> according to RFC 9562, following the Version 7 format.</summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.guid.createversion7?view=net-11.0#system-guid-createversion7(system-datetimeoffset)
        public static Guid CreateVersion7(DateTimeOffset timestamp)
        {
            var unixMilliseconds = timestamp.ToUnixTimeMilliseconds();

#if NET8_0_OR_GREATER

            var timeBytes = BitConverter.GetBytes(unixMilliseconds);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(timeBytes);
            }

            Span<byte> uuidBytes = stackalloc byte[16];
            timeBytes.AsSpan(2, 6).CopyTo(uuidBytes);

            var randomBytes = uuidBytes.Slice(6);

            RandomNumberGenerator.Fill(randomBytes);

            uuidBytes[6] &= 0x0F;
            uuidBytes[6] += 0x70;

            uuidBytes[8] &= 0x3F;
            uuidBytes[8] += 0x80;

            return new(uuidBytes, true);

#else

            // As the BCL does: take the random bits from a new (version 4) Guid, then overwrite the
            // timestamp, version and variant. ToByteArray lays the Guid out as a little-endian int
            // and two little-endian shorts followed by eight bytes.
            var random = Guid.NewGuid().ToByteArray();

            // unix_ts_ms: the low 48 bits of the timestamp, big-endian across the first two fields
            var a = unchecked((int) (unixMilliseconds >> 16));
            var b = unchecked((short) unixMilliseconds);

            // ver (0b0111) followed by the 12 random bits of rand_a
            var c = unchecked((short) (0x7000 | ((random[6] | (random[7] << 8)) & 0x0FFF)));

            // var (0b10) followed by the first 6 random bits of rand_b
            var d = (byte) ((random[8] & 0x3F) | 0x80);

            return new(a, b, c, d, random[9], random[10], random[11], random[12], random[13], random[14], random[15]);
#endif
        }

#endif

#if FeatureMemory

#if !NETCOREAPP2_1_OR_GREATER && !NETSTANDARD2_1_OR_GREATER

        /// <summary>
        /// Converts span of characters representing the GUID to the equivalent Guid structure, provided that the string is in the specified format.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.guid.tryparseexact?view=net-11.0#system-guid-tryparseexact(system-readonlyspan((system-char))-system-readonlyspan((system-char))-system-guid@)
        public static bool TryParseExact(ReadOnlySpan<char> input, ReadOnlySpan<char> format, out Guid result) =>
            Guid.TryParseExact(input.ToString(), format.ToString(), out result);

#endif

#if !NET7_0_OR_GREATER

        /// <summary>
        /// Tries to parse a span of UTF-8 characters into a value.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.guid.tryparse?view=net-11.0#system-guid-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-guid@)
        public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, out Guid result) =>
            Guid.TryParse(s.ToString(), out result);

#endif

#if !(NETSTANDARD2_1 || NETSTANDARD2_1_OR_GREATER || NET5_0_OR_GREATER)

        /// <summary>
        /// Tries to parse a span of UTF-8 characters into a value.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.guid.tryparse?view=net-11.0#system-guid-tryparse(system-readonlyspan((system-char))-system-guid@)
        public static bool TryParse(ReadOnlySpan<char> input, out Guid result) =>
            Guid.TryParse(input.ToString(), out result);

#endif
#if !NET10_0_OR_GREATER

        /// <summary>
        /// Tries to parse a span of UTF-8 bytes into a value.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.guid.tryparse?view=net-11.0#system-guid-tryparse(system-readonlyspan((system-byte))-system-guid@)
        public static bool TryParse(ReadOnlySpan<byte> utf8Text, out Guid result) =>
            Guid.TryParse(Encoding.UTF8.GetString(utf8Text), out result);

        /// <summary>
        /// Parse a span of UTF-8 bytes into a value.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.guid.parse?view=net-11.0#system-guid-parse(system-readonlyspan((system-byte)))
        public static Guid Parse(ReadOnlySpan<byte> utf8Text) =>
            Guid.Parse(Encoding.UTF8.GetString(utf8Text));

#endif

#endif
    }
}
#endif
