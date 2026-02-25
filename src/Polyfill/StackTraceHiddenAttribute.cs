#if !NET6_0_OR_GREATER

namespace System.Diagnostics;

// ReSharper disable once RedundantNameQualifier
using System.Diagnostics.CodeAnalysis;
using Targets = AttributeTargets;

/// <summary>
/// Types and Methods attributed with StackTraceHidden will be omitted from the stack trace text shown in StackTrace.ToString() and Exception.StackTrace
/// </summary>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
[AttributeUsage(
    validOn: Targets.Class |
             Targets.Method |
             Targets.Constructor |
             Targets.Struct,
    Inherited = false)]
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
#if PolyPublic
public
#endif
sealed class StackTraceHiddenAttribute :
    Attribute;
#else
[assembly: System.Runtime.CompilerServices.TypeForwardedTo(typeof(System.Diagnostics.StackTraceHiddenAttribute))]
#endif
