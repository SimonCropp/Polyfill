#if !NET5_0_OR_GREATER

#pragma warning disable

#nullable enable

// ReSharper disable RedundantUsingDirective
// ReSharper disable PartialTypeWithSinglePart

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace System.Runtime.Versioning;

using Targets = AttributeTargets;

/// <summary>
/// Marks APIs that were removed in a given operating system version.
/// </summary>
/// <remarks>
/// Primarily used by OS bindings to indicate APIs that are only available in
/// earlier versions.
/// </remarks>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
[AttributeUsage(
    validOn: Targets.Assembly |
             Targets.Class |
             Targets.Constructor |
             Targets.Enum |
             Targets.Event |
             Targets.Field |
             Targets.Interface |
             Targets.Method |
             Targets.Module |
             Targets.Property |
             Targets.Struct,
    AllowMultiple = true,
    Inherited = false)]
#if PolyPublic
public
#endif
sealed class UnsupportedOSPlatformAttribute :
    OSPlatformAttribute
{
    public UnsupportedOSPlatformAttribute(string platformName) :
        base(platformName)
    {
    }

    public UnsupportedOSPlatformAttribute(string platformName, string? message) :
        base(platformName) =>
        Message = message;

    public string? Message { get; }
}

#endif