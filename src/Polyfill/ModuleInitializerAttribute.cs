#if !NET

namespace System.Runtime.CompilerServices;

// ReSharper disable RedundantNameQualifier
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
// ReSharper restore RedundantNameQualifier

/// <summary>
/// Used to indicate to the compiler that a method should be called
/// in its containing module's initializer.
/// </summary>
//Link: https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.moduleinitializerattribute?view=net-11.0
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
[AttributeUsage(
    validOn: AttributeTargets.Method,
    Inherited = false)]
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
#if PolyPublic
public
#endif
sealed class ModuleInitializerAttribute :
    Attribute;
#else
[assembly: System.Runtime.CompilerServices.TypeForwardedTo(typeof(System.Runtime.CompilerServices.ModuleInitializerAttribute))]
#endif
