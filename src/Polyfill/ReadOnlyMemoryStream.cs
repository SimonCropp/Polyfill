#nullable enable

// === Polyfill target window =====================================================================
// System.IO.ReadOnlyMemoryStream was approved and merged into dotnet/runtime for net11
// (https://github.com/dotnet/runtime/pull/126669) but is NOT in the current net11 preview/RC SDK,
// so this polyfill is intentionally ACTIVE on net11 as well.
// When you upgrade to a net11 SDK that actually ships this type, the build will collide (CS0436)
// and the ApiBuilderTests "StreamWrapperBclDetectionTests" test FAILS with these exact steps.
// To retire this polyfill for net11:
//   1. change `#if FeatureMemory` below to `#if FeatureMemory && !NET11_0_OR_GREATER`
//   2. add at the bottom of this file (after the final #endif):
//        #if NET11_0_OR_GREATER
//        [assembly: System.Runtime.CompilerServices.TypeForwardedTo(typeof(System.IO.ReadOnlyMemoryStream))]
//        #endif
//   3. re-run ApiBuilderTests in Debug to regenerate Split + api_list.
// ================================================================================================
#if FeatureMemory

#pragma warning disable

namespace System.IO;

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

/// <summary>
/// Provides a seekable, read-only <see cref="MemoryStream"/> over a <see cref="ReadOnlyMemory{Byte}"/>.
/// </summary>
/// <remarks>
/// The stream cannot be written to. <see cref="MemoryStream.CanWrite"/> always returns <see langword="false"/>.
/// <see cref="MemoryStream.GetBuffer"/> throws and <see cref="MemoryStream.TryGetBuffer"/> returns <see langword="false"/>.
/// When the supplied memory is not backed by an array (for example native memory) the polyfill copies it;
/// the BCL always wraps the memory without copying.
/// </remarks>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
//Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.readonlymemorystream?view=net-11.0
#if PolyPublic
public
#endif
sealed class ReadOnlyMemoryStream :
    MemoryStream
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ReadOnlyMemoryStream"/> class over the specified <see cref="ReadOnlyMemory{Byte}"/>.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.readonlymemorystream.-ctor?view=net-11.0
    public ReadOnlyMemoryStream(ReadOnlyMemory<byte> source)
        : base(GetArray(source, out var offset, out var count), offset, count, writable: false, publiclyVisible: false)
    {
    }

    static byte[] GetArray(ReadOnlyMemory<byte> source, out int offset, out int count)
    {
        if (MemoryMarshal.TryGetArray(source, out var segment) &&
            segment.Array != null)
        {
            offset = segment.Offset;
            count = segment.Count;
            return segment.Array;
        }

        var array = source.ToArray();
        offset = 0;
        count = array.Length;
        return array;
    }
}

#endif
