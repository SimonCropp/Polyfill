#if !NET

namespace System.Runtime.InteropServices;

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// An attribute used to indicate a GC transition should be skipped when making an unmanaged function call.
/// </summary>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
[AttributeUsage(
    validOn: AttributeTargets.Method,
    Inherited = false)]
//Link: https://learn.microsoft.com/en-us/dotnet/api/system.runtime.interopservices.suppressgctransitionattribute?view=net-11.0
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
#if PolyPublic
public
#endif
sealed class SuppressGCTransitionAttribute :
    Attribute;
#else
[assembly: System.Runtime.CompilerServices.TypeForwardedTo(typeof(System.Runtime.InteropServices.SuppressGCTransitionAttribute))]
#endif
