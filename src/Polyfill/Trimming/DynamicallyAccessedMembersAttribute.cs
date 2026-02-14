#if !NET

namespace System.Diagnostics.CodeAnalysis;

using Targets = AttributeTargets;

/// <summary>
/// Indicates that certain members on a specified <see cref="Type"/> are accessed dynamically,
/// for example through <see cref="System.Reflection"/>.
/// </summary>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
[AttributeUsage(
    validOn: Targets.Class |
             Targets.Field |
             Targets.GenericParameter |
             Targets.Interface |
             Targets.Method |
             Targets.Parameter |
             Targets.Property |
             Targets.ReturnValue |
             Targets.Struct,
    Inherited = false)]
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
//Link: https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.codeanalysis.dynamicallyaccessedmembersattribute?view=net-11.0
#if PolyPublic
public
#endif
sealed class DynamicallyAccessedMembersAttribute :
    Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DynamicallyAccessedMembersAttribute"/> class
    /// with the specified member types.
    /// </summary>
    public DynamicallyAccessedMembersAttribute(DynamicallyAccessedMemberTypes memberTypes) =>
        MemberTypes = memberTypes;

    /// <summary>
    /// Gets the <see cref="DynamicallyAccessedMemberTypes"/> which specifies the type
    /// of members dynamically accessed.
    /// </summary>
    public DynamicallyAccessedMemberTypes MemberTypes { get; }
}
#else
[assembly: System.Runtime.CompilerServices.TypeForwardedTo(typeof(System.Diagnostics.CodeAnalysis.DynamicallyAccessedMembersAttribute))]
#endif
