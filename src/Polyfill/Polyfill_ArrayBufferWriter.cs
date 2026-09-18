// ResetWrittenCount was added to the BCL ArrayBufferWriter<T> in net8.0, but the type itself
// exists from netcoreapp3.0/netstandard2.1. For those targets the type is present (so it cannot
// be recreated) and ResetWrittenCount is added as an extension method. For earlier targets it
// lives on the recreated type in ArrayBufferWriter.cs.
#if (NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER) && !NET8_0_OR_GREATER

namespace Polyfills;

using System.Buffers;

static partial class Polyfill
{
    /// <summary>
    /// Resets the data written to the underlying buffer without zeroing its contents.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.arraybufferwriter-1.resetwrittencount?view=net-11.0
    //Note: Delegates to Clear(), so the written region is zeroed: O(n) where the BCL is O(1), and the reclaimed space is zeroed instead of holding the stale data the BCL leaves behind.
    public static void ResetWrittenCount<T>(this ArrayBufferWriter<T> target) =>
        target.Clear();
}
#endif
