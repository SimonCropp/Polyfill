
#pragma warning disable


namespace System.Diagnostics.CodeAnalysis;

/// <summary>
///   Specifies that a method that will never return under any circumstance.
/// </summary>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
[AttributeUsage(
    validOn: AttributeTargets.Method,
    Inherited = false)]
sealed class DoesNotReturnAttribute :
    Attribute;
