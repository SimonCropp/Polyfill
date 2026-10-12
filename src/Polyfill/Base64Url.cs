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

        var length = EncodedLength(source.Length);

        // 256 bytes of stack and no more: a larger buffer that is filled and then turned into a string measures markedly slower
        if (length <= 128)
        {
            Span<char> buffer = stackalloc char[128];
            buffer = buffer.Slice(0, length);
            Encode(source, buffer);
            return buffer.ToString();
        }

        var rented = ArrayPool<char>.Shared.Rent(length);
        Encode(source, rented);
        var result = new string(rented, 0, length);
        ArrayPool<char>.Shared.Return(rented);
        return result;
    }

    /// <summary>Encodes the span of binary data into UTF-16 encoded text represented as base-64-url.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.text.base64url.encodetochars?view=net-11.0#system-buffers-text-base64url-encodetochars(system-readonlyspan((system-byte))-system-span((system-char)))
    public static int EncodeToChars(ReadOnlySpan<byte> source, Span<char> destination)
    {
        var length = EncodedLength(source.Length);

        if (length > destination.Length)
        {
            throw new ArgumentException("Destination is too small.", nameof(destination));
        }

        Encode(source, destination);
        return length;
    }

    /// <summary>Encodes the span of binary data into UTF-16 encoded text represented as base-64-url.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.text.base64url.encodetochars?view=net-11.0#system-buffers-text-base64url-encodetochars(system-readonlyspan((system-byte))-system-span((system-char))-system-int32@-system-int32@-system-boolean)
    public static OperationStatus EncodeToChars(ReadOnlySpan<byte> source, Span<char> destination, out int bytesConsumed, out int charsWritten, bool isFinalBlock = true)
    {
        var status = PlanEncode(source.Length, destination.Length, isFinalBlock, out bytesConsumed, out charsWritten);
        Encode(source.Slice(0, bytesConsumed), destination);
        return status;
    }

    /// <summary>Tries to encode the span of binary data into UTF-16 encoded text represented as base-64-url.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.text.base64url.tryencodetochars?view=net-11.0
    public static bool TryEncodeToChars(ReadOnlySpan<byte> source, Span<char> destination, out int charsWritten)
    {
        var length = EncodedLength(source.Length);

        if (length > destination.Length)
        {
            charsWritten = 0;
            return false;
        }

        Encode(source, destination);
        charsWritten = length;
        return true;
    }

    /// <summary>Encodes the span of binary data into UTF-8 encoded text represented as base-64-url.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.text.base64url.encodetoutf8?view=net-11.0#system-buffers-text-base64url-encodetoutf8(system-readonlyspan((system-byte)))
    public static byte[] EncodeToUtf8(ReadOnlySpan<byte> source)
    {
        var result = new byte[EncodedLength(source.Length)];
        Encode(source, result);
        return result;
    }

    /// <summary>Encodes the span of binary data into UTF-8 encoded text represented as base-64-url.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.text.base64url.encodetoutf8?view=net-11.0#system-buffers-text-base64url-encodetoutf8(system-readonlyspan((system-byte))-system-span((system-byte)))
    public static int EncodeToUtf8(ReadOnlySpan<byte> source, Span<byte> destination)
    {
        var length = EncodedLength(source.Length);

        if (length > destination.Length)
        {
            throw new ArgumentException("Destination is too small.", nameof(destination));
        }

        Encode(source, destination);
        return length;
    }

    /// <summary>Encodes the span of binary data into UTF-8 encoded text represented as base-64-url.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.text.base64url.encodetoutf8?view=net-11.0#system-buffers-text-base64url-encodetoutf8(system-readonlyspan((system-byte))-system-span((system-byte))-system-int32@-system-int32@-system-boolean)
    public static OperationStatus EncodeToUtf8(ReadOnlySpan<byte> source, Span<byte> destination, out int bytesConsumed, out int bytesWritten, bool isFinalBlock = true)
    {
        var status = PlanEncode(source.Length, destination.Length, isFinalBlock, out bytesConsumed, out bytesWritten);
        Encode(source.Slice(0, bytesConsumed), destination);
        return status;
    }

    /// <summary>Tries to encode the span of binary data into UTF-8 encoded text represented as base-64-url.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.text.base64url.tryencodetoutf8?view=net-11.0
    public static bool TryEncodeToUtf8(ReadOnlySpan<byte> source, Span<byte> destination, out int bytesWritten)
    {
        var length = EncodedLength(source.Length);

        if (length > destination.Length)
        {
            bytesWritten = 0;
            return false;
        }

        Encode(source, destination);
        bytesWritten = length;
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

        var encodedLength = EncodedLength(dataLength);

        if (encodedLength > buffer.Length)
        {
            bytesWritten = 0;
            return false;
        }

        // The output of a group never starts before its input, so encoding from the last group to
        // the first reads every byte before it is overwritten.
        var map = encodeMap;
        var groups = dataLength / 3;
        var sourceIndex = groups * 3;
        var destinationIndex = groups * 4;
        var remaining = dataLength - sourceIndex;

        if (remaining == 1)
        {
            var value = buffer[sourceIndex] << 16;
            buffer[destinationIndex] = (byte) map[value >> 18];
            buffer[destinationIndex + 1] = (byte) map[value >> 12 & 0x3F];
        }
        else if (remaining == 2)
        {
            var value = buffer[sourceIndex] << 16 | buffer[sourceIndex + 1] << 8;
            buffer[destinationIndex] = (byte) map[value >> 18];
            buffer[destinationIndex + 1] = (byte) map[value >> 12 & 0x3F];
            buffer[destinationIndex + 2] = (byte) map[value >> 6 & 0x3F];
        }

        while (sourceIndex > 0)
        {
            sourceIndex -= 3;
            destinationIndex -= 4;
            var value = buffer[sourceIndex] << 16 | buffer[sourceIndex + 1] << 8 | buffer[sourceIndex + 2];
            buffer[destinationIndex] = (byte) map[value >> 18];
            buffer[destinationIndex + 1] = (byte) map[value >> 12 & 0x3F];
            buffer[destinationIndex + 2] = (byte) map[value >> 6 & 0x3F];
            buffer[destinationIndex + 3] = (byte) map[value & 0x3F];
        }

        bytesWritten = encodedLength;
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
        var status = DecodeFromChars(chars, destination, out var charsConsumed, out bytesWritten, isFinalBlock);
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

    const string encodeMap = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_";

    static int EncodedLength(int bytesLength) =>
        checked((int) (((long) bytesLength * 4 + 2) / 3));

    // Works out how much of the source an encode with the given destination size consumes.
    static OperationStatus PlanEncode(int sourceLength, int destinationLength, bool isFinalBlock, out int bytesConsumed, out int written)
    {
        if (isFinalBlock)
        {
            var total = EncodedLength(sourceLength);

            if (total <= destinationLength)
            {
                bytesConsumed = sourceLength;
                written = total;
                return OperationStatus.Done;
            }
        }

        // only complete groups are written when the whole of the source cannot be
        var completeGroups = sourceLength / 3;
        var groups = Math.Min(completeGroups, destinationLength / 4);
        bytesConsumed = groups * 3;
        written = groups * 4;

        if (isFinalBlock || groups < completeGroups)
        {
            return OperationStatus.DestinationTooSmall;
        }

        if (sourceLength % 3 > 0)
        {
            return OperationStatus.NeedMoreData;
        }

        return OperationStatus.Done;
    }

#if NET8_0_OR_GREATER
    // From about this many bytes, the vectorized encoder of the BCL followed by two vectorized passes
    // to change the alphabet is faster than the loop. Below net8 there is no vectorized Replace, and
    // a loop to change the alphabet costs as much as encoding with the loop does.
    const int bulkThreshold = 192;

    // Encodes all of source. The destination must hold EncodedLength(source.Length) chars.
    static void Encode(ReadOnlySpan<byte> source, Span<char> destination)
    {
        if (source.Length >= bulkThreshold)
        {
            // Complete groups encode without padding, so they can go through the encoder of the BCL,
            // straight into the destination. Only '+' and '/' are then left to change.
            var bulk = source.Length / 3 * 3;
            Convert.TryToBase64Chars(source.Slice(0, bulk), destination, out var written);
            var encoded = destination.Slice(0, written);
            encoded.Replace('+', '-');
            encoded.Replace('/', '_');
            source = source.Slice(bulk);
            destination = destination.Slice(written);
        }

        EncodeLoop(source, destination);
    }

    static void EncodeLoop(ReadOnlySpan<byte> source, Span<char> destination)
#else
    // Encodes all of source. The destination must hold EncodedLength(source.Length) chars.
    static void Encode(ReadOnlySpan<byte> source, Span<char> destination)
#endif
    {
        var map = encodeMap;
        var sourceIndex = 0;
        var destinationIndex = 0;
        var end = source.Length - 2;

        while (sourceIndex < end)
        {
            var value = source[sourceIndex] << 16 | source[sourceIndex + 1] << 8 | source[sourceIndex + 2];
            destination[destinationIndex] = map[value >> 18];
            destination[destinationIndex + 1] = map[value >> 12 & 0x3F];
            destination[destinationIndex + 2] = map[value >> 6 & 0x3F];
            destination[destinationIndex + 3] = map[value & 0x3F];
            sourceIndex += 3;
            destinationIndex += 4;
        }

        var remaining = source.Length - sourceIndex;

        if (remaining == 1)
        {
            var value = source[sourceIndex] << 16;
            destination[destinationIndex] = map[value >> 18];
            destination[destinationIndex + 1] = map[value >> 12 & 0x3F];
        }
        else if (remaining == 2)
        {
            var value = source[sourceIndex] << 16 | source[sourceIndex + 1] << 8;
            destination[destinationIndex] = map[value >> 18];
            destination[destinationIndex + 1] = map[value >> 12 & 0x3F];
            destination[destinationIndex + 2] = map[value >> 6 & 0x3F];
        }
    }

#if NET8_0_OR_GREATER
    // Encodes all of source. The destination must hold EncodedLength(source.Length) bytes.
    static void Encode(ReadOnlySpan<byte> source, Span<byte> destination)
    {
        if (source.Length >= bulkThreshold)
        {
            var bulk = source.Length / 3 * 3;
            Base64.EncodeToUtf8(source.Slice(0, bulk), destination, out _, out var written);
            var encoded = destination.Slice(0, written);
            encoded.Replace((byte) '+', (byte) '-');
            encoded.Replace((byte) '/', (byte) '_');
            source = source.Slice(bulk);
            destination = destination.Slice(written);
        }

        EncodeLoop(source, destination);
    }

    static void EncodeLoop(ReadOnlySpan<byte> source, Span<byte> destination)
#else
    // Encodes all of source. The destination must hold EncodedLength(source.Length) bytes.
    static void Encode(ReadOnlySpan<byte> source, Span<byte> destination)
#endif
    {
        var map = encodeMap;
        var sourceIndex = 0;
        var destinationIndex = 0;
        var end = source.Length - 2;

        while (sourceIndex < end)
        {
            var value = source[sourceIndex] << 16 | source[sourceIndex + 1] << 8 | source[sourceIndex + 2];
            destination[destinationIndex] = (byte) map[value >> 18];
            destination[destinationIndex + 1] = (byte) map[value >> 12 & 0x3F];
            destination[destinationIndex + 2] = (byte) map[value >> 6 & 0x3F];
            destination[destinationIndex + 3] = (byte) map[value & 0x3F];
            sourceIndex += 3;
            destinationIndex += 4;
        }

        var remaining = source.Length - sourceIndex;

        if (remaining == 1)
        {
            var value = source[sourceIndex] << 16;
            destination[destinationIndex] = (byte) map[value >> 18];
            destination[destinationIndex + 1] = (byte) map[value >> 12 & 0x3F];
        }
        else if (remaining == 2)
        {
            var value = source[sourceIndex] << 16 | source[sourceIndex + 1] << 8;
            destination[destinationIndex] = (byte) map[value >> 18];
            destination[destinationIndex + 1] = (byte) map[value >> 12 & 0x3F];
            destination[destinationIndex + 2] = (byte) map[value >> 6 & 0x3F];
        }
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
