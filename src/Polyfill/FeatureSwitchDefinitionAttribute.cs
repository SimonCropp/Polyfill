#if !NET9_0_OR_GREATER

namespace System.Diagnostics.CodeAnalysis;

using Diagnostics;

/// <summary>
/// [AttributeUsage(AttributeTargets.Property, Inherited = false)]
/// </summary>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
[AttributeUsage(AttributeTargets.Property, Inherited = false)]
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
//Link: https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.codeanalysis.featureswitchdefinitionattribute?view=net-11.0
#if PolyPublic
public
#endif
sealed class FeatureSwitchDefinitionAttribute : Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FeatureSwitchDefinitionAttribute"/> class
    /// with the specified feature switch name.
    /// </summary>
    public FeatureSwitchDefinitionAttribute(string switchName) => SwitchName = switchName;

    /// <summary>
    /// The name of the feature switch that provides the value for the specified property.
    /// </summary>
    public string SwitchName { get; }
}
#else
[assembly: System.Runtime.CompilerServices.TypeForwardedTo(typeof(System.Diagnostics.CodeAnalysis.FeatureSwitchDefinitionAttribute))]
#endif
