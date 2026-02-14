#nullable enable

#if !NET8_0_OR_GREATER

namespace System.Diagnostics.CodeAnalysis;

using Diagnostics;

/// <summary>
/// Indicates that a parameter captures the expression passed for another parameter as a string.
/// </summary>
/// <summary>
///  Indicates that an API is experimental and it may change in the future.
/// </summary>
[AttributeUsage(AttributeTargets.Assembly |
                AttributeTargets.Module |
                AttributeTargets.Class |
                AttributeTargets.Struct |
                AttributeTargets.Enum |
                AttributeTargets.Constructor |
                AttributeTargets.Method |
                AttributeTargets.Property |
                AttributeTargets.Field |
                AttributeTargets.Event |
                AttributeTargets.Interface |
                AttributeTargets.Delegate, Inherited = false)]
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
//Link: https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.codeanalysis.experimentalattribute?view=net-11.0
#if PolyPublic
public
#endif
sealed class ExperimentalAttribute : Attribute
{
    /// <summary>
    ///  Initializes a new instance of the <see cref="ExperimentalAttribute"/> class, specifying the ID that the compiler will use
    ///  when reporting a use of the API the attribute applies to.
    /// </summary>
    public ExperimentalAttribute(string diagnosticId) =>
        DiagnosticId = diagnosticId;

    /// <summary>
    ///  Gets the ID that the compiler will use when reporting a use of the API the attribute applies to.
    /// </summary>
    public string DiagnosticId { get; }

    /// <summary>
    ///  Gets or sets the URL for corresponding documentation.
    ///  The API accepts a format string instead of an actual URL, creating a generic URL that includes the diagnostic ID.
    /// </summary>
    public string? UrlFormat { get; set; }
}

#else
[assembly: System.Runtime.CompilerServices.TypeForwardedTo(typeof(System.Diagnostics.CodeAnalysis.ExperimentalAttribute))]
#endif
