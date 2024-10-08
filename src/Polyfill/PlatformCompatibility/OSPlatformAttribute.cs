#if !NET7_0_OR_GREATER

namespace System.Runtime.Versioning;

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Base type for all platform-specific API attributes.
/// </summary>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
#if PolyPublic
public
#endif
abstract class OSPlatformAttribute :
    Attribute
{
    protected OSPlatformAttribute(string platformName) =>
        PlatformName = platformName;

    public string PlatformName { get; }
}

#endif