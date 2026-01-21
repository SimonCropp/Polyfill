#if !NET7_0_OR_GREATER

#nullable enable

namespace System.Diagnostics.CodeAnalysis;

using Targets = AttributeTargets;

/// <summary>
/// Indicates that the specified method requires the ability to generate new code at runtime,
/// for example through <see cref="System.Reflection"/>.
/// </summary>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
[AttributeUsage(
    validOn: Targets.Method |
             Targets.Constructor |
             Targets.Class,
    Inherited = false)]
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
#if PolyPublic
public
#endif
sealed class RequiresDynamicCodeAttribute :
    Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RequiresDynamicCodeAttribute"/> class
    /// with the specified message.
    /// </summary>
    public RequiresDynamicCodeAttribute(string message) =>
        Message = message;

    /// <summary>
    /// When set to true, indicates that the annotation should not apply to static members.
    /// </summary>
    public bool ExcludeStatics { get; set; }

    /// <summary>
    /// Gets a message that contains information about the usage of dynamic code.
    /// </summary>
    public string Message { get; }

    /// <summary>
    /// Gets or sets an optional URL that contains more information about the method,
    /// why it requires dynamic code, and what options a consumer has to deal with it.
    /// </summary>
    public string? Url { get; set; }
}
#else
[assembly: System.Runtime.CompilerServices.TypeForwardedTo(typeof(System.Diagnostics.CodeAnalysis.RequiresDynamicCodeAttribute))]
#endif