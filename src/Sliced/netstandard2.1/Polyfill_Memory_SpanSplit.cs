

#pragma warning disable

#if FeatureMemory

namespace Polyfills;
using System;
using System.Buffers;
using System.Diagnostics.CodeAnalysis;
using Link = System.ComponentModel.DescriptionAttribute;

static partial class Polyfill
{

#if !NET9_0_OR_GREATER
    

    /// <summary>
    /// Returns a type that allows for enumeration of each element within a split span
    /// using the provided separator character.
    /// </summary>
    /// <typeparam name="T">The type of the elements.</typeparam>
    /// <param name="source">The source span to be enumerated.</param>
    /// <param name="separator">The separator character to be used to split the provided span.</param>
    /// <returns>Returns a <see cref="SpanSplitEnumerator{T}"/>.</returns>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.split#system-memoryextensions-split-1(system-readonlyspan((-0))-0)")]
    public static SpanSplitEnumerator<T> Split<T>(this ReadOnlySpan<T> source, T separator)
        where T : IEquatable<T> =>
        new SpanSplitEnumerator<T>(source, separator);

    /// <summary>
    /// Returns a type that allows for enumeration of each element within a split span
    /// using the provided separator span.
    /// </summary>
    /// <typeparam name="T">The type of the elements.</typeparam>
    /// <param name="source">The source span to be enumerated.</param>
    /// <param name="separator">The separator span to be used to split the provided span.</param>
    /// <returns>Returns a <see cref="SpanSplitEnumerator{T}"/>.</returns>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.split#system-memoryextensions-split-1(system-readonlyspan((-0))-system-readonlyspan((-0)))")]
    public static SpanSplitEnumerator<T> Split<T>(this ReadOnlySpan<T> source, ReadOnlySpan<T> separator)
        where T : IEquatable<T> =>
        new SpanSplitEnumerator<T>(source, separator, treatAsSingleSeparator: true);

#if LangVersion13
#endif

#if NET8_0
#endif

#endif

}

#endif
