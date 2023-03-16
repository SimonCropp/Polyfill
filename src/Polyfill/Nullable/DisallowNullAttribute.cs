#if NETSTANDARD2_0 || NETFRAMEWORK || NETCOREAPP2_1 || NETCOREAPP2_0 || NETCOREAPP2_2

namespace System.Diagnostics.CodeAnalysis;

using Targets = AttributeTargets;

/// <summary>
///   Specifies that <see langword="null"/> is disallowed as an input even if the
///   corresponding type allows it.
/// </summary>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
[AttributeUsage(Targets.Field | Targets.Parameter | Targets.Property)]
sealed class DisallowNullAttribute : Attribute
{
    /// <summary>
    ///   Initializes a new instance of the <see cref="DisallowNullAttribute"/> class.
    /// </summary>
    public DisallowNullAttribute() { }
}

#endif