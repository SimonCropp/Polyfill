#nullable enable

#if !NET11_0_OR_GREATER

namespace System.Diagnostics.CodeAnalysis;

// ReSharper disable RedundantNameQualifier
using System.Diagnostics;
// ReSharper restore RedundantNameQualifier

[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method,
    Inherited = false)]
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
//Link: https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.codeanalysis.requiresunsafeattribute?view=net-11.0
#if PolyPublic
public
#endif
sealed class RequiresUnsafeAttribute :
    Attribute;
#else
[assembly: System.Runtime.CompilerServices.TypeForwardedTo(typeof(System.Diagnostics.CodeAnalysis.RequiresUnsafeAttribute))]
#endif
