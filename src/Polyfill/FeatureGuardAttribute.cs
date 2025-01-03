// <auto-generated />
#pragma warning disable

#if !NET9_0_OR_GREATER

namespace System.Diagnostics.CodeAnalysis;

using Diagnostics;
using Diagnostics.CodeAnalysis;
using Link = ComponentModel.DescriptionAttribute;


/// <summary>
/// Indicates that the specified public static boolean get-only property
/// guards access to the specified feature.
/// </summary>
/// <remarks>
/// Analyzers can use this to prevent warnings on calls to code that is
/// annotated as requiring that feature, when the callsite is guarded by a
/// call to the property.
/// </remarks>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
[AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
//Link: https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.codeanalysis.featureguardattribute
#if PolyPublic
public
#endif
sealed class FeatureGuardAttribute : Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FeatureGuardAttribute"/> class
    /// with the specified feature type.
    /// </summary>
    /// <param name="featureType">
    /// The type that represents the feature guarded by the property.
    /// </param>
    public FeatureGuardAttribute(Type featureType) =>
        FeatureType = featureType;

    /// <summary>
    /// The type that represents the feature guarded by the property.
    /// </summary>
    public Type FeatureType { get; }
}

#endif