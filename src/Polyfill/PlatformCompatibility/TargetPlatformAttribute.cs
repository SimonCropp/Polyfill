#if !NET5_0_OR_GREATER

// ReSharper disable RedundantUsingDirective
// ReSharper disable PartialTypeWithSinglePart

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace System.Runtime.Versioning;

/// <summary>
/// Records the platform that the project targeted.
/// </summary>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
[AttributeUsage(AttributeTargets.Assembly)]
#if PolyPublic
public
#endif
sealed class TargetPlatformAttribute :
    OSPlatformAttribute
{
    public TargetPlatformAttribute(string platformName) :
        base(platformName)
    {
    }
}

#endif