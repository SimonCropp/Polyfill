
#pragma warning disable


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
sealed class MaybeNullAttribute :
    Attribute;