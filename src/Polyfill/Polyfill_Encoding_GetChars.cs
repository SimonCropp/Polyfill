#if FeatureMemory

namespace Polyfills;

using System;
using System.Runtime.InteropServices;
using System.Text;

static partial class Polyfill
{
#if !NETCOREAPP2_1_OR_GREATER && !NETSTANDARD2_1_OR_GREATER
    /// <summary>
    /// Decodes all the bytes in the specified read-only byte span into a character span.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.encoding.getchars?view=net-11.0#system-text-encoding-getchars(system-readonlyspan((system-byte))-system-span((system-char)))
#if AllowUnsafeBlocks
    public static unsafe int GetChars(this Encoding target, ReadOnlySpan<byte> bytes, Span<char> chars)
    {
        fixed (byte* bytesPtr = bytes)
        fixed (char* charsPtr = chars)
        {
            return target.GetChars(bytesPtr, bytes.Length, charsPtr, chars.Length);
        }
    }
#else
    public static int GetChars(this Encoding target, ReadOnlySpan<byte> bytes, Span<char> chars)
    {
        var charArray = new char[bytes.Length];
        var array = bytes.ToArray();
        var count = target.GetChars(array, 0, bytes.Length, charArray, 0);
        new ReadOnlySpan<char>(charArray).CopyTo(chars);
        return count;
    }
#endif
#endif

}

#endif
