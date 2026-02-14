#if !NET

#nullable enable

namespace System.Diagnostics.CodeAnalysis;

/// <summary>
/// Suppresses reporting of a specific rule violation, allowing multiple suppressions on a
/// single code artifact.
/// </summary>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
[AttributeUsage(
    validOn: AttributeTargets.All,
    Inherited = false,
    AllowMultiple = true)]
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
//Link: https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.codeanalysis.unconditionalsuppressmessageattribute?view=net-11.0
#if PolyPublic
public
#endif
sealed class UnconditionalSuppressMessageAttribute :
    Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UnconditionalSuppressMessageAttribute"/>
    /// class, specifying the category of the tool and the identifier for an analysis rule.
    /// </summary>
    public UnconditionalSuppressMessageAttribute(string category, string checkId)
    {
        Category = category;
        CheckId = checkId;
    }

    /// <summary>
    /// Gets the category identifying the classification of the attribute.
    /// </summary>
    public string Category { get; }

    /// <summary>
    /// Gets the identifier of the analysis tool rule to be suppressed.
    /// </summary>
    public string CheckId { get; }

    /// <summary>
    /// Gets or sets the scope of the code that is relevant for the attribute.
    /// </summary>
    public string? Scope { get; set; }

    /// <summary>
    /// Gets or sets a fully qualified path that represents the target of the attribute.
    /// </summary>
    public string? Target { get; set; }

    /// <summary>
    /// Gets or sets an optional argument expanding on exclusion criteria.
    /// </summary>
    public string? MessageId { get; set; }

    /// <summary>
    /// Gets or sets the justification for suppressing the code analysis message.
    /// </summary>
    public string? Justification { get; set; }
}
#else
[assembly: System.Runtime.CompilerServices.TypeForwardedTo(typeof(System.Diagnostics.CodeAnalysis.UnconditionalSuppressMessageAttribute))]
#endif
