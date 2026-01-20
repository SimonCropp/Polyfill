#if !NET6_0 && !NET5_0

#if !NET7_0_OR_GREATER

#nullable enable

namespace System.Runtime.Versioning;

using Diagnostics;
using Diagnostics.CodeAnalysis;

using Targets = AttributeTargets;

/// <summary>
/// Marks APIs that were obsoleted in a given operating system version.
/// </summary>
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
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
#if PolyPublic
public
#endif
sealed class ObsoletedOSPlatformAttribute :
    OSPlatformAttribute
{
    public ObsoletedOSPlatformAttribute(string platformName) :
        base(platformName)
    {
    }

    public ObsoletedOSPlatformAttribute(string platformName, string? message) :
        base(platformName) =>
        Message = message;

    public string? Message { get; }
    public string? Url { get; set; }
}
#else
[assembly: System.Runtime.CompilerServices.TypeForwardedTo(typeof(System.Runtime.Versioning.ObsoletedOSPlatformAttribute))]
#endif
#endif