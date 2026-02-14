#if FeatureMemory

namespace Polyfills;

using System;
using System.Runtime.InteropServices;
using System.Text;

static partial class Polyfill
{
#if !NETCOREAPP2_1_OR_GREATER && !NETSTANDARD2_1_OR_GREATER
    /// <summary>
    /// Calculates the number of bytes produced by encoding the characters in the specified character span.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.encoding.getbytecount?view=net-11.0#system-text-encoding-getbytecount(system-readonlyspan((system-char)))
#if AllowUnsafeBlocks
    public unsafe static int GetByteCount(this Encoding target, ReadOnlySpan<char> chars)
    {
        fixed (char* charsPtr = chars)
        {
            return target.GetByteCount(charsPtr, chars.Length);
        }
    }
#else
    public static int GetByteCount(this Encoding target, ReadOnlySpan<char> chars) =>
        target.GetByteCount(chars.ToArray());
#endif
#endif

}

#endif
