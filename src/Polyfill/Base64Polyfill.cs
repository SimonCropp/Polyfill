#if FeatureMemory && !NET11_0_OR_GREATER

namespace Polyfills;

using System;
using System.Buffers;
using System.Buffers.Text;

static partial class Polyfill
{
    extension(Base64)
    {
        /// <summary>
        /// Returns the length (in chars) of the result if you were to encode binary data within a byte span of size <paramref name="bytesLength"/>.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.text.base64.getencodedlength?view=net-11.0
        public static int GetEncodedLength(int bytesLength)
        {
            if (bytesLength < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(bytesLength));
            }

            return (bytesLength + 2) / 3 * 4;
        }

        /// <summary>
        /// Returns the maximum length (in bytes) of the result if you were to decode base-64 encoded text of size <paramref name="base64Length"/>.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.text.base64.getmaxdecodedlength?view=net-11.0
        public static int GetMaxDecodedLength(int base64Length)
        {
            if (base64Length < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(base64Length));
            }

            return base64Length / 4 * 3;
        }

        /// <summary>
        /// Encodes the span of binary data into a string that is represented as base-64.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.text.base64.encodetostring?view=net-11.0
        public static string EncodeToString(ReadOnlySpan<byte> source) =>
            EncodeBase64(source);

        /// <summary>
        /// Encodes the span of binary data into UTF-16 encoded text represented as base-64.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.text.base64.encodetochars?view=net-11.0#system-buffers-text-base64-encodetochars(system-readonlyspan((system-byte))-system-span((system-char)))
        public static int EncodeToChars(ReadOnlySpan<byte> source, Span<char> destination)
        {
            if (!TryEncodeBase64(source, destination, out var charsWritten))
            {
                throw new ArgumentException("Destination is too small.", nameof(destination));
            }

            return charsWritten;
        }

        /// <summary>
        /// Encodes the span of binary data into UTF-16 encoded text represented as base-64.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.text.base64.encodetochars?view=net-11.0#system-buffers-text-base64-encodetochars(system-readonlyspan((system-byte)))
        public static char[] EncodeToChars(ReadOnlySpan<byte> source)
        {
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            var result = new char[(source.Length + 2) / 3 * 4];
            System.Convert.TryToBase64Chars(source, result, out _);
            return result;
#else
            return EncodeBase64(source).ToCharArray();
#endif
        }

        /// <summary>
        /// Encodes the span of binary data into UTF-16 encoded text represented as base-64.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.text.base64.encodetochars?view=net-11.0#system-buffers-text-base64-encodetochars(system-readonlyspan((system-byte))-system-span((system-char))-system-int32@-system-int32@-system-boolean)
        public static OperationStatus EncodeToChars(ReadOnlySpan<byte> source, Span<char> destination, out int bytesConsumed, out int charsWritten, bool isFinalBlock = true)
        {
            // No more than the encoded length of the source is ever written, so a larger buffer changes nothing
            var length = (int) Math.Min(destination.Length, ((long) source.Length + 2) / 3 * 4);

            if (length <= 256)
            {
                Span<byte> buffer = stackalloc byte[256];
                return EncodeViaUtf8(source, buffer.Slice(0, length), destination, out bytesConsumed, out charsWritten, isFinalBlock);
            }

            var rented = ArrayPool<byte>.Shared.Rent(length);
            var status = EncodeViaUtf8(source, rented.AsSpan(0, length), destination, out bytesConsumed, out charsWritten, isFinalBlock);
            ArrayPool<byte>.Shared.Return(rented);
            return status;
        }

        // Kept apart from the stackalloc: a method with both a loop and a stackalloc is not tiered up by the JIT
        static OperationStatus EncodeViaUtf8(ReadOnlySpan<byte> source, Span<byte> utf8, Span<char> destination, out int bytesConsumed, out int charsWritten, bool isFinalBlock)
        {
            var status = Base64.EncodeToUtf8(source, utf8, out bytesConsumed, out charsWritten, isFinalBlock);

            for (var i = 0; i < charsWritten; i++)
            {
                destination[i] = (char) utf8[i];
            }

            return status;
        }

        /// <summary>
        /// Tries to encode the span of binary data into UTF-16 encoded text represented as base-64.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.text.base64.tryencodetochars?view=net-11.0
        public static bool TryEncodeToChars(ReadOnlySpan<byte> source, Span<char> destination, out int charsWritten) =>
            TryEncodeBase64(source, destination, out charsWritten);

        /// <summary>
        /// Encodes the span of binary data into a byte array of UTF-8 encoded text represented as base-64.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.text.base64.encodetoutf8?view=net-11.0#system-buffers-text-base64-encodetoutf8(system-readonlyspan((system-byte)))
        public static byte[] EncodeToUtf8(ReadOnlySpan<byte> source)
        {
            var result = new byte[(source.Length + 2) / 3 * 4];
            Base64.EncodeToUtf8(source, result, out _, out _);
            return result;
        }

        /// <summary>
        /// Decodes the span of UTF-16 encoded text represented as base-64 into binary data.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.text.base64.decodefromchars?view=net-11.0#system-buffers-text-base64-decodefromchars(system-readonlyspan((system-char)))
        public static byte[] DecodeFromChars(ReadOnlySpan<char> source) =>
            DecodeBase64(source);

        /// <summary>
        /// Decodes the span of UTF-16 encoded text represented as base-64 into binary data.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.text.base64.decodefromchars?view=net-11.0#system-buffers-text-base64-decodefromchars(system-readonlyspan((system-char))-system-span((system-byte)))
        public static int DecodeFromChars(ReadOnlySpan<char> source, Span<byte> destination)
        {
            if (!TryDecodeBase64(source, destination, out var bytesWritten))
            {
                throw new ArgumentException("Destination is too small.", nameof(destination));
            }

            return bytesWritten;
        }

        /// <summary>
        /// Decodes the span of UTF-16 encoded text represented as base-64 into binary data.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.text.base64.decodefromchars?view=net-11.0#system-buffers-text-base64-decodefromchars(system-readonlyspan((system-char))-system-span((system-byte))-system-int32@-system-int32@-system-boolean)
        public static OperationStatus DecodeFromChars(ReadOnlySpan<char> source, Span<byte> destination, out int charsConsumed, out int bytesWritten, bool isFinalBlock = true)
        {
            if (source.Length <= 256)
            {
                Span<byte> buffer = stackalloc byte[256];
                return DecodeViaUtf8(source, buffer.Slice(0, source.Length), destination, out charsConsumed, out bytesWritten, isFinalBlock);
            }

            var rented = ArrayPool<byte>.Shared.Rent(source.Length);
            var status = DecodeViaUtf8(source, rented.AsSpan(0, source.Length), destination, out charsConsumed, out bytesWritten, isFinalBlock);
            ArrayPool<byte>.Shared.Return(rented);
            return status;
        }

        // Narrows the source into utf8, a buffer of the same length, and decodes that
        static OperationStatus DecodeViaUtf8(ReadOnlySpan<char> source, Span<byte> utf8, Span<byte> destination, out int charsConsumed, out int bytesWritten, bool isFinalBlock)
        {
            for (var i = 0; i < source.Length; i++)
            {
                var c = source[i];

                // narrowing a non-ASCII char could turn it into a valid base-64 byte
                if (c > 0x7F)
                {
                    utf8[i] = 0xFF;
                    continue;
                }

                utf8[i] = (byte) c;
            }

            return Base64.DecodeFromUtf8(utf8, destination, out charsConsumed, out bytesWritten, isFinalBlock);
        }

        /// <summary>
        /// Tries to decode the span of UTF-16 encoded text represented as base-64 into binary data.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.text.base64.trydecodefromchars?view=net-11.0
        public static bool TryDecodeFromChars(ReadOnlySpan<char> source, Span<byte> destination, out int bytesWritten) =>
            TryDecodeBase64(source, destination, out bytesWritten);

        /// <summary>
        /// Decodes the span of UTF-8 encoded text represented as base-64 into binary data.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.text.base64.decodefromutf8?view=net-11.0#system-buffers-text-base64-decodefromutf8(system-readonlyspan((system-byte)))
        public static byte[] DecodeFromUtf8(ReadOnlySpan<byte> source)
        {
            if (source.IsEmpty)
            {
                return Array.Empty<byte>();
            }

#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            if (source.Length <= 256)
            {
                Span<char> buffer = stackalloc char[256];
                buffer = buffer.Slice(0, source.Length);
                Widen(source, buffer);
                return DecodeBase64(buffer);
            }

            var rented = ArrayPool<char>.Shared.Rent(source.Length);
            var chars = rented.AsSpan(0, source.Length);
            Widen(source, chars);
            var result = DecodeBase64(chars);
            ArrayPool<char>.Shared.Return(rented);
            return result;
#else
            var chars = new char[source.Length];

            for (var i = 0; i < source.Length; i++)
            {
                chars[i] = (char) source[i];
            }

            return System.Convert.FromBase64CharArray(chars, 0, chars.Length);
#endif
        }

#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER

        static string EncodeBase64(ReadOnlySpan<byte> source) =>
            System.Convert.ToBase64String(source);

        static bool TryEncodeBase64(ReadOnlySpan<byte> source, Span<char> destination, out int charsWritten) =>
            System.Convert.TryToBase64Chars(source, destination, out charsWritten);

        static void Widen(ReadOnlySpan<byte> source, Span<char> destination)
        {
            for (var i = 0; i < source.Length; i++)
            {
                destination[i] = (char) source[i];
            }
        }

        static byte[] DecodeBase64(ReadOnlySpan<char> source)
        {
            var maxLength = source.Length / 4 * 3;

            if (maxLength <= 256)
            {
                Span<byte> buffer = stackalloc byte[256];
                return buffer.Slice(0, DecodeBase64(source, buffer)).ToArray();
            }

            var rented = ArrayPool<byte>.Shared.Rent(maxLength);
            var result = rented.AsSpan(0, DecodeBase64(source, rented)).ToArray();
            ArrayPool<byte>.Shared.Return(rented);
            return result;
        }

        // Returns false when the destination is too small, and throws when the input is not valid.
        static bool TryDecodeBase64(ReadOnlySpan<char> source, Span<byte> destination, out int bytesWritten)
        {
            // Decode to a buffer that is always large enough, so that the destination is only written to on success
            var maxLength = source.Length / 4 * 3;
            byte[]? rented = null;
            Span<byte> buffer = stackalloc byte[256];

            if (maxLength > 256)
            {
                rented = ArrayPool<byte>.Shared.Rent(maxLength);
                buffer = rented;
            }

            var decoded = buffer.Slice(0, DecodeBase64(source, buffer));
            var fits = decoded.Length <= destination.Length;
            bytesWritten = 0;

            if (fits)
            {
                decoded.CopyTo(destination);
                bytesWritten = decoded.Length;
            }

            if (rented != null)
            {
                ArrayPool<byte>.Shared.Return(rented);
            }

            return fits;
        }

        // The destination must hold source.Length / 4 * 3 bytes, the most that valid input can decode to.
        static int DecodeBase64(ReadOnlySpan<char> source, Span<byte> destination)
        {
            if (System.Convert.TryFromBase64Chars(source, destination, out var bytesWritten))
            {
                return bytesWritten;
            }

            // The input is not valid, and the array overload throws the FormatException for that
            var decoded = System.Convert.FromBase64CharArray(source.ToArray(), 0, source.Length);
            decoded.CopyTo(destination);
            return decoded.Length;
        }

#else

        static string EncodeBase64(ReadOnlySpan<byte> source)
        {
            if (source.IsEmpty)
            {
                return string.Empty;
            }

            return System.Convert.ToBase64String(source.ToArray());
        }

        static bool TryEncodeBase64(ReadOnlySpan<byte> source, Span<char> destination, out int charsWritten)
        {
            var encoded = EncodeBase64(source);

            if (encoded.Length > destination.Length)
            {
                charsWritten = 0;
                return false;
            }

            encoded.AsSpan().CopyTo(destination);
            charsWritten = encoded.Length;
            return true;
        }

        static byte[] DecodeBase64(ReadOnlySpan<char> source)
        {
            if (source.IsEmpty)
            {
                return Array.Empty<byte>();
            }

            return System.Convert.FromBase64CharArray(source.ToArray(), 0, source.Length);
        }

        static bool TryDecodeBase64(ReadOnlySpan<char> source, Span<byte> destination, out int bytesWritten)
        {
            var decoded = DecodeBase64(source);

            if (decoded.Length > destination.Length)
            {
                bytesWritten = 0;
                return false;
            }

            decoded.CopyTo(destination);
            bytesWritten = decoded.Length;
            return true;
        }

#endif
    }
}

#endif
