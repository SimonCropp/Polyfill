#if !NET7_0_OR_GREATER

#nullable enable
namespace System.Diagnostics.CodeAnalysis;

using Diagnostics;

/// <summary>
/// Indicates that the specified method parameter expects a constant.
/// </summary>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
//Link: https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.codeanalysis.constantexpectedattribute?view=net-11.0
#if PolyPublic
public
#endif
sealed class ConstantExpectedAttribute : Attribute
{
    /// <summary>
    /// Indicates the minimum bound of the expected constant, inclusive.
    /// </summary>
    public object? Min { get; set; }
    /// <summary>
    /// Indicates the maximum bound of the expected constant, inclusive.
    /// </summary>
    public object? Max { get; set; }
}
#else
[assembly: System.Runtime.CompilerServices.TypeForwardedTo(typeof(System.Diagnostics.CodeAnalysis.ConstantExpectedAttribute))]
#endif
