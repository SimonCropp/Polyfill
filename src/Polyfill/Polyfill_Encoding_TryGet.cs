#if FeatureMemory

namespace Polyfills;

using System;
using System.Runtime.InteropServices;
using System.Text;

static partial class Polyfill
{
#if !NET8_0_OR_GREATER

    /// <summary>Decodes into a span of chars a set of bytes from the specified read-only span if the destination is large enough.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.encoding.trygetchars?view=net-11.0
    public static bool TryGetChars(this Encoding target, ReadOnlySpan<byte> bytes, Span<char> chars, out int charsWritten)
    {
        int required = target.GetCharCount(bytes);
        if (required <= chars.Length)
        {
            charsWritten = target.GetChars(bytes, chars);
            return true;
        }

        charsWritten = 0;
        return false;
    }

    /// <summary>Encodes into a span of bytes a set of characters from the specified read-only span if the destination is large enough.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.encoding.trygetbytes?view=net-11.0
    public static bool TryGetBytes(this Encoding target, ReadOnlySpan<char> chars, Span<byte> bytes, out int bytesWritten)
    {
        int required = target.GetByteCount(chars);
        if (required <= bytes.Length)
        {
            bytesWritten = target.GetBytes(chars, bytes);
            return true;
        }

        bytesWritten = 0;
        return false;
    }
#endif
}

#endif
