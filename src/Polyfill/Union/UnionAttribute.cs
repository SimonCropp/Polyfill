#if !NET11_0_OR_GREATER

namespace System.Runtime.CompilerServices;

using Diagnostics;
using Diagnostics.CodeAnalysis;

/// <summary>
/// Indicates that a class or struct is a union type, enabling compiler support for union behaviors.
/// </summary>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
//Link: https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.unionattribute?view=net-11.0
#if PolyPublic
public
#endif
sealed class UnionAttribute : Attribute;

#else
[assembly: System.Runtime.CompilerServices.TypeForwardedTo(typeof(System.Runtime.CompilerServices.UnionAttribute))]
#endif
