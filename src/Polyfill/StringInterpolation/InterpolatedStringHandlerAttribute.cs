#if !NET6_0_OR_GREATER

namespace System.Runtime.CompilerServices;

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Targets = AttributeTargets;

/// <summary>
/// Indicates the attributed type is to be used as an interpolated string handler.
/// </summary>
//Link: https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.interpolatedstringhandlerargumentattribute?view=net-11.0
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
[AttributeUsage(
    validOn: Targets.Class |
             Targets.Struct,
    Inherited = false)]
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
#if PolyPublic
public
#endif
sealed class InterpolatedStringHandlerAttribute :
    Attribute;

#else
[assembly: System.Runtime.CompilerServices.TypeForwardedTo(typeof(System.Runtime.CompilerServices.InterpolatedStringHandlerAttribute))]
#endif
