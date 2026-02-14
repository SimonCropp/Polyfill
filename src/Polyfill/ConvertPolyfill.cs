#if !NET9_0_OR_GREATER

namespace Polyfills;

using System;

static partial class Polyfill
{
    extension(Convert)
    {
#if !NET

        /// <summary>
        /// Converts a subset of an array of 8-bit unsigned integers to its equivalent string representation that is encoded with uppercase hex characters.
        /// Parameters specify the subset as an offset in the input array and the number of elements in the array to convert.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.convert.tohexstring?view=net-11.0#system-convert-tohexstring(system-byte()-system-int32-system-int32)
        public static string ToHexString(byte[] inArray, int offset, int length) =>
            ToHexString(inArray, offset, length, "X2");
#endif

#if !NET9_0_OR_GREATER
        /// <summary>
        /// Converts a subset of an array of 8-bit unsigned integers to its equivalent string representation that is encoded with lowercase hex characters.
        /// Parameters specify the subset as an offset in the input array and the number of elements in the array to convert.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.convert.tohexstringlower?view=net-11.0#system-convert-tohexstringlower(system-byte()-system-int32-system-int32)
        public static string ToHexStringLower(byte[] inArray, int offset, int length) =>
            ToHexString(inArray, offset, length, "x2");

        /// <summary>
        /// Converts an array of 8-bit unsigned integers to its equivalent string representation that is encoded with lowercase hex characters.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.convert.tohexstringlower?view=net-11.0#system-convert-tohexstringlower(system-byte())
        public static string ToHexStringLower(byte[] inArray) =>
            Polyfill.ToHexStringLower(inArray, 0, inArray.Length);
#endif
#if !NET
        /// <summary>
        /// Converts an array of 8-bit unsigned integers to its equivalent string representation that is encoded with uppercase hex characters.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.convert.tohexstring?view=net-11.0#system-convert-tohexstring(system-byte())
        public static string ToHexString(byte[] inArray) =>
            Polyfill.ToHexString(inArray, 0, inArray.Length);

        /// <summary>
        /// Converts the specified string, which encodes binary data as hex characters, to an equivalent 8-bit unsigned integer array.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.convert.fromhexstring?view=net-11.0#system-convert-fromhexstring(system-string)
        public static byte[] FromHexString(string hexString)
        {
            if (hexString.Length % 2 != 0)
                throw new FormatException("Hex string must have an even length.");

            var result = new byte[hexString.Length / 2];

            for (var i = 0; i < result.Length; i++)
            {
                result[i] = (byte)((GetHexValue(hexString[i * 2]) << 4) + GetHexValue(hexString[i * 2 + 1]));
            }

            return result;

            static int GetHexValue(char hex) =>
                hex switch
                {
                    >= '0' and <= '9' => hex - '0',
                    >= 'A' and <= 'F' => hex - 'A' + 10,
                    >= 'a' and <= 'f' => hex - 'a' + 10,
                    _ => throw new FormatException($"Invalid hex character: {hex}")
                };
        }
#endif

#if FeatureMemory

#if !NET
        /// <summary>
        /// Converts the span, which encodes binary data as hex characters, to an equivalent 8-bit unsigned integer array.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.convert.fromhexstring?view=net-11.0#system-convert-fromhexstring(system-readonlyspan((system-char)))
        public static byte[] FromHexString(ReadOnlySpan<char> chars) =>
            Polyfill.FromHexString(chars.ToString());

        /// <summary>
        /// Converts a span of 8-bit unsigned integers to its equivalent string representation that is encoded with uppercase hex characters.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.convert.tohexstring?view=net-11.0#system-convert-tohexstring(system-readonlyspan((system-byte)))
        public static string ToHexString(ReadOnlySpan<byte> bytes) =>
            Polyfill.ToHexString(bytes.ToArray());
#endif

#if !NET9_0_OR_GREATER
        /// <summary>
        /// Converts a span of 8-bit unsigned integers to its equivalent string representation that is encoded with lowercase hex characters.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.convert.tohexstringlower?view=net-11.0#system-convert-tohexstringlower(system-readonlyspan((system-byte)))
        public static string ToHexStringLower(ReadOnlySpan<byte> bytes) =>
            Polyfill.ToHexStringLower(bytes.ToArray());

        /// <summary>
        /// Converts a span of 8-bit unsigned integers to its equivalent span representation that is encoded with uppercase hex characters.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.convert.trytohexstring?view=net-11.0
        public static bool TryToHexString(ReadOnlySpan<byte> source, Span<char> destination, out int charsWritten)
        {
            if (source.Length > destination.Length / 2)
            {
                charsWritten = 0;
                return false;
            }

            var hexString = Convert.ToHexString(source);
            hexString.CopyTo(destination);
            charsWritten = hexString.Length;
            return true;
        }

        /// <summary>
        /// Converts a span of 8-bit unsigned integers to its equivalent span representation that is encoded with lowercase hex characters.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.convert.trytohexstringlower?view=net-11.0
        public static bool TryToHexStringLower(ReadOnlySpan<byte> source, Span<char> destination, out int charsWritten)
        {
            if (source.Length > destination.Length / 2)
            {
                charsWritten = 0;
                return false;
            }

            var hexString = Convert.ToHexStringLower(source);
            hexString.CopyTo(destination);
            charsWritten = hexString.Length;
            return true;
        }
#endif

#endif

#if !NET || !NET9_0_OR_GREATER

        static string ToHexString(byte[] inArray, int offset, int length, string format)
        {
            if (length < 0)
                throw new ArgumentOutOfRangeException(nameof(length));
            if (offset < 0)
                throw new ArgumentOutOfRangeException(nameof(offset));
            if (offset > inArray.Length - length)
                throw new ArgumentOutOfRangeException(nameof(offset));

            var hexAlphabet = format == "x2" ? "0123456789abcdef" : "0123456789ABCDEF";
            var chars = new char[length * 2];

            var end = length + offset;
            var charIndex = 0;
            for (var i = offset; i < end; i++)
            {
                var b = inArray[i];
                chars[charIndex++] = hexAlphabet[b >> 4];
                chars[charIndex++] = hexAlphabet[b & 0xF];
            }

            return new string(chars);
        }

#endif
    }
}
#endif
