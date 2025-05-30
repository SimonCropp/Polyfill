// <auto-generated />
#pragma warning disable

#if !NET7_0_OR_GREATER

namespace System.Runtime.CompilerServices;

using Diagnostics;
using Diagnostics.CodeAnalysis;

/// <summary>
/// Indicates that compiler support for a particular feature is required for the location where this attribute is applied.
/// </summary>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
[AttributeUsage(
    validOn: AttributeTargets.All,
    AllowMultiple = true,
    Inherited = false)]
//Link: https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.compilerfeaturerequiredattribute?view=net-10.0
#if PolyPublic
public
#endif
sealed class CompilerFeatureRequiredAttribute :
    Attribute
{
    /// <summary>
    /// Initialize a new instance of <see cref="CompilerFeatureRequiredAttribute"/>
    /// </summary>
    public CompilerFeatureRequiredAttribute(string featureName) =>
        FeatureName = featureName;

    /// <summary>
    /// The name of the compiler feature.
    /// </summary>
    public string FeatureName { get; }

    /// <summary>
    /// If true, the compiler can choose to allow access to the location where this attribute is applied if it does not understand <see cref="FeatureName"/>.
    /// </summary>
    public bool IsOptional { get; init; }

    /// <summary>
    /// The <see cref="FeatureName"/> used for the ref structs C# feature.
    /// </summary>
    public const string RefStructs = nameof(RefStructs);

    /// <summary>
    /// The <see cref="FeatureName"/> used for the required members C# feature.
    /// </summary>
    public const string RequiredMembers = nameof(RequiredMembers);
}

#else
using System.Runtime.CompilerServices;
[assembly: TypeForwardedTo(typeof(CompilerFeatureRequiredAttribute))]
#endif