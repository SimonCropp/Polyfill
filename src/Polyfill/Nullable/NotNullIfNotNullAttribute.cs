#if NETSTANDARD2_0 || NETFRAMEWORK || NETCOREAPP2_1 || NETCOREAPP2_0 || NETCOREAPP2_2

namespace System.Diagnostics.CodeAnalysis;

using Targets = AttributeTargets;

[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
[AttributeUsage(
    Targets.Parameter | Targets.Property | Targets.ReturnValue,
    AllowMultiple = true)]
sealed class NotNullIfNotNullAttribute : Attribute
{
    /// <summary>
    ///   Gets the associated parameter name.
    ///   The output will be non-<see langword="null"/> if the argument to the
    ///   parameter specified is non-<see langword="null"/>.
    /// </summary>
    public string ParameterName { get; }

    /// <summary>
    ///   Initializes the attribute with the associated parameter name.
    /// </summary>
    /// <param name="parameterName">
    ///   The associated parameter name.
    ///   The output will be non-<see langword="null"/> if the argument to the
    ///   parameter specified is non-<see langword="null"/>.
    /// </param>
    public NotNullIfNotNullAttribute(string parameterName) =>
        ParameterName = parameterName;
}
#endif
