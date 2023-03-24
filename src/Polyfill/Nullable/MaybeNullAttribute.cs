#if NETSTANDARD2_0 || NETFRAMEWORK || NETCOREAPP2X

#pragma warning disable

// ReSharper disable RedundantUsingDirective
// ReSharper disable UnusedMember.Global

namespace System.Diagnostics.CodeAnalysis;

using Targets = AttributeTargets;

/// <summary>
///   Specifies that an output may be <see langword="null"/> even if the
///   corresponding type disallows it.
/// </summary>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
[AttributeUsage(
    validOn: Targets.Field |
             Targets.Parameter |
             Targets.Property |
             Targets.ReturnValue)]
#if PolyPublic
public
#endif
sealed class MaybeNullAttribute :
    Attribute
{
}

#endif