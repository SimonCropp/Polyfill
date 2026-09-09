#if !NET11_0_OR_GREATER
#nullable enable

namespace System.Runtime.CompilerServices;

/// <summary>
/// Provides a common interface for accessing the contents of a union type at runtime.
/// </summary>
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
//Link: https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.iunion?view=net-11.0
#if PolyPublic
public
#endif
interface IUnion
{
    /// <summary>
    /// Gets the value contained in the union, or <see langword="null"/> if the union has no value.
    /// </summary>
    object? Value { get; }
}

#else
[assembly: System.Runtime.CompilerServices.TypeForwardedTo(typeof(System.Runtime.CompilerServices.IUnion))]
#endif
