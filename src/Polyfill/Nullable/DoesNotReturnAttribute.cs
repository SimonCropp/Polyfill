#if NETSTANDARD2_0 || NETFRAMEWORK || NETCOREAPP2X

// ReSharper disable RedundantUsingDirective
// ReSharper disable UnusedMember.Global

namespace System.Diagnostics.CodeAnalysis;

/// <summary>
///   Specifies that a method that will never return under any circumstance.
/// </summary>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
[AttributeUsage(
    validOn: AttributeTargets.Method,
    Inherited = false)]
#if PolyPublic
public
#endif
sealed class DoesNotReturnAttribute :
    Attribute
{
}

#endif