#if !NET6_0_OR_GREATER

// ReSharper disable RedundantUsingDirective
// ReSharper disable UnusedType.Global

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace System.Runtime.CompilerServices;

using Targets = AttributeTargets;

/// <summary>Indicates the attributed type is to be used as an interpolated string handler.</summary>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
[AttributeUsage(
    validOn: Targets.Class |
             Targets.Struct,
    Inherited = false)]
#if PolyPublic
public
#endif
sealed class InterpolatedStringHandlerAttribute :
    Attribute
{
}

#endif