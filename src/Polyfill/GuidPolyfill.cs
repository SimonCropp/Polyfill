
#if !NET10_0_OR_GREATER

namespace Polyfills;

// ReSharper disable once RedundantUsingDirective
using System.Text;
using System;
using System.Security.Cryptography;

static partial class Polyfill
{
    extension(Guid)
    {
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

            var timeBytes = BitConverter.GetBytes(unixMilliseconds);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(timeBytes);
            }

#if NET8_0_OR_GREATER

            var uuidBytes = new byte[16];
            timeBytes[2..8].CopyTo(uuidBytes, 0);

            var randomBytes = uuidBytes.AsSpan().Slice(6);

            RandomNumberGenerator.Fill(randomBytes);

            uuidBytes[6] &= 0x0F;
            uuidBytes[6] += 0x70;

            return new(uuidBytes, true);

#else

            var randomBytes = new byte[10];

            using (var numberGenerator = RandomNumberGenerator.Create())
            {
                numberGenerator.GetBytes(randomBytes);
            }

            var uuidBytes = new byte[16];
            Array.Copy(timeBytes, 2, uuidBytes, 0, 6);
            Array.Copy(randomBytes, 0, uuidBytes, 6, 10);

            uuidBytes[6] = (byte) ((uuidBytes[6] & 0x0F) | 0x70);

            uuidBytes[8] = (byte) ((uuidBytes[8] & 0x3F) | 0x80);

            return new(uuidBytes);
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
