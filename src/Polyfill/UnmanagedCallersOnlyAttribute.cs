#if !NET
#pragma warning disable CS0649
#nullable enable

namespace System.Runtime.InteropServices;

// ReSharper disable RedundantNameQualifier
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
// ReSharper restore RedundantNameQualifier

/// <summary>
/// Any method marked with <see cref="System.Runtime.InteropServices.UnmanagedCallersOnlyAttribute" /> can be directly called from
/// native code. The function token can be loaded to a local variable using the <see href="https://docs.microsoft.com/dotnet/csharp/language-reference/operators/pointer-related-operators#address-of-operator-">address-of</see> operator
/// in C# and passed as a callback to a native method.
/// </summary>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
[AttributeUsage(
    AttributeTargets.Method,
    Inherited = false)]
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
#if PolyPublic
public
#endif
sealed class UnmanagedCallersOnlyAttribute :
    Attribute
{
    /// <summary>
    /// Optional. If omitted, the runtime will use the default platform calling convention.
    /// </summary>
    public Type[]? CallConvs;

    /// <summary>
    /// Optional. If omitted, no named export is emitted during compilation.
    /// </summary>
    public string? EntryPoint;
}
#else
[assembly: System.Runtime.CompilerServices.TypeForwardedTo(typeof(System.Runtime.InteropServices.UnmanagedCallersOnlyAttribute))]
#endif