#if !NETCOREAPP3_0_OR_GREATER

namespace System.Runtime.CompilerServices;

using Diagnostics;
using Diagnostics.CodeAnalysis;

/// <summary>
/// Indicates that a parameter captures the expression passed for another parameter as a string.
/// </summary>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
[AttributeUsage(AttributeTargets.Parameter)]
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
//Link: https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.callerargumentexpressionattribute?view=net-11.0
#if PolyPublic
public
#endif
sealed class CallerArgumentExpressionAttribute :
    Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CallerArgumentExpressionAttribute"/> class.
    /// </summary>
    public CallerArgumentExpressionAttribute(string parameterName) =>
        ParameterName = parameterName;

    /// <summary>
    /// Gets the name of the parameter whose expression should be captured as a string.
    /// </summary>
    public string ParameterName { get; }
}

#else
[assembly: System.Runtime.CompilerServices.TypeForwardedTo(typeof(System.Runtime.CompilerServices.CallerArgumentExpressionAttribute))]
#endif
