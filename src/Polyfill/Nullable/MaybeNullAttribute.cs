#if(NETSTANDARD2_0 || NETFRAMEWORK || NETCOREAPP2_1 || NETCOREAPP2_0)

namespace System.Diagnostics.CodeAnalysis;

/// <summary>
///     Specifies that an output may be <see langword="null"/> even if the
///     corresponding type disallows it.
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.ReturnValue)]
sealed class MaybeNullAttribute : Attribute
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="MaybeNullAttribute"/> class.
    /// </summary>
    public MaybeNullAttribute() { }
}
#endif