
#pragma warning disable


namespace System.Runtime.Versioning;

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Records the platform that the project targeted.
/// </summary>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
[AttributeUsage(AttributeTargets.Assembly)]
sealed class TargetPlatformAttribute :
    OSPlatformAttribute
{
    public TargetPlatformAttribute(string platformName) :
        base(platformName)
    {
    }
}
