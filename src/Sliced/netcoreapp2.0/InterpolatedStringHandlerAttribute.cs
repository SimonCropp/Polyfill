
#pragma warning disable


namespace System.Runtime.CompilerServices;

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Link = System.ComponentModel.DescriptionAttribute;

using Targets = AttributeTargets;

/// <summary>
/// Indicates the attributed type is to be used as an interpolated string handler.
/// </summary>
[Link("https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.interpolatedstringhandlerargumentattribute")]
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
[AttributeUsage(
    validOn: Targets.Class |
             Targets.Struct,
    Inherited = false)]
sealed class InterpolatedStringHandlerAttribute :
    Attribute;
