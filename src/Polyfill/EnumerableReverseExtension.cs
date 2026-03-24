#pragma warning disable

// This extension is in System.Linq (not Polyfills) because C# resolves extension methods from
// file-level `using` directives before `global using` directives. Without this, MemoryExtensions.Reverse(Span<T>)
// from `using System;` wins over a Polyfills-namespace polyfill from `global using Polyfills;`, causing a
// breaking change where array.Reverse() becomes void (in-place) instead of returning IEnumerable<T>.
// https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/breaking-changes/compiler%20breaking%20changes%20-%20dotnet%2010#enumerablereverse

#if !NET10_0_OR_GREATER

namespace System.Linq;

using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

[EditorBrowsable(EditorBrowsableState.Never)]
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
#if PolyPublic
public
#endif
static class PolyfillEnumerableReverseExtension
{
    /// <summary>Inverts the order of the elements in an array.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.reverse?view=net-11.0#system-linq-enumerable-reverse-1(-0())
    public static IEnumerable<TSource> Reverse<TSource>(this TSource[] source) =>
        Enumerable.Reverse(source);
}

#endif
