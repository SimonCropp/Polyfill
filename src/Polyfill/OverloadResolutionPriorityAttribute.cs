#if !NET9_0_OR_GREATER

namespace System.Runtime.CompilerServices;

using Diagnostics;
using Diagnostics.CodeAnalysis;

/// <summary>
/// Specifies the priority of a member in overload resolution. When unspecified, the default priority is 0.
/// </summary>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
[AttributeUsage(
    AttributeTargets.Method |
    AttributeTargets.Constructor |
    AttributeTargets.Property,
    Inherited = false)]
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
//Link: https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.overloadresolutionpriorityattribute?view=net-11.0
#if PolyPublic
public
#endif
sealed class OverloadResolutionPriorityAttribute :
    Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OverloadResolutionPriorityAttribute"/> class.
    /// </summary>
    public OverloadResolutionPriorityAttribute(int priority) => Priority = priority;

    /// <summary>
    /// The priority of the member.
    /// </summary>
    public int Priority { get; }
}
#else
[assembly: System.Runtime.CompilerServices.TypeForwardedTo(typeof(System.Runtime.CompilerServices.OverloadResolutionPriorityAttribute))]
#endif
