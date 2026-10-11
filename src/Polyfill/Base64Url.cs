#if FeatureMemory && !RefsBclMemory

#if !NET9_0_OR_GREATER

#pragma warning disable

namespace System.Buffers.Text;

using System.Buffers;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

/// <summary>Converts between binary data and UTF-8 or UTF-16 encoded text that is represented in base-64-url format.</summary>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
#if PolyPublic
public
#endif
static class Base64Url
{
    /// <summary>Returns the length (in characters) of the result if you were to encode binary data within a byte span of size <paramref name="bytesLength"/>.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.text.base64url.getencodedlength?view=net-11.0
    public static int GetEncodedLength(int bytesLength)
    {
        if (bytesLength < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(bytesLength));
        }

        return (bytesLength * 4 + 2) / 3;
    }

    /// <summary>Returns the maximum length (in bytes) of the result if you were to decode base-64-url encoded text within a char span of size <paramref name="base64Length"/>.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.text.base64url.getmaxdecodedlength?view=net-11.0
    public static int GetMaxDecodedLength(int base64Length)
    {
        if (base64Length < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(base64Length));
        }

        return base64Length * 3 / 4;
    }

    /// <summary>Encodes the span of binary data into a string that is represented in base-64-url format.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.text.base64url.encodetostring?view=net-11.0
    public static string EncodeToString(ReadOnlySpan<byte> source)
    {
        if (source.IsEmpty)
        {
            return string.Empty;
        }

        var base64 = Convert.ToBase64String(source.ToArray());
        return ToBase64UrlString(base64);
    }

    /// <summary>Encodes the span of binary data into UTF-16 encoded text represented as base-64-url.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.text.base64url.encodetochars?view=net-11.0#system-buffers-text-base64url-encodetochars(system-readonlyspan((system-byte))-system-span((system-char)))
    public static int EncodeToChars(ReadOnlySpan<byte> source, Span<char> destination)
    {
        var encoded = EncodeToString(source);

        if (encoded.Length > destination.Length)
        {
            throw new ArgumentException("Destination is too small.", nameof(destination));
        }

        encoded.AsSpan().CopyTo(destination);
        return encoded.Length;
    }

    /// <summary>Encodes the span of binary data into UTF-16 encoded text represented as base-64-url.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.text.base64url.encodetochars?view=net-11.0#system-buffers-text-base64url-encodetochars(system-readonlyspan((system-byte))-system-span((system-char))-system-int32@-system-int32@-system-boolean)
    public static OperationStatus EncodeToChars(ReadOnlySpan<byte> source, Span<char> destination, out int bytesConsumed, out int charsWritten, bool isFinalBlock = true)
    {
        if (source.IsEmpty)
        {
            charsWritten = 0;
            bytesConsumed = 0;
            return OperationStatus.Done;
        }

        var completeGroups = source.Length / 3;
        var remainingBytes = source.Length % 3;

        if (!isFinalBlock)
        {
            var groupsWeCanFit = Math.Min(completeGroups, destination.Length / 4);
            bytesConsumed = groupsWeCanFit * 3;
            charsWritten = groupsWeCanFit * 4;

            if (bytesConsumed > 0)
            {
                EncodeToString(source.Slice(0, bytesConsumed)).AsSpan().CopyTo(destination);
            }

            if (groupsWeCanFit < completeGroups)
            {
                return OperationStatus.DestinationTooSmall;
            }

            return remainingBytes > 0 ? OperationStatus.NeedMoreData : OperationStatus.Done;
        }

        var totalChars = GetEncodedLength(source.Length);

        if (totalChars <= destination.Length)
        {
            EncodeToString(source).AsSpan().CopyTo(destination);
            charsWritten = totalChars;
            bytesConsumed = source.Length;
            return OperationStatus.Done;
        }

        var fitting = destination.Length / 4;
        bytesConsumed = fitting * 3;
        charsWritten = fitting * 4;

        if (bytesConsumed > 0)
        {
            EncodeToString(source.Slice(0, bytesConsumed)).AsSpan().CopyTo(destination);
        }

        return OperationStatus.DestinationTooSmall;
    }

    /// <summary>Tries to encode the span of binary data into UTF-16 encoded text represented as base-64-url.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.text.base64url.tryencodetochars?view=net-11.0
    public static bool TryEncodeToChars(ReadOnlySpan<byte> source, Span<char> destination, out int charsWritten)
    {
        var encoded = EncodeToString(source);

        if (encoded.Length > destination.Length)
        {
            charsWritten = 0;
            return false;
        }

        encoded.AsSpan().CopyTo(destination);
        charsWritten = encoded.Length;
        return true;
    }

    /// <summary>Encodes the span of binary data into UTF-8 encoded text represented as base-64-url.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.text.base64url.encodetoutf8?view=net-11.0#system-buffers-text-base64url-encodetoutf8(system-readonlyspan((system-byte)))
    public static byte[] EncodeToUtf8(ReadOnlySpan<byte> source)
    {
        var chars = EncodeToString(source);
        var result = new byte[chars.Length];

        for (var i = 0; i < chars.Length; i++)
        {
            result[i] = (byte)chars[i];
        }

        return result;
    }

    /// <summary>Encodes the span of binary data into UTF-8 encoded text represented as base-64-url.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.text.base64url.encodetoutf8?view=net-11.0#system-buffers-text-base64url-encodetoutf8(system-readonlyspan((system-byte))-system-span((system-byte)))
    public static int EncodeToUtf8(ReadOnlySpan<byte> source, Span<byte> destination)
    {
        var encoded = EncodeToString(source);

        if (encoded.Length > destination.Length)
        {
            throw new ArgumentException("Destination is too small.", nameof(destination));
        }

        WriteStringToBytes(encoded, destination);
        return encoded.Length;
    }

    /// <summary>Encodes the span of binary data into UTF-8 encoded text represented as base-64-url.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.text.base64url.encodetoutf8?view=net-11.0#system-buffers-text-base64url-encodetoutf8(system-readonlyspan((system-byte))-system-span((system-byte))-system-int32@-system-int32@-system-boolean)
    public static OperationStatus EncodeToUtf8(ReadOnlySpan<byte> source, Span<byte> destination, out int bytesConsumed, out int bytesWritten, bool isFinalBlock = true)
    {
        if (source.IsEmpty)
        {
            bytesWritten = 0;
            bytesConsumed = 0;
            return OperationStatus.Done;
        }

        var completeGroups = source.Length / 3;
        var remainingBytes = source.Length % 3;

        if (!isFinalBlock)
        {
            var groupsWeCanFit = Math.Min(completeGroups, destination.Length / 4);
            bytesConsumed = groupsWeCanFit * 3;
            bytesWritten = groupsWeCanFit * 4;

            if (bytesConsumed > 0)
            {
                WriteStringToBytes(EncodeToString(source.Slice(0, bytesConsumed)), destination);
            }

            if (groupsWeCanFit < completeGroups)
            {
                return OperationStatus.DestinationTooSmall;
            }

            return remainingBytes > 0 ? OperationStatus.NeedMoreData : OperationStatus.Done;
        }

        var totalBytes = GetEncodedLength(source.Length);

        if (totalBytes <= destination.Length)
        {
            WriteStringToBytes(EncodeToString(source), destination);
            bytesWritten = totalBytes;
            bytesConsumed = source.Length;
            return OperationStatus.Done;
        }

        var fitting = destination.Length / 4;
        bytesConsumed = fitting * 3;
        bytesWritten = fitting * 4;

        if (bytesConsumed > 0)
        {
            WriteStringToBytes(EncodeToString(source.Slice(0, bytesConsumed)), destination);
        }

        return OperationStatus.DestinationTooSmall;
    }

    /// <summary>Tries to encode the span of binary data into UTF-8 encoded text represented as base-64-url.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.text.base64url.tryencodetoutf8?view=net-11.0
    public static bool TryEncodeToUtf8(ReadOnlySpan<byte> source, Span<byte> destination, out int bytesWritten)
    {
        var encoded = EncodeToString(source);

        if (encoded.Length > destination.Length)
        {
            bytesWritten = 0;
            return false;
        }

        WriteStringToBytes(encoded, destination);
        bytesWritten = encoded.Length;
        return true;
    }

    /// <summary>Encodes the span of binary data (in-place) into UTF-8 encoded text represented as base-64-url.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.text.base64url.tryencodetoutf8inplace?view=net-11.0
    public static bool TryEncodeToUtf8InPlace(Span<byte> buffer, int dataLength, out int bytesWritten)
    {
        if (dataLength < 0 || dataLength > buffer.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(dataLength));
        }

        var encodedLength = GetEncodedLength(dataLength);

        if (encodedLength > buffer.Length)
        {
            bytesWritten = 0;
            return false;
        }

        var source = buffer.Slice(0, dataLength).ToArray();
        var encoded = EncodeToString(source);
        WriteStringToBytes(encoded, buffer);
        bytesWritten = encoded.Length;
        return true;
    }

    /// <summary>Decodes the span of UTF-16 encoded text represented as base-64-url into binary data.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.text.base64url.decodefromchars?view=net-11.0#system-buffers-text-base64url-decodefromchars(system-readonlyspan((system-char)))
    public static byte[] DecodeFromChars(ReadOnlySpan<char> source)
    {
        if (source.IsEmpty)
        {
            return Array.Empty<byte>();
        }

        var buffer = new byte[GetMaxDecodedLength(source.Length)];

        if (Decode(source, buffer, true, out _, out var bytesWritten, true) != OperationStatus.Done)
        {
            throw new FormatException("The input is not a valid Base64Url string.");
        }

        if (bytesWritten == buffer.Length)
        {
            return buffer;
        }

        return buffer.AsSpan(0, bytesWritten).ToArray();
    }

    /// <summary>Decodes the span of UTF-16 encoded text represented as base-64-url into binary data.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.text.base64url.decodefromchars?view=net-11.0#system-buffers-text-base64url-decodefromchars(system-readonlyspan((system-char))-system-span((system-byte)))
    public static int DecodeFromChars(ReadOnlySpan<char> source, Span<byte> destination)
    {
        var decoded = DecodeFromChars(source);

        if (decoded.Length > destination.Length)
        {
            throw new ArgumentException("Destination is too small.", nameof(destination));
        }

        decoded.CopyTo(destination);
        return decoded.Length;
    }

    /// <summary>Decodes the span of UTF-16 encoded text represented as base-64-url into binary data.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.text.base64url.decodefromchars?view=net-11.0#system-buffers-text-base64url-decodefromchars(system-readonlyspan((system-char))-system-span((system-byte))-system-int32@-system-int32@-system-boolean)
    public static OperationStatus DecodeFromChars(ReadOnlySpan<char> source, Span<byte> destination, out int charsConsumed, out int bytesWritten, bool isFinalBlock = true)
    {
        return Decode(source, destination, true, out charsConsumed, out bytesWritten, isFinalBlock);
    }

    /// <summary>Tries to decode the span of UTF-16 encoded text represented as base-64-url into binary data.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.text.base64url.trydecodefromchars?view=net-11.0
    public static bool TryDecodeFromChars(ReadOnlySpan<char> source, Span<byte> destination, out int bytesWritten)
    {
        var status = Decode(source, destination, true, out _, out bytesWritten, true);

        if (status == OperationStatus.Done)
        {
            return true;
        }

        if (status == OperationStatus.DestinationTooSmall)
        {
            return false;
        }

        throw new FormatException("The input is not a valid Base64Url string.");
    }

    /// <summary>Decodes the span of UTF-8 encoded text represented as base-64-url into binary data.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.text.base64url.decodefromutf8?view=net-11.0#system-buffers-text-base64url-decodefromutf8(system-readonlyspan((system-byte)))
    public static byte[] DecodeFromUtf8(ReadOnlySpan<byte> source)
    {
        if (source.IsEmpty)
        {
            return Array.Empty<byte>();
        }

        var chars = BytesToChars(source);
        return DecodeFromChars(chars);
    }

    /// <summary>Decodes the span of UTF-8 encoded text represented as base-64-url into binary data.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.text.base64url.decodefromutf8?view=net-11.0#system-buffers-text-base64url-decodefromutf8(system-readonlyspan((system-byte))-system-span((system-byte)))
    public static int DecodeFromUtf8(ReadOnlySpan<byte> source, Span<byte> destination)
    {
        var decoded = DecodeFromUtf8(source);

        if (decoded.Length > destination.Length)
        {
            throw new ArgumentException("Destination is too small.", nameof(destination));
        }

        decoded.CopyTo(destination);
        return decoded.Length;
    }

    /// <summary>Decodes the span of UTF-8 encoded text represented as base-64-url into binary data.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.text.base64url.decodefromutf8?view=net-11.0#system-buffers-text-base64url-decodefromutf8(system-readonlyspan((system-byte))-system-span((system-byte))-system-int32@-system-int32@-system-boolean)
    public static OperationStatus DecodeFromUtf8(ReadOnlySpan<byte> source, Span<byte> destination, out int bytesConsumed, out int bytesWritten, bool isFinalBlock = true)
    {
        var chars = BytesToChars(source);
        var status = DecodeFromChars(chars, destination, out bytesWritten, out var charsConsumed, isFinalBlock);
        bytesConsumed = charsConsumed;
        return status;
    }

    /// <summary>Tries to decode the span of UTF-8 encoded text represented as base-64-url into binary data.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.text.base64url.trydecodefromutf8?view=net-11.0
    public static bool TryDecodeFromUtf8(ReadOnlySpan<byte> source, Span<byte> destination, out int bytesWritten)
    {
        var chars = BytesToChars(source);
        return TryDecodeFromChars(chars, destination, out bytesWritten);
    }

    /// <summary>Decodes the span of UTF-8 encoded text in-place into binary data.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.text.base64url.decodefromutf8inplace?view=net-11.0
    public static int DecodeFromUtf8InPlace(Span<byte> buffer)
    {
        var chars = BytesToChars(buffer);
        var decoded = DecodeFromChars(chars);
        decoded.CopyTo(buffer);
        return decoded.Length;
    }

    /// <summary>Validates that the specified span of UTF-16 text is valid base-64-url.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.text.base64url.isvalid?view=net-11.0#system-buffers-text-base64url-isvalid(system-readonlyspan((system-char)))
    public static bool IsValid(ReadOnlySpan<char> base64UrlText) =>
        IsValid(base64UrlText, out _);

    /// <summary>Validates that the specified span of UTF-16 text is valid base-64-url, and outputs the decoded length.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.text.base64url.isvalid?view=net-11.0#system-buffers-text-base64url-isvalid(system-readonlyspan((system-char))-system-int32@)
    public static bool IsValid(ReadOnlySpan<char> base64UrlText, out int decodedLength)
    {
        if (Decode(base64UrlText, default, false, out _, out decodedLength, true) == OperationStatus.Done)
        {
            return true;
        }

        decodedLength = 0;
        return false;
    }

    /// <summary>Validates that the specified span of UTF-8 text is valid base-64-url.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.text.base64url.isvalid?view=net-11.0#system-buffers-text-base64url-isvalid(system-readonlyspan((system-byte)))
    public static bool IsValid(ReadOnlySpan<byte> utf8Base64UrlText) =>
        IsValid(utf8Base64UrlText, out _);

    /// <summary>Validates that the specified span of UTF-8 text is valid base-64-url, and outputs the decoded length.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.text.base64url.isvalid?view=net-11.0#system-buffers-text-base64url-isvalid(system-readonlyspan((system-byte))-system-int32@)
    public static bool IsValid(ReadOnlySpan<byte> utf8Base64UrlText, out int decodedLength)
    {
        var chars = BytesToChars(utf8Base64UrlText);
        return IsValid(chars, out decodedLength);
    }

    static string ToBase64UrlString(string base64)
    {
        var len = base64.Length;

        while (len > 0 && base64[len - 1] == '=')
        {
            len--;
        }

        var chars = new char[len];

        for (var i = 0; i < len; i++)
        {
            chars[i] = base64[i] switch
            {
                '+' => '-',
                '/' => '_',
                _ => base64[i]
            };
        }

        return new string(chars);
    }

    // Decodes block by block. Whitespace (space, tab, CR, LF) is skipped anywhere, and the final
    // quantum may be completed by '=' or '%' padding. When write is false the input is only validated
    // and bytesWritten is the decoded length.
    static OperationStatus Decode(ReadOnlySpan<char> source, Span<byte> destination, bool write, out int charsConsumed, out int bytesWritten, bool isFinalBlock)
    {
        charsConsumed = 0;
        bytesWritten = 0;
        Span<int> quantum = stackalloc int[4];
        var count = 0;
        var index = 0;

        for (; index < source.Length; index++)
        {
            var c = source[index];

            if (c is ' ' or '\t' or '\r' or '\n')
            {
                continue;
            }

            quantum[count] = DecodeValue(c);
            count++;

            if (count < 4)
            {
                continue;
            }

            if (quantum[0] < 0 || quantum[1] < 0 || quantum[2] < 0 || quantum[3] < 0)
            {
                // only the final quantum can be padded
                if (isFinalBlock)
                {
                    index++;
                    break;
                }

                return OperationStatus.InvalidData;
            }

            if (write)
            {
                if (destination.Length - bytesWritten < 3)
                {
                    return OperationStatus.DestinationTooSmall;
                }

                destination[bytesWritten] = (byte) (quantum[0] << 2 | quantum[1] >> 4);
                destination[bytesWritten + 1] = (byte) ((quantum[1] & 0xF) << 4 | quantum[2] >> 2);
                destination[bytesWritten + 2] = (byte) ((quantum[2] & 0x3) << 6 | quantum[3]);
            }

            bytesWritten += 3;
            charsConsumed = index + 1;
            count = 0;
        }

        if (!isFinalBlock)
        {
            if (count > 0)
            {
                return OperationStatus.NeedMoreData;
            }

            charsConsumed = source.Length;
            return OperationStatus.Done;
        }

        // nothing other than whitespace can follow the final quantum
        for (; index < source.Length; index++)
        {
            if (source[index] is not (' ' or '\t' or '\r' or '\n'))
            {
                return OperationStatus.InvalidData;
            }
        }

        if (count == 0)
        {
            charsConsumed = source.Length;
            return OperationStatus.Done;
        }

        var dataChars = 0;

        while (dataChars < count && quantum[dataChars] >= 0)
        {
            dataChars++;
        }

        if (dataChars < 2)
        {
            return OperationStatus.InvalidData;
        }

        for (var i = dataChars; i < count; i++)
        {
            if (quantum[i] != paddingValue)
            {
                return OperationStatus.InvalidData;
            }
        }

        var remaining = dataChars - 1;

        if (write && destination.Length - bytesWritten < remaining)
        {
            return OperationStatus.DestinationTooSmall;
        }

        // the bits not used by the final quantum must be zero
        if (dataChars == 2)
        {
            if ((quantum[1] & 0xF) != 0)
            {
                return OperationStatus.InvalidData;
            }
        }
        else if ((quantum[2] & 0x3) != 0)
        {
            return OperationStatus.InvalidData;
        }

        if (write)
        {
            destination[bytesWritten] = (byte) (quantum[0] << 2 | quantum[1] >> 4);

            if (dataChars == 3)
            {
                destination[bytesWritten + 1] = (byte) ((quantum[1] & 0xF) << 4 | quantum[2] >> 2);
            }
        }

        bytesWritten += remaining;
        charsConsumed = source.Length;
        return OperationStatus.Done;
    }

    const int paddingValue = -2;

    static int DecodeValue(char c)
    {
        if (c is >= 'A' and <= 'Z')
        {
            return c - 'A';
        }

        if (c is >= 'a' and <= 'z')
        {
            return c - 'a' + 26;
        }

        if (c is >= '0' and <= '9')
        {
            return c - '0' + 52;
        }

        if (c == '-')
        {
            return 62;
        }

        if (c == '_')
        {
            return 63;
        }

        if (c is '=' or '%')
        {
            return paddingValue;
        }

        return -1;
    }

    static void WriteStringToBytes(string source, Span<byte> destination)
    {
        for (var i = 0; i < source.Length; i++)
        {
            destination[i] = (byte)source[i];
        }
    }

    static char[] BytesToChars(ReadOnlySpan<byte> source)
    {
        var chars = new char[source.Length];

        for (var i = 0; i < source.Length; i++)
        {
            chars[i] = (char)source[i];
        }

        return chars;
    }
}

#else
[assembly: System.Runtime.CompilerServices.TypeForwardedTo(typeof(System.Buffers.Text.Base64Url))]
#endif

#endif
