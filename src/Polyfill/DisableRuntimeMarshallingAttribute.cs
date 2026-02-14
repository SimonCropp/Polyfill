#if !NET7_0_OR_GREATER

namespace System.Runtime.CompilerServices;

using Diagnostics;
using Diagnostics.CodeAnalysis;

/// <summary>
/// Disables the built-in runtime managed/unmanaged marshalling subsystem for
/// P/Invokes, Delegate types, and unmanaged function pointer invocations.
/// </summary>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
[AttributeUsage(AttributeTargets.Assembly)]
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
//Link: https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.disableruntimemarshallingattribute?view=net-11.0
#if PolyPublic
public
#endif
sealed class DisableRuntimeMarshallingAttribute :
    Attribute;
#else
[assembly: System.Runtime.CompilerServices.TypeForwardedTo(typeof(System.Runtime.CompilerServices.DisableRuntimeMarshallingAttribute))]
#endif
