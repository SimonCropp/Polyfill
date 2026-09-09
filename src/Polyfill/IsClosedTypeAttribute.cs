#if !NET11_0_OR_GREATER

namespace System.Runtime.CompilerServices;

using Diagnostics;
using Diagnostics.CodeAnalysis;

/// <summary>
/// Reserved for use by a compiler for tracking metadata.
/// This attribute should not be used by developers in source code.
/// </summary>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
//Link: https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.isclosedtypeattribute?view=net-11.0
#if PolyPublic
public
#endif
sealed class IsClosedTypeAttribute :
    Attribute
{
    Type[] derivedTypes = Type.EmptyTypes;

    /// <summary>
    /// Gets or sets the derived types of the closed type.
    /// A <see langword="null"/> value is normalized to an empty array.
    /// </summary>
    public Type[] DerivedTypes
    {
        get => derivedTypes;
        set => derivedTypes = value ?? Type.EmptyTypes;
    }
}

#else
[assembly: System.Runtime.CompilerServices.TypeForwardedTo(typeof(System.Runtime.CompilerServices.IsClosedTypeAttribute))]
#endif
