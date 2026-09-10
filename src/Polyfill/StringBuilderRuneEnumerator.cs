#if NETCOREAPP3_0_OR_GREATER
#if !NET11_0_OR_GREATER

namespace System.Text;

using Collections;
using Collections.Generic;
using Diagnostics;
using Diagnostics.CodeAnalysis;

/// <summary>
/// Enumerates the <see cref="Rune"/>s of a <see cref="StringBuilder"/>.
/// </summary>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
//Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilderruneenumerator?view=net-11.0
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
#if PolyPublic
public
#endif
struct StringBuilderRuneEnumerator :
    IEnumerable<Rune>,
    IEnumerator<Rune>
{
    string content;
    StringRuneEnumerator inner;

    internal StringBuilderRuneEnumerator(StringBuilder builder)
    {
        content = builder.ToString();
        inner = content.EnumerateRunes();
    }

    public Rune Current => inner.Current;

    public StringBuilderRuneEnumerator GetEnumerator() => this;

    public bool MoveNext() => inner.MoveNext();

    object IEnumerator.Current => Current;

    void IEnumerator.Reset() => inner = content.EnumerateRunes();

    void IDisposable.Dispose()
    {
    }

    IEnumerator<Rune> IEnumerable<Rune>.GetEnumerator() => GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

#else
[assembly: System.Runtime.CompilerServices.TypeForwardedTo(typeof(System.Text.StringBuilderRuneEnumerator))]
#endif
#endif
