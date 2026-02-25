#if !NET7_0_OR_GREATER

namespace System.Runtime.CompilerServices;

// ReSharper disable RedundantNameQualifier
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
// ReSharper restore RedundantNameQualifier

using Targets = AttributeTargets;

/// <summary>
/// Specifies that a type has required members or that a member is required.
/// </summary>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
[AttributeUsage(
    validOn: Targets.Class |
             Targets.Struct |
             Targets.Field |
             Targets.Property,
    Inherited = false)]
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
#if PolyPublic
public
#endif
sealed class RequiredMemberAttribute :
    Attribute;

#else
[assembly: System.Runtime.CompilerServices.TypeForwardedTo(typeof(System.Runtime.CompilerServices.RequiredMemberAttribute))]
#endif
