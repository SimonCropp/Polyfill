#nullable enable

#if !NET6_0_OR_GREATER

namespace System.Runtime.Versioning;

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

[AttributeUsage(AttributeTargets.Assembly |
                AttributeTargets.Module |
                AttributeTargets.Class |
                AttributeTargets.Interface |
                AttributeTargets.Delegate |
                AttributeTargets.Struct |
                AttributeTargets.Enum |
                AttributeTargets.Constructor |
                AttributeTargets.Method |
                AttributeTargets.Property |
                AttributeTargets.Field |
                AttributeTargets.Event,
    Inherited = false)]
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
//Link: https://learn.microsoft.com/en-us/dotnet/api/system.runtime.versioning.requirespreviewfeaturesattribute?view=net-11.0
#if PolyPublic
public
#endif
sealed class RequiresPreviewFeaturesAttribute :
    Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RequiresPreviewFeaturesAttribute"/> class.
    /// </summary>
    public RequiresPreviewFeaturesAttribute()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RequiresPreviewFeaturesAttribute"/> class with the specified message.
    /// </summary>
    public RequiresPreviewFeaturesAttribute(string? message) =>
        Message = message;

    /// <summary>
    /// Returns the optional message associated with this attribute instance.
    /// </summary>
    public string? Message { get; }

    /// <summary>
    /// Returns the optional URL associated with this attribute instance.
    /// </summary>
    public string? Url { get; set; }
}
#else
[assembly: System.Runtime.CompilerServices.TypeForwardedTo(typeof(System.Runtime.Versioning.RequiresPreviewFeaturesAttribute))]
#endif
