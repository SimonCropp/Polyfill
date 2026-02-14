
#if !NET8_0_OR_GREATER

namespace Polyfills;

using System;
using System.Security.Cryptography;

static partial class Polyfill
{
    extension(RandomNumberGenerator)
    {
#if !NETSTANDARD2_1_OR_GREATER && !NETCOREAPP3_0_OR_GREATER
        /// <summary>
        ///Generates a random integer between a specified inclusive lower bound and a specified exclusive upper bound using a cryptographically strong random number generator.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.randomnumbergenerator.getint32?view=net-11.0#system-security-cryptography-randomnumbergenerator-getint32(system-int32-system-int32)
        public static int GetInt32(int fromInclusive, int toExclusive)
        {
            if (fromInclusive >= toExclusive)
            {
                throw new ArgumentException("toExclusive must be greater than fromInclusive.");
            }

            // The total possible range is [0, 4,294,967,295).
            // Subtract one to account for zero being an actual possibility.
            var range = (uint) toExclusive - (uint) fromInclusive - 1;

            // If there is only one possible choice, nothing random will actually happen, so return
            // the only possibility.
            if (range == 0)
            {
                return fromInclusive;
            }

            // Create a mask for the bits that we care about for the range. The other bits will be
            // masked away.
            var mask = range;
            mask |= mask >> 1;
            mask |= mask >> 2;
            mask |= mask >> 4;
            mask |= mask >> 8;
            mask |= mask >> 16;

            uint value;
            var bytes = new byte[4];

            using var generator = RandomNumberGenerator.Create();
            do
            {
                generator.GetBytes(bytes);
                value = BitConverter.ToUInt32(bytes, 0) & mask;
            } while (value >= range);

            return (int) (fromInclusive + value);
        }


        /// <summary>
        /// Generates a random integer between 0 (inclusive) and a specified exclusive upper bound using a cryptographically strong random number generator.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.randomnumbergenerator.getint32?view=net-11.0#system-security-cryptography-randomnumbergenerator-getint32(system-int32)
        public static int GetInt32(int toExclusive)
        {
            if (toExclusive <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(toExclusive), "Value must be positive and non-zero.");
            }

            return GetInt32(0, toExclusive);
        }
#endif

#if !NET6_0_OR_GREATER
        /// <summary>
        /// Creates an array of bytes with a cryptographically strong random sequence of values.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.randomnumbergenerator.getbytes?view=net-11.0#system-security-cryptography-randomnumbergenerator-getbytes(system-int32)
        public static byte[] GetBytes(int count)
        {
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), "Value is negative.");
            }

            using var generator = RandomNumberGenerator.Create();
            var bytes = new byte[count];
            generator.GetBytes(bytes);
            return bytes;
        }
#endif

#if FeatureMemory

#if !NETSTANDARD2_1_OR_GREATER && !NETCOREAPP2_1_OR_GREATER
        /// <summary>
        /// Fills a span with cryptographically strong random bytes.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.randomnumbergenerator.fill?view=net-11.0
        public static void Fill(Span<byte> data)
        {
            using var generator = RandomNumberGenerator.Create();
            var bytes = new byte[data.Length];
            generator.GetBytes(bytes);
            bytes.CopyTo(data);
        }
#endif

#if !NET8_0_OR_GREATER
        /// <summary>
        ///   Fills the elements of a specified span with items chosen at random from the provided set of choices.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.randomnumbergenerator.getitems?view=net-11.0#system-security-cryptography-randomnumbergenerator-getitems-1(system-readonlyspan((-0))-system-span((-0)))
        public static void GetItems<T>(ReadOnlySpan<T> choices, Span<T> destination)
        {
            if (choices.IsEmpty)
            {
                throw new ArgumentException("Empty span", nameof(choices));
            }

            for (var i = 0; i < destination.Length; i++)
            {
                destination[i] = choices[RandomNumberGenerator.GetInt32(choices.Length)];
            }
        }

        /// <summary>
        ///   Creates an array populated with items chosen at random from choices.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.randomnumbergenerator.getitems?view=net-11.0#system-security-cryptography-randomnumbergenerator-getitems-1(system-readonlyspan((-0))-system-int32)
        public static T[] GetItems<T>(ReadOnlySpan<T> choices, int length)
        {
            var result = new T[length];
            GetItems(choices, result);
            return result;
        }

        /// <summary>
        ///   Creates a string populated with characters chosen at random from choices.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.randomnumbergenerator.getstring?view=net-11.0
        public static string GetString(ReadOnlySpan<char> choices, int length)
        {
            var result = new char[length];
            GetItems(choices, result);
            return new(result);
        }

        /// <summary>
        ///   Fills a buffer with cryptographically random hexadecimal characters.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.randomnumbergenerator.getstring?view=net-11.0
        public static void GetHexString(Span<char> destination, bool lowercase = false)
        {
            if (destination.IsEmpty)
            {
                return;
            }

            // Each byte gives two hex chars
            var byteCount = (destination.Length + 1) / 2;
            Span<byte> bytes = stackalloc byte[byteCount];
            RandomNumberGenerator.Fill(bytes);

            ReadOnlySpan<char> hex = lowercase
                ? "0123456789abcdef".AsSpan()
                : "0123456789ABCDEF".AsSpan();

            var charIndex = 0;
            for (var i = 0; i < bytes.Length && charIndex < destination.Length; i++)
            {
                var b = bytes[i];
                destination[charIndex++] = hex[b >> 4];
                if (charIndex < destination.Length)
                {
                    destination[charIndex++] = hex[b & 0xF];
                }
            }
        }

        /// <summary>
        ///   Performs an in-place shuffle of a span using cryptographically random number generation.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.randomnumbergenerator.shuffle?view=net-11.0
        public static void Shuffle<T>(Span<T> values)
        {
            var n = values.Length;

            for (int i = 0; i < n - 1; i++)
            {
                var j = RandomNumberGenerator.GetInt32(i, n);

                if (i != j)
                {
                    var temp = values[i];
                    values[i] = values[j];
                    values[j] = temp;
                }
            }
        }
#endif
#endif

#if !NET8_0_OR_GREATER
        /// <summary>
        ///   Creates a string filled with cryptographically random hexadecimal characters.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.randomnumbergenerator.gethexstring?view=net-11.0#system-security-cryptography-randomnumbergenerator-gethexstring(system-int32-system-boolean)
        public static string GetHexString(int stringLength, bool lowercase = false)
        {
            if (stringLength == 0)
            {
                return string.Empty;
            }

            if (stringLength < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(stringLength), "Value is negative.");
            }

            var byteCount = (stringLength + 1) / 2;
            var bytes = RandomNumberGenerator.GetBytes(byteCount);

            var hexChars = new char[stringLength];
            var hexAlphabet = lowercase ? "0123456789abcdef" : "0123456789ABCDEF";
            var charIndex = 0;
            for (var i = 0; i < bytes.Length && charIndex < stringLength; i++)
            {
                var b = bytes[i];
                hexChars[charIndex++] = hexAlphabet[b >> 4];
                if (charIndex < stringLength)
                {
                    hexChars[charIndex++] = hexAlphabet[b & 0xF];
                }
            }

            return new(hexChars);
        }
#endif
    }
}

#endif
