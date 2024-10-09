
#pragma warning disable

#if !NET5_0_OR_GREATER

namespace System.Runtime.Versioning;

using Diagnostics;
using Diagnostics.CodeAnalysis;

/// <summary>
/// Records the platform that the project targeted.
/// </summary>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
[AttributeUsage(AttributeTargets.Assembly)]
#if PolyPublic
#endif
sealed class TargetPlatformAttribute(string platformName) :
    OSPlatformAttribute(platformName)
{
}

#endif