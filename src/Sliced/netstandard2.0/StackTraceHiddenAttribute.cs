
#pragma warning disable


namespace System.Diagnostics;

using System.Diagnostics.CodeAnalysis;
using Targets = AttributeTargets;

/// <summary>
/// Types and Methods attributed with StackTraceHidden will be omitted from the stack trace text shown in StackTrace.ToString()
/// and Exception.StackTrace
/// </summary>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
[AttributeUsage(
    validOn: Targets.Class |
             Targets.Method |
             Targets.Constructor |
             Targets.Struct,
    Inherited = false)]
sealed class StackTraceHiddenAttribute :
    Attribute;