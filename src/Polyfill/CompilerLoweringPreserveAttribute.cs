#if !NET10_0_OR_GREATER

namespace System.Runtime.CompilerServices;

using Diagnostics;
using Diagnostics.CodeAnalysis;

/// <summary>
/// Indicates to the compiler that applications of an attribute should be preserved
/// through compiler lowering, flowing down to any compiler-generated symbols.
/// </summary>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
//Link: https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.compilerloweringpreserveattribute?view=net-11.0
#if PolyPublic
public
#endif
sealed class CompilerLoweringPreserveAttribute :
    Attribute;
#else
[assembly: System.Runtime.CompilerServices.TypeForwardedTo(typeof(System.Runtime.CompilerServices.CompilerLoweringPreserveAttribute))]
#endif
