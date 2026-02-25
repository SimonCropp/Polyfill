#if !NET9_0_OR_GREATER

namespace System.Diagnostics;

// ReSharper disable once RedundantNameQualifier
using Diagnostics.CodeAnalysis;

/// <summary>
/// If a .NET Debugger is attached that supports the Debugger.BreakForUserUnhandledException(System.Exception) API, the debugger won't break on user-unhandled exceptions when the exception is caught by a method with this attribute, unless Debugger.BreakForUserUnhandledException(System.Exception) is called.
/// </summary>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
[AttributeUsage(AttributeTargets.Method)]
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
//Link: https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.debuggerdisableuserunhandledexceptionsattribute?view=net-11.0
#if PolyPublic
public
#endif
sealed class DebuggerDisableUserUnhandledExceptionsAttribute :
    Attribute;
#else
[assembly: System.Runtime.CompilerServices.TypeForwardedTo(typeof(System.Diagnostics.DebuggerDisableUserUnhandledExceptionsAttribute))]
#endif
