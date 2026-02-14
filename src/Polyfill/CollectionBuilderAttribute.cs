#if FeatureMemory
#if !NET8_0_OR_GREATER

namespace System.Runtime.CompilerServices;

using Diagnostics;
using Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, Inherited = false)]
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
//Link: https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.collectionbuilderattribute?view=net-11.0
#if PolyPublic
public
#endif
sealed class CollectionBuilderAttribute : Attribute
{
    /// <summary>Initialize the attribute to refer to the <paramref name="methodName"/> method on the <paramref name="builderType"/> type.</summary>
    public CollectionBuilderAttribute(Type builderType, string methodName)
    {
        BuilderType = builderType;
        MethodName = methodName;
    }

    /// <summary>Gets the type of the builder to use to construct the collection.</summary>
    public Type BuilderType { get; }

    /// <summary>Gets the name of the method on the builder to use to construct the collection.</summary>
    public string MethodName { get; }
}
#else
[assembly: System.Runtime.CompilerServices.TypeForwardedTo(typeof(System.Runtime.CompilerServices.CollectionBuilderAttribute))]
#endif
#endif
