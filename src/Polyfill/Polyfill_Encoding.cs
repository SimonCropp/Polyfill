#if FeatureMemory

namespace Polyfills;

using System;
using System.Runtime.InteropServices;
using System.Text;

static partial class Polyfill
{
#if !NETCOREAPP2_1_OR_GREATER
    /// <summary>Encodes into a span of bytes a set of characters from the specified read-only span.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.encoding.getbytes?view=net-11.0#system-text-encoding-getbytes(system-readonlyspan((system-char))-system-span((system-byte)))
#if AllowUnsafeBlocks
    public static unsafe int GetBytes(this Encoding target, ReadOnlySpan<char> chars, Span<byte> bytes)
    {
        fixed (char* charsPtr = chars)
        fixed (byte* bytesPtr = bytes)
        {
            return target.GetBytes(charsPtr, chars.Length, bytesPtr, bytes.Length);
        }
    }
#else
    public static int GetBytes(this Encoding target, ReadOnlySpan<char> chars, Span<byte> bytes)
    {
        var result = target.GetBytes(chars.ToArray());
        result.CopyTo(bytes);
        return result.Length;
    }
#endif
#endif
#if !NETCOREAPP2_1_OR_GREATER
    /// <summary>When overridden in a derived class, decodes all the bytes in the specified byte span into a string.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.encoding.getstring?view=net-11.0#system-text-encoding-getstring(system-readonlyspan((system-byte)))
#if AllowUnsafeBlocks
    public static unsafe string GetString(this Encoding target, ReadOnlySpan<byte> bytes)
    {
        fixed (byte* bytesPtr = bytes)
        {
            return target.GetString(bytesPtr, bytes.Length);
        }
    }
#else
    public static string GetString(this Encoding target, ReadOnlySpan<byte> bytes) =>
        target.GetString(bytes.ToArray());
#endif
#endif
}

#endif
