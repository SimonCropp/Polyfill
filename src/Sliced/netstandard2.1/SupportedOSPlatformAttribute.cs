
#pragma warning disable


namespace System.Runtime.Versioning;

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

using Targets = AttributeTargets;

/// <summary>
/// Records the operating system (and minimum version) that supports an API. Multiple attributes can be
/// applied to indicate support on multiple operating systems.
/// </summary>
/// <remarks>
/// Callers can apply a <see cref="SupportedOSPlatformAttribute " />
/// or use guards to prevent calls to APIs on unsupported operating systems.
///
/// A given platform should only be specified once.
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
sealed class SupportedOSPlatformAttribute :
    OSPlatformAttribute
{
    public SupportedOSPlatformAttribute(string platformName) :
        base(platformName)
    {
    }
}
