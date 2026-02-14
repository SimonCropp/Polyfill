#if FeatureMemory

namespace Polyfills;

using System;
using System.Runtime.InteropServices;
using System.Text;

static partial class Polyfill
{
#if !NETCOREAPP2_1_OR_GREATER && !NETSTANDARD2_1_OR_GREATER
    /// <summary>
    /// Calculates the number of characters produced by decoding the provided read-only byte span.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.encoding.getcharcount?view=net-11.0#system-text-encoding-getcharcount(system-readonlyspan((system-byte)))
#if AllowUnsafeBlocks
    public static unsafe int GetCharCount(this Encoding target, ReadOnlySpan<byte> bytes)
    {
        fixed (byte* bytesPtr = bytes)
        {
            return target.GetCharCount(bytesPtr, bytes.Length);
        }
    }
#else
    public static int GetCharCount(this Encoding target, ReadOnlySpan<byte> bytes) =>
        target.GetCharCount(bytes.ToArray());
#endif
#endif

}

#endif
