#if !NET

namespace System.Runtime.CompilerServices;

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

using Targets = AttributeTargets;

/// <summary>
/// Used to indicate to the compiler that the <c>.locals init</c>
/// flag should not be set in method headers.
/// </summary>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
[AttributeUsage(
    validOn: Targets.Module |
             Targets.Class |
             Targets.Struct |
             Targets.Interface |
             Targets.Constructor |
             Targets.Method |
             Targets.Property |
             Targets.Event,
    Inherited = false)]
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
//Link: https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.skiplocalsinitattribute?view=net-11.0
#if PolyPublic
public
#endif
sealed class SkipLocalsInitAttribute :
    Attribute;
#else
[assembly: System.Runtime.CompilerServices.TypeForwardedTo(typeof(System.Runtime.CompilerServices.SkipLocalsInitAttribute))]
#endif
