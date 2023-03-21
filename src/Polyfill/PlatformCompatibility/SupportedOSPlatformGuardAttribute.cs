#if !NET6_0_OR_GREATER

#pragma warning disable CS0436

// ReSharper disable RedundantUsingDirective
// ReSharper disable PartialTypeWithSinglePart

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace System.Runtime.Versioning;

using Targets = AttributeTargets;

/// <summary>
/// Annotates a custom guard field, property or method with a supported platform name and optional version.
/// Multiple attributes can be applied to indicate guard for multiple supported platforms.
/// </summary>
/// <remarks>
/// Callers can apply a <see cref="SupportedOSPlatformGuardAttribute " /> to a field, property or method
/// and use that field, property or method in a conditional or assert statements in order to safely call platform specific APIs.
///
/// The type of the field or property should be boolean, the method return type should be boolean in order to be used as platform guard.
/// </remarks>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
[AttributeUsage(
    validOn: Targets.Field |
             Targets.Method |
             Targets.Property,
    AllowMultiple = true,
    Inherited = false)]
sealed class SupportedOSPlatformGuardAttribute :
    OSPlatformAttribute
{
    public SupportedOSPlatformGuardAttribute(string platformName) :
        base(platformName)
    {
    }
}

#endif