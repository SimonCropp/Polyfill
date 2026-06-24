#nullable enable

// === Polyfill target window =====================================================================
// System.IO.WritableMemoryStream was approved and merged into dotnet/runtime for net11
// (https://github.com/dotnet/runtime/pull/126669) but is NOT in the current net11 preview/RC SDK,
// so this polyfill is intentionally ACTIVE on net11 as well.
// When you upgrade to a net11 SDK that actually ships this type, the build will collide (CS0436)
// and the ApiBuilderTests "StreamWrapperBclDetectionTests" test FAILS with these exact steps.
// To retire this polyfill for net11:
//   1. change `#if FeatureMemory` below to `#if FeatureMemory && !NET11_0_OR_GREATER`
//   2. add at the bottom of this file (after the final #endif):
//        #if NET11_0_OR_GREATER
//        [assembly: System.Runtime.CompilerServices.TypeForwardedTo(typeof(System.IO.WritableMemoryStream))]
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
/// Provides a seekable, read-write <see cref="MemoryStream"/> over a <see cref="Memory{Byte}"/> with fixed capacity.
/// </summary>
/// <remarks>
/// The stream cannot expand beyond the initial memory capacity.
/// <see cref="MemoryStream.GetBuffer"/> throws and <see cref="MemoryStream.TryGetBuffer"/> returns <see langword="false"/>.
/// The polyfill requires an array-backed <see cref="Memory{Byte}"/>; memory backed by native (or other non-array)
/// buffers is not supported and the constructor throws <see cref="NotSupportedException"/>.
/// </remarks>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
//Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.writablememorystream?view=net-11.0
#if PolyPublic
public
#endif
sealed class WritableMemoryStream :
    MemoryStream
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WritableMemoryStream"/> class over the specified <see cref="Memory{Byte}"/>.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.writablememorystream.-ctor?view=net-11.0
    public WritableMemoryStream(Memory<byte> buffer)
        : base(GetArray(buffer, out var offset, out var count), offset, count, writable: true, publiclyVisible: false) =>
        // The base constructor reports the whole region as content; reset to an empty, ready-to-write stream.
        base.SetLength(0);

    /// <inheritdoc/>
    public override void SetLength(long value) =>
        throw new NotSupportedException("Memory stream is not expandable.");

    static byte[] GetArray(Memory<byte> buffer, out int offset, out int count)
    {
        if (MemoryMarshal.TryGetArray((ReadOnlyMemory<byte>)buffer, out var segment) &&
            segment.Array != null)
        {
            offset = segment.Offset;
            count = segment.Count;
            return segment.Array;
        }

        throw new NotSupportedException("WritableMemoryStream polyfill requires an array-backed Memory<byte>.");
    }
}

#endif
