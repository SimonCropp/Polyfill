// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#if FeatureMemory
#if !NET6_0_OR_GREATER

namespace System.Text;

using Diagnostics;
using Diagnostics.CodeAnalysis;

/// <summary>
/// Enumerates the lines of a <see cref="ReadOnlySpan{Char}"/>.
/// </summary>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
//Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.spanlineenumerator?view=net-11.0
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
#if PolyPublic
public
#endif
ref struct SpanLineEnumerator
{
    ReadOnlySpan<char> remaining;
    bool isActive;
    ReadOnlySpan<char> newlines = "\r\f\u0085\u2028\u2029\n".AsSpan();

    internal SpanLineEnumerator(ReadOnlySpan<char> buffer)
    {
        remaining = buffer;
        Current = default;
        isActive = true;
    }

    public ReadOnlySpan<char> Current { get; private set; }

    public SpanLineEnumerator GetEnumerator() => this;

    public bool MoveNext()
    {
        if (!isActive)
        {
            return false;
        }

        //TODO: revisit when SearchValues is implemented
        var index = remaining.IndexOfAny(newlines);

        var remainingLength = (uint)remaining.Length;
        if ((uint)index < remainingLength)
        {
            var stride = 1;

            if (remaining[index] == '\r' &&
                (uint)(index + 1) < remainingLength &&
                remaining[index + 1] == '\n')
            {
                stride = 2;
            }

            Current = remaining.Slice(0, index);
            remaining = remaining.Slice(index + stride);
            return true;
        }

        Current = remaining;
        remaining = default;
        isActive = false;
        return true;
    }
}
#else
[assembly: System.Runtime.CompilerServices.TypeForwardedTo(typeof(System.Text.SpanLineEnumerator))]
#endif
#endif
