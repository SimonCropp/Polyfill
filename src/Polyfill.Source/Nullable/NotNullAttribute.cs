#if(NETSTANDARD2_0 || NETFRAMEWORK || NETCOREAPP2_1 || NETCOREAPP2_0)

namespace System.Diagnostics.CodeAnalysis;

/// <summary>
///     Specifies that an output is not <see langword="null"/> even if the
///     corresponding type allows it.
/// </summary>
[AttributeUsage(
    AttributeTargets.Field | AttributeTargets.Parameter |
    AttributeTargets.Property | AttributeTargets.ReturnValue,
    Inherited = false
)]
#if(PublicPolyfill)
public
#endif
sealed class NotNullAttribute : Attribute
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="NotNullAttribute"/> class.
    /// </summary>
    public NotNullAttribute() { }
}
#endif