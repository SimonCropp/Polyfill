#if !NET7_0_OR_GREATER

namespace System.Diagnostics.CodeAnalysis;

/// <summary>
/// Specifies that this constructor sets all required members for the current type, and callers
/// do not need to set any required members themselves.
/// </summary>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
[AttributeUsage(AttributeTargets.Constructor)]
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
#if PolyPublic
public
#endif
sealed class SetsRequiredMembersAttribute :
    Attribute;
#else
[assembly: System.Runtime.CompilerServices.TypeForwardedTo(typeof(System.Diagnostics.CodeAnalysis.SetsRequiredMembersAttribute))]
#endif
