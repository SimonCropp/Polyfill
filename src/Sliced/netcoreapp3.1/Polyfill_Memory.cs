
#pragma warning disable

#if FeatureMemory

namespace Polyfills;

using System;
using System.Text;
using Link = System.ComponentModel.DescriptionAttribute;

static partial class Polyfill
{
#if !NET6_0_OR_GREATER

    /// <summary>
    /// Returns an enumeration of lines over the provided span.
    /// </summary>
    /// <remarks>
    /// It is recommended that protocol parsers not utilize this API. See the documentation
    /// for <see cref="string.ReplaceLineEndings"/> for more information on how newline
    /// sequences are detected.
    /// </remarks>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.enumeratelines#system-memoryextensions-enumeratelines(system-readonlyspan((system-char)))")]
    public static SpanLineEnumerator EnumerateLines(this ReadOnlySpan<char> target) =>
        new(target);

    /// <summary>
    /// Returns an enumeration of lines over the provided span.
    /// </summary>
    /// <remarks>
    /// It is recommended that protocol parsers not utilize this API. See the documentation
    /// for <see cref="string.ReplaceLineEndings"/> for more information on how newline
    /// sequences are detected.
    /// </remarks>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.enumeratelines#system-memoryextensions-enumeratelines(system-span((system-char)))")]
    public static SpanLineEnumerator EnumerateLines(this Span<char> target) =>
        new(target);

#endif

#if NETFRAMEWORK || NETSTANDARD || NETCOREAPP2X
#endif

}

#endif
