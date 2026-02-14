#if !NET6_0_OR_GREATER

namespace System.Runtime.CompilerServices;

using Diagnostics;
using Diagnostics.CodeAnalysis;

/// <summary>
/// Indicates which arguments to a method involving an interpolated string handler should be passed to that handler.
/// </summary>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
[AttributeUsage(AttributeTargets.Parameter)]
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
//Link: https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.interpolatedstringhandlerargumentattribute?view=net-11.0
#if PolyPublic
public
#endif
sealed class InterpolatedStringHandlerArgumentAttribute :
    Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InterpolatedStringHandlerArgumentAttribute"/> class.
    /// </summary>
    public InterpolatedStringHandlerArgumentAttribute(string argument) => Arguments = [argument];

    /// <summary>
    /// Initializes a new instance of the <see cref="InterpolatedStringHandlerArgumentAttribute"/> class.
    /// </summary>
    public InterpolatedStringHandlerArgumentAttribute(params string[] arguments) => Arguments = arguments;

    /// <summary>Gets the names of the arguments that should be passed to the handler.</summary>
    public string[] Arguments { get; }
}

#else
[assembly: System.Runtime.CompilerServices.TypeForwardedTo(typeof(System.Runtime.CompilerServices.InterpolatedStringHandlerArgumentAttribute))]
#endif
