#if NETSTANDARD2_0 || NETFRAMEWORK || NETCOREAPP2X

// ReSharper disable RedundantUsingDirective
// ReSharper disable UnusedMember.Global

namespace System.Diagnostics.CodeAnalysis;

using Targets = AttributeTargets;

/// <summary>
///   Specifies that <see langword="null"/> is allowed as an input even if the
///   corresponding type disallows it.
/// </summary>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
[AttributeUsage(Targets.Field | Targets.Parameter | Targets.Property)]
sealed class AllowNullAttribute : Attribute
{
    /// <summary>
    ///   Initializes a new instance of the <see cref="AllowNullAttribute"/> class.
    /// </summary>
    public AllowNullAttribute() { }
}

#endif