
#pragma warning disable


namespace System.Runtime.CompilerServices;

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

using Targets = AttributeTargets;

/// <summary>
/// Specifies that a type has required members or that a member is required.
/// </summary>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
[AttributeUsage(
    validOn: Targets.Class |
             Targets.Struct |
             Targets.Field |
             Targets.Property,
    Inherited = false)]
sealed class RequiredMemberAttribute :
    Attribute;