#if !NET7_0_OR_GREATER

// ReSharper disable RedundantUsingDirective
// ReSharper disable PartialTypeWithSinglePart

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace System.Runtime.Versioning;

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

#endif