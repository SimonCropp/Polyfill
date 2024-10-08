
#pragma warning disable


#pragma warning disable

namespace System.Runtime.Versioning;

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Base type for all platform-specific API attributes.
/// </summary>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
abstract class OSPlatformAttribute :
    Attribute
{
    protected OSPlatformAttribute(string platformName) =>
        PlatformName = platformName;

    public string PlatformName { get; }
}

