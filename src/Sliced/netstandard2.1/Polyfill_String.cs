
#pragma warning disable

namespace Polyfills;
using System;
using Link = System.ComponentModel.DescriptionAttribute;
using System.Text;

static partial class Polyfill
{
#if FeatureMemory && !NET6_0_OR_GREATER

    /// <summary>
    /// Copies the contents of this string into the destination span.
    /// </summary>
    /// <param name="destination">The span into which to copy this string's contents</param>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.string.copyto")]
    public static void CopyTo(this string target, Span<char> destination) =>
        target.AsSpan().CopyTo(destination);

    /// <summary>
    /// Copies the contents of this string into the destination span.
    /// </summary>
    /// <param name="destination">The span into which to copy this string's contents</param>
    /// <returns>true if the data was copied; false if the destination was too short to fit the contents of the string.</returns>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.string.trycopyto")]
    public static bool TryCopyTo(this string target, Span<char> destination) =>
        target.AsSpan().TryCopyTo(destination);
#endif

#if NETFRAMEWORK || NETSTANDARD2_0
#endif

#if NETFRAMEWORK || NETSTANDARD2_0 || NETCOREAPP2_0
#endif
}