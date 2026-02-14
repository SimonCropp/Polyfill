#if !NET9_0_OR_GREATER

namespace System.Runtime.CompilerServices;

using Diagnostics;
using Diagnostics.CodeAnalysis;

/// <summary>
/// Indicates that a method allows a variable number of arguments in its invocation.
/// </summary>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
[AttributeUsage(AttributeTargets.Parameter, Inherited = true, AllowMultiple = false)]
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
//Link: https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.paramcollectionattribute?view=net-11.0
#if PolyPublic
public
#endif
sealed class ParamCollectionAttribute : Attribute;
#else
[assembly: System.Runtime.CompilerServices.TypeForwardedTo(typeof(System.Runtime.CompilerServices.ParamCollectionAttribute))]
#endif
