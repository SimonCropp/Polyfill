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
    AttributeTargets.Method,
    Inherited = false)]
sealed class DoesNotReturnAttribute : Attribute
{
    /// <summary>
    ///   Initializes a new instance of the <see cref="DoesNotReturnAttribute"/> class.
    /// </summary>
    public DoesNotReturnAttribute() { }
}

#endif