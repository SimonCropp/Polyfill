#if FeatureMemory && FeatureValueTuple

namespace Polyfills;
using System;
using System.Buffers;
using System.Diagnostics.CodeAnalysis;

static partial class Polyfill
{

#if !NET9_0_OR_GREATER
    //https://github.com/bbartels/runtime/blob/master/src/libraries/System.Private.CoreLib/src/System/MemoryExtensions.cs

    /// <summary>
    /// Returns a type that allows for enumeration of each element within a split span
    /// using the provided separator character.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.split?view=net-11.0#system-memoryextensions-split-1(system-readonlyspan((-0))-0)
    public static SpanSplitEnumerator<T> Split<T>(this ReadOnlySpan<T> source, T separator)
        where T : IEquatable<T> =>
        new(source, separator);

    /// <summary>
    /// Returns a type that allows for enumeration of each element within a split span
    /// using the provided separator span.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.split?view=net-11.0#system-memoryextensions-split-1(system-readonlyspan((-0))-system-readonlyspan((-0)))
    public static SpanSplitEnumerator<T> Split<T>(this ReadOnlySpan<T> source, ReadOnlySpan<T> separator)
        where T : IEquatable<T> =>
        new(source, separator, treatAsSingleSeparator: true);

    /// <summary>
    /// Returns a type that allows for enumeration of each element within a split span
    /// using any of the provided elements.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.splitany?view=net-11.0#system-memoryextensions-splitany-1(system-readonlyspan((-0))-system-readonlyspan((-0)))
    public static SpanSplitEnumerator<T> SplitAny<T>(this ReadOnlySpan<T> source, [UnscopedRef] params ReadOnlySpan<T> separators)
        where T : IEquatable<T> =>
        new(source, separators);

#if NET8_0

    /// <summary>
    /// Returns a type that allows for enumeration of each element within a split span
    /// using the provided <see cref="SpanSplitEnumerator{T}"/>.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.splitany?view=net-11.0#system-memoryextensions-splitany-1(system-readonlyspan((-0))-system-buffers-searchvalues((-0)))
    public static SpanSplitEnumerator<T> SplitAny<T>(this ReadOnlySpan<T> source, SearchValues<T> separators)
        where T : IEquatable<T> =>
        new(source, separators);

#endif

#endif

}

#endif
