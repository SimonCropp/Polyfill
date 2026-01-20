#if !NET6_0 && !NET5_0
#if !NET6_0_OR_GREATER

namespace System.Runtime.Versioning;

using Diagnostics;
using Diagnostics.CodeAnalysis;

using Targets = AttributeTargets;

/// <summary>
/// Annotates a custom guard field, property or method with a supported platform name and optional version.
/// Multiple attributes can be applied to indicate guard for multiple supported platforms.
/// </summary>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
[AttributeUsage(
    validOn: Targets.Field |
             Targets.Method |
             Targets.Property,
    AllowMultiple = true,
    Inherited = false)]
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
#if PolyPublic
public
#endif
sealed class SupportedOSPlatformGuardAttribute(string platformName) :
    OSPlatformAttribute(platformName);
#else
[assembly: System.Runtime.CompilerServices.TypeForwardedTo(typeof(System.Runtime.Versioning.SupportedOSPlatformGuardAttribute))]
#endif
#endif