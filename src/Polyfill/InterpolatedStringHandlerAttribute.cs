#if !NET6_0_OR_GREATER

// ReSharper disable RedundantUsingDirective
// ReSharper disable UnusedType.Global

using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;

namespace System.Runtime.CompilerServices;

/// <summary>Indicates the attributed type is to be used as an interpolated string handler.</summary>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
[AttributeUsage(
    AttributeTargets.Class | AttributeTargets.Struct,
    Inherited = false)]
sealed class InterpolatedStringHandlerAttribute : Attribute
{
}

#endif