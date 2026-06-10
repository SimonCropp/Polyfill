#if FeatureMemory && !RefsBclMemory && !NET11_0_OR_GREATER

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
            var encoded = EncodeBase64(source);

            if (encoded.Length > destination.Length)
            {
                throw new ArgumentException("Destination is too small.", nameof(destination));
            }

            encoded.AsSpan().CopyTo(destination);
            return encoded.Length;
        }

        /// <summary>
        /// Encodes the span of binary data into UTF-16 encoded text represented as base-64.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.text.base64.encodetochars?view=net-11.0#system-buffers-text-base64-encodetochars(system-readonlyspan((system-byte)))
        public static char[] EncodeToChars(ReadOnlySpan<byte> source) =>
            EncodeBase64(source).ToCharArray();

        /// <summary>
        /// Encodes the span of binary data into UTF-16 encoded text represented as base-64.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.text.base64.encodetochars?view=net-11.0#system-buffers-text-base64-encodetochars(system-readonlyspan((system-byte))-system-span((system-char))-system-int32@-system-int32@-system-boolean)
        public static OperationStatus EncodeToChars(ReadOnlySpan<byte> source, Span<char> destination, out int bytesConsumed, out int charsWritten, bool isFinalBlock = true)
        {
            var utf8 = new byte[destination.Length];
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
        public static bool TryEncodeToChars(ReadOnlySpan<byte> source, Span<char> destination, out int charsWritten)
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

        /// <summary>
        /// Encodes the span of binary data into a byte array of UTF-8 encoded text represented as base-64.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.text.base64.encodetoutf8?view=net-11.0#system-buffers-text-base64-encodetoutf8(system-readonlyspan((system-byte)))
        public static byte[] EncodeToUtf8(ReadOnlySpan<byte> source)
        {
            var encoded = EncodeBase64(source);
            var result = new byte[encoded.Length];

            for (var i = 0; i < encoded.Length; i++)
            {
                result[i] = (byte) encoded[i];
            }

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
            var decoded = DecodeBase64(source);

            if (decoded.Length > destination.Length)
            {
                throw new ArgumentException("Destination is too small.", nameof(destination));
            }

            decoded.CopyTo(destination);
            return decoded.Length;
        }

        /// <summary>
        /// Decodes the span of UTF-16 encoded text represented as base-64 into binary data.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.text.base64.decodefromchars?view=net-11.0#system-buffers-text-base64-decodefromchars(system-readonlyspan((system-char))-system-span((system-byte))-system-int32@-system-int32@-system-boolean)
        public static OperationStatus DecodeFromChars(ReadOnlySpan<char> source, Span<byte> destination, out int charsConsumed, out int bytesWritten, bool isFinalBlock = true)
        {
            var utf8 = new byte[source.Length];

            for (var i = 0; i < source.Length; i++)
            {
                utf8[i] = (byte) source[i];
            }

            return Base64.DecodeFromUtf8(utf8, destination, out charsConsumed, out bytesWritten, isFinalBlock);
        }

        /// <summary>
        /// Tries to decode the span of UTF-16 encoded text represented as base-64 into binary data.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.text.base64.trydecodefromchars?view=net-11.0
        public static bool TryDecodeFromChars(ReadOnlySpan<char> source, Span<byte> destination, out int bytesWritten)
        {
            try
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
            catch (FormatException)
            {
                bytesWritten = 0;
                return false;
            }
        }

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

            var chars = new char[source.Length];

            for (var i = 0; i < source.Length; i++)
            {
                chars[i] = (char) source[i];
            }

            return Convert.FromBase64CharArray(chars, 0, chars.Length);
        }

        static string EncodeBase64(ReadOnlySpan<byte> source) =>
            source.IsEmpty ? string.Empty : Convert.ToBase64String(source.ToArray());

        static byte[] DecodeBase64(ReadOnlySpan<char> source) =>
            source.IsEmpty ? Array.Empty<byte>() : Convert.FromBase64CharArray(source.ToArray(), 0, source.Length);
    }
}

#endif
