#if(NETSTANDARD2_0 || NETFRAMEWORK || NETCOREAPP2_1 || NETCOREAPP2_0)

namespace System.Diagnostics.CodeAnalysis;

using Targets = AttributeTargets;

/// <summary>
///     Specifies that <see langword="null"/> is allowed as an input even if the
///     corresponding type disallows it.
/// </summary>
[AttributeUsage(Targets.Field | Targets.Parameter | Targets.Property)]
sealed class AllowNullAttribute : Attribute
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="AllowNullAttribute"/> class.
    /// </summary>
    public AllowNullAttribute() { }
}
#endif