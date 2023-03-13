#if(NETSTANDARD2_0 || NETFRAMEWORK || NETCOREAPP2_1 || NETCOREAPP2_0 || NETCOREAPP2_2)

namespace System.Diagnostics.CodeAnalysis;

using Targets = AttributeTargets;

/// <summary>
///   Specifies that an output is not <see langword="null"/> even if the
///   corresponding type allows it.
/// </summary>
[AttributeUsage(Targets.Field | Targets.Parameter | Targets.Property | Targets.ReturnValue)]
sealed class NotNullAttribute : Attribute
{
    /// <summary>
    ///   Initializes a new instance of the <see cref="NotNullAttribute"/> class.
    /// </summary>
    public NotNullAttribute() { }
}

#endif