#if !NET7_0_OR_GREATER

namespace System.Diagnostics.CodeAnalysis;

// ReSharper disable once RedundantNameQualifier
using System.Diagnostics;

using Targets = AttributeTargets;

/// <summary>
/// Used to indicate a byref escapes and is not scoped.
/// </summary>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
[AttributeUsage(
    validOn: Targets.Method |
             Targets.Property |
             Targets.Parameter,
    Inherited = false)]
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
#if PolyPublic
public
#endif
sealed class UnscopedRefAttribute :
    Attribute;
#else
[assembly: System.Runtime.CompilerServices.TypeForwardedTo(typeof(System.Diagnostics.CodeAnalysis.UnscopedRefAttribute))]
#endif