
#if !NET8_0_OR_GREATER

namespace Polyfills;

using System;
#if FeatureMemory
using System.Buffers;
using System.Buffers.Binary;
#endif
using System.Security.Cryptography;

static partial class Polyfill
{
#if !NETSTANDARD2_1_OR_GREATER && !NETCOREAPP2_1_OR_GREATER
    // A RandomNumberGenerator is thread safe, so one instance serves every call
    static class SharedRandomNumberGenerator
    {
        internal static readonly RandomNumberGenerator Instance = RandomNumberGenerator.Create();

        [ThreadStatic]
        static byte[]? buffer;

        internal static uint NextUInt32()
        {
            var bytes = buffer ??= new byte[4];
            Instance.GetBytes(bytes);
            var value = BitConverter.ToUInt32(bytes, 0);
            Array.Clear(bytes, 0, bytes.Length);
            return value;
        }
    }
#endif

#if FeatureMemory
    // Masked rejection sampling, as RandomNumberGenerator.GetInt32 does, of a value from 0 to range inclusive.
    // The random bytes are taken four at a time from the buffer, which is refilled when used up.
    static uint RandomNumberGeneratorNext(uint range, Span<byte> buffer, ref int position)
    {
        var mask = range;
        mask |= mask >> 1;
        mask |= mask >> 2;
        mask |= mask >> 4;
        mask |= mask >> 8;
        mask |= mask >> 16;

        while (true)
        {
            if (position == buffer.Length)
            {
                RandomNumberGenerator.Fill(buffer);
                position = 0;
            }

            var value = BinaryPrimitives.ReadUInt32LittleEndian(buffer.Slice(position)) & mask;
            position += 4;

            if (value <= range)
            {
                return value;
            }
        }
    }

    // The size of the buffer to draw from for the given number of values. Most values are accepted first time.
    static int RandomNumberGeneratorBufferSize(int count)
    {
        if (count >= 64)
        {
            return 256;
        }

        return count * 4;
    }

    // The loops are kept apart from the stackalloc of the buffer: a method with both is not tiered up by the JIT
    static void RandomNumberGeneratorGetItems<T>(ReadOnlySpan<T> choices, Span<T> destination, Span<byte> buffer)
    {
        var range = (uint) choices.Length - 1;
        var position = buffer.Length;

        for (var i = 0; i < destination.Length; i++)
        {
            destination[i] = choices[(int) RandomNumberGeneratorNext(range, buffer, ref position)];
        }
    }

    static void RandomNumberGeneratorShuffle<T>(Span<T> values, Span<byte> buffer)
    {
        var n = values.Length;
        var position = buffer.Length;

        for (var i = 0; i < n - 1; i++)
        {
            var j = i + (int) RandomNumberGeneratorNext((uint) (n - i - 1), buffer, ref position);

            if (i != j)
            {
                var temp = values[i];
                values[i] = values[j];
                values[j] = temp;
            }
        }
    }
#endif

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
            var range = unchecked((uint) toExclusive - (uint) fromInclusive - 1);

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

#if NETCOREAPP2_1_OR_GREATER
            Span<byte> bytes = stackalloc byte[4];
            do
            {
                RandomNumberGenerator.Fill(bytes);
                value = BitConverter.ToUInt32(bytes) & mask;
            } while (value > range);
#else
            do
            {
                value = SharedRandomNumberGenerator.NextUInt32() & mask;
            } while (value > range);
#endif

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

            var bytes = new byte[count];
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            RandomNumberGenerator.Fill(bytes);
#else
            SharedRandomNumberGenerator.Instance.GetBytes(bytes);
#endif
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
            if (data.IsEmpty)
            {
                return;
            }

            // Fill through a pooled buffer, in chunks so that the buffer stays small. The whole of the buffer
            // is filled each time, since GetBytes(byte[], int, int) allocates an array of its own on .NET Framework.
            var rented = ArrayPool<byte>.Shared.Rent(Math.Min(data.Length, 4096));

            try
            {
                while (!data.IsEmpty)
                {
                    SharedRandomNumberGenerator.Instance.GetBytes(rented);
                    var count = Math.Min(data.Length, rented.Length);
                    rented.AsSpan(0, count).CopyTo(data);
                    data = data.Slice(count);
                }
            }
            finally
            {
                // Do not leave the random bytes behind in the pool
                ArrayPool<byte>.Shared.Return(rented, clearArray: true);
            }
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

            Span<byte> buffer = stackalloc byte[256];
            RandomNumberGeneratorGetItems(choices, destination, buffer.Slice(0, RandomNumberGeneratorBufferSize(destination.Length)));
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
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.randomnumbergenerator.getstring?view=net-11.0#system-security-cryptography-randomnumbergenerator-getstring(system-readonlyspan((system-char))-system-int32)
        public static string GetString(ReadOnlySpan<char> choices, int length)
        {
            if (length is >= 0 and <= 256)
            {
                Span<char> buffer = stackalloc char[256];
                buffer = buffer.Slice(0, length);
                GetItems(choices, buffer);
                return buffer.ToString();
            }

            var result = new char[length];
            GetItems(choices, result);
            return new(result);
        }

        /// <summary>
        ///   Fills a buffer with cryptographically random hexadecimal characters.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.randomnumbergenerator.gethexstring?view=net-11.0#system-security-cryptography-randomnumbergenerator-gethexstring(system-span((system-char))-system-boolean)
        public static void GetHexString(Span<char> destination, bool lowercase = false)
        {
            if (destination.IsEmpty)
            {
                return;
            }

            ReadOnlySpan<char> hex = lowercase
                ? "0123456789abcdef".AsSpan()
                : "0123456789ABCDEF".AsSpan();

            // Fill through a fixed size buffer so the stack use does not grow with the destination
            Span<byte> buffer = stackalloc byte[256];

            var charIndex = 0;
            while (charIndex < destination.Length)
            {
                // Each byte gives two hex chars
                var remaining = destination.Length - charIndex;
                var byteCount = Math.Min(buffer.Length, remaining / 2 + remaining % 2);
                var bytes = buffer.Slice(0, byteCount);
                RandomNumberGenerator.Fill(bytes);

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
        }

        /// <summary>
        ///   Performs an in-place shuffle of a span using cryptographically random number generation.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.randomnumbergenerator.shuffle?view=net-11.0
        public static void Shuffle<T>(Span<T> values)
        {
            Span<byte> buffer = stackalloc byte[256];
            RandomNumberGeneratorShuffle(values, buffer.Slice(0, RandomNumberGeneratorBufferSize(values.Length)));
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
