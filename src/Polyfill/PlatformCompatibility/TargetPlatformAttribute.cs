﻿// <auto-generated />
#pragma warning disable

#if !NET5_0_OR_GREATER

namespace System.Runtime.Versioning;

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

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