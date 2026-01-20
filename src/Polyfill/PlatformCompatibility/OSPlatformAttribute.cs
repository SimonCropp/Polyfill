#if !NET5_0_OR_GREATER

namespace System.Runtime.Versioning;

using Diagnostics;
using Diagnostics.CodeAnalysis;

/// <summary>
/// Base type for all platform-specific API attributes.
/// </summary>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
#if PolyPublic
public
#endif
abstract class OSPlatformAttribute(string platformName) :
    Attribute
{
    public string PlatformName { get; } = platformName;
}
#else
[assembly: System.Runtime.CompilerServices.TypeForwardedTo(typeof(System.Runtime.Versioning.OSPlatformAttribute))]
#endif