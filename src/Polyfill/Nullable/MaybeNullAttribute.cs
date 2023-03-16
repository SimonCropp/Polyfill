#if NETSTANDARD2_0 || NETFRAMEWORK || NETCOREAPP2_1 || NETCOREAPP2_0 || NETCOREAPP2_2

namespace System.Diagnostics.CodeAnalysis;

using Targets = AttributeTargets;

/// <summary>
///   Specifies that an output may be <see langword="null"/> even if the
///   corresponding type disallows it.
/// </summary>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
[AttributeUsage(Targets.Field | Targets.Parameter | Targets.Property | Targets.ReturnValue)]
sealed class MaybeNullAttribute : Attribute
{
    /// <summary>
    ///   Initializes a new instance of the <see cref="MaybeNullAttribute"/> class.
    /// </summary>
    public MaybeNullAttribute() { }
}

#endif