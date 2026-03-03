#if (NETFRAMEWORK || NETSTANDARD2_0) && !WINDOWS_UWP
#nullable enable

namespace System.Collections.Generic;

// ReSharper disable RedundantNameQualifier
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
// ReSharper restore RedundantNameQualifier

[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
// Provides the Create factory method for KeyValuePair<TKey, TValue>.
// https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.keyvaluepair?view=net-11.0
#if PolyPublic
public
#endif
static class KeyValuePair
{
    /// <summary>
    /// Creates a new key/value pair instance using provided values.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.keyvaluepair.create?view=net-11.0
    public static KeyValuePair<TKey, TValue> Create<TKey, TValue>(TKey key, TValue value) =>
        new(key, value);
}
#else
[assembly: System.Runtime.CompilerServices.TypeForwardedTo(typeof(System.Collections.Generic.KeyValuePair))]
#endif
